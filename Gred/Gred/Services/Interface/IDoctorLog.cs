using Gred.Data.Entities;
using Gred.Data.Entities.Common;

namespace Gred.Services.Interface
{
  public interface IDoctorLog
  {
    Task<CommonRsult> SaveDoctorLogs(EDoctorLog eDoctorLog);
    Task<CommonRsult> GetDoctorLogs();
  }
}
