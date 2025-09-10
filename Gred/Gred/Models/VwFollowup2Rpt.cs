using System;
using System.Collections.Generic;

namespace gred.Models;

public partial class VwFollowup2Rpt
{
    public string? Initial { get; set; }

    public string? SubjectNo { get; set; }

    public DateTime? Date { get; set; }

    public int? Age { get; set; }

    public string? Gender { get; set; }

    public string? Education { get; set; }

    public string? Occupation { get; set; }

    public string? StateName { get; set; }

    public string? CityName { get; set; }

    public string? PlaceType { get; set; }

    public string? SocioeconomicStatus { get; set; }

    public string? FamilyIncome { get; set; }

    public int? HbDuration { get; set; }

    public int? HbFrequency { get; set; }

    public string? HbPostural { get; set; }

    public string? HbNocturnal { get; set; }

    public int? RDuration { get; set; }

    public int? RFrequency { get; set; }

    public string? RPostural { get; set; }

    public string? RNocturnal { get; set; }

    public int? RpDuration { get; set; }

    public int? RpFrequency { get; set; }

    public string? RpPostural { get; set; }

    public string? RpNocturnal { get; set; }

    public int? AtDuration { get; set; }

    public int? AtFrequency { get; set; }

    public string? AtPostural { get; set; }

    public string? AtNocturnal { get; set; }

    public string HtPresent { get; set; } = null!;

    public string? HtRemark { get; set; }

    public string DbPresent { get; set; } = null!;

    public string? DbRemark { get; set; }

    public string DdPresent { get; set; } = null!;

    public string? DdRemark { get; set; }

    public string CldPresent { get; set; } = null!;

    public string? CldRemark { get; set; }

    public string NdPresent { get; set; } = null!;

    public string? NdRemark { get; set; }

    public string CdPresent { get; set; } = null!;

    public string? CdRemark { get; set; }

    public string HtdPresent { get; set; } = null!;

    public string? HtdRemark { get; set; }

    public string BdPresent { get; set; } = null!;

    public string? BdRemark { get; set; }

    public string CkdPresent { get; set; } = null!;

    public string? CkdRemark { get; set; }

    public string APresent { get; set; } = null!;

    public string? ARemark { get; set; }

    public string OPresent { get; set; } = null!;

    public string? ORemark { get; set; }

    public string RaPresent { get; set; } = null!;

    public string? RaRemark { get; set; }

    public string SsPresent { get; set; } = null!;

    public string? SsRemark { get; set; }

    public string CPresent { get; set; } = null!;

    public string? CRemark { get; set; }

    public string CmoPresent { get; set; } = null!;

    public string? CmoRemark { get; set; }

    public int? DoYouGetHeartburn { get; set; }

    public int? DoesYourStomachGetBloated { get; set; }

    public int? DoesYourStomachEverFeelHeavyAfterMeals { get; set; }

    public int? DoYouSometimesSubconsciouslyRubYourChestWithYourHand { get; set; }

    public int? DoYouEverFeelSickAfterMeals { get; set; }

    public int? DoYouGetHeartburnAfterMeals { get; set; }

    public int? DoYouHaveAnUnusualSymptomEGBurningSensationInYourThroat { get; set; }

    public int? DoYouFeelFullWhileEatingMeals { get; set; }

    public int? DoSomeThingsGetStuckWhenYouSwallow { get; set; }

    public int? DoYouGetBitterLiquidAcidComingUpIntoYourThroat { get; set; }

    public int? DoYouBurpALot { get; set; }

    public int? DoYouGetHeartburnIfYouBendOver { get; set; }

    public string? TotalPoints { get; set; }

    public string? AcidRefluxRelatedSymptom { get; set; }

    public string? DyspepticDysmotilitySymptom { get; set; }

    public bool? Heartburn { get; set; }

    public bool? Regurgitation { get; set; }

    public bool? RetrosternalPain { get; set; }

    public bool? AcidTasteInMouth { get; set; }

    public int? TotalSymptomScoreTssInGerdPatients { get; set; }

    public string SymtopmScore { get; set; } = null!;

    public string LaxLesClassification { get; set; } = null!;

    public string? LosAngelesGrade { get; set; }

    public string? LosAngelesGradeRemarks { get; set; }

    public string? BarrettSRemarks { get; set; }

    public string HillSClassification { get; set; } = null!;

    public string? HillSClassificationRemarks { get; set; }

    public string? HillSClassificationGrade { get; set; }

    public string PHImpedanceMonitoring { get; set; } = null!;

    public DateTime? PHImpedanceMonitoringDate { get; set; }

    public string PHimReportAttached { get; set; } = null!;

    public string? PHImpedanceMonitoringRemarks { get; set; }

    public string ManometryTest { get; set; } = null!;

    public DateTime? MtDate { get; set; }

    public string ManometryTestAttached { get; set; } = null!;

    public string? MtRemark { get; set; }

    public string BiopsyAttached { get; set; } = null!;

    public DateTime? BiopsyDate { get; set; }

    public string BiopsyAttached1 { get; set; } = null!;

    public string? BiopsyRemark { get; set; }

    public string DietModifications { get; set; } = null!;

    public string ModerationOfAlcohol { get; set; } = null!;

    public string WeightLoss { get; set; } = null!;

    public string RegularExercise { get; set; } = null!;

    public string StopTobaccoUse { get; set; } = null!;

    public string? PpiMedicationName { get; set; }

    public string? PpiDose { get; set; }

    public string? PpiFrequency { get; set; }

    public string? ProkineticsMedicationName { get; set; }

    public string? ProkineticsDose { get; set; }

    public string? ProkineticsFrequency { get; set; }

    public string? SucralfateMedicationName { get; set; }

    public string? SucralfateDose { get; set; }

    public string? SucralfateFrequency { get; set; }

    public string? AlginateMedicationName { get; set; }

    public string? AlginateDose { get; set; }

    public string? AlginateFrequency { get; set; }

    public string? H2blockersMedicationName { get; set; }

    public string? H2blockersDose { get; set; }

    public string? H2blockersFrequency { get; set; }

    public string? H2blockersCMedicationName { get; set; }

    public string? H2blockersCDose { get; set; }

    public string? H2blockersCFrequency { get; set; }

    public string? PcabMedicationName { get; set; }

    public string? PcabDose { get; set; }

    public string? PcabFrequency { get; set; }

    public string? OthersMedicationName { get; set; }

    public string? Otherdose { get; set; }

    public string? Otherfrequency { get; set; }
}
