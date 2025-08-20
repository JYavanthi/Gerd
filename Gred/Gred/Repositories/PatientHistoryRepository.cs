using gred.Data;
using gred.Models;
using Gred.Data;
using Gred.Data.Entities;
using Gred.Data.Entities.Common;
using Gred.Services.Interface;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System.Data;

namespace Gred.Repositories
{
  public class PatientHistoryRepository : IPatientHistory
  {
    private readonly GredDbContext _context;

        public PatientHistoryRepository(GredDbContext context)
        {
            this._context = context;
        }

    //public async Task<CommonRsult> GetPatientHistoryById(int id)
    //{
    //  var result = new CommonRsult();

    //  var complaint = await _context.VwPatientHistories
    //                                .Where(c => c.PatientHistoryId == id)
    //                                .FirstOrDefaultAsync();

    //  if (complaint != null)
    //  {
    //    result.Type = "S";
    //    result.Message = "Data fetched successfully.";
    //    result.Data = complaint;
    //  }
    //  else
    //  {
    //    result.Type = "E";
    //    result.Message = "No data found for this Patient ID.";
    //    result.Data = null;
    //  }

    //  return result;
    //}


    public async Task<CommonRsult> GetPatientHistory()
    {
      CommonRsult result = new CommonRsult();
      try
      {
        var data = await _context.VwPatients.ToListAsync();
        result.Data = data;
        result.Type = "S";
        result.Message = "Successfully";
      }
      catch (Exception ex)
      {
        result.Type = "E";
        result.Message = ex.Message;
      }
      return result;
    }

    public async Task<CommonRsult> GetPatientHistoryById(int id)
    {
      CommonRsult result = new CommonRsult();
      try
      {
        var patient = await _context.PatientHistories
            .FirstOrDefaultAsync(p => p.PatientHistoryId == id);

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

    public async Task<CommonRsult> SavePatientHistory(EPatientHistory patientHistory)
    {
      CommonRsult result = new CommonRsult();
      try
      {
        DataTable dt = new DataTable();
        var con = (SqlConnection)_context.Database.GetDbConnection();
        using (var cmd = new SqlCommand("dbo.sp_PatientHistory", con))
        {
          cmd.CommandType = CommandType.StoredProcedure;

          cmd.Parameters.AddWithValue("@Flag", patientHistory.Flag);
          cmd.Parameters.AddWithValue("@PatientHistoryID", patientHistory.PatientHistoryID);
          cmd.Parameters.AddWithValue("@PatientID", patientHistory.PatientID);

          cmd.Parameters.AddWithValue("@Stage", patientHistory.Stage);

          cmd.Parameters.AddWithValue("@AD_Intake", patientHistory.AD_Intake);
          cmd.Parameters.AddWithValue("@AD_Frequency", patientHistory.AD_Frequency);
          cmd.Parameters.AddWithValue("@AD_Quantity", patientHistory.AD_Quantity);
          cmd.Parameters.AddWithValue("@AD_Duration", patientHistory.AD_Duration);
          cmd.Parameters.AddWithValue("@CF_Intake", patientHistory.CF_Intake);
          cmd.Parameters.AddWithValue("@CF_Frequency", patientHistory.CF_Frequency);
          cmd.Parameters.AddWithValue("@CF_Quantity", patientHistory.CF_Quantity);
          cmd.Parameters.AddWithValue("@CF_Duration", patientHistory.CF_Duration);
          cmd.Parameters.AddWithValue("@T_Intake", patientHistory.T_Intake);
          cmd.Parameters.AddWithValue("@T_Frequency", patientHistory.T_Frequency);
          cmd.Parameters.AddWithValue("@T_Quantity", patientHistory.T_Quantity);
          cmd.Parameters.AddWithValue("@T_Duration", patientHistory.T_Duration);
          cmd.Parameters.AddWithValue("@SF_Intake", patientHistory.SF_Intake);
          cmd.Parameters.AddWithValue("@SF_Frequency", patientHistory.SF_Frequency);
          cmd.Parameters.AddWithValue("@SF_Quantity", patientHistory.SF_Quantity);
          cmd.Parameters.AddWithValue("@SF_Duration", patientHistory.SF_Duration);
          cmd.Parameters.AddWithValue("@AH_Intake", patientHistory.AH_Intake);
          cmd.Parameters.AddWithValue("@AH_Frequency", patientHistory.AH_Frequency);
          cmd.Parameters.AddWithValue("@AH_Quantity", patientHistory.AH_Quantity);
          cmd.Parameters.AddWithValue("@AH_Duration", patientHistory.AH_Duration);
          cmd.Parameters.AddWithValue("@CS_Intake", patientHistory.CS_Intake);
          cmd.Parameters.AddWithValue("@CS_Frequency", patientHistory.CS_Frequency);
          cmd.Parameters.AddWithValue("@CS_Quantity", patientHistory.CS_Quantity);
          cmd.Parameters.AddWithValue("@CS_Duration", patientHistory.CS_Duration);
          cmd.Parameters.AddWithValue("@S_Intake", patientHistory.S_Intake);
          cmd.Parameters.AddWithValue("@S_Frequency", patientHistory.S_Frequency);
          cmd.Parameters.AddWithValue("@S_Quantity", patientHistory.S_Quantity);
          cmd.Parameters.AddWithValue("@S_Duration", patientHistory.S_Duration);
          cmd.Parameters.AddWithValue("@TB_Intake", patientHistory.TB_Intake);
          cmd.Parameters.AddWithValue("@TB_Frequency", patientHistory.TB_Frequency);
          cmd.Parameters.AddWithValue("@TB_Quantity", patientHistory.TB_Quantity);
          cmd.Parameters.AddWithValue("@TB_Duration", patientHistory.TB_Duration);
          cmd.Parameters.AddWithValue("@G_Name", patientHistory.G_Name);
          cmd.Parameters.AddWithValue("@G_Usage", patientHistory.G_Usage);
          cmd.Parameters.AddWithValue("@G_Frequency", patientHistory.G_Frequency);
          cmd.Parameters.AddWithValue("@G_YearOfUsage", patientHistory.G_YearOfUsage);
          cmd.Parameters.AddWithValue("@WorkingHours", patientHistory.WorkingHours);
          cmd.Parameters.AddWithValue("@JobType", patientHistory.JobType);
          cmd.Parameters.AddWithValue("@Duration", patientHistory.Duration);
          cmd.Parameters.AddWithValue("@CreatedBy", patientHistory.CreatedBy);
          cmd.Parameters.AddWithValue("@ModifiedBy", patientHistory.ModifiedBy);
          cmd.Parameters.AddWithValue("@Past_History", patientHistory.Past_History);
          cmd.Parameters.AddWithValue("@Diet", patientHistory.Diet);
          cmd.Parameters.AddWithValue("@SleepApnea_Intake", patientHistory.SleepApnea_Intake);
          cmd.Parameters.AddWithValue("@SleepApnea_Frequency", patientHistory.SleepApnea_Frequency);
          cmd.Parameters.AddWithValue("@SleepApnea_Duration", patientHistory.SleepApnea_Duration);
          cmd.Parameters.AddWithValue("@Exercise_Intake", patientHistory.Exercise_Intake);
          cmd.Parameters.AddWithValue("@Walking_Intake", patientHistory.Walking_Intake);
          cmd.Parameters.AddWithValue("@Walking_Frequency", patientHistory.Walking_Frequency);
          cmd.Parameters.AddWithValue("@Walking_Duration", patientHistory.Walking_Duration);
          cmd.Parameters.AddWithValue("@Jogging_Intake", patientHistory.Jogging_Intake);
          cmd.Parameters.AddWithValue("@Jogging_Frequency", patientHistory.Jogging_Frequency);
          cmd.Parameters.AddWithValue("@Jogging_Duration", patientHistory.Jogging_Duration);
          cmd.Parameters.AddWithValue("@Gym_Intake", patientHistory.Gym_Intake);
          cmd.Parameters.AddWithValue("@Gym_Frequency", patientHistory.Gym_Frequency);
          cmd.Parameters.AddWithValue("@Gym_Duration", patientHistory.Gym_Duration);
          cmd.Parameters.AddWithValue("@Yoga_Intake", patientHistory.Yoga_Intake);
          cmd.Parameters.AddWithValue("@Yoga_Frequency", patientHistory.Yoga_Frequency);
          cmd.Parameters.AddWithValue("@Yoga_Duration", patientHistory.Yoga_Duration);
          cmd.Parameters.AddWithValue("@Aerobics_Intake", patientHistory.Aerobics_Intake);
          cmd.Parameters.AddWithValue("@Aerobics_Frequency", patientHistory.Aerobics_Frequency);
          cmd.Parameters.AddWithValue("@Aerobics_Duration", patientHistory.Aerobics_Duration);
          cmd.Parameters.AddWithValue("@Zumba_Intake", patientHistory.Zumba_Intake);
          cmd.Parameters.AddWithValue("@Zumba_Frequency", patientHistory.Zumba_Frequency);
          cmd.Parameters.AddWithValue("@Zumba_Duration", patientHistory.Zumba_Duration);
          cmd.Parameters.AddWithValue("@OthersExercise_Intake", patientHistory.OthersExercise_Intake);
          cmd.Parameters.AddWithValue("@OthersExercise_Frequency", patientHistory.OthersExercise_Frequency);
          cmd.Parameters.AddWithValue("@OthersExercise_Duration", patientHistory.OthersExercise_Duration);

          using (var da = new SqlDataAdapter(cmd))
          {
            await Task.Run(() => da.Fill(dt));
            result.Type = "S";
            result.Message = "Saved Successfully";
          }
        }
      }
      catch (Exception ex)
      {
        result.Type = "E";
        result.Message = $"Error: {ex.Message}" + (ex.InnerException != null ? $" | {ex.InnerException.Message}" : "");
      }
      return result;


    }

    
  }
}
