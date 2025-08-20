using gred.Data;
using Gred.Data.Entities;
using Gred.Data.Entities.Common;
using Gred.Services.Interface;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System.Data;

namespace Gred.Repositories
{
    public class ComorbiditiesRepository : IComorbidities
    {
        private readonly GredDbContext _context;

        public ComorbiditiesRepository(GredDbContext context)
        {
            this._context = context;
        }

        public async Task<CommonRsult> GetComorbidities()
        {
            CommonRsult result = new CommonRsult();
            try
            {
                var data = await _context.VwComorbidities.ToListAsync();
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

    public async Task<CommonRsult> GetComorbditiesById(int patientId ,int stage)
    {
      var result = new CommonRsult();

      var complaint = await _context.Comorbidities
                                    .Where(c => c.PatientId == patientId && c.Stage == stage)
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

    public async Task<CommonRsult> SaveComorbidities(EComorbidities eComorbidities)
        {
            CommonRsult result = new CommonRsult();
            try
            {
                DataTable dt = new DataTable();
                var con = (SqlConnection)_context.Database.GetDbConnection();
                using (var cmd = new SqlCommand("dbo.sp_Comorbidities", con))
                {

                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@Flag", eComorbidities.Flag);
                    cmd.Parameters.AddWithValue("@ComorbiditiesID", eComorbidities.ComorbiditiesID);
                    cmd.Parameters.AddWithValue("@Stage", eComorbidities.Stage);
                    cmd.Parameters.AddWithValue("@PatientID", eComorbidities.PatientID);
                    cmd.Parameters.AddWithValue("@DoctorID", eComorbidities.DoctorID);
                    cmd.Parameters.AddWithValue("@HT_Present", eComorbidities.HT_Present);
                    cmd.Parameters.AddWithValue("@HT_Remark", eComorbidities.HT_Remark);
                    cmd.Parameters.AddWithValue("@DB_Present", eComorbidities.DB_Present);
                    cmd.Parameters.AddWithValue("@DB_Remark", eComorbidities.DB_Remark);
                    cmd.Parameters.AddWithValue("@DD_Present", eComorbidities.DD_Present);
                    cmd.Parameters.AddWithValue("@DD_Remark", eComorbidities.DD_Remark);
                    cmd.Parameters.AddWithValue("@CLD_Present", eComorbidities.CLD_Present);
                    cmd.Parameters.AddWithValue("@CLD_Remark", eComorbidities.CLD_Remark);
                    cmd.Parameters.AddWithValue("@ND_Present", eComorbidities.ND_Present);
                    cmd.Parameters.AddWithValue("@ND_Remark", eComorbidities.ND_Remark);
                    cmd.Parameters.AddWithValue("@CD_Present", eComorbidities.CD_Present);
                    cmd.Parameters.AddWithValue("@CD_Remark", eComorbidities.CD_Remark);
                    cmd.Parameters.AddWithValue("@H_Present", eComorbidities.H_Present);
                    cmd.Parameters.AddWithValue("@H_Remark", eComorbidities.H_Remark);
                    cmd.Parameters.AddWithValue("@HTD_Present", eComorbidities.HTD_Present);
                    cmd.Parameters.AddWithValue("@HTD_Remark", eComorbidities.HTD_Remark);
                    cmd.Parameters.AddWithValue("@BD_Present", eComorbidities.BD_Present);
                    cmd.Parameters.AddWithValue("@BD_Remark", eComorbidities.BD_Remark);
                    cmd.Parameters.AddWithValue("@CKD_Present", eComorbidities.CKD_Present);
                    cmd.Parameters.AddWithValue("@CKD_Remark", eComorbidities.CKD_Remark);
                    cmd.Parameters.AddWithValue("@A_Present", eComorbidities.A_Present);
                    cmd.Parameters.AddWithValue("@A_Remark", eComorbidities.A_Remark);
                    cmd.Parameters.AddWithValue("@O_Present", eComorbidities.O_Present);
                    cmd.Parameters.AddWithValue("@O_Remark", eComorbidities.O_Remark);
                    cmd.Parameters.AddWithValue("@RA_Present", eComorbidities.RA_Present);
                    cmd.Parameters.AddWithValue("@RA_Remark", eComorbidities.RA_Remark);
                    cmd.Parameters.AddWithValue("@SS_Present", eComorbidities.SS_Present);
                    cmd.Parameters.AddWithValue("@SS_Remark", eComorbidities.SS_Remark);
                    cmd.Parameters.AddWithValue("@C_Present", eComorbidities.C_Present);
                    cmd.Parameters.AddWithValue("@C_Remark", eComorbidities.C_Remark);
                    cmd.Parameters.AddWithValue("@CMO_Present", eComorbidities.CMO_Present);
                    cmd.Parameters.AddWithValue("@CMO_Remark", eComorbidities.CMO_Remark);
                    cmd.Parameters.AddWithValue("@CreatedBy", eComorbidities.CreatedBy);


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
