namespace Gred.Data.Entities
{
  public class EDoctorLog
  {
    public char Flag { get; set; }

    public int DoctorlogID { get; set; }

    public int? DoctorID { get; set; }

    public DateTime? LoginTime { get; set; }

    public DateTime? LogoutTime { get; set; }
    public string? Token { get; set; }
    public int CreatedBy { get; set; }
  }
}
