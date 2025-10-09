using System;
using System.Collections.Generic;

namespace gred.Models;

public partial class VwComorbitiesRpt
{
    public string? Initial { get; set; }

    public string? SubjectNo { get; set; }

    public DateTime? Date { get; set; }

    public int? Age { get; set; }

    public string? Gender { get; set; }

    public string? FamilyIncome { get; set; }

    public string? Occupation { get; set; }

    public string? Education { get; set; }

    public int? Pincode { get; set; }

    public string City { get; set; } = null!;

    public string State { get; set; } = null!;

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

    public string? HtdPresent { get; set; }

    public string? HtdRemark { get; set; }

    public string? BdPresent { get; set; }

    public string? BdRemark { get; set; }

    public string Zone { get; set; } = null!;
}
