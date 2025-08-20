namespace Gred.Data.Entities
{
  public class EHistoryEndsocopy
  {
    public string Flag { get; set; }
    public int? Id { get; set; }
    public int? PatientId { get; set; }
    public int? Stage { get; set; }

    public bool? Endoscopy_History { get; set; }
    public DateTime? Endoscopy_Date { get; set; }
    public bool? Endoscopy_ReportAttached { get; set; }
    public string? Endoscopy_Remarks { get; set; }
    public bool? GastroSurgery_History { get; set; }
    public bool? Bariatric_Surgery { get; set; }
    public string? Bariatric_Remarks { get; set; }
    public bool? Fundoplication_Surgery { get; set; }
    public string? Fundoplication_Remarks { get; set; }
    public bool? POEM_Surgery { get; set; }
    public string? POEM_Remarks { get; set; }
    public bool? Gastrojejunostomy_Surgery { get; set; }
    public string? Gastrojejunostomy_Remarks { get; set; }
    public bool? Other_Surgery { get; set; }
    public string? Other_Surgery_Specify { get; set; }
    public string? Other_Remarks { get; set; }
    public DateTime? CreatedDt { get; set; }
    public int? CreatedBy { get; set; }
    public DateTime? ModifiedDt { get; set; }
    public int? ModifiedBy { get; set; }

  }
}
