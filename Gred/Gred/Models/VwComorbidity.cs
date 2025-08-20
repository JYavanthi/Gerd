using System;
using System.Collections.Generic;

namespace gred.Models;

public partial class VwComorbidity
{
    public int ComorbiditiesId { get; set; }

    public int? Stage { get; set; }

    public int? PatientId { get; set; }

    public int? DoctorId { get; set; }

    public string? HtPresent { get; set; }

    public string? HtRemark { get; set; }

    public string? DbPresent { get; set; }

    public string? DbRemark { get; set; }

    public string? DdPresent { get; set; }

    public string? DdRemark { get; set; }

    public string? CldPresent { get; set; }

    public string? CldRemark { get; set; }

    public string? NdPresent { get; set; }

    public string? NdRemark { get; set; }

    public string? CdPresent { get; set; }

    public string? CdRemark { get; set; }

    public string? HPresent { get; set; }

    public string? HRemark { get; set; }

    public string? HtdPresent { get; set; }

    public string? HtdRemark { get; set; }

    public string? BdPresent { get; set; }

    public string? BdRemark { get; set; }

    public string? CkdPresent { get; set; }

    public string? CkdRemark { get; set; }

    public string? APresent { get; set; }

    public string? ARemark { get; set; }

    public string? OPresent { get; set; }

    public string? ORemark { get; set; }

    public string? RaPresent { get; set; }

    public string? RaRemark { get; set; }

    public string? SsPresent { get; set; }

    public string? SsRemark { get; set; }

    public string? CPresent { get; set; }

    public string? CRemark { get; set; }

    public string? CmoPresent { get; set; }

    public string? CmoRemark { get; set; }

    public int? CreatedBy { get; set; }

    public string? CreatedByName { get; set; }

    public DateTime? CreatedDt { get; set; }

    public int? ModifiedBy { get; set; }

    public string? ModifiedByName { get; set; }

    public DateTime? ModifiedDt { get; set; }
}
