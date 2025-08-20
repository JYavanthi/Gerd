using Gred.Data.Entities.Common;
using Gred.Data.Entities;
using Gred.Services.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using gred.Models;
using gred.Data;

namespace Gred.Controllers
{
  [Route("api/[controller]")]
  [ApiController]
  public class DiagnosisController : ControllerBase
  {
    private readonly GredDbContext _context;
    private readonly IDiagnosis _Diagnosis;

    public DiagnosisController(GredDbContext context, IDiagnosis Diagnosis)
    {
      this._context = context;
      this._Diagnosis = Diagnosis;
    }
    [HttpPost("SaveDiagnosis")]
    public async Task<CommonRsult> SaveDoctorReg(GredDbContext context, EDiagnosis eDiagnosis)
    {
      return await _Diagnosis.SaveDiagnosis(eDiagnosis);
    }
    [HttpGet("GetDoctor")]
    public async Task<CommonRsult> GetDiagnosis()
    {
      return await _Diagnosis.GetDiagnosis();
    }

    [HttpGet("GetDiagnosisById/{patientId}")]
    public async Task<CommonRsult> GetDiagnosisById(int patientId)
    {
      return await _Diagnosis.GetDiagnosisById(patientId);
    }
  }
}
