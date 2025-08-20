using Gred.Data.Entities.Common;
using Gred.Data.Entities;

namespace Gred.Services.Interface
{
    public interface IComorbidities
    {
        Task<CommonRsult> SaveComorbidities(EComorbidities eComorbidities);
        Task<CommonRsult> GetComorbidities();
    Task<CommonRsult> GetComorbditiesById(int patientId ,int stage);

  }
}
