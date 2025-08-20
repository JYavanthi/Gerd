using Gred.Data.Entities.Common;
using Gred.Data.Entities;
using Gred.Services.Interface;
using Microsoft.AspNetCore.Mvc;

namespace Gred.Controllers
{
  [Route("api/[controller]")]
  [ApiController]
  public class GadgetController : Controller
  {
   
    private readonly IGadget _gadget;

    public GadgetController(IGadget gadget)
    {
      this._gadget = gadget;
    }
    [HttpGet("GetGadget")]
    public async Task<CommonRsult> GetGadget()
    {
      return await _gadget.GetGadget();
    }

    [HttpPost("SaveGadget")]
    public async Task<CommonRsult> SaveGadget(EGadget gadget)
    {
      return await _gadget.SaveGadget(gadget);
    }

    [HttpGet("GetGadgetById/{PatientId}/{stage}")]
    public async Task<CommonRsult> GetGadgetById(int PatientId ,int stage)
    {
      return await _gadget.GetGadgetById(PatientId ,stage);
    }
  }
}
