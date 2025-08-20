using System;
using System.Collections.Generic;

namespace gred.Models;

public partial class VwGenderRpt
{
    public string? Initial { get; set; }

    public string? SubjectNo { get; set; }

    public string? Gender { get; set; }

    public int? State { get; set; }

    public int? City { get; set; }

    public string Zone { get; set; } = null!;
}
