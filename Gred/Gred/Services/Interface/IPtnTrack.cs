public interface IPtnTrackRepository
{
  Task<string?> GetPageRouterByPatientIdAsync(int patientId);
}
