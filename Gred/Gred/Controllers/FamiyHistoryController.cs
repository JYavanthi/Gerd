using Gred.Data.Entities.Common;
using Gred.Data.Entities;
using Gred.Services.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using gred.Models;

namespace Gred.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FamiyHistoryController : ControllerBase
    {
        private readonly IFamilyHistory _family;

        public FamiyHistoryController(IFamilyHistory family)
        {
            this._family = family;
        }
        [HttpGet("GetFamilyHistory")]
        public async Task<CommonRsult> GetFamilyHistory()
        {
            CommonRsult result = new CommonRsult();
            try
            {
                result = await _family.GetFamilyHistory();
            }
            catch (Exception ex)
            {
                result.Type = "E";
                result.Message = ex.Message;
            }
            return result;
        }
        [HttpPost("SaveFamilyHistory")]
        public async Task<CommonRsult> AddFamilyHistory(EFamilyHistory eFamilyHistory) { 
        //{
        //    CommonRsult result = new CommonRsult();
        //    try
        //    {
        //        result = await _family.AddFamilyHistory(familyHistory);
        //    }
        //    catch (Exception ex)
        //    {
        //        result.Type = "E";
        //        result.Message = ex.Message;
        //    }
            return await _family.AddFamilyHistory(eFamilyHistory);
        }


    [HttpGet("GetFamilyHistoryById/{id}/{stage}")]
    public async Task<CommonRsult> GetFamilyHistoryById(int id, int stage)
    {
      return await _family.GetFamilyHistoryById(id ,stage);
    }

  }
}
