using Gred.Data.Entities.Common;
using Gred.Services.Interface;
using Microsoft.AspNetCore.Mvc;

namespace Gred.Controllers
{
  [Route("api/[controller]")]
  [ApiController]
  public class GenderRPTController : Controller
  {
    private readonly IGenderRPT _genderRptService;

    public GenderRPTController(IGenderRPT genderRptService)
    {
      _genderRptService = genderRptService;
    }

    [HttpGet("GetAll")]
    public async Task<CommonRsult> GetAll()
    {
      return await _genderRptService.GetAllGenderData();
    }
  }
}
