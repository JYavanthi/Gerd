using Gred.Data.Entities.Common;
using Gred.Data.Entities;
using Gred.Services.Interface;
using Microsoft.AspNetCore.Mvc;
//using Gred.Models;
using Microsoft.AspNetCore.Authorization;
using gred.Data;

namespace Gred.Controllers
{

  [Route("api/[controller]")]
  [ApiController]
  public class CurrentMedicationController : Controller

  {
    private readonly ICurrentMedication _currentMedication;
    private readonly GredDbContext _context;

    public CurrentMedicationController(GredDbContext context, ICurrentMedication currentMedication)
    {
      this._context = context;
      this._currentMedication = currentMedication;
    }
    [HttpGet("GetCurrentMedication")]
    public async Task<CommonRsult> GetCurrentMedication()
    {
      return await _currentMedication.GetCurrentMedication();
    }

    [HttpPost("SaveCurrentMedication")]
    public async Task<CommonRsult> SaveCurrentMedication(ECurrentMedication eCurrentMedication)
    {
      return await _currentMedication.SaveCurrentMedication(eCurrentMedication);
    }

    [HttpGet("GetCurrentMedicationById/{patientId}")]
    public async Task<CommonRsult> GetCurrentMedicationById(int patientId)
    {
      return await _currentMedication.GetCurrentMedicationById(patientId);
    }
  }
}
