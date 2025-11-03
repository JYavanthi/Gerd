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

    public string StateName { get; set; } = null!;

    public string? H2blockersDose { get; set; }

    public string? H2blockersFrequency { get; set; }

    public string? H2blockersMedicationName { get; set; }

    public string? H2blockersCDose { get; set; }

    public string? H2blockersCFrequency { get; set; }

    public string? H2blockersCMedicationName { get; set; }

    public string? OthersDose { get; set; }

    public string? OthersFrequency { get; set; }

    public string? OthersMedicationName { get; set; }

    public string? AlginateDose { get; set; }

    public string? AlginateFrequency { get; set; }

    public string? AlginateMedicationName { get; set; }

    public string? PpiDose { get; set; }

    public string? PpiFrequency { get; set; }

    public string? PpiMedicationName { get; set; }

    public string? ProkineticsDose { get; set; }

    public string? ProkineticsFrequency { get; set; }

    public string? ProkineticsMedicationName { get; set; }

    public string? SucralfateDose { get; set; }

    public string? SucralfateFrequency { get; set; }

    public string? SucralfateMedicationName { get; set; }

    public int? State { get; set; }

    public string Zone { get; set; } = null!;

    public string DietModifications { get; set; } = null!;

    public string ModerationOfAlcohol { get; set; } = null!;

    public string WeightLoss { get; set; } = null!;

    public string RegularExercise { get; set; } = null!;

    public string StopTobaccoUse { get; set; } = null!;

    public int? Stage { get; set; }
}
