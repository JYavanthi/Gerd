using System;
using System.Collections.Generic;

namespace gred.Models;

public partial class VwAbbre
{
    public byte Id { get; set; }

    public string Abbre { get; set; } = null!;

    public string Desc { get; set; } = null!;
}
