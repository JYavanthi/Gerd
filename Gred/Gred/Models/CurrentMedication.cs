using System;
using System.Collections.Generic;

namespace gred.Models;

public partial class CurrentMedication
{
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

    public DateTime? CreatedDt { get; set; }

    public int? CreatedBy { get; set; }

    public DateTime? ModifiedDt { get; set; }

    public int? ModifiedBy { get; set; }

    public int? Stage { get; set; }
}
