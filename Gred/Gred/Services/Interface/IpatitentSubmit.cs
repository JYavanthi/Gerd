using Gred.Data.Entities.Common;

public interface IPatientSubmit
{
  Task<CommonRsult> SubmitPatientStage(int? stage, int? patientId, int createdBy);
}
