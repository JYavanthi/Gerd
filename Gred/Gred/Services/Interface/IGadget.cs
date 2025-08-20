using Gred.Data.Entities.Common;
using Gred.Data.Entities;

namespace Gred.Services.Interface
{
  public interface IGadget
  {
    Task<CommonRsult> SaveGadget(EGadget gadget);
    Task<CommonRsult> GetGadget();
    Task<CommonRsult> GetGadgetById(int PatientId, int  stage);
  }
}
