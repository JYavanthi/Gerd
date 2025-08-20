using System;
using System.Collections.Generic;

namespace gred.Models;

public partial class Attachment
{
  public int AttachmentId { get; set; }

  public int PatientId { get; set; }

  public int? DoctorId { get; set; }

  public int? Stage { get; set; }

  public int? Sortorder { get; set; }

  public string AttachmentName { get; set; } = null!;

  public string Section { get; set; } = null!;

  public int CreatedBy { get; set; }

  public DateTime? CreatedDt { get; set; }

  public int? ModifiedBy { get; set; }

  public DateTime? ModifiedDt { get; set; }
}
