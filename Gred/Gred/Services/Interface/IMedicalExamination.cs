using gred.Models;
using Gred.Data.Entities;
using Gred.Data.Entities.Common;

namespace Gred.Services.Interface
{
  public interface IMedicalExamination
  {
    Task<CommonRsult> SaveMedicalExamination(EMedicalExamination medicalexamination);

    Task<CommonRsult> GetMedicalExamination();
    Task<CommonRsult> GetMedicalExaminationById(int patientId);
  }
}
