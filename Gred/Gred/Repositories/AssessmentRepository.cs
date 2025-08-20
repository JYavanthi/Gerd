using Gred.Data.Entities.Common;
using Gred.Data.Entities;
using gred.Data;
using Microsoft.Data.SqlClient;
using System.Data;
using Gred.Services.Interface;
using Microsoft.EntityFrameworkCore;
namespace Gred.Repositories
{
  public class AssessmentRepository : IAssessment
  {
    private readonly GredDbContext _context;

    public AssessmentRepository(GredDbContext context)
    {
      this._context = context;
    }

    public async Task<CommonRsult> GetAssessment()
    {
      CommonRsult result = new CommonRsult();
      try
      {
        var data = await _context.Assessments.ToListAsync();
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

    public async Task<CommonRsult> GetAssessmentById(int id, int stage)
    {
      var result = new CommonRsult();

      var complaint = await _context.Assessments
                                    .Where(c => c.Pid == id && c.Stage==stage)
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


    public async Task<CommonRsult> SaveAssessment(EAssessment assessment)
    {
      CommonRsult result = new CommonRsult();
      try
      {
        DataTable dt = new DataTable();
        var con = (SqlConnection)_context.Database.GetDbConnection();
        using (var cmd = new SqlCommand("dbo.sp_Assessment", con))
        {

          cmd.CommandType = CommandType.StoredProcedure;
          cmd.Parameters.AddWithValue("@Flag", assessment.Flag);
          cmd.Parameters.AddWithValue("@AssessmentId", assessment.AssessmentId);
          cmd.Parameters.AddWithValue("@Stage", assessment.Stage);
          cmd.Parameters.AddWithValue("@PID", assessment.Pid);
          //cmd.Parameters.Add("@PID", SqlDbType.Int).Value = (object?)assessment.Pid ?? DBNull.Value;
          cmd.Parameters.AddWithValue("@Q1", assessment.Q1);
          cmd.Parameters.AddWithValue("@Q2", assessment.Q2);
          cmd.Parameters.AddWithValue("@Q3", assessment.Q3);
          cmd.Parameters.AddWithValue("@Q4", assessment.Q4);
          cmd.Parameters.AddWithValue("@Q5", assessment.Q5);
          cmd.Parameters.AddWithValue("@Q6", assessment.Q6);
          cmd.Parameters.AddWithValue("@Q7", assessment.Q7);
          cmd.Parameters.AddWithValue("@Q8", assessment.Q8);
          cmd.Parameters.AddWithValue("@Q9", assessment.Q9);
          cmd.Parameters.AddWithValue("@Q10", assessment.Q10);
          cmd.Parameters.AddWithValue("@Q11", assessment.Q11);
          cmd.Parameters.AddWithValue("@Q12", assessment.Q12);
          cmd.Parameters.AddWithValue("@AcidRefluxSymptom", assessment.AcidRefluxSymptom);
          cmd.Parameters.AddWithValue("@Dysmotity", assessment.Dysmotity);
          cmd.Parameters.AddWithValue("@TotalPoints", assessment.TotalPoints);
          cmd.Parameters.AddWithValue("@HeartburnNil", assessment.HeartburnNil);
          cmd.Parameters.AddWithValue("@HeartburnMinimal", assessment.HeartburnMinimal);
          cmd.Parameters.AddWithValue("@HeartburnModerate", assessment.HeartburnModerate);
          cmd.Parameters.AddWithValue("@HeartburnHeartburn", assessment.HeartburnHeartburn);
          cmd.Parameters.AddWithValue("@RegurgitationNil", assessment.RegurgitationNil);
          cmd.Parameters.AddWithValue("@RegurgitationMinimal", assessment.RegurgitationMinimal);
          cmd.Parameters.AddWithValue("@RegurgitationModerate", assessment.RegurgitationModerate);
          cmd.Parameters.AddWithValue("@RegurgitationHeartburn", assessment.RegurgitationHeartburn);
          cmd.Parameters.AddWithValue("@RetrosternalPainNil", assessment.RetrosternalPainNil);
          cmd.Parameters.AddWithValue("@RetrosternalPainMinimal", assessment.RetrosternalPainMinimal);
          cmd.Parameters.AddWithValue("@RetrosternalPainModerate", assessment.RetrosternalPainModerate);
          cmd.Parameters.AddWithValue("@RetrosternalPainHeartburn", assessment.RetrosternalPainHeartburn);
          cmd.Parameters.AddWithValue("@AcidTasteMouthNil", assessment.AcidTasteMouthNil);
          cmd.Parameters.AddWithValue("@AcidTasteMouthMinimal", assessment.AcidTasteMouthMinimal);
          cmd.Parameters.AddWithValue("@AcidTasteMouthModerate", assessment.AcidTasteMouthModerate);
          cmd.Parameters.AddWithValue("@AcidTasteMouthHeartburn", assessment.AcidTasteMouthHeartburn);
          cmd.Parameters.AddWithValue("@EE_LAXLesClassification", assessment.EeLaxlesClassification);
          cmd.Parameters.AddWithValue("@EE_AngelesGrade", assessment.EeAngelesGrade);
          cmd.Parameters.AddWithValue("@EE_AGRemarks", assessment.EeAgremarks);
          cmd.Parameters.AddWithValue("@EE_BarrettRemark", assessment.EeBarrettRemark);
          cmd.Parameters.AddWithValue("@EE_HillClassificationGrade", assessment.EeHillClassificationGrade);
          cmd.Parameters.AddWithValue("@EE_HillRemarks", assessment.EeHillRemarks);
          cmd.Parameters.AddWithValue("@PHimpedanceMonitoring", assessment.PHimpedanceMonitoring);
          cmd.Parameters.AddWithValue("@pHIM_Date", assessment.PHimDate);
          cmd.Parameters.AddWithValue("@pHIM_Attached", assessment.PHimAttached);
          cmd.Parameters.AddWithValue("@pHIM_Attachement", assessment.PHimAttachement);
          cmd.Parameters.AddWithValue("@pHIM_Remark", assessment.PHimRemark);
          cmd.Parameters.AddWithValue("@ManometryTest", assessment.ManometryTest);
          cmd.Parameters.AddWithValue("@MT_Date", assessment.MtDate);
          cmd.Parameters.AddWithValue("@MT_Attached", assessment.MtAttached);
          cmd.Parameters.AddWithValue("@MT_Attachement", assessment.MtAttachement);
          cmd.Parameters.AddWithValue("@MT_Remark", assessment.MtRemark);
          cmd.Parameters.AddWithValue("@Biopsy", assessment.Biopsy);
          cmd.Parameters.AddWithValue("@Biopsy_Date", assessment.BiopsyDate);
          cmd.Parameters.AddWithValue("@Biopsy_Attached", assessment.BiopsyAttached);
          cmd.Parameters.AddWithValue("@Biopsy_Attachement", assessment.BiopsyAttachement);
          cmd.Parameters.AddWithValue("@Biopsy_Remark", assessment.BiopsyRemark);
          cmd.Parameters.AddWithValue("@CreatedBy", assessment.CreatedBy);
          cmd.Parameters.AddWithValue("@TotalSymptomScore", assessment.TotalSymptomScore);
          cmd.Parameters.AddWithValue("@SymptomScore", assessment.SymptomScore);
          

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
