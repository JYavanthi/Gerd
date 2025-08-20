using System;
using System.Collections.Generic;

namespace gred.Models;

public partial class Gerdhistory
{
    public int Ghid { get; set; }

    public int? DoctorId { get; set; }

    public int? PatientId { get; set; }

    public string? UsageOfPpi { get; set; }

    public string? HistoryofEndoscopy { get; set; }

    public DateTime? EndoscopyDate { get; set; }

    public bool? EndoscopyAttached { get; set; }

    public string? EndoscopyAttement { get; set; }

    public string? EndoscopyRemark { get; set; }

    public bool? HistoryofGs { get; set; }

    public bool? GsBariatricSurgery { get; set; }

    public string? GsBsremark { get; set; }

    public bool? GsFundoplicationSurgery { get; set; }

    public string? GsFsremark { get; set; }

    public bool? GsGastricPoemsurgery { get; set; }

    public string? GsGpsremark { get; set; }

    public bool? GsGastrojejunostomy { get; set; }

    public string? GsGjremark { get; set; }

    public string? GsOtherText { get; set; }

    public bool? GsOther { get; set; }

    public string? GsOtherRemark { get; set; }

    public int? CreatedBy { get; set; }

    public DateTime? CreatedDt { get; set; }

    public int? ModifiedBy { get; set; }

    public DateTime? ModifiedDt { get; set; }

    public int? Stage { get; set; }
}
