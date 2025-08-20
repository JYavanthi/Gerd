using Gred.Data.Entities;
using Gred.Data.Entities.Common;

namespace Gred.Services.Interface
{
  public interface ICurrentMedication
  {
    Task<CommonRsult> SaveCurrentMedication(ECurrentMedication eCurrentMedication);

    Task<CommonRsult> GetCurrentMedication();

    Task<CommonRsult> GetCurrentMedicationById(int patientId);
  }
}
