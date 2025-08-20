namespace Gred.Data.Entities
{
    public class EGerdHistory
    {
    public string Flag { get; set; }
    public int GHID { get; set; }
    public int DoctorID { get; set; }
    public int PatientID { get; set; }
    public int? Stage { get; set; }

    public string UsageOfPPI { get; set; }

    public string HistoryofEndoscopy { get; set; }
    public DateTime? EndoscopyDate { get; set; }
    public bool? EndoscopyAttached { get; set; }
    public string EndoscopyAttement { get; set; }
    public string EndoscopyRemark { get; set; }

    public bool? HistoryofGS { get; set; }

    public bool? GS_BariatricSurgery { get; set; }
    public string GS_BSRemark { get; set; }

    public bool? GS_FundoplicationSurgery { get; set; }
    public string GS_FSRemark { get; set; }

    public bool? GS_GastricPOEMSurgery { get; set; }
    public string GS_GPSRemark { get; set; }

    public bool? GS_Gastrojejunostomy { get; set; }
    public string GS_GJRemark { get; set; }
    public string gs_OtherText { get; set; }
    public bool? GS_Other { get; set; }
    public string GS_OtherRemark { get; set; }

    public int CreatedBy { get; set; }


  }
}
