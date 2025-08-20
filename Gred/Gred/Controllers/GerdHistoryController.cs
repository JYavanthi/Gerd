using Gred.Data.Entities;
using Gred.Data.Entities.Common;
using Gred.Services.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Gred.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GerdHistoryController : ControllerBase
    {
        private readonly IGerdHistory _gerd;

        public GerdHistoryController(IGerdHistory gerd)
        {
            this._gerd = gerd;
        }
        [HttpPost("AddGerdHistory")]
        public async Task<IActionResult> AddGerdHistory(EGerdHistory gerdHistory)
        {
            CommonRsult result = new CommonRsult();
            try
            {
               result = await _gerd.AddGerdHistory(gerdHistory);
            }
            catch (Exception ex)
            {
                result.Type = "E";
                result.Message = ex.Message;
            }
            return Ok( result);
        }
        [HttpGet("GetGerdHistory")]
        public async Task<CommonRsult> GetGerdHistory()
        {
            CommonRsult result = new CommonRsult();
            try
            {
                result = await _gerd.GetGerdHistory();
            }
            catch (Exception ex)
            {
                result.Type = "E";
                result.Message = ex.Message;
            }
            return result;
        }

    [HttpGet("GetGerdHistoryById/{patientId}")]
    public async Task<CommonRsult> GetGerdHistoryById(int patientId)
    {
      return await _gerd.GetGerdHistoryById(patientId);
    }

  }
}
