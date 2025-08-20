using System;
using System.Collections.Generic;

namespace gred.Models;

public partial class VwMedicalExamination
{
    public int Meid { get; set; }

    public int? DoctorId { get; set; }

    public int? PatientId { get; set; }

    public string? PeHeight { get; set; }

    public string? PeWeight { get; set; }

    public string? PeBmi { get; set; }

    public string? SeGanormal { get; set; }

    public string? SeGaabNormalCs { get; set; }

    public string? SeGaabNormalNcs { get; set; }

    public string? SeGaabNormalRemark { get; set; }

    public string? PaeFindings { get; set; }

    public int? CreatedBy { get; set; }

    public DateTime? CreatedDt { get; set; }

    public int? ModifiedBy { get; set; }

    public DateTime? ModifiedDt { get; set; }

    public string? SeRsnormal { get; set; }

    public string? SeRsabNormalCs { get; set; }

    public string? SeRsabNormalNcs { get; set; }

    public string? SeRsabNormalRemark { get; set; }

    public string? OthersNormal { get; set; }

    public string? OthersAbNormalCs { get; set; }

    public string? OthersAbNormalNcs { get; set; }

    public string? OthersAbNormalRemark { get; set; }

    public int? Stage { get; set; }
}
