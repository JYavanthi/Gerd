using gred.Data;
using gred.Models;
using Gred.Data.Entities;
using Gred.Data.Entities.Common;
using Gred.Services.Interface;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System.Data;

namespace Gred.Repositories
{
    public class GerdHistoryRepository : IGerdHistory
    {
        private readonly GredDbContext _context;

        public GerdHistoryRepository(GredDbContext context)
        {
            this._context = context;
        }

        public async Task<CommonRsult> AddGerdHistory(EGerdHistory gerdHistory)
        {
            CommonRsult result = new CommonRsult();
            try
            {
                DataTable dt = new DataTable();
                var con = (SqlConnection)_context.Database.GetDbConnection();
                using (var cmd = new SqlCommand("dbo.sp_GERDHistory", con))
                {

          //cmd.CommandType = CommandType.StoredProcedure;
          //cmd.Parameters.AddWithValue("@Flag", gerdHistory.Flag);
          //cmd.Parameters.AddWithValue("@GHID", gerdHistory.GHID);
          //cmd.Parameters.AddWithValue("@DoctorID", gerdHistory.DoctorID);
          //cmd.Parameters.AddWithValue("@PatientID", gerdHistory.PatientID);
          //cmd.Parameters.AddWithValue("@UsageOfPPI", gerdHistory.UsageOfPPI);
          //cmd.Parameters.AddWithValue("@HistoryofEndoscopy", gerdHistory.HistoryofEndoscopy);
          //cmd.Parameters.AddWithValue("@EndoscopyDate", gerdHistory.EndoscopyDate ?? (object)DBNull.Value);
          //cmd.Parameters.AddWithValue("@EndoscopyAttached", gerdHistory.EndoscopyAttached ?? (object)DBNull.Value);
          //cmd.Parameters.AddWithValue("@EndoscopyAttement", gerdHistory.EndoscopyAttement);
          //cmd.Parameters.AddWithValue("@EndoscopyRemark", gerdHistory.EndoscopyRemark);
          //cmd.Parameters.AddWithValue("@HistoryofGS", gerdHistory.HistoryofGS);
          //cmd.Parameters.AddWithValue("@GS_BariatricSurgery", gerdHistory.GS_BariatricSurgery ?? (object)DBNull.Value);
          //cmd.Parameters.AddWithValue("@GS_BSRemark", gerdHistory.GS_BSRemark);
          //cmd.Parameters.AddWithValue("@GS_FundoplicationSurgery", gerdHistory.GS_FundoplicationSurgery);
          //cmd.Parameters.AddWithValue("@GS_FSRemark", gerdHistory.GS_FSRemark);
          //cmd.Parameters.AddWithValue("@GS_GastricPOEMSurgery", gerdHistory.GS_GastricPOEMSurgery);
          //cmd.Parameters.AddWithValue("@GS_GPSRemark", gerdHistory.GS_GPSRemark);
          //cmd.Parameters.AddWithValue("@GS_Gastrojejunostomy", gerdHistory.GS_Gastrojejunostomy);
          //cmd.Parameters.AddWithValue("@GS_GJRemark", gerdHistory.GS_GJRemark);
          //cmd.Parameters.AddWithValue("@GS_Other", gerdHistory.GS_Other);
          //cmd.Parameters.AddWithValue("@GS_OtherRemark", gerdHistory.GS_OtherRemark);
          //cmd.Parameters.AddWithValue("@CreatedBy", gerdHistory.CreatedBy);


          cmd.CommandType = CommandType.StoredProcedure;
          cmd.Parameters.AddWithValue("@Flag", gerdHistory.Flag ?? (object)DBNull.Value);
          cmd.Parameters.AddWithValue("@GHID", gerdHistory.GHID);
          cmd.Parameters.AddWithValue("@DoctorID", gerdHistory.DoctorID);
          cmd.Parameters.AddWithValue("@PatientID", gerdHistory.PatientID);
          cmd.Parameters.AddWithValue("@Stage", gerdHistory.Stage);

          cmd.Parameters.AddWithValue("@UsageOfPPI", gerdHistory.UsageOfPPI ?? (object)DBNull.Value);
          cmd.Parameters.AddWithValue("@HistoryofEndoscopy", gerdHistory.HistoryofEndoscopy ?? (object)DBNull.Value);
          cmd.Parameters.AddWithValue("@EndoscopyDate", gerdHistory.EndoscopyDate ?? (object)DBNull.Value);
          cmd.Parameters.AddWithValue("@EndoscopyAttached", gerdHistory.EndoscopyAttached ?? (object)DBNull.Value);
          cmd.Parameters.AddWithValue("@EndoscopyAttement", gerdHistory.EndoscopyAttement ?? (object)DBNull.Value);
          cmd.Parameters.AddWithValue("@EndoscopyRemark", gerdHistory.EndoscopyRemark ?? (object)DBNull.Value);
          cmd.Parameters.AddWithValue("@HistoryofGS", gerdHistory.HistoryofGS ?? (object)DBNull.Value);
          cmd.Parameters.AddWithValue("@GS_BariatricSurgery", gerdHistory.GS_BariatricSurgery ?? (object)DBNull.Value);
          cmd.Parameters.AddWithValue("@GS_BSRemark", gerdHistory.GS_BSRemark ?? (object)DBNull.Value);
          cmd.Parameters.AddWithValue("@GS_FundoplicationSurgery", gerdHistory.GS_FundoplicationSurgery ?? (object)DBNull.Value);
          cmd.Parameters.AddWithValue("@GS_FSRemark", gerdHistory.GS_FSRemark ?? (object)DBNull.Value);
          cmd.Parameters.AddWithValue("@GS_GastricPOEMSurgery", gerdHistory.GS_GastricPOEMSurgery ?? (object)DBNull.Value);
          cmd.Parameters.AddWithValue("@GS_GPSRemark", gerdHistory.GS_GPSRemark ?? (object)DBNull.Value);
          cmd.Parameters.AddWithValue("@GS_Gastrojejunostomy", gerdHistory.GS_Gastrojejunostomy ?? (object)DBNull.Value);
          cmd.Parameters.AddWithValue("@GS_GJRemark", gerdHistory.GS_GJRemark ?? (object)DBNull.Value);
          cmd.Parameters.AddWithValue("@gs_OtherText", gerdHistory.gs_OtherText ?? (object)DBNull.Value);
          cmd.Parameters.AddWithValue("@GS_Other", gerdHistory.GS_Other ?? (object)DBNull.Value);
          cmd.Parameters.AddWithValue("@GS_OtherRemark", gerdHistory.GS_OtherRemark ?? (object)DBNull.Value);
          cmd.Parameters.AddWithValue("@CreatedBy", gerdHistory.CreatedBy);



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

        public async Task<CommonRsult> GetGerdHistory()
        {
            CommonRsult result = new CommonRsult();
            try
            {
                var data = await _context.VwGerdhistories.ToListAsync();
                result.Data = data;
                result.Type = "S";
                result.Message = "SuccussFully";
                result.Count = data.Count();
            }
            catch(Exception ex)
            {
                result.Type = "E";
                result.Message = ex.Message;
            }
            return result;
        }

    public async Task<CommonRsult> GetGerdHistoryById(int patientId)
    {
      var result = new CommonRsult();

      var complaint = await _context.VwGerdhistories
                                    .Where(c => c.PatientId == patientId)
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
  }
}
