using Gred.Data.Entities.Common;
using Gred.Data.Entities;
using gred.Data;
using Gred.Services.Interface;
using Microsoft.AspNetCore.Mvc;

namespace Gred.Controllers
{
  [Route("api/[controller]")]
  [ApiController]
  public class HistoryEndsocopyController : Controller
  {
    private readonly GredDbContext _context;
    private readonly IHistoryEndsocopy _history;

    public HistoryEndsocopyController(GredDbContext context, IHistoryEndsocopy history)
    {
      this._context = context;
      this._history = history;
    }

    [HttpPost("SaveHistoryEndsocopy")]
    public async Task<CommonRsult> SaveHistoryEndsocopy(EHistoryEndsocopy ehistoryendsocopy)
    {
      return await _history.SaveHistoryEndsocopy(ehistoryendsocopy);
    }
    [HttpGet("GetHistoryEndsocopy")]
    public async Task<CommonRsult> GetCheifComplaint()
    {
      return await _history.GetHistoryEndsocopy();
    }
  }
}
