using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GERD.SendNotificationEmailtoDoctors.Modal
{
  internal class EmailReminderLog
  {
    public int PatientId { get; set; }
    public int DoctorId { get; set; }
    public int Stage { get; set; }
    public int ReminderCount { get; set; }
    public DateTime LastSentDate { get; set; }
    public DateTime InitationOrSubmittedDate { get; set; }
    public int? DueDays { get; set; }

  }
}
