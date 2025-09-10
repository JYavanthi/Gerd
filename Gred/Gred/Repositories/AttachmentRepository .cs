using gred.Data;
using gred.Models;
using Microsoft.EntityFrameworkCore;

namespace gred.Repository
{
  public class AttachmentRepository : IAttachmentRepository
  {
    private readonly GredDbContext _context;
    public AttachmentRepository(GredDbContext context)
    {
      _context = context;
    }

    public async Task<Attachment> AddAttachmentAsync(Attachment attachment)
    {
      _context.Attachments.Add(attachment);
      await _context.SaveChangesAsync();
      return attachment;
    }

    public async Task<Attachment?> GetAttachmentAsync(int id)
    {
      return await _context.Attachments.FirstOrDefaultAsync(x => x.AttachmentId == id);
    }

    public async Task<IEnumerable<Attachment>> GetAttachmentsByPatientAsync(int patientId, int stage, string filesection)
    {
        return await _context.Attachments.Where(x => x.PatientId == patientId && x.Stage == stage && x.Section == filesection).ToListAsync();
    }

    public async Task<bool> DeleteAttachmentAsync(int id)
    {
      var attachment = await _context.Attachments.FindAsync(id);
      if (attachment == null) return false;

      _context.Attachments.Remove(attachment);
      await _context.SaveChangesAsync();
      return true;
    }
  }
}
