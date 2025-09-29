//using Microsoft.AspNetCore.Mvc;
//using gred.Models;
//using gred.Repository;

//namespace gred.Controllers
//{
//    [Route("api/[controller]")]
//    [ApiController]
//    public class PincodeController : ControllerBase
//    {
//        private readonly IAllIndiaPincodeRepository _repository;

//        public PincodeController(IAllIndiaPincodeRepository repository)
//        {
//            _repository = repository;
//        }

//        // GET: api/pincode/searchByName?name=Post
//        [HttpGet("searchByName")]
//        public async Task<IActionResult> SearchByName([FromQuery] string name)
//        {
//            if (string.IsNullOrEmpty(name))
//                return BadRequest("Please provide a name to search.");

//            var results = await _repository.SearchByNameAsync(name);

//            if (!results.Any())
//                return NotFound("No records found.");

//            return Ok(results);
//        }
//    }
//}
