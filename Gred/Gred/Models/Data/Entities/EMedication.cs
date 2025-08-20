namespace Gred.Data.Entities
{
  public class EMedication
  {
    public string? Flag { get; set; } 

    public int MedicationId { get; set; }
    public int? Ghid { get; set; }

    public string? MedicationName { get; set; }
    public string? Dose { get; set; }
    public string? Frequency { get; set; }
    public string? Molecule { get; set; }

    public int? CreatedBy { get; set; }
    public DateTime? CreatedDt { get; set; }

    public int? ModifiedBy { get; set; }
    public DateTime? ModifiedDt { get; set; }

    public int? PatientId { get; set; }
    public int? Stage { get; set; }
  }
}
