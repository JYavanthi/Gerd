using Gred.Data.Entities.Common;
using Gred.Data.Entities;
using Gred.Services.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Runtime.CompilerServices;

namespace Gred.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ComorbiditiesController : ControllerBase
    {
        private readonly IComorbidities _comorbidities;

        public ComorbiditiesController(IComorbidities comorbidities)
        {
            this._comorbidities = comorbidities;
        }
        [HttpGet("GetComorbidities")]
        public async Task<CommonRsult> GetComorbidities()
        {
            return await _comorbidities.GetComorbidities();
        }


    [HttpGet("GetComorbditiesById/{patientId}/{stage}")]
    public async Task<CommonRsult> GetComorbditiesById(int patientId , int stage)
    {
      return await _comorbidities.GetComorbditiesById(patientId ,stage);
    }


    [HttpPost("SaveComorbidities")]
        public async Task<CommonRsult> SaveComorbidities(EComorbidities eComorbidities)
        {
            return await _comorbidities.SaveComorbidities(eComorbidities);
        }
    }
}
