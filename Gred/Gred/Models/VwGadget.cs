using System;
using System.Collections.Generic;

namespace gred.Models;

public partial class VwGadget
{
    public int Id { get; set; }

    public int PatientId { get; set; }

    public bool ComputerUsed { get; set; }

    public string? ComputerFrequency { get; set; }

    public int? ComputerDurationYears { get; set; }

    public bool SmartphoneUsed { get; set; }

    public string? SmartphoneFrequency { get; set; }

    public int? SmartphoneDurationYears { get; set; }

    public string? WorkingHours { get; set; }

    public string? JobType { get; set; }

    public int? TotalWorkingYears { get; set; }

    public DateTime? CreatedAt { get; set; }

    public DateTime? ModifiedAt { get; set; }

    public int? Stage { get; set; }
}
