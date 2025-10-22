using System;
using System.Collections.Generic;

namespace gred.Models;

public partial class VwCurrentMedication
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

    public int Id { get; set; }

    public int PatientId { get; set; }

    public string? NsaidsMolecule { get; set; }

    public string? NsaidsDose { get; set; }

    public string? NsaidsFrequency { get; set; }

    public string? BisphosphonatesMolecule { get; set; }

    public string? BisphosphonatesDose { get; set; }

    public string? BisphosphonatesFrequency { get; set; }

    public string? SteroidsMolecule { get; set; }

    public string? SteroidsDose { get; set; }

    public string? SteroidsFrequency { get; set; }

    public string? AntiplateletMolecule { get; set; }

    public string? AntiplateletDose { get; set; }

    public string? AntiplateletFrequency { get; set; }

    public string? OthersMolecule { get; set; }

    public string? OthersDose { get; set; }

    public string? OthersFrequency { get; set; }

    public int? CreatedBy { get; set; }

    public DateTime? CreatedDt { get; set; }

    public int? ModifiedBy { get; set; }

    public DateTime? ModifiedDt { get; set; }

    public int? Stage { get; set; }
}
