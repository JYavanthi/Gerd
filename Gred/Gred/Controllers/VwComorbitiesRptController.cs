//using Microsoft.AspNetCore.Mvc;
//using Microsoft.EntityFrameworkCore;
//using gred.Models;

//namespace gred.Controllers;

//[ApiController]
//[Route("api/[controller]")]
//public class VwComorbitiesRptController : ControllerBase
//{
//  private readonly GerdContext _context;

//  public VwComorbitiesRptController(GerdContext context)
//  {
//    _context = context;
//  }

//  [HttpGet]
//  public async Task<ActionResult<IEnumerable<VwComorbitiesRpt>>> Get()
//  {
//    return await _context.VwComorbitiesRpt.ToListAsync();
//  }
//}
