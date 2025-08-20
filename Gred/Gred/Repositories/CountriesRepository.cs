using gred.Data;
using gred.Models;
using Gred.Data.Entities;
using Gred.Data.Entities.Common;
using Gred.Services.Interface;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System.Data;

namespace Gred.Repositories;

public class CountriesRepository : ICountry
{
  private readonly GredDbContext _context;

  public CountriesRepository(GredDbContext context)
  {
    this._context = context;
  }

  public async Task<CommonRsult> Getcountries()
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

  public Task<CommonRsult> GetCountries()
  {
    throw new NotImplementedException();
  }

  }


