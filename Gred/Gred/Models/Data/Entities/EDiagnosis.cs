namespace Gred.Data.Entities
{
  public class EDiagnosis
  {
    public char Flag { get; set; }
    public int? DiagnosisID { get; set; }
    public int? PatientID { get; set; }

    public int? DoctorID { get; set; }
    public bool? NewlyDiagnosed { get; set; }
    public bool? KnownCaseOfGERD { get; set; }
    public int? GRED_NoOfYear { get; set; }
    public string? GERDType { get; set; }
    public bool? RefractoryToPPI { get; set; }
    public bool? AdherenceToTherapy { get; set; }
    public int Stage { get; set; }
    public int CreatedBy { get; set; }

  }
}
