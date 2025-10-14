using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System.Net;
using System.Net.Mail;
using Gred.Models;

namespace Gred.Controllers
{
  [Route("api/[controller]")]
  [ApiController]
  public class EmailController : ControllerBase
  {

    private static IConfiguration _config;
    private static string smtpServer = "";
    private static int smtpPort = 0;
    private static string smtpUser = "";
    private static string smtpPass = "";
    private static bool enableSsl;


    [HttpPost]
    public IActionResult SendEmail([FromBody] Case caseModel)
    {
      try
      {
        // send immediately
        SendMail(caseModel);

        // schedule based on stage
        if (caseModel.Stage == 1) // Follow-up One
        {
          ScheduleMail(caseModel, TimeSpan.FromDays(20));
          ScheduleMail(caseModel, TimeSpan.FromDays(40));
          ScheduleMail(caseModel, TimeSpan.FromDays(60));
        }
        else if (caseModel.Stage == 2 || caseModel.Stage == 3) // Follow-up Two
        {
          ScheduleMail(caseModel, TimeSpan.FromDays(90));
          ScheduleMail(caseModel, TimeSpan.FromDays(180));
        }

        return Ok("Emails scheduled (in-memory).");
      }
      catch (Exception ex)
      {
        return StatusCode(500, $"Failed to send/schedule email: {ex.Message}");
      }
    }

    private void ScheduleMail(Case caseModel, TimeSpan delay)
    {
      Task.Run(async () =>
      {
        await Task.Delay(delay); // wait before sending
        SendMail(caseModel);
      });
    }

    private void SendMail(Case caseModel)
    {
      _config = new ConfigurationBuilder()
            .SetBasePath(AppContext.BaseDirectory) // <-- Here
            .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
            .Build();
      smtpServer = _config["Smtp:Server"];
      smtpPort = int.Parse(_config["Smtp:Port"]);
      smtpUser = _config["Smtp:User"];
      smtpPass = _config["Smtp:Pass"];
      enableSsl = bool.Parse(_config["Smtp:EnableSsl"]);

      Console.WriteLine("SMTP Server: " + smtpServer);
      Console.WriteLine("smtpPort: " + smtpPort);
      Console.WriteLine("smtpUser: " + smtpUser);
      Console.WriteLine("smtpPass: " + smtpPass);
      Console.WriteLine("enableSsl: " + enableSsl);

      ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;

      using var smtpClient = new SmtpClient(smtpServer, smtpPort)
      {
        UseDefaultCredentials = false,
        EnableSsl = enableSsl,
        Credentials = new NetworkCredential(smtpUser, smtpPass),
        Timeout = 50000
      };

      var mailMessage = new MailMessage
      {
        From = new MailAddress(smtpUser),
        Subject = caseModel.Subject ?? $"Case Update - Patient ID {caseModel.PatientId}",
        Body =
              $"<p><b>Patient ID:</b> {caseModel.PatientId}</p>" +
              $"<p><b>Date:</b> {caseModel.Date?.ToString("yyyy-MM-dd")}</p>" +
              $"<p><b>Stage:</b> {(caseModel.Stage == 1 ? "Follow-up One" : caseModel.Stage == 2 || caseModel.Stage == 3 ? "Follow-up Two" : "Baseline")}</p>" +
              $"<hr/>" +
              $"<p>{caseModel.Body}</p>",
        IsBodyHtml = true
      };

      mailMessage.To.Add(!string.IsNullOrWhiteSpace(caseModel.Email)
          ? caseModel.Email
          : smtpUser);

      smtpClient.Send(mailMessage);
    }
  }
}




