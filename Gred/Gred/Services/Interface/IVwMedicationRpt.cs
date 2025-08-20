using System.Collections.Generic;
using System.Threading.Tasks;
using gred.Models;

namespace Gred.Repositories.Interface
{
  public interface IVwMedicationRptRepository
  {
    Task<IEnumerable<VwMedicationRpt>> GetAllAsync();
  }
}
