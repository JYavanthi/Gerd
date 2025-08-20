namespace Gred.Data.Entities
{
  public class ESleep
  {
    public string? Flag { get; set; }
    public int Id { get; set; }
    public int PatientId { get; set; }
    public int Stage { get; set; }

    public string? SleepApneayes { get; set; }
    public string? SleepApneano { get; set; }
    public string? SleepApneaFrequency { get; set; }
    public string? SleepApneaDuration { get; set; }

    public string? ExerciseIntakeyes { get; set; }
    public string? ExerciseIntakeno { get; set; }

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

    public string? WalkingSelectedyes { get; set; }
    public string? WalkingSelectedno { get; set; }
    public string? WalkingFrequency { get; set; }
    public string? WalkingDuration { get; set; }

    public string? Aerobicsyes { get; set; }
    public string? Aerobicsno { get; set; }
    public string? AerobicsFrequency { get; set; }
    public string? AerobicsDuration { get; set; }

    public string? Zumbayes { get; set; }
    public string? Zumbano { get; set; }
    public string? ZumbaFrequency { get; set; }
    public string? ZumbaDuration { get; set; }
   
    public string? othersText { get; set; }
    public string? Othersyes { get; set; }
    public string? Othersno { get; set; }
    public string? OthersFrequency { get; set; }
    public string? OthersDuration { get; set; }

    public int CreatedBy { get; set; }
  }
}
