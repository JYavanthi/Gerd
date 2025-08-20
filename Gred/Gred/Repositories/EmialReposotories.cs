//using System.Net;
//using System.Net.Mail;
//using System.Threading.Tasks;

//namespace gred.Repositories
//{
//  public class EmailRepository : IEmailRepository
//  {
//    public async Task<bool> SendEmailAsync(string to, string? cc, string? subject, string? body)
//    {
//      try
//      {
//        using (var smtpClient = new SmtpClient("smtp.gmail.com", 587))
//        {
//          smtpClient.UseDefaultCredentials = false;
//          smtpClient.Credentials = new NetworkCredential("noreply@microlabs.in", "Password@1");
//          smtpClient.EnableSsl = true;

//          using (var mailMessage = new MailMessage())
//          {
//            mailMessage.From = new MailAddress("noreply@microlabs.in");
//            mailMessage.To.Add(to);

//            if (!string.IsNullOrEmpty(cc))
//              mailMessage.CC.Add(cc);

//            mailMessage.Subject = subject ?? "";
//            mailMessage.Body = body ?? "";
//            mailMessage.IsBodyHtml = true;

//            await smtpClient.SendMailAsync(mailMessage);
//          }
//        }

//        return true;
//      }
//      catch
//      {
//        // log exception here
//        return false;
//      }
//    }
//  }
//}
