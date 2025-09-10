using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using gred.Data;
using Microsoft.EntityFrameworkCore;

namespace Gred.Controllers
{
  [Route("api/[controller]")]
  [ApiController]
  public class DoctorReportController : Controller
  {
    private readonly IConfiguration _configuration;
    private readonly GredDbContext _context;

    public DoctorReportController(IConfiguration configuration, GredDbContext context)
    {
      _configuration = configuration;
      _context = context;
    }

    [HttpGet("DownloadDoctorReport")]
    public async Task<IActionResult> DownloadDoctorReport()
    {
      try
      {
        var data = await _context.VwDoctorRpts.ToListAsync();
        return Ok(data);
      }
      catch (Exception ex)
      {
        return Ok(ex.Message);
      }
    }
  }
}
