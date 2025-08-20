using System.Threading.Tasks;
using Gred.Repositories.Interface;
using Microsoft.AspNetCore.Mvc;

namespace Gred.Controllers
{
  [Route("api/[controller]")]
  [ApiController]
  public class ComorbitiesRptController : ControllerBase
  {
    private readonly IComorbitiesRptRepository _comorbitiesRptRepository;

    public ComorbitiesRptController(IComorbitiesRptRepository comorbitiesRptRepository)
    {
      _comorbitiesRptRepository = comorbitiesRptRepository;
    }

    [HttpGet("GetAll")]
    public async Task<IActionResult> GetAll()
    {
      var result = await _comorbitiesRptRepository.GetAllAsync();
      return Ok(result);
    }
  }
}
