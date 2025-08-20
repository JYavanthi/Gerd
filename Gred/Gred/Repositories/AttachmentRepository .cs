//using System.Collections.Generic;
//using System.Linq;
//using System.Threading.Tasks;
//using Microsoft.EntityFrameworkCore;
//using gred.Data;
//using gred.Models;
//using Microsoft.AspNetCore.Mvc;

//namespace gred.Repositories
//{
//  [ApiController]
//  [Route("api/[controller]")]
//  public class AttachmentsController : ControllerBase
//  {
//    private readonly IAttachmentRepository _repository;

//    public AttachmentsController(IAttachmentRepository repository)
//    {
//      _repository = repository;
//    }

//    // GET api/attachments/patient/{patientId}
//    [HttpGet("patient/{patientId}")]
//    public async Task<ActionResult<IEnumerable<Attachment>>> GetByPatientId(int patientId)
//    {
//      var attachments = await _repository.GetByPatientIdAsync(patientId);
//      if (attachments == null || !attachments.Any())
//        return NotFound();

//      return Ok(attachments);
//    }

//    // POST api/attachments
//    [HttpPost]
//    public async Task<ActionResult<Attachment>> CreateAttachment([FromBody] Attachment attachment)
//    {
//      await _repository.AddAsync(attachment);
//      return CreatedAtAction(nameof(GetByPatientId), new { patientId = attachment.PatientId }, attachment);
//    }
//  }

//}
