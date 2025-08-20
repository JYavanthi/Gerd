using System;
using System.Collections.Generic;

namespace gred.Models;

public partial class VwPatient
{
    public int PatientId { get; set; }

    public int DoctorId { get; set; }

    public string? Initial { get; set; }

    public string? SubjectNo { get; set; }

    public DateTime? Date { get; set; }

    public int? Age { get; set; }

    public string? Gender { get; set; }

    public string? Education { get; set; }

    public string? Occupation { get; set; }

    public int? State { get; set; }

    public int? City { get; set; }

    public int? Pincode { get; set; }

    public string? PlaceType { get; set; }

    public string? SocioeconomicStatus { get; set; }

    public string? FamilyIncome { get; set; }

    public string? PastHistory { get; set; }

    public string? Diet { get; set; }

    public int? CreatedBy { get; set; }

    public DateTime? CreatedDt { get; set; }

    public int? ModifiedBy { get; set; }

    public DateTime? ModifiedDt { get; set; }

    public int? Stage { get; set; }
}
