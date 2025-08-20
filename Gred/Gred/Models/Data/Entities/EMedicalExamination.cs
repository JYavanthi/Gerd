namespace Gred.Data.Entities
{
  public class EMedicalExamination
  {
    public string Flag { get; set; }

    public int? MEID { get; set; }

    public int? Id { get; set; }

    public int? DoctorID { get; set; }

    public int? PatientID { get; set; }
    public int? Stage { get; set; }


      public decimal? PE_Height { get; set; }
      public decimal? PE_Weight { get; set; }
      public decimal? PE_BMI { get; set; }

      public bool SE_GANormal { get; set; }
      public bool SE_GAAbNormalCS { get; set; }
      public bool SE_GAAbNormalNCS { get; set; }
      public string? PE_BMSE_GAAbNormalRemarkI5 { get; set; } 

      public string? PAE_Findings { get; set; }

      public bool SE_RSNormal { get; set; }
      public bool SE_RSAbNormal_CS { get; set; }
      public bool SE_RSAbNormal_NCS { get; set; }
      public string? SE_RSAbNormalRemark { get; set; }

      public bool OthersNormal { get; set; }
      public bool OthersAbNormal_CS { get; set; }
      public bool OthersAbNormal_NCS { get; set; }
      public string? OthersAbNormalRemark { get; set; }

      public int CreatedBy { get; set; }
    }

  }

