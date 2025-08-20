using Gred.Data.Entities;
using Gred.Data.Entities.Common;

namespace Gred.Services.Interface
{
    public interface IGerdHistory
    {
        Task<CommonRsult> AddGerdHistory(EGerdHistory gerdHistory);
        Task<CommonRsult> GetGerdHistory();
        Task<CommonRsult> GetGerdHistoryById(int patientId);

  }
}
