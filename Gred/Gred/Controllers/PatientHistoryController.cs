using Gred.Data.Entities.Common;
using Gred.Data.Entities;
using Gred.Services.Interface;
using Microsoft.AspNetCore.Mvc;

namespace Gred.Controllers
{
  [Route("api/[controller]")]
  [ApiController]
  public class PatientHistoryController : ControllerBase
  {
    private readonly IPatientHistory _patient;

        public PatientHistoryController(IPatientHistory patient)
        {
            this._patient = patient;
        }
        [HttpPost("SavePatientHistory")]
        public async Task<CommonRsult> SavePatientHistory(EPatientHistory patient)
        {
            return await _patient.SavePatientHistory(patient);
        }
        [HttpGet("GetPatientHistory")]
        public async Task<CommonRsult> GetPatientHistory()
        {
            CommonRsult result = new CommonRsult();
            try
            {
                result = await _patient.GetPatientHistory();
            }
            catch(Exception ex)
            {
                result.Type = "E";
                result.Message = ex.Message;
            }
            return result;
        }

    [HttpGet("GetPatientHistoryById/{id}")]
    public async Task<CommonRsult> GetPaitentHistoryById(int id)
    {
      return await _patient.GetPatientHistoryById(id);
    }

    
  }
}
