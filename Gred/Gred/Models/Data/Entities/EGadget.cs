namespace Gred.Data.Entities
{
  public class EGadget
  {
    public string? Flag { get; set; }

    public int? Id { get; set; }
    public int? PatientId { get; set; }
    public bool? ComputerUsed { get; set; }
    public string ComputerFrequency { get; set; }
    public int? ComputerDurationYears { get; set; }
    public bool? SmartphoneUsed { get; set; }
    public string SmartphoneFrequency { get; set; }
    public int? SmartphoneDurationYears { get; set; }
    public string WorkingHours { get; set; }
    public string JobType { get; set; }
    public int? TotalWorkingYears { get; set; }
    public string? CreatedBy { get; set; }
    public int? Stage { get; set; }
  }
}
