using Gred.Data.Entities.Common;
using Gred.Data.Entities;

namespace Gred.Services.Interface
{
  public interface ISleep
  {
    Task<CommonRsult> SaveSleep(ESleep sleep);

    Task<CommonRsult> GetSleep();

    Task<CommonRsult> GetSleepByPatientId(int patientId);
  }
}
