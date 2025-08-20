using Gred.Data.Entities;
using Gred.Data.Entities.Common;

namespace Gred.Services.Interface
{
  public interface IHistoryEndsocopy
  {
    Task<CommonRsult> SaveHistoryEndsocopy(EHistoryEndsocopy ehistoryendsocopy);

    Task<CommonRsult> GetHistoryEndsocopy();
  }
}
