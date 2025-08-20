using gred.Data;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore; // ✅ THIS IS REQUIRED
using gred.Models;

public class PtnTrackRepository : IPtnTrackRepository
{
  private readonly GredDbContext _context;

  public PtnTrackRepository(GredDbContext context)
  {
    _context = context;
  }

  public async Task<string?> GetPageRouterByPatientIdAsync(int patientId)
  {
    var record = await _context.PtnTracks
        .FirstOrDefaultAsync(p => p.PatientId == patientId);

    return record?.PageRouter;
  }
}
