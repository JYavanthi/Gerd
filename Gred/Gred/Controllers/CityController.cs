using Gred.Services.Interface;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

[ApiController]
[Route("api/[controller]")]
public class CityController : ControllerBase
{
  private readonly gred.Data.GredDbContext _context;
  private readonly ICities _state;

  public CityController(gred.Data.GredDbContext context)
  {
    this._context = context;
  }


  [HttpGet("GetCitiesById")]
  public async Task<IActionResult> GetCities(int stateId)
  {
    try
    {
      var data = await _context.Cities.Where(m => stateId == m.StateId).ToListAsync();
      return Ok(data);
    }
    catch (Exception ex)
    {
      return BadRequest(ex.Message);
    }
  }
}
