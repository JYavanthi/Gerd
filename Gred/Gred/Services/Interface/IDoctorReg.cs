using Gred.Data.Entities;
using Gred.Data.Entities.Common;

namespace Gred.Services.Interface
{
    public interface IDoctorReg
    {
        Task<CommonRsult> SaveDoctorReg(EDoctorReg reg);
        Task<CommonRsult> GetDoctorvalue();
    }
}
