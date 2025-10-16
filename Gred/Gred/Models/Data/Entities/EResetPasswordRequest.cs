namespace Gred.Models.Data.Entities
{
  public class EResetPasswordRequest
  {
    public string Token { get; set; }
    public string NewPassword { get; set; }
  }
}
