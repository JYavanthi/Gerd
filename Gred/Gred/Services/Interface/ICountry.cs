using Gred.Data.Entities.Common;
using Gred.Data.Entities;

namespace Gred.Services.Interface
{
  public interface ICountry
  {
    Task<CommonRsult> GetCountries();
    
    


  }
}
