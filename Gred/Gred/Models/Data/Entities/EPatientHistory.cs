namespace Gred.Data.Entities
{
  public class EPatientHistory
  {
    public string Flag { get; set; }
    public int PatientHistoryID { get; set; }
    public int? PatientID { get; set; }
    public int? Stage { get; set; }

    public string? AD_Intake { get; set; }
    public string?  AD_Frequency { get; set; }
    public string?  AD_Quantity { get; set; }
    public string?  AD_Duration { get; set; }
    public string?  CF_Intake { get; set; }
    public string?  CF_Frequency { get; set; }
    public string? CF_Quantity { get; set; }
    public string? CF_Duration { get; set; }
    public string? T_Intake { get; set; }
    public string? T_Frequency { get; set; }
    public string? T_Quantity { get; set; }
    public string? T_Duration { get; set; }
    public string? SF_Intake { get; set; }
    public string? SF_Frequency { get; set; }
    public string? SF_Quantity { get; set; }
    public string? SF_Duration { get; set; }
    public string? AH_Intake { get; set; }
    public string? AH_Frequency { get; set; }
    public string? AH_Quantity { get; set; }
    public string? AH_Duration { get; set; }
    public string? CS_Intake { get; set; }
    public string? CS_Frequency { get; set; }
    public string? CS_Quantity { get; set; }
    public string? CS_Duration { get; set; }
    public string? S_Intake { get; set; }
    public string? S_Frequency { get; set; }
    public string? S_Quantity { get; set; }
    public string? S_Duration { get; set; }
    public string? TB_Intake { get; set; }
    public string? TB_Frequency { get; set; }
    public string? TB_Quantity { get; set; }
    public string? TB_Duration { get; set; }
    public string? G_Name { get; set; }
    public string? G_Usage { get; set; }
    public string? G_Frequency { get; set; }
    public string? G_YearOfUsage { get; set; }
    public string? WorkingHours { get; set; }
    public string? JobType { get; set; }
    public string? Duration { get; set; }
    public int? CreatedBy { get; set; }
    public int? ModifiedBy { get; set; }
    public string? Past_History { get; set; }
    public string? Diet { get; set; }

    public string? SleepApnea_Intake { get; set; }
    public string? SleepApnea_Frequency { get; set; }
    public string? SleepApnea_Duration { get; set; }

    public string? Exercise_Intake { get; set; }

    public string? Walking_Intake { get; set; }
    public string? Walking_Frequency { get; set; }
    public string? Walking_Duration { get; set; }

    public string? Jogging_Intake { get; set; }
    public string? Jogging_Frequency { get; set; }
    public string? Jogging_Duration { get; set; }

    public string? Gym_Intake { get; set; }
    public string? Gym_Frequency { get; set; }
    public string? Gym_Duration { get; set; }

    public string? Yoga_Intake { get; set; }
    public string? Yoga_Frequency { get; set; }
    public string? Yoga_Duration { get; set; }

    public string? Aerobics_Intake { get; set; }
    public string? Aerobics_Frequency { get; set; }
    public string? Aerobics_Duration { get; set; }

    public string? Zumba_Intake { get; set; }
    public string? Zumba_Frequency { get; set; }
    public string? Zumba_Duration { get; set; }

    public string? OthersExercise_Intake { get; set; }
    public string? OthersExercise_Frequency { get; set; }
    public string? OthersExercise_Duration { get; set; }
  }
}
