using Gred.Data.Entities.Common;
using Gred.Data.Entities;
using Gred.Services.Interface;
using Microsoft.AspNetCore.Mvc;
using gred.Models;

namespace Gred.Controllers
{
  [Route("api/[controller]")]
  [ApiController]
  public class AssessmentController : Controller
  {

    private readonly IAssessment _assessment;

    public AssessmentController(IAssessment assessment)
    {
      this._assessment = assessment;
    }
    [HttpGet("GetAssessment")]
    public async Task<CommonRsult> GetAssessment()
    {
      return await _assessment.GetAssessment();
    }

    [HttpPost("SaveAssessment")]
    public async Task<CommonRsult> SaveAssessment(EAssessment assessment)
    {
      return await _assessment.SaveAssessment(assessment);
    }

    [HttpGet("GetAssessmentById/{id}/{stage}")]
    public async Task<CommonRsult> GetAssessmentById(int id,int stage)
    {
      return await _assessment.GetAssessmentById(id,stage);

    }

  }
}
