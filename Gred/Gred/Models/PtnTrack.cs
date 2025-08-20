using System;
using System.Collections.Generic;

namespace gred.Models;

public partial class PtnTrack
{
    public int PtnTrackId { get; set; }

    public int DoctorId { get; set; }

    public int PatientId { get; set; }

    public string? PageRouter { get; set; }

    public int? Stage { get; set; }

    public int? CreatedBy { get; set; }

    public DateTime? CreatedDt { get; set; }

    public int? ModifiedBy { get; set; }

    public DateTime? ModifiedDt { get; set; }
}
