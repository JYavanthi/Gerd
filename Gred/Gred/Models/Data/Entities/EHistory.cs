namespace Gred.Data.Entities
{
  public class EHistory
  {
    public string Flag { get; set; }
    public int? DoctorID { get; set; }
    public int PatientID { get; set; }
    public string Past_History { get; set; }
    public bool? Diet_Vegetarian { get; set; }
    public bool? Diet_NonVegetarian { get; set; }
    public int? CreatedBy { get; set; }
  }
}
