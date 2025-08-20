using gred.Data;
using Gred.Data.Entities.Common;
using Gred.Data.Entities;
using Gred.Services.Interface;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System.Data;

namespace Gred.Repositories
{
    public class MedicationRepository : IMedicationService
    {
        private readonly GredDbContext _context;

        public MedicationRepository(GredDbContext context)
        {
            _context = context;
        }

        public async Task<CommonRsult> GetMedicationByPatientId(int patientId)
        {
            var result = new CommonRsult();
            try
            {
                var data = await _context.Medications
                                         .Where(x => x.PatientId == patientId)
                                         .ToListAsync();

                result.Data = data;
                result.Type = "S";
                result.Message = "Medication data fetched successfully.";
                result.Count = data.Count;
            }
            catch (Exception ex)
            {
                result.Type = "E";
                result.Message = ex.Message;
            }
            return result;
        }

        public async Task<CommonRsult> SaveMedication(EMedication medication)
        {
            var result = new CommonRsult();
            try
            {
                var con = (SqlConnection)_context.Database.GetDbConnection();
                await using (var cmd = new SqlCommand("dbo.sp_Medication", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    if (con.State != ConnectionState.Open)
                        await con.OpenAsync();

                    cmd.Parameters.AddWithValue("@Flag", medication.Flag ?? "I");
                    cmd.Parameters.AddWithValue("@MedicationID", medication.MedicationId);
                    cmd.Parameters.AddWithValue("@GHID", medication.Ghid);
                    cmd.Parameters.AddWithValue("@MedicationName", medication.MedicationName ?? string.Empty);
                    cmd.Parameters.AddWithValue("@Dose", medication.Dose ?? string.Empty);
                    cmd.Parameters.AddWithValue("@Frequency", medication.Frequency ?? string.Empty);
                    cmd.Parameters.AddWithValue("@Molecule", medication.Molecule ?? string.Empty);
                    cmd.Parameters.AddWithValue("@CreatedBy", medication.CreatedBy);
                    cmd.Parameters.AddWithValue("@PatientID", medication.PatientId);
                    cmd.Parameters.AddWithValue("@Stage", medication.Stage);

                    await cmd.ExecuteNonQueryAsync();

                    result.Type = "S";
                    result.Message = "Medication saved successfully.";
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
