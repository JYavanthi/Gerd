using System;
using System.Collections.Generic;

namespace gred.Models;

public partial class Patient
{
    public int PatientId { get; set; }

    public int DoctorId { get; set; }

    public string? Initial { get; set; }

    public string? SubjectNo { get; set; }

    public DateTime? Date { get; set; }

    public int? Age { get; set; }

    public string? Gender { get; set; }

    public string? Education { get; set; }

    public string? Occupation { get; set; }

    public int? State { get; set; }

    public int? City { get; set; }

    public int? Pincode { get; set; }

    public string? PlaceType { get; set; }

    public string? SocioeconomicStatus { get; set; }

    public string? FamilyIncome { get; set; }

    public string? PastHistory { get; set; }

    public string? Diet { get; set; }

    public int? CreatedBy { get; set; }

    public DateTime? CreatedDt { get; set; }

    public int? ModifiedBy { get; set; }

    public DateTime? ModifiedDt { get; set; }

    public int? Stage { get; set; }

    public DateTime? Blsubmitted { get; set; }

    public DateTime? Fu1submitted { get; set; }

    public DateTime? Fu2submitted { get; set; }

    public virtual ICollection<Assessment> Assessments { get; set; } = new List<Assessment>();

    public virtual ICollection<ChiefComplaint> ChiefComplaints { get; set; } = new List<ChiefComplaint>();

    public virtual ICollection<Comorbidity> Comorbidities { get; set; } = new List<Comorbidity>();

    public virtual ICollection<CurrentMedication> CurrentMedications { get; set; } = new List<CurrentMedication>();

    public virtual ICollection<Diagnosis> Diagnoses { get; set; } = new List<Diagnosis>();

    public virtual ICollection<FamilyHistory> FamilyHistories { get; set; } = new List<FamilyHistory>();

    public virtual ICollection<Gadget> Gadgets { get; set; } = new List<Gadget>();

    public virtual ICollection<Gerdhistory> Gerdhistories { get; set; } = new List<Gerdhistory>();

    public virtual ICollection<History> Histories { get; set; } = new List<History>();

    public virtual ICollection<Management> Managements { get; set; } = new List<Management>();

    public virtual ICollection<MedicalExamination> MedicalExaminations { get; set; } = new List<MedicalExamination>();

    public virtual ICollection<Medication> Medications { get; set; } = new List<Medication>();

    public virtual ICollection<PersonalHistory> PersonalHistories { get; set; } = new List<PersonalHistory>();

    public virtual ICollection<Sleep> Sleeps { get; set; } = new List<Sleep>();
}
