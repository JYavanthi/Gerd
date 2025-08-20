using Gred.Data.Entities.Common;
using Gred.Data.Entities;
using gred.Data;
using Microsoft.Data.SqlClient;
using System.Data;
using Microsoft.EntityFrameworkCore;
using Gred.Services.Interface;

namespace Gred.Repositories
{
  public class SleepRepository : ISleep
  {
    private readonly GredDbContext _context;

    public SleepRepository(GredDbContext context)
    {
      this._context = context;
    }

    public async Task<CommonRsult> GetSleep()
    {
      CommonRsult result = new CommonRsult();
      try
      {
        var data = await _context.PatientHistories.ToListAsync();
        result.Data = data;
        result.Message = "Successfully Added";
        result.Type = "S";
        result.Count = data.Count();
      }
      catch (Exception ex)
      {
        result.Type = "E";
        result.Message = ex.Message;
      }
      return result;
    }

    public async Task<CommonRsult> GetSleepByPatientId(int patientId)
    {
      CommonRsult result = new CommonRsult();
      try
      {
        var patient = await _context.Sleeps
        .Where(p => p.PatientId == patientId)
        .OrderByDescending(p => p.Stage) // ✅ get latest stage
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

    public async Task<CommonRsult> SaveSleep(ESleep sleep)
    {
      CommonRsult result = new CommonRsult();
      try
      {
        DataTable dt = new DataTable();
        var con = (SqlConnection)_context.Database.GetDbConnection();
        using (var cmd = new SqlCommand("dbo.sp_Sleep", con))
        {

          cmd.CommandType = CommandType.StoredProcedure;

          cmd.Parameters.AddWithValue("@Flag", sleep.Flag ?? (object)DBNull.Value);
          cmd.Parameters.AddWithValue("@Id", sleep.Id);
          cmd.Parameters.AddWithValue("@PatientId", sleep.PatientId);
          cmd.Parameters.AddWithValue("@Stage", sleep.Stage);

          cmd.Parameters.AddWithValue("@sleepApneayes", sleep.SleepApneayes ?? (object)DBNull.Value);
          cmd.Parameters.AddWithValue("@sleepApneano", sleep.SleepApneano ?? (object)DBNull.Value);
          cmd.Parameters.AddWithValue("@sleepApneaFrequency", sleep.SleepApneaFrequency ?? (object)DBNull.Value);
          cmd.Parameters.AddWithValue("@sleepApneaDuration", sleep.SleepApneaDuration ?? (object)DBNull.Value);

          cmd.Parameters.AddWithValue("@exerciseIntakeyes", sleep.ExerciseIntakeyes ?? (object)DBNull.Value);
          cmd.Parameters.AddWithValue("@exerciseIntakeno", sleep.ExerciseIntakeno ?? (object)DBNull.Value);

          cmd.Parameters.AddWithValue("@joggingSelectedyes", sleep.JoggingSelectedyes ?? (object)DBNull.Value);
          cmd.Parameters.AddWithValue("@joggingSelectedno", sleep.JoggingSelectedno ?? (object)DBNull.Value);
          cmd.Parameters.AddWithValue("@joggingFrequency", sleep.JoggingFrequency ?? (object)DBNull.Value);
          cmd.Parameters.AddWithValue("@joggingDuration", sleep.JoggingDuration ?? (object)DBNull.Value);

          cmd.Parameters.AddWithValue("@gymSelectedyes", sleep.GymSelectedyes ?? (object)DBNull.Value);
          cmd.Parameters.AddWithValue("@gymSelectedno", sleep.GymSelectedno ?? (object)DBNull.Value);
          cmd.Parameters.AddWithValue("@gymFrequency", sleep.GymFrequency ?? (object)DBNull.Value);
          cmd.Parameters.AddWithValue("@gymDuration", sleep.GymDuration ?? (object)DBNull.Value);

          cmd.Parameters.AddWithValue("@yogaSelectedyes", sleep.YogaSelectedyes ?? (object)DBNull.Value);
          cmd.Parameters.AddWithValue("@yogaSelectedno", sleep.YogaSelectedno ?? (object)DBNull.Value);
          cmd.Parameters.AddWithValue("@yogaFrequency", sleep.YogaFrequency ?? (object)DBNull.Value);
          cmd.Parameters.AddWithValue("@yogaDuration", sleep.YogaDuration ?? (object)DBNull.Value);

          cmd.Parameters.AddWithValue("@walkingSelectedyes", sleep.WalkingSelectedyes ?? (object)DBNull.Value);
          cmd.Parameters.AddWithValue("@walkingSelectedno", sleep.WalkingSelectedno ?? (object)DBNull.Value);
          cmd.Parameters.AddWithValue("@walkingFrequency", sleep.WalkingFrequency ?? (object)DBNull.Value);
          cmd.Parameters.AddWithValue("@walkingDuration", sleep.WalkingDuration ?? (object)DBNull.Value);

          cmd.Parameters.AddWithValue("@aerobicsyes", sleep.Aerobicsyes ?? (object)DBNull.Value);
          cmd.Parameters.AddWithValue("@aerobicsno", sleep.Aerobicsno ?? (object)DBNull.Value);
          cmd.Parameters.AddWithValue("@aerobicsFrequency", sleep.AerobicsFrequency ?? (object)DBNull.Value);
          cmd.Parameters.AddWithValue("@aerobicsDuration", sleep.AerobicsDuration ?? (object)DBNull.Value);

          cmd.Parameters.AddWithValue("@zumbayes", sleep.Zumbayes ?? (object)DBNull.Value);
          cmd.Parameters.AddWithValue("@zumbano", sleep.Zumbano ?? (object)DBNull.Value);
          cmd.Parameters.AddWithValue("@zumbaFrequency", sleep.ZumbaFrequency ?? (object)DBNull.Value);
          cmd.Parameters.AddWithValue("@zumbaDuration", sleep.ZumbaDuration ?? (object)DBNull.Value);

          cmd.Parameters.AddWithValue("@othersyes", sleep.Othersyes ?? (object)DBNull.Value);
          cmd.Parameters.AddWithValue("@othersText", sleep.othersText ?? (object)DBNull.Value);
          cmd.Parameters.AddWithValue("@othersno", sleep.Othersno ?? (object)DBNull.Value);
          cmd.Parameters.AddWithValue("@othersFrequency", sleep.OthersFrequency ?? (object)DBNull.Value);
          cmd.Parameters.AddWithValue("@othersDuration", sleep.OthersDuration ?? (object)DBNull.Value);
          cmd.Parameters.AddWithValue("@ModifiedDt", (object)DateTime.Now ?? DBNull.Value);
          cmd.Parameters.AddWithValue("@CreatedAt", DateTime.Now);

          cmd.Parameters.AddWithValue("@CreatedBy", sleep.CreatedBy);


          using (var da = new SqlDataAdapter(cmd))
          {
            await Task.Run(() => da.Fill(dt));
            result.Type = "S";
            result.Message = "Insert Successfully";
          }
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
