using gred.Data;
using Gred.Data.Entities;
using Gred.Data.Entities.Common;
using Gred.Services.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Gred.Controllers
{
  [Route("api/[controller]")]
  [ApiController]
  public class DoctorLogController : ControllerBase
  {
    private readonly IDoctorLog _doctorLog;
    private readonly GredDbContext _context;

    public DoctorLogController(IDoctorLog doctorLog, GredDbContext context)
    {
      this._doctorLog = doctorLog;
      this._context = context;
    }
    [HttpGet("GetDoctorLogs")]
    public async Task<IActionResult> GetDoctorlogsdata()
    {
      var data = await _doctorLog.GetDoctorLogs();
      return Ok(data);
    }
    [HttpPost("PostDoctorLogs")]
    public async Task<CommonRsult> SaveDoctorLogs(EDoctorLog eDoctorLog)
    {
      var data1 = await _doctorLog.SaveDoctorLogs(eDoctorLog);
      return data1;
    }
  }
}
