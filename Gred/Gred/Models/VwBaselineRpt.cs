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

    public string Heartburn { get; set; } = null!;

    public string Regurgitation { get; set; } = null!;

    public string RetrosternalPain { get; set; } = null!;

    public string AcidTasteInMouth { get; set; } = null!;

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
}
