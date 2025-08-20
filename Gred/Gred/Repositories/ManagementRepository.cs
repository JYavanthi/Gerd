using gred.Data;
using gred.Models;
using Gred.Data.Entities;
using Gred.Data.Entities.Common;
using Gred.Data.Entities.Gred.Data.Entities;
using Gred.Services.Interface;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System.Data;
using System.Linq.Expressions;


namespace Gred.Repositories
{
  public class ManagementRepository : IManagement
  {
    private readonly GredDbContext _context;

    public ManagementRepository(GredDbContext context)
    {
      this._context = context;
    }

    public async Task<CommonRsult> GetManagement()
    {
      CommonRsult result = new CommonRsult();
      try
      {
        var data = await _context.Managements.ToListAsync();
        result.Data = data;
        result.Message = "Successfully";
      }
      catch (Exception ex)
      {
        result.Type = "E";
        result.Message = ex.Message;
      }
      return result;
    }

    public async Task<CommonRsult> GetManagementById(int patientId ,int stage)
    {
      var result = new CommonRsult();

      var complaint = await _context.Managements
                                    .Where(c => c.PatientId == patientId && c.Stage==stage)
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

    public async Task<CommonRsult> SaveManagement(EManagement management)
    {
      CommonRsult result = new CommonRsult();
      try
      {
        DataTable dt = new DataTable();
        var con = (SqlConnection)_context.Database.GetDbConnection();
        using (var cmd = new SqlCommand("dbo.sp_Management", con))
        {

          cmd.CommandType = CommandType.StoredProcedure;
          cmd.Parameters.AddWithValue("@Flag", management.Flag);
          cmd.Parameters.AddWithValue("@ManagementID", management.ManagementID);
          cmd.Parameters.AddWithValue("@Stage", management.Stage);
          cmd.Parameters.AddWithValue("PatientID", management.PatientID);
          cmd.Parameters.AddWithValue("@LifestyleRecommendations", management.LifestyleRecommendations);
          cmd.Parameters.AddWithValue("@PPI_Medication_Name", management.PPI_Medication_Name);
          cmd.Parameters.AddWithValue("@PPI_Dose", management.PPI_Dose);
          cmd.Parameters.AddWithValue("@PPI_Frequency", management.PPI_Frequency);
          cmd.Parameters.AddWithValue("@Prokinetics_Medication_Name", management.Prokinetics_Medication_Name);
          cmd.Parameters.AddWithValue("@Prokinetics_Dose", management.Prokinetics_Dose);
          cmd.Parameters.AddWithValue("@Prokinetics_Frequency", management.Prokinetics_Frequency);
          cmd.Parameters.AddWithValue("@Sucralfate_Medication_Name", management.Sucralfate_Medication_Name);
          cmd.Parameters.AddWithValue("@Sucralfate_Frequency", management.Sucralfate_Frequency);
          cmd.Parameters.AddWithValue("@Sucralfate_Dose", management.Sucralfate_Dose);
          cmd.Parameters.AddWithValue("@Alginate_Medication_Name", management.Alginate_Medication_Name);
          cmd.Parameters.AddWithValue("@Alginate_Frequency", management.Alginate_Frequency);
          cmd.Parameters.AddWithValue("@Alginate_Dose", management.Alginate_Dose);
          cmd.Parameters.AddWithValue("@H2Blockers_Medication_Name", management.H2Blockers_Medication_Name);
          cmd.Parameters.AddWithValue("@H2Blockers_Frequency", management.H2Blockers_Frequency);
          cmd.Parameters.AddWithValue("@H2Blockers_Dose", management.H2Blockers_Dose);
          cmd.Parameters.AddWithValue("@H2BlockersC_Medication_Name", management.H2BlockersC_Medication_Name);
          cmd.Parameters.AddWithValue("@H2BlockersC_Frequency", management.H2BlockersC_Frequency);
          cmd.Parameters.AddWithValue("@H2BlockersC_Dose", management.H2BlockersC_Dose);
          cmd.Parameters.AddWithValue("@PCAB_Medication_Name", management.PCAB_Medication_Name);
          cmd.Parameters.AddWithValue("@PCAB_Frequency", management.PCAB_Frequency);
          cmd.Parameters.AddWithValue("@PCAB_Dose", management.PCAB_Dose);
          cmd.Parameters.AddWithValue("@others_Medication_Name", management.others_Medication_Name);
          cmd.Parameters.AddWithValue("@others_Frequency", management.others_Frequency);
          cmd.Parameters.AddWithValue("@others_Dose", management.others_Dose);
          cmd.Parameters.AddWithValue("@CreatedBy", management.CreatedBy);


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

    // public async Task<CommonRsult> SubmitStage(EPatitentSubmit ptn)


    public async Task<CommonRsult> SubmitStage(EStageUpdate stageUodateObj)
    {
      CommonRsult result = new CommonRsult();
      try
      {
        DataTable dt = new DataTable();
        var con = (SqlConnection)_context.Database.GetDbConnection();
        using (var cmd = new SqlCommand("dbo.sp_PatientDtlsSubmit", con))
        {
          cmd.CommandType = CommandType.StoredProcedure;

          cmd.Parameters.AddWithValue("@Stage", stageUodateObj.stage);
          cmd.Parameters.AddWithValue("@PatientID", stageUodateObj.patientId);
          cmd.Parameters.AddWithValue("@CreatedBy", stageUodateObj.createdby);

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


