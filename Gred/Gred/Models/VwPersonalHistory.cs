using System;
using System.Collections.Generic;

namespace gred.Models;

public partial class VwPersonalHistory
{
    public int PersonalHistoryId { get; set; }

    public int DoctorId { get; set; }

    public int PatientId { get; set; }

    public bool? AeratedIntake { get; set; }

    public string? AeratedFrequency { get; set; }

    public string? AeratedQuantity { get; set; }

    public string? AeratedDuration { get; set; }

    public bool? CoffeeIntake { get; set; }

    public string? CoffeeFrequency { get; set; }

    public string? CoffeeQuantity { get; set; }

    public string? CoffeeDuration { get; set; }

    public bool? TeaIntake { get; set; }

    public string? TeaFrequency { get; set; }

    public string? TeaQuantity { get; set; }

    public string? TeaDuration { get; set; }

    public bool? SpicyIntake { get; set; }

    public string? SpicyFrequency { get; set; }

    public string? SpicyQuantity { get; set; }

    public string? SpicyDuration { get; set; }

    public bool? AlcoholIntake { get; set; }

    public string? AlcoholFrequency { get; set; }

    public string? AlcoholQuantity { get; set; }

    public string? AlcoholDuration { get; set; }

    public bool? SweetsIntake { get; set; }

    public string? SweetsFrequency { get; set; }

    public string? SweetsQuantity { get; set; }

    public string? SweetsDuration { get; set; }

    public bool? SmokingIntake { get; set; }

    public string? SmokingFrequency { get; set; }

    public string? SmokingQuantity { get; set; }

    public string? SmokingDuration { get; set; }

    public bool? TobaccoIntake { get; set; }

    public string? TobaccoFrequency { get; set; }

    public string? TobaccoQuantity { get; set; }

    public string? TobaccoDuration { get; set; }

    public DateTime? CreatedDt { get; set; }

    public int ModifiedBy { get; set; }

    public DateTime? ModifiedDt { get; set; }
}
