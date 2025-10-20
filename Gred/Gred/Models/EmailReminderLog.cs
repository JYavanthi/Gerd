using System;
using System.Collections.Generic;

namespace gred.Models;

public partial class EmailReminderLog
{
    public int EmailReminderId { get; set; }

    public int PatientId { get; set; }

    public int Stage { get; set; }

    public int DoctorId { get; set; }

    public int ReminderCount { get; set; }

    public DateTime LastSentDate { get; set; }

    public DateTime InitationOrSubmittedDate { get; set; }

    public int? DueDays { get; set; }
}
