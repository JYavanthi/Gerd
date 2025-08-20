using gred.Data;
using Gred.Data.Entities.Common;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics.Metrics;

[ApiController]
[Route("api/[controller]")]
public class CountryController : ControllerBase
{
  private readonly GredDbContext _context;

  public CountryController(GredDbContext context)
  {
    _context = context;
  }


  //public async Task<ActionResult<Countries>> GetCountries()
  //{
  //  var country = await _context.Countries.ToListAsync();

  //  if (country == null)
  //    return NotFound();

  //  return country;
  //}
  [HttpGet("GetCountries")]
  public async Task<CommonRsult> GetCountries()
  {
    return await GetCountries();
  }
}


