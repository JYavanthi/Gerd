using gred.Data;
using Gred.Data.Entities;
using Gred.Data.Entities.Common;
using Gred.Services.Interface;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System.Data;

namespace Gred.Repositories;
  public class citiesRepository : ICities
{
  private readonly GredDbContext _context;

  public citiesRepository(GredDbContext context)
  {
    this._context = context;
  }



  public async Task<CommonRsult> Getcities()
  {
    CommonRsult result = new CommonRsult();
    try
    {
      var data = "";
      result.Data = data;
    }

    catch (Exception ex)
    {
      result.Type = "E";
      result.Message = ex.Message;
    }

    return result;
  }

}
