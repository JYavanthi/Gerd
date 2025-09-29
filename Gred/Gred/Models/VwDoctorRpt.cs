using System;
using System.Collections.Generic;

namespace gred.Models;

public partial class VwDoctorRpt
{
    public int DoctorId { get; set; }

    public string? Name { get; set; }

    public string? Email { get; set; }

    public string? PhoneNo { get; set; }

    public string? Mcicode { get; set; }

    public string? PlaceOfPractice { get; set; }

    public string? HospitalName { get; set; }

    public string State { get; set; } = null!;

    public string City { get; set; } = null!;

    public string? EnterCodeNo { get; set; }

    public string? Status { get; set; }
}
