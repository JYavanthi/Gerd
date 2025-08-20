//using gred.Models;
//using gred.Repositories;
//using Microsoft.AspNetCore.Mvc;

//namespace gred.Controllers
//{
//  [ApiController]
//  [Route("api/[controller]")]
//  public class CityController : ControllerBase
//  {
//    private readonly ICityRepository _cityRepository;

//    public CityController(ICityRepository cityRepository)
//    {
//      _cityRepository = cityRepository;
//    }

//    [HttpGet]
//    public async Task<ActionResult<IEnumerable<VwCity>>> GetAll()
//    {
//      var cities = await _cityRepository.GetAllCitiesAsync();
//      return Ok(cities);
//    }

//    [HttpGet("{stateId}")]
//    public async Task<ActionResult<IEnumerable<VwCity>>> GetByStateId(int stateId)
//    {
//      var cities = await _cityRepository.GetCitiesByStateIdAsync(stateId);
//      return Ok(cities);
//    }
//  }
//}
