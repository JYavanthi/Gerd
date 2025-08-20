using System.Collections.Generic;
using System.Threading.Tasks;
using gred.Data;
using gred.Models;
using Gred.Repositories.Interface;
using Microsoft.EntityFrameworkCore;

namespace Gred.Repositories.Implementation
{
  public class VwMedicationRptRepository : IVwMedicationRptRepository
  {
    private readonly GredDbContext _context;

    public VwMedicationRptRepository(GredDbContext context)
    {
      _context = context;
    }

    public async Task<IEnumerable<VwMedicationRpt>> GetAllAsync()
    {
      return await _context.VwMedicationRpts.ToListAsync();
    }
  }
}
