using Gred.Data.Entities.Common;
using Gred.Data.Entities;
using Gred.Services.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Gred.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PatientRegController : ControllerBase
    {
        private readonly IPatientReg _patient;

        public PatientRegController(IPatientReg patient)
        {
            this._patient = patient;
        }

        [HttpPost("SavePatient")]
        public async Task<CommonRsult> SavePatientReg(EPatientReg ePatient)
        {
            return await _patient.SavePatientReg(ePatient);
        }
        [HttpGet("GetPatient")]
        public async Task<CommonRsult> GetPatientDetails()
        {
            return await _patient.GetPatientDetails();
        }


    [HttpGet("GetPatient/{id}")]
    public async Task<CommonRsult> GetPatientById(int id)
    {
      return await _patient.GetPatientById(id);
    }

    

  }
}
