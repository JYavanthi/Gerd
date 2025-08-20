using System;
using System.Collections.Generic;

namespace gred.Models;

public partial class VwAssessment
{
    public int AssessmentId { get; set; }

    public int Pid { get; set; }

    public int? Q1 { get; set; }

    public int? Q2 { get; set; }

    public int? Q3 { get; set; }

    public int? Q4 { get; set; }

    public int? Q5 { get; set; }

    public int? Q6 { get; set; }

    public int? Q7 { get; set; }

    public int? Q8 { get; set; }

    public int? Q9 { get; set; }

    public int? Q10 { get; set; }

    public int? Q11 { get; set; }

    public int? Q12 { get; set; }

    public string? AcidRefluxSymptom { get; set; }

    public string? Dysmotity { get; set; }

    public string? TotalPoints { get; set; }

    public bool? HeartburnNil { get; set; }

    public bool? HeartburnMinimal { get; set; }

    public bool? HeartburnModerate { get; set; }

    public bool? HeartburnHeartburn { get; set; }

    public bool? RegurgitationNil { get; set; }

    public bool? RegurgitationMinimal { get; set; }

    public bool? RegurgitationModerate { get; set; }

    public bool? RegurgitationHeartburn { get; set; }

    public bool? RetrosternalPainNil { get; set; }

    public bool? RetrosternalPainMinimal { get; set; }

    public bool? RetrosternalPainModerate { get; set; }

    public bool? RetrosternalPainHeartburn { get; set; }

    public bool? AcidTasteMouthNil { get; set; }

    public bool? AcidTasteMouthMinimal { get; set; }

    public bool? AcidTasteMouthModerate { get; set; }

    public bool? AcidTasteMouthHeartburn { get; set; }

    public bool? EeLaxlesClassification { get; set; }

    public string? EeAngelesGrade { get; set; }

    public string? EeAgremarks { get; set; }

    public string? EeBarrettRemark { get; set; }

    public string? EeHillClassificationGrade { get; set; }

    public string? EeHillRemarks { get; set; }

    public bool? PHimpedanceMonitoring { get; set; }

    public DateTime? PHimDate { get; set; }

    public bool? PHimAttached { get; set; }

    public string? PHimAttachement { get; set; }

    public string? PHimRemark { get; set; }

    public bool? ManometryTest { get; set; }

    public DateTime? MtDate { get; set; }

    public bool? MtAttached { get; set; }

    public string? MtAttachement { get; set; }

    public string? MtRemark { get; set; }

    public bool? Biopsy { get; set; }

    public DateTime? BiopsyDate { get; set; }

    public bool? BiopsyAttached { get; set; }

    public string? BiopsyAttachement { get; set; }

    public string? BiopsyRemark { get; set; }

    public int? CreatedBy { get; set; }

    public DateTime? CreatedDt { get; set; }

    public int? ModifiedBy { get; set; }

    public DateTime? ModifiedDt { get; set; }

    public int? TotalSymptomScore { get; set; }

    public int? SymptomScore { get; set; }
}
