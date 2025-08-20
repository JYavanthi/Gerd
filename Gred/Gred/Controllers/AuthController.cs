using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Gred.Data;
using Gred.Data.Entities.Common;
using Gred.Services.Interface;
using Microsoft.EntityFrameworkCore;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using gred.Models;
using Microsoft.AspNetCore.Authorization;
using gred.Data;
using Gred.Data.Entities;


namespace Gred.Controllers
{
  [Route("api/[controller]")]
  [ApiController]

  public class AuthController : ControllerBase
  {
    private readonly ILogin _authService;
    private readonly IConfiguration _configuration;
    private readonly GredDbContext _context;

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
