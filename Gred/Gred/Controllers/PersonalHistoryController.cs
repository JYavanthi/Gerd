using Gred.Data.Entities.Common;
using Gred.Data.Entities;
using Gred.Services.Interface;
using Microsoft.AspNetCore.Mvc;

namespace Gred.Controllers
{
  [Route("api/[controller]")]
  [ApiController]
  public class PersonalHistoryController : Controller
  {
    private readonly IPersonalHistory _personalhistory;

    public PersonalHistoryController(IPersonalHistory personalhistory)
    {
      this._personalhistory = personalhistory;
    }

    [HttpGet("GetPersonalHistory")]
    public async Task<CommonRsult> GetPersonalHistory()
    {
      return await _personalhistory.GetPersonalHistory();
    }

    [HttpPost("SavePersonalHistory")]
    public async Task<CommonRsult> SavePersonalHistory(EPersonalHistory personalhistory)
    {
      return await _personalhistory.SavePersonalHistory(personalhistory);
    }

    [HttpGet("GetPersonalHistoryById/{id}/{stage}")]
    public async Task<CommonRsult> GetPersonalHistoryById(int id, int stage)
    {
      return await _personalhistory.GetPersonalHistoryById(id, stage);
    }
  }
}

