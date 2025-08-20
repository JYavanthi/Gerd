using System;
using System.Collections.Generic;

namespace gred.Models;

public partial class Exercise
{
    public int ExerciseId { get; set; }

    public int? Stage { get; set; }

    public int? DoctorId { get; set; }

    public int? PatientId { get; set; }

    public string? WalkingOption { get; set; }

    public string? WalkingFrequency { get; set; }

    public string? WalkingDuration { get; set; }

    public string? JoggingOption { get; set; }

    public string? JoggingFrequency { get; set; }

    public string? JoggingDuration { get; set; }

    public string? GymOption { get; set; }

    public string? GymFrequency { get; set; }

    public string? GymDuration { get; set; }

    public string? YogaOption { get; set; }

    public string? YogaFrequency { get; set; }

    public string? YogaDuration { get; set; }

    public string? AerobicsOption { get; set; }

    public string? AerobicsFrequency { get; set; }

    public string? AerobicsDuration { get; set; }

    public string? ZumbaOption { get; set; }

    public string? ZumbaFrequency { get; set; }

    public string? ZumbaDuration { get; set; }

    public string? OthersOption { get; set; }

    public string? OthersFrequency { get; set; }

    public string? OthersDuration { get; set; }

    public string? Status { get; set; }

    public int? CreatedBy { get; set; }

    public DateTime? CreatedDt { get; set; }

    public int? ModifiedBy { get; set; }

    public DateTime? ModifiedDt { get; set; }
}
