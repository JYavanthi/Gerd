namespace Gred.Data.Entities
{
  public class EManagement
  {
    public int ManagementID { get; set; }
    public string? Flag { get; set; }
    public int? Stage { get; set; }
    public int? PatientID { get; set; }

    public int? LifestyleRecommendations { get; set; }
    public int? CreatedBy { get; set; }
    public DateTime? CreatedDt { get; set; }
    public int? ModifiedBy { get; set; }
    public DateTime? ModifiedDt { get; set; }

    public string? PPI_Medication_Name { get; set; }
    public string? PPI_Dose { get; set; }
    public string? PPI_Frequency { get; set; }

    public string? Prokinetics_Medication_Name { get; set; }
    public string? Prokinetics_Dose { get; set; }
    public string? Prokinetics_Frequency { get; set; }

    public string? Sucralfate_Medication_Name { get; set; }
    public string? Sucralfate_Dose { get; set; }
    public string? Sucralfate_Frequency { get; set; }

    public string? Alginate_Medication_Name { get; set; }
    public string? Alginate_Dose { get; set; }
    public string? Alginate_Frequency { get; set; }

    public string? H2Blockers_Medication_Name { get; set; }
    public string? H2Blockers_Dose { get; set; }
    public string? H2Blockers_Frequency { get; set; }

    public string? H2BlockersC_Medication_Name { get; set; }
    public string? H2BlockersC_Dose { get; set; }
    public string? H2BlockersC_Frequency { get; set; }

    public string? PCAB_Medication_Name { get; set; }
    public string? PCAB_Dose { get; set; }
    public string? PCAB_Frequency { get; set; }

    public string? others_Medication_Name { get; set; }
    public string? others_Dose { get; set; }
    public string? others_Frequency { get; set; }
  }


  public class EStageUpdate
  {
    public int patientId { get; set; }
    public int stage { get; set; }
    public int createdby { get; set; }
  }
}
