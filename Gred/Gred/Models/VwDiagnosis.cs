using System;
using System.Collections.Generic;

namespace gred.Models;

public partial class VwDiagnosis
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

    public int? Expr1 { get; set; }
}
