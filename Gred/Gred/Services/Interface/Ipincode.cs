using System.Runtime.InteropServices;


using Gred.Data.Entities.Common;
using System.Threading.Tasks;

namespace Gred.Services.Interface
{
  public interface Ipincode
  {
    Task<CommonRsult> GetPincodes();
  }
}

