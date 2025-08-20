using Gred.Data.Entities.Common;
using Gred.Data.Entities;
using System.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Gred.Data.Entities.Gred.Data.Entities;

[Route("api/[controller]")]
[ApiController]
public class PatientController : ControllerBase
{
  private readonly IPatientSubmit _repository;

  public PatientController(IPatientSubmit repository)
  {
    _repository = repository;
  }

  public class PatientStageDto
  {
    public int? Stage { get; set; }
    public int? PatientID { get; set; }
    public int CreatedBy { get; set; }
  }
}

//  [HttpPost("SubmitStage")]
////  public async Task<IActionResult> SubmitStage([FromBody] PatientStageDto dto)
////  {
////    if (dto == null)
////      return BadRequest("Request is null");

////    var result = await _repository.SubmitPatientStage(dto.Stage, dto.PatientID, dto.CreatedBy);

////    if (result.Type == "S")
////      return Ok(result);
////    else
////      return StatusCode(500, result);
////  }
////}

//public async Task<CommonRsult> SubmitStage(EPatitentSubmit ptn)
//{
//  CommonRsult result = new CommonRsult();
//  try
//  {
//    DataTable dt = new DataTable();
//    var con = (SqlConnection)_context.Database.GetDbConnection();
//    using (var cmd = new SqlCommand("dbo.sp_PatientDtlsSubmit", con))
//    {
//      cmd.CommandType = CommandType.StoredProcedure;

//      cmd.Parameters.AddWithValue("@Stage", ptn.Stage);
//      cmd.Parameters.AddWithValue("@PatientID", ptn.PatientID);
//      cmd.Parameters.AddWithValue("@CreatedBy", ptn.CreatedBy);

//      using (var da = new SqlDataAdapter(cmd))
//      {
//        await Task.Run(() => da.Fill(dt));
//        result.Type = "S";
//        result.Message = "Insert Successfully";
//      }
//    }
//  }

//  catch (Exception ex)
//  {
//    result.Type = "E";
//    result.Message = ex.Message;
//  }
//  return result;
//}
//  }
