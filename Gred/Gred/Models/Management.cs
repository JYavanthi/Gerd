using System;
using System.Collections.Generic;

namespace gred.Models;

public partial class Management
{
    public int ManagementId { get; set; }

    public int? LifestyleRecommendations { get; set; }

    public int? CreatedBy { get; set; }

    public DateTime? CreatedDt { get; set; }

    public int? ModifiedBy { get; set; }

    public DateTime? ModifiedDt { get; set; }

    public string? PpiMedicationName { get; set; }

    public string? PpiDose { get; set; }

    public string? PpiFrequency { get; set; }

    public string? ProkineticsMedicationName { get; set; }

    public string? ProkineticsDose { get; set; }

    public string? ProkineticsFrequency { get; set; }

    public string? SucralfateMedicationName { get; set; }

    public string? SucralfateFrequency { get; set; }

    public string? SucralfateDose { get; set; }

    public string? AlginateMedicationName { get; set; }

    public string? AlginateFrequency { get; set; }

    public string? AlginateDose { get; set; }

    public string? H2blockersMedicationName { get; set; }

    public string? H2blockersFrequency { get; set; }

    public string? H2blockersDose { get; set; }

    public string? H2blockersCMedicationName { get; set; }

    public string? H2blockersCFrequency { get; set; }

    public string? H2blockersCDose { get; set; }

    public string? PcabMedicationName { get; set; }

    public string? PcabFrequency { get; set; }

    public string? PcabDose { get; set; }

    public string? OthersMedicationName { get; set; }

    public string? OthersFrequency { get; set; }

    public string? OthersDose { get; set; }

    public int? PatientId { get; set; }

    public int? Stage { get; set; }
}
