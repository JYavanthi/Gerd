using System;
using System.Collections.Generic;

namespace gred.Models;

public partial class VwBaselineRpt
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

    public int? Pincode { get; set; }

    public string? PlaceType { get; set; }

    public string? SocioeconomicStatus { get; set; }

    public string? FamilyIncome { get; set; }

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

    public string? HtPresent { get; set; }

    public string? HtRemark { get; set; }

    public string? DbPresent { get; set; }

    public string? DbRemark { get; set; }

    public string? DdPresent { get; set; }

    public string? DdRemark { get; set; }

    public string? CldPresent { get; set; }

    public string? CldRemark { get; set; }

    public string? NdPresent { get; set; }

    public string? NdRemark { get; set; }

    public string? CdPresent { get; set; }

    public string? CdRemark { get; set; }

    public string? HtdPresent { get; set; }

    public string? HtdRemark { get; set; }

    public string? BdPresent { get; set; }

    public string? BdRemark { get; set; }

    public string? CkdPresent { get; set; }

    public string? CkdRemark { get; set; }

    public string? APresent { get; set; }

    public string? ARemark { get; set; }

    public string? OPresent { get; set; }

    public string? ORemark { get; set; }

    public string? RaPresent { get; set; }

    public string? RaRemark { get; set; }

    public string? SsPresent { get; set; }

    public string? SsRemark { get; set; }

    public string? CPresent { get; set; }

    public string? CRemark { get; set; }

    public string? CmoPresent { get; set; }

    public string? CmoRemark { get; set; }

    public string? PastHistory { get; set; }

    public bool? DietVegetarian { get; set; }

    public bool? AeratedIntake { get; set; }

    public string? AeratedQuantity { get; set; }

    public string? AeratedFrequency { get; set; }

    public string? AeratedDuration { get; set; }

    public bool? CoffeeIntake { get; set; }

    public string? CoffeeFrequency { get; set; }

    public string? CoffeeQuantity { get; set; }

    public string? CoffeeDuration { get; set; }

    public bool? TeaIntake { get; set; }

    public string? TeaFrequency { get; set; }

    public string? TeaDuration { get; set; }

    public bool? SpicyIntake { get; set; }

    public string? SpicyFrequency { get; set; }

    public string? SpicyQuantity { get; set; }

    public string? SpicyDuration { get; set; }

    public bool? AlcoholIntake { get; set; }

    public string? AlcoholFrequency { get; set; }

    public string? AlcoholQuantity { get; set; }

    public string? AlcoholDuration { get; set; }

    public bool? SweetsIntake { get; set; }

    public string? SweetsFrequency { get; set; }

    public string? SweetsQuantity { get; set; }

    public string? SweetsDuration { get; set; }

    public bool? SmokingIntake { get; set; }

    public string? SmokingFrequency { get; set; }

    public string? SmokingQuantity { get; set; }

    public string? SmokingDuration { get; set; }

    public bool? TobaccoIntake { get; set; }

    public string? TobaccoFrequency { get; set; }

    public string? TobaccoQuantity { get; set; }

    public string? TobaccoDuration { get; set; }

    public string? SleepApneano { get; set; }

    public string? SleepApneayes { get; set; }

    public string? SleepApneaFrequency { get; set; }

    public string? SleepApneaDuration { get; set; }

    public string? ExerciseIntakeno { get; set; }

    public string? ExerciseIntakeyes { get; set; }

    public string? WalkingSelectedyes { get; set; }

    public string? WalkingSelectedno { get; set; }

    public string? WalkingFrequency { get; set; }

    public string? WalkingDuration { get; set; }

    public string? JoggingSelectedyes { get; set; }

    public string? JoggingSelectedno { get; set; }

    public string? JoggingFrequency { get; set; }

    public string? JoggingDuration { get; set; }

    public string? GymSelectedyes { get; set; }

    public string? GymSelectedno { get; set; }

    public string? GymFrequency { get; set; }

    public string? GymDuration { get; set; }

    public string? YogaSelectedyes { get; set; }

    public string? YogaSelectedno { get; set; }

    public string? YogaFrequency { get; set; }

    public string? YogaDuration { get; set; }

    public string? Aerobicsyes { get; set; }

    public string? Aerobicsno { get; set; }

    public string? AerobicsFrequency { get; set; }

    public string? AerobicsDuration { get; set; }

    public string? Zumbayes { get; set; }

    public string? Zumbano { get; set; }

    public string? ZumbaFrequency { get; set; }

    public string? ZumbaDuration { get; set; }

    public string? Othersyes { get; set; }

    public string? Othersno { get; set; }

    public string? OthersFrequency { get; set; }

    public string? OthersDuration { get; set; }

    public bool? ComputerUsed { get; set; }

    public string? ComputerFrequency { get; set; }

    public int? ComputerDurationYears { get; set; }

    public bool? SmartphoneUsed { get; set; }

    public string? SmartphoneFrequency { get; set; }

    public int? SmartphoneDurationYears { get; set; }

    public string? WorkingHours { get; set; }

    public string? JobType { get; set; }

    public int? TotalWorkingYears { get; set; }

    public string? FhGred { get; set; }

    public string? FhRemark { get; set; }

    public string? FhEgc { get; set; }

    public string? FhEgcremark { get; set; }

    public string? UsageOfPpi { get; set; }

    public string? MedicationName { get; set; }

    public string Dose { get; set; } = null!;

    public string Frequency { get; set; } = null!;

    public string? HistoryofEndoscopy { get; set; }

    public DateTime? EndoscopyDate { get; set; }

    public bool? EndoscopyAttached { get; set; }

    public string? EndoscopyRemark { get; set; }

    public bool? HistoryofGs { get; set; }

    public bool? GsBariatricSurgery { get; set; }

    public string? GsBsremark { get; set; }

    public bool? GsFundoplicationSurgery { get; set; }

    public string? GsFsremark { get; set; }

    public bool? GsGastricPoemsurgery { get; set; }

    public string? GsGpsremark { get; set; }

    public bool? GsGastrojejunostomy { get; set; }

    public string? GsGjremark { get; set; }

    public bool? GsOther { get; set; }

    public string? GsOtherRemark { get; set; }

    public string? NsaidsMolecule { get; set; }

    public string? NsaidsDose { get; set; }

    public string? NsaidsFrequency { get; set; }

    public string? BisphosphonatesMolecule { get; set; }

    public string? BisphosphonatesDose { get; set; }

    public string? BisphosphonatesFrequency { get; set; }

    public string? SteroidsMolecule { get; set; }

    public string? SteroidsDose { get; set; }

    public string? SteroidsFrequency { get; set; }

    public string? AntiplateletMolecule { get; set; }

    public string? AntiplateletDose { get; set; }

    public string? AntiplateletFrequency { get; set; }

    public string? OthersMolecule { get; set; }

    public string? OthersDose { get; set; }

    public string? OthersFrequency1 { get; set; }

    public string? PeHeight { get; set; }

    public string? PeWeight { get; set; }

    public string? PeBmi { get; set; }

    public string? SeGanormal { get; set; }

    public string? SeGaabNormalRemark { get; set; }

    public string? SeRsnormal { get; set; }

    public string? SeRsabNormalRemark { get; set; }

    public string? OthersNormal { get; set; }

    public string? OthersAbNormalCs { get; set; }

    public string? OthersAbNormalRemark { get; set; }

    public string? PaeFindings { get; set; }

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

    public string? TotalPoints { get; set; }

    public string? AcidRefluxSymptom { get; set; }

    public string? Dysmotity { get; set; }

    public bool? HeartburnHeartburn { get; set; }

    public bool? RegurgitationHeartburn { get; set; }

    public bool? RetrosternalPainHeartburn { get; set; }

    public bool? AcidTasteMouthHeartburn { get; set; }

    public string SymtopmScore { get; set; } = null!;

    public bool? EeLaxlesClassification { get; set; }

    public string? EeAngelesGrade { get; set; }

    public string? EeAgremarks { get; set; }

    public string? EeBarrettRemark { get; set; }

    public string? EeHillRemarks { get; set; }

    public string? EeHillClassificationGrade { get; set; }

    public bool? PHimpedanceMonitoring { get; set; }

    public DateTime? PHimDate { get; set; }

    public bool? PHimAttached { get; set; }

    public string? PHimRemark { get; set; }

    public bool? ManometryTest { get; set; }

    public DateTime? MtDate { get; set; }

    public bool? MtAttached { get; set; }

    public string? MtRemark { get; set; }

    public bool? Biopsy { get; set; }

    public DateTime? BiopsyDate { get; set; }

    public bool? BiopsyAttached { get; set; }

    public string? BiopsyRemark { get; set; }

    public bool? NewlyDiagnosed { get; set; }

    public bool? KnownCaseOfGerd { get; set; }

    public int? GredNoOfYear { get; set; }

    public string? Gerdtype { get; set; }

    public bool? RefractoryToPpi { get; set; }

    public bool? AdherenceToTherapy { get; set; }

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
