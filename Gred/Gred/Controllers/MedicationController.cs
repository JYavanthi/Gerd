using gred.Data;
using Gred.Data.Entities;
using Gred.Data.Entities.Common;
using Gred.Services.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Gred.Controllers
{
  [Route("api/[controller]")]
  [ApiController]
  public class MedicationController : ControllerBase
  {
    private readonly GredDbContext _context;
    private readonly IMedicationService _medicationService;

    public MedicationController(GredDbContext context, IMedicationService medicationService)
    {
      _context = context;
      _medicationService = medicationService;
    }

    [HttpPost("SaveMedication")]
    public async Task<CommonRsult> SaveMedication(EMedication medication)
    {
      return await _medicationService.SaveMedication(medication);
    }

    [HttpGet("GetMedicationByPatientId/{patientId}/{stage}")]
    public async Task<CommonRsult> GetMedicationByPatientId(int patientId ,int stage)
    {
      return await _medicationService.GetMedicationByPatientId(patientId);
    }
  }
}
