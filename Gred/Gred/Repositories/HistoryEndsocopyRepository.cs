using Gred.Data.Entities.Common;
using Gred.Data.Entities;
using gred.Data;
using System.Data;
using Gred.Services.Interface;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Gred.Data;

namespace Gred.Repositories
{
  public class HistoryEndsocopyRepository : IHistoryEndsocopy
  {
    private readonly GredDbContext _context;

    public HistoryEndsocopyRepository(GredDbContext context)
    {
      this._context = context;
    }

    public async Task<CommonRsult> GetHistoryEndsocopy()
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

    public async Task<CommonRsult> SaveHistoryEndsocopy(EHistoryEndsocopy ehistoryendsocopy)
    {
      CommonRsult result = new CommonRsult();
      try
      {
        DataTable dt = new DataTable();
        var con = (SqlConnection)_context.Database.GetDbConnection();
        using (var cmd = new SqlCommand("dbo.sp_HistoryEndsocopy", con))
        {

          cmd.CommandType = CommandType.StoredProcedure;
          cmd.Parameters.AddWithValue("@Flag", ehistoryendsocopy.Flag);
          cmd.Parameters.AddWithValue("@Id", ehistoryendsocopy.Id);
          cmd.Parameters.AddWithValue("@PatientId", ehistoryendsocopy.PatientId);
          cmd.Parameters.AddWithValue("@Endoscopy_History", ehistoryendsocopy.Endoscopy_History);
          cmd.Parameters.AddWithValue("@Endoscopy_Date", ehistoryendsocopy.Endoscopy_Date);
          cmd.Parameters.AddWithValue("@Endoscopy_ReportAttached", ehistoryendsocopy.Endoscopy_ReportAttached);
          cmd.Parameters.AddWithValue("@Endoscopy_Remarks", ehistoryendsocopy.Endoscopy_Remarks);
          cmd.Parameters.AddWithValue("@GastroSurgery_History", ehistoryendsocopy.GastroSurgery_History);
          cmd.Parameters.AddWithValue("@Bariatric_Surgery", ehistoryendsocopy.Bariatric_Surgery);
          cmd.Parameters.AddWithValue("@Bariatric_Remarks", ehistoryendsocopy.Bariatric_Remarks);
          cmd.Parameters.AddWithValue("@Fundoplication_Surgery", ehistoryendsocopy.Fundoplication_Surgery);
          cmd.Parameters.AddWithValue("@Fundoplication_Remarks", ehistoryendsocopy.Fundoplication_Remarks);
          cmd.Parameters.AddWithValue("@POEM_Surgery", ehistoryendsocopy.POEM_Surgery);
          cmd.Parameters.AddWithValue("@POEM_Remarks", ehistoryendsocopy.POEM_Remarks);
          cmd.Parameters.AddWithValue("@Gastrojejunostomy_Surgery", ehistoryendsocopy.Gastrojejunostomy_Surgery);
          cmd.Parameters.AddWithValue("@Gastrojejunostomy_Remarks", ehistoryendsocopy.Gastrojejunostomy_Remarks);
          cmd.Parameters.AddWithValue("@Other_Surgery", ehistoryendsocopy.Other_Surgery);
          cmd.Parameters.AddWithValue("@Other_Surgery_Specify", ehistoryendsocopy.Other_Surgery_Specify);
          cmd.Parameters.AddWithValue("@Other_Remarks", ehistoryendsocopy.Other_Remarks);
          cmd.Parameters.AddWithValue("@CreatedBy", ehistoryendsocopy.CreatedBy);

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
