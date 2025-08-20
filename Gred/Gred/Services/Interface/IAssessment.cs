using Gred.Data.Entities;
using Gred.Data.Entities.Common;

namespace Gred.Services.Interface
{
  public interface IAssessment
  {
    Task<CommonRsult> SaveAssessment(EAssessment assessment);

    Task<CommonRsult> GetAssessment();
    Task<CommonRsult> GetAssessmentById(int id ,int stage);


  }
}
