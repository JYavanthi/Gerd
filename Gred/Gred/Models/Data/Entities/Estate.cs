namespace Gred.Data.Entities

{
  public class EState
  {
    public char Flag { get; set; }
    public short id { get; set; }
    public Byte country_id { get; set; }
    public string country_code { get; set; }
    public string country_name { get; set; }
    public string? state_code { get; set; }
    public string type { get; set; }
    public double? latitude { get; set; }
    public double? longitude { get; set; }
    public int CreatedBy { get; set; }

  }
}
