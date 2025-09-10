using gred.Models;

namespace gred.Repository
{
  public interface IAttachmentRepository
  {
    Task<Attachment> AddAttachmentAsync(Attachment attachment);
    Task<Attachment?> GetAttachmentAsync(int id);
    Task<IEnumerable<Attachment>> GetAttachmentsByPatientAsync(int patientId, int stage, string filesection);
    Task<bool> DeleteAttachmentAsync(int id);
  }
}
