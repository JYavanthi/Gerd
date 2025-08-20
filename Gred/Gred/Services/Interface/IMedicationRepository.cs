using Gred.Data.Entities;
using Gred.Data.Entities.Common;
using System.Threading.Tasks;

// Interface
namespace Gred.Services.Interface
{
  public interface IMedicationService
  {
    Task<CommonRsult> SaveMedication(EMedication medication);
    Task<CommonRsult> GetMedicationByPatientId(int patientId);
  }
}

