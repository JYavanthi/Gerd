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
    public class PatientRegRepository : IPatientReg
    {
        private readonly GredDbContext _context;

        public PatientRegRepository(GredDbContext context)
        {
            this._context = context;
        }

        public async Task<CommonRsult> GetPatientDetails()
        {
            CommonRsult result = new CommonRsult();
            try
            {
                var data = await _context.VwPatients.ToListAsync();
                result.Data = data;
                result.Type = "S";
                result.Message = "Succefully Added";
                result.Count = data.Count();
            }
            catch(Exception ex)
            {
                result.Type = "E";
                result.Message = ex.Message;
            }
            return result;
        }
    public async Task<CommonRsult> GetPatientById(int id)
    {
      CommonRsult result = new CommonRsult();
      try
      {
        var patient = await _context.VwPatients
            .FirstOrDefaultAsync(p => p.PatientId == id);

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

    public async Task<CommonRsult> SavePatientReg(EPatientReg ePatient)
        {
            CommonRsult result = new CommonRsult();
            try
            {
                DataTable dt = new DataTable();
                var con = (SqlConnection)_context.Database.GetDbConnection();
                using (var cmd = new SqlCommand("dbo.sp_Patient", con))
                {

                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@Flag", ePatient.Flag);
                    cmd.Parameters.AddWithValue("@PatientID", ePatient.PatientID);
                    cmd.Parameters.AddWithValue("@DoctorID", ePatient.doctorID);
                     cmd.Parameters.AddWithValue("@Stage", ePatient.Stage);

          cmd.Parameters.AddWithValue("@Initial", ePatient.Initial);
                    cmd.Parameters.AddWithValue("@SubjectNo", ePatient.SubjectNo);
                    cmd.Parameters.AddWithValue("@Date", ePatient.Date);
                    cmd.Parameters.AddWithValue("@Age", ePatient.Age);
                    //cmd.Parameters.AddWithValue("@DOB", ePatient.DOB);
                    cmd.Parameters.AddWithValue("@Gender", ePatient.Gender);
                    cmd.Parameters.AddWithValue("@Education", ePatient.Education);
                    cmd.Parameters.AddWithValue("@Occupation", ePatient.Occupation);
                    cmd.Parameters.AddWithValue("@State", ePatient.State);
                    cmd.Parameters.AddWithValue("@City", ePatient.City);
                    cmd.Parameters.AddWithValue("@Pincode", ePatient.Pincode);
                    cmd.Parameters.AddWithValue("@PlaceType", ePatient.PlaceType);
                    cmd.Parameters.AddWithValue("@SocioeconomicStatus", ePatient.SocioeconomicStatus);
                    cmd.Parameters.AddWithValue("@FamilyIncome", ePatient.FamilyIncome);
                    cmd.Parameters.AddWithValue("@PastHistory", ePatient.PastHistory);
                    cmd.Parameters.AddWithValue("@Diet", ePatient.Diet);
                    cmd.Parameters.AddWithValue("@CreatedBy", ePatient.CreatedBy);


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
