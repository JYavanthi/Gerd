using System;
using System.Collections.Generic;

namespace gred.Models;

public partial class PatientHistoryBackup
{
    public int PatientHistoryId { get; set; }

    public string? AdIntake { get; set; }

    public string? AdFrequency { get; set; }

    public string? AdQuantity { get; set; }

    public string? AdDuration { get; set; }

    public string? CfIntake { get; set; }

    public string? CfFrequency { get; set; }

    public string? CfQuantity { get; set; }

    public string? CfDuration { get; set; }

    public string? TIntake { get; set; }

    public string? TFrequency { get; set; }

    public string? TQuantity { get; set; }

    public string? TDuration { get; set; }

    public string? SfIntake { get; set; }

    public string? SfFrequency { get; set; }

    public string? SfQuantity { get; set; }

    public string? SfDuration { get; set; }

    public string? AhIntake { get; set; }

    public string? AhFrequency { get; set; }

    public string? AhQuantity { get; set; }

    public string? AhDuration { get; set; }

    public string? CsIntake { get; set; }

    public string? CsFrequency { get; set; }

    public string? CsQuantity { get; set; }

    public string? CsDuration { get; set; }

    public string? SIntake { get; set; }

    public string? SFrequency { get; set; }

    public string? SQuantity { get; set; }

    public string? SDuration { get; set; }

    public string? TbIntake { get; set; }

    public string? TbFrequency { get; set; }

    public string? TbQuantity { get; set; }

    public string? TbDuration { get; set; }

    public string? GName { get; set; }

    public string? GUsage { get; set; }

    public string? GFrequency { get; set; }

    public string? GYearOfUsage { get; set; }

    public string? WorkingHours { get; set; }

    public string? JobType { get; set; }

    public string? Duration { get; set; }

    public int? CreatedBy { get; set; }

    public DateTime? CreatedDt { get; set; }

    public int? ModifiedBy { get; set; }

    public DateTime? ModifiedDt { get; set; }

    public string? PastHistory { get; set; }

    public string? Diet { get; set; }
}
