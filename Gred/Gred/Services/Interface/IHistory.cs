using Gred.Data.Entities;
using Gred.Data.Entities.Common;

namespace Gred.Services.Interface
{
  public interface IHistory
  {
    Task<CommonRsult> SaveHistory(EHistory history);
    Task<CommonRsult> GetHistory();

    Task<CommonRsult> GetHistoryById(int id,int stage);
  }
}
