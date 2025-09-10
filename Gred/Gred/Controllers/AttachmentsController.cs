using gred.Models;
using gred.Repository;
using Microsoft.AspNetCore.Mvc;

namespace gred.Controllers
{
  [ApiController]
  [Route("api/[controller]")]
  public class AttachmentController : ControllerBase
  {
    private readonly IAttachmentRepository _repository;
    private readonly IWebHostEnvironment _env;

    public AttachmentController(IAttachmentRepository repository, IWebHostEnvironment env)
    {
      _repository = repository;
      _env = env;
    }

    [HttpPost("Upload")]
    public async Task<IActionResult> UploadFile(
        int patientId,
        int? doctorId,
        int? stage,
        string section,
        int createdBy,
        IFormFile file)
    {
      try
      {
        if (file == null || file.Length == 0)
          return BadRequest(new { Message = "No file uploaded." });

        var uploadsFolder = Path.Combine(_env.ContentRootPath, "Attachments");
        if (!Directory.Exists(uploadsFolder))
          Directory.CreateDirectory(uploadsFolder);

        var attachment = new Attachment
        {
          PatientId = patientId,
          DoctorId = doctorId,
          Stage = stage,
          Section = section,
          AttachmentName = file.FileName,
          CreatedBy = createdBy,
          CreatedDt = DateTime.Now,
          ModifiedBy = createdBy,
          ModifiedDt = DateTime.Now
        };

        var savedAttachment = await _repository.AddAttachmentAsync(attachment);

        var fileName = $"{savedAttachment.AttachmentId}_{file.FileName}";
        var filePath = Path.Combine(uploadsFolder, fileName);

        using (var stream = new FileStream(filePath, FileMode.Create))
        {
          await file.CopyToAsync(stream);
        }

        return Ok(new
        {
          savedAttachment.AttachmentId,
          FilePath = filePath,
          Message = "File uploaded successfully."
        });
      }
      catch (Exception ex)
      {
        // log the exception if you have a logger
        // _logger.LogError(ex, "File upload failed");

        return StatusCode(StatusCodes.Status500InternalServerError, new
        {
          Message = "An error occurred while uploading the file.",
          Details = ex.Message // ⚠️ Optional: remove in production to avoid exposing sensitive info
        });
      }
    }

    [HttpGet("GetByPatient/{patientId}/{stage}/{filesection}")]
    public async Task<IActionResult> GetFiles(int patientId,int stage, string filesection)
    {
      var files = await _repository.GetAttachmentsByPatientAsync(patientId,stage,filesection);
      return Ok(files);
    }

    [HttpGet("Download/{id}")]
    public async Task<IActionResult> DownloadFile(int id)
    {
      var attachment = await _repository.GetAttachmentAsync(id);
      if (attachment == null) return NotFound();

      var uploadsFolder = Path.Combine(_env.ContentRootPath, "Attachments");
      var fileName = $"{id}_{attachment.AttachmentName}";
      var filePath = Path.Combine(uploadsFolder, fileName);

      if (!System.IO.File.Exists(filePath))
        return NotFound("File not found on server.");

      var fileBytes = await System.IO.File.ReadAllBytesAsync(filePath);
      return File(fileBytes, "application/octet-stream", attachment.AttachmentName);
    }

    [HttpDelete("Delete/{id}")]
    public async Task<IActionResult> DeleteFile(int id)
    {
      var attachment = await _repository.GetAttachmentAsync(id);
      if (attachment == null) return NotFound();

      var uploadsFolder = Path.Combine(_env.ContentRootPath, "Attachments");
      var fileName = $"{id}_{attachment.AttachmentName}";
      var filePath = Path.Combine(uploadsFolder, fileName);

      if (System.IO.File.Exists(filePath))
        System.IO.File.Delete(filePath);

      var deleted = await _repository.DeleteAttachmentAsync(id);
      if (!deleted) return BadRequest("Could not delete.");

      return Ok(new { message = "File deleted successfully." });
    }

    [HttpGet("View/{id}")]
    public async Task<IActionResult> ViewFile(int id)
    {
      var attachment = await _repository.GetAttachmentAsync(id);
      if (attachment == null) return NotFound();

      var uploadsFolder = Path.Combine(_env.ContentRootPath, "Attachments");
      var fileName = $"{id}_{attachment.AttachmentName}";
      var filePath = Path.Combine(uploadsFolder, fileName);

      if (!System.IO.File.Exists(filePath))
        return NotFound("File not found.");

      var fileBytes = await System.IO.File.ReadAllBytesAsync(filePath);
      var contentType = GetContentType(filePath);

      return File(fileBytes, contentType, attachment.AttachmentName);
    }

    private string GetContentType(string filePath)
    {
      var ext = Path.GetExtension(filePath).ToLowerInvariant();
      return ext switch
      {
        ".jpg" or ".jpeg" => "image/jpeg",
        ".png" => "image/png",
        ".gif" => "image/gif",
        ".pdf" => "application/pdf",
        _ => "application/octet-stream"
      };
    }
  }
}
