using gred.Data;
using Gred.Data.Entities;
using Gred.Data.Entities.Common;
using Gred.Services.Interface;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System.Data;

namespace Gred.Repositories
{
    public class FamilyHistoryRepository : IFamilyHistory
    {
        private readonly GredDbContext _context;

        public FamilyHistoryRepository(GredDbContext context)
        {
            this._context = context;
        }

    public async Task<CommonRsult> GetFamilyHistoryById(int id,int stage)
    {
      var result = new CommonRsult();

      var complaint = await _context.VwFamilyHistories
                                    .Where(c => c.PatientId == id && c.Stage==stage)
                                    .OrderByDescending(c => c.Stage )
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

    public async Task<CommonRsult> AddFamilyHistory(EFamilyHistory familyHistory)
        {
            CommonRsult result = new CommonRsult();
            try
            {
                DataTable dt = new DataTable();
                var con = (SqlConnection)_context.Database.GetDbConnection();
                using (var cmd = new SqlCommand("dbo.sp_FamilyHistory", con))
                {

                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@Flag", familyHistory.Flag);
                    cmd.Parameters.AddWithValue("@FamilyHistoryID", familyHistory.FamilyHistoryID);
                    cmd.Parameters.AddWithValue("@Stage", familyHistory.Stage);
                    cmd.Parameters.AddWithValue("@DoctorID", familyHistory.DoctorID);
                    cmd.Parameters.AddWithValue("@PatientID", familyHistory.PatientID);
                    cmd.Parameters.AddWithValue("@FH_GRED", familyHistory.FH_GRED);
                    cmd.Parameters.AddWithValue("@FH_Remark", familyHistory.FH_Remark);
                    cmd.Parameters.AddWithValue("@FH_EGC", familyHistory.FH_EGC);
                    cmd.Parameters.AddWithValue("@FH_EGCRemark", familyHistory.FH_EGCRemark);
                    cmd.Parameters.AddWithValue("@gH_PPI", familyHistory.gH_PPI);
          //cmd.Parameters.AddWithValue("@GH_PPI", familyHistory.FH_GRED);
          //cmd.Parameters.AddWithValue("@Medication_Name", familyHistory.FH_Remark);
          //cmd.Parameters.AddWithValue("@Dose", familyHistory.FH_EGC);
          //cmd.Parameters.AddWithValue("@Frequency", familyHistory.FH_EGCRemark);
          cmd.Parameters.AddWithValue("@CreatedBy", familyHistory.CreatedBy);


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

        public async Task<CommonRsult> GetFamilyHistory()
        {
            CommonRsult result = new CommonRsult();
            try
            {
                var data = await _context.VwFamilyHistories.ToListAsync();
                result.Data = data;
                result.Type = "S";
                result.Message = "Successfully";
                result.Count = data.Count();
            }
            catch(Exception ex)
            {
                result.Type = "E";
                result.Message = ex.Message;
            }
            return result;
        }
    }
}
