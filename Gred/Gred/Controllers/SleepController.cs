using Gred.Data.Entities.Common;
using Gred.Data.Entities;
using gred.Data;
using Gred.Services.Interface;
using Microsoft.AspNetCore.Mvc;

namespace Gred.Controllers
{
  [Route("api/[controller]")]
  [ApiController]
  public class SleepController : Controller
  {
    private readonly GredDbContext _context;
    private readonly ISleep _sleep;

    public SleepController(GredDbContext context, ISleep sleep)
    {
      this._context = context;
      this._sleep = sleep;
    }
    [HttpPost("SaveSleep")]
    public async Task<CommonRsult> SaveSleep(ESleep sleep)
    {
      return await _sleep.SaveSleep(sleep);
    }
    [HttpGet("GetSleep")]
    public async Task<CommonRsult> GetSleep()
    {
      return await _sleep.GetSleep();
    }

    [HttpGet("GetSleepByPatientId/{patientId}/{stage}")]
    public async Task<CommonRsult> GetSleepByPatientId(int patientId, int stage )
    {
      return await _sleep.GetSleepByPatientId(patientId);
    }
  }
}
