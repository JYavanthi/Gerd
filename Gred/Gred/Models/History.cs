using System;
using System.Collections.Generic;

namespace gred.Models;

public partial class History
{
    public int HistoryId { get; set; }

    public int PatientId { get; set; }

    public int Stage { get; set; }

    public string? PastHistory { get; set; }

    public bool? DietVegetarian { get; set; }

    public bool? DietNonVegetarian { get; set; }

    public int? DoctorId { get; set; }

    public int? CreatedBy { get; set; }

    public int? ModifiedBy { get; set; }

    public DateTime? CreatedDate { get; set; }

    public DateTime? ModifiedDate { get; set; }
}
