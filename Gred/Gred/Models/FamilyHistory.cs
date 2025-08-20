using System;
using System.Collections.Generic;

namespace gred.Models;

public partial class FamilyHistory
{
    public int FamilyHistoryId { get; set; }

    public int? DoctorId { get; set; }

    public int? PatientId { get; set; }

    public string? FhGred { get; set; }

    public string? FhRemark { get; set; }

    public string? FhEgc { get; set; }

    public string? FhEgcremark { get; set; }

    public string? GHPpi { get; set; }

    public int? CreatedBy { get; set; }

    public DateTime? CreatedDt { get; set; }

    public int? ModifiedBy { get; set; }

    public DateTime? ModifiedDt { get; set; }

    public int? Stage { get; set; }
}
