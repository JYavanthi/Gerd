using gred.Data;
using gred.Models;
using Gred.Data.Entities;
using Gred.Data.Entities.Common;
using Gred.Services.Interface;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System.Data;

namespace Gred.Repository
{
  public class GenderRPTRepository : IGenderRPT
  {
    private readonly GredDbContext _context;

    public GenderRPTRepository(GredDbContext context)
    {
      _context = context;
    }

    public async Task<CommonRsult> GetAllGenderData()
    {
      var result = new CommonRsult();
      try
      {
        var data = await _context.VwGenderRpts.ToListAsync();
        result.Data = data;
        result.Success = true;
        result.Message = "Data fetched successfully.";
      }
      catch (Exception ex)
      {
        result.Success = false;
        result.Message = $"Error: {ex.Message}";
      }
      return result;
    }
  }
}
