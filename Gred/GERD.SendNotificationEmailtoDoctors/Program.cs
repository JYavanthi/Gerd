using GERD.SendNotificationEmailtoDoctors.Modal;
using MailKit;
using MailKit.Net.Smtp;
using MailKit.Security;
using Microsoft.Extensions.Configuration;
using Microsoft.VisualBasic;
using MimeKit;
using MimeKit.Text;
using System;
using System.IO;
using System.Net;
using System.Net.Http.Json;
using System.Net.Mail;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using static Org.BouncyCastle.Math.EC.ECCurve;


namespace AutoEmailNotification
{


  internal class Program
  {
    private static IConfiguration _config;
    // Class-level static fields
    private static string baseApiUrl = "";
    private static string smtpServer = "";
    private static int smtpPort = 0;
    private static string smtpUser = "";
    private static string smtpPass = "";
    private static bool enableSsl ;

    private static string logFilePath = Path.Combine(AppContext.BaseDirectory, "GERDSendNotificationlog.txt");
    static async Task Main()
    {
      // Redirect all Console.WriteLine output to a file (and keep showing in console)
      var logStream = new StreamWriter(logFilePath, append: true) { AutoFlush = true };
      Console.SetOut(new MultiTextWriter(Console.Out, logStream));
      Console.SetError(new MultiTextWriter(Console.Error, logStream));

      Console.WriteLine("--------------------------------------------------");
      Console.WriteLine("Task started at: " + DateTime.Now);

      _config = new ConfigurationBuilder()
            .SetBasePath(AppContext.BaseDirectory) // <-- Here
            .AddJsonFile("AppSettings.json", optional: false, reloadOnChange: true)
            .Build();

      baseApiUrl = _config["BaseApiUrl"];
      smtpServer = _config["Smtp:Server"];
      smtpPort = int.Parse(_config["Smtp:Port"]);
      smtpUser = _config["Smtp:User"];
      smtpPass = _config["Smtp:Pass"];
      enableSsl = bool.Parse(_config["Smtp:EnableSsl"]);

      Console.WriteLine("API URL: " + baseApiUrl);
      Console.WriteLine("SMTP Server: " + smtpServer);
      Console.WriteLine("---- Daily Reminder Job Started ----");
      Console.WriteLine("smtpPort: " + smtpPort);
      Console.WriteLine("smtpUser: " + smtpUser);
      //Console.WriteLine("smtpPass: " + smtpPass);
      Console.WriteLine("enableSsl: " + enableSsl);

      var today = DateTime.Now;
      var http = new HttpClient();

      try
      {
        var doctors = await http.GetFromJsonAsync<List<Doctor>>($"{baseApiUrl}/DoctorReport/DownloadDoctorReport");
        var result = await http.GetFromJsonAsync<CommonResult<List<Patient>>>($"{baseApiUrl}/PatientReg/GetPatient");
        var cases = result?.Data ?? new List<Patient>();

        if (doctors == null || cases == null)
        {
          Console.WriteLine("Failed to load doctors or patient data");
          return;
        }

        foreach (var doctor in doctors)
        {
          var patientsForDoctor = cases.Where(p => p.DoctorId == doctor.DoctorId).ToList();
          var duePatients = new List<(Patient, string, DateTime, DateTime, int, string)>();

          foreach (var p in patientsForDoctor)
          {
            DateTime dueDate, createdDate;
            string stageText = "", subjectNo = "";
            bool isDue = false;
            bool shouldSend = false;

            // --- Determine Stage and Due Date ---
            if (p.Stage == 0)
            {
              dueDate = p.CreatedDt.AddDays(16);
              createdDate = p.CreatedDt;
              stageText = "Baseline";
              isDue = dueDate <= today;
              subjectNo = p.SubjectNo;
            }
            else if (p.Stage == 1 && p.BlSubmitted != null)
            {
              dueDate = p.BlSubmitted.Value.AddDays(46);
              createdDate = p.BlSubmitted.Value;
              stageText = "Follow-up 1";
              isDue = dueDate <= today;
              subjectNo = p.SubjectNo;
            }
            else if (p.Stage == 3 && p.Fu1Submitted != null)
            {
              dueDate = p.Fu1Submitted.Value.AddDays(76);
              createdDate = p.Fu1Submitted.Value;
              stageText = "Follow-up 2";
              isDue = dueDate <= today;
              subjectNo = p.SubjectNo;
            }
            else continue;

            if (!isDue) continue;

            int dueDays = (int)Math.Ceiling((today - dueDate).TotalDays);

            // --- Reminder Logic ---
            var reminderLog = await GetReminderLog(http, p.PatientId, p.Stage);

            if (reminderLog == null)
            {
              shouldSend = true;
              reminderLog = new EmailReminderLog
              {
                PatientId = p.PatientId,
                DoctorId = doctor.DoctorId,
                Stage = p.Stage,
                ReminderCount = 1,
                LastSentDate = today,
                DueDays = dueDays,
                InitationOrSubmittedDate = createdDate
              };
            }
            else if (reminderLog.ReminderCount == 1 && reminderLog.LastSentDate <= today.AddDays(-7))
            {
              shouldSend = true;
              reminderLog.ReminderCount = 2;
              reminderLog.LastSentDate = today;
              reminderLog.DueDays = dueDays;
            }

            if (shouldSend)
            {
              // Add this patient to list for combined email
              duePatients.Add((p, stageText, createdDate, dueDate, dueDays, subjectNo));

              // Save or update reminder log now
              await SaveOrUpdateReminder(http, reminderLog);

              Console.WriteLine($"Added {p.Initial} ({stageText}) for Dr. {doctor.Name} - Reminder #{reminderLog.ReminderCount}");
            }

          }

          // --- Send One Combined Email per Doctor ---
          if (duePatients.Any())
          {
            string emailBody = BuildEmailBody(doctor, duePatients);
            await SendEmailAsync(
                doctor.Email,
                $"{duePatients.Count} Patient(s) Due Reminder - {DateTime.Now:dd-MMM-yyyy}",
                emailBody
            );

            Console.WriteLine($" Sent one combined email to Dr. {doctor.Name} ({duePatients.Count} patients)");
            await Task.Delay(2000); // optional delay for Gmail throttling
          }
          else
          {
            Console.WriteLine("No patients are due to get reminders");
          }
        }

        Console.WriteLine("---- Reminder Job Completed Successfully ----" + DateTime.Now);
      }
      catch (Exception ex)
      {
        Console.WriteLine($" Error: {ex.Message}");
        throw;
      }
    }


    static string BuildEmailBody(Doctor doctor, List<(Patient patient, string stage, DateTime CreatedDate, DateTime dueDate, int dueDays, string subjectNo)> list)
    {
      var body = $@"<p>Dear Dr. {doctor.Name},</p>
        <p>This is an automatic reminder for the following due patient(s):</p>
        <table border='1' cellpadding='6' cellspacing='0'>
            <tr><th>Patient Initial</th><th>SubjectNo</th><th>Stage</th><th>Created/SubmittedDate</th><th>Due Date</th><th>Overdue (Days)</th></tr>";

      foreach (var (patient, stage, createdDate, dueDate, dueDays, subjectNo) in list)
      {
        body += $"<tr><td>{patient.Initial}</td><td>{subjectNo}</td><td>{stage}</td><td>{createdDate:yyyy-MM-dd}</td><td>{dueDate:yyyy-MM-dd}</td><td>{dueDays}</td></tr>";
      }

      body += "</table><br/><p>Could you please take action accordingly.<br/></p>";

      body += "</table><br/><p>Best regards,<br/>Admin</p>";
      return body;
    }


    public static async Task SendEmailAsync(string to, string subject, string htmlBody)
    {
      try
      {

        using var smtpClient = new System.Net.Mail.SmtpClient(smtpServer, smtpPort)
        {
          UseDefaultCredentials = false,
          EnableSsl = enableSsl,
          Credentials = new NetworkCredential(smtpUser, smtpPass),
          Timeout = 50000
        };

        var mailMessage = new MailMessage
        {
          From = new MailAddress(_config["Smtp:User"]),
          Subject = subject,
          Body = htmlBody,
          IsBodyHtml = true
        };

        mailMessage.To.Add(!string.IsNullOrWhiteSpace(to) ? to : smtpUser);

        // Wrap Send() in Task.Run() to avoid blocking
        smtpClient.Send(mailMessage);

        Console.WriteLine($" Email sent successfully to {to}");
      }
      catch (SmtpException ex)
      {
        Console.WriteLine($" SMTP Error: {ex.Message}");
        throw;
      }
      catch (Exception ex)
      {
        Console.WriteLine($" General Error sending email: {ex.Message}");
        throw;
      }
    }

    static async Task<EmailReminderLog?> GetReminderLog(HttpClient http, int patientId, int stage)
    {
      Console.WriteLine($"{baseApiUrl}/EmailReminder/GetByPatient/{patientId}/{stage}");
      var response = await http.GetAsync($"{baseApiUrl}/EmailReminder/GetByPatient/{patientId}/{stage}");


      if (response.IsSuccessStatusCode)
      {
        return await response.Content.ReadFromJsonAsync<EmailReminderLog>();
      }

      //Return null if 404 or any failure
      return null;
    }

    static async Task SaveOrUpdateReminder(HttpClient http, EmailReminderLog log)
    {
      await http.PostAsJsonAsync($"{baseApiUrl}/EmailReminder/SaveOrUpdate", log);
    }

    // Helper class to write to multiple outputs
    public class MultiTextWriter : TextWriter
    {
      private readonly TextWriter[] writers;

      public MultiTextWriter(params TextWriter[] writers)
      {
        this.writers = writers;
      }

      public override Encoding Encoding => Encoding.UTF8;

      public override void WriteLine(string value)
      {
        foreach (var w in writers)
        {
          w.WriteLine(value);
        }
      }

      public override void Write(char value)
      {
        foreach (var w in writers)
        {
          w.Write(value);
        }
      }
    }

  }
}
