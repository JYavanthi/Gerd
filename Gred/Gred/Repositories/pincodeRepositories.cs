using System.Runtime.InteropServices;


using Gred.Data;
using Gred.Models;
using Gred.Data.Entities.Common;
using Microsoft.Data.SqlClient;
using Dapper;
using Microsoft.Extensions.Configuration;
using Gred.Services.Interface;
using System.Threading.Tasks;
using gred.Data;
using gred.Models;

namespace Gred.Repositories
{
  public class pincodeRepositories : Ipincode
  {
    private readonly GredDbContext _context;

    public async Task<CommonRsult> GetPincodes()
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
}

