using Gred.Data.Entities.Common;
using Gred.Data.Entities;
using Gred.Services.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

namespace Gred.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DoctorRegController : ControllerBase
    {
        private readonly IDoctorReg _doctor;

        public DoctorRegController(IDoctorReg doctor)
        {
            this._doctor = doctor;
        }
        [HttpPost("SaveDoctorReg")]
         [AllowAnonymous]
    public async Task<CommonRsult> SaveDoctorReg(EDoctorReg reg)
        {
            return await _doctor.SaveDoctorReg(reg);
        }
        [HttpGet("GetDoctor")]
        public async Task<CommonRsult> GetDoctorvalue()
        {
            return await _doctor.GetDoctorvalue();
        }
    }
}
