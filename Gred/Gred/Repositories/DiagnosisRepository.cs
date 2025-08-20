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
  public class DiagnosisRepository : IDiagnosis

  {

    private readonly GredDbContext _context;

    public DiagnosisRepository(GredDbContext context)
    {
      this._context = context;
    }
    public async Task<CommonRsult> GetDiagnosis()
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

    public async Task<CommonRsult> GetDiagnosisById(int patientId)
    {
      var result = new CommonRsult();

      var complaint = await _context.VwDiagnoses
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

    public async Task<CommonRsult> SaveDiagnosis(EDiagnosis eDiagnosis)
    {
      CommonRsult result = new CommonRsult();
      try
      {
        DataTable dt = new DataTable();
        var con = (SqlConnection)_context.Database.GetDbConnection();
        using (var cmd = new SqlCommand("dbo.sp_Diagnosis", con))
        {

          cmd.CommandType = CommandType.StoredProcedure;
          cmd.Parameters.AddWithValue("@Flag", eDiagnosis.Flag);
          cmd.Parameters.AddWithValue("@DiagnosisID", eDiagnosis.DiagnosisID);
          cmd.Parameters.AddWithValue("@PatientID", eDiagnosis.PatientID);

          cmd.Parameters.AddWithValue("@DoctorID", eDiagnosis.DoctorID);
          cmd.Parameters.AddWithValue("@NewlyDiagnosed", eDiagnosis.NewlyDiagnosed);
          cmd.Parameters.AddWithValue("@KnownCaseOfGERD", eDiagnosis.KnownCaseOfGERD);
          cmd.Parameters.AddWithValue("@GRED_NoOfYear", eDiagnosis.GRED_NoOfYear);
          cmd.Parameters.AddWithValue("@GERDType", eDiagnosis.GERDType);
          cmd.Parameters.AddWithValue("@RefractoryToPPI", eDiagnosis.RefractoryToPPI);
          cmd.Parameters.AddWithValue("@AdherenceToTherapy", eDiagnosis.AdherenceToTherapy);
          cmd.Parameters.AddWithValue("@Stage", eDiagnosis.Stage);          
          cmd.Parameters.AddWithValue("@CreatedBy", eDiagnosis.CreatedBy);


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
