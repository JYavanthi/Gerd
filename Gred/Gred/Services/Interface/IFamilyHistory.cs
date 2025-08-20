using Gred.Data.Entities;
using Gred.Data.Entities.Common;

namespace Gred.Services.Interface
{
    public interface IFamilyHistory
    {
        Task<CommonRsult> AddFamilyHistory(EFamilyHistory familyHistory);
        Task<CommonRsult> GetFamilyHistory();
        Task<CommonRsult> GetFamilyHistoryById(int id,int stage);

  }
}
