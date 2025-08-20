using System;
using System.Collections.Generic;

namespace gred.Models;

public partial class VwCheifComplaint
{
    public int CheifCompliantId { get; set; }

    public int? PatientId { get; set; }

    public int? DoctorId { get; set; }

    public int? HbDuration { get; set; }

    public int? HbFrequency { get; set; }

    public string? HbPostural { get; set; }

    public string? HbNocturnal { get; set; }

    public int? RDuration { get; set; }

    public int? RFrequency { get; set; }

    public string? RPostural { get; set; }

    public string? RNocturnal { get; set; }

    public int? RpDuration { get; set; }

    public int? RpFrequency { get; set; }

    public string? RpPostural { get; set; }

    public string? RpNocturnal { get; set; }

    public int? AtDuration { get; set; }

    public int? AtFrequency { get; set; }

    public string? AtPostural { get; set; }

    public string? AtNocturnal { get; set; }

    public int? CreatedBy { get; set; }

    public string? CreatedByName { get; set; }

    public DateTime? CreatedDt { get; set; }

    public int? ModifiedBy { get; set; }

    public string? ModifiedByName { get; set; }

    public DateTime? ModifiedDt { get; set; }

    public int? Stage { get; set; }
}
