using Gred.Data.Entities;
using Gred.Data.Entities.Common;

namespace Gred.Services.Interface
{
  public interface IManagement
  {
    Task<CommonRsult> SaveManagement(EManagement management);
    Task<CommonRsult> GetManagement();
    Task<CommonRsult> GetManagementById(int id,int stage);
    Task<CommonRsult> SubmitStage(EStageUpdate stageUodateObj);

  }
}
