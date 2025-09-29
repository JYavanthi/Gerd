using System;
using System.Collections.Generic;

namespace gred.Models;

public partial class Pincode
{
    public int Id { get; set; }

    public string? Officename { get; set; }

    public string? Pincode1 { get; set; }

    public string? OfficeType { get; set; }

    public string? Deliverystatus { get; set; }

    public string? Divisionname { get; set; }

    public string? Regionname { get; set; }

    public string? Circlename { get; set; }

    public string? Taluk { get; set; }

    public string? Districtname { get; set; }

    public string? Statename { get; set; }

    public string? Telephone { get; set; }

    public string? RelatedSuboffice { get; set; }

    public string? RelatedHeadoffice { get; set; }

    public int? Citiid { get; set; }

    public int? Stateid { get; set; }
}
