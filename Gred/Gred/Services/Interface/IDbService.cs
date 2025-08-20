namespace Gred.Services.Interface
{
  public interface IDbService
  {
    Task<int> ExecuteAsync(string sql, object param);
    Task<T> ExecuteScalarAsync<T>(string v, object value);

  }
}
