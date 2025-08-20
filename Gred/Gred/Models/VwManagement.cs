using System;
using System.Collections.Generic;

namespace gred.Models;

public partial class VwManagement
{
    public int? PatientId { get; set; }

    public int ManagementId { get; set; }

    public int? LifestyleRecommendations { get; set; }

    public int? CreatedBy { get; set; }

    public DateTime? CreatedDt { get; set; }

    public int? ModifiedBy { get; set; }

    public DateTime? ModifiedDt { get; set; }

    public int? Stage { get; set; }
}
