
using Gred.Data.Entities.Common;
using Gred.Data.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using gred.Data;
using Gred.Services.Interface;
using Microsoft.EntityFrameworkCore;

namespace sanchar6tBackEnd.Controllers
{
  [Route("api/[controller]")]
  [ApiController]
  public class StateController : ControllerBase
  {
    private readonly IState _state;
    private readonly GredDbContext _context;

    public StateController(IState state, GredDbContext context)
    {
      this._state = state;
      this._context = context;
    }
    [HttpGet("GetState")]
    public async Task<IActionResult> GetState()
    {
      var data = await _context.States.Where(m=>m.CountryId==101).ToListAsync();
      return Ok(data);
    }
   
  }
}
