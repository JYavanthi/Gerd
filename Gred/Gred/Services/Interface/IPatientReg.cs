using Gred.Data.Entities.Common;
using Gred.Data.Entities;

namespace Gred.Services.Interface
{
    public interface IPatientReg
    {
        Task<CommonRsult> SavePatientReg(EPatientReg ePatient);
        Task<CommonRsult> GetPatientDetails();
        Task<CommonRsult> GetPatientById(int id); 

  }
}
