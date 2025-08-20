


//using Microsoft.AspNetCore.Mvc;
//using System.Net;
//using System.Net.Mail;
//using Gred.Models;


//namespace Gred.Controllers
//{
//  [Route("api/[controller]")]
//  [ApiController]
//  public class EmailController : ControllerBase
//  {
//    [HttpPost]
//    public IActionResult SendEmail([FromBody] Case caseModel)
//    {
//      try
//      {
//        using var smtpClient = new SmtpClient("smtp.gmail.com", 587)
//        {
//          UseDefaultCredentials = false,
//          EnableSsl = true,
//          Credentials = new NetworkCredential(
//                "akashdey1718@gmail.com",  // Gmail
//                "frtl lphl kust uopj"      // Gmail App Password
//            ),
//          Timeout = 20000
//        };

//        var mailMessage = new MailMessage
//        {
//          From = new MailAddress("akashdey1718@gmail.com", "Akash Dey"),
//          Subject = caseModel.Subject ?? $"Case Update - Patient ID {caseModel.PatientId}",
//          Body =
//                $"<p><b>Patient ID:</b> {caseModel.PatientId}</p>" +
//                $"<p><b>Date:</b> {caseModel.Date?.ToString("yyyy-MM-dd")}</p>" +
//                $"<p><b>Stage:</b> {(caseModel.Stage == 1 ? "Follow-up One" : caseModel.Stage == 2 || caseModel.Stage == 3 ? "Follow-up Two" : "Baseline")}</p>" +
//                $"<hr/>" +
//                $"<p>{caseModel.Body}</p>",
//          IsBodyHtml = true
//        };

//        // Recipient email
//        mailMessage.To.Add(!string.IsNullOrWhiteSpace(caseModel.Email)
//            ? caseModel.Email
//            : "akashdeypersonal17@gmail.com");

//        smtpClient.Send(mailMessage);

//        return Ok("Email sent successfully.");
//      }
//      catch (Exception ex)
//      {
//        return StatusCode(500, $"Failed to send email: {ex.Message}");
//      }
//    }
//  }
//}

using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Net.Mail;
using Gred.Models;

namespace Gred.Controllers
{
  [Route("api/[controller]")]
  [ApiController]
  public class EmailController : ControllerBase
  {
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

        //if (caseModel.Stage == 1) // Follow-up One
        //{
        //  ScheduleMail(caseModel, TimeSpan.FromMinutes(1));   // instead of 20 days
        //  ScheduleMail(caseModel, TimeSpan.FromMinutes(2));   // instead of 40 days
        //  ScheduleMail(caseModel, TimeSpan.FromMinutes(3));   // instead of 60 days
        //}
        //else if (caseModel.Stage == 2 || caseModel.Stage == 3) // Follow-up Two
        //{
        //  ScheduleMail(caseModel, TimeSpan.FromMinutes(1));   // instead of 90 days
        //  ScheduleMail(caseModel, TimeSpan.FromMinutes(2));   // instead of 180 days
        //}


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
      using var smtpClient = new SmtpClient("smtp.gmail.com", 587)
      {
        UseDefaultCredentials = false,
        EnableSsl = true,
        Credentials = new NetworkCredential(
                "akashdey1718@gmail.com",
                "frtl lphl kust uopj"  // Gmail App Password
            ),
        Timeout = 20000
      };

      var mailMessage = new MailMessage
      {
        From = new MailAddress("akashdey1718@gmail.com", "Akash Dey"),
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
          : "akashdeypersonal17@gmail.com");

      smtpClient.Send(mailMessage);
    }
  }
}




