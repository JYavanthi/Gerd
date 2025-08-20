using System.Collections.Generic;
using System.Threading.Tasks;
using gred.Data;
using gred.Models;
using Gred.Repositories.Interface;
using Microsoft.EntityFrameworkCore;

namespace Gred.Repositories.Implementation
{
  public class ComorbitiesRptRepository : IComorbitiesRptRepository
  {
    private readonly GredDbContext _context;

    public ComorbitiesRptRepository(GredDbContext context)
    {
      _context = context;
    }

    public async Task<IEnumerable<VwComorbitiesRpt>> GetAllAsync()
    {
      return await _context.VwComorbitiesRpts.ToListAsync();
    }
  }
}
