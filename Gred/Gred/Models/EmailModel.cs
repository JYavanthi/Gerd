namespace Gred.Models
{
  public class EmailModel
  {
    public string To { get; set; }
    public string? CC { get; set; }
    public string? Subject { get; set; }
    public string? Body { get; set; }
  }
}
