using Gred.Data.Entities.Common;
using Gred.Data.Entities;

namespace Gred.Services.Interface { 
  public interface ICities
  {
    //Task<CommonRsult> Savecities(Ecities ecities);
    Task<CommonRsult> Getcities();
  }
}
