using Gred.Data.Entities.Common;
using System.Threading.Tasks;

namespace Gred.Services.Interface
{
  public interface IGenderRPT
  {
    Task<CommonRsult> GetAllGenderData();
  }
}
