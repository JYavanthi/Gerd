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
    public class CheifComplaintRepository : ICheifComplaint
    {
        private readonly GredDbContext _context;

        public CheifComplaintRepository(GredDbContext context)
        {
            this._context = context;
        }

        public async Task<CommonRsult> GetCheifComplaint()
        {
            CommonRsult result = new CommonRsult();
            try
            {
                var data = await _context.VwCheifComplaints.ToListAsync();
                result.Data = data;
                result.Type = "S";
                result.Message = "Succefully Added";
                result.Count = data.Count();
            }
            catch (Exception ex)
            {
                result.Type = "E";
                result.Message = ex.Message;
            }
            return result;
        }

    public async Task<CommonRsult> GetCheifComplaintById(int patientId, int stage)
    {
      var result = new CommonRsult();

      var complaint = await _context.VwCheifComplaints
                                    .Where(c => c.PatientId == patientId && c.Stage==stage)
                                    .OrderByDescending(c => c.Stage) // ✅ get highest stage
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
    public async Task<CommonRsult> SaveCheifComplaint(ECheifComplaint eCheif)
        {
            CommonRsult result = new CommonRsult();
            try
            {
                DataTable dt = new DataTable();
                var con = (SqlConnection)_context.Database.GetDbConnection();
                using (var cmd = new SqlCommand("dbo.sp_ChiefComplaint", con))
                {

                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@Flag", eCheif.Flag);
                    cmd.Parameters.AddWithValue("@CheifCompliantID", eCheif.CheifCompliantID);
                    cmd.Parameters.AddWithValue("@Stage", eCheif.Stage);

                    cmd.Parameters.AddWithValue("@PatientID", eCheif.PatientID);
                    cmd.Parameters.AddWithValue("@DoctorID", eCheif.DoctorID);
                    cmd.Parameters.AddWithValue("@HB_Duration", eCheif.HB_Duration);
                    cmd.Parameters.AddWithValue("@HB_Frequency", eCheif.HB_Frequency);
                    cmd.Parameters.AddWithValue("@HB_Postural", eCheif.HB_Postural);
                    cmd.Parameters.AddWithValue("@HB_Nocturnal", eCheif.HB_Nocturnal);
                    cmd.Parameters.AddWithValue("@R_Duration", eCheif.R_Duration);
                    cmd.Parameters.AddWithValue("@R_Frequency", eCheif.R_Frequency);
                    cmd.Parameters.AddWithValue("@R_Postural", eCheif.R_Postural);
                    cmd.Parameters.AddWithValue("@R_Nocturnal", eCheif.R_Nocturnal);
                    cmd.Parameters.AddWithValue("@RP_Duration", eCheif.RP_Duration);
                    cmd.Parameters.AddWithValue("@RP_Frequency", eCheif.RP_Frequency);
                    cmd.Parameters.AddWithValue("@RP_Postural", eCheif.RP_Postural);
                    cmd.Parameters.AddWithValue("@RP_Nocturnal", eCheif.RP_Nocturnal);
                    cmd.Parameters.AddWithValue("@AT_Duration", eCheif.AT_Duration);
                    cmd.Parameters.AddWithValue("@AT_Frequency", eCheif.AT_Frequency);
                    cmd.Parameters.AddWithValue("@AT_Postural", eCheif.AT_Postural);
                    cmd.Parameters.AddWithValue("@AT_Nocturnal", eCheif.AT_Nocturnal);
                    cmd.Parameters.AddWithValue("@CreatedBy", eCheif.CreatedBy);


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
