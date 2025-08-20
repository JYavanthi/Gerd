//using Microsoft.AspNetCore.Mvc;
//using System.Collections.Generic;
//using System.Threading.Tasks;
//using gred.Models;
//using gred.Repositories;

//namespace gred.Controllers
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

//    // GET: api/attachments/patient/5
//    [HttpGet("patient/{patientId}")]
//    public async Task<ActionResult<IEnumerable<Attachment>>> GetByPatientId(int patientId)
//    {
//      var attachments = await _repository.GetByPatientIdAsync(patientId);
//      if (attachments == null)
//        return NotFound();

//      return Ok(attachments);
//    }

//    [HttpPost]
//    public async Task<ActionResult<Attachment>> CreateAttachment([FromBody] Attachment attachment)
//    {
//      await _repository.AddAsync(attachment);
//      // Return 201 Created with route to GET by AttachmentId (optional)
//      return CreatedAtAction(nameof(GetByPatientId), new { patientId = attachment.PatientId }, attachment);
//    }

//  }
//}
