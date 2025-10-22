using gred.Data;
using gred.Models;
using Microsoft.AspNetCore.Mvc;

namespace Gred.Controllers
{
  [Route("api/[controller]")]
  [ApiController]
  public class EmailReminderController : Controller
  {
    private readonly IConfiguration _configuration;
    private readonly GredDbContext _context;

    public EmailReminderController(IConfiguration configuration, GredDbContext context)
    {
      _configuration = configuration;
      _context = context;
    }

    [HttpPost("SaveOrUpdate")]
    public IActionResult SaveOrUpdate([FromBody] EmailReminderLog log)
    {
      var existing = _context.EmailReminderLogs
          .FirstOrDefault(x => x.PatientId == log.PatientId && x.Stage == log.Stage);

      if (existing == null)
        _context.EmailReminderLogs.Add(log);
      else
      {
        existing.ReminderCount = log.ReminderCount;
        existing.LastSentDate = log.LastSentDate;
        existing.DueDays = log.DueDays;
      }

      _context.SaveChanges();
      return Ok(log);
    }


    [HttpGet("GetByPatient/{patientId}/{stage}")]
    public IActionResult GetByPatient(int patientId, int stage)
    {
      var log = _context.EmailReminderLogs
          .FirstOrDefault(x => x.PatientId == patientId && x.Stage == stage);

      if (log == null)
      {
        return NotFound();
      }

      return Ok(log);
    }
  }
}
