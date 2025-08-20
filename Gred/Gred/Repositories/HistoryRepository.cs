using Gred.Data.Entities.Common;
using Gred.Data.Entities;
using gred.Data;
using Microsoft.Data.SqlClient;
using System.Data;
using Gred.Services.Interface;
using Microsoft.EntityFrameworkCore;
using gred.Models;

namespace Gred.Repositories
{
  public class HistoryRepository : IHistory
  {
    private readonly GredDbContext _context;

    public HistoryRepository(GredDbContext context)
    {
      this._context = context;
    }

    public async Task<CommonRsult> GetHistory()
    {
      CommonRsult result = new CommonRsult();
      try
      {
        var data = await _context.Histories.ToListAsync();
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

    public async Task<CommonRsult> GetHistoryById(int id,int stage)
    {
      var result = new CommonRsult();

      var complaint = await _context.Histories
                                    .Where(c => c.PatientId == id && c.Stage==stage)
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

    public async Task<CommonRsult> SaveHistory(EHistory history)
    {
      CommonRsult result = new CommonRsult();
      try
      {
        DataTable dt = new DataTable();
        var con = (SqlConnection)_context.Database.GetDbConnection();
        using (var cmd = new SqlCommand("dbo.sp_History", con))
        {

          cmd.CommandType = CommandType.StoredProcedure;

          cmd.Parameters.AddWithValue("@Flag", history.Flag);
          cmd.Parameters.AddWithValue("@DoctorID", history.DoctorID ?? (object)DBNull.Value);
          cmd.Parameters.AddWithValue("@PatientID", history.PatientID);
          cmd.Parameters.AddWithValue("@Past_History",history.Past_History);
          cmd.Parameters.AddWithValue("@Diet_Vegetarian",history.Diet_Vegetarian);
          cmd.Parameters.AddWithValue("@Diet_NonVegetarian",history.Diet_NonVegetarian);
          cmd.Parameters.AddWithValue("@CreatedBy", history.CreatedBy);

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
