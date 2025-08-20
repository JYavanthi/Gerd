
using Microsoft.AspNetCore.Mvc;
using gred.Models;
using Microsoft.AspNetCore.Mvc;
using gred.Data;
using Microsoft.EntityFrameworkCore;

namespace gred.Controllers;

[ApiController]
[Route("api/[controller]")]
public class PtnTrackController : ControllerBase
{
  private readonly IPtnTrackRepository _ptnTrackRepository;

  public PtnTrackController(IPtnTrackRepository ptnTrackRepository)
  {
    _ptnTrackRepository = ptnTrackRepository;
  }
  [HttpGet("GetPageRouterByPatientId/{patientId}")]
  public async Task<IActionResult> GetPageRouterByPatientId(int patientId)
    
  {
    var result = await _ptnTrackRepository.GetPageRouterByPatientIdAsync(patientId);

    if (result == null)
      result= "/case-details/{patientId}"; // Default page

    return Ok(new { pageRouter = result }); // ✅ Return a JSON object
  }

}






