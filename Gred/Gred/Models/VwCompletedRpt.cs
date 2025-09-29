using System;
using System.Collections.Generic;

namespace gred.Models;

public partial class VwCompletedRpt
{
    public string? Initial { get; set; }

    public string? SubjectNo { get; set; }

    public DateTime? Date { get; set; }

    public int? Age { get; set; }

    public string? Gender { get; set; }

    public string? Education { get; set; }

    public string? Occupation { get; set; }

    public string StateName { get; set; } = null!;

    public string CityName { get; set; } = null!;

    public int? Pincode { get; set; }

    public string? PlaceType { get; set; }

    public string? SocioeconomicStatus { get; set; }

    public string? FamilyIncome { get; set; }

    public int? HeartburnDurationYrs { get; set; }

    public int? HeartburnFrequencyWk { get; set; }

    public string? HeartburnPostural { get; set; }

    public string? HeartburnNocturnal { get; set; }

    public int? RegurgitationDurationYrs { get; set; }

    public int? RegurgitationFrequencyWk { get; set; }

    public string? RegurgitationPostural { get; set; }

    public string? RegurgitationNocturnal { get; set; }

    public int? RetrosternalPainDurationYrs { get; set; }

    public int? RetrosternalPainFrequencyWk { get; set; }

    public string? RetrosternalPostural { get; set; }

    public string? RetrosternalNocturnal { get; set; }

    public int? AcidTasteDurationYrs { get; set; }

    public int? AcidTasteFrequencyWk { get; set; }

    public string? AcidTastePostural { get; set; }

    public string? AcidTasteNocturnal { get; set; }

    public string Hypertension { get; set; } = null!;

    public string? HypertensionRemarks { get; set; }

    public string Diabetes { get; set; } = null!;

    public string? DiabetesRemarks { get; set; }

    public string Dyslipidemia { get; set; } = null!;

    public string? DyslipidemiaRemarks { get; set; }

    public string ChronicLiverDisease { get; set; } = null!;

    public string? ChronicLiverDiseaseRemarks { get; set; }

    public string NeurologicalDisorder { get; set; } = null!;

    public string? NeurologicalDisorderRemarks { get; set; }

    public string CardiovascularDisorders { get; set; } = null!;

    public string? CardiovascularDisordersRemarks { get; set; }

    public string Hypothyroidism { get; set; } = null!;

    public string? HypothyroidismRemarks { get; set; }

    public string Hyperthyroidism { get; set; } = null!;

    public string? HyperthyroidismRemarks { get; set; }

    public string BehaviouralDisorders { get; set; } = null!;

    public string? BehaviouralDisorderRemarks { get; set; }

    public string ChronicKidneyDisease { get; set; } = null!;

    public string? ChronicKidneyDiseaseRemarks { get; set; }

    public string Asthma { get; set; } = null!;

    public string? AsthmaRemarks { get; set; }

    public string Osteoarthritis { get; set; } = null!;

    public string? OsteoarthritisRemarks { get; set; }

    public string RheumatoidArthritis { get; set; } = null!;

    public string? RheumatoidArthritisRemrks { get; set; }

    public string SystemicSclerosis { get; set; } = null!;

    public string? SystemicSclerosisRemarks { get; set; }

    public string Cancer { get; set; } = null!;

    public string? CancerRemarks { get; set; }

    public string OtherComorbidity { get; set; } = null!;

    public string? OtherComorbiditySpecify { get; set; }

    public string? PastHistory { get; set; }

    public string Diet { get; set; } = null!;

    public string AeratedDrinks { get; set; } = null!;

    public string? AeratedDrinksFrequencyDay { get; set; }

    public string? AeratedDrinksQuantityMl { get; set; }

    public string? AeratedDrinksDurationYrs { get; set; }

    public string Coffee { get; set; } = null!;

    public string? CoffeeFrequencyDay { get; set; }

    public string? CoffeeQuantityMl { get; set; }

    public string? CoffeeDurationYrs { get; set; }

    public string Tea { get; set; } = null!;

    public string? TeaFrequencyDay { get; set; }

    public string? TeaQuantityMl { get; set; }

    public string? TeaDurationYrs { get; set; }

    public string SpicyFood { get; set; } = null!;

    public string? SpicyFoodFrequencyWeek { get; set; }

    public string? SpicyFoodQuantity { get; set; }

    public string? SpicyFoodDurationYrs { get; set; }

    public string Alcohol { get; set; } = null!;

    public string? AlcoholFrequencyWeek { get; set; }

    public string? AlcoholQuantityMl { get; set; }

    public string? AlcoholDurationYrs { get; set; }

    public string ChocolatesSweets { get; set; } = null!;

    public string? ChocolatesSweetsFrequencyWeek { get; set; }

    public string? ChocolatesSweetsQuantityG { get; set; }

    public string? ChocolatesSweetsDurationYrs { get; set; }

    public string Smoking { get; set; } = null!;

    public string? SmokingFrequencyDay { get; set; }

    public string? SmokingQuantityPacks { get; set; }

    public string? SmokingDurationYrs { get; set; }

    public string TobaccoInOtherForms { get; set; } = null!;

    public string? TobaccoInOtherFormsFrequencyDay { get; set; }

    public string? TobaccoInOtherFormsQuantity { get; set; }

    public string? TobaccoInOtherFormsDurationYrs { get; set; }

    public string SleepApnea { get; set; } = null!;

    public string? SleepApneaFrequencyWeek { get; set; }

    public string? SleepApneaDurationYears { get; set; }

    public string Exercise { get; set; } = null!;

    public string Walking { get; set; } = null!;

    public string? WalkingFrequencyHrsWeek { get; set; }

    public string? WalkingDurationYrs { get; set; }

    public string Jogging { get; set; } = null!;

    public string? JoggingFrequencyHrsWeek { get; set; }

    public string? JoggingDurationYrs { get; set; }

    public string Gym { get; set; } = null!;

    public string? GymFrequencyHrsWeek { get; set; }

    public string? GymDurationYrs { get; set; }

    public string Yoga { get; set; } = null!;

    public string? YogaFrequencyHrsWeek { get; set; }

    public string? YogaDurationYrs { get; set; }

    public string Aerobics { get; set; } = null!;

    public string? AerobicsFrequencyHrsWeek { get; set; }

    public string? AerobicsDurationYrs { get; set; }

    public string Zumba { get; set; } = null!;

    public string? ZumbaFrequencyHrsWeek { get; set; }

    public string? ZumbaDurationYrs { get; set; }

    public string OtherExercise { get; set; } = null!;

    public string? OtherExerciseFrequencyHrsWeek { get; set; }

    public string? OtherExerciseDurationYrs { get; set; }

    public string ComputerUse { get; set; } = null!;

    public string? ComputerUsageHrsDay { get; set; }

    public int? ComputerUsageDurationYears { get; set; }

    public string SmartphoneUse { get; set; } = null!;

    public string? SmartphoneUsageHrsDay { get; set; }

    public int? SmartphoneUsageDurationYears { get; set; }

    public string? WorkingHoursOccupation { get; set; }

    public string? JobOccupationType { get; set; }

    public int? DurationNoOfYearsInTheAboveWorkingHours { get; set; }

    public string? FamilyHistoryOfGerd { get; set; }

    public string? GerdRemarks { get; set; }

    public string? FamilyHistoryOfEsophagoGastricCancer { get; set; }

    public string? EsophagoGastricCancerRemarks { get; set; }

    public string? PpiUsage { get; set; }

    public string? MedicationName { get; set; }

    public string Dose { get; set; } = null!;

    public string Frequency { get; set; } = null!;

    public string? HistoryOfEndoscopy { get; set; }

    public DateTime? EndoscopyDate { get; set; }

    public string ReportAttached { get; set; } = null!;

    public string? EndoscopyRemarks { get; set; }

    public string HistoryOfGastroSurgery { get; set; } = null!;

    public string BariatricSurgery { get; set; } = null!;

    public string? BariatricSurgeryRemarks { get; set; }

    public string FundoplicationSurgery { get; set; } = null!;

    public string? FundoplicationRemarks { get; set; }

    public string GastricPoemSurgery { get; set; } = null!;

    public string? GastricPoemRemarks { get; set; }

    public string Gastrojejunostomy { get; set; } = null!;

    public string? GastrojejunostomyRemarks { get; set; }

    public string OtherGastroSurgery { get; set; } = null!;

    public string? OtherGastroSurgeryRemarks { get; set; }

    public string? NsaidsMoleculeName { get; set; }

    public string? NsaidsDose { get; set; }

    public string? NsaidsFrequency { get; set; }

    public string? BisphosphonatesMoleculeName { get; set; }

    public string? BisphosphonatesDose { get; set; }

    public string? BisphosphonatesFrequency { get; set; }

    public string? SteroidsMoleculeName { get; set; }

    public string? SteroidsDose { get; set; }

    public string? SteroidsFrequency { get; set; }

    public string? AntiPlateletAgentsMoleculeName { get; set; }

    public string? AntiPlateletDose { get; set; }

    public string? AntiPlateletFrequency { get; set; }

    public string? OtherDrugMoleculeName { get; set; }

    public string? OtherDose { get; set; }

    public string? OtherFrequency { get; set; }

    public string? HeightInCms { get; set; }

    public string? WeightInKg { get; set; }

    public string? Bmi { get; set; }

    public string? GeneralAppearance { get; set; }

    public string? GeneralAppearanceComments { get; set; }

    public string? RespiratorySystem { get; set; }

    public string? RespiratorySystemComments { get; set; }

    public string? OtherExamAreaSpecify { get; set; }

    public string? OtherExamStatus { get; set; }

    public string? OtherExamComments { get; set; }

    public string? PerAbdomenExaminationFindings { get; set; }

    public int? Doyougetheartburn { get; set; }

    public int? Doesyourstomachgetbloated { get; set; }

    public int? Doesyourstomacheverfeelheavyaftermeals { get; set; }

    public int? Doyousometimessubconsciouslyrubyourchestwithyourhand { get; set; }

    public int? Doyoueverfeelsickaftermeals { get; set; }

    public int? Doyougetheartburnaftermeals { get; set; }

    public int? Doyouhaveanunusualsymptom { get; set; }

    public int? Doyoufeelfullwhileeatingmeals { get; set; }

    public int? Dosomethingsgetstuckwhenyouswallow { get; set; }

    public int? DoyougetbitterliquidAcidComingupintoyourthroat { get; set; }

    public int? Doyouburpalot { get; set; }

    public int? Doyougetheartburnifyoubendover { get; set; }

    public string? TotalPoints { get; set; }

    public string? AcidRefluxrelatedSymptom { get; set; }

    public string? DyspepticDysmotilitySymptom { get; set; }

    public string HeartburnHeartburn { get; set; } = null!;

    public string B1Regurgitation { get; set; } = null!;

    public string Retrosternalpain { get; set; } = null!;

    public string Acidtasteinmouth { get; set; } = null!;

    public int? TotalSymptomScoreinGerdpatients { get; set; }

    public string SymtopmScore { get; set; } = null!;

    public string Laxlesclassification { get; set; } = null!;

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

    public DateTime? Mtdate { get; set; }

    public string MtAttached { get; set; } = null!;

    public string? MtRemark { get; set; }

    public string Biopsy { get; set; } = null!;

    public DateTime? BiopsyDate { get; set; }

    public string BiopsyAttached { get; set; } = null!;

    public string? BiopsyRemark { get; set; }

    public string NewlyDiagnosed { get; set; } = null!;

    public string KnownCaseOfGerd { get; set; } = null!;

    public int? GredNoOfYear { get; set; }

    public string? Gerdtype { get; set; }

    public string RefractoryToPpi { get; set; } = null!;

    public string AdherenceToTherapy { get; set; } = null!;

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

    public string? Otherdose1 { get; set; }

    public string? Otherfrequency1 { get; set; }

    public int? F1HbDuration { get; set; }

    public int? F1HbFrequency { get; set; }

    public string? F1HbPostural { get; set; }

    public string? F1HbNocturnal { get; set; }

    public int? F1RDuration { get; set; }

    public int? F1RFrequency { get; set; }

    public string? F1RPostural { get; set; }

    public string? F1RNocturnal { get; set; }

    public int? F1RpDuration { get; set; }

    public int? F1RpFrequency { get; set; }

    public string? F1RpPostural { get; set; }

    public string? F1RpNocturnal { get; set; }

    public int? F1AtDuration { get; set; }

    public int? F1AtFrequency { get; set; }

    public string? F1AtPostural { get; set; }

    public string? F1AtNocturnal { get; set; }

    public string F1Htpresent { get; set; } = null!;

    public string? F1HtRemark { get; set; }

    public string F1Dbpresent { get; set; } = null!;

    public string? F1DbRemark { get; set; }

    public string F1DdPresent { get; set; } = null!;

    public string? F1DdRemark { get; set; }

    public string F1CldPresent { get; set; } = null!;

    public string? F1CldRemark { get; set; }

    public string F1NdPresent { get; set; } = null!;

    public string? F1NdRemark { get; set; }

    public string F1CdPresent { get; set; } = null!;

    public string F1HtdPresent { get; set; } = null!;

    public string? F1HtdRemark { get; set; }

    public string F1BdPresent { get; set; } = null!;

    public string? F1BdRemark { get; set; }

    public string F1CkdPresent { get; set; } = null!;

    public string? F1CkdRemark { get; set; }

    public string F1APresent { get; set; } = null!;

    public string? F1ARemark { get; set; }

    public string F1OPresent { get; set; } = null!;

    public string? F1ORemark { get; set; }

    public string F1RaPresent { get; set; } = null!;

    public string? F1RaRemark { get; set; }

    public string F1SsPresent { get; set; } = null!;

    public string? F1SsRemark { get; set; }

    public string F1CPresent { get; set; } = null!;

    public string? F1CRemark { get; set; }

    public string F1CmoPresent { get; set; } = null!;

    public string? F1CmoRemark { get; set; }

    public int? F1Doyougetheartburn { get; set; }

    public int? F1Doesyourstomachgetbloated { get; set; }

    public int? F1Doesyourstomacheverfeelheavyaftermeals { get; set; }

    public int? F1Doyousometimessubconsciouslyrubyourchestwithyourhand { get; set; }

    public int? F1Doyoueverfeelsickaftermeals { get; set; }

    public int? F1Doyougetheartburnaftermeals { get; set; }

    public int? F1Doyouhaveanunusualsymptom { get; set; }

    public int? F1Doyoufeelfullwhileeatingmeals { get; set; }

    public int? F1Dosomethingsgetstuckwhenyouswallow { get; set; }

    public int? F1DoyougetbitterliquidAcidComingupintoyourthroat { get; set; }

    public int? F1Doyouburpalot { get; set; }

    public int? F1Doyougetheartburnifyoubendover { get; set; }

    public string? F1TotalPoints { get; set; }

    public string? F1AcidRefluxrelatedSymptom { get; set; }

    public string? F1DyspepticDysmotilitySymptom { get; set; }

    public string F1HeartburnHeartburn { get; set; } = null!;

    public string F1Regurgitation { get; set; } = null!;

    public string F1Retrosternalpain { get; set; } = null!;

    public string F1Acidtasteinmouth { get; set; } = null!;

    public int? F1TotalSymptomScoreinGerdpatients { get; set; }

    public string F1SymtopmScore { get; set; } = null!;

    public string F1Laxlesclassification { get; set; } = null!;

    public string? F1LosAngelesGrade { get; set; }

    public string? F1LosAngelesGradeRemarks { get; set; }

    public string? F1BarrettsRemarks { get; set; }

    public string F1Hillsclassification { get; set; } = null!;

    public string? F1HillsclassificationRemarks { get; set; }

    public string? F1HillsclassificationGrade { get; set; }

    public string F1PHImpedanceMonitoring { get; set; } = null!;

    public DateTime? F1PHImpedanceMonitoringDate { get; set; }

    public string F1PHimReportAttached { get; set; } = null!;

    public string? F1PHImpedanceMonitoringRemarks { get; set; }

    public string F1ManometryTest { get; set; } = null!;

    public DateTime? F1MtDate { get; set; }

    public string F1ManometryTestAttached { get; set; } = null!;

    public string? F1MtRemark { get; set; }

    public string F1Biopsy { get; set; } = null!;

    public DateTime? F1BiopsyDate { get; set; }

    public string F1BiopsyAttached { get; set; } = null!;

    public string? F1BiopsyRemark { get; set; }

    public string F1DietModifications { get; set; } = null!;

    public string F1ModerationOfAlcohol { get; set; } = null!;

    public string F1WeightLoss { get; set; } = null!;

    public string F1RegularExercise { get; set; } = null!;

    public string F1StopTobaccoUse { get; set; } = null!;

    public string? F1PpiMedicationName { get; set; }

    public string? F1PpiDose { get; set; }

    public string? F1PpiFrequency { get; set; }

    public string? F1ProkineticsMedicationName { get; set; }

    public string? F1ProkineticsDose { get; set; }

    public string? F1ProkineticsFrequency { get; set; }

    public string? F1SucralfateMedicationName { get; set; }

    public string? F1SucralfateDose { get; set; }

    public string? F1SucralfateFrequency { get; set; }

    public string? F1AlginateMedicationName { get; set; }

    public string? F1AlginateDose { get; set; }

    public string? F1AlginateFrequency { get; set; }

    public string? F1H2blockersMedicationName { get; set; }

    public string? F1H2blockersDose { get; set; }

    public string? F1H2blockersFrequency { get; set; }

    public string? F1H2blockersCMedicationName { get; set; }

    public string? F1H2blockersCDose { get; set; }

    public string? F1H2blockersCFrequency { get; set; }

    public string? F1PcabMedicationName { get; set; }

    public string? F1PcabDose { get; set; }

    public string? F1PcabFrequency { get; set; }

    public string? F1OthersMedicationName { get; set; }

    public string? F1OthersDose { get; set; }

    public string? F1OthersFrequency { get; set; }

    public int? F2HbDuration { get; set; }

    public int? F2HbFrequency { get; set; }

    public string? F2HbPostural { get; set; }

    public string? F2HbNocturnal { get; set; }

    public int? F2RDuration { get; set; }

    public int? F2RFrequency { get; set; }

    public string? F2RPostural { get; set; }

    public string? F2RNocturnal { get; set; }

    public int? F2RpDuration { get; set; }

    public int? F2RpFrequency { get; set; }

    public string? F2RpPostural { get; set; }

    public string? F2RpNocturnal { get; set; }

    public int? F2AtDuration { get; set; }

    public int? F2AtFrequency { get; set; }

    public string? F2AtPostural { get; set; }

    public string? F2AtNocturnal { get; set; }

    public string F2Htpresent { get; set; } = null!;

    public string? F2HtRemark { get; set; }

    public string F2Dbpresent { get; set; } = null!;

    public string? F2DbRemark { get; set; }

    public string F2DdPresent { get; set; } = null!;

    public string? F2DdRemark { get; set; }

    public string F2CldPresent { get; set; } = null!;

    public string? F2CldRemark { get; set; }

    public string F2NdPresent { get; set; } = null!;

    public string? F2NdRemark { get; set; }

    public string F2CdPresent { get; set; } = null!;

    public string F2HtdPresent { get; set; } = null!;

    public string? F2HtdRemark { get; set; }

    public string F2BdPresent { get; set; } = null!;

    public string? F2BdRemark { get; set; }

    public string F2CkdPresent { get; set; } = null!;

    public string? F2CkdRemark { get; set; }

    public string F2APresent { get; set; } = null!;

    public string? F2ARemark { get; set; }

    public string F2OPresent { get; set; } = null!;

    public string? F2ORemark { get; set; }

    public string F2RaPresent { get; set; } = null!;

    public string? F2RaRemark { get; set; }

    public string F2SsPresent { get; set; } = null!;

    public string? F2SsRemark { get; set; }

    public string F2CPresent { get; set; } = null!;

    public string? F2CRemark { get; set; }

    public string F2CmoPresent { get; set; } = null!;

    public string? F2CmoRemark { get; set; }

    public int? F2Doyougetheartburn { get; set; }

    public int? F2Doesyourstomachgetbloated { get; set; }

    public int? F2Doesyourstomacheverfeelheavyaftermeals { get; set; }

    public int? F2Doyousometimessubconsciouslyrubyourchestwithyourhand { get; set; }

    public int? F2Doyoueverfeelsickaftermeals { get; set; }

    public int? F2Doyougetheartburnaftermeals { get; set; }

    public int? F2Doyouhaveanunusualsymptom { get; set; }

    public int? F2Doyoufeelfullwhileeatingmeals { get; set; }

    public int? F2Dosomethingsgetstuckwhenyouswallow { get; set; }

    public int? F2DoyougetbitterliquidAcidComingupintoyourthroat { get; set; }

    public int? F2Doyouburpalot { get; set; }

    public int? F2Doyougetheartburnifyoubendover { get; set; }

    public string? F2TotalPoints { get; set; }

    public string? F2AcidRefluxrelatedSymptom { get; set; }

    public string? F2DyspepticDysmotilitySymptom { get; set; }

    public string F2HeartburnHeartburn { get; set; } = null!;

    public string F2Regurgitation { get; set; } = null!;

    public string F2Retrosternalpain { get; set; } = null!;

    public string F2Acidtasteinmouth { get; set; } = null!;

    public int? F2TotalSymptomScoreinGerdpatients { get; set; }

    public string F2SymtopmScore { get; set; } = null!;

    public string F2Laxlesclassification { get; set; } = null!;

    public string? F2LosAngelesGrade { get; set; }

    public string? F2LosAngelesGradeRemarks { get; set; }

    public string? F2BarrettsRemarks { get; set; }

    public string F2Hillsclassification { get; set; } = null!;

    public string? F2HillsclassificationRemarks { get; set; }

    public string? F2HillsclassificationGrade { get; set; }

    public string F2PHImpedanceMonitoring { get; set; } = null!;

    public DateTime? F2PHImpedanceMonitoringDate { get; set; }

    public string F2PHimReportAttached { get; set; } = null!;

    public string? F2PHImpedanceMonitoringRemarks { get; set; }

    public string F2ManometryTest { get; set; } = null!;

    public DateTime? F2MtDate { get; set; }

    public string F2ManometryTestAttached { get; set; } = null!;

    public string? F2MtRemark { get; set; }

    public string F2Biopsy { get; set; } = null!;

    public DateTime? F2BiopsyDate { get; set; }

    public string F2BiopsyAttached { get; set; } = null!;

    public string? F2BiopsyRemark { get; set; }

    public string F2DietModifications { get; set; } = null!;

    public string F2ModerationOfAlcohol { get; set; } = null!;

    public string F2WeightLoss { get; set; } = null!;

    public string F2RegularExercise { get; set; } = null!;

    public string F2StopTobaccoUse { get; set; } = null!;

    public string? F2PpiMedicationName { get; set; }

    public string? F2PpiDose { get; set; }

    public string? F2PpiFrequency { get; set; }

    public string? F2ProkineticsMedicationName { get; set; }

    public string? F2ProkineticsDose { get; set; }

    public string? F2ProkineticsFrequency { get; set; }

    public string? F2SucralfateMedicationName { get; set; }

    public string? F2SucralfateDose { get; set; }

    public string? F2SucralfateFrequency { get; set; }

    public string? F2AlginateMedicationName { get; set; }

    public string? F2AlginateDose { get; set; }

    public string? F2AlginateFrequency { get; set; }

    public string? F2H2blockersMedicationName { get; set; }

    public string? F2H2blockersDose { get; set; }

    public string? F2H2blockersFrequency { get; set; }

    public string? F2H2blockersCMedicationName { get; set; }

    public string? F2H2blockersCDose { get; set; }

    public string? F2H2blockersCFrequency { get; set; }

    public string? F2PcabMedicationName { get; set; }

    public string? F2PcabDose { get; set; }

    public string? F2PcabFrequency { get; set; }

    public string? F2OthersMedicationName { get; set; }

    public string? F2OthersDose { get; set; }

    public string? F2OthersFrequency { get; set; }
}
