using gred.Data;
using gred.Models;
using Gred.Data;
using Gred.Data.Entities;
using Gred.Data.Entities.Common;
using Gred.Models.Data.Entities;
using Gred.Services.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Net;
using System.Net.Mail;
using System.Security.Claims;
using System.Text;


namespace Gred.Controllers
{
  [Route("api/[controller]")]
  [ApiController]

  public class AuthController : ControllerBase
  {
    private readonly ILogin _authService;
    private readonly IConfiguration _configuration;
    private readonly GredDbContext _context;
    private static IConfiguration _config;

    public AuthController(ILogin authService, IConfiguration configuration, GredDbContext context)
    {
      _authService = authService;
      _configuration = configuration;
      this._context = context;
    }
    [HttpPost("login")]
    [AllowAnonymous]
    public async Task<IActionResult> Login([FromBody] ELogin login)
    {
      CommonRsult result = new CommonRsult();
      try
      {
        var user = login.MobileNo != ""
            ? await _authService.AuthenticateDoctor(login.Email, login.MobileNo, login.Password)
            : await _authService.AuthenticateDoctor(login.Email, login.MobileNo, login.Password);

        // Log user data for debugging
        Console.WriteLine($"User data: {user?.Data}");

        // Check if user.Data is null or empty
        //if (user == null || user.Data == null)
        //{
        //    return Unauthorized(new { message = "Invalid credentials" });
        //}

        var userData = user.Data as List<gred.Models.VwDoctor>;

        // Check if userData is null or empty
        if (userData == null || userData.Count == 0)
        {
          return Unauthorized(new { message = "Invalid credentials" });
        }

        var firstUser = userData.FirstOrDefault();

        // Check if firstUser is null
        if (firstUser == null)
        {
          return Unauthorized(new { message = "Access denied: Only admin can log in." });
        }

        var token = GenerateJwtToken(user);

        return Ok(new
        {
          Token = token,
          Message = "Admin login successful",
          userData = firstUser
        });
      }
      catch (Exception ex)
      {
        result.Type = "E";
        result.Message = ex.Message;
        return StatusCode(500, new { message = "An error occurred while processing the request.", error = result.Message });
      }
    }

    // ========== FORGOT PASSWORD ==========
    [HttpPost("forgot-password")]
    public async Task<IActionResult> ForgotPassword([FromBody] EForgotPasswordRequest request)
    {
      try
      {
        if (string.IsNullOrEmpty(request.Email))
          return BadRequest(new { message = "Email is required." });

        var doctor = await _context.Doctors.FirstOrDefaultAsync(d => d.Email == request.Email);
        if (doctor == null)
          return NotFound(new { message = "No doctor found with this email." });

        // Generate token and expiry
        var token = Guid.NewGuid().ToString();
        doctor.ResetToken = token;
        doctor.ResetTokenExpires = DateTime.UtcNow.AddMinutes(15); // valid 15 min

        await _context.SaveChangesAsync();

        // Build reset link
        var frontendUrl = _configuration["FrontendUrl"] ?? "https://localhost:4200";
        var resetLink = $"{frontendUrl}/reset-password?token={token}";

        // Build email content
        var subject = "Reset Your Password";
        var body = $@"
      <p>Hello {doctor.Name},</p>
      <p>You requested to reset your password. Click the link below:</p>
      <p><a href='{resetLink}'>Reset Password</a></p>
      <p>This link will expire in 15 minutes.</p>
      <br/>
      <p> Best regards,<br/> GERD Registry </p>";

        // Send email
        await SendEmailAsync(doctor.Email, subject, body);

        return Ok(new { message = "Password reset link has been sent to your email." });
      }
      catch (Exception ex)
      {
        return StatusCode(500, new { message = "An error occurred while processing the request.", error = ex.Message });
      }
    }

    // ========== RESET PASSWORD ==========
    [HttpPost("reset-password")]
    public async Task<IActionResult> ResetPassword([FromBody] EResetPasswordRequest request)
    {
      try
      {
        if (string.IsNullOrEmpty(request.Token) || string.IsNullOrEmpty(request.NewPassword))
          return BadRequest(new { message = "Token and new password are required." });

        var doctor = await _context.Doctors.FirstOrDefaultAsync(d =>
            d.ResetToken == request.Token &&
            d.ResetTokenExpires > DateTime.UtcNow);

        if (doctor == null)
          return BadRequest(new { message = "Invalid or expired reset token." });

        // Update password
        doctor.Password = request.NewPassword; // ideally hash it
        doctor.ResetToken = null;
        doctor.ResetTokenExpires = null;
        doctor.ModifiedDt = DateTime.UtcNow;

        await _context.SaveChangesAsync();

        return Ok(new { message = "Password has been successfully reset." });
      }
      catch (Exception ex)
      {
        return StatusCode(500, new { message = "An error occurred while processing the request.", error = ex.Message });
      }
    }

    private async Task SendEmailAsync(string toEmail, string subject, string body)
    {
      try
      {
        _config = new ConfigurationBuilder()
              .SetBasePath(AppContext.BaseDirectory) // <-- Here
              .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
              .Build();
        var smtpServer = _configuration["Smtp:Server"];
        var smtpPort = int.Parse(_configuration["Smtp:Port"]);
        var smtpUser = _configuration["Smtp:User"];
        var smtpPass = _configuration["Smtp:Pass"];

        using (var client = new SmtpClient(smtpServer, smtpPort))
        {
          client.Credentials = new NetworkCredential(smtpUser, smtpPass);
          client.EnableSsl = true;

          var mailMessage = new MailMessage
          {
            From = new MailAddress(smtpUser, "GERD"),
            Subject = subject,
            Body = body,
            IsBodyHtml = true
          };
          mailMessage.To.Add(toEmail);

          await client.SendMailAsync(mailMessage);
        }
      }
      catch (Exception ex)
      {
        Console.WriteLine($"Failed to send email: {ex.Message}");
        throw; // rethrow to let caller handle it
      }
    }

    private string GenerateJwtToken(CommonRsult user)
    {
      var keyString = _configuration["Jwt:Key"];
      if (string.IsNullOrEmpty(keyString))
      {
        Console.WriteLine("JWT Key is missing from configuration!");
        return string.Empty;
      }
      var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(keyString));
      var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

      if (user.Data is not List<gred.Models.VwDoctor> userData || userData.Count == 0)
      {
        Console.WriteLine("No valid user data found in CommonRsult!");
        return string.Empty;
      }
      var firstUser = userData.First();
      if (firstUser == null)
      {
        Console.WriteLine("First user is null!");
        return string.Empty;
      }

      var claims = new[]
      {
        new Claim(JwtRegisteredClaimNames.Sub, firstUser.Name ?? ""),
        new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
        new Claim("UserId", firstUser.DoctorId.ToString()),
    };
      var token = new JwtSecurityToken(
          issuer: _configuration["Jwt:Issuer"],
          audience: _configuration["Jwt:Audience"],
          claims: claims,
          expires: DateTime.UtcNow.AddHours(2),
          signingCredentials: credentials
      );

      var tokenString = new JwtSecurityTokenHandler().WriteToken(token);
      return tokenString;
    }

  }
}
