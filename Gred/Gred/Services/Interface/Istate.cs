using Gred.Data.Entities;
using Gred.Data.Entities.Common;
using System.Threading.Tasks;


namespace Gred.Services.Interface
{
  public interface IState
  {
    public Task<CommonRsult> GetState();
  }
}
