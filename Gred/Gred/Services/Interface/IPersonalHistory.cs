using Gred.Data.Entities.Common;
using Gred.Data.Entities;

namespace Gred.Services.Interface
{
  public interface IPersonalHistory
  {
    Task<CommonRsult> SavePersonalHistory(EPersonalHistory personalhistory);
    Task<CommonRsult> GetPersonalHistory();

    Task<CommonRsult> GetPersonalHistoryById(int id, int stage);
  }
}
