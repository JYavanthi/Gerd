using System;
using System.Collections.Generic;

namespace gred.Models;

public partial class VwManagement
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

    public string DietModifications { get; set; } = null!;

    public string ModerationOfAlcohol { get; set; } = null!;

    public string WeightLoss { get; set; } = null!;

    public string RegularExercise { get; set; } = null!;

    public string StopTobaccoUse { get; set; } = null!;

    public int? Stage { get; set; }
}
