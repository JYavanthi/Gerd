using System;
using System.Collections.Generic;

namespace gred.Models;

public partial class VwGadget
{
    public string? Initial { get; set; }

    public string? SubjectNo { get; set; }

    public DateTime? Date { get; set; }

    public int? Age { get; set; }

    public string? Gender { get; set; }

    public string? FamilyIncome { get; set; }

    public string? Occupation { get; set; }

    public string? Education { get; set; }

    public int? Pincode { get; set; }

    public int CityId { get; set; }

    public string City { get; set; } = null!;

    public short StateId { get; set; }

    public string State { get; set; } = null!;

    public string Zone { get; set; } = null!;

    public bool ComputerUsed { get; set; }

    public string? ComputerFrequency { get; set; }

    public int? ComputerDurationYears { get; set; }

    public bool SmartphoneUsed { get; set; }

    public string? SmartphoneFrequency { get; set; }

    public int? SmartphoneDurationYears { get; set; }

    public string? WorkingHours { get; set; }

    public string? JobType { get; set; }

    public int? TotalWorkingYears { get; set; }

    public int? Stage { get; set; }
}
