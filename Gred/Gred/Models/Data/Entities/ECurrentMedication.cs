namespace Gred.Data.Entities
{
  public class ECurrentMedication
  {
    public string Flag { get; set; }
    public int? Id { get; set; }
    public int? PatientId { get; set; }
    public int? Stage { get; set; }

    public string? NSAIDs_Molecule { get; set; }
    public string? NSAIDs_Dose { get; set; }
    public string? NSAIDs_Frequency { get; set; }
    public string? Bisphosphonates_Molecule { get; set; }
    public string? Bisphosphonates_Dose { get; set; }
    public string? Bisphosphonates_Frequency { get; set; }
    public string? Steroids_Molecule { get; set; }
    public string? Steroids_Dose { get; set; }
    public string? Steroids_Frequency { get; set; }
    public string? Antiplatelet_Molecule { get; set; }
    public string? Antiplatelet_Dose { get; set; }
    public string? Antiplatelet_Frequency { get; set; }
    public string? Others_Molecule { get; set; }
    public string? Others_Dose { get; set; }
    public string? Others_Frequency { get; set; }
    public int? CreatedBy { get; set; }
   

  }
}
