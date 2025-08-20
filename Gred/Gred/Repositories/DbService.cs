using Gred.Services.Interface;
using Microsoft.Data.SqlClient;
using System.Data;
using Dapper;
using DocumentFormat.OpenXml.InkML;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Http.HttpResults;

namespace Gred.Repositories
{
  public class DbService : IDbService
  {
    private readonly IDbConnection _connection;
    private readonly gred.Data.GredDbContext _context;

    public DbService(IConfiguration config, gred.Data.GredDbContext context)
    {
      _connection = new SqlConnection(config.GetConnectionString("DefaultConnection"));
      this._context = context;
    }

    public async Task<int> ExecuteAsync(string sql, object param)
    {
      return await _connection.ExecuteAsync(sql, param);
    }
    public async Task<T> ExecuteScalarAsync<T>(string sql, object param)
    {
      return await _connection.ExecuteScalarAsync<T>(sql, param);
    }

    public async Task<T?> getStage<T>(int patientId)
    {
      var data = await _context.Managements.Where(m=> m.PatientId == patientId).ToListAsync();
      return(T)(object) data;
    }
  } 
}
