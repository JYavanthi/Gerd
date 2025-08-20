using System;
using System.Collections.Generic;

namespace gred.Models;

public partial class Diagnosis
{
    public int DiagnosisId { get; set; }

    public int? PatientId { get; set; }

    public int? DoctorId { get; set; }

    public bool? NewlyDiagnosed { get; set; }

    public bool? KnownCaseOfGerd { get; set; }

    public int? GredNoOfYear { get; set; }

    public string? Gerdtype { get; set; }

    public bool? RefractoryToPpi { get; set; }

    public bool? AdherenceToTherapy { get; set; }

    public int? Stage { get; set; }

    public int? CreatedBy { get; set; }

    public DateTime? CreatedDt { get; set; }

    public int? ModifiedBy { get; set; }

    public DateTime? ModifiedDt { get; set; }
}
