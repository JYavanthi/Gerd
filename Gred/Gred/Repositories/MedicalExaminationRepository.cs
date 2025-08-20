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
  public class MedicalExaminationRepository : IMedicalExamination
  {
    private readonly GredDbContext _context;

    public MedicalExaminationRepository(GredDbContext context)
    {
      this._context = context;
    }

    public async Task<CommonRsult> GetMedicalExamination()
    {
      CommonRsult result = new CommonRsult();
      try
      {
        var data = await _context.VwMedicalExaminations.ToListAsync();
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

    public async Task<CommonRsult> GetMedicalExaminationById(int patientId)
    {
      var result = new CommonRsult();

      var complaint = await _context.VwMedicalExaminations
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

    public async Task<CommonRsult> SaveMedicalExamination(EMedicalExamination medicalexamination)
    {
      CommonRsult result = new CommonRsult();
      try
      {
        DataTable dt = new DataTable();
        var con = (SqlConnection)_context.Database.GetDbConnection();
        using (var cmd = new SqlCommand("dbo.sp_MedicalExamination", con))
        {
          cmd.CommandType = CommandType.StoredProcedure;

          cmd.Parameters.AddWithValue("@Flag", medicalexamination.Flag);
          cmd.Parameters.AddWithValue("@Stage", medicalexamination.Stage);
          cmd.Parameters.AddWithValue("@MEID", medicalexamination.MEID);
          cmd.Parameters.AddWithValue("@DoctorID", medicalexamination.DoctorID);
          cmd.Parameters.AddWithValue("@PatientID", medicalexamination.PatientID);

          cmd.Parameters.AddWithValue("@PE_Height", medicalexamination.PE_Height);
          cmd.Parameters.AddWithValue("@PE_Weight", medicalexamination.PE_Weight);
          cmd.Parameters.AddWithValue("@PE_BMI", medicalexamination.PE_BMI);

          cmd.Parameters.AddWithValue("@SE_GANormal", medicalexamination.SE_GANormal);
          cmd.Parameters.AddWithValue("@SE_GAAbNormalCS", medicalexamination.SE_GAAbNormalCS);
          cmd.Parameters.AddWithValue("@SE_GAAbNormalNCS", medicalexamination.SE_GAAbNormalNCS);
          cmd.Parameters.AddWithValue("@SE_GAAbNormalRemark", medicalexamination.PE_BMSE_GAAbNormalRemarkI5);

          cmd.Parameters.AddWithValue("@PAE_Findings", medicalexamination.PAE_Findings);

          cmd.Parameters.AddWithValue("@SE_RSNormal", medicalexamination.SE_RSNormal);
          cmd.Parameters.AddWithValue("@SE_RSAbNormal_CS", medicalexamination.SE_RSAbNormal_CS);
          cmd.Parameters.AddWithValue("@SE_RSAbNormal_NCS", medicalexamination.SE_RSAbNormal_NCS);
          cmd.Parameters.AddWithValue("@SE_RSAbNormalRemark", medicalexamination.SE_RSAbNormalRemark);

          cmd.Parameters.AddWithValue("@OthersNormal", medicalexamination.OthersNormal);
          cmd.Parameters.AddWithValue("@OthersAbNormal_CS", medicalexamination.OthersAbNormal_CS);
          cmd.Parameters.AddWithValue("@OthersAbNormal_NCS", medicalexamination.OthersAbNormal_NCS);
          cmd.Parameters.AddWithValue("@OthersAbNormalRemark", medicalexamination.OthersAbNormalRemark);

          cmd.Parameters.AddWithValue("@CreatedBy", medicalexamination.CreatedBy);

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

