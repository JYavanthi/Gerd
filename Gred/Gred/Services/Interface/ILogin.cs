using Gred.Data.Entities.Common;

namespace Gred.Services.Interface
{
  public interface ILogin
  {
    Task<CommonRsult> AuthenticateDoctor(string Email, string MoblieNo, string password);
  }
}
