using System;
using System.Collections.Generic;

namespace gred.Models;

public partial class VwMedication
{
    public int MedicationId { get; set; }

    public int? Ghid { get; set; }

    public string? MedicationName { get; set; }

    public string? Dose { get; set; }

    public string? Frequency { get; set; }

    public string? Molecule { get; set; }

    public int? CreatedBy { get; set; }

    public DateTime? CreatedDt { get; set; }

    public int? ModifiedBy { get; set; }

    public DateTime? ModifiedDt { get; set; }
}
