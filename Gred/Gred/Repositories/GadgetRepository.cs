using Gred.Data.Entities.Common;
using Gred.Data.Entities;
using gred.Data;
using Microsoft.Data.SqlClient;
using System.Data;
using Gred.Services.Interface;
using Microsoft.EntityFrameworkCore;

namespace Gred.Repositories
{
  public class GadgetRepository : IGadget
  {
    private readonly GredDbContext _context;

    public GadgetRepository(GredDbContext context)
    {
      this._context = context;
    }

    public async Task<CommonRsult> GetGadget()
    {
      CommonRsult result = new CommonRsult();
      try
      {
        var data = await _context.Gadgets.ToListAsync();
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

    public async Task<CommonRsult> GetGadgetById(int PatientId ,int stage)
    {
      var result = new CommonRsult();

      var complaint = await _context.Gadgets
                                    .Where(c => c.PatientId == PatientId && c.Stage==stage)
                                    .OrderByDescending(c => c.Stage)
                                    .FirstOrDefaultAsync();

      if (complaint != null)
      {
        result.Type = "S";
        result.Message = "Data fetched successfully.";
        result.Data = complaint;
      }
      else
      {
        result.Type = "E";
        result.Message = "No data found for this Patient ID.";
        result.Data = null;
      }

      return result;
    }

    //public async Task<CommonRsult> SaveGadget(EGadget gadget)
    //{
    //  CommonRsult result = new CommonRsult();
    //  try
    //  {
    //    DataTable dt = new DataTable();
    //    var con = (SqlConnection)_context.Database.GetDbConnection();
    //    using (var cmd = new SqlCommand("dbo.sp_Gadget", con))
    //    {

    //      cmd.CommandType = CommandType.StoredProcedure;

    //      cmd.Parameters.AddWithValue("@Flag", gadget.Flag ?? (object)DBNull.Value);
    //      cmd.Parameters.AddWithValue("@Id", gadget.Id);  
    //      cmd.Parameters.AddWithValue("@PatientId", gadget.PatientId);
    //      cmd.Parameters.AddWithValue("@ComputerUsed", gadget.ComputerUsed.HasValue ? (object)gadget.ComputerUsed.Value : DBNull.Value);
    //      cmd.Parameters.AddWithValue("@ComputerFrequency", string.IsNullOrEmpty(gadget.ComputerFrequency) ? DBNull.Value : gadget.ComputerFrequency);
    //      cmd.Parameters.AddWithValue("@ComputerDurationYears", gadget.ComputerDurationYears ?? (object)DBNull.Value);
    //      cmd.Parameters.AddWithValue("@SmartphoneUsed", gadget.SmartphoneUsed.HasValue ? (object)gadget.SmartphoneUsed.Value : DBNull.Value);
    //      cmd.Parameters.AddWithValue("@SmartphoneFrequency", string.IsNullOrEmpty(gadget.SmartphoneFrequency) ? DBNull.Value : gadget.SmartphoneFrequency);
    //      cmd.Parameters.AddWithValue("@SmartphoneDurationYears", gadget.SmartphoneDurationYears ?? (object)DBNull.Value);
    //      cmd.Parameters.AddWithValue("@WorkingHours", string.IsNullOrEmpty(gadget.WorkingHours) ? DBNull.Value : gadget.WorkingHours);
    //      cmd.Parameters.AddWithValue("@JobType", string.IsNullOrEmpty(gadget.JobType) ? DBNull.Value : gadget.JobType);
    //      cmd.Parameters.AddWithValue("@TotalWorkingYears", gadget.TotalWorkingYears ?? (object)DBNull.Value);
    //      cmd.Parameters.AddWithValue("@CreatedBy", string.IsNullOrEmpty(gadget.CreatedBy) ? DBNull.Value : gadget.CreatedBy);


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

    public async Task<CommonRsult> SaveGadget(EGadget gadget)
    {
      CommonRsult result = new CommonRsult();
      try
      {
        DataTable dt = new DataTable();
        var con = (SqlConnection)_context.Database.GetDbConnection();

        using (var cmd = new SqlCommand("dbo.sp_Gadget", con))
        {
          cmd.CommandType = CommandType.StoredProcedure;

          cmd.Parameters.AddWithValue("@Flag", gadget.Flag ?? (object)DBNull.Value);

          
          if (gadget.Flag == "U" || gadget.Flag == "D")
            cmd.Parameters.AddWithValue("@Id", gadget.Id);
          else
            cmd.Parameters.AddWithValue("@Id", DBNull.Value);


          if (gadget.Flag == "I" || gadget.Flag == "U")
            cmd.Parameters.AddWithValue("@PatientId", gadget.PatientId);
          else
            cmd.Parameters.AddWithValue("@PatientId", DBNull.Value);
          cmd.Parameters.AddWithValue("@Stage", gadget.Stage);
          cmd.Parameters.AddWithValue("@ComputerUsed", gadget.ComputerUsed.HasValue ? (object)gadget.ComputerUsed.Value : DBNull.Value);
          cmd.Parameters.AddWithValue("@ComputerFrequency", string.IsNullOrEmpty(gadget.ComputerFrequency) ? DBNull.Value : gadget.ComputerFrequency);
          cmd.Parameters.AddWithValue("@ComputerDurationYears", gadget.ComputerDurationYears.HasValue ? (object)gadget.ComputerDurationYears.Value : DBNull.Value);
          cmd.Parameters.AddWithValue("@SmartphoneUsed", gadget.SmartphoneUsed.HasValue ? (object)gadget.SmartphoneUsed.Value : DBNull.Value);
          cmd.Parameters.AddWithValue("@SmartphoneFrequency", string.IsNullOrEmpty(gadget.SmartphoneFrequency) ? DBNull.Value : gadget.SmartphoneFrequency);
          cmd.Parameters.AddWithValue("@SmartphoneDurationYears", gadget.SmartphoneDurationYears.HasValue ? (object)gadget.SmartphoneDurationYears.Value : DBNull.Value);
          cmd.Parameters.AddWithValue("@WorkingHours", string.IsNullOrEmpty(gadget.WorkingHours) ? DBNull.Value : gadget.WorkingHours);
          cmd.Parameters.AddWithValue("@JobType", string.IsNullOrEmpty(gadget.JobType) ? DBNull.Value : gadget.JobType);
          cmd.Parameters.AddWithValue("@TotalWorkingYears", gadget.TotalWorkingYears.HasValue ? (object)gadget.TotalWorkingYears.Value : DBNull.Value);
          cmd.Parameters.AddWithValue("@CreatedBy", string.IsNullOrEmpty(gadget.CreatedBy) ? DBNull.Value : gadget.CreatedBy);

          using (var da = new SqlDataAdapter(cmd))
          {
            await Task.Run(() => da.Fill(dt));
            result.Type = "S";
            result.Message = "Operation Successful";
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
