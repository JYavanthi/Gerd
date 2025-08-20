using Gred.Data.Entities.Common;
using Gred.Data.Entities;
using Gred.Services.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Gred.Data.Entities.Gred.Data.Entities;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System.Data;
using Microsoft.AspNetCore.Http.HttpResults;

namespace Gred.Controllers
{
  [Route("api/[controller]")]
  [ApiController]



  public class ManagementController : ControllerBase
  {
    private readonly IManagement _management;
    private readonly IDbService _db;
    private readonly IManagement _repository;

    public ManagementController(IManagement management, IDbService db, IManagement repository)
    {
      this._management = management;
      _db = db;
      _repository = repository;

    }
    //private readonly IManagement _repository;

    //public ManagementController(IManagement repository)
    //{
    //  _repository = repository;
    //}


    [HttpGet("GetManagement")]
    public async Task<CommonRsult> GetManagement()
    {
      return await _management.GetManagement();
    }

    [HttpPost("SaveManagement")]
    public async Task<CommonRsult> SaveManagement(EManagement eManagement)
    {
      return await _management.SaveManagement(eManagement);
    }

    [HttpGet("GetManagementById/{id}/{stage}")]
    public async Task<CommonRsult> GetManagementById(int id, int stage)
    {
      return await _management.GetManagementById(id ,stage);

    }
    [HttpGet("GetCurrentStage/{patientId}/{stage}")]
    public async Task<IActionResult> GetCurrentStage(int patientId)
    {
      var currentStage = await _db.ExecuteScalarAsync<int?>(@"
        SELECT MAX(Stage) FROM dbo.Management WHERE PatientID = @PatientID
    ", new { PatientID = patientId }) ?? 0;

      return Ok(new { currentStage });
    }

    [HttpPost("CompleteCase")]
    public async Task<IActionResult> SubmitStage(EStageUpdate stageUpdateInstance)
    {
      var result = await _management.SubmitStage(stageUpdateInstance);
      return Ok(result);
    } 
  }
}
