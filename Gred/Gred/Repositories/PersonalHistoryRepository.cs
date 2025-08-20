using Gred.Data.Entities.Common;
using Gred.Data.Entities;
using gred.Data;
using Microsoft.Data.SqlClient;
using System.Data;
using Gred.Services.Interface;
using Microsoft.EntityFrameworkCore;

namespace Gred.Repositories
{
  public class PersonalHistoryRepository : IPersonalHistory
  {
    private readonly GredDbContext _context;

    public PersonalHistoryRepository(GredDbContext context)
    {
      this._context = context;
    }

    public async Task<CommonRsult> GetPersonalHistory()
    {
      CommonRsult result = new CommonRsult();
      try
      {
        var data = await _context.PersonalHistories.ToListAsync();
        result.Data = data;
        result.Message = "Successfully retrieved";
      }
      catch (Exception ex)
      {
        result.Type = "E";
        result.Message = ex.Message;
      }
      return result;
    }

    public async Task<CommonRsult> GetPersonalHistoryById(int id, int stage)
    {
      CommonRsult result = new CommonRsult();
      try
      {
        var patient = await _context.PersonalHistories
     .Where(p => p.PatientId == id && p.Stage==stage)
     .OrderByDescending(p => p.Stage) // ✅ ensures latest stage
     .FirstOrDefaultAsync();


        if (patient != null)
        {
          result.Type = "S";
          result.Message = "Patient found.";
          result.Data = patient;
          result.Count = 1;
        }
        else
        {
          result.Type = "E";
          result.Message = "Patient not found.";
          result.Data = null;
          result.Count = 0;
        }
      }
      catch (Exception ex)
      {
        result.Type = "E";
        result.Message = ex.Message;
      }

      return result;
    }

    public async Task<CommonRsult> SavePersonalHistory(EPersonalHistory personalhistory)
    {
      CommonRsult result = new CommonRsult();
      try
      {
        DataTable dt = new DataTable();
        var con = (SqlConnection)_context.Database.GetDbConnection();

        using (var cmd = new SqlCommand("dbo.sp_PersonalHistory", con))
        {
          cmd.CommandType = CommandType.StoredProcedure;
          cmd.Parameters.AddWithValue("@Flag", personalhistory.Flag);
          cmd.Parameters.AddWithValue("@PersonalHistoryId", (object?)personalhistory.PersonalHistoryId ?? DBNull.Value);
          cmd.Parameters.AddWithValue("@DoctorId", (object?)personalhistory.DoctorId ?? DBNull.Value);
          cmd.Parameters.AddWithValue("@PatientId", (object?)personalhistory.PatientId ?? DBNull.Value);
          cmd.Parameters.AddWithValue("@Stage", (object?)personalhistory.Stage ?? DBNull.Value);

          cmd.Parameters.AddWithValue("@AeratedIntake", (object?)personalhistory.AeratedIntake ?? DBNull.Value);
          cmd.Parameters.AddWithValue("@AeratedFrequency", (object?)personalhistory.AeratedFrequency ?? DBNull.Value);
          cmd.Parameters.AddWithValue("@AeratedQuantity", (object?)personalhistory.AeratedQuantity ?? DBNull.Value);
          cmd.Parameters.AddWithValue("@AeratedDuration", (object?)personalhistory.AeratedDuration ?? DBNull.Value);

          cmd.Parameters.AddWithValue("@CoffeeIntake", (object?)personalhistory.CoffeeIntake ?? DBNull.Value);
          cmd.Parameters.AddWithValue("@CoffeeFrequency", (object?)personalhistory.CoffeeFrequency ?? DBNull.Value);
          cmd.Parameters.AddWithValue("@CoffeeQuantity", (object?)personalhistory.CoffeeQuantity ?? DBNull.Value);
          cmd.Parameters.AddWithValue("@CoffeeDuration", (object?)personalhistory.CoffeeDuration ?? DBNull.Value);

          cmd.Parameters.AddWithValue("@TeaIntake", (object?)personalhistory.TeaIntake ?? DBNull.Value);
          cmd.Parameters.AddWithValue("@TeaFrequency", (object?)personalhistory.TeaFrequency ?? DBNull.Value);
          cmd.Parameters.AddWithValue("@TeaQuantity", (object?)personalhistory.TeaQuantity ?? DBNull.Value);
          cmd.Parameters.AddWithValue("@TeaDuration", (object?)personalhistory.TeaDuration ?? DBNull.Value);

          cmd.Parameters.AddWithValue("@SpicyIntake", (object?)personalhistory.SpicyIntake ?? DBNull.Value);
          cmd.Parameters.AddWithValue("@SpicyFrequency", (object?)personalhistory.SpicyFrequency ?? DBNull.Value);
          cmd.Parameters.AddWithValue("@SpicyQuantity", (object?)personalhistory.SpicyQuantity ?? DBNull.Value);
          cmd.Parameters.AddWithValue("@SpicyDuration", (object?)personalhistory.SpicyDuration ?? DBNull.Value);

          cmd.Parameters.AddWithValue("@AlcoholIntake", (object?)personalhistory.AlcoholIntake ?? DBNull.Value);
          cmd.Parameters.AddWithValue("@AlcoholFrequency", (object?)personalhistory.AlcoholFrequency ?? DBNull.Value);
          cmd.Parameters.AddWithValue("@AlcoholQuantity", (object?)personalhistory.AlcoholQuantity ?? DBNull.Value);
          cmd.Parameters.AddWithValue("@AlcoholDuration", (object?)personalhistory.AlcoholDuration ?? DBNull.Value);

          cmd.Parameters.AddWithValue("@SweetsIntake", (object?)personalhistory.SweetsIntake ?? DBNull.Value);
          cmd.Parameters.AddWithValue("@SweetsFrequency", (object?)personalhistory.SweetsFrequency ?? DBNull.Value);
          cmd.Parameters.AddWithValue("@SweetsQuantity", (object?)personalhistory.SweetsQuantity ?? DBNull.Value);
          cmd.Parameters.AddWithValue("@SweetsDuration", (object?)personalhistory.SweetsDuration ?? DBNull.Value);

          cmd.Parameters.AddWithValue("@SmokingIntake", (object?)personalhistory.SmokingIntake ?? DBNull.Value);
          cmd.Parameters.AddWithValue("@SmokingFrequency", (object?)personalhistory.SmokingFrequency ?? DBNull.Value);
          cmd.Parameters.AddWithValue("@SmokingQuantity", (object?)personalhistory.SmokingQuantity ?? DBNull.Value);
          cmd.Parameters.AddWithValue("@SmokingDuration", (object?)personalhistory.SmokingDuration ?? DBNull.Value);

          cmd.Parameters.AddWithValue("@TobaccoIntake", (object?)personalhistory.TobaccoIntake ?? DBNull.Value);
          cmd.Parameters.AddWithValue("@TobaccoFrequency", (object?)personalhistory.TobaccoFrequency ?? DBNull.Value);
          cmd.Parameters.AddWithValue("@TobaccoQuantity", (object?)personalhistory.TobaccoQuantity ?? DBNull.Value);
          cmd.Parameters.AddWithValue("@TobaccoDuration", (object?)personalhistory.TobaccoDuration ?? DBNull.Value);

          cmd.Parameters.AddWithValue("@CreatedBy", personalhistory.CreatedBy);

          if (con.State != ConnectionState.Open)
            await con.OpenAsync();

          using (var da = new SqlDataAdapter(cmd))
          {
            da.Fill(dt);
          }

          result.Type = "S";
          result.Message = "Insert Successfully";
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
}
