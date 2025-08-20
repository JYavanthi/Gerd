using gred.Data;
using Gred.Data.Entities.Common;
using Gred.Data.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Gred.Services.Interface;

namespace Gred.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CheifComplaintController : ControllerBase
    {
        private readonly GredDbContext _context;
        private readonly ICheifComplaint _cheif;

        public CheifComplaintController(GredDbContext context,ICheifComplaint cheif)
        {
            this._context = context;
            this._cheif = cheif;
        }

        [HttpPost("SaveCheifComplaint")]
        public async Task<CommonRsult> SaveCheifComplaint(ECheifComplaint eCheif)
        {
            return await _cheif.SaveCheifComplaint(eCheif);
        }
        [HttpGet("GetCheifComplaint")]
        public async Task<CommonRsult> GetCheifComplaint()
        {
            return await _cheif.GetCheifComplaint();
        }

   [HttpGet("GetCheifComplaintById/{patientId}/{stage}")]
    public async Task<CommonRsult> GetCheifComplaintById(int patientId, int stage)
    {
      return await _cheif.GetCheifComplaintById(patientId, stage);
    }

  }
}
