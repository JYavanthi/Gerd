using Gred.Data.Entities.Common;
using Gred.Data.Entities;
using Gred.Services.Interface;
using Microsoft.AspNetCore.Mvc;
using Gred.Models;

namespace Gred.Controllers
{
  [Route("api/[controller]")]
  [ApiController]
  public class HistoryController : Controller
  {
    private readonly IHistory _history;

    public HistoryController(IHistory history)
    {
      this._history = history;
    }

    [HttpGet("GetHistory")]
    public async Task<CommonRsult> GetHistory()
    {
      return await _history.GetHistory();
    }

    [HttpPost("SaveHistory")]
    public async Task<CommonRsult> SaveHistory(EHistory history)
    {
      return await _history.SaveHistory(history);
    }

    [HttpGet("GetHistoryById/{id}/{stage}")]
    public async Task<CommonRsult> GetHistoryById(int id ,int stage)
    {
      return await _history.GetHistoryById(id ,stage);
    }
  }
}
