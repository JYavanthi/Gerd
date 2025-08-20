using Gred.Data.Entities.Common;
using gred.Data;
using Microsoft.Data.SqlClient;
using System.Data;
using Microsoft.EntityFrameworkCore;
namespace Gred.Repositories;



public class PatientSubmitRepository : IPatientSubmit

{
  private readonly GredDbContext _context;

  public PatientSubmitRepository(GredDbContext context)
  {
    this._context = context;
  }

  public async Task<CommonRsult> SubmitPatientStage(int? stage, int? patientId, int createdBy)
  {
    CommonRsult result = new CommonRsult();
    try
    {
      var con = (SqlConnection)_context.Database.GetDbConnection();

      using (var cmd = new SqlCommand("dbo.sp_PatientDtlsSubmit", con))
      {
        cmd.CommandType = CommandType.StoredProcedure;

        cmd.Parameters.AddWithValue("@Stage", (object)stage ?? DBNull.Value);
        cmd.Parameters.AddWithValue("@PatientID", (object)patientId ?? DBNull.Value);
        cmd.Parameters.AddWithValue("@CreatedBy", createdBy);

        if (con.State != ConnectionState.Open)
          await con.OpenAsync();

        await cmd.ExecuteNonQueryAsync();

        result.Type = "S";
        result.Message = "Patient stage submitted successfully.";
      }
    }
    catch (Exception ex)
    {
      result.Type = "E";
      result.Message = ex.Message;
    }

    return result;
  }
}

