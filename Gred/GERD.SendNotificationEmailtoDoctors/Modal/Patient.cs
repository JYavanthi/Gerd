using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GERD.SendNotificationEmailtoDoctors.Modal
{
  public class Patient
  {
    public int PatientId { get; set; }
    public int DoctorId { get; set; }
    public int Stage { get; set; }
    public DateTime CreatedDt { get; set; }
    public DateTime? BlSubmitted { get; set; }
    public DateTime? Fu1Submitted { get; set; }
    public string Initial { get; set; }
    public string? SubjectNo { get; set; }
  }
}
