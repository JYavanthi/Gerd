using Gred.Data.Entities;
using Gred.Data.Entities.Common;

namespace Gred.Services.Interface
{
    public interface IPatientHistory
    {
    Task<CommonRsult> SavePatientHistory(EPatientHistory patientHistory);
    Task<CommonRsult> GetPatientHistory();
    Task<CommonRsult> GetPatientHistoryById(int id);


  }
}
