using Gred.Data.Entities.Common;
using Gred.Data.Entities;
using Gred.Services.Interface;
using Microsoft.AspNetCore.Mvc;

namespace Gred.Controllers
{
  [Route("api/[controller]")]
  [ApiController]
  public class MedicalExaminationController : Controller
  {
    private readonly IMedicalExamination _medicalexamination;

    public MedicalExaminationController(IMedicalExamination medicalexamination)
    {
      this._medicalexamination = medicalexamination;
    }
    [HttpGet("GetMedicalExamination")]
    public async Task<CommonRsult> GetMedicalExamination()
    {
      return await _medicalexamination.GetMedicalExamination();
    }

    [HttpPost("SaveMedicalExamination")]
    public async Task<CommonRsult> SaveMedicalExamination(EMedicalExamination medicalexamination)
    {
      return await _medicalexamination.SaveMedicalExamination(medicalexamination);
    }

    [HttpGet("GetMedicalExaminationById/{patientId}")]
    public async Task<CommonRsult> GetMedicalExaminationById(int patientId)
    {
      return await _medicalexamination.GetMedicalExaminationById(patientId);
    }
  }
}
