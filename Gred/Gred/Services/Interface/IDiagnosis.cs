using Gred.Data.Entities;
using Gred.Data.Entities.Common;

namespace Gred.Services.Interface
{
  public interface IDiagnosis
  {
    Task<CommonRsult> SaveDiagnosis(EDiagnosis eDiagnosis);
    Task<CommonRsult> GetDiagnosis();

    Task<CommonRsult> GetDiagnosisById(int patientId);
  }
}
