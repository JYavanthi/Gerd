using System.Runtime.InteropServices;

using Gred.Data.Entities.Common;
using Gred.Data.Entities;
using gred.Data;
using Microsoft.Data.SqlClient;
using System.Data;
using Gred.Services.Interface;
using Microsoft.EntityFrameworkCore;
namespace Gred.Repositories;


public class StateRepository : IState
{
  private readonly GredDbContext _context;

  public StateRepository(GredDbContext context)
  {
    this._context = context;
  }
  public async Task<CommonRsult> GetState()
  {
    CommonRsult result = new CommonRsult();
    try
    {
      var data = "";

      result.Data = data;
      result.Message = "Successfully";
      result.Count = data.Count();
      result.Type = "S";
    }
    catch (Exception ex)
    {
      result.Type = "E";
      result.Message = ex.Message;
    }
    return result;
  }
}
