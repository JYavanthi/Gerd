using Gred.Data.Entities;
using Gred.Data.Entities.Common;

namespace Gred.Services.Interface
{
    public interface ICheifComplaint
    {
        Task <CommonRsult> SaveCheifComplaint(ECheifComplaint eCheif);
        Task<CommonRsult> GetCheifComplaint();
    Task<CommonRsult> GetCheifComplaintById(int patientId, int stage);

  }
}
