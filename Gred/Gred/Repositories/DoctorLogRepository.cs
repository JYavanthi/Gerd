using System.Data;
using gred.Data;
using Gred.Data.Entities;
using Gred.Data.Entities.Common;
using Gred.Services.Interface;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

namespace Gred.Repositories
{
  public class DoctorLogRepository : IDoctorLog
  {
    private readonly GredDbContext _context;
    public DoctorLogRepository(GredDbContext context)
    {
      this._context = context;
    }

    public async Task<CommonRsult> GetDoctorLogs()
    {
      CommonRsult result = new CommonRsult();
      try
      {
        var data = await _context.VwDoctorLogs.ToListAsync();
        result.Data = data;
      }
      catch (Exception ex)
      {
        result.Message = ex.Message;
      }
      return result;
    }

    public async Task<CommonRsult> SaveDoctorLogs(EDoctorLog eDoctorLog)
    {
      CommonRsult result = new CommonRsult();
      try
      {
        DataTable dt = new DataTable();
        var con = (SqlConnection)_context.Database.GetDbConnection();
        using (var cmd = new SqlCommand("dbo.sp_DoctorLogs", con))
        {
          cmd.CommandType = CommandType.StoredProcedure;
          cmd.Parameters.AddWithValue("@Flag", eDoctorLog.Flag);
          cmd.Parameters.AddWithValue("@DoctorlogID", eDoctorLog.DoctorlogID);
          cmd.Parameters.AddWithValue("@DoctorID", eDoctorLog.DoctorID);
          cmd.Parameters.AddWithValue("@LoginTime", eDoctorLog.LoginTime);
          cmd.Parameters.AddWithValue("@LogoutTime", eDoctorLog.LogoutTime);
          cmd.Parameters.AddWithValue("@Token", eDoctorLog.Token);
          cmd.Parameters.AddWithValue("@CreatedBy", eDoctorLog.CreatedBy);
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
