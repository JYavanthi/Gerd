using System.Runtime.InteropServices;


using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Gred.Models;
using DocumentFormat.OpenXml.Office2010.Excel;
using gred.Models;

namespace Gred.Controllers
{
  [Route("api/[controller]")]
  [ApiController]
  public class PincodeController : ControllerBase
  {
    private readonly gred.Data.GredDbContext _context;

    public PincodeController(gred.Data.GredDbContext context)
    {
      _context = context;
    }

    [HttpGet("GetPincodesByCity")]
    public async Task<IActionResult> GetPincodesByCity(int citiid)
    {
      try
      {
        var data = await _context.VwPincodes.Where(p => p.Citiid == citiid).ToListAsync();

        if (!data.Any())
          return NotFound($"No pincodes found for City Id {citiid}");

        return Ok(data);
      }
      catch (Exception ex)
      {
        return BadRequest(ex.Message);
      }
    }
  }
}

