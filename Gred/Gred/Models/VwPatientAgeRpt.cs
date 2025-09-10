using System;
using System.Collections.Generic;

namespace gred.Models;

public partial class VwPatientAgeRpt
{
    public string? Initial { get; set; }

    public int? Age { get; set; }

    public string AgeGroup { get; set; } = null!;

    public string? Gender { get; set; }
}
