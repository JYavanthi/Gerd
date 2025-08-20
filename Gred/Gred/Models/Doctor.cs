using System;
using System.Collections.Generic;

namespace gred.Models;

public partial class Doctor
{
    public int DoctorId { get; set; }

    public string? Name { get; set; }

    public string? Email { get; set; }

    public string? PhoneNo { get; set; }

    public string? Mcicode { get; set; }

    public string? PlaceOfPractice { get; set; }

    public string? HospitalName { get; set; }

    public string? Password { get; set; }

    public int? State { get; set; }

    public int? City { get; set; }

    public string? EnterCodeNo { get; set; }

    public string? Status { get; set; }

    public int? CreatedBy { get; set; }

    public DateTime? CreatedDt { get; set; }

    public int? ModifiedBy { get; set; }

    public DateTime? ModifiedDt { get; set; }
}
