using System.Data;
using gred.Data;
using Gred.Data.Entities;
using Gred.Data.Entities.Common;
using Gred.Services.Interface;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

namespace Gred.Repositories
{
  public class CurrentMedicatonRepositary : ICurrentMedication
  {
    private readonly GredDbContext _context;

    public CurrentMedicatonRepositary(GredDbContext context)
    {
      this._context = context;
    }
    public async Task<CommonRsult> GetCurrentMedication()
    {
      CommonRsult result = new CommonRsult();
      try
      {
        var data = await _context.CurrentMedications.ToListAsync();
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

    public async Task<CommonRsult> GetCurrentMedicationById(int patientId)
    {
      var result = new CommonRsult();

      var complaint = await _context.VwCurrentMedications
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

    public async Task<CommonRsult> SaveCurrentMedication(ECurrentMedication eCurrentMedication)
    {
      CommonRsult result = new CommonRsult();
      try
      {
        DataTable dt = new DataTable();
        var con = (SqlConnection)_context.Database.GetDbConnection();
        using (var cmd = new SqlCommand("dbo.sp_ManageCurrentMedication", con))
        {
          cmd.CommandType = CommandType.StoredProcedure;
          cmd.Parameters.AddWithValue("@Flag", eCurrentMedication.Flag);
          cmd.Parameters.AddWithValue("@Id", eCurrentMedication.Id);
          cmd.Parameters.AddWithValue("@PatientId", eCurrentMedication.PatientId);
          cmd.Parameters.AddWithValue("@Stage", eCurrentMedication.Stage);

          cmd.Parameters.AddWithValue("@NSAIDs_Molecule", eCurrentMedication.NSAIDs_Molecule);
          cmd.Parameters.AddWithValue("@NSAIDs_Dose", eCurrentMedication.NSAIDs_Dose);
          cmd.Parameters.AddWithValue("@NSAIDs_Frequency", eCurrentMedication.NSAIDs_Frequency);
          cmd.Parameters.AddWithValue("@Bisphosphonates_Molecule", eCurrentMedication.Bisphosphonates_Molecule);
          cmd.Parameters.AddWithValue("@Bisphosphonates_Dose", eCurrentMedication.Bisphosphonates_Dose);
          cmd.Parameters.AddWithValue("@Bisphosphonates_Frequency", eCurrentMedication.Bisphosphonates_Frequency);
          cmd.Parameters.AddWithValue("@Steroids_Molecule", eCurrentMedication.Steroids_Molecule);
          cmd.Parameters.AddWithValue("@Steroids_Dose", eCurrentMedication.Steroids_Dose);
          cmd.Parameters.AddWithValue("@Steroids_Frequency", eCurrentMedication.Steroids_Frequency);
          cmd.Parameters.AddWithValue("@Antiplatelet_Molecule", eCurrentMedication.Antiplatelet_Molecule);
          cmd.Parameters.AddWithValue("@Antiplatelet_Dose", eCurrentMedication.Antiplatelet_Dose);
          cmd.Parameters.AddWithValue("@Antiplatelet_Frequency", eCurrentMedication.Antiplatelet_Frequency);
          cmd.Parameters.AddWithValue("@Others_Molecule", eCurrentMedication.Others_Molecule);
          cmd.Parameters.AddWithValue("@Others_Dose", eCurrentMedication.Others_Dose);
          cmd.Parameters.AddWithValue("@Others_Frequency", eCurrentMedication.Others_Frequency);
          cmd.Parameters.AddWithValue("@CreatedBy", eCurrentMedication.CreatedBy);


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
