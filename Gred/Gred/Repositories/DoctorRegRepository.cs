using gred.Data;
using Gred.Data.Entities;
using Gred.Data.Entities.Common;
using Gred.Services.Interface;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System.Data;

namespace Gred.Repositories
{
    public class DoctorRegRepository : IDoctorReg
    {
        private readonly GredDbContext _context;

        public DoctorRegRepository(GredDbContext context)
        {
            this._context = context;
        }

        public async Task<CommonRsult> GetDoctorvalue()
        {
            CommonRsult result = new CommonRsult();
            try
            {
                var data = await _context.VwDoctors.ToListAsync();
                result.Data = data;
                result.Message = "Successfully";
            }
            catch(Exception ex)
            {
                result.Type = "E";
                result.Message = ex.Message;
            }
            return result;
        }

        public async Task<CommonRsult> SaveDoctorReg(EDoctorReg reg)
        {
            CommonRsult result = new CommonRsult();
            try
            {
                DataTable dt = new DataTable();
                var con = (SqlConnection)_context.Database.GetDbConnection();
                using (var cmd = new SqlCommand("dbo.sp_Doctor", con))
                {

                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@Flag", reg.Flag);
                    cmd.Parameters.AddWithValue("@DoctorID", reg.DoctorID);
                    cmd.Parameters.AddWithValue("@Name", reg.Name);
                    cmd.Parameters.AddWithValue("@Email", reg.Email);
                    cmd.Parameters.AddWithValue("@PhoneNO", reg.PhoneNO);
                    cmd.Parameters.AddWithValue("@MCICode", reg.MCICode);
                    cmd.Parameters.AddWithValue("@PlaceOfPractice", reg.PlaceOfPractice);
                    cmd.Parameters.AddWithValue("@HospitalName", reg.HospitalName);
                    cmd.Parameters.AddWithValue("@Password", reg.Password);
                    cmd.Parameters.AddWithValue("@State", reg.State);
                    cmd.Parameters.AddWithValue("@City", reg.City);
                    cmd.Parameters.AddWithValue("@EnterCodeNO", reg.EnterCodeNO);
                    cmd.Parameters.AddWithValue("@Status", reg.Status);
                    cmd.Parameters.AddWithValue("@CreatedBy", reg.CreatedBy);


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
