using System.Threading.Tasks;
using Gred.Repositories.Interface;
using Microsoft.AspNetCore.Mvc;

namespace Gred.Controllers
{
  [Route("api/[controller]")]
  [ApiController]
  public class VwMedicationRptController : ControllerBase
  {
    private readonly IVwMedicationRptRepository _repository;

    public VwMedicationRptController(IVwMedicationRptRepository repository)
    {
      _repository = repository;
    }

    [HttpGet("GetAll")]
    public async Task<IActionResult> GetAll()
    {
      var data = await _repository.GetAllAsync();
      return Ok(data);
    }
  }
}
