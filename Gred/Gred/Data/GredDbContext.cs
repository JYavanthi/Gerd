using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using gred.Models;

namespace gred.Data;

public partial class GredDbContext : DbContext
{
    public GredDbContext()
    {
    }

    public GredDbContext(DbContextOptions<GredDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Abbre> Abbres { get; set; }

    public virtual DbSet<Assessment> Assessments { get; set; }

    public virtual DbSet<Attachment> Attachments { get; set; }

    public virtual DbSet<ChiefComplaint> ChiefComplaints { get; set; }

    public virtual DbSet<City> Cities { get; set; }

    public virtual DbSet<Comorbidity> Comorbidities { get; set; }

    public virtual DbSet<Country> Countries { get; set; }

    public virtual DbSet<CurrentMedication> CurrentMedications { get; set; }

    public virtual DbSet<Diagnosis> Diagnoses { get; set; }

    public virtual DbSet<Doctor> Doctors { get; set; }

    public virtual DbSet<DoctorLog> DoctorLogs { get; set; }

    public virtual DbSet<Exercise> Exercises { get; set; }

    public virtual DbSet<FamilyHistory> FamilyHistories { get; set; }

    public virtual DbSet<Gadget> Gadgets { get; set; }

    public virtual DbSet<Gerdhistory> Gerdhistories { get; set; }

    public virtual DbSet<History> Histories { get; set; }

    public virtual DbSet<Management> Managements { get; set; }

    public virtual DbSet<MedicalExamination> MedicalExaminations { get; set; }

    public virtual DbSet<Medication> Medications { get; set; }

    public virtual DbSet<Patient> Patients { get; set; }

    public virtual DbSet<PatientHistory> PatientHistories { get; set; }

    public virtual DbSet<PatientHistoryBackup> PatientHistoryBackups { get; set; }

    public virtual DbSet<PersonalHistory> PersonalHistories { get; set; }

    public virtual DbSet<PtnTrack> PtnTracks { get; set; }

    public virtual DbSet<Sleep> Sleeps { get; set; }

    public virtual DbSet<State> States { get; set; }

    public virtual DbSet<VwAbbre> VwAbbres { get; set; }

    public virtual DbSet<VwAssessment> VwAssessments { get; set; }

    public virtual DbSet<VwBaselineRpt> VwBaselineRpts { get; set; }

    public virtual DbSet<VwCheifComplaint> VwCheifComplaints { get; set; }

    public virtual DbSet<VwCity> VwCities { get; set; }

    public virtual DbSet<VwComorbidity> VwComorbidities { get; set; }

    public virtual DbSet<VwComorbitiesRpt> VwComorbitiesRpts { get; set; }

    public virtual DbSet<VwCompletedRpt> VwCompletedRpts { get; set; }

    public virtual DbSet<VwCurrentMedication> VwCurrentMedications { get; set; }

    public virtual DbSet<VwDiagnosis> VwDiagnoses { get; set; }

    public virtual DbSet<VwDoctor> VwDoctors { get; set; }

    public virtual DbSet<VwDoctorLog> VwDoctorLogs { get; set; }

    public virtual DbSet<VwDoctorRpt> VwDoctorRpts { get; set; }

    public virtual DbSet<VwExercise> VwExercises { get; set; }

    public virtual DbSet<VwFamilyHistory> VwFamilyHistories { get; set; }

    public virtual DbSet<VwFollowup1Rpt> VwFollowup1Rpts { get; set; }

    public virtual DbSet<VwFollowup2Rpt> VwFollowup2Rpts { get; set; }

    public virtual DbSet<VwGadget> VwGadgets { get; set; }

    public virtual DbSet<VwGenderRpt> VwGenderRpts { get; set; }

    public virtual DbSet<VwGerdhistory> VwGerdhistories { get; set; }

    public virtual DbSet<VwHistory> VwHistories { get; set; }

    public virtual DbSet<VwInCompletedRpt> VwInCompletedRpts { get; set; }

    public virtual DbSet<VwManagement> VwManagements { get; set; }

    public virtual DbSet<VwMedicalExamination> VwMedicalExaminations { get; set; }

    public virtual DbSet<VwMedication> VwMedications { get; set; }

    public virtual DbSet<VwMedicationRpt> VwMedicationRpts { get; set; }

    public virtual DbSet<VwPatient> VwPatients { get; set; }

    public virtual DbSet<VwPatientAgeRpt> VwPatientAgeRpts { get; set; }

    public virtual DbSet<VwPatientHistory> VwPatientHistories { get; set; }

    public virtual DbSet<VwPatientRpt> VwPatientRpts { get; set; }

    public virtual DbSet<VwPersonalHistory> VwPersonalHistories { get; set; }

    public virtual DbSet<VwSleep> VwSleeps { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
       => optionsBuilder.UseSqlServer("Server=DESKTOP-RA0KPRS\\SQLEXPRESS;Database=GERD;Trusted_Connection=True;TrustServerCertificate=True");
  // => optionsBuilder.UseSqlServer("Server=EC2AMAZ-4MMGIBF\\SQLEXPRESS;Database=GERD;user Id=sa1; Password=Micro@123#; Trusted_Connection=True;TrustServerCertificate=True");
  protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Abbre>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("Abbre");

            entity.Property(e => e.Abbre1)
                .HasMaxLength(50)
                .HasColumnName("Abbre");
            entity.Property(e => e.Desc).HasMaxLength(50);
            entity.Property(e => e.Id).HasColumnName("ID");
        });

        modelBuilder.Entity<Assessment>(entity =>
        {
            entity.ToTable("Assessment");

            entity.Property(e => e.AssessmentId).HasColumnName("AssessmentID");
            entity.Property(e => e.AcidRefluxSymptom)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.BiopsyAttached).HasColumnName("Biopsy_Attached");
            entity.Property(e => e.BiopsyAttachement)
                .HasMaxLength(500)
                .IsUnicode(false)
                .HasColumnName("Biopsy_Attachement");
            entity.Property(e => e.BiopsyDate)
                .HasColumnType("datetime")
                .HasColumnName("Biopsy_Date");
            entity.Property(e => e.BiopsyRemark)
                .HasMaxLength(1000)
                .IsUnicode(false)
                .HasColumnName("Biopsy_Remark");
            entity.Property(e => e.CreatedDt).HasColumnType("datetime");
            entity.Property(e => e.Dysmotity)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.EeAgremarks)
                .HasMaxLength(1000)
                .IsUnicode(false)
                .HasColumnName("EE_AGRemarks");
            entity.Property(e => e.EeAngelesGrade)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("EE_AngelesGrade");
            entity.Property(e => e.EeBarrettRemark)
                .HasMaxLength(1000)
                .IsUnicode(false)
                .HasColumnName("EE_BarrettRemark");
            entity.Property(e => e.EeHillClassificationGrade)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("EE_HillClassificationGrade");
            entity.Property(e => e.EeHillRemarks)
                .HasMaxLength(1000)
                .IsUnicode(false)
                .HasColumnName("EE_HillRemarks");
            entity.Property(e => e.EeLaxlesClassification).HasColumnName("EE_LAXLesClassification");
            entity.Property(e => e.ModifiedDt).HasColumnType("datetime");
            entity.Property(e => e.MtAttached).HasColumnName("MT_Attached");
            entity.Property(e => e.MtAttachement)
                .HasMaxLength(500)
                .IsUnicode(false)
                .HasColumnName("MT_Attachement");
            entity.Property(e => e.MtDate)
                .HasColumnType("datetime")
                .HasColumnName("MT_Date");
            entity.Property(e => e.MtRemark)
                .HasMaxLength(1000)
                .IsUnicode(false)
                .HasColumnName("MT_Remark");
            entity.Property(e => e.PHimAttached).HasColumnName("pHIM_Attached");
            entity.Property(e => e.PHimAttachement)
                .HasMaxLength(500)
                .IsUnicode(false)
                .HasColumnName("pHIM_Attachement");
            entity.Property(e => e.PHimDate)
                .HasColumnType("datetime")
                .HasColumnName("pHIM_Date");
            entity.Property(e => e.PHimRemark)
                .HasMaxLength(1000)
                .IsUnicode(false)
                .HasColumnName("pHIM_Remark");
            entity.Property(e => e.PHimpedanceMonitoring).HasColumnName("pHImpedanceMonitoring");
            entity.Property(e => e.Pid).HasColumnName("PID");
            entity.Property(e => e.TotalPoints)
                .HasMaxLength(50)
                .IsUnicode(false);

            entity.HasOne(d => d.PidNavigation).WithMany(p => p.Assessments)
                .HasForeignKey(d => d.Pid)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_assessment_Patient");
        });

        modelBuilder.Entity<Attachment>(entity =>
        {
            entity.Property(e => e.AttachmentId).HasColumnName("AttachmentID");
            entity.Property(e => e.AttachmentName)
                .HasMaxLength(500)
                .IsUnicode(false);
            entity.Property(e => e.CreatedDt).HasColumnType("datetime");
            entity.Property(e => e.DoctorId).HasColumnName("DoctorID");
            entity.Property(e => e.ModifiedDt).HasColumnType("datetime");
            entity.Property(e => e.PatientId).HasColumnName("PatientID");
            entity.Property(e => e.Section)
                .HasMaxLength(1000)
                .IsUnicode(false);
        });

        modelBuilder.Entity<ChiefComplaint>(entity =>
        {
            entity.HasKey(e => e.CheifCompliantId);

            entity.ToTable("ChiefComplaint");

            entity.Property(e => e.CheifCompliantId).HasColumnName("CheifCompliantID");
            entity.Property(e => e.AtDuration).HasColumnName("AT_Duration");
            entity.Property(e => e.AtFrequency).HasColumnName("AT_Frequency");
            entity.Property(e => e.AtNocturnal)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("AT_Nocturnal");
            entity.Property(e => e.AtPostural)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("AT_Postural");
            entity.Property(e => e.CreatedDt).HasColumnType("datetime");
            entity.Property(e => e.DoctorId).HasColumnName("DoctorID");
            entity.Property(e => e.HbDuration).HasColumnName("HB_Duration");
            entity.Property(e => e.HbFrequency).HasColumnName("HB_Frequency");
            entity.Property(e => e.HbNocturnal)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("HB_Nocturnal");
            entity.Property(e => e.HbPostural)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("HB_Postural");
            entity.Property(e => e.ModifiedDt).HasColumnType("datetime");
            entity.Property(e => e.PatientId).HasColumnName("PatientID");
            entity.Property(e => e.RDuration).HasColumnName("R_Duration");
            entity.Property(e => e.RFrequency).HasColumnName("R_Frequency");
            entity.Property(e => e.RNocturnal)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("R_Nocturnal");
            entity.Property(e => e.RPostural)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("R_Postural");
            entity.Property(e => e.RpDuration).HasColumnName("RP_Duration");
            entity.Property(e => e.RpFrequency).HasColumnName("RP_Frequency");
            entity.Property(e => e.RpNocturnal)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("RP_Nocturnal");
            entity.Property(e => e.RpPostural)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("RP_Postural");

            entity.HasOne(d => d.Patient).WithMany(p => p.ChiefComplaints)
                .HasForeignKey(d => d.PatientId)
                .HasConstraintName("FK_ChiefComplaint_Patient");
        });

        modelBuilder.Entity<City>(entity =>
        {
            entity.ToTable("cities");

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("id");
            entity.Property(e => e.CountryCode)
                .HasMaxLength(50)
                .HasColumnName("country_code");
            entity.Property(e => e.CountryId).HasColumnName("country_id");
            entity.Property(e => e.CountryName)
                .HasMaxLength(50)
                .HasColumnName("country_name");
            entity.Property(e => e.CreatedDt).HasColumnType("datetime");
            entity.Property(e => e.Latitude).HasColumnName("latitude");
            entity.Property(e => e.Longitude).HasColumnName("longitude");
            entity.Property(e => e.ModifiedDt).HasColumnType("datetime");
            entity.Property(e => e.Name)
                .HasMaxLength(100)
                .HasColumnName("name");
            entity.Property(e => e.StateCode)
                .HasMaxLength(50)
                .HasColumnName("state_code");
            entity.Property(e => e.StateId).HasColumnName("state_id");
            entity.Property(e => e.StateName)
                .HasMaxLength(50)
                .HasColumnName("state_name");
            entity.Property(e => e.Town)
                .HasMaxLength(250)
                .IsUnicode(false);
            entity.Property(e => e.WikiDataId)
                .HasColumnType("money")
                .HasColumnName("wikiDataId");
        });

        modelBuilder.Entity<Comorbidity>(entity =>
        {
            entity.HasKey(e => e.ComorbiditiesId);

            entity.Property(e => e.ComorbiditiesId).HasColumnName("ComorbiditiesID");
            entity.Property(e => e.APresent)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("A_Present");
            entity.Property(e => e.ARemark)
                .HasMaxLength(250)
                .IsUnicode(false)
                .HasColumnName("A_Remark");
            entity.Property(e => e.BdPresent)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("BD_Present");
            entity.Property(e => e.BdRemark)
                .HasMaxLength(250)
                .IsUnicode(false)
                .HasColumnName("BD_Remark");
            entity.Property(e => e.CPresent)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("C_Present");
            entity.Property(e => e.CRemark)
                .HasMaxLength(250)
                .IsUnicode(false)
                .HasColumnName("C_Remark");
            entity.Property(e => e.CdPresent)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("CD_Present");
            entity.Property(e => e.CdRemark)
                .HasMaxLength(250)
                .IsUnicode(false)
                .HasColumnName("CD_Remark");
            entity.Property(e => e.CkdPresent)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("CKD_Present");
            entity.Property(e => e.CkdRemark)
                .HasMaxLength(250)
                .IsUnicode(false)
                .HasColumnName("CKD_Remark");
            entity.Property(e => e.CldPresent)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("CLD_Present");
            entity.Property(e => e.CldRemark)
                .HasMaxLength(250)
                .IsUnicode(false)
                .HasColumnName("CLD_Remark");
            entity.Property(e => e.CmoPresent)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("CMO_Present");
            entity.Property(e => e.CmoRemark)
                .HasMaxLength(250)
                .IsUnicode(false)
                .HasColumnName("CMO_Remark");
            entity.Property(e => e.CreatedDt).HasColumnType("datetime");
            entity.Property(e => e.DbPresent)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("DB_Present");
            entity.Property(e => e.DbRemark)
                .HasMaxLength(250)
                .IsUnicode(false)
                .HasColumnName("DB_Remark");
            entity.Property(e => e.DdPresent)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("DD_Present");
            entity.Property(e => e.DdRemark)
                .HasMaxLength(250)
                .IsUnicode(false)
                .HasColumnName("DD_Remark");
            entity.Property(e => e.DoctorId).HasColumnName("DoctorID");
            entity.Property(e => e.HPresent)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("H_Present");
            entity.Property(e => e.HRemark)
                .HasMaxLength(250)
                .IsUnicode(false)
                .HasColumnName("H_Remark");
            entity.Property(e => e.HtPresent)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("HT_Present");
            entity.Property(e => e.HtRemark)
                .HasMaxLength(250)
                .IsUnicode(false)
                .HasColumnName("HT_Remark");
            entity.Property(e => e.HtdPresent)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("HTD_Present");
            entity.Property(e => e.HtdRemark)
                .HasMaxLength(250)
                .IsUnicode(false)
                .HasColumnName("HTD_Remark");
            entity.Property(e => e.ModifiedDt).HasColumnType("datetime");
            entity.Property(e => e.NdPresent)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("ND_Present");
            entity.Property(e => e.NdRemark)
                .HasMaxLength(250)
                .IsUnicode(false)
                .HasColumnName("ND_Remark");
            entity.Property(e => e.OPresent)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("O_Present");
            entity.Property(e => e.ORemark)
                .HasMaxLength(250)
                .IsUnicode(false)
                .HasColumnName("O_Remark");
            entity.Property(e => e.PatientId).HasColumnName("PatientID");
            entity.Property(e => e.RaPresent)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("RA_Present");
            entity.Property(e => e.RaRemark)
                .HasMaxLength(250)
                .IsUnicode(false)
                .HasColumnName("RA_Remark");
            entity.Property(e => e.SsPresent)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("SS_Present");
            entity.Property(e => e.SsRemark)
                .HasMaxLength(250)
                .IsUnicode(false)
                .HasColumnName("SS_Remark");

            entity.HasOne(d => d.Patient).WithMany(p => p.Comorbidities)
                .HasForeignKey(d => d.PatientId)
                .HasConstraintName("FK_Comorbidities_Patient");
        });

        modelBuilder.Entity<Country>(entity =>
        {
            entity.ToTable("countries");

            entity.Property(e => e.Id)
                .ValueGeneratedOnAdd()
                .HasColumnName("id");
            entity.Property(e => e.Capital)
                .HasMaxLength(50)
                .HasColumnName("capital");
            entity.Property(e => e.CreatedDt).HasColumnType("datetime");
            entity.Property(e => e.Currency)
                .HasMaxLength(50)
                .HasColumnName("currency");
            entity.Property(e => e.CurrencyName)
                .HasMaxLength(50)
                .HasColumnName("currency_name");
            entity.Property(e => e.CurrencySymbol)
                .HasMaxLength(50)
                .HasColumnName("currency_symbol");
            entity.Property(e => e.Emoji)
                .HasMaxLength(50)
                .HasColumnName("emoji");
            entity.Property(e => e.EmojiU)
                .HasMaxLength(50)
                .HasColumnName("emojiU");
            entity.Property(e => e.Iso2)
                .HasMaxLength(50)
                .HasColumnName("iso2");
            entity.Property(e => e.Iso3)
                .HasMaxLength(50)
                .HasColumnName("iso3");
            entity.Property(e => e.Latitude).HasColumnName("latitude");
            entity.Property(e => e.Longitude).HasColumnName("longitude");
            entity.Property(e => e.ModifiedDt).HasColumnType("datetime");
            entity.Property(e => e.Name)
                .HasMaxLength(50)
                .HasColumnName("name");
            entity.Property(e => e.Nationality)
                .HasMaxLength(50)
                .HasColumnName("nationality");
            entity.Property(e => e.Native)
                .HasMaxLength(100)
                .HasColumnName("native");
            entity.Property(e => e.NumericCode).HasColumnName("numeric_code");
            entity.Property(e => e.Phonecode).HasColumnName("phonecode");
            entity.Property(e => e.Region)
                .HasMaxLength(50)
                .HasColumnName("region");
            entity.Property(e => e.RegionId).HasColumnName("region_id");
            entity.Property(e => e.Subregion)
                .HasMaxLength(50)
                .HasColumnName("subregion");
            entity.Property(e => e.SubregionId).HasColumnName("subregion_id");
            entity.Property(e => e.Timezones).HasColumnName("timezones");
            entity.Property(e => e.Tld)
                .HasMaxLength(50)
                .HasColumnName("tld");
        });

        modelBuilder.Entity<CurrentMedication>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_CurrentMedication_Id");

            entity.ToTable("CurrentMedication");

            entity.Property(e => e.AntiplateletDose)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("Antiplatelet_Dose");
            entity.Property(e => e.AntiplateletFrequency)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("Antiplatelet_Frequency");
            entity.Property(e => e.AntiplateletMolecule)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("Antiplatelet_Molecule");
            entity.Property(e => e.BisphosphonatesDose)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("Bisphosphonates_Dose");
            entity.Property(e => e.BisphosphonatesFrequency)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("Bisphosphonates_Frequency");
            entity.Property(e => e.BisphosphonatesMolecule)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("Bisphosphonates_Molecule");
            entity.Property(e => e.CreatedDt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.ModifiedDt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.NsaidsDose)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("NSAIDs_Dose");
            entity.Property(e => e.NsaidsFrequency)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("NSAIDs_Frequency");
            entity.Property(e => e.NsaidsMolecule)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("NSAIDs_Molecule");
            entity.Property(e => e.OthersDose)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("Others_Dose");
            entity.Property(e => e.OthersFrequency)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("Others_Frequency");
            entity.Property(e => e.OthersMolecule)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("Others_Molecule");
            entity.Property(e => e.SteroidsDose)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("Steroids_Dose");
            entity.Property(e => e.SteroidsFrequency)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("Steroids_Frequency");
            entity.Property(e => e.SteroidsMolecule)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("Steroids_Molecule");

            entity.HasOne(d => d.Patient).WithMany(p => p.CurrentMedications)
                .HasForeignKey(d => d.PatientId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_CurrentMedication_Patient");
        });

        modelBuilder.Entity<Diagnosis>(entity =>
        {
            entity.ToTable("Diagnosis");

            entity.Property(e => e.DiagnosisId).HasColumnName("DiagnosisID");
            entity.Property(e => e.CreatedDt).HasColumnType("datetime");
            entity.Property(e => e.DoctorId).HasColumnName("DoctorID");
            entity.Property(e => e.Gerdtype)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("GERDType");
            entity.Property(e => e.GredNoOfYear).HasColumnName("GRED_NoOfYear");
            entity.Property(e => e.KnownCaseOfGerd).HasColumnName("KnownCaseOfGERD");
            entity.Property(e => e.ModifiedDt).HasColumnType("datetime");
            entity.Property(e => e.PatientId).HasColumnName("PatientID");
            entity.Property(e => e.RefractoryToPpi).HasColumnName("RefractoryToPPI");

            entity.HasOne(d => d.Patient).WithMany(p => p.Diagnoses)
                .HasForeignKey(d => d.PatientId)
                .HasConstraintName("FK_Diagnosis_Patient");
        });

        modelBuilder.Entity<Doctor>(entity =>
        {
            entity.ToTable("Doctor");

            entity.Property(e => e.DoctorId).HasColumnName("DoctorID");
            entity.Property(e => e.CreatedDt).HasColumnType("datetime");
            entity.Property(e => e.Email)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.EnterCodeNo)
                .HasMaxLength(250)
                .IsUnicode(false)
                .HasColumnName("EnterCodeNO");
            entity.Property(e => e.HospitalName)
                .HasMaxLength(250)
                .IsUnicode(false);
            entity.Property(e => e.Mcicode)
                .HasMaxLength(250)
                .IsUnicode(false)
                .HasColumnName("MCICode");
            entity.Property(e => e.ModifiedDt).HasColumnType("datetime");
            entity.Property(e => e.Name)
                .HasMaxLength(250)
                .IsUnicode(false);
            entity.Property(e => e.Password)
                .HasMaxLength(250)
                .IsUnicode(false);
            entity.Property(e => e.PhoneNo)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("PhoneNO");
            entity.Property(e => e.PlaceOfPractice).HasMaxLength(250);
            entity.Property(e => e.Status)
                .HasMaxLength(50)
                .IsUnicode(false);
        });

        modelBuilder.Entity<DoctorLog>(entity =>
        {
            entity.HasKey(e => e.DoctorlogId).HasName("PK__DoctorLo__2E8573BAC96E8946");

            entity.Property(e => e.DoctorlogId).HasColumnName("DoctorlogID");
            entity.Property(e => e.CreatedDt).HasColumnType("datetime");
            entity.Property(e => e.DoctorId).HasColumnName("DoctorID");
            entity.Property(e => e.LoginTime).HasColumnType("datetime");
            entity.Property(e => e.LogoutTime).HasColumnType("datetime");
            entity.Property(e => e.ModifiedDt).HasColumnType("datetime");
            entity.Property(e => e.Token).HasMaxLength(2000);
        });

        modelBuilder.Entity<Exercise>(entity =>
        {
            entity.ToTable("Exercise");

            entity.Property(e => e.ExerciseId).HasColumnName("ExerciseID");
            entity.Property(e => e.AerobicsDuration)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("Aerobics_Duration");
            entity.Property(e => e.AerobicsFrequency)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("Aerobics_Frequency");
            entity.Property(e => e.AerobicsOption)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("Aerobics_Option");
            entity.Property(e => e.CreatedDt).HasColumnType("datetime");
            entity.Property(e => e.DoctorId).HasColumnName("DoctorID");
            entity.Property(e => e.GymDuration)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("Gym_Duration");
            entity.Property(e => e.GymFrequency)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("Gym_Frequency");
            entity.Property(e => e.GymOption)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("Gym_Option");
            entity.Property(e => e.JoggingDuration)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("Jogging_Duration");
            entity.Property(e => e.JoggingFrequency)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("Jogging_Frequency");
            entity.Property(e => e.JoggingOption)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("Jogging_Option");
            entity.Property(e => e.ModifiedDt).HasColumnType("datetime");
            entity.Property(e => e.OthersDuration)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("Others_Duration");
            entity.Property(e => e.OthersFrequency)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("Others_Frequency");
            entity.Property(e => e.OthersOption)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("Others_Option");
            entity.Property(e => e.PatientId).HasColumnName("PatientID");
            entity.Property(e => e.Status)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.WalkingDuration)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("Walking_Duration");
            entity.Property(e => e.WalkingFrequency)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("Walking_Frequency");
            entity.Property(e => e.WalkingOption)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("Walking_Option");
            entity.Property(e => e.YogaDuration)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("Yoga_Duration");
            entity.Property(e => e.YogaFrequency)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("Yoga_Frequency");
            entity.Property(e => e.YogaOption)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("Yoga_Option");
            entity.Property(e => e.ZumbaDuration)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("Zumba_Duration");
            entity.Property(e => e.ZumbaFrequency)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("Zumba_Frequency");
            entity.Property(e => e.ZumbaOption)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("Zumba_Option");
        });

        modelBuilder.Entity<FamilyHistory>(entity =>
        {
            entity.ToTable("FamilyHistory");

            entity.Property(e => e.FamilyHistoryId).HasColumnName("FamilyHistoryID");
            entity.Property(e => e.CreatedDt).HasColumnType("datetime");
            entity.Property(e => e.DoctorId).HasColumnName("DoctorID");
            entity.Property(e => e.FhEgc)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("FH_EGC");
            entity.Property(e => e.FhEgcremark)
                .HasMaxLength(250)
                .IsUnicode(false)
                .HasColumnName("FH_EGCRemark");
            entity.Property(e => e.FhGred)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("FH_GRED");
            entity.Property(e => e.FhRemark)
                .HasMaxLength(250)
                .IsUnicode(false)
                .HasColumnName("FH_Remark");
            entity.Property(e => e.GHPpi)
                .HasMaxLength(250)
                .IsUnicode(false)
                .HasColumnName("gH_PPI");
            entity.Property(e => e.ModifiedDt).HasColumnType("datetime");
            entity.Property(e => e.PatientId).HasColumnName("PatientID");

            entity.HasOne(d => d.Patient).WithMany(p => p.FamilyHistories)
                .HasForeignKey(d => d.PatientId)
                .HasConstraintName("FK_FamilyHistory_Patient");
        });

        modelBuilder.Entity<Gadget>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Gadget__3214EC07BE3CA265");

            entity.ToTable("Gadget");

            entity.Property(e => e.ComputerFrequency)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.CreatedBy)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.JobType)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.ModifiedAt).HasColumnType("datetime");
            entity.Property(e => e.SmartphoneFrequency)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.WorkingHours)
                .HasMaxLength(50)
                .IsUnicode(false);

            entity.HasOne(d => d.Patient).WithMany(p => p.Gadgets)
                .HasForeignKey(d => d.PatientId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Gadget_Patient");
        });

        modelBuilder.Entity<Gerdhistory>(entity =>
        {
            entity.HasKey(e => e.Ghid);

            entity.ToTable("GERDHistory");

            entity.Property(e => e.Ghid).HasColumnName("GHID");
            entity.Property(e => e.CreatedDt).HasColumnType("datetime");
            entity.Property(e => e.DoctorId).HasColumnName("DoctorID");
            entity.Property(e => e.EndoscopyAttement)
                .HasMaxLength(250)
                .IsUnicode(false);
            entity.Property(e => e.EndoscopyDate).HasColumnType("datetime");
            entity.Property(e => e.EndoscopyRemark)
                .HasMaxLength(1000)
                .IsUnicode(false);
            entity.Property(e => e.GsBariatricSurgery).HasColumnName("GS_BariatricSurgery");
            entity.Property(e => e.GsBsremark)
                .HasMaxLength(1000)
                .IsUnicode(false)
                .HasColumnName("GS_BSRemark");
            entity.Property(e => e.GsFsremark)
                .HasMaxLength(1000)
                .IsUnicode(false)
                .HasColumnName("GS_FSRemark");
            entity.Property(e => e.GsFundoplicationSurgery).HasColumnName("GS_FundoplicationSurgery");
            entity.Property(e => e.GsGastricPoemsurgery).HasColumnName("GS_GastricPOEMSurgery");
            entity.Property(e => e.GsGastrojejunostomy).HasColumnName("GS_Gastrojejunostomy");
            entity.Property(e => e.GsGjremark)
                .HasMaxLength(1000)
                .IsUnicode(false)
                .HasColumnName("GS_GJRemark");
            entity.Property(e => e.GsGpsremark)
                .HasMaxLength(1000)
                .IsUnicode(false)
                .HasColumnName("GS_GPSRemark");
            entity.Property(e => e.GsOther).HasColumnName("GS_Other");
            entity.Property(e => e.GsOtherRemark)
                .HasMaxLength(1000)
                .IsUnicode(false)
                .HasColumnName("GS_OtherRemark");
            entity.Property(e => e.GsOtherText)
                .HasMaxLength(1000)
                .IsUnicode(false)
                .HasColumnName("gs_OtherText");
            entity.Property(e => e.HistoryofEndoscopy)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.HistoryofGs).HasColumnName("HistoryofGS");
            entity.Property(e => e.ModifiedDt).HasColumnType("datetime");
            entity.Property(e => e.PatientId).HasColumnName("PatientID");
            entity.Property(e => e.UsageOfPpi)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("UsageOfPPI");

            entity.HasOne(d => d.Patient).WithMany(p => p.Gerdhistories)
                .HasForeignKey(d => d.PatientId)
                .HasConstraintName("FK_GERDHistory_Patient");
        });

        modelBuilder.Entity<History>(entity =>
        {
            entity.HasKey(e => e.HistoryId).HasName("PK__History__4D7B4ADD65746B54");

            entity.ToTable("History");

            entity.Property(e => e.HistoryId).HasColumnName("HistoryID");
            entity.Property(e => e.CreatedDate).HasColumnType("datetime");
            entity.Property(e => e.DietNonVegetarian).HasColumnName("Diet_NonVegetarian");
            entity.Property(e => e.DietVegetarian).HasColumnName("Diet_Vegetarian");
            entity.Property(e => e.DoctorId).HasColumnName("DoctorID");
            entity.Property(e => e.ModifiedDate).HasColumnType("datetime");
            entity.Property(e => e.PastHistory).HasColumnName("Past_History");
            entity.Property(e => e.PatientId).HasColumnName("PatientID");

            entity.HasOne(d => d.Patient).WithMany(p => p.Histories)
                .HasForeignKey(d => d.PatientId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_History_Patient");
        });

        modelBuilder.Entity<Management>(entity =>
        {
            entity.ToTable("Management");

            entity.Property(e => e.ManagementId).HasColumnName("ManagementID");
            entity.Property(e => e.AlginateDose)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("Alginate_Dose");
            entity.Property(e => e.AlginateFrequency)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("Alginate_Frequency");
            entity.Property(e => e.AlginateMedicationName)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("Alginate_Medication_Name");
            entity.Property(e => e.CreatedDt).HasColumnType("datetime");
            entity.Property(e => e.H2blockersCDose)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("H2BlockersC_Dose");
            entity.Property(e => e.H2blockersCFrequency)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("H2BlockersC_Frequency");
            entity.Property(e => e.H2blockersCMedicationName)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("H2BlockersC_Medication_Name");
            entity.Property(e => e.H2blockersDose)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("H2Blockers_Dose");
            entity.Property(e => e.H2blockersFrequency)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("H2Blockers_Frequency");
            entity.Property(e => e.H2blockersMedicationName)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("H2Blockers_Medication_Name");
            entity.Property(e => e.ModifiedDt).HasColumnType("datetime");
            entity.Property(e => e.OthersDose)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("others_Dose");
            entity.Property(e => e.OthersFrequency)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("others_Frequency");
            entity.Property(e => e.OthersMedicationName)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("others_Medication_Name");
            entity.Property(e => e.PatientId).HasColumnName("PatientID");
            entity.Property(e => e.PcabDose)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("PCAB_Dose");
            entity.Property(e => e.PcabFrequency)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("PCAB_Frequency");
            entity.Property(e => e.PcabMedicationName)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("PCAB_Medication_Name");
            entity.Property(e => e.PpiDose)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("PPI_Dose");
            entity.Property(e => e.PpiFrequency)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("PPI_Frequency");
            entity.Property(e => e.PpiMedicationName)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("PPI_Medication_Name");
            entity.Property(e => e.ProkineticsDose)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("Prokinetics_Dose");
            entity.Property(e => e.ProkineticsFrequency)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("Prokinetics_Frequency");
            entity.Property(e => e.ProkineticsMedicationName)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("Prokinetics_Medication_Name");
            entity.Property(e => e.SucralfateDose)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("Sucralfate_Dose");
            entity.Property(e => e.SucralfateFrequency)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("Sucralfate_Frequency");
            entity.Property(e => e.SucralfateMedicationName)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("Sucralfate_Medication_Name");

            entity.HasOne(d => d.Patient).WithMany(p => p.Managements)
                .HasForeignKey(d => d.PatientId)
                .HasConstraintName("FK_Management_Patient");
        });

        modelBuilder.Entity<MedicalExamination>(entity =>
        {
            entity.HasKey(e => e.Meid).HasName("PK__MedicalE__1A36DA7A20FF0C57");

            entity.ToTable("MedicalExamination");

            entity.Property(e => e.Meid).HasColumnName("MEID");
            entity.Property(e => e.CreatedDt).HasColumnType("datetime");
            entity.Property(e => e.DoctorId).HasColumnName("DoctorID");
            entity.Property(e => e.ModifiedDt).HasColumnType("datetime");
            entity.Property(e => e.OthersAbNormalCs)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("OthersAbNormal_CS");
            entity.Property(e => e.OthersAbNormalNcs)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("OthersAbNormal_NCS");
            entity.Property(e => e.OthersAbNormalRemark)
                .HasMaxLength(1000)
                .IsUnicode(false);
            entity.Property(e => e.OthersNormal)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.PaeFindings)
                .HasMaxLength(1000)
                .IsUnicode(false)
                .HasColumnName("PAE_Findings");
            entity.Property(e => e.PatientId).HasColumnName("PatientID");
            entity.Property(e => e.PeBmi)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("PE_BMI");
            entity.Property(e => e.PeHeight)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("PE_Height");
            entity.Property(e => e.PeWeight)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("PE_Weight");
            entity.Property(e => e.SeGaabNormalCs)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("SE_GAAbNormal_CS");
            entity.Property(e => e.SeGaabNormalNcs)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("SE_GAAbNormal_NCS");
            entity.Property(e => e.SeGaabNormalRemark)
                .HasMaxLength(1000)
                .IsUnicode(false)
                .HasColumnName("SE_GAAbNormalRemark");
            entity.Property(e => e.SeGanormal)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("SE_GANormal");
            entity.Property(e => e.SeRsabNormalCs)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("SE_RSAbNormal_CS");
            entity.Property(e => e.SeRsabNormalNcs)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("SE_RSAbNormal_NCS");
            entity.Property(e => e.SeRsabNormalRemark)
                .HasMaxLength(1000)
                .IsUnicode(false)
                .HasColumnName("SE_RSAbNormalRemark");
            entity.Property(e => e.SeRsnormal)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("SE_RSNormal");

            entity.HasOne(d => d.Patient).WithMany(p => p.MedicalExaminations)
                .HasForeignKey(d => d.PatientId)
                .HasConstraintName("FK_MedicalExamination_Patient");
        });

        modelBuilder.Entity<Medication>(entity =>
        {
            entity.ToTable("Medication");

            entity.Property(e => e.MedicationId).HasColumnName("MedicationID");
            entity.Property(e => e.CreatedDt).HasColumnType("datetime");
            entity.Property(e => e.Dose)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Frequency)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Ghid).HasColumnName("GHID");
            entity.Property(e => e.MedicationName)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.ModifiedDt).HasColumnType("datetime");
            entity.Property(e => e.Molecule)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.PatientId).HasColumnName("PatientID");

            entity.HasOne(d => d.Patient).WithMany(p => p.Medications)
                .HasForeignKey(d => d.PatientId)
                .HasConstraintName("FK_Medication_Patient");
        });

        modelBuilder.Entity<Patient>(entity =>
        {
            entity.ToTable("Patient");

            entity.Property(e => e.PatientId).HasColumnName("PatientID");
            entity.Property(e => e.CreatedDt).HasColumnType("datetime");
            entity.Property(e => e.Date).HasColumnType("datetime");
            entity.Property(e => e.Diet)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.DoctorId).HasColumnName("DoctorID");
            entity.Property(e => e.Education)
                .HasMaxLength(250)
                .IsUnicode(false);
            entity.Property(e => e.FamilyIncome)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Gender)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Initial)
                .HasMaxLength(250)
                .IsUnicode(false);
            entity.Property(e => e.ModifiedDt).HasColumnType("datetime");
            entity.Property(e => e.Occupation)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.PastHistory)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.PlaceType)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.SocioeconomicStatus)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.SubjectNo)
                .HasMaxLength(50)
                .IsUnicode(false);
        });

        modelBuilder.Entity<PatientHistory>(entity =>
        {
            entity.HasKey(e => e.PatientHistoryId).HasName("PK_PatientHistory_New");

            entity.ToTable("PatientHistory");

            entity.Property(e => e.PatientHistoryId).HasColumnName("PatientHistoryID");
            entity.Property(e => e.AdDuration)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("AD_Duration");
            entity.Property(e => e.AdFrequency)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("AD_Frequency");
            entity.Property(e => e.AdIntake)
                .HasMaxLength(50)
                .HasColumnName("AD_Intake");
            entity.Property(e => e.AdQuantity)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("AD_Quantity");
            entity.Property(e => e.AerobicsDuration)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("Aerobics_Duration");
            entity.Property(e => e.AerobicsFrequency)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("Aerobics_Frequency");
            entity.Property(e => e.AerobicsIntake)
                .HasMaxLength(10)
                .HasColumnName("Aerobics_Intake");
            entity.Property(e => e.AhDuration)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("AH_Duration");
            entity.Property(e => e.AhFrequency)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("AH_Frequency");
            entity.Property(e => e.AhIntake)
                .HasMaxLength(50)
                .HasColumnName("AH_Intake");
            entity.Property(e => e.AhQuantity)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("AH_Quantity");
            entity.Property(e => e.CfDuration)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("CF_Duration");
            entity.Property(e => e.CfFrequency)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("CF_Frequency");
            entity.Property(e => e.CfIntake)
                .HasMaxLength(50)
                .HasColumnName("CF_Intake");
            entity.Property(e => e.CfQuantity)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("CF_Quantity");
            entity.Property(e => e.CreatedDt).HasColumnType("datetime");
            entity.Property(e => e.CsDuration)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("CS_Duration");
            entity.Property(e => e.CsFrequency)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("CS_Frequency");
            entity.Property(e => e.CsIntake)
                .HasMaxLength(50)
                .HasColumnName("CS_Intake");
            entity.Property(e => e.CsQuantity)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("CS_Quantity");
            entity.Property(e => e.Duration)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.ExerciseIntake)
                .HasMaxLength(10)
                .HasColumnName("Exercise_Intake");
            entity.Property(e => e.GFrequency)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("G_Frequency");
            entity.Property(e => e.GName)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("G_Name");
            entity.Property(e => e.GUsage)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("G_Usage");
            entity.Property(e => e.GYearOfUsage)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("G_YearOfUsage");
            entity.Property(e => e.GymDuration)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("Gym_Duration");
            entity.Property(e => e.GymFrequency)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("Gym_Frequency");
            entity.Property(e => e.GymIntake)
                .HasMaxLength(10)
                .HasColumnName("Gym_Intake");
            entity.Property(e => e.JobType)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.JoggingDuration)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("Jogging_Duration");
            entity.Property(e => e.JoggingFrequency)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("Jogging_Frequency");
            entity.Property(e => e.JoggingIntake)
                .HasMaxLength(10)
                .HasColumnName("Jogging_Intake");
            entity.Property(e => e.ModifiedDt).HasColumnType("datetime");
            entity.Property(e => e.OthersExerciseDuration)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("OthersExercise_Duration");
            entity.Property(e => e.OthersExerciseFrequency)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("OthersExercise_Frequency");
            entity.Property(e => e.OthersExerciseIntake)
                .HasMaxLength(10)
                .HasColumnName("OthersExercise_Intake");
            entity.Property(e => e.PastHistory).HasColumnName("Past_History");
            entity.Property(e => e.PatientId).HasColumnName("PatientID");
            entity.Property(e => e.SDuration)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("S_Duration");
            entity.Property(e => e.SFrequency)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("S_Frequency");
            entity.Property(e => e.SIntake)
                .HasMaxLength(50)
                .HasColumnName("S_Intake");
            entity.Property(e => e.SQuantity)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("S_Quantity");
            entity.Property(e => e.SfDuration)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("SF_Duration");
            entity.Property(e => e.SfFrequency)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("SF_Frequency");
            entity.Property(e => e.SfIntake)
                .HasMaxLength(50)
                .HasColumnName("SF_Intake");
            entity.Property(e => e.SfQuantity)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("SF_Quantity");
            entity.Property(e => e.SleepApneaDuration)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("SleepApnea_Duration");
            entity.Property(e => e.SleepApneaFrequency)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("SleepApnea_Frequency");
            entity.Property(e => e.SleepApneaIntake)
                .HasMaxLength(10)
                .HasColumnName("SleepApnea_Intake");
            entity.Property(e => e.TDuration)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("T_Duration");
            entity.Property(e => e.TFrequency)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("T_Frequency");
            entity.Property(e => e.TIntake)
                .HasMaxLength(50)
                .HasColumnName("T_Intake");
            entity.Property(e => e.TQuantity)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("T_Quantity");
            entity.Property(e => e.TbDuration)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("TB_Duration");
            entity.Property(e => e.TbFrequency)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("TB_Frequency");
            entity.Property(e => e.TbIntake)
                .HasMaxLength(50)
                .HasColumnName("TB_Intake");
            entity.Property(e => e.TbQuantity)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("TB_Quantity");
            entity.Property(e => e.WalkingDuration)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("Walking_Duration");
            entity.Property(e => e.WalkingFrequency)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("Walking_Frequency");
            entity.Property(e => e.WalkingIntake)
                .HasMaxLength(10)
                .HasColumnName("Walking_Intake");
            entity.Property(e => e.WorkingHours)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.YogaDuration)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("Yoga_Duration");
            entity.Property(e => e.YogaFrequency)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("Yoga_Frequency");
            entity.Property(e => e.YogaIntake)
                .HasMaxLength(10)
                .HasColumnName("Yoga_Intake");
            entity.Property(e => e.ZumbaDuration)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("Zumba_Duration");
            entity.Property(e => e.ZumbaFrequency)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("Zumba_Frequency");
            entity.Property(e => e.ZumbaIntake)
                .HasMaxLength(10)
                .HasColumnName("Zumba_Intake");
        });

        modelBuilder.Entity<PatientHistoryBackup>(entity =>
        {
            entity.HasKey(e => e.PatientHistoryId).HasName("PK_PatientHistory");

            entity.ToTable("PatientHistoryBackup");

            entity.Property(e => e.PatientHistoryId)
                .ValueGeneratedNever()
                .HasColumnName("PatientHistoryID");
            entity.Property(e => e.AdDuration)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("AD_Duration");
            entity.Property(e => e.AdFrequency)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("AD_Frequency");
            entity.Property(e => e.AdIntake)
                .HasMaxLength(50)
                .HasColumnName("AD_Intake");
            entity.Property(e => e.AdQuantity)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("AD_Quantity");
            entity.Property(e => e.AhDuration)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("AH_Duration");
            entity.Property(e => e.AhFrequency)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("AH_Frequency");
            entity.Property(e => e.AhIntake)
                .HasMaxLength(50)
                .HasColumnName("AH_Intake");
            entity.Property(e => e.AhQuantity)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("AH_Quantity");
            entity.Property(e => e.CfDuration)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("CF_Duration");
            entity.Property(e => e.CfFrequency)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("CF_Frequency");
            entity.Property(e => e.CfIntake)
                .HasMaxLength(50)
                .HasColumnName("CF_Intake");
            entity.Property(e => e.CfQuantity)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("CF_Quantity");
            entity.Property(e => e.CreatedDt).HasColumnType("datetime");
            entity.Property(e => e.CsDuration)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("CS_Duration");
            entity.Property(e => e.CsFrequency)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("CS_Frequency");
            entity.Property(e => e.CsIntake)
                .HasMaxLength(50)
                .HasColumnName("CS_Intake");
            entity.Property(e => e.CsQuantity)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("CS_Quantity");
            entity.Property(e => e.Duration)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.GFrequency)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("G_Frequency");
            entity.Property(e => e.GName)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("G_Name");
            entity.Property(e => e.GUsage)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("G_Usage");
            entity.Property(e => e.GYearOfUsage)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("G_YearOfUsage");
            entity.Property(e => e.JobType)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.ModifiedDt).HasColumnType("datetime");
            entity.Property(e => e.PastHistory).HasColumnName("Past_History");
            entity.Property(e => e.SDuration)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("S_Duration");
            entity.Property(e => e.SFrequency)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("S_Frequency");
            entity.Property(e => e.SIntake)
                .HasMaxLength(50)
                .HasColumnName("S_Intake");
            entity.Property(e => e.SQuantity)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("S_Quantity");
            entity.Property(e => e.SfDuration)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("SF_Duration");
            entity.Property(e => e.SfFrequency)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("SF_Frequency");
            entity.Property(e => e.SfIntake)
                .HasMaxLength(50)
                .HasColumnName("SF_Intake");
            entity.Property(e => e.SfQuantity)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("SF_Quantity");
            entity.Property(e => e.TDuration)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("T_Duration");
            entity.Property(e => e.TFrequency)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("T_Frequency");
            entity.Property(e => e.TIntake)
                .HasMaxLength(50)
                .HasColumnName("T_Intake");
            entity.Property(e => e.TQuantity)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("T_Quantity");
            entity.Property(e => e.TbDuration)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("TB_Duration");
            entity.Property(e => e.TbFrequency)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("TB_Frequency");
            entity.Property(e => e.TbIntake)
                .HasMaxLength(50)
                .HasColumnName("TB_Intake");
            entity.Property(e => e.TbQuantity)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("TB_Quantity");
            entity.Property(e => e.WorkingHours)
                .HasMaxLength(50)
                .IsUnicode(false);
        });

        modelBuilder.Entity<PersonalHistory>(entity =>
        {
            entity.HasKey(e => e.PersonalHistoryId).HasName("PK__Personal__1ED0A1ADA6AC77E4");

            entity.ToTable("PersonalHistory");

            entity.Property(e => e.AeratedDuration)
                .HasMaxLength(20)
                .IsUnicode(false);
            entity.Property(e => e.AeratedFrequency)
                .HasMaxLength(20)
                .IsUnicode(false);
            entity.Property(e => e.AeratedQuantity)
                .HasMaxLength(20)
                .IsUnicode(false);
            entity.Property(e => e.AlcoholDuration)
                .HasMaxLength(20)
                .IsUnicode(false);
            entity.Property(e => e.AlcoholFrequency)
                .HasMaxLength(20)
                .IsUnicode(false);
            entity.Property(e => e.AlcoholQuantity)
                .HasMaxLength(20)
                .IsUnicode(false);
            entity.Property(e => e.CoffeeDuration)
                .HasMaxLength(20)
                .IsUnicode(false);
            entity.Property(e => e.CoffeeFrequency)
                .HasMaxLength(20)
                .IsUnicode(false);
            entity.Property(e => e.CoffeeQuantity)
                .HasMaxLength(20)
                .IsUnicode(false);
            entity.Property(e => e.CreatedDt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.ModifiedDt).HasColumnType("datetime");
            entity.Property(e => e.SmokingDuration)
                .HasMaxLength(20)
                .IsUnicode(false);
            entity.Property(e => e.SmokingFrequency)
                .HasMaxLength(20)
                .IsUnicode(false);
            entity.Property(e => e.SmokingQuantity)
                .HasMaxLength(20)
                .IsUnicode(false);
            entity.Property(e => e.SpicyDuration)
                .HasMaxLength(20)
                .IsUnicode(false);
            entity.Property(e => e.SpicyFrequency)
                .HasMaxLength(20)
                .IsUnicode(false);
            entity.Property(e => e.SpicyQuantity)
                .HasMaxLength(20)
                .IsUnicode(false);
            entity.Property(e => e.SweetsDuration)
                .HasMaxLength(20)
                .IsUnicode(false);
            entity.Property(e => e.SweetsFrequency)
                .HasMaxLength(20)
                .IsUnicode(false);
            entity.Property(e => e.SweetsQuantity)
                .HasMaxLength(20)
                .IsUnicode(false);
            entity.Property(e => e.TeaDuration)
                .HasMaxLength(20)
                .IsUnicode(false);
            entity.Property(e => e.TeaFrequency)
                .HasMaxLength(20)
                .IsUnicode(false);
            entity.Property(e => e.TeaQuantity)
                .HasMaxLength(20)
                .IsUnicode(false);
            entity.Property(e => e.TobaccoDuration)
                .HasMaxLength(20)
                .IsUnicode(false);
            entity.Property(e => e.TobaccoFrequency)
                .HasMaxLength(20)
                .IsUnicode(false);
            entity.Property(e => e.TobaccoQuantity)
                .HasMaxLength(20)
                .IsUnicode(false);

            entity.HasOne(d => d.Patient).WithMany(p => p.PersonalHistories)
                .HasForeignKey(d => d.PatientId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_PersonalHistory_Patient");
        });

        modelBuilder.Entity<PtnTrack>(entity =>
        {
            entity.ToTable("PtnTrack");

            entity.Property(e => e.PtnTrackId).HasColumnName("PtnTrackID");
            entity.Property(e => e.CreatedDt).HasColumnType("datetime");
            entity.Property(e => e.DoctorId).HasColumnName("DoctorID");
            entity.Property(e => e.ModifiedDt).HasColumnType("datetime");
            entity.Property(e => e.PageRouter)
                .HasMaxLength(250)
                .IsUnicode(false);
            entity.Property(e => e.PatientId).HasColumnName("PatientID");
        });

        modelBuilder.Entity<Sleep>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Sleep__3214EC07AA1A65AD");

            entity.ToTable("Sleep");

            entity.Property(e => e.AerobicsDuration)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("aerobicsDuration");
            entity.Property(e => e.AerobicsFrequency)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("aerobicsFrequency");
            entity.Property(e => e.Aerobicsno)
                .HasMaxLength(3)
                .IsUnicode(false)
                .HasColumnName("aerobicsno");
            entity.Property(e => e.Aerobicsyes)
                .HasMaxLength(3)
                .IsUnicode(false)
                .HasColumnName("aerobicsyes");
            entity.Property(e => e.CreatedAt).HasColumnType("datetime");
            entity.Property(e => e.ExerciseIntakeno)
                .HasMaxLength(3)
                .IsUnicode(false)
                .HasColumnName("exerciseIntakeno");
            entity.Property(e => e.ExerciseIntakeyes)
                .HasMaxLength(3)
                .IsUnicode(false)
                .HasColumnName("exerciseIntakeyes");
            entity.Property(e => e.GymDuration)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("gymDuration");
            entity.Property(e => e.GymFrequency)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("gymFrequency");
            entity.Property(e => e.GymSelectedno)
                .HasMaxLength(3)
                .IsUnicode(false)
                .HasColumnName("gymSelectedno");
            entity.Property(e => e.GymSelectedyes)
                .HasMaxLength(3)
                .IsUnicode(false)
                .HasColumnName("gymSelectedyes");
            entity.Property(e => e.JoggingDuration)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("joggingDuration");
            entity.Property(e => e.JoggingFrequency)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("joggingFrequency");
            entity.Property(e => e.JoggingSelectedno)
                .HasMaxLength(3)
                .IsUnicode(false)
                .HasColumnName("joggingSelectedno");
            entity.Property(e => e.JoggingSelectedyes)
                .HasMaxLength(3)
                .IsUnicode(false)
                .HasColumnName("joggingSelectedyes");
            entity.Property(e => e.ModifiedDt).HasColumnType("datetime");
            entity.Property(e => e.OthersDuration)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("othersDuration");
            entity.Property(e => e.OthersFrequency)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("othersFrequency");
            entity.Property(e => e.OthersText)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("othersText");
            entity.Property(e => e.Othersno)
                .HasMaxLength(3)
                .IsUnicode(false)
                .HasColumnName("othersno");
            entity.Property(e => e.Othersyes)
                .HasMaxLength(3)
                .IsUnicode(false)
                .HasColumnName("othersyes");
            entity.Property(e => e.SleepApneaDuration)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("sleepApneaDuration");
            entity.Property(e => e.SleepApneaFrequency)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("sleepApneaFrequency");
            entity.Property(e => e.SleepApneano)
                .HasMaxLength(3)
                .IsUnicode(false)
                .HasColumnName("sleepApneano");
            entity.Property(e => e.SleepApneayes)
                .HasMaxLength(3)
                .IsUnicode(false)
                .HasColumnName("sleepApneayes");
            entity.Property(e => e.Stage).HasColumnName("stage");
            entity.Property(e => e.WalkingDuration)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("walkingDuration");
            entity.Property(e => e.WalkingFrequency)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("walkingFrequency");
            entity.Property(e => e.WalkingSelectedno)
                .HasMaxLength(3)
                .IsUnicode(false)
                .HasColumnName("walkingSelectedno");
            entity.Property(e => e.WalkingSelectedyes)
                .HasMaxLength(3)
                .IsUnicode(false)
                .HasColumnName("walkingSelectedyes");
            entity.Property(e => e.YogaDuration)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("yogaDuration");
            entity.Property(e => e.YogaFrequency)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("yogaFrequency");
            entity.Property(e => e.YogaSelectedno)
                .HasMaxLength(3)
                .IsUnicode(false)
                .HasColumnName("yogaSelectedno");
            entity.Property(e => e.YogaSelectedyes)
                .HasMaxLength(3)
                .IsUnicode(false)
                .HasColumnName("yogaSelectedyes");
            entity.Property(e => e.ZumbaDuration)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("zumbaDuration");
            entity.Property(e => e.ZumbaFrequency)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("zumbaFrequency");
            entity.Property(e => e.Zumbano)
                .HasMaxLength(3)
                .IsUnicode(false)
                .HasColumnName("zumbano");
            entity.Property(e => e.Zumbayes)
                .HasMaxLength(3)
                .IsUnicode(false)
                .HasColumnName("zumbayes");

            entity.HasOne(d => d.Patient).WithMany(p => p.Sleeps)
                .HasForeignKey(d => d.PatientId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Sleep_Patient");
        });

        modelBuilder.Entity<State>(entity =>
        {
            entity.ToTable("states");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.CountryCode)
                .HasMaxLength(50)
                .HasColumnName("country_code");
            entity.Property(e => e.CountryId).HasColumnName("country_id");
            entity.Property(e => e.CountryName)
                .HasMaxLength(50)
                .HasColumnName("country_name");
            entity.Property(e => e.CreatedDt).HasColumnType("datetime");
            entity.Property(e => e.Latitude).HasColumnName("latitude");
            entity.Property(e => e.Longitude).HasColumnName("longitude");
            entity.Property(e => e.ModifiedDt).HasColumnType("datetime");
            entity.Property(e => e.Name)
                .HasMaxLength(50)
                .HasColumnName("name");
            entity.Property(e => e.StateCode)
                .HasMaxLength(50)
                .HasColumnName("state_code");
            entity.Property(e => e.Type)
                .HasMaxLength(50)
                .HasColumnName("type");
        });

        modelBuilder.Entity<VwAbbre>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("vw_Abbre");

            entity.Property(e => e.Abbre).HasMaxLength(50);
            entity.Property(e => e.Desc).HasMaxLength(50);
            entity.Property(e => e.Id).HasColumnName("ID");
        });

        modelBuilder.Entity<VwAssessment>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("vw_Assessment");

            entity.Property(e => e.AcidRefluxSymptom)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.AssessmentId)
                .ValueGeneratedOnAdd()
                .HasColumnName("AssessmentID");
            entity.Property(e => e.BiopsyAttached).HasColumnName("Biopsy_Attached");
            entity.Property(e => e.BiopsyAttachement)
                .HasMaxLength(500)
                .IsUnicode(false)
                .HasColumnName("Biopsy_Attachement");
            entity.Property(e => e.BiopsyDate)
                .HasColumnType("datetime")
                .HasColumnName("Biopsy_Date");
            entity.Property(e => e.BiopsyRemark)
                .HasMaxLength(1000)
                .IsUnicode(false)
                .HasColumnName("Biopsy_Remark");
            entity.Property(e => e.CreatedDt).HasColumnType("datetime");
            entity.Property(e => e.Dysmotity)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.EeAgremarks)
                .HasMaxLength(1000)
                .IsUnicode(false)
                .HasColumnName("EE_AGRemarks");
            entity.Property(e => e.EeAngelesGrade)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("EE_AngelesGrade");
            entity.Property(e => e.EeBarrettRemark)
                .HasMaxLength(1000)
                .IsUnicode(false)
                .HasColumnName("EE_BarrettRemark");
            entity.Property(e => e.EeHillClassificationGrade)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("EE_HillClassificationGrade");
            entity.Property(e => e.EeHillRemarks)
                .HasMaxLength(1000)
                .IsUnicode(false)
                .HasColumnName("EE_HillRemarks");
            entity.Property(e => e.EeLaxlesClassification).HasColumnName("EE_LAXLesClassification");
            entity.Property(e => e.ModifiedDt).HasColumnType("datetime");
            entity.Property(e => e.MtAttached).HasColumnName("MT_Attached");
            entity.Property(e => e.MtAttachement)
                .HasMaxLength(500)
                .IsUnicode(false)
                .HasColumnName("MT_Attachement");
            entity.Property(e => e.MtDate)
                .HasColumnType("datetime")
                .HasColumnName("MT_Date");
            entity.Property(e => e.MtRemark)
                .HasMaxLength(1000)
                .IsUnicode(false)
                .HasColumnName("MT_Remark");
            entity.Property(e => e.PHimAttached).HasColumnName("pHIM_Attached");
            entity.Property(e => e.PHimAttachement)
                .HasMaxLength(500)
                .IsUnicode(false)
                .HasColumnName("pHIM_Attachement");
            entity.Property(e => e.PHimDate)
                .HasColumnType("datetime")
                .HasColumnName("pHIM_Date");
            entity.Property(e => e.PHimRemark)
                .HasMaxLength(1000)
                .IsUnicode(false)
                .HasColumnName("pHIM_Remark");
            entity.Property(e => e.PHimpedanceMonitoring).HasColumnName("pHImpedanceMonitoring");
            entity.Property(e => e.Pid).HasColumnName("PID");
            entity.Property(e => e.TotalPoints)
                .HasMaxLength(50)
                .IsUnicode(false);
        });

        modelBuilder.Entity<VwBaselineRpt>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("vw_BaselineRPT");

            entity.Property(e => e.AcidRefluxRelatedSymptom)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("Acid Reflux related Symptom");
            entity.Property(e => e.AcidTasteDurationYrs).HasColumnName("Acid Taste Duration (Yrs)");
            entity.Property(e => e.AcidTasteFrequencyWk).HasColumnName("Acid Taste Frequency (/Wk)");
            entity.Property(e => e.AcidTasteInMouth).HasColumnName("Acid taste in mouth");
            entity.Property(e => e.AcidTasteNocturnal)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("Acid Taste Nocturnal");
            entity.Property(e => e.AcidTastePostural)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("Acid Taste Postural");
            entity.Property(e => e.AeratedDrinks)
                .HasMaxLength(3)
                .IsUnicode(false)
                .HasColumnName("Aerated Drinks");
            entity.Property(e => e.AeratedDrinksDurationYrs)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("Aerated Drinks Duration (yrs)");
            entity.Property(e => e.AeratedDrinksFrequencyDay)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("Aerated Drinks Frequency (/day)");
            entity.Property(e => e.AeratedDrinksQuantityMl)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("Aerated Drinks Quantity (ml)");
            entity.Property(e => e.Aerobics)
                .HasMaxLength(3)
                .IsUnicode(false);
            entity.Property(e => e.AerobicsDurationYrs)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("Aerobics Duration (yrs)");
            entity.Property(e => e.AerobicsFrequencyHrsWeek)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("Aerobics Frequency (hrs/week)");
            entity.Property(e => e.Alcohol)
                .HasMaxLength(3)
                .IsUnicode(false);
            entity.Property(e => e.AlcoholDurationYrs)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("Alcohol Duration (yrs)");
            entity.Property(e => e.AlcoholFrequencyWeek)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("Alcohol Frequency (/week)");
            entity.Property(e => e.AlcoholQuantityMl)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("Alcohol Quantity (ml)");
            entity.Property(e => e.AlginateDose)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("Alginate_Dose");
            entity.Property(e => e.AlginateFrequency)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("Alginate_Frequency");
            entity.Property(e => e.AlginateMedicationName)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("Alginate_Medication_Name");
            entity.Property(e => e.AntiPlateletAgentsMoleculeName)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("Anti-platelet Agents - Molecule Name");
            entity.Property(e => e.AntiPlateletDose)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("Anti-platelet - Dose");
            entity.Property(e => e.AntiPlateletFrequency)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("Anti-platelet - Frequency");
            entity.Property(e => e.Asthma)
                .HasMaxLength(3)
                .IsUnicode(false);
            entity.Property(e => e.AsthmaRemarks)
                .HasMaxLength(250)
                .IsUnicode(false)
                .HasColumnName("Asthma Remarks");
            entity.Property(e => e.BariatricSurgery)
                .HasMaxLength(3)
                .IsUnicode(false)
                .HasColumnName("Bariatric Surgery");
            entity.Property(e => e.BariatricSurgeryRemarks)
                .HasMaxLength(1000)
                .IsUnicode(false)
                .HasColumnName("Bariatric Surgery Remarks");
            entity.Property(e => e.BarrettSRemarks)
                .HasMaxLength(1000)
                .IsUnicode(false)
                .HasColumnName("Barrett’s Remarks");
            entity.Property(e => e.BehaviouralDisorderRemarks)
                .HasMaxLength(250)
                .IsUnicode(false)
                .HasColumnName("Behavioural disorder Remarks");
            entity.Property(e => e.BehaviouralDisorders)
                .HasMaxLength(3)
                .IsUnicode(false)
                .HasColumnName("Behavioural disorders");
            entity.Property(e => e.BiopsyAttached)
                .HasMaxLength(3)
                .IsUnicode(false)
                .HasColumnName("Biopsy Attached");
            entity.Property(e => e.BiopsyAttached1)
                .HasMaxLength(3)
                .IsUnicode(false)
                .HasColumnName("Biopsy_Attached");
            entity.Property(e => e.BiopsyDate)
                .HasColumnType("datetime")
                .HasColumnName("Biopsy_Date");
            entity.Property(e => e.BiopsyRemark)
                .HasMaxLength(1000)
                .IsUnicode(false)
                .HasColumnName("Biopsy_Remark");
            entity.Property(e => e.BisphosphonatesDose)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("Bisphosphonates - Dose");
            entity.Property(e => e.BisphosphonatesFrequency)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("Bisphosphonates - Frequency");
            entity.Property(e => e.BisphosphonatesMoleculeName)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("Bisphosphonates - Molecule Name");
            entity.Property(e => e.Bmi)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("BMI");
            entity.Property(e => e.Cancer)
                .HasMaxLength(3)
                .IsUnicode(false);
            entity.Property(e => e.CancerRemarks)
                .HasMaxLength(250)
                .IsUnicode(false)
                .HasColumnName("Cancer Remarks");
            entity.Property(e => e.CardiovascularDisorders)
                .HasMaxLength(3)
                .IsUnicode(false)
                .HasColumnName("Cardiovascular Disorders");
            entity.Property(e => e.CardiovascularDisordersRemarks)
                .HasMaxLength(250)
                .IsUnicode(false)
                .HasColumnName("Cardiovascular Disorders Remarks");
            entity.Property(e => e.ChocolatesSweets)
                .HasMaxLength(3)
                .IsUnicode(false)
                .HasColumnName("Chocolates/Sweets");
            entity.Property(e => e.ChocolatesSweetsDurationYrs)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("Chocolates/Sweets Duration (yrs)");
            entity.Property(e => e.ChocolatesSweetsFrequencyWeek)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("Chocolates/Sweets Frequency (/week)");
            entity.Property(e => e.ChocolatesSweetsQuantityG)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("Chocolates/Sweets Quantity (g)");
            entity.Property(e => e.ChronicKidneyDisease)
                .HasMaxLength(3)
                .IsUnicode(false)
                .HasColumnName("Chronic kidney disease");
            entity.Property(e => e.ChronicKidneyDiseaseRemarks)
                .HasMaxLength(250)
                .IsUnicode(false)
                .HasColumnName("Chronic kidney disease Remarks");
            entity.Property(e => e.ChronicLiverDisease)
                .HasMaxLength(3)
                .IsUnicode(false)
                .HasColumnName("Chronic Liver Disease");
            entity.Property(e => e.ChronicLiverDiseaseRemarks)
                .HasMaxLength(250)
                .IsUnicode(false)
                .HasColumnName("Chronic Liver Disease Remarks");
            entity.Property(e => e.CityName).HasMaxLength(100);
            entity.Property(e => e.Coffee)
                .HasMaxLength(3)
                .IsUnicode(false);
            entity.Property(e => e.CoffeeDurationYrs)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("Coffee Duration (yrs)");
            entity.Property(e => e.CoffeeFrequencyDay)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("Coffee Frequency (/day)");
            entity.Property(e => e.CoffeeQuantityMl)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("Coffee Quantity(ml)");
            entity.Property(e => e.ComputerUsageDurationYears).HasColumnName("Computer Usage Duration (years)");
            entity.Property(e => e.ComputerUsageHrsDay)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("Computer Usage (hrs/day)");
            entity.Property(e => e.ComputerUse)
                .HasMaxLength(3)
                .IsUnicode(false)
                .HasColumnName("Computer Use");
            entity.Property(e => e.Date).HasColumnType("datetime");
            entity.Property(e => e.Diabetes)
                .HasMaxLength(3)
                .IsUnicode(false);
            entity.Property(e => e.DiabetesRemarks)
                .HasMaxLength(250)
                .IsUnicode(false)
                .HasColumnName("Diabetes Remarks");
            entity.Property(e => e.Diet)
                .HasMaxLength(14)
                .IsUnicode(false);
            entity.Property(e => e.DietModifications)
                .HasMaxLength(3)
                .IsUnicode(false)
                .HasColumnName("Diet Modifications");
            entity.Property(e => e.DoSomeThingsGetStuckWhenYouSwallow).HasColumnName("Do some things get stuck when you swallow?");
            entity.Property(e => e.DoYouBurpALot).HasColumnName("Do you burp a lot?");
            entity.Property(e => e.DoYouEverFeelSickAfterMeals).HasColumnName("Do you ever feel sick after meals?");
            entity.Property(e => e.DoYouFeelFullWhileEatingMeals).HasColumnName("Do you feel full while eating meals?");
            entity.Property(e => e.DoYouGetBitterLiquidAcidComingUpIntoYourThroat).HasColumnName("Do you get bitter liquid (acid) coming up into your throat?");
            entity.Property(e => e.DoYouGetHeartburn).HasColumnName("Do you get heartburn?");
            entity.Property(e => e.DoYouGetHeartburnAfterMeals).HasColumnName("Do you get heartburn after meals?");
            entity.Property(e => e.DoYouGetHeartburnIfYouBendOver).HasColumnName("Do you get heartburn if you bend over?");
            entity.Property(e => e.DoYouHaveAnUnusualSymptomEGBurningSensationInYourThroat).HasColumnName("Do you have an unusual symptom (e.g. burning) sensation in your throat?");
            entity.Property(e => e.DoYouSometimesSubconsciouslyRubYourChestWithYourHand).HasColumnName("Do you sometimes subconsciously rub your chest with your hand?");
            entity.Property(e => e.DoesYourStomachEverFeelHeavyAfterMeals).HasColumnName("Does your stomach ever feel heavy after meals?");
            entity.Property(e => e.DoesYourStomachGetBloated).HasColumnName("Does your stomach get bloated?");
            entity.Property(e => e.Dose)
                .HasMaxLength(1)
                .IsUnicode(false);
            entity.Property(e => e.DurationNoOfYearsInTheAboveWorkingHours).HasColumnName("Duration (No. of years in the above working hours)");
            entity.Property(e => e.Dyslipidemia)
                .HasMaxLength(3)
                .IsUnicode(false);
            entity.Property(e => e.DyslipidemiaRemarks)
                .HasMaxLength(250)
                .IsUnicode(false)
                .HasColumnName("Dyslipidemia Remarks");
            entity.Property(e => e.DyspepticDysmotilitySymptom)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("Dyspeptic (Dysmotility) symptom");
            entity.Property(e => e.Education)
                .HasMaxLength(250)
                .IsUnicode(false);
            entity.Property(e => e.EndoscopyDate)
                .HasColumnType("datetime")
                .HasColumnName("Endoscopy Date");
            entity.Property(e => e.EndoscopyRemarks)
                .HasMaxLength(1000)
                .IsUnicode(false)
                .HasColumnName("Endoscopy Remarks");
            entity.Property(e => e.EsophagoGastricCancerRemarks)
                .HasMaxLength(250)
                .IsUnicode(false)
                .HasColumnName("Esophago-gastric Cancer Remarks");
            entity.Property(e => e.Exercise)
                .HasMaxLength(3)
                .IsUnicode(false);
            entity.Property(e => e.FamilyHistoryOfEsophagoGastricCancer)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("Family History of Esophago-gastric Cancer");
            entity.Property(e => e.FamilyHistoryOfGerd)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("Family History of GERD");
            entity.Property(e => e.FamilyIncome)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Frequency)
                .HasMaxLength(1)
                .IsUnicode(false);
            entity.Property(e => e.FundoplicationRemarks)
                .HasMaxLength(1000)
                .IsUnicode(false)
                .HasColumnName("Fundoplication Remarks");
            entity.Property(e => e.FundoplicationSurgery)
                .HasMaxLength(3)
                .IsUnicode(false)
                .HasColumnName("Fundoplication Surgery");
            entity.Property(e => e.GastricPoemRemarks)
                .HasMaxLength(1000)
                .IsUnicode(false)
                .HasColumnName("Gastric POEM Remarks");
            entity.Property(e => e.GastricPoemSurgery)
                .HasMaxLength(3)
                .IsUnicode(false)
                .HasColumnName("Gastric POEM Surgery");
            entity.Property(e => e.Gastrojejunostomy)
                .HasMaxLength(3)
                .IsUnicode(false);
            entity.Property(e => e.GastrojejunostomyRemarks)
                .HasMaxLength(1000)
                .IsUnicode(false)
                .HasColumnName("Gastrojejunostomy Remarks");
            entity.Property(e => e.Gender)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.GeneralAppearance)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("General Appearance");
            entity.Property(e => e.GeneralAppearanceComments)
                .HasMaxLength(1000)
                .IsUnicode(false)
                .HasColumnName("General Appearance – Comments");
            entity.Property(e => e.GerdRemarks)
                .HasMaxLength(250)
                .IsUnicode(false)
                .HasColumnName("GERD - Remarks");
            entity.Property(e => e.Gerdtype)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("GERDType");
            entity.Property(e => e.GredNoOfYear).HasColumnName("GRED_NoOfYear");
            entity.Property(e => e.Gym)
                .HasMaxLength(3)
                .IsUnicode(false);
            entity.Property(e => e.GymDurationYrs)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("Gym Duration (yrs)");
            entity.Property(e => e.GymFrequencyHrsWeek)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("Gym Frequency (hrs/week)");
            entity.Property(e => e.H2blockersCDose)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("H2BlockersC_Dose");
            entity.Property(e => e.H2blockersCFrequency)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("H2BlockersC_Frequency");
            entity.Property(e => e.H2blockersCMedicationName)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("H2BlockersC_Medication_Name");
            entity.Property(e => e.H2blockersDose)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("H2Blockers_Dose");
            entity.Property(e => e.H2blockersFrequency)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("H2Blockers_Frequency");
            entity.Property(e => e.H2blockersMedicationName)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("H2Blockers_Medication_Name");
            entity.Property(e => e.HeartburnDurationYrs).HasColumnName("Heartburn Duration[Yrs)");
            entity.Property(e => e.HeartburnFrequencyWk).HasColumnName("Heartburn Frequency(/Wk)");
            entity.Property(e => e.HeartburnNocturnal)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("Heartburn Nocturnal");
            entity.Property(e => e.HeartburnPostural)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("Heartburn Postural");
            entity.Property(e => e.HeightInCms)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("Height (in cms)");
            entity.Property(e => e.HillSClassification)
                .HasMaxLength(3)
                .IsUnicode(false)
                .HasColumnName("Hill’s classification");
            entity.Property(e => e.HillSClassificationGrade)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("Hill’s classification Grade");
            entity.Property(e => e.HillSClassificationRemarks)
                .HasMaxLength(1000)
                .IsUnicode(false)
                .HasColumnName("Hill’s classification Remarks");
            entity.Property(e => e.HistoryOfEndoscopy)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("History of Endoscopy");
            entity.Property(e => e.HistoryOfGastroSurgery)
                .HasMaxLength(3)
                .IsUnicode(false)
                .HasColumnName("History of Gastro-surgery");
            entity.Property(e => e.Hypertension)
                .HasMaxLength(3)
                .IsUnicode(false);
            entity.Property(e => e.HypertensionRemarks)
                .HasMaxLength(250)
                .IsUnicode(false)
                .HasColumnName("Hypertension Remarks");
            entity.Property(e => e.Hyperthyroidism)
                .HasMaxLength(3)
                .IsUnicode(false);
            entity.Property(e => e.HyperthyroidismRemarks)
                .HasMaxLength(250)
                .IsUnicode(false)
                .HasColumnName("Hyperthyroidism Remarks");
            entity.Property(e => e.Hypothyroidism)
                .HasMaxLength(3)
                .IsUnicode(false);
            entity.Property(e => e.HypothyroidismRemarks)
                .HasMaxLength(250)
                .IsUnicode(false)
                .HasColumnName("Hypothyroidism Remarks");
            entity.Property(e => e.Initial)
                .HasMaxLength(250)
                .IsUnicode(false);
            entity.Property(e => e.JobOccupationType)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("Job/ Occupation type");
            entity.Property(e => e.Jogging)
                .HasMaxLength(3)
                .IsUnicode(false);
            entity.Property(e => e.JoggingDurationYrs)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("Jogging Duration (yrs)");
            entity.Property(e => e.JoggingFrequencyHrsWeek)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("Jogging Frequency (hrs/week)");
            entity.Property(e => e.KnownCaseOfGerd).HasColumnName("KnownCaseOfGERD");
            entity.Property(e => e.LaxLesClassification)
                .HasMaxLength(3)
                .IsUnicode(false)
                .HasColumnName("LAX les classification");
            entity.Property(e => e.LosAngelesGrade)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("Los Angeles Grade");
            entity.Property(e => e.LosAngelesGradeRemarks)
                .HasMaxLength(1000)
                .IsUnicode(false)
                .HasColumnName("Los Angeles Grade Remarks");
            entity.Property(e => e.ManometryTest)
                .HasMaxLength(3)
                .IsUnicode(false);
            entity.Property(e => e.ManometryTestAttached)
                .HasMaxLength(3)
                .IsUnicode(false)
                .HasColumnName("ManometryTest Attached");
            entity.Property(e => e.ModerationOfAlcohol)
                .HasMaxLength(3)
                .IsUnicode(false)
                .HasColumnName("Moderation of alcohol");
            entity.Property(e => e.MtDate)
                .HasColumnType("datetime")
                .HasColumnName("MT_Date");
            entity.Property(e => e.MtRemark)
                .HasMaxLength(1000)
                .IsUnicode(false)
                .HasColumnName("MT_Remark");
            entity.Property(e => e.NeurologicalDisorder)
                .HasMaxLength(3)
                .IsUnicode(false)
                .HasColumnName("Neurological Disorder");
            entity.Property(e => e.NeurologicalDisorderRemarks)
                .HasMaxLength(250)
                .IsUnicode(false)
                .HasColumnName("Neurological Disorder Remarks");
            entity.Property(e => e.NsaidsDose)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("NSAIDs - Dose");
            entity.Property(e => e.NsaidsFrequency)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("NSAIDs - Frequency");
            entity.Property(e => e.NsaidsMoleculeName)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("NSAIDs - Molecule Name");
            entity.Property(e => e.Occupation)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Osteoarthritis)
                .HasMaxLength(3)
                .IsUnicode(false);
            entity.Property(e => e.OsteoarthritisRemarks)
                .HasMaxLength(250)
                .IsUnicode(false)
                .HasColumnName("Osteoarthritis Remarks");
            entity.Property(e => e.OtherComorbidity)
                .HasMaxLength(3)
                .IsUnicode(false)
                .HasColumnName("Other Comorbidity");
            entity.Property(e => e.OtherComorbiditySpecify)
                .HasMaxLength(250)
                .IsUnicode(false)
                .HasColumnName("Other Comorbidity- Specify");
            entity.Property(e => e.OtherDose)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("Other - Dose");
            entity.Property(e => e.OtherDrugMoleculeName)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("Other Drug - Molecule Name");
            entity.Property(e => e.OtherExamAreaSpecify)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("Other Exam Area (Specify)");
            entity.Property(e => e.OtherExamComments)
                .HasMaxLength(1000)
                .IsUnicode(false)
                .HasColumnName("Other Exam – Comments");
            entity.Property(e => e.OtherExamStatus)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("Other Exam – Status");
            entity.Property(e => e.OtherExercise)
                .HasMaxLength(3)
                .IsUnicode(false)
                .HasColumnName("Other Exercise");
            entity.Property(e => e.OtherExerciseDurationYrs)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("Other Exercise Duration (yrs)");
            entity.Property(e => e.OtherExerciseFrequencyHrsWeek)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("Other Exercise Frequency (hrs/week)");
            entity.Property(e => e.OtherFrequency)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("Other - Frequency");
            entity.Property(e => e.OtherGastroSurgery)
                .HasMaxLength(3)
                .IsUnicode(false)
                .HasColumnName("Other Gastro Surgery");
            entity.Property(e => e.OtherGastroSurgeryRemarks)
                .HasMaxLength(1000)
                .IsUnicode(false)
                .HasColumnName("Other Gastro Surgery Remarks");
            entity.Property(e => e.Otherdose1)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("otherdose");
            entity.Property(e => e.Otherfrequency1)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("otherfrequency");
            entity.Property(e => e.OthersMedicationName)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("others_Medication_Name");
            entity.Property(e => e.PHImpedanceMonitoring)
                .HasMaxLength(3)
                .IsUnicode(false)
                .HasColumnName("pH Impedance monitoring");
            entity.Property(e => e.PHImpedanceMonitoringDate)
                .HasColumnType("datetime")
                .HasColumnName("pH Impedance monitoring Date");
            entity.Property(e => e.PHImpedanceMonitoringRemarks)
                .HasMaxLength(1000)
                .IsUnicode(false)
                .HasColumnName("pH Impedance monitoring Remarks");
            entity.Property(e => e.PHimReportAttached)
                .HasMaxLength(3)
                .IsUnicode(false)
                .HasColumnName("pHIM Report Attached");
            entity.Property(e => e.PastHistory).HasColumnName("Past History");
            entity.Property(e => e.PcabDose)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("PCAB_Dose");
            entity.Property(e => e.PcabFrequency)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("PCAB_Frequency");
            entity.Property(e => e.PcabMedicationName)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("PCAB_Medication_Name");
            entity.Property(e => e.PerAbdomenExaminationFindings)
                .HasMaxLength(1000)
                .IsUnicode(false)
                .HasColumnName("Per Abdomen Examination Findings");
            entity.Property(e => e.Pincode).HasColumnName("pincode");
            entity.Property(e => e.PlaceType)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.PpiDose)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("PPI_Dose");
            entity.Property(e => e.PpiFrequency)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("PPI_Frequency");
            entity.Property(e => e.PpiMedicationName)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("PPI_Medication_Name");
            entity.Property(e => e.PpiUsage)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("PPI Usage");
            entity.Property(e => e.ProkineticsDose)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("Prokinetics_Dose");
            entity.Property(e => e.ProkineticsFrequency)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("Prokinetics_Frequency");
            entity.Property(e => e.ProkineticsMedicationName)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("Prokinetics_Medication_Name");
            entity.Property(e => e.RefractoryToPpi).HasColumnName("RefractoryToPPI");
            entity.Property(e => e.RegularExercise)
                .HasMaxLength(3)
                .IsUnicode(false)
                .HasColumnName("Regular exercise");
            entity.Property(e => e.RegurgitationDurationYrs).HasColumnName("Regurgitation Duration (Yrs)");
            entity.Property(e => e.RegurgitationFrequencyWk).HasColumnName("Regurgitation Frequency(/Wk)");
            entity.Property(e => e.RegurgitationNocturnal)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("Regurgitation Nocturnal");
            entity.Property(e => e.RegurgitationPostural)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("Regurgitation Postural");
            entity.Property(e => e.ReportAttached)
                .HasMaxLength(3)
                .IsUnicode(false)
                .HasColumnName("Report Attached");
            entity.Property(e => e.RespiratorySystem)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("Respiratory System");
            entity.Property(e => e.RespiratorySystemComments)
                .HasMaxLength(1000)
                .IsUnicode(false)
                .HasColumnName("Respiratory System – Comments");
            entity.Property(e => e.RetrosternalNocturnal)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("Retrosternal Nocturnal");
            entity.Property(e => e.RetrosternalPain).HasColumnName("Retrosternal pain");
            entity.Property(e => e.RetrosternalPainDurationYrs).HasColumnName("Retrosternal Pain Duration (Yrs)");
            entity.Property(e => e.RetrosternalPainFrequencyWk).HasColumnName("Retrosternal Pain Frequency(/Wk)");
            entity.Property(e => e.RetrosternalPostural)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("Retrosternal Postural");
            entity.Property(e => e.RheumatoidArthritis)
                .HasMaxLength(3)
                .IsUnicode(false)
                .HasColumnName("Rheumatoid Arthritis");
            entity.Property(e => e.RheumatoidArthritisRemrks)
                .HasMaxLength(250)
                .IsUnicode(false)
                .HasColumnName("Rheumatoid Arthritis Remrks");
            entity.Property(e => e.SleepApnea)
                .HasMaxLength(3)
                .IsUnicode(false)
                .HasColumnName("Sleep Apnea");
            entity.Property(e => e.SleepApneaDurationYears)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("Sleep Apnea Duration (years)");
            entity.Property(e => e.SleepApneaFrequencyWeek)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("Sleep Apnea Frequency (/week)");
            entity.Property(e => e.SmartphoneUsageDurationYears).HasColumnName("Smartphone Usage Duration (years)");
            entity.Property(e => e.SmartphoneUsageHrsDay)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("Smartphone Usage (hrs/day)");
            entity.Property(e => e.SmartphoneUse)
                .HasMaxLength(3)
                .IsUnicode(false)
                .HasColumnName("Smartphone Use");
            entity.Property(e => e.Smoking)
                .HasMaxLength(3)
                .IsUnicode(false);
            entity.Property(e => e.SmokingDurationYrs)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("Smoking Duration (yrs)");
            entity.Property(e => e.SmokingFrequencyDay)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("Smoking Frequency (/day)");
            entity.Property(e => e.SmokingQuantityPacks)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("Smoking Quantity (packs)");
            entity.Property(e => e.SocioeconomicStatus)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.SpicyFood)
                .HasMaxLength(3)
                .IsUnicode(false)
                .HasColumnName("Spicy Food");
            entity.Property(e => e.SpicyFoodDurationYrs)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("Spicy Food Duration (yrs)");
            entity.Property(e => e.SpicyFoodFrequencyWeek)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("Spicy Food Frequency(/week)");
            entity.Property(e => e.SpicyFoodQuantity)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("Spicy Food Quantity");
            entity.Property(e => e.StateName).HasMaxLength(50);
            entity.Property(e => e.SteroidsDose)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("Steroids - Dose");
            entity.Property(e => e.SteroidsFrequency)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("Steroids - Frequency");
            entity.Property(e => e.SteroidsMoleculeName)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("Steroids - Molecule Name");
            entity.Property(e => e.StopTobaccoUse)
                .HasMaxLength(3)
                .IsUnicode(false)
                .HasColumnName("Stop Tobacco use");
            entity.Property(e => e.SubjectNo)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.SucralfateDose)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("Sucralfate_Dose");
            entity.Property(e => e.SucralfateFrequency)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("Sucralfate_Frequency");
            entity.Property(e => e.SucralfateMedicationName)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("Sucralfate_Medication_Name");
            entity.Property(e => e.SymtopmScore)
                .HasMaxLength(103)
                .IsUnicode(false);
            entity.Property(e => e.SystemicSclerosis)
                .HasMaxLength(3)
                .IsUnicode(false)
                .HasColumnName("Systemic Sclerosis");
            entity.Property(e => e.SystemicSclerosisRemarks)
                .HasMaxLength(250)
                .IsUnicode(false)
                .HasColumnName("Systemic Sclerosis Remarks");
            entity.Property(e => e.Tea)
                .HasMaxLength(3)
                .IsUnicode(false);
            entity.Property(e => e.TeaDurationYrs)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("Tea Duration(yrs)");
            entity.Property(e => e.TeaFrequencyDay)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("Tea Frequency(/day)");
            entity.Property(e => e.TeaQuantityMl)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("Tea Quantity(ml)");
            entity.Property(e => e.TobaccoInOtherForms)
                .HasMaxLength(3)
                .IsUnicode(false)
                .HasColumnName("Tobacco in other forms");
            entity.Property(e => e.TobaccoInOtherFormsDurationYrs)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("Tobacco in other forms Duration (yrs)");
            entity.Property(e => e.TobaccoInOtherFormsFrequencyDay)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("Tobacco in other forms Frequency (/day)");
            entity.Property(e => e.TobaccoInOtherFormsQuantity)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("Tobacco in other forms Quantity");
            entity.Property(e => e.TotalPoints)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("Total Points");
            entity.Property(e => e.TotalSymptomScoreTssInGerdPatients).HasColumnName("Total Symptom Score (TSS) in GERD patients");
            entity.Property(e => e.Walking)
                .HasMaxLength(3)
                .IsUnicode(false);
            entity.Property(e => e.WalkingDurationYrs)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("Walking Duration (yrs)");
            entity.Property(e => e.WalkingFrequencyHrsWeek)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("Walking Frequency (hrs/week)");
            entity.Property(e => e.WeightInKg)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("Weight (in Kg)");
            entity.Property(e => e.WeightLoss)
                .HasMaxLength(3)
                .IsUnicode(false)
                .HasColumnName("Weight loss");
            entity.Property(e => e.WorkingHoursOccupation)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("Working Hours (Occupation)");
            entity.Property(e => e.Yoga)
                .HasMaxLength(3)
                .IsUnicode(false);
            entity.Property(e => e.YogaDurationYrs)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("Yoga Duration (yrs)");
            entity.Property(e => e.YogaFrequencyHrsWeek)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("Yoga Frequency (hrs/week)");
            entity.Property(e => e.Zumba)
                .HasMaxLength(3)
                .IsUnicode(false);
            entity.Property(e => e.ZumbaDurationYrs)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("Zumba Duration (yrs)");
            entity.Property(e => e.ZumbaFrequencyHrsWeek)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("Zumba Frequency (hrs/week)");
        });

        modelBuilder.Entity<VwCheifComplaint>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("vw_CheifComplaint");

            entity.Property(e => e.AtDuration).HasColumnName("AT_Duration");
            entity.Property(e => e.AtFrequency).HasColumnName("AT_Frequency");
            entity.Property(e => e.AtNocturnal)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("AT_Nocturnal");
            entity.Property(e => e.AtPostural)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("AT_Postural");
            entity.Property(e => e.CheifCompliantId).HasColumnName("CheifCompliantID");
            entity.Property(e => e.CreatedByName)
                .HasMaxLength(250)
                .IsUnicode(false);
            entity.Property(e => e.CreatedDt).HasColumnType("datetime");
            entity.Property(e => e.DoctorId).HasColumnName("DoctorID");
            entity.Property(e => e.HbDuration).HasColumnName("HB_Duration");
            entity.Property(e => e.HbFrequency).HasColumnName("HB_Frequency");
            entity.Property(e => e.HbNocturnal)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("HB_Nocturnal");
            entity.Property(e => e.HbPostural)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("HB_Postural");
            entity.Property(e => e.ModifiedByName)
                .HasMaxLength(250)
                .IsUnicode(false);
            entity.Property(e => e.ModifiedDt).HasColumnType("datetime");
            entity.Property(e => e.PatientId).HasColumnName("PatientID");
            entity.Property(e => e.RDuration).HasColumnName("R_Duration");
            entity.Property(e => e.RFrequency).HasColumnName("R_Frequency");
            entity.Property(e => e.RNocturnal)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("R_Nocturnal");
            entity.Property(e => e.RPostural)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("R_Postural");
            entity.Property(e => e.RpDuration).HasColumnName("RP_Duration");
            entity.Property(e => e.RpFrequency).HasColumnName("RP_Frequency");
            entity.Property(e => e.RpNocturnal)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("RP_Nocturnal");
            entity.Property(e => e.RpPostural)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("RP_Postural");
        });

        modelBuilder.Entity<VwCity>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("vw_city");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Latitude).HasColumnName("latitude");
            entity.Property(e => e.Longitude).HasColumnName("longitude");
            entity.Property(e => e.Name)
                .HasMaxLength(100)
                .HasColumnName("name");
            entity.Property(e => e.StateId).HasColumnName("state_id");
            entity.Property(e => e.StateName)
                .HasMaxLength(50)
                .HasColumnName("state_name");
            entity.Property(e => e.WikiDataId)
                .HasColumnType("money")
                .HasColumnName("wikiDataId");
        });

        modelBuilder.Entity<VwComorbidity>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("vw_Comorbidities");

            entity.Property(e => e.APresent)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("A_Present");
            entity.Property(e => e.ARemark)
                .HasMaxLength(250)
                .IsUnicode(false)
                .HasColumnName("A_Remark");
            entity.Property(e => e.BdPresent)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("BD_Present");
            entity.Property(e => e.BdRemark)
                .HasMaxLength(250)
                .IsUnicode(false)
                .HasColumnName("BD_Remark");
            entity.Property(e => e.CPresent)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("C_Present");
            entity.Property(e => e.CRemark)
                .HasMaxLength(250)
                .IsUnicode(false)
                .HasColumnName("C_Remark");
            entity.Property(e => e.CdPresent)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("CD_Present");
            entity.Property(e => e.CdRemark)
                .HasMaxLength(250)
                .IsUnicode(false)
                .HasColumnName("CD_Remark");
            entity.Property(e => e.CkdPresent)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("CKD_Present");
            entity.Property(e => e.CkdRemark)
                .HasMaxLength(250)
                .IsUnicode(false)
                .HasColumnName("CKD_Remark");
            entity.Property(e => e.CldPresent)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("CLD_Present");
            entity.Property(e => e.CldRemark)
                .HasMaxLength(250)
                .IsUnicode(false)
                .HasColumnName("CLD_Remark");
            entity.Property(e => e.CmoPresent)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("CMO_Present");
            entity.Property(e => e.CmoRemark)
                .HasMaxLength(250)
                .IsUnicode(false)
                .HasColumnName("CMO_Remark");
            entity.Property(e => e.ComorbiditiesId).HasColumnName("ComorbiditiesID");
            entity.Property(e => e.CreatedByName)
                .HasMaxLength(250)
                .IsUnicode(false);
            entity.Property(e => e.CreatedDt).HasColumnType("datetime");
            entity.Property(e => e.DbPresent)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("DB_Present");
            entity.Property(e => e.DbRemark)
                .HasMaxLength(250)
                .IsUnicode(false)
                .HasColumnName("DB_Remark");
            entity.Property(e => e.DdPresent)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("DD_Present");
            entity.Property(e => e.DdRemark)
                .HasMaxLength(250)
                .IsUnicode(false)
                .HasColumnName("DD_Remark");
            entity.Property(e => e.DoctorId).HasColumnName("DoctorID");
            entity.Property(e => e.HPresent)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("H_Present");
            entity.Property(e => e.HRemark)
                .HasMaxLength(250)
                .IsUnicode(false)
                .HasColumnName("H_Remark");
            entity.Property(e => e.HtPresent)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("HT_Present");
            entity.Property(e => e.HtRemark)
                .HasMaxLength(250)
                .IsUnicode(false)
                .HasColumnName("HT_Remark");
            entity.Property(e => e.HtdPresent)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("HTD_Present");
            entity.Property(e => e.HtdRemark)
                .HasMaxLength(250)
                .IsUnicode(false)
                .HasColumnName("HTD_Remark");
            entity.Property(e => e.ModifiedByName)
                .HasMaxLength(250)
                .IsUnicode(false);
            entity.Property(e => e.ModifiedDt).HasColumnType("datetime");
            entity.Property(e => e.NdPresent)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("ND_Present");
            entity.Property(e => e.NdRemark)
                .HasMaxLength(250)
                .IsUnicode(false)
                .HasColumnName("ND_Remark");
            entity.Property(e => e.OPresent)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("O_Present");
            entity.Property(e => e.ORemark)
                .HasMaxLength(250)
                .IsUnicode(false)
                .HasColumnName("O_Remark");
            entity.Property(e => e.PatientId).HasColumnName("PatientID");
            entity.Property(e => e.RaPresent)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("RA_Present");
            entity.Property(e => e.RaRemark)
                .HasMaxLength(250)
                .IsUnicode(false)
                .HasColumnName("RA_Remark");
            entity.Property(e => e.SsPresent)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("SS_Present");
            entity.Property(e => e.SsRemark)
                .HasMaxLength(250)
                .IsUnicode(false)
                .HasColumnName("SS_Remark");
        });

        modelBuilder.Entity<VwComorbitiesRpt>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("vw_ComorbitiesRPT");

            entity.Property(e => e.BdPresent)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("BD_Present");
            entity.Property(e => e.BdRemark)
                .HasMaxLength(250)
                .IsUnicode(false)
                .HasColumnName("BD_Remark");
            entity.Property(e => e.CdPresent)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("CD_Present");
            entity.Property(e => e.CdRemark)
                .HasMaxLength(250)
                .IsUnicode(false)
                .HasColumnName("CD_Remark");
            entity.Property(e => e.City).HasColumnName("city");
            entity.Property(e => e.CldPresent)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("CLD_Present");
            entity.Property(e => e.CldRemark)
                .HasMaxLength(250)
                .IsUnicode(false)
                .HasColumnName("CLD_Remark");
            entity.Property(e => e.DbPresent)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("DB_Present");
            entity.Property(e => e.DbRemark)
                .HasMaxLength(250)
                .IsUnicode(false)
                .HasColumnName("DB_Remark");
            entity.Property(e => e.DdPresent)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("DD_Present");
            entity.Property(e => e.DdRemark)
                .HasMaxLength(250)
                .IsUnicode(false)
                .HasColumnName("DD_Remark");
            entity.Property(e => e.Gender)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.HtPresent)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("HT_Present");
            entity.Property(e => e.HtRemark)
                .HasMaxLength(250)
                .IsUnicode(false)
                .HasColumnName("HT_Remark");
            entity.Property(e => e.HtdPresent)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("HTD_Present");
            entity.Property(e => e.HtdRemark)
                .HasMaxLength(250)
                .IsUnicode(false)
                .HasColumnName("HTD_Remark");
            entity.Property(e => e.Initial)
                .HasMaxLength(250)
                .IsUnicode(false);
            entity.Property(e => e.NdPresent)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("ND_Present");
            entity.Property(e => e.NdRemark)
                .HasMaxLength(250)
                .IsUnicode(false)
                .HasColumnName("ND_Remark");
            entity.Property(e => e.State).HasColumnName("state");
            entity.Property(e => e.SubjectNo)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Zone)
                .HasMaxLength(6)
                .IsUnicode(false);
        });

        modelBuilder.Entity<VwCompletedRpt>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("vw_CompletedRPT");

            entity.Property(e => e.AcidRefluxrelatedSymptom)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.AcidTasteDurationYrs).HasColumnName("Acid Taste Duration (Yrs)");
            entity.Property(e => e.AcidTasteFrequencyWk).HasColumnName("Acid Taste Frequency (/Wk)");
            entity.Property(e => e.AcidTasteNocturnal)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("Acid Taste Nocturnal");
            entity.Property(e => e.AcidTastePostural)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("Acid Taste Postural");
            entity.Property(e => e.AeratedDrinks)
                .HasMaxLength(3)
                .IsUnicode(false)
                .HasColumnName("Aerated Drinks");
            entity.Property(e => e.AeratedDrinksDurationYrs)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("Aerated Drinks Duration (yrs)");
            entity.Property(e => e.AeratedDrinksFrequencyDay)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("Aerated Drinks Frequency (/day)");
            entity.Property(e => e.AeratedDrinksQuantityMl)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("Aerated Drinks Quantity (ml)");
            entity.Property(e => e.Aerobics)
                .HasMaxLength(3)
                .IsUnicode(false);
            entity.Property(e => e.AerobicsDurationYrs)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("Aerobics Duration (yrs)");
            entity.Property(e => e.AerobicsFrequencyHrsWeek)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("Aerobics Frequency (hrs/week)");
            entity.Property(e => e.Alcohol)
                .HasMaxLength(3)
                .IsUnicode(false);
            entity.Property(e => e.AlcoholDurationYrs)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("Alcohol Duration (yrs)");
            entity.Property(e => e.AlcoholFrequencyWeek)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("Alcohol Frequency (/week)");
            entity.Property(e => e.AlcoholQuantityMl)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("Alcohol Quantity (ml)");
            entity.Property(e => e.AlginateDose)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("Alginate_Dose");
            entity.Property(e => e.AlginateFrequency)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("Alginate_Frequency");
            entity.Property(e => e.AlginateMedicationName)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("Alginate_Medication_Name");
            entity.Property(e => e.AntiPlateletAgentsMoleculeName)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("Anti-platelet Agents - Molecule Name");
            entity.Property(e => e.AntiPlateletDose)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("Anti-platelet - Dose");
            entity.Property(e => e.AntiPlateletFrequency)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("Anti-platelet - Frequency");
            entity.Property(e => e.Asthma)
                .HasMaxLength(3)
                .IsUnicode(false);
            entity.Property(e => e.AsthmaRemarks)
                .HasMaxLength(250)
                .IsUnicode(false)
                .HasColumnName("Asthma Remarks");
            entity.Property(e => e.B1Regurgitation).HasColumnName("B1_Regurgitation");
            entity.Property(e => e.BariatricSurgery)
                .HasMaxLength(3)
                .IsUnicode(false)
                .HasColumnName("Bariatric Surgery");
            entity.Property(e => e.BariatricSurgeryRemarks)
                .HasMaxLength(1000)
                .IsUnicode(false)
                .HasColumnName("Bariatric Surgery Remarks");
            entity.Property(e => e.BarrettSRemarks)
                .HasMaxLength(1000)
                .IsUnicode(false)
                .HasColumnName("Barrett’s Remarks");
            entity.Property(e => e.BehaviouralDisorderRemarks)
                .HasMaxLength(250)
                .IsUnicode(false)
                .HasColumnName("Behavioural disorder Remarks");
            entity.Property(e => e.BehaviouralDisorders)
                .HasMaxLength(3)
                .IsUnicode(false)
                .HasColumnName("Behavioural disorders");
            entity.Property(e => e.BiopsyAttached).HasColumnName("Biopsy_Attached");
            entity.Property(e => e.BiopsyDate)
                .HasColumnType("datetime")
                .HasColumnName("Biopsy_Date");
            entity.Property(e => e.BiopsyRemark)
                .HasMaxLength(1000)
                .IsUnicode(false)
                .HasColumnName("Biopsy_Remark");
            entity.Property(e => e.BisphosphonatesDose)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("Bisphosphonates - Dose");
            entity.Property(e => e.BisphosphonatesFrequency)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("Bisphosphonates - Frequency");
            entity.Property(e => e.BisphosphonatesMoleculeName)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("Bisphosphonates - Molecule Name");
            entity.Property(e => e.Bmi)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("BMI");
            entity.Property(e => e.Cancer)
                .HasMaxLength(3)
                .IsUnicode(false);
            entity.Property(e => e.CancerRemarks)
                .HasMaxLength(250)
                .IsUnicode(false)
                .HasColumnName("Cancer Remarks");
            entity.Property(e => e.CardiovascularDisorders)
                .HasMaxLength(3)
                .IsUnicode(false)
                .HasColumnName("Cardiovascular Disorders");
            entity.Property(e => e.CardiovascularDisordersRemarks)
                .HasMaxLength(250)
                .IsUnicode(false)
                .HasColumnName("Cardiovascular Disorders Remarks");
            entity.Property(e => e.ChocolatesSweets)
                .HasMaxLength(3)
                .IsUnicode(false)
                .HasColumnName("Chocolates/Sweets");
            entity.Property(e => e.ChocolatesSweetsDurationYrs)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("Chocolates/Sweets Duration (yrs)");
            entity.Property(e => e.ChocolatesSweetsFrequencyWeek)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("Chocolates/Sweets Frequency (/week)");
            entity.Property(e => e.ChocolatesSweetsQuantityG)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("Chocolates/Sweets Quantity (g)");
            entity.Property(e => e.ChronicKidneyDisease)
                .HasMaxLength(3)
                .IsUnicode(false)
                .HasColumnName("Chronic kidney disease");
            entity.Property(e => e.ChronicKidneyDiseaseRemarks)
                .HasMaxLength(250)
                .IsUnicode(false)
                .HasColumnName("Chronic kidney disease Remarks");
            entity.Property(e => e.ChronicLiverDisease)
                .HasMaxLength(3)
                .IsUnicode(false)
                .HasColumnName("Chronic Liver Disease");
            entity.Property(e => e.ChronicLiverDiseaseRemarks)
                .HasMaxLength(250)
                .IsUnicode(false)
                .HasColumnName("Chronic Liver Disease Remarks");
            entity.Property(e => e.CityName).HasMaxLength(100);
            entity.Property(e => e.Coffee)
                .HasMaxLength(3)
                .IsUnicode(false);
            entity.Property(e => e.CoffeeDurationYrs)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("Coffee Duration (yrs)");
            entity.Property(e => e.CoffeeFrequencyDay)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("Coffee Frequency (/day)");
            entity.Property(e => e.CoffeeQuantityMl)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("Coffee Quantity(ml)");
            entity.Property(e => e.ComputerUsageDurationYears).HasColumnName("Computer Usage Duration (years)");
            entity.Property(e => e.ComputerUsageHrsDay)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("Computer Usage (hrs/day)");
            entity.Property(e => e.ComputerUse)
                .HasMaxLength(3)
                .IsUnicode(false)
                .HasColumnName("Computer Use");
            entity.Property(e => e.Date).HasColumnType("datetime");
            entity.Property(e => e.Diabetes)
                .HasMaxLength(3)
                .IsUnicode(false);
            entity.Property(e => e.DiabetesRemarks)
                .HasMaxLength(250)
                .IsUnicode(false)
                .HasColumnName("Diabetes Remarks");
            entity.Property(e => e.Diet)
                .HasMaxLength(14)
                .IsUnicode(false);
            entity.Property(e => e.DietModifications)
                .HasMaxLength(3)
                .IsUnicode(false)
                .HasColumnName("Diet Modifications");
            entity.Property(e => e.Doesyourstomacheverfeelheavyaftermeals).HasColumnName("Doesyourstomacheverfeelheavyaftermeals?");
            entity.Property(e => e.Doesyourstomachgetbloated).HasColumnName("Doesyourstomachgetbloated?");
            entity.Property(e => e.Dose)
                .HasMaxLength(1)
                .IsUnicode(false);
            entity.Property(e => e.Dosomethingsgetstuckwhenyouswallow).HasColumnName("Dosomethingsgetstuckwhenyouswallow?");
            entity.Property(e => e.Doyouburpalot).HasColumnName("Doyouburpalot?");
            entity.Property(e => e.Doyoueverfeelsickaftermeals).HasColumnName("Doyoueverfeelsickaftermeals?");
            entity.Property(e => e.Doyoufeelfullwhileeatingmeals).HasColumnName("Doyoufeelfullwhileeatingmeals?");
            entity.Property(e => e.DoyougetbitterliquidAcidComingupintoyourthroat).HasColumnName("Doyougetbitterliquid(acid)comingupintoyourthroat?");
            entity.Property(e => e.Doyougetheartburn).HasColumnName("Doyougetheartburn?");
            entity.Property(e => e.Doyougetheartburnaftermeals).HasColumnName("Doyougetheartburnaftermeals?");
            entity.Property(e => e.Doyougetheartburnifyoubendover).HasColumnName("Doyougetheartburnifyoubendover?");
            entity.Property(e => e.Doyousometimessubconsciouslyrubyourchestwithyourhand).HasColumnName("Doyousometimessubconsciouslyrubyourchestwithyourhand?");
            entity.Property(e => e.DurationNoOfYearsInTheAboveWorkingHours).HasColumnName("Duration (No. of years in the above working hours)");
            entity.Property(e => e.Dyslipidemia)
                .HasMaxLength(3)
                .IsUnicode(false);
            entity.Property(e => e.DyslipidemiaRemarks)
                .HasMaxLength(250)
                .IsUnicode(false)
                .HasColumnName("Dyslipidemia Remarks");
            entity.Property(e => e.DyspepticDysmotilitySymptom)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("Dyspeptic(Dysmotility)symptom");
            entity.Property(e => e.Education)
                .HasMaxLength(250)
                .IsUnicode(false);
            entity.Property(e => e.EndoscopyDate)
                .HasColumnType("datetime")
                .HasColumnName("Endoscopy Date");
            entity.Property(e => e.EndoscopyRemarks)
                .HasMaxLength(1000)
                .IsUnicode(false)
                .HasColumnName("Endoscopy Remarks");
            entity.Property(e => e.EsophagoGastricCancerRemarks)
                .HasMaxLength(250)
                .IsUnicode(false)
                .HasColumnName("Esophago-gastric Cancer Remarks");
            entity.Property(e => e.Exercise)
                .HasMaxLength(3)
                .IsUnicode(false);
            entity.Property(e => e.F1APresent)
                .HasMaxLength(3)
                .IsUnicode(false)
                .HasColumnName("F1_A_Present");
            entity.Property(e => e.F1ARemark)
                .HasMaxLength(250)
                .IsUnicode(false)
                .HasColumnName("F1_A_Remark");
            entity.Property(e => e.F1AcidRefluxrelatedSymptom)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("F1_AcidRefluxrelatedSymptom");
            entity.Property(e => e.F1Acidtasteinmouth).HasColumnName("F1_Acidtasteinmouth");
            entity.Property(e => e.F1AlginateDose)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("F1_Alginate_Dose");
            entity.Property(e => e.F1AlginateFrequency)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("F1_Alginate_Frequency");
            entity.Property(e => e.F1AlginateMedicationName)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("F1_Alginate_Medication_Name");
            entity.Property(e => e.F1AtDuration).HasColumnName("F1_AT_Duration");
            entity.Property(e => e.F1AtFrequency).HasColumnName("F1_AT_Frequency");
            entity.Property(e => e.F1AtNocturnal)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("F1_AT_Nocturnal");
            entity.Property(e => e.F1AtPostural)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("F1_AT_Postural");
            entity.Property(e => e.F1BarrettsRemarks)
                .HasMaxLength(1000)
                .IsUnicode(false)
                .HasColumnName("F1_BarrettsRemarks");
            entity.Property(e => e.F1BdPresent)
                .HasMaxLength(3)
                .IsUnicode(false)
                .HasColumnName("F1_BD_Present");
            entity.Property(e => e.F1BdRemark)
                .HasMaxLength(250)
                .IsUnicode(false)
                .HasColumnName("F1_BD_Remark");
            entity.Property(e => e.F1Biopsy)
                .HasMaxLength(3)
                .IsUnicode(false)
                .HasColumnName("F1_Biopsy");
            entity.Property(e => e.F1BiopsyAttached)
                .HasMaxLength(3)
                .IsUnicode(false)
                .HasColumnName("F1_Biopsy_Attached");
            entity.Property(e => e.F1BiopsyDate)
                .HasColumnType("datetime")
                .HasColumnName("F1_Biopsy_Date");
            entity.Property(e => e.F1BiopsyRemark)
                .HasMaxLength(1000)
                .IsUnicode(false)
                .HasColumnName("F1_Biopsy_Remark");
            entity.Property(e => e.F1CPresent)
                .HasMaxLength(3)
                .IsUnicode(false)
                .HasColumnName("F1_C_Present");
            entity.Property(e => e.F1CRemark)
                .HasMaxLength(250)
                .IsUnicode(false)
                .HasColumnName("F1_C_Remark");
            entity.Property(e => e.F1CdPresent)
                .HasMaxLength(3)
                .IsUnicode(false)
                .HasColumnName("F1_CD_Present");
            entity.Property(e => e.F1CkdPresent)
                .HasMaxLength(3)
                .IsUnicode(false)
                .HasColumnName("F1_CKD_Present");
            entity.Property(e => e.F1CkdRemark)
                .HasMaxLength(250)
                .IsUnicode(false)
                .HasColumnName("F1_CKD_Remark");
            entity.Property(e => e.F1CldPresent)
                .HasMaxLength(3)
                .IsUnicode(false)
                .HasColumnName("F1_CLD_Present");
            entity.Property(e => e.F1CldRemark)
                .HasMaxLength(250)
                .IsUnicode(false)
                .HasColumnName("F1_CLD_Remark");
            entity.Property(e => e.F1CmoPresent)
                .HasMaxLength(3)
                .IsUnicode(false)
                .HasColumnName("F1_CMO_Present");
            entity.Property(e => e.F1CmoRemark)
                .HasMaxLength(250)
                .IsUnicode(false)
                .HasColumnName("F1_CMO_Remark");
            entity.Property(e => e.F1DbRemark)
                .HasMaxLength(250)
                .IsUnicode(false)
                .HasColumnName("F1_DB_Remark");
            entity.Property(e => e.F1Dbpresent)
                .HasMaxLength(3)
                .IsUnicode(false)
                .HasColumnName("F1_DBPresent");
            entity.Property(e => e.F1DdPresent)
                .HasMaxLength(3)
                .IsUnicode(false)
                .HasColumnName("F1_DD_Present");
            entity.Property(e => e.F1DdRemark)
                .HasMaxLength(250)
                .IsUnicode(false)
                .HasColumnName("F1_DD_Remark");
            entity.Property(e => e.F1DietModifications)
                .HasMaxLength(3)
                .IsUnicode(false)
                .HasColumnName("F1 Diet Modifications");
            entity.Property(e => e.F1Doesyourstomacheverfeelheavyaftermeals).HasColumnName("F1_Doesyourstomacheverfeelheavyaftermeals?");
            entity.Property(e => e.F1Doesyourstomachgetbloated).HasColumnName("F1_Doesyourstomachgetbloated?");
            entity.Property(e => e.F1Dosomethingsgetstuckwhenyouswallow).HasColumnName("F1_Dosomethingsgetstuckwhenyouswallow?");
            entity.Property(e => e.F1Doyouburpalot).HasColumnName("F1_Doyouburpalot?");
            entity.Property(e => e.F1Doyoueverfeelsickaftermeals).HasColumnName("F1_Doyoueverfeelsickaftermeals?");
            entity.Property(e => e.F1Doyoufeelfullwhileeatingmeals).HasColumnName("F1_Doyoufeelfullwhileeatingmeals?");
            entity.Property(e => e.F1DoyougetbitterliquidAcidComingupintoyourthroat).HasColumnName("F1_Doyougetbitterliquid(acid)comingupintoyourthroat?");
            entity.Property(e => e.F1Doyougetheartburn).HasColumnName("F1_Doyougetheartburn?");
            entity.Property(e => e.F1Doyougetheartburnaftermeals).HasColumnName("F1_Doyougetheartburnaftermeals?");
            entity.Property(e => e.F1Doyougetheartburnifyoubendover).HasColumnName("F1_Doyougetheartburnifyoubendover?");
            entity.Property(e => e.F1Doyouhaveanunusualsymptom).HasColumnName("F1_Doyouhaveanunusualsymptom");
            entity.Property(e => e.F1Doyousometimessubconsciouslyrubyourchestwithyourhand).HasColumnName("F1_Doyousometimessubconsciouslyrubyourchestwithyourhand?");
            entity.Property(e => e.F1DyspepticDysmotilitySymptom)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("F1_Dyspeptic(Dysmotility)symptom");
            entity.Property(e => e.F1H2blockersCDose)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("F1_H2BlockersC_Dose");
            entity.Property(e => e.F1H2blockersCFrequency)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("F1_H2BlockersC_Frequency");
            entity.Property(e => e.F1H2blockersCMedicationName)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("F1_H2BlockersC_Medication_Name");
            entity.Property(e => e.F1H2blockersDose)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("F1_H2Blockers_Dose");
            entity.Property(e => e.F1H2blockersFrequency)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("F1_H2Blockers_Frequency");
            entity.Property(e => e.F1H2blockersMedicationName)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("F1_H2Blockers_Medication_Name");
            entity.Property(e => e.F1HbDuration).HasColumnName("F1_HB_Duration");
            entity.Property(e => e.F1HbFrequency).HasColumnName("F1_HB_Frequency");
            entity.Property(e => e.F1HbNocturnal)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("F1_HB_Nocturnal");
            entity.Property(e => e.F1HbPostural)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("F1_HB_Postural");
            entity.Property(e => e.F1HeartburnHeartburn).HasColumnName("F1_HeartburnHeartburn");
            entity.Property(e => e.F1Hillsclassification)
                .HasMaxLength(3)
                .IsUnicode(false)
                .HasColumnName("F1_Hillsclassification");
            entity.Property(e => e.F1HillsclassificationGrade)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("F1_HillsclassificationGrade");
            entity.Property(e => e.F1HillsclassificationRemarks)
                .HasMaxLength(1000)
                .IsUnicode(false)
                .HasColumnName("F1_HillsclassificationRemarks");
            entity.Property(e => e.F1HtRemark)
                .HasMaxLength(250)
                .IsUnicode(false)
                .HasColumnName("F1_HT_Remark");
            entity.Property(e => e.F1HtdPresent)
                .HasMaxLength(3)
                .IsUnicode(false)
                .HasColumnName("F1_HTD_Present");
            entity.Property(e => e.F1HtdRemark)
                .HasMaxLength(250)
                .IsUnicode(false)
                .HasColumnName("F1_HTD_Remark");
            entity.Property(e => e.F1Htpresent)
                .HasMaxLength(3)
                .IsUnicode(false)
                .HasColumnName("F1_HTPresent");
            entity.Property(e => e.F1Laxlesclassification)
                .HasMaxLength(3)
                .IsUnicode(false)
                .HasColumnName("F1_LAXlesclassification");
            entity.Property(e => e.F1LosAngelesGrade)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("F1_LosAngelesGrade");
            entity.Property(e => e.F1LosAngelesGradeRemarks)
                .HasMaxLength(1000)
                .IsUnicode(false)
                .HasColumnName("F1_LosAngelesGradeRemarks");
            entity.Property(e => e.F1ManometryTest)
                .HasMaxLength(3)
                .IsUnicode(false)
                .HasColumnName("F1_ManometryTest");
            entity.Property(e => e.F1ManometryTestAttached)
                .HasMaxLength(3)
                .IsUnicode(false)
                .HasColumnName("F1_ManometryTest Attached");
            entity.Property(e => e.F1ModerationOfAlcohol)
                .HasMaxLength(3)
                .IsUnicode(false)
                .HasColumnName("F1 Moderation of alcohol");
            entity.Property(e => e.F1MtDate)
                .HasColumnType("datetime")
                .HasColumnName("F1_MT_Date");
            entity.Property(e => e.F1MtRemark)
                .HasMaxLength(1000)
                .IsUnicode(false)
                .HasColumnName("F1_MT_Remark");
            entity.Property(e => e.F1NdPresent)
                .HasMaxLength(3)
                .IsUnicode(false)
                .HasColumnName("F1_ND_Present");
            entity.Property(e => e.F1NdRemark)
                .HasMaxLength(250)
                .IsUnicode(false)
                .HasColumnName("F1_ND_Remark");
            entity.Property(e => e.F1OPresent)
                .HasMaxLength(3)
                .IsUnicode(false)
                .HasColumnName("F1_O_Present");
            entity.Property(e => e.F1ORemark)
                .HasMaxLength(250)
                .IsUnicode(false)
                .HasColumnName("F1_O_Remark");
            entity.Property(e => e.F1OthersDose)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("F1_others_Dose");
            entity.Property(e => e.F1OthersFrequency)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("F1_others_Frequency");
            entity.Property(e => e.F1OthersMedicationName)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("F1_others_Medication_Name");
            entity.Property(e => e.F1PHImpedanceMonitoring)
                .HasMaxLength(3)
                .IsUnicode(false)
                .HasColumnName("F1_pH Impedance monitoring");
            entity.Property(e => e.F1PHImpedanceMonitoringDate)
                .HasColumnType("datetime")
                .HasColumnName("F1_pH Impedance monitoring Date");
            entity.Property(e => e.F1PHImpedanceMonitoringRemarks)
                .HasMaxLength(1000)
                .IsUnicode(false)
                .HasColumnName("F1_pH Impedance monitoring Remarks");
            entity.Property(e => e.F1PHimReportAttached)
                .HasMaxLength(3)
                .IsUnicode(false)
                .HasColumnName("F1_pHIM Report Attached");
            entity.Property(e => e.F1PcabDose)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("F1_PCAB_Dose");
            entity.Property(e => e.F1PcabFrequency)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("F1_PCAB_Frequency");
            entity.Property(e => e.F1PcabMedicationName)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("F1_PCAB_Medication_Name");
            entity.Property(e => e.F1PpiDose)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("F1_PPI_Dose");
            entity.Property(e => e.F1PpiFrequency)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("F1_PPI_Frequency");
            entity.Property(e => e.F1PpiMedicationName)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("F1_PPI_Medication_Name");
            entity.Property(e => e.F1ProkineticsDose)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("F1_Prokinetics_Dose");
            entity.Property(e => e.F1ProkineticsFrequency)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("F1_Prokinetics_Frequency");
            entity.Property(e => e.F1ProkineticsMedicationName)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("F1_Prokinetics_Medication_Name");
            entity.Property(e => e.F1RDuration).HasColumnName("F1_R_Duration");
            entity.Property(e => e.F1RFrequency).HasColumnName("F1_R_Frequency");
            entity.Property(e => e.F1RNocturnal)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("F1_R_Nocturnal");
            entity.Property(e => e.F1RPostural)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("F1_R_Postural");
            entity.Property(e => e.F1RaPresent)
                .HasMaxLength(3)
                .IsUnicode(false)
                .HasColumnName("F1_RA_Present");
            entity.Property(e => e.F1RaRemark)
                .HasMaxLength(250)
                .IsUnicode(false)
                .HasColumnName("F1_RA_Remark");
            entity.Property(e => e.F1RegularExercise)
                .HasMaxLength(3)
                .IsUnicode(false)
                .HasColumnName("F1 Regular exercise");
            entity.Property(e => e.F1Regurgitation).HasColumnName("F1_Regurgitation");
            entity.Property(e => e.F1Retrosternalpain).HasColumnName("F1_Retrosternalpain");
            entity.Property(e => e.F1RpDuration).HasColumnName("F1_RP_Duration");
            entity.Property(e => e.F1RpFrequency).HasColumnName("F1_RP_Frequency");
            entity.Property(e => e.F1RpNocturnal)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("F1_RP_Nocturnal");
            entity.Property(e => e.F1RpPostural)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("F1_RP_Postural");
            entity.Property(e => e.F1SsPresent)
                .HasMaxLength(3)
                .IsUnicode(false)
                .HasColumnName("F1_SS_Present");
            entity.Property(e => e.F1SsRemark)
                .HasMaxLength(250)
                .IsUnicode(false)
                .HasColumnName("F1_SS_Remark");
            entity.Property(e => e.F1StopTobaccoUse)
                .HasMaxLength(3)
                .IsUnicode(false)
                .HasColumnName("F1 Stop Tobacco use");
            entity.Property(e => e.F1SucralfateDose)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("F1_Sucralfate_Dose");
            entity.Property(e => e.F1SucralfateFrequency)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("F1_Sucralfate_Frequency");
            entity.Property(e => e.F1SucralfateMedicationName)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("F1_Sucralfate_Medication_Name");
            entity.Property(e => e.F1SymtopmScore)
                .HasMaxLength(103)
                .IsUnicode(false)
                .HasColumnName("F1_SymtopmScore");
            entity.Property(e => e.F1TotalPoints)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("F1_TotalPoints");
            entity.Property(e => e.F1TotalSymptomScoreinGerdpatients).HasColumnName("F1_TotalSymptomScoreinGERDpatients");
            entity.Property(e => e.F1WeightLoss)
                .HasMaxLength(3)
                .IsUnicode(false)
                .HasColumnName("F1 Weight loss");
            entity.Property(e => e.F2APresent)
                .HasMaxLength(3)
                .IsUnicode(false)
                .HasColumnName("F2_A_Present");
            entity.Property(e => e.F2ARemark)
                .HasMaxLength(250)
                .IsUnicode(false)
                .HasColumnName("F2_A_Remark");
            entity.Property(e => e.F2AcidRefluxrelatedSymptom)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("F2_AcidRefluxrelatedSymptom");
            entity.Property(e => e.F2Acidtasteinmouth).HasColumnName("F2_Acidtasteinmouth");
            entity.Property(e => e.F2AlginateDose)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("F2_Alginate_Dose");
            entity.Property(e => e.F2AlginateFrequency)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("F2_Alginate_Frequency");
            entity.Property(e => e.F2AlginateMedicationName)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("F2_Alginate_Medication_Name");
            entity.Property(e => e.F2AtDuration).HasColumnName("F2_AT_Duration");
            entity.Property(e => e.F2AtFrequency).HasColumnName("F2_AT_Frequency");
            entity.Property(e => e.F2AtNocturnal)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("F2_AT_Nocturnal");
            entity.Property(e => e.F2AtPostural)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("F2_AT_Postural");
            entity.Property(e => e.F2BarrettsRemarks)
                .HasMaxLength(1000)
                .IsUnicode(false)
                .HasColumnName("F2_BarrettsRemarks");
            entity.Property(e => e.F2BdPresent)
                .HasMaxLength(3)
                .IsUnicode(false)
                .HasColumnName("F2_BD_Present");
            entity.Property(e => e.F2BdRemark)
                .HasMaxLength(250)
                .IsUnicode(false)
                .HasColumnName("F2_BD_Remark");
            entity.Property(e => e.F2Biopsy)
                .HasMaxLength(3)
                .IsUnicode(false)
                .HasColumnName("F2_Biopsy");
            entity.Property(e => e.F2BiopsyAttached)
                .HasMaxLength(3)
                .IsUnicode(false)
                .HasColumnName("F2_Biopsy_Attached");
            entity.Property(e => e.F2BiopsyDate)
                .HasColumnType("datetime")
                .HasColumnName("F2_Biopsy_Date");
            entity.Property(e => e.F2BiopsyRemark)
                .HasMaxLength(1000)
                .IsUnicode(false)
                .HasColumnName("F2_Biopsy_Remark");
            entity.Property(e => e.F2CPresent)
                .HasMaxLength(3)
                .IsUnicode(false)
                .HasColumnName("F2_C_Present");
            entity.Property(e => e.F2CRemark)
                .HasMaxLength(250)
                .IsUnicode(false)
                .HasColumnName("F2_C_Remark");
            entity.Property(e => e.F2CdPresent)
                .HasMaxLength(3)
                .IsUnicode(false)
                .HasColumnName("F2_CD_Present");
            entity.Property(e => e.F2CkdPresent)
                .HasMaxLength(3)
                .IsUnicode(false)
                .HasColumnName("F2_CKD_Present");
            entity.Property(e => e.F2CkdRemark)
                .HasMaxLength(250)
                .IsUnicode(false)
                .HasColumnName("F2_CKD_Remark");
            entity.Property(e => e.F2CldPresent)
                .HasMaxLength(3)
                .IsUnicode(false)
                .HasColumnName("F2_CLD_Present");
            entity.Property(e => e.F2CldRemark)
                .HasMaxLength(250)
                .IsUnicode(false)
                .HasColumnName("F2_CLD_Remark");
            entity.Property(e => e.F2CmoPresent)
                .HasMaxLength(3)
                .IsUnicode(false)
                .HasColumnName("F2_CMO_Present");
            entity.Property(e => e.F2CmoRemark)
                .HasMaxLength(250)
                .IsUnicode(false)
                .HasColumnName("F2_CMO_Remark");
            entity.Property(e => e.F2DbRemark)
                .HasMaxLength(250)
                .IsUnicode(false)
                .HasColumnName("F2_DB_Remark");
            entity.Property(e => e.F2Dbpresent)
                .HasMaxLength(3)
                .IsUnicode(false)
                .HasColumnName("F2_DBPresent");
            entity.Property(e => e.F2DdPresent)
                .HasMaxLength(3)
                .IsUnicode(false)
                .HasColumnName("F2_DD_Present");
            entity.Property(e => e.F2DdRemark)
                .HasMaxLength(250)
                .IsUnicode(false)
                .HasColumnName("F2_DD_Remark");
            entity.Property(e => e.F2DietModifications)
                .HasMaxLength(3)
                .IsUnicode(false)
                .HasColumnName("F2 Diet Modifications");
            entity.Property(e => e.F2Doesyourstomacheverfeelheavyaftermeals).HasColumnName("F2_Doesyourstomacheverfeelheavyaftermeals?");
            entity.Property(e => e.F2Doesyourstomachgetbloated).HasColumnName("F2_Doesyourstomachgetbloated?");
            entity.Property(e => e.F2Dosomethingsgetstuckwhenyouswallow).HasColumnName("F2_Dosomethingsgetstuckwhenyouswallow?");
            entity.Property(e => e.F2Doyouburpalot).HasColumnName("F2_Doyouburpalot?");
            entity.Property(e => e.F2Doyoueverfeelsickaftermeals).HasColumnName("F2_Doyoueverfeelsickaftermeals?");
            entity.Property(e => e.F2Doyoufeelfullwhileeatingmeals).HasColumnName("F2_Doyoufeelfullwhileeatingmeals?");
            entity.Property(e => e.F2DoyougetbitterliquidAcidComingupintoyourthroat).HasColumnName("F2_Doyougetbitterliquid(acid)comingupintoyourthroat?");
            entity.Property(e => e.F2Doyougetheartburn).HasColumnName("F2_Doyougetheartburn?");
            entity.Property(e => e.F2Doyougetheartburnaftermeals).HasColumnName("F2_Doyougetheartburnaftermeals?");
            entity.Property(e => e.F2Doyougetheartburnifyoubendover).HasColumnName("F2_Doyougetheartburnifyoubendover?");
            entity.Property(e => e.F2Doyouhaveanunusualsymptom).HasColumnName("F2_Doyouhaveanunusualsymptom");
            entity.Property(e => e.F2Doyousometimessubconsciouslyrubyourchestwithyourhand).HasColumnName("F2_Doyousometimessubconsciouslyrubyourchestwithyourhand?");
            entity.Property(e => e.F2DyspepticDysmotilitySymptom)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("F2_Dyspeptic(Dysmotility)symptom");
            entity.Property(e => e.F2H2blockersCDose)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("F2_H2BlockersC_Dose");
            entity.Property(e => e.F2H2blockersCFrequency)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("F2_H2BlockersC_Frequency");
            entity.Property(e => e.F2H2blockersCMedicationName)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("F2_H2BlockersC_Medication_Name");
            entity.Property(e => e.F2H2blockersDose)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("F2_H2Blockers_Dose");
            entity.Property(e => e.F2H2blockersFrequency)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("F2_H2Blockers_Frequency");
            entity.Property(e => e.F2H2blockersMedicationName)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("F2_H2Blockers_Medication_Name");
            entity.Property(e => e.F2HbDuration).HasColumnName("F2_HB_Duration");
            entity.Property(e => e.F2HbFrequency).HasColumnName("F2_HB_Frequency");
            entity.Property(e => e.F2HbNocturnal)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("F2_HB_Nocturnal");
            entity.Property(e => e.F2HbPostural)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("F2_HB_Postural");
            entity.Property(e => e.F2HeartburnHeartburn).HasColumnName("F2_HeartburnHeartburn");
            entity.Property(e => e.F2Hillsclassification)
                .HasMaxLength(3)
                .IsUnicode(false)
                .HasColumnName("F2_Hillsclassification");
            entity.Property(e => e.F2HillsclassificationGrade)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("F2_HillsclassificationGrade");
            entity.Property(e => e.F2HillsclassificationRemarks)
                .HasMaxLength(1000)
                .IsUnicode(false)
                .HasColumnName("F2_HillsclassificationRemarks");
            entity.Property(e => e.F2HtRemark)
                .HasMaxLength(250)
                .IsUnicode(false)
                .HasColumnName("F2_HT_Remark");
            entity.Property(e => e.F2HtdPresent)
                .HasMaxLength(3)
                .IsUnicode(false)
                .HasColumnName("F2_HTD_Present");
            entity.Property(e => e.F2HtdRemark)
                .HasMaxLength(250)
                .IsUnicode(false)
                .HasColumnName("F2_HTD_Remark");
            entity.Property(e => e.F2Htpresent)
                .HasMaxLength(3)
                .IsUnicode(false)
                .HasColumnName("F2_HTPresent");
            entity.Property(e => e.F2Laxlesclassification)
                .HasMaxLength(3)
                .IsUnicode(false)
                .HasColumnName("F2_LAXlesclassification");
            entity.Property(e => e.F2LosAngelesGrade)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("F2_LosAngelesGrade");
            entity.Property(e => e.F2LosAngelesGradeRemarks)
                .HasMaxLength(1000)
                .IsUnicode(false)
                .HasColumnName("F2_LosAngelesGradeRemarks");
            entity.Property(e => e.F2ManometryTest)
                .HasMaxLength(3)
                .IsUnicode(false)
                .HasColumnName("F2_ManometryTest");
            entity.Property(e => e.F2ManometryTestAttached)
                .HasMaxLength(3)
                .IsUnicode(false)
                .HasColumnName("F2_ManometryTest Attached");
            entity.Property(e => e.F2ModerationOfAlcohol)
                .HasMaxLength(3)
                .IsUnicode(false)
                .HasColumnName("F2 Moderation of alcohol");
            entity.Property(e => e.F2MtDate)
                .HasColumnType("datetime")
                .HasColumnName("F2_MT_Date");
            entity.Property(e => e.F2MtRemark)
                .HasMaxLength(1000)
                .IsUnicode(false)
                .HasColumnName("F2_MT_Remark");
            entity.Property(e => e.F2NdPresent)
                .HasMaxLength(3)
                .IsUnicode(false)
                .HasColumnName("F2_ND_Present");
            entity.Property(e => e.F2NdRemark)
                .HasMaxLength(250)
                .IsUnicode(false)
                .HasColumnName("F2_ND_Remark");
            entity.Property(e => e.F2OPresent)
                .HasMaxLength(3)
                .IsUnicode(false)
                .HasColumnName("F2_O_Present");
            entity.Property(e => e.F2ORemark)
                .HasMaxLength(250)
                .IsUnicode(false)
                .HasColumnName("F2_O_Remark");
            entity.Property(e => e.F2OthersDose)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("F2_others_Dose");
            entity.Property(e => e.F2OthersFrequency)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("F2_others_Frequency");
            entity.Property(e => e.F2OthersMedicationName)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("F2_others_Medication_Name");
            entity.Property(e => e.F2PHImpedanceMonitoring)
                .HasMaxLength(3)
                .IsUnicode(false)
                .HasColumnName("F2_pH Impedance monitoring");
            entity.Property(e => e.F2PHImpedanceMonitoringDate)
                .HasColumnType("datetime")
                .HasColumnName("F2_pH Impedance monitoring Date");
            entity.Property(e => e.F2PHImpedanceMonitoringRemarks)
                .HasMaxLength(1000)
                .IsUnicode(false)
                .HasColumnName("F2_pH Impedance monitoring Remarks");
            entity.Property(e => e.F2PHimReportAttached)
                .HasMaxLength(3)
                .IsUnicode(false)
                .HasColumnName("F2_pHIM Report Attached");
            entity.Property(e => e.F2PcabDose)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("F2_PCAB_Dose");
            entity.Property(e => e.F2PcabFrequency)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("F2_PCAB_Frequency");
            entity.Property(e => e.F2PcabMedicationName)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("F2_PCAB_Medication_Name");
            entity.Property(e => e.F2PpiDose)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("F2_PPI_Dose");
            entity.Property(e => e.F2PpiFrequency)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("F2_PPI_Frequency");
            entity.Property(e => e.F2PpiMedicationName)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("F2_PPI_Medication_Name");
            entity.Property(e => e.F2ProkineticsDose)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("F2_Prokinetics_Dose");
            entity.Property(e => e.F2ProkineticsFrequency)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("F2_Prokinetics_Frequency");
            entity.Property(e => e.F2ProkineticsMedicationName)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("F2_Prokinetics_Medication_Name");
            entity.Property(e => e.F2RDuration).HasColumnName("F2_R_Duration");
            entity.Property(e => e.F2RFrequency).HasColumnName("F2_R_Frequency");
            entity.Property(e => e.F2RNocturnal)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("F2_R_Nocturnal");
            entity.Property(e => e.F2RPostural)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("F2_R_Postural");
            entity.Property(e => e.F2RaPresent)
                .HasMaxLength(3)
                .IsUnicode(false)
                .HasColumnName("F2_RA_Present");
            entity.Property(e => e.F2RaRemark)
                .HasMaxLength(250)
                .IsUnicode(false)
                .HasColumnName("F2_RA_Remark");
            entity.Property(e => e.F2RegularExercise)
                .HasMaxLength(3)
                .IsUnicode(false)
                .HasColumnName("F2 Regular exercise");
            entity.Property(e => e.F2Regurgitation).HasColumnName("F2_Regurgitation");
            entity.Property(e => e.F2Retrosternalpain).HasColumnName("F2_Retrosternalpain");
            entity.Property(e => e.F2RpDuration).HasColumnName("F2_RP_Duration");
            entity.Property(e => e.F2RpFrequency).HasColumnName("F2_RP_Frequency");
            entity.Property(e => e.F2RpNocturnal)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("F2_RP_Nocturnal");
            entity.Property(e => e.F2RpPostural)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("F2_RP_Postural");
            entity.Property(e => e.F2SsPresent)
                .HasMaxLength(3)
                .IsUnicode(false)
                .HasColumnName("F2_SS_Present");
            entity.Property(e => e.F2SsRemark)
                .HasMaxLength(250)
                .IsUnicode(false)
                .HasColumnName("F2_SS_Remark");
            entity.Property(e => e.F2StopTobaccoUse)
                .HasMaxLength(3)
                .IsUnicode(false)
                .HasColumnName("F2 Stop Tobacco use");
            entity.Property(e => e.F2SucralfateDose)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("F2_Sucralfate_Dose");
            entity.Property(e => e.F2SucralfateFrequency)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("F2_Sucralfate_Frequency");
            entity.Property(e => e.F2SucralfateMedicationName)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("F2_Sucralfate_Medication_Name");
            entity.Property(e => e.F2SymtopmScore)
                .HasMaxLength(103)
                .IsUnicode(false)
                .HasColumnName("F2_SymtopmScore");
            entity.Property(e => e.F2TotalPoints)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("F2_TotalPoints");
            entity.Property(e => e.F2TotalSymptomScoreinGerdpatients).HasColumnName("F2_TotalSymptomScoreinGERDpatients");
            entity.Property(e => e.F2WeightLoss)
                .HasMaxLength(3)
                .IsUnicode(false)
                .HasColumnName("F2 Weight loss");
            entity.Property(e => e.FamilyHistoryOfEsophagoGastricCancer)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("Family History of Esophago-gastric Cancer");
            entity.Property(e => e.FamilyHistoryOfGerd)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("Family History of GERD");
            entity.Property(e => e.FamilyIncome)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Frequency)
                .HasMaxLength(1)
                .IsUnicode(false);
            entity.Property(e => e.FundoplicationRemarks)
                .HasMaxLength(1000)
                .IsUnicode(false)
                .HasColumnName("Fundoplication Remarks");
            entity.Property(e => e.FundoplicationSurgery)
                .HasMaxLength(3)
                .IsUnicode(false)
                .HasColumnName("Fundoplication Surgery");
            entity.Property(e => e.GastricPoemRemarks)
                .HasMaxLength(1000)
                .IsUnicode(false)
                .HasColumnName("Gastric POEM Remarks");
            entity.Property(e => e.GastricPoemSurgery)
                .HasMaxLength(3)
                .IsUnicode(false)
                .HasColumnName("Gastric POEM Surgery");
            entity.Property(e => e.Gastrojejunostomy)
                .HasMaxLength(3)
                .IsUnicode(false);
            entity.Property(e => e.GastrojejunostomyRemarks)
                .HasMaxLength(1000)
                .IsUnicode(false)
                .HasColumnName("Gastrojejunostomy Remarks");
            entity.Property(e => e.Gender)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.GeneralAppearance)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("General Appearance");
            entity.Property(e => e.GeneralAppearanceComments)
                .HasMaxLength(1000)
                .IsUnicode(false)
                .HasColumnName("General Appearance – Comments");
            entity.Property(e => e.GerdRemarks)
                .HasMaxLength(250)
                .IsUnicode(false)
                .HasColumnName("GERD - Remarks");
            entity.Property(e => e.Gerdtype)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("GERDType");
            entity.Property(e => e.GredNoOfYear).HasColumnName("GRED_NoOfYear");
            entity.Property(e => e.Gym)
                .HasMaxLength(3)
                .IsUnicode(false);
            entity.Property(e => e.GymDurationYrs)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("Gym Duration (yrs)");
            entity.Property(e => e.GymFrequencyHrsWeek)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("Gym Frequency (hrs/week)");
            entity.Property(e => e.H2blockersCDose)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("H2BlockersC_Dose");
            entity.Property(e => e.H2blockersCFrequency)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("H2BlockersC_Frequency");
            entity.Property(e => e.H2blockersCMedicationName)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("H2BlockersC_Medication_Name");
            entity.Property(e => e.H2blockersDose)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("H2Blockers_Dose");
            entity.Property(e => e.H2blockersFrequency)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("H2Blockers_Frequency");
            entity.Property(e => e.H2blockersMedicationName)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("H2Blockers_Medication_Name");
            entity.Property(e => e.HeartburnDurationYrs).HasColumnName("HeartburnDuration[Yrs)");
            entity.Property(e => e.HeartburnFrequencyWk).HasColumnName("HeartburnFrequency(/Wk)");
            entity.Property(e => e.HeartburnNocturnal)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.HeartburnPostural)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.HeightInCms)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("Height (in cms)");
            entity.Property(e => e.HillSClassification)
                .HasMaxLength(3)
                .IsUnicode(false)
                .HasColumnName("Hill’s classification");
            entity.Property(e => e.HillSClassificationGrade)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("Hill’s classification Grade");
            entity.Property(e => e.HillSClassificationRemarks)
                .HasMaxLength(1000)
                .IsUnicode(false)
                .HasColumnName("Hill’s classification Remarks");
            entity.Property(e => e.HistoryOfEndoscopy)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("History of Endoscopy");
            entity.Property(e => e.HistoryOfGastroSurgery)
                .HasMaxLength(3)
                .IsUnicode(false)
                .HasColumnName("History of Gastro-surgery");
            entity.Property(e => e.Hypertension)
                .HasMaxLength(3)
                .IsUnicode(false);
            entity.Property(e => e.HypertensionRemarks)
                .HasMaxLength(250)
                .IsUnicode(false)
                .HasColumnName("Hypertension Remarks");
            entity.Property(e => e.Hyperthyroidism)
                .HasMaxLength(3)
                .IsUnicode(false);
            entity.Property(e => e.HyperthyroidismRemarks)
                .HasMaxLength(250)
                .IsUnicode(false)
                .HasColumnName("Hyperthyroidism Remarks");
            entity.Property(e => e.Hypothyroidism)
                .HasMaxLength(3)
                .IsUnicode(false);
            entity.Property(e => e.HypothyroidismRemarks)
                .HasMaxLength(250)
                .IsUnicode(false)
                .HasColumnName("Hypothyroidism Remarks");
            entity.Property(e => e.Initial)
                .HasMaxLength(250)
                .IsUnicode(false);
            entity.Property(e => e.JobOccupationType)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("Job/ Occupation type");
            entity.Property(e => e.Jogging)
                .HasMaxLength(3)
                .IsUnicode(false);
            entity.Property(e => e.JoggingDurationYrs)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("Jogging Duration (yrs)");
            entity.Property(e => e.JoggingFrequencyHrsWeek)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("Jogging Frequency (hrs/week)");
            entity.Property(e => e.KnownCaseOfGerd).HasColumnName("KnownCaseOfGERD");
            entity.Property(e => e.Laxlesclassification)
                .HasMaxLength(3)
                .IsUnicode(false)
                .HasColumnName("LAXlesclassification");
            entity.Property(e => e.LosAngelesGrade)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("Los Angeles Grade");
            entity.Property(e => e.LosAngelesGradeRemarks)
                .HasMaxLength(1000)
                .IsUnicode(false)
                .HasColumnName("Los Angeles Grade Remarks");
            entity.Property(e => e.ModerationOfAlcohol)
                .HasMaxLength(3)
                .IsUnicode(false)
                .HasColumnName("Moderation of alcohol");
            entity.Property(e => e.MtAttached).HasColumnName("MT_Attached");
            entity.Property(e => e.MtDate)
                .HasColumnType("datetime")
                .HasColumnName("MT_Date");
            entity.Property(e => e.MtRemark)
                .HasMaxLength(1000)
                .IsUnicode(false)
                .HasColumnName("MT_Remark");
            entity.Property(e => e.NeurologicalDisorder)
                .HasMaxLength(3)
                .IsUnicode(false)
                .HasColumnName("Neurological Disorder");
            entity.Property(e => e.NeurologicalDisorderRemarks)
                .HasMaxLength(250)
                .IsUnicode(false)
                .HasColumnName("Neurological Disorder Remarks");
            entity.Property(e => e.NsaidsDose)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("NSAIDs - Dose");
            entity.Property(e => e.NsaidsFrequency)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("NSAIDs - Frequency");
            entity.Property(e => e.NsaidsMoleculeName)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("NSAIDs - Molecule Name");
            entity.Property(e => e.Occupation)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Osteoarthritis)
                .HasMaxLength(3)
                .IsUnicode(false);
            entity.Property(e => e.OsteoarthritisRemarks)
                .HasMaxLength(250)
                .IsUnicode(false)
                .HasColumnName("Osteoarthritis Remarks");
            entity.Property(e => e.OtherComorbidity)
                .HasMaxLength(3)
                .IsUnicode(false)
                .HasColumnName("Other Comorbidity");
            entity.Property(e => e.OtherComorbiditySpecify)
                .HasMaxLength(250)
                .IsUnicode(false)
                .HasColumnName("Other Comorbidity- Specify");
            entity.Property(e => e.OtherDose)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("Other - Dose");
            entity.Property(e => e.OtherDrugMoleculeName)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("Other Drug - Molecule Name");
            entity.Property(e => e.OtherExamAreaSpecify)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("Other Exam Area (Specify)");
            entity.Property(e => e.OtherExamComments)
                .HasMaxLength(1000)
                .IsUnicode(false)
                .HasColumnName("Other Exam – Comments");
            entity.Property(e => e.OtherExamStatus)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("Other Exam – Status");
            entity.Property(e => e.OtherExercise)
                .HasMaxLength(3)
                .IsUnicode(false)
                .HasColumnName("Other Exercise");
            entity.Property(e => e.OtherExerciseDurationYrs)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("Other Exercise Duration (yrs)");
            entity.Property(e => e.OtherExerciseFrequencyHrsWeek)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("Other Exercise Frequency (hrs/week)");
            entity.Property(e => e.OtherFrequency)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("Other - Frequency");
            entity.Property(e => e.OtherGastroSurgery)
                .HasMaxLength(3)
                .IsUnicode(false)
                .HasColumnName("Other Gastro Surgery");
            entity.Property(e => e.OtherGastroSurgeryRemarks)
                .HasMaxLength(1000)
                .IsUnicode(false)
                .HasColumnName("Other Gastro Surgery Remarks");
            entity.Property(e => e.Otherdose1)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("otherdose");
            entity.Property(e => e.Otherfrequency1)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("otherfrequency");
            entity.Property(e => e.OthersMedicationName)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("others_Medication_Name");
            entity.Property(e => e.PHImpedanceMonitoring)
                .HasMaxLength(3)
                .IsUnicode(false)
                .HasColumnName("pH Impedance monitoring");
            entity.Property(e => e.PHImpedanceMonitoringDate)
                .HasColumnType("datetime")
                .HasColumnName("pH Impedance monitoring Date");
            entity.Property(e => e.PHImpedanceMonitoringRemarks)
                .HasMaxLength(1000)
                .IsUnicode(false)
                .HasColumnName("pH Impedance monitoring Remarks");
            entity.Property(e => e.PHimReportAttached)
                .HasMaxLength(3)
                .IsUnicode(false)
                .HasColumnName("pHIM Report Attached");
            entity.Property(e => e.PastHistory).HasColumnName("Past History");
            entity.Property(e => e.PcabDose)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("PCAB_Dose");
            entity.Property(e => e.PcabFrequency)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("PCAB_Frequency");
            entity.Property(e => e.PcabMedicationName)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("PCAB_Medication_Name");
            entity.Property(e => e.PerAbdomenExaminationFindings)
                .HasMaxLength(1000)
                .IsUnicode(false)
                .HasColumnName("Per Abdomen Examination Findings");
            entity.Property(e => e.Pincode).HasColumnName("pincode");
            entity.Property(e => e.PlaceType)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.PpiDose)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("PPI_Dose");
            entity.Property(e => e.PpiFrequency)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("PPI_Frequency");
            entity.Property(e => e.PpiMedicationName)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("PPI_Medication_Name");
            entity.Property(e => e.PpiUsage)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("PPI Usage");
            entity.Property(e => e.ProkineticsDose)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("Prokinetics_Dose");
            entity.Property(e => e.ProkineticsFrequency)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("Prokinetics_Frequency");
            entity.Property(e => e.ProkineticsMedicationName)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("Prokinetics_Medication_Name");
            entity.Property(e => e.RefractoryToPpi).HasColumnName("RefractoryToPPI");
            entity.Property(e => e.RegularExercise)
                .HasMaxLength(3)
                .IsUnicode(false)
                .HasColumnName("Regular exercise");
            entity.Property(e => e.RegurgitationDurationYrs).HasColumnName("RegurgitationDuration (Yrs)");
            entity.Property(e => e.RegurgitationFrequencyWk).HasColumnName("RegurgitationFrequency(/Wk)");
            entity.Property(e => e.RegurgitationNocturnal)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.RegurgitationPostural)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.ReportAttached)
                .HasMaxLength(3)
                .IsUnicode(false)
                .HasColumnName("Report Attached");
            entity.Property(e => e.RespiratorySystem)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("Respiratory System");
            entity.Property(e => e.RespiratorySystemComments)
                .HasMaxLength(1000)
                .IsUnicode(false)
                .HasColumnName("Respiratory System – Comments");
            entity.Property(e => e.RetrosternalNocturnal)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("Retrosternal Nocturnal");
            entity.Property(e => e.RetrosternalPainDurationYrs).HasColumnName("RetrosternalPainDuration (Yrs)");
            entity.Property(e => e.RetrosternalPainFrequencyWk).HasColumnName("RetrosternalPainFrequency(/Wk)");
            entity.Property(e => e.RetrosternalPostural)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("Retrosternal Postural");
            entity.Property(e => e.RheumatoidArthritis)
                .HasMaxLength(3)
                .IsUnicode(false)
                .HasColumnName("Rheumatoid Arthritis");
            entity.Property(e => e.RheumatoidArthritisRemrks)
                .HasMaxLength(250)
                .IsUnicode(false)
                .HasColumnName("Rheumatoid Arthritis Remrks");
            entity.Property(e => e.SleepApnea)
                .HasMaxLength(3)
                .IsUnicode(false)
                .HasColumnName("Sleep Apnea");
            entity.Property(e => e.SleepApneaDurationYears)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("Sleep Apnea Duration (years)");
            entity.Property(e => e.SleepApneaFrequencyWeek)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("Sleep Apnea Frequency (/week)");
            entity.Property(e => e.SmartphoneUsageDurationYears).HasColumnName("Smartphone Usage Duration (years)");
            entity.Property(e => e.SmartphoneUsageHrsDay)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("Smartphone Usage (hrs/day)");
            entity.Property(e => e.SmartphoneUse)
                .HasMaxLength(3)
                .IsUnicode(false)
                .HasColumnName("Smartphone Use");
            entity.Property(e => e.Smoking)
                .HasMaxLength(3)
                .IsUnicode(false);
            entity.Property(e => e.SmokingDurationYrs)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("Smoking Duration (yrs)");
            entity.Property(e => e.SmokingFrequencyDay)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("Smoking Frequency (/day)");
            entity.Property(e => e.SmokingQuantityPacks)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("Smoking Quantity (packs)");
            entity.Property(e => e.SocioeconomicStatus)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.SpicyFood)
                .HasMaxLength(3)
                .IsUnicode(false)
                .HasColumnName("Spicy Food");
            entity.Property(e => e.SpicyFoodDurationYrs)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("Spicy Food Duration (yrs)");
            entity.Property(e => e.SpicyFoodFrequencyWeek)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("Spicy Food Frequency(/week)");
            entity.Property(e => e.SpicyFoodQuantity)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("Spicy Food Quantity");
            entity.Property(e => e.StateName).HasMaxLength(50);
            entity.Property(e => e.SteroidsDose)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("Steroids - Dose");
            entity.Property(e => e.SteroidsFrequency)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("Steroids - Frequency");
            entity.Property(e => e.SteroidsMoleculeName)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("Steroids - Molecule Name");
            entity.Property(e => e.StopTobaccoUse)
                .HasMaxLength(3)
                .IsUnicode(false)
                .HasColumnName("Stop Tobacco use");
            entity.Property(e => e.SubjectNo)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.SucralfateDose)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("Sucralfate_Dose");
            entity.Property(e => e.SucralfateFrequency)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("Sucralfate_Frequency");
            entity.Property(e => e.SucralfateMedicationName)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("Sucralfate_Medication_Name");
            entity.Property(e => e.SymtopmScore)
                .HasMaxLength(103)
                .IsUnicode(false);
            entity.Property(e => e.SystemicSclerosis)
                .HasMaxLength(3)
                .IsUnicode(false)
                .HasColumnName("Systemic Sclerosis");
            entity.Property(e => e.SystemicSclerosisRemarks)
                .HasMaxLength(250)
                .IsUnicode(false)
                .HasColumnName("Systemic Sclerosis Remarks");
            entity.Property(e => e.Tea)
                .HasMaxLength(3)
                .IsUnicode(false);
            entity.Property(e => e.TeaDurationYrs)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("Tea Duration(yrs)");
            entity.Property(e => e.TeaFrequencyDay)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("Tea Frequency(/day)");
            entity.Property(e => e.TeaQuantityMl)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("Tea Quantity(ml)");
            entity.Property(e => e.TobaccoInOtherForms)
                .HasMaxLength(3)
                .IsUnicode(false)
                .HasColumnName("Tobacco in other forms");
            entity.Property(e => e.TobaccoInOtherFormsDurationYrs)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("Tobacco in other forms Duration (yrs)");
            entity.Property(e => e.TobaccoInOtherFormsFrequencyDay)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("Tobacco in other forms Frequency (/day)");
            entity.Property(e => e.TobaccoInOtherFormsQuantity)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("Tobacco in other forms Quantity");
            entity.Property(e => e.TotalPoints)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.TotalSymptomScoreinGerdpatients).HasColumnName("TotalSymptomScoreinGERDpatients");
            entity.Property(e => e.Walking)
                .HasMaxLength(3)
                .IsUnicode(false);
            entity.Property(e => e.WalkingDurationYrs)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("Walking Duration (yrs)");
            entity.Property(e => e.WalkingFrequencyHrsWeek)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("Walking Frequency (hrs/week)");
            entity.Property(e => e.WeightInKg)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("Weight (in Kg)");
            entity.Property(e => e.WeightLoss)
                .HasMaxLength(3)
                .IsUnicode(false)
                .HasColumnName("Weight loss");
            entity.Property(e => e.WorkingHoursOccupation)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("Working Hours (Occupation)");
            entity.Property(e => e.Yoga)
                .HasMaxLength(3)
                .IsUnicode(false);
            entity.Property(e => e.YogaDurationYrs)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("Yoga Duration (yrs)");
            entity.Property(e => e.YogaFrequencyHrsWeek)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("Yoga Frequency (hrs/week)");
            entity.Property(e => e.Zumba)
                .HasMaxLength(3)
                .IsUnicode(false);
            entity.Property(e => e.ZumbaDurationYrs)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("Zumba Duration (yrs)");
            entity.Property(e => e.ZumbaFrequencyHrsWeek)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("Zumba Frequency (hrs/week)");
        });

        modelBuilder.Entity<VwCurrentMedication>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("vw_CurrentMedication");

            entity.Property(e => e.AntiplateletDose)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("Antiplatelet_Dose");
            entity.Property(e => e.AntiplateletFrequency)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("Antiplatelet_Frequency");
            entity.Property(e => e.AntiplateletMolecule)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("Antiplatelet_Molecule");
            entity.Property(e => e.BisphosphonatesDose)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("Bisphosphonates_Dose");
            entity.Property(e => e.BisphosphonatesFrequency)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("Bisphosphonates_Frequency");
            entity.Property(e => e.BisphosphonatesMolecule)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("Bisphosphonates_Molecule");
            entity.Property(e => e.CreatedDt).HasColumnType("datetime");
            entity.Property(e => e.Id).ValueGeneratedOnAdd();
            entity.Property(e => e.ModifiedDt).HasColumnType("datetime");
            entity.Property(e => e.NsaidsDose)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("NSAIDs_Dose");
            entity.Property(e => e.NsaidsFrequency)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("NSAIDs_Frequency");
            entity.Property(e => e.NsaidsMolecule)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("NSAIDs_Molecule");
            entity.Property(e => e.OthersDose)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("Others_Dose");
            entity.Property(e => e.OthersFrequency)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("Others_Frequency");
            entity.Property(e => e.OthersMolecule)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("Others_Molecule");
            entity.Property(e => e.SteroidsDose)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("Steroids_Dose");
            entity.Property(e => e.SteroidsFrequency)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("Steroids_Frequency");
            entity.Property(e => e.SteroidsMolecule)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("Steroids_Molecule");
        });

        modelBuilder.Entity<VwDiagnosis>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("vw_Diagnosis");

            entity.Property(e => e.CreatedByName)
                .HasMaxLength(250)
                .IsUnicode(false);
            entity.Property(e => e.CreatedDt).HasColumnType("datetime");
            entity.Property(e => e.DiagnosisId).HasColumnName("DiagnosisID");
            entity.Property(e => e.DoctorId).HasColumnName("DoctorID");
            entity.Property(e => e.Gerdtype)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("GERDType");
            entity.Property(e => e.GredNoOfYear).HasColumnName("GRED_NoOfYear");
            entity.Property(e => e.KnownCaseOfGerd).HasColumnName("KnownCaseOfGERD");
            entity.Property(e => e.ModifiedByName)
                .HasMaxLength(250)
                .IsUnicode(false);
            entity.Property(e => e.ModifiedDt).HasColumnType("datetime");
            entity.Property(e => e.PatientId).HasColumnName("PatientID");
            entity.Property(e => e.RefractoryToPpi).HasColumnName("RefractoryToPPI");
        });

        modelBuilder.Entity<VwDoctor>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("vw_Doctor");

            entity.Property(e => e.CreatedDt).HasColumnType("datetime");
            entity.Property(e => e.DoctorId)
                .ValueGeneratedOnAdd()
                .HasColumnName("DoctorID");
            entity.Property(e => e.Email)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.EnterCodeNo)
                .HasMaxLength(250)
                .IsUnicode(false)
                .HasColumnName("EnterCodeNO");
            entity.Property(e => e.HospitalName)
                .HasMaxLength(250)
                .IsUnicode(false);
            entity.Property(e => e.Mcicode)
                .HasMaxLength(250)
                .IsUnicode(false)
                .HasColumnName("MCICode");
            entity.Property(e => e.ModifiedDt).HasColumnType("datetime");
            entity.Property(e => e.Name)
                .HasMaxLength(250)
                .IsUnicode(false);
            entity.Property(e => e.Password)
                .HasMaxLength(250)
                .IsUnicode(false);
            entity.Property(e => e.PhoneNo)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("PhoneNO");
            entity.Property(e => e.PlaceOfPractice).HasMaxLength(250);
            entity.Property(e => e.Status)
                .HasMaxLength(50)
                .IsUnicode(false);
        });

        modelBuilder.Entity<VwDoctorLog>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("vw_DoctorLogs");

            entity.Property(e => e.CreatedDt).HasColumnType("datetime");
            entity.Property(e => e.DoctorId).HasColumnName("DoctorID");
            entity.Property(e => e.DoctorlogId)
                .ValueGeneratedOnAdd()
                .HasColumnName("DoctorlogID");
            entity.Property(e => e.LoginTime).HasColumnType("datetime");
            entity.Property(e => e.LogoutTime).HasColumnType("datetime");
            entity.Property(e => e.ModifiedDt).HasColumnType("datetime");
            entity.Property(e => e.Token).HasMaxLength(2000);
        });

        modelBuilder.Entity<VwDoctorRpt>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("vw_DoctorRPT");

            entity.Property(e => e.City).HasMaxLength(100);
            entity.Property(e => e.CreatedDt).HasColumnType("datetime");
            entity.Property(e => e.DoctorId).HasColumnName("DoctorID");
            entity.Property(e => e.Email)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.EnterCodeNo)
                .HasMaxLength(250)
                .IsUnicode(false)
                .HasColumnName("EnterCodeNO");
            entity.Property(e => e.HospitalName)
                .HasMaxLength(250)
                .IsUnicode(false);
            entity.Property(e => e.Mcicode)
                .HasMaxLength(250)
                .IsUnicode(false)
                .HasColumnName("MCICode");
            entity.Property(e => e.ModifiedDt).HasColumnType("datetime");
            entity.Property(e => e.Name)
                .HasMaxLength(250)
                .IsUnicode(false);
            entity.Property(e => e.Password)
                .HasMaxLength(250)
                .IsUnicode(false);
            entity.Property(e => e.PhoneNo)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("PhoneNO");
            entity.Property(e => e.PlaceOfPractice).HasMaxLength(250);
            entity.Property(e => e.State).HasMaxLength(50);
            entity.Property(e => e.Status)
                .HasMaxLength(50)
                .IsUnicode(false);
        });

        modelBuilder.Entity<VwExercise>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("vw_Exercise");

            entity.Property(e => e.AerobicsDuration)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("Aerobics_Duration");
            entity.Property(e => e.AerobicsFrequency)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("Aerobics_Frequency");
            entity.Property(e => e.AerobicsOption)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("Aerobics_Option");
            entity.Property(e => e.CreatedDt).HasColumnType("datetime");
            entity.Property(e => e.DoctorId).HasColumnName("DoctorID");
            entity.Property(e => e.ExerciseId)
                .ValueGeneratedOnAdd()
                .HasColumnName("ExerciseID");
            entity.Property(e => e.GymDuration)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("Gym_Duration");
            entity.Property(e => e.GymFrequency)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("Gym_Frequency");
            entity.Property(e => e.GymOption)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("Gym_Option");
            entity.Property(e => e.JoggingDuration)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("Jogging_Duration");
            entity.Property(e => e.JoggingFrequency)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("Jogging_Frequency");
            entity.Property(e => e.JoggingOption)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("Jogging_Option");
            entity.Property(e => e.ModifiedDt).HasColumnType("datetime");
            entity.Property(e => e.OthersDuration)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("Others_Duration");
            entity.Property(e => e.OthersFrequency)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("Others_Frequency");
            entity.Property(e => e.OthersOption)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("Others_Option");
            entity.Property(e => e.PatientId).HasColumnName("PatientID");
            entity.Property(e => e.Status)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.WalkingDuration)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("Walking_Duration");
            entity.Property(e => e.WalkingFrequency)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("Walking_Frequency");
            entity.Property(e => e.WalkingOption)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("Walking_Option");
            entity.Property(e => e.YogaDuration)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("Yoga_Duration");
            entity.Property(e => e.YogaFrequency)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("Yoga_Frequency");
            entity.Property(e => e.YogaOption)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("Yoga_Option");
            entity.Property(e => e.ZumbaDuration)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("Zumba_Duration");
            entity.Property(e => e.ZumbaFrequency)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("Zumba_Frequency");
            entity.Property(e => e.ZumbaOption)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("Zumba_Option");
        });

        modelBuilder.Entity<VwFamilyHistory>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("vw_FamilyHistory");

            entity.Property(e => e.CreatedDt).HasColumnType("datetime");
            entity.Property(e => e.DoctorId).HasColumnName("DoctorID");
            entity.Property(e => e.FamilyHistoryId)
                .ValueGeneratedOnAdd()
                .HasColumnName("FamilyHistoryID");
            entity.Property(e => e.FhEgc)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("FH_EGC");
            entity.Property(e => e.FhEgcremark)
                .HasMaxLength(250)
                .IsUnicode(false)
                .HasColumnName("FH_EGCRemark");
            entity.Property(e => e.FhGred)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("FH_GRED");
            entity.Property(e => e.FhRemark)
                .HasMaxLength(250)
                .IsUnicode(false)
                .HasColumnName("FH_Remark");
            entity.Property(e => e.GHPpi)
                .HasMaxLength(250)
                .IsUnicode(false)
                .HasColumnName("gH_PPI");
            entity.Property(e => e.ModifiedDt).HasColumnType("datetime");
            entity.Property(e => e.PatientId).HasColumnName("PatientID");
        });

        modelBuilder.Entity<VwFollowup1Rpt>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("vw_Followup1RPT");

            entity.Property(e => e.APresent)
                .HasMaxLength(3)
                .IsUnicode(false)
                .HasColumnName("A_Present");
            entity.Property(e => e.ARemark)
                .HasMaxLength(250)
                .IsUnicode(false)
                .HasColumnName("A_Remark");
            entity.Property(e => e.AcidRefluxRelatedSymptom)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("Acid Reflux related Symptom");
            entity.Property(e => e.AcidTasteInMouth).HasColumnName("Acid taste in mouth");
            entity.Property(e => e.AlginateDose)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("Alginate_Dose");
            entity.Property(e => e.AlginateFrequency)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("Alginate_Frequency");
            entity.Property(e => e.AlginateMedicationName)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("Alginate_Medication_Name");
            entity.Property(e => e.AtDuration).HasColumnName("AT_Duration");
            entity.Property(e => e.AtFrequency).HasColumnName("AT_Frequency");
            entity.Property(e => e.AtNocturnal)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("AT_Nocturnal");
            entity.Property(e => e.AtPostural)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("AT_Postural");
            entity.Property(e => e.BarrettSRemarks)
                .HasMaxLength(1000)
                .IsUnicode(false)
                .HasColumnName("Barrett’s Remarks");
            entity.Property(e => e.BdPresent)
                .HasMaxLength(3)
                .IsUnicode(false)
                .HasColumnName("BD_Present");
            entity.Property(e => e.BdRemark)
                .HasMaxLength(250)
                .IsUnicode(false)
                .HasColumnName("BD_Remark");
            entity.Property(e => e.BiopsyAttached)
                .HasMaxLength(3)
                .IsUnicode(false)
                .HasColumnName("Biopsy Attached");
            entity.Property(e => e.BiopsyAttached1)
                .HasMaxLength(3)
                .IsUnicode(false)
                .HasColumnName("Biopsy_Attached");
            entity.Property(e => e.BiopsyDate)
                .HasColumnType("datetime")
                .HasColumnName("Biopsy_Date");
            entity.Property(e => e.BiopsyRemark)
                .HasMaxLength(1000)
                .IsUnicode(false)
                .HasColumnName("Biopsy_Remark");
            entity.Property(e => e.CPresent)
                .HasMaxLength(3)
                .IsUnicode(false)
                .HasColumnName("C_Present");
            entity.Property(e => e.CRemark)
                .HasMaxLength(250)
                .IsUnicode(false)
                .HasColumnName("C_Remark");
            entity.Property(e => e.CdPresent)
                .HasMaxLength(3)
                .IsUnicode(false)
                .HasColumnName("CD_Present");
            entity.Property(e => e.CdRemark)
                .HasMaxLength(250)
                .IsUnicode(false)
                .HasColumnName("CD_Remark");
            entity.Property(e => e.CityName).HasMaxLength(100);
            entity.Property(e => e.CkdPresent)
                .HasMaxLength(3)
                .IsUnicode(false)
                .HasColumnName("CKD_Present");
            entity.Property(e => e.CkdRemark)
                .HasMaxLength(250)
                .IsUnicode(false)
                .HasColumnName("CKD_Remark");
            entity.Property(e => e.CldPresent)
                .HasMaxLength(3)
                .IsUnicode(false)
                .HasColumnName("CLD_Present");
            entity.Property(e => e.CldRemark)
                .HasMaxLength(250)
                .IsUnicode(false)
                .HasColumnName("CLD_Remark");
            entity.Property(e => e.CmoPresent)
                .HasMaxLength(3)
                .IsUnicode(false)
                .HasColumnName("CMO_Present");
            entity.Property(e => e.CmoRemark)
                .HasMaxLength(250)
                .IsUnicode(false)
                .HasColumnName("CMO_Remark");
            entity.Property(e => e.Date).HasColumnType("datetime");
            entity.Property(e => e.DbRemark)
                .HasMaxLength(250)
                .IsUnicode(false)
                .HasColumnName("DB_Remark");
            entity.Property(e => e.Dbpresent)
                .HasMaxLength(3)
                .IsUnicode(false)
                .HasColumnName("DBPresent");
            entity.Property(e => e.DdPresent)
                .HasMaxLength(3)
                .IsUnicode(false)
                .HasColumnName("DD_Present");
            entity.Property(e => e.DdRemark)
                .HasMaxLength(250)
                .IsUnicode(false)
                .HasColumnName("DD_Remark");
            entity.Property(e => e.DietModifications)
                .HasMaxLength(3)
                .IsUnicode(false)
                .HasColumnName("Diet Modifications");
            entity.Property(e => e.DoSomeThingsGetStuckWhenYouSwallow).HasColumnName("Do some things get stuck when you swallow?");
            entity.Property(e => e.DoYouBurpALot).HasColumnName("Do you burp a lot?");
            entity.Property(e => e.DoYouEverFeelSickAfterMeals).HasColumnName("Do you ever feel sick after meals?");
            entity.Property(e => e.DoYouFeelFullWhileEatingMeals).HasColumnName("Do you feel full while eating meals?");
            entity.Property(e => e.DoYouGetBitterLiquidAcidComingUpIntoYourThroat).HasColumnName("Do you get bitter liquid (acid) coming up into your throat?");
            entity.Property(e => e.DoYouGetHeartburn).HasColumnName("Do you get heartburn?");
            entity.Property(e => e.DoYouGetHeartburnAfterMeals).HasColumnName("Do you get heartburn after meals?");
            entity.Property(e => e.DoYouGetHeartburnIfYouBendOver).HasColumnName("Do you get heartburn if you bend over?");
            entity.Property(e => e.DoYouHaveAnUnusualSymptomEGBurningSensationInYourThroat).HasColumnName("Do you have an unusual symptom (e.g. burning) sensation in your throat?");
            entity.Property(e => e.DoYouSometimesSubconsciouslyRubYourChestWithYourHand).HasColumnName("Do you sometimes subconsciously rub your chest with your hand?");
            entity.Property(e => e.DoesYourStomachEverFeelHeavyAfterMeals).HasColumnName("Does your stomach ever feel heavy after meals?");
            entity.Property(e => e.DoesYourStomachGetBloated).HasColumnName("Does your stomach get bloated?");
            entity.Property(e => e.DyspepticDysmotilitySymptom)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("Dyspeptic (Dysmotility) symptom");
            entity.Property(e => e.Education)
                .HasMaxLength(250)
                .IsUnicode(false);
            entity.Property(e => e.FamilyIncome)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Gender)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.H2blockersCDose)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("H2BlockersC_Dose");
            entity.Property(e => e.H2blockersCFrequency)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("H2BlockersC_Frequency");
            entity.Property(e => e.H2blockersCMedicationName)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("H2BlockersC_Medication_Name");
            entity.Property(e => e.H2blockersDose)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("H2Blockers_Dose");
            entity.Property(e => e.H2blockersFrequency)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("H2Blockers_Frequency");
            entity.Property(e => e.H2blockersMedicationName)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("H2Blockers_Medication_Name");
            entity.Property(e => e.HbDuration).HasColumnName("HB_Duration");
            entity.Property(e => e.HbFrequency).HasColumnName("HB_Frequency");
            entity.Property(e => e.HbNocturnal)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("HB_Nocturnal");
            entity.Property(e => e.HbPostural)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("HB_Postural");
            entity.Property(e => e.HillSClassification)
                .HasMaxLength(3)
                .IsUnicode(false)
                .HasColumnName("Hill’s classification");
            entity.Property(e => e.HillSClassificationGrade)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("Hill’s classification Grade");
            entity.Property(e => e.HillSClassificationRemarks)
                .HasMaxLength(1000)
                .IsUnicode(false)
                .HasColumnName("Hill’s classification Remarks");
            entity.Property(e => e.HtRemark)
                .HasMaxLength(250)
                .IsUnicode(false)
                .HasColumnName("HT_Remark");
            entity.Property(e => e.HtdPresent)
                .HasMaxLength(3)
                .IsUnicode(false)
                .HasColumnName("HTD_Present");
            entity.Property(e => e.HtdRemark)
                .HasMaxLength(250)
                .IsUnicode(false)
                .HasColumnName("HTD_Remark");
            entity.Property(e => e.Htpresent)
                .HasMaxLength(3)
                .IsUnicode(false)
                .HasColumnName("HTPresent");
            entity.Property(e => e.Initial)
                .HasMaxLength(250)
                .IsUnicode(false);
            entity.Property(e => e.LaxLesClassification)
                .HasMaxLength(3)
                .IsUnicode(false)
                .HasColumnName("LAX les classification");
            entity.Property(e => e.LosAngelesGrade)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("Los Angeles Grade");
            entity.Property(e => e.LosAngelesGradeRemarks)
                .HasMaxLength(1000)
                .IsUnicode(false)
                .HasColumnName("Los Angeles Grade Remarks");
            entity.Property(e => e.ManometryTest)
                .HasMaxLength(3)
                .IsUnicode(false);
            entity.Property(e => e.ManometryTestAttached)
                .HasMaxLength(3)
                .IsUnicode(false)
                .HasColumnName("ManometryTest Attached");
            entity.Property(e => e.ModerationOfAlcohol)
                .HasMaxLength(3)
                .IsUnicode(false)
                .HasColumnName("Moderation of alcohol");
            entity.Property(e => e.MtDate)
                .HasColumnType("datetime")
                .HasColumnName("MT_Date");
            entity.Property(e => e.MtRemark)
                .HasMaxLength(1000)
                .IsUnicode(false)
                .HasColumnName("MT_Remark");
            entity.Property(e => e.NdPresent)
                .HasMaxLength(3)
                .IsUnicode(false)
                .HasColumnName("ND_Present");
            entity.Property(e => e.NdRemark)
                .HasMaxLength(250)
                .IsUnicode(false)
                .HasColumnName("ND_Remark");
            entity.Property(e => e.OPresent)
                .HasMaxLength(3)
                .IsUnicode(false)
                .HasColumnName("O_Present");
            entity.Property(e => e.ORemark)
                .HasMaxLength(250)
                .IsUnicode(false)
                .HasColumnName("O_Remark");
            entity.Property(e => e.Occupation)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Otherdose)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("otherdose");
            entity.Property(e => e.Otherfrequency)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("otherfrequency");
            entity.Property(e => e.OthersMedicationName)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("others_Medication_Name");
            entity.Property(e => e.PHImpedanceMonitoring)
                .HasMaxLength(3)
                .IsUnicode(false)
                .HasColumnName("pH Impedance monitoring");
            entity.Property(e => e.PHImpedanceMonitoringDate)
                .HasColumnType("datetime")
                .HasColumnName("pH Impedance monitoring Date");
            entity.Property(e => e.PHImpedanceMonitoringRemarks)
                .HasMaxLength(1000)
                .IsUnicode(false)
                .HasColumnName("pH Impedance monitoring Remarks");
            entity.Property(e => e.PHimReportAttached)
                .HasMaxLength(3)
                .IsUnicode(false)
                .HasColumnName("pHIM Report Attached");
            entity.Property(e => e.PcabDose)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("PCAB_Dose");
            entity.Property(e => e.PcabFrequency)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("PCAB_Frequency");
            entity.Property(e => e.PcabMedicationName)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("PCAB_Medication_Name");
            entity.Property(e => e.PlaceType)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.PpiDose)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("PPI_Dose");
            entity.Property(e => e.PpiFrequency)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("PPI_Frequency");
            entity.Property(e => e.PpiMedicationName)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("PPI_Medication_Name");
            entity.Property(e => e.ProkineticsDose)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("Prokinetics_Dose");
            entity.Property(e => e.ProkineticsFrequency)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("Prokinetics_Frequency");
            entity.Property(e => e.ProkineticsMedicationName)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("Prokinetics_Medication_Name");
            entity.Property(e => e.RDuration).HasColumnName("R_Duration");
            entity.Property(e => e.RFrequency).HasColumnName("R_Frequency");
            entity.Property(e => e.RNocturnal)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("R_Nocturnal");
            entity.Property(e => e.RPostural)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("R_Postural");
            entity.Property(e => e.RaPresent)
                .HasMaxLength(3)
                .IsUnicode(false)
                .HasColumnName("RA_Present");
            entity.Property(e => e.RaRemark)
                .HasMaxLength(250)
                .IsUnicode(false)
                .HasColumnName("RA_Remark");
            entity.Property(e => e.RegularExercise)
                .HasMaxLength(3)
                .IsUnicode(false)
                .HasColumnName("Regular exercise");
            entity.Property(e => e.RetrosternalPain).HasColumnName("Retrosternal pain");
            entity.Property(e => e.RpDuration).HasColumnName("RP_Duration");
            entity.Property(e => e.RpFrequency).HasColumnName("RP_Frequency");
            entity.Property(e => e.RpNocturnal)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("RP_Nocturnal");
            entity.Property(e => e.RpPostural)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("RP_Postural");
            entity.Property(e => e.SocioeconomicStatus)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.SsPresent)
                .HasMaxLength(3)
                .IsUnicode(false)
                .HasColumnName("SS_Present");
            entity.Property(e => e.SsRemark)
                .HasMaxLength(250)
                .IsUnicode(false)
                .HasColumnName("SS_Remark");
            entity.Property(e => e.StateName).HasMaxLength(50);
            entity.Property(e => e.StopTobaccoUse)
                .HasMaxLength(3)
                .IsUnicode(false)
                .HasColumnName("Stop Tobacco use");
            entity.Property(e => e.SubjectNo)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.SucralfateDose)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("Sucralfate_Dose");
            entity.Property(e => e.SucralfateFrequency)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("Sucralfate_Frequency");
            entity.Property(e => e.SucralfateMedicationName)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("Sucralfate_Medication_Name");
            entity.Property(e => e.SymtopmScore)
                .HasMaxLength(103)
                .IsUnicode(false);
            entity.Property(e => e.TotalPoints)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("Total Points");
            entity.Property(e => e.TotalSymptomScoreTssInGerdPatients).HasColumnName("Total Symptom Score (TSS) in GERD patients");
            entity.Property(e => e.WeightLoss)
                .HasMaxLength(3)
                .IsUnicode(false)
                .HasColumnName("Weight loss");
        });

        modelBuilder.Entity<VwFollowup2Rpt>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("vw_Followup2RPT");

            entity.Property(e => e.APresent)
                .HasMaxLength(3)
                .IsUnicode(false)
                .HasColumnName("A_Present");
            entity.Property(e => e.ARemark)
                .HasMaxLength(250)
                .IsUnicode(false)
                .HasColumnName("A_Remark");
            entity.Property(e => e.AcidRefluxRelatedSymptom)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("Acid Reflux related Symptom");
            entity.Property(e => e.AcidTasteInMouth).HasColumnName("Acid taste in mouth");
            entity.Property(e => e.AlginateDose)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("Alginate_Dose");
            entity.Property(e => e.AlginateFrequency)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("Alginate_Frequency");
            entity.Property(e => e.AlginateMedicationName)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("Alginate_Medication_Name");
            entity.Property(e => e.AtDuration).HasColumnName("AT_Duration");
            entity.Property(e => e.AtFrequency).HasColumnName("AT_Frequency");
            entity.Property(e => e.AtNocturnal)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("AT_Nocturnal");
            entity.Property(e => e.AtPostural)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("AT_Postural");
            entity.Property(e => e.BarrettSRemarks)
                .HasMaxLength(1000)
                .IsUnicode(false)
                .HasColumnName("Barrett’s Remarks");
            entity.Property(e => e.BdPresent)
                .HasMaxLength(3)
                .IsUnicode(false)
                .HasColumnName("BD_Present");
            entity.Property(e => e.BdRemark)
                .HasMaxLength(250)
                .IsUnicode(false)
                .HasColumnName("BD_Remark");
            entity.Property(e => e.BiopsyAttached)
                .HasMaxLength(3)
                .IsUnicode(false)
                .HasColumnName("Biopsy Attached");
            entity.Property(e => e.BiopsyAttached1)
                .HasMaxLength(3)
                .IsUnicode(false)
                .HasColumnName("Biopsy_Attached");
            entity.Property(e => e.BiopsyDate)
                .HasColumnType("datetime")
                .HasColumnName("Biopsy_Date");
            entity.Property(e => e.BiopsyRemark)
                .HasMaxLength(1000)
                .IsUnicode(false)
                .HasColumnName("Biopsy_Remark");
            entity.Property(e => e.CPresent)
                .HasMaxLength(3)
                .IsUnicode(false)
                .HasColumnName("C_Present");
            entity.Property(e => e.CRemark)
                .HasMaxLength(250)
                .IsUnicode(false)
                .HasColumnName("C_Remark");
            entity.Property(e => e.CdPresent)
                .HasMaxLength(3)
                .IsUnicode(false)
                .HasColumnName("CD_Present");
            entity.Property(e => e.CdRemark)
                .HasMaxLength(250)
                .IsUnicode(false)
                .HasColumnName("CD_Remark");
            entity.Property(e => e.CityName).HasMaxLength(100);
            entity.Property(e => e.CkdPresent)
                .HasMaxLength(3)
                .IsUnicode(false)
                .HasColumnName("CKD_Present");
            entity.Property(e => e.CkdRemark)
                .HasMaxLength(250)
                .IsUnicode(false)
                .HasColumnName("CKD_Remark");
            entity.Property(e => e.CldPresent)
                .HasMaxLength(3)
                .IsUnicode(false)
                .HasColumnName("CLD_Present");
            entity.Property(e => e.CldRemark)
                .HasMaxLength(250)
                .IsUnicode(false)
                .HasColumnName("CLD_Remark");
            entity.Property(e => e.CmoPresent)
                .HasMaxLength(3)
                .IsUnicode(false)
                .HasColumnName("CMO_Present");
            entity.Property(e => e.CmoRemark)
                .HasMaxLength(250)
                .IsUnicode(false)
                .HasColumnName("CMO_Remark");
            entity.Property(e => e.Date).HasColumnType("datetime");
            entity.Property(e => e.DbPresent)
                .HasMaxLength(3)
                .IsUnicode(false)
                .HasColumnName("DB_Present");
            entity.Property(e => e.DbRemark)
                .HasMaxLength(250)
                .IsUnicode(false)
                .HasColumnName("DB_Remark");
            entity.Property(e => e.DdPresent)
                .HasMaxLength(3)
                .IsUnicode(false)
                .HasColumnName("DD_Present");
            entity.Property(e => e.DdRemark)
                .HasMaxLength(250)
                .IsUnicode(false)
                .HasColumnName("DD_Remark");
            entity.Property(e => e.DietModifications)
                .HasMaxLength(3)
                .IsUnicode(false)
                .HasColumnName("Diet Modifications");
            entity.Property(e => e.DoSomeThingsGetStuckWhenYouSwallow).HasColumnName("Do some things get stuck when you swallow?");
            entity.Property(e => e.DoYouBurpALot).HasColumnName("Do you burp a lot?");
            entity.Property(e => e.DoYouEverFeelSickAfterMeals).HasColumnName("Do you ever feel sick after meals?");
            entity.Property(e => e.DoYouFeelFullWhileEatingMeals).HasColumnName("Do you feel full while eating meals?");
            entity.Property(e => e.DoYouGetBitterLiquidAcidComingUpIntoYourThroat).HasColumnName("Do you get bitter liquid (acid) coming up into your throat?");
            entity.Property(e => e.DoYouGetHeartburn).HasColumnName("Do you get heartburn?");
            entity.Property(e => e.DoYouGetHeartburnAfterMeals).HasColumnName("Do you get heartburn after meals?");
            entity.Property(e => e.DoYouGetHeartburnIfYouBendOver).HasColumnName("Do you get heartburn if you bend over?");
            entity.Property(e => e.DoYouHaveAnUnusualSymptomEGBurningSensationInYourThroat).HasColumnName("Do you have an unusual symptom (e.g. burning) sensation in your throat?");
            entity.Property(e => e.DoYouSometimesSubconsciouslyRubYourChestWithYourHand).HasColumnName("Do you sometimes subconsciously rub your chest with your hand?");
            entity.Property(e => e.DoesYourStomachEverFeelHeavyAfterMeals).HasColumnName("Does your stomach ever feel heavy after meals?");
            entity.Property(e => e.DoesYourStomachGetBloated).HasColumnName("Does your stomach get bloated?");
            entity.Property(e => e.DyspepticDysmotilitySymptom)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("Dyspeptic (Dysmotility) symptom");
            entity.Property(e => e.Education)
                .HasMaxLength(250)
                .IsUnicode(false);
            entity.Property(e => e.FamilyIncome)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Gender)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.H2blockersCDose)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("H2BlockersC_Dose");
            entity.Property(e => e.H2blockersCFrequency)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("H2BlockersC_Frequency");
            entity.Property(e => e.H2blockersCMedicationName)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("H2BlockersC_Medication_Name");
            entity.Property(e => e.H2blockersDose)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("H2Blockers_Dose");
            entity.Property(e => e.H2blockersFrequency)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("H2Blockers_Frequency");
            entity.Property(e => e.H2blockersMedicationName)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("H2Blockers_Medication_Name");
            entity.Property(e => e.HbDuration).HasColumnName("HB_Duration");
            entity.Property(e => e.HbFrequency).HasColumnName("HB_Frequency");
            entity.Property(e => e.HbNocturnal)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("HB_Nocturnal");
            entity.Property(e => e.HbPostural)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("HB_Postural");
            entity.Property(e => e.HillSClassification)
                .HasMaxLength(3)
                .IsUnicode(false)
                .HasColumnName("Hill’s classification");
            entity.Property(e => e.HillSClassificationGrade)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("Hill’s classification Grade");
            entity.Property(e => e.HillSClassificationRemarks)
                .HasMaxLength(1000)
                .IsUnicode(false)
                .HasColumnName("Hill’s classification Remarks");
            entity.Property(e => e.HtPresent)
                .HasMaxLength(3)
                .IsUnicode(false)
                .HasColumnName("HT_Present");
            entity.Property(e => e.HtRemark)
                .HasMaxLength(250)
                .IsUnicode(false)
                .HasColumnName("HT_Remark");
            entity.Property(e => e.HtdPresent)
                .HasMaxLength(3)
                .IsUnicode(false)
                .HasColumnName("HTD_Present");
            entity.Property(e => e.HtdRemark)
                .HasMaxLength(250)
                .IsUnicode(false)
                .HasColumnName("HTD_Remark");
            entity.Property(e => e.Initial)
                .HasMaxLength(250)
                .IsUnicode(false);
            entity.Property(e => e.LaxLesClassification)
                .HasMaxLength(3)
                .IsUnicode(false)
                .HasColumnName("LAX les classification");
            entity.Property(e => e.LosAngelesGrade)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("Los Angeles Grade");
            entity.Property(e => e.LosAngelesGradeRemarks)
                .HasMaxLength(1000)
                .IsUnicode(false)
                .HasColumnName("Los Angeles Grade Remarks");
            entity.Property(e => e.ManometryTest)
                .HasMaxLength(3)
                .IsUnicode(false);
            entity.Property(e => e.ManometryTestAttached)
                .HasMaxLength(3)
                .IsUnicode(false)
                .HasColumnName("ManometryTest Attached");
            entity.Property(e => e.ModerationOfAlcohol)
                .HasMaxLength(3)
                .IsUnicode(false)
                .HasColumnName("Moderation of alcohol");
            entity.Property(e => e.MtDate)
                .HasColumnType("datetime")
                .HasColumnName("MT_Date");
            entity.Property(e => e.MtRemark)
                .HasMaxLength(1000)
                .IsUnicode(false)
                .HasColumnName("MT_Remark");
            entity.Property(e => e.NdPresent)
                .HasMaxLength(3)
                .IsUnicode(false)
                .HasColumnName("ND_Present");
            entity.Property(e => e.NdRemark)
                .HasMaxLength(250)
                .IsUnicode(false)
                .HasColumnName("ND_Remark");
            entity.Property(e => e.OPresent)
                .HasMaxLength(3)
                .IsUnicode(false)
                .HasColumnName("O_Present");
            entity.Property(e => e.ORemark)
                .HasMaxLength(250)
                .IsUnicode(false)
                .HasColumnName("O_Remark");
            entity.Property(e => e.Occupation)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Otherdose)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("otherdose");
            entity.Property(e => e.Otherfrequency)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("otherfrequency");
            entity.Property(e => e.OthersMedicationName)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("others_Medication_Name");
            entity.Property(e => e.PHImpedanceMonitoring)
                .HasMaxLength(3)
                .IsUnicode(false)
                .HasColumnName("pH Impedance monitoring");
            entity.Property(e => e.PHImpedanceMonitoringDate)
                .HasColumnType("datetime")
                .HasColumnName("pH Impedance monitoring Date");
            entity.Property(e => e.PHImpedanceMonitoringRemarks)
                .HasMaxLength(1000)
                .IsUnicode(false)
                .HasColumnName("pH Impedance monitoring Remarks");
            entity.Property(e => e.PHimReportAttached)
                .HasMaxLength(3)
                .IsUnicode(false)
                .HasColumnName("pHIM Report Attached");
            entity.Property(e => e.PcabDose)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("PCAB_Dose");
            entity.Property(e => e.PcabFrequency)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("PCAB_Frequency");
            entity.Property(e => e.PcabMedicationName)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("PCAB_Medication_Name");
            entity.Property(e => e.PlaceType)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.PpiDose)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("PPI_Dose");
            entity.Property(e => e.PpiFrequency)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("PPI_Frequency");
            entity.Property(e => e.PpiMedicationName)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("PPI_Medication_Name");
            entity.Property(e => e.ProkineticsDose)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("Prokinetics_Dose");
            entity.Property(e => e.ProkineticsFrequency)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("Prokinetics_Frequency");
            entity.Property(e => e.ProkineticsMedicationName)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("Prokinetics_Medication_Name");
            entity.Property(e => e.RDuration).HasColumnName("R_Duration");
            entity.Property(e => e.RFrequency).HasColumnName("R_Frequency");
            entity.Property(e => e.RNocturnal)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("R_Nocturnal");
            entity.Property(e => e.RPostural)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("R_Postural");
            entity.Property(e => e.RaPresent)
                .HasMaxLength(3)
                .IsUnicode(false)
                .HasColumnName("RA_Present");
            entity.Property(e => e.RaRemark)
                .HasMaxLength(250)
                .IsUnicode(false)
                .HasColumnName("RA_Remark");
            entity.Property(e => e.RegularExercise)
                .HasMaxLength(3)
                .IsUnicode(false)
                .HasColumnName("Regular exercise");
            entity.Property(e => e.RetrosternalPain).HasColumnName("Retrosternal pain");
            entity.Property(e => e.RpDuration).HasColumnName("RP_Duration");
            entity.Property(e => e.RpFrequency).HasColumnName("RP_Frequency");
            entity.Property(e => e.RpNocturnal)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("RP_Nocturnal");
            entity.Property(e => e.RpPostural)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("RP_Postural");
            entity.Property(e => e.SocioeconomicStatus)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.SsPresent)
                .HasMaxLength(3)
                .IsUnicode(false)
                .HasColumnName("SS_Present");
            entity.Property(e => e.SsRemark)
                .HasMaxLength(250)
                .IsUnicode(false)
                .HasColumnName("SS_Remark");
            entity.Property(e => e.StateName).HasMaxLength(50);
            entity.Property(e => e.StopTobaccoUse)
                .HasMaxLength(3)
                .IsUnicode(false)
                .HasColumnName("Stop Tobacco use");
            entity.Property(e => e.SubjectNo)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.SucralfateDose)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("Sucralfate_Dose");
            entity.Property(e => e.SucralfateFrequency)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("Sucralfate_Frequency");
            entity.Property(e => e.SucralfateMedicationName)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("Sucralfate_Medication_Name");
            entity.Property(e => e.SymtopmScore)
                .HasMaxLength(103)
                .IsUnicode(false);
            entity.Property(e => e.TotalPoints)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("Total Points");
            entity.Property(e => e.TotalSymptomScoreTssInGerdPatients).HasColumnName("Total Symptom Score (TSS) in GERD patients");
            entity.Property(e => e.WeightLoss)
                .HasMaxLength(3)
                .IsUnicode(false)
                .HasColumnName("Weight loss");
        });

        modelBuilder.Entity<VwGadget>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("vw_Gadget");

            entity.Property(e => e.ComputerFrequency)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.CreatedAt).HasColumnType("datetime");
            entity.Property(e => e.Id).ValueGeneratedOnAdd();
            entity.Property(e => e.JobType)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.ModifiedAt).HasColumnType("datetime");
            entity.Property(e => e.SmartphoneFrequency)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.WorkingHours)
                .HasMaxLength(50)
                .IsUnicode(false);
        });

        modelBuilder.Entity<VwGenderRpt>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("vw_GenderRPT");

            entity.Property(e => e.City).HasColumnName("city");
            entity.Property(e => e.Gender)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Initial)
                .HasMaxLength(250)
                .IsUnicode(false);
            entity.Property(e => e.State).HasColumnName("state");
            entity.Property(e => e.SubjectNo)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Zone)
                .HasMaxLength(6)
                .IsUnicode(false);
        });

        modelBuilder.Entity<VwGerdhistory>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("vw_GERDHistory");

            entity.Property(e => e.CreatedDt).HasColumnType("datetime");
            entity.Property(e => e.DoctorId).HasColumnName("DoctorID");
            entity.Property(e => e.EndoscopyAttement)
                .HasMaxLength(250)
                .IsUnicode(false);
            entity.Property(e => e.EndoscopyDate).HasColumnType("datetime");
            entity.Property(e => e.EndoscopyRemark)
                .HasMaxLength(1000)
                .IsUnicode(false);
            entity.Property(e => e.Ghid)
                .ValueGeneratedOnAdd()
                .HasColumnName("GHID");
            entity.Property(e => e.GsBariatricSurgery).HasColumnName("GS_BariatricSurgery");
            entity.Property(e => e.GsBsremark)
                .HasMaxLength(1000)
                .IsUnicode(false)
                .HasColumnName("GS_BSRemark");
            entity.Property(e => e.GsFsremark)
                .HasMaxLength(1000)
                .IsUnicode(false)
                .HasColumnName("GS_FSRemark");
            entity.Property(e => e.GsFundoplicationSurgery).HasColumnName("GS_FundoplicationSurgery");
            entity.Property(e => e.GsGastricPoemsurgery).HasColumnName("GS_GastricPOEMSurgery");
            entity.Property(e => e.GsGastrojejunostomy).HasColumnName("GS_Gastrojejunostomy");
            entity.Property(e => e.GsGjremark)
                .HasMaxLength(1000)
                .IsUnicode(false)
                .HasColumnName("GS_GJRemark");
            entity.Property(e => e.GsGpsremark)
                .HasMaxLength(1000)
                .IsUnicode(false)
                .HasColumnName("GS_GPSRemark");
            entity.Property(e => e.GsOther).HasColumnName("GS_Other");
            entity.Property(e => e.GsOtherRemark)
                .HasMaxLength(1000)
                .IsUnicode(false)
                .HasColumnName("GS_OtherRemark");
            entity.Property(e => e.GsOtherText)
                .HasMaxLength(1000)
                .IsUnicode(false)
                .HasColumnName("gs_OtherText");
            entity.Property(e => e.HistoryofEndoscopy)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.HistoryofGs).HasColumnName("HistoryofGS");
            entity.Property(e => e.ModifiedDt).HasColumnType("datetime");
            entity.Property(e => e.PatientId).HasColumnName("PatientID");
            entity.Property(e => e.UsageOfPpi)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("UsageOfPPI");
        });

        modelBuilder.Entity<VwHistory>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("vw_History");

            entity.Property(e => e.CreatedDate).HasColumnType("datetime");
            entity.Property(e => e.DietNonVegetarian).HasColumnName("Diet_NonVegetarian");
            entity.Property(e => e.DietVegetarian).HasColumnName("Diet_Vegetarian");
            entity.Property(e => e.DoctorId).HasColumnName("DoctorID");
            entity.Property(e => e.ModifiedDate).HasColumnType("datetime");
            entity.Property(e => e.PastHistory).HasColumnName("Past_History");
            entity.Property(e => e.PatientId).HasColumnName("PatientID");
        });

        modelBuilder.Entity<VwInCompletedRpt>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("vw_InCompletedRPT");

            entity.Property(e => e.AcidRefluxrelatedSymptom)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.AcidTasteDurationYrs).HasColumnName("Acid Taste Duration (Yrs)");
            entity.Property(e => e.AcidTasteFrequencyWk).HasColumnName("Acid Taste Frequency (/Wk)");
            entity.Property(e => e.AcidTasteNocturnal)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("Acid Taste Nocturnal");
            entity.Property(e => e.AcidTastePostural)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("Acid Taste Postural");
            entity.Property(e => e.AeratedDrinks)
                .HasMaxLength(3)
                .IsUnicode(false)
                .HasColumnName("Aerated Drinks");
            entity.Property(e => e.AeratedDrinksDurationYrs)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("Aerated Drinks Duration (yrs)");
            entity.Property(e => e.AeratedDrinksFrequencyDay)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("Aerated Drinks Frequency (/day)");
            entity.Property(e => e.AeratedDrinksQuantityMl)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("Aerated Drinks Quantity (ml)");
            entity.Property(e => e.Aerobics)
                .HasMaxLength(3)
                .IsUnicode(false);
            entity.Property(e => e.AerobicsDurationYrs)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("Aerobics Duration (yrs)");
            entity.Property(e => e.AerobicsFrequencyHrsWeek)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("Aerobics Frequency (hrs/week)");
            entity.Property(e => e.Alcohol)
                .HasMaxLength(3)
                .IsUnicode(false);
            entity.Property(e => e.AlcoholDurationYrs)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("Alcohol Duration (yrs)");
            entity.Property(e => e.AlcoholFrequencyWeek)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("Alcohol Frequency (/week)");
            entity.Property(e => e.AlcoholQuantityMl)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("Alcohol Quantity (ml)");
            entity.Property(e => e.AlginateDose)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("Alginate_Dose");
            entity.Property(e => e.AlginateFrequency)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("Alginate_Frequency");
            entity.Property(e => e.AlginateMedicationName)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("Alginate_Medication_Name");
            entity.Property(e => e.AntiPlateletAgentsMoleculeName)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("Anti-platelet Agents - Molecule Name");
            entity.Property(e => e.AntiPlateletDose)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("Anti-platelet - Dose");
            entity.Property(e => e.AntiPlateletFrequency)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("Anti-platelet - Frequency");
            entity.Property(e => e.Asthma)
                .HasMaxLength(3)
                .IsUnicode(false);
            entity.Property(e => e.AsthmaRemarks)
                .HasMaxLength(250)
                .IsUnicode(false)
                .HasColumnName("Asthma Remarks");
            entity.Property(e => e.B1Regurgitation).HasColumnName("B1_Regurgitation");
            entity.Property(e => e.BariatricSurgery)
                .HasMaxLength(3)
                .IsUnicode(false)
                .HasColumnName("Bariatric Surgery");
            entity.Property(e => e.BariatricSurgeryRemarks)
                .HasMaxLength(1000)
                .IsUnicode(false)
                .HasColumnName("Bariatric Surgery Remarks");
            entity.Property(e => e.BarrettSRemarks)
                .HasMaxLength(1000)
                .IsUnicode(false)
                .HasColumnName("Barrett’s Remarks");
            entity.Property(e => e.BehaviouralDisorderRemarks)
                .HasMaxLength(250)
                .IsUnicode(false)
                .HasColumnName("Behavioural disorder Remarks");
            entity.Property(e => e.BehaviouralDisorders)
                .HasMaxLength(3)
                .IsUnicode(false)
                .HasColumnName("Behavioural disorders");
            entity.Property(e => e.BiopsyAttached).HasColumnName("Biopsy_Attached");
            entity.Property(e => e.BiopsyDate)
                .HasColumnType("datetime")
                .HasColumnName("Biopsy_Date");
            entity.Property(e => e.BiopsyRemark)
                .HasMaxLength(1000)
                .IsUnicode(false)
                .HasColumnName("Biopsy_Remark");
            entity.Property(e => e.BisphosphonatesDose)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("Bisphosphonates - Dose");
            entity.Property(e => e.BisphosphonatesFrequency)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("Bisphosphonates - Frequency");
            entity.Property(e => e.BisphosphonatesMoleculeName)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("Bisphosphonates - Molecule Name");
            entity.Property(e => e.Bmi)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("BMI");
            entity.Property(e => e.Cancer)
                .HasMaxLength(3)
                .IsUnicode(false);
            entity.Property(e => e.CancerRemarks)
                .HasMaxLength(250)
                .IsUnicode(false)
                .HasColumnName("Cancer Remarks");
            entity.Property(e => e.CardiovascularDisorders)
                .HasMaxLength(3)
                .IsUnicode(false)
                .HasColumnName("Cardiovascular Disorders");
            entity.Property(e => e.CardiovascularDisordersRemarks)
                .HasMaxLength(250)
                .IsUnicode(false)
                .HasColumnName("Cardiovascular Disorders Remarks");
            entity.Property(e => e.ChocolatesSweets)
                .HasMaxLength(3)
                .IsUnicode(false)
                .HasColumnName("Chocolates/Sweets");
            entity.Property(e => e.ChocolatesSweetsDurationYrs)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("Chocolates/Sweets Duration (yrs)");
            entity.Property(e => e.ChocolatesSweetsFrequencyWeek)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("Chocolates/Sweets Frequency (/week)");
            entity.Property(e => e.ChocolatesSweetsQuantityG)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("Chocolates/Sweets Quantity (g)");
            entity.Property(e => e.ChronicKidneyDisease)
                .HasMaxLength(3)
                .IsUnicode(false)
                .HasColumnName("Chronic kidney disease");
            entity.Property(e => e.ChronicKidneyDiseaseRemarks)
                .HasMaxLength(250)
                .IsUnicode(false)
                .HasColumnName("Chronic kidney disease Remarks");
            entity.Property(e => e.ChronicLiverDisease)
                .HasMaxLength(3)
                .IsUnicode(false)
                .HasColumnName("Chronic Liver Disease");
            entity.Property(e => e.ChronicLiverDiseaseRemarks)
                .HasMaxLength(250)
                .IsUnicode(false)
                .HasColumnName("Chronic Liver Disease Remarks");
            entity.Property(e => e.CityName).HasMaxLength(100);
            entity.Property(e => e.Coffee)
                .HasMaxLength(3)
                .IsUnicode(false);
            entity.Property(e => e.CoffeeDurationYrs)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("Coffee Duration (yrs)");
            entity.Property(e => e.CoffeeFrequencyDay)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("Coffee Frequency (/day)");
            entity.Property(e => e.CoffeeQuantityMl)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("Coffee Quantity(ml)");
            entity.Property(e => e.ComputerUsageDurationYears).HasColumnName("Computer Usage Duration (years)");
            entity.Property(e => e.ComputerUsageHrsDay)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("Computer Usage (hrs/day)");
            entity.Property(e => e.ComputerUse)
                .HasMaxLength(3)
                .IsUnicode(false)
                .HasColumnName("Computer Use");
            entity.Property(e => e.Date).HasColumnType("datetime");
            entity.Property(e => e.Diabetes)
                .HasMaxLength(3)
                .IsUnicode(false);
            entity.Property(e => e.DiabetesRemarks)
                .HasMaxLength(250)
                .IsUnicode(false)
                .HasColumnName("Diabetes Remarks");
            entity.Property(e => e.Diet)
                .HasMaxLength(14)
                .IsUnicode(false);
            entity.Property(e => e.DietModifications)
                .HasMaxLength(3)
                .IsUnicode(false)
                .HasColumnName("Diet Modifications");
            entity.Property(e => e.Doesyourstomacheverfeelheavyaftermeals).HasColumnName("Doesyourstomacheverfeelheavyaftermeals?");
            entity.Property(e => e.Doesyourstomachgetbloated).HasColumnName("Doesyourstomachgetbloated?");
            entity.Property(e => e.Dose)
                .HasMaxLength(1)
                .IsUnicode(false);
            entity.Property(e => e.Dosomethingsgetstuckwhenyouswallow).HasColumnName("Dosomethingsgetstuckwhenyouswallow?");
            entity.Property(e => e.Doyouburpalot).HasColumnName("Doyouburpalot?");
            entity.Property(e => e.Doyoueverfeelsickaftermeals).HasColumnName("Doyoueverfeelsickaftermeals?");
            entity.Property(e => e.Doyoufeelfullwhileeatingmeals).HasColumnName("Doyoufeelfullwhileeatingmeals?");
            entity.Property(e => e.DoyougetbitterliquidAcidComingupintoyourthroat).HasColumnName("Doyougetbitterliquid(acid)comingupintoyourthroat?");
            entity.Property(e => e.Doyougetheartburn).HasColumnName("Doyougetheartburn?");
            entity.Property(e => e.Doyougetheartburnaftermeals).HasColumnName("Doyougetheartburnaftermeals?");
            entity.Property(e => e.Doyougetheartburnifyoubendover).HasColumnName("Doyougetheartburnifyoubendover?");
            entity.Property(e => e.Doyousometimessubconsciouslyrubyourchestwithyourhand).HasColumnName("Doyousometimessubconsciouslyrubyourchestwithyourhand?");
            entity.Property(e => e.DurationNoOfYearsInTheAboveWorkingHours).HasColumnName("Duration (No. of years in the above working hours)");
            entity.Property(e => e.Dyslipidemia)
                .HasMaxLength(3)
                .IsUnicode(false);
            entity.Property(e => e.DyslipidemiaRemarks)
                .HasMaxLength(250)
                .IsUnicode(false)
                .HasColumnName("Dyslipidemia Remarks");
            entity.Property(e => e.DyspepticDysmotilitySymptom)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("Dyspeptic(Dysmotility)symptom");
            entity.Property(e => e.Education)
                .HasMaxLength(250)
                .IsUnicode(false);
            entity.Property(e => e.EndoscopyDate)
                .HasColumnType("datetime")
                .HasColumnName("Endoscopy Date");
            entity.Property(e => e.EndoscopyRemarks)
                .HasMaxLength(1000)
                .IsUnicode(false)
                .HasColumnName("Endoscopy Remarks");
            entity.Property(e => e.EsophagoGastricCancerRemarks)
                .HasMaxLength(250)
                .IsUnicode(false)
                .HasColumnName("Esophago-gastric Cancer Remarks");
            entity.Property(e => e.Exercise)
                .HasMaxLength(3)
                .IsUnicode(false);
            entity.Property(e => e.F1APresent)
                .HasMaxLength(3)
                .IsUnicode(false)
                .HasColumnName("F1_A_Present");
            entity.Property(e => e.F1ARemark)
                .HasMaxLength(250)
                .IsUnicode(false)
                .HasColumnName("F1_A_Remark");
            entity.Property(e => e.F1AcidRefluxrelatedSymptom)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("F1_AcidRefluxrelatedSymptom");
            entity.Property(e => e.F1Acidtasteinmouth).HasColumnName("F1_Acidtasteinmouth");
            entity.Property(e => e.F1AlginateDose)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("F1_Alginate_Dose");
            entity.Property(e => e.F1AlginateFrequency)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("F1_Alginate_Frequency");
            entity.Property(e => e.F1AlginateMedicationName)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("F1_Alginate_Medication_Name");
            entity.Property(e => e.F1AtDuration).HasColumnName("F1_AT_Duration");
            entity.Property(e => e.F1AtFrequency).HasColumnName("F1_AT_Frequency");
            entity.Property(e => e.F1AtNocturnal)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("F1_AT_Nocturnal");
            entity.Property(e => e.F1AtPostural)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("F1_AT_Postural");
            entity.Property(e => e.F1BarrettsRemarks)
                .HasMaxLength(1000)
                .IsUnicode(false)
                .HasColumnName("F1_BarrettsRemarks");
            entity.Property(e => e.F1BdPresent)
                .HasMaxLength(3)
                .IsUnicode(false)
                .HasColumnName("F1_BD_Present");
            entity.Property(e => e.F1BdRemark)
                .HasMaxLength(250)
                .IsUnicode(false)
                .HasColumnName("F1_BD_Remark");
            entity.Property(e => e.F1Biopsy)
                .HasMaxLength(3)
                .IsUnicode(false)
                .HasColumnName("F1_Biopsy");
            entity.Property(e => e.F1BiopsyAttached)
                .HasMaxLength(3)
                .IsUnicode(false)
                .HasColumnName("F1_Biopsy_Attached");
            entity.Property(e => e.F1BiopsyDate)
                .HasColumnType("datetime")
                .HasColumnName("F1_Biopsy_Date");
            entity.Property(e => e.F1BiopsyRemark)
                .HasMaxLength(1000)
                .IsUnicode(false)
                .HasColumnName("F1_Biopsy_Remark");
            entity.Property(e => e.F1CPresent)
                .HasMaxLength(3)
                .IsUnicode(false)
                .HasColumnName("F1_C_Present");
            entity.Property(e => e.F1CRemark)
                .HasMaxLength(250)
                .IsUnicode(false)
                .HasColumnName("F1_C_Remark");
            entity.Property(e => e.F1CdPresent)
                .HasMaxLength(3)
                .IsUnicode(false)
                .HasColumnName("F1_CD_Present");
            entity.Property(e => e.F1CkdPresent)
                .HasMaxLength(3)
                .IsUnicode(false)
                .HasColumnName("F1_CKD_Present");
            entity.Property(e => e.F1CkdRemark)
                .HasMaxLength(250)
                .IsUnicode(false)
                .HasColumnName("F1_CKD_Remark");
            entity.Property(e => e.F1CldPresent)
                .HasMaxLength(3)
                .IsUnicode(false)
                .HasColumnName("F1_CLD_Present");
            entity.Property(e => e.F1CldRemark)
                .HasMaxLength(250)
                .IsUnicode(false)
                .HasColumnName("F1_CLD_Remark");
            entity.Property(e => e.F1CmoPresent)
                .HasMaxLength(3)
                .IsUnicode(false)
                .HasColumnName("F1_CMO_Present");
            entity.Property(e => e.F1CmoRemark)
                .HasMaxLength(250)
                .IsUnicode(false)
                .HasColumnName("F1_CMO_Remark");
            entity.Property(e => e.F1DbRemark)
                .HasMaxLength(250)
                .IsUnicode(false)
                .HasColumnName("F1_DB_Remark");
            entity.Property(e => e.F1Dbpresent)
                .HasMaxLength(3)
                .IsUnicode(false)
                .HasColumnName("F1_DBPresent");
            entity.Property(e => e.F1DdPresent)
                .HasMaxLength(3)
                .IsUnicode(false)
                .HasColumnName("F1_DD_Present");
            entity.Property(e => e.F1DdRemark)
                .HasMaxLength(250)
                .IsUnicode(false)
                .HasColumnName("F1_DD_Remark");
            entity.Property(e => e.F1DietModifications)
                .HasMaxLength(3)
                .IsUnicode(false)
                .HasColumnName("F1 Diet Modifications");
            entity.Property(e => e.F1Doesyourstomacheverfeelheavyaftermeals).HasColumnName("F1_Doesyourstomacheverfeelheavyaftermeals?");
            entity.Property(e => e.F1Doesyourstomachgetbloated).HasColumnName("F1_Doesyourstomachgetbloated?");
            entity.Property(e => e.F1Dosomethingsgetstuckwhenyouswallow).HasColumnName("F1_Dosomethingsgetstuckwhenyouswallow?");
            entity.Property(e => e.F1Doyouburpalot).HasColumnName("F1_Doyouburpalot?");
            entity.Property(e => e.F1Doyoueverfeelsickaftermeals).HasColumnName("F1_Doyoueverfeelsickaftermeals?");
            entity.Property(e => e.F1Doyoufeelfullwhileeatingmeals).HasColumnName("F1_Doyoufeelfullwhileeatingmeals?");
            entity.Property(e => e.F1DoyougetbitterliquidAcidComingupintoyourthroat).HasColumnName("F1_Doyougetbitterliquid(acid)comingupintoyourthroat?");
            entity.Property(e => e.F1Doyougetheartburn).HasColumnName("F1_Doyougetheartburn?");
            entity.Property(e => e.F1Doyougetheartburnaftermeals).HasColumnName("F1_Doyougetheartburnaftermeals?");
            entity.Property(e => e.F1Doyougetheartburnifyoubendover).HasColumnName("F1_Doyougetheartburnifyoubendover?");
            entity.Property(e => e.F1Doyouhaveanunusualsymptom).HasColumnName("F1_Doyouhaveanunusualsymptom");
            entity.Property(e => e.F1Doyousometimessubconsciouslyrubyourchestwithyourhand).HasColumnName("F1_Doyousometimessubconsciouslyrubyourchestwithyourhand?");
            entity.Property(e => e.F1DyspepticDysmotilitySymptom)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("F1_Dyspeptic(Dysmotility)symptom");
            entity.Property(e => e.F1H2blockersCDose)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("F1_H2BlockersC_Dose");
            entity.Property(e => e.F1H2blockersCFrequency)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("F1_H2BlockersC_Frequency");
            entity.Property(e => e.F1H2blockersCMedicationName)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("F1_H2BlockersC_Medication_Name");
            entity.Property(e => e.F1H2blockersDose)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("F1_H2Blockers_Dose");
            entity.Property(e => e.F1H2blockersFrequency)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("F1_H2Blockers_Frequency");
            entity.Property(e => e.F1H2blockersMedicationName)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("F1_H2Blockers_Medication_Name");
            entity.Property(e => e.F1HbDuration).HasColumnName("F1_HB_Duration");
            entity.Property(e => e.F1HbFrequency).HasColumnName("F1_HB_Frequency");
            entity.Property(e => e.F1HbNocturnal)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("F1_HB_Nocturnal");
            entity.Property(e => e.F1HbPostural)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("F1_HB_Postural");
            entity.Property(e => e.F1HeartburnHeartburn).HasColumnName("F1_HeartburnHeartburn");
            entity.Property(e => e.F1Hillsclassification)
                .HasMaxLength(3)
                .IsUnicode(false)
                .HasColumnName("F1_Hillsclassification");
            entity.Property(e => e.F1HillsclassificationGrade)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("F1_HillsclassificationGrade");
            entity.Property(e => e.F1HillsclassificationRemarks)
                .HasMaxLength(1000)
                .IsUnicode(false)
                .HasColumnName("F1_HillsclassificationRemarks");
            entity.Property(e => e.F1HtRemark)
                .HasMaxLength(250)
                .IsUnicode(false)
                .HasColumnName("F1_HT_Remark");
            entity.Property(e => e.F1HtdPresent)
                .HasMaxLength(3)
                .IsUnicode(false)
                .HasColumnName("F1_HTD_Present");
            entity.Property(e => e.F1HtdRemark)
                .HasMaxLength(250)
                .IsUnicode(false)
                .HasColumnName("F1_HTD_Remark");
            entity.Property(e => e.F1Htpresent)
                .HasMaxLength(3)
                .IsUnicode(false)
                .HasColumnName("F1_HTPresent");
            entity.Property(e => e.F1Laxlesclassification)
                .HasMaxLength(3)
                .IsUnicode(false)
                .HasColumnName("F1_LAXlesclassification");
            entity.Property(e => e.F1LosAngelesGrade)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("F1_LosAngelesGrade");
            entity.Property(e => e.F1LosAngelesGradeRemarks)
                .HasMaxLength(1000)
                .IsUnicode(false)
                .HasColumnName("F1_LosAngelesGradeRemarks");
            entity.Property(e => e.F1ManometryTest)
                .HasMaxLength(3)
                .IsUnicode(false)
                .HasColumnName("F1_ManometryTest");
            entity.Property(e => e.F1ManometryTestAttached)
                .HasMaxLength(3)
                .IsUnicode(false)
                .HasColumnName("F1_ManometryTest Attached");
            entity.Property(e => e.F1ModerationOfAlcohol)
                .HasMaxLength(3)
                .IsUnicode(false)
                .HasColumnName("F1 Moderation of alcohol");
            entity.Property(e => e.F1MtDate)
                .HasColumnType("datetime")
                .HasColumnName("F1_MT_Date");
            entity.Property(e => e.F1MtRemark)
                .HasMaxLength(1000)
                .IsUnicode(false)
                .HasColumnName("F1_MT_Remark");
            entity.Property(e => e.F1NdPresent)
                .HasMaxLength(3)
                .IsUnicode(false)
                .HasColumnName("F1_ND_Present");
            entity.Property(e => e.F1NdRemark)
                .HasMaxLength(250)
                .IsUnicode(false)
                .HasColumnName("F1_ND_Remark");
            entity.Property(e => e.F1OPresent)
                .HasMaxLength(3)
                .IsUnicode(false)
                .HasColumnName("F1_O_Present");
            entity.Property(e => e.F1ORemark)
                .HasMaxLength(250)
                .IsUnicode(false)
                .HasColumnName("F1_O_Remark");
            entity.Property(e => e.F1OthersDose)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("F1_others_Dose");
            entity.Property(e => e.F1OthersFrequency)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("F1_others_Frequency");
            entity.Property(e => e.F1OthersMedicationName)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("F1_others_Medication_Name");
            entity.Property(e => e.F1PHImpedanceMonitoring)
                .HasMaxLength(3)
                .IsUnicode(false)
                .HasColumnName("F1_pH Impedance monitoring");
            entity.Property(e => e.F1PHImpedanceMonitoringDate)
                .HasColumnType("datetime")
                .HasColumnName("F1_pH Impedance monitoring Date");
            entity.Property(e => e.F1PHImpedanceMonitoringRemarks)
                .HasMaxLength(1000)
                .IsUnicode(false)
                .HasColumnName("F1_pH Impedance monitoring Remarks");
            entity.Property(e => e.F1PHimReportAttached)
                .HasMaxLength(3)
                .IsUnicode(false)
                .HasColumnName("F1_pHIM Report Attached");
            entity.Property(e => e.F1PcabDose)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("F1_PCAB_Dose");
            entity.Property(e => e.F1PcabFrequency)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("F1_PCAB_Frequency");
            entity.Property(e => e.F1PcabMedicationName)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("F1_PCAB_Medication_Name");
            entity.Property(e => e.F1PpiDose)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("F1_PPI_Dose");
            entity.Property(e => e.F1PpiFrequency)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("F1_PPI_Frequency");
            entity.Property(e => e.F1PpiMedicationName)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("F1_PPI_Medication_Name");
            entity.Property(e => e.F1ProkineticsDose)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("F1_Prokinetics_Dose");
            entity.Property(e => e.F1ProkineticsFrequency)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("F1_Prokinetics_Frequency");
            entity.Property(e => e.F1ProkineticsMedicationName)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("F1_Prokinetics_Medication_Name");
            entity.Property(e => e.F1RDuration).HasColumnName("F1_R_Duration");
            entity.Property(e => e.F1RFrequency).HasColumnName("F1_R_Frequency");
            entity.Property(e => e.F1RNocturnal)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("F1_R_Nocturnal");
            entity.Property(e => e.F1RPostural)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("F1_R_Postural");
            entity.Property(e => e.F1RaPresent)
                .HasMaxLength(3)
                .IsUnicode(false)
                .HasColumnName("F1_RA_Present");
            entity.Property(e => e.F1RaRemark)
                .HasMaxLength(250)
                .IsUnicode(false)
                .HasColumnName("F1_RA_Remark");
            entity.Property(e => e.F1RegularExercise)
                .HasMaxLength(3)
                .IsUnicode(false)
                .HasColumnName("F1 Regular exercise");
            entity.Property(e => e.F1Regurgitation).HasColumnName("F1_Regurgitation");
            entity.Property(e => e.F1Retrosternalpain).HasColumnName("F1_Retrosternalpain");
            entity.Property(e => e.F1RpDuration).HasColumnName("F1_RP_Duration");
            entity.Property(e => e.F1RpFrequency).HasColumnName("F1_RP_Frequency");
            entity.Property(e => e.F1RpNocturnal)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("F1_RP_Nocturnal");
            entity.Property(e => e.F1RpPostural)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("F1_RP_Postural");
            entity.Property(e => e.F1SsPresent)
                .HasMaxLength(3)
                .IsUnicode(false)
                .HasColumnName("F1_SS_Present");
            entity.Property(e => e.F1SsRemark)
                .HasMaxLength(250)
                .IsUnicode(false)
                .HasColumnName("F1_SS_Remark");
            entity.Property(e => e.F1StopTobaccoUse)
                .HasMaxLength(3)
                .IsUnicode(false)
                .HasColumnName("F1 Stop Tobacco use");
            entity.Property(e => e.F1SucralfateDose)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("F1_Sucralfate_Dose");
            entity.Property(e => e.F1SucralfateFrequency)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("F1_Sucralfate_Frequency");
            entity.Property(e => e.F1SucralfateMedicationName)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("F1_Sucralfate_Medication_Name");
            entity.Property(e => e.F1SymtopmScore)
                .HasMaxLength(103)
                .IsUnicode(false)
                .HasColumnName("F1_SymtopmScore");
            entity.Property(e => e.F1TotalPoints)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("F1_TotalPoints");
            entity.Property(e => e.F1TotalSymptomScoreinGerdpatients).HasColumnName("F1_TotalSymptomScoreinGERDpatients");
            entity.Property(e => e.F1WeightLoss)
                .HasMaxLength(3)
                .IsUnicode(false)
                .HasColumnName("F1 Weight loss");
            entity.Property(e => e.F2APresent)
                .HasMaxLength(3)
                .IsUnicode(false)
                .HasColumnName("F2_A_Present");
            entity.Property(e => e.F2ARemark)
                .HasMaxLength(250)
                .IsUnicode(false)
                .HasColumnName("F2_A_Remark");
            entity.Property(e => e.F2AcidRefluxrelatedSymptom)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("F2_AcidRefluxrelatedSymptom");
            entity.Property(e => e.F2Acidtasteinmouth).HasColumnName("F2_Acidtasteinmouth");
            entity.Property(e => e.F2AlginateDose)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("F2_Alginate_Dose");
            entity.Property(e => e.F2AlginateFrequency)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("F2_Alginate_Frequency");
            entity.Property(e => e.F2AlginateMedicationName)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("F2_Alginate_Medication_Name");
            entity.Property(e => e.F2AtDuration).HasColumnName("F2_AT_Duration");
            entity.Property(e => e.F2AtFrequency).HasColumnName("F2_AT_Frequency");
            entity.Property(e => e.F2AtNocturnal)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("F2_AT_Nocturnal");
            entity.Property(e => e.F2AtPostural)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("F2_AT_Postural");
            entity.Property(e => e.F2BarrettsRemarks)
                .HasMaxLength(1000)
                .IsUnicode(false)
                .HasColumnName("F2_BarrettsRemarks");
            entity.Property(e => e.F2BdPresent)
                .HasMaxLength(3)
                .IsUnicode(false)
                .HasColumnName("F2_BD_Present");
            entity.Property(e => e.F2BdRemark)
                .HasMaxLength(250)
                .IsUnicode(false)
                .HasColumnName("F2_BD_Remark");
            entity.Property(e => e.F2Biopsy)
                .HasMaxLength(3)
                .IsUnicode(false)
                .HasColumnName("F2_Biopsy");
            entity.Property(e => e.F2BiopsyAttached)
                .HasMaxLength(3)
                .IsUnicode(false)
                .HasColumnName("F2_Biopsy_Attached");
            entity.Property(e => e.F2BiopsyDate)
                .HasColumnType("datetime")
                .HasColumnName("F2_Biopsy_Date");
            entity.Property(e => e.F2BiopsyRemark)
                .HasMaxLength(1000)
                .IsUnicode(false)
                .HasColumnName("F2_Biopsy_Remark");
            entity.Property(e => e.F2CPresent)
                .HasMaxLength(3)
                .IsUnicode(false)
                .HasColumnName("F2_C_Present");
            entity.Property(e => e.F2CRemark)
                .HasMaxLength(250)
                .IsUnicode(false)
                .HasColumnName("F2_C_Remark");
            entity.Property(e => e.F2CdPresent)
                .HasMaxLength(3)
                .IsUnicode(false)
                .HasColumnName("F2_CD_Present");
            entity.Property(e => e.F2CkdPresent)
                .HasMaxLength(3)
                .IsUnicode(false)
                .HasColumnName("F2_CKD_Present");
            entity.Property(e => e.F2CkdRemark)
                .HasMaxLength(250)
                .IsUnicode(false)
                .HasColumnName("F2_CKD_Remark");
            entity.Property(e => e.F2CldPresent)
                .HasMaxLength(3)
                .IsUnicode(false)
                .HasColumnName("F2_CLD_Present");
            entity.Property(e => e.F2CldRemark)
                .HasMaxLength(250)
                .IsUnicode(false)
                .HasColumnName("F2_CLD_Remark");
            entity.Property(e => e.F2CmoPresent)
                .HasMaxLength(3)
                .IsUnicode(false)
                .HasColumnName("F2_CMO_Present");
            entity.Property(e => e.F2CmoRemark)
                .HasMaxLength(250)
                .IsUnicode(false)
                .HasColumnName("F2_CMO_Remark");
            entity.Property(e => e.F2DbRemark)
                .HasMaxLength(250)
                .IsUnicode(false)
                .HasColumnName("F2_DB_Remark");
            entity.Property(e => e.F2Dbpresent)
                .HasMaxLength(3)
                .IsUnicode(false)
                .HasColumnName("F2_DBPresent");
            entity.Property(e => e.F2DdPresent)
                .HasMaxLength(3)
                .IsUnicode(false)
                .HasColumnName("F2_DD_Present");
            entity.Property(e => e.F2DdRemark)
                .HasMaxLength(250)
                .IsUnicode(false)
                .HasColumnName("F2_DD_Remark");
            entity.Property(e => e.F2DietModifications)
                .HasMaxLength(3)
                .IsUnicode(false)
                .HasColumnName("F2 Diet Modifications");
            entity.Property(e => e.F2Doesyourstomacheverfeelheavyaftermeals).HasColumnName("F2_Doesyourstomacheverfeelheavyaftermeals?");
            entity.Property(e => e.F2Doesyourstomachgetbloated).HasColumnName("F2_Doesyourstomachgetbloated?");
            entity.Property(e => e.F2Dosomethingsgetstuckwhenyouswallow).HasColumnName("F2_Dosomethingsgetstuckwhenyouswallow?");
            entity.Property(e => e.F2Doyouburpalot).HasColumnName("F2_Doyouburpalot?");
            entity.Property(e => e.F2Doyoueverfeelsickaftermeals).HasColumnName("F2_Doyoueverfeelsickaftermeals?");
            entity.Property(e => e.F2Doyoufeelfullwhileeatingmeals).HasColumnName("F2_Doyoufeelfullwhileeatingmeals?");
            entity.Property(e => e.F2DoyougetbitterliquidAcidComingupintoyourthroat).HasColumnName("F2_Doyougetbitterliquid(acid)comingupintoyourthroat?");
            entity.Property(e => e.F2Doyougetheartburn).HasColumnName("F2_Doyougetheartburn?");
            entity.Property(e => e.F2Doyougetheartburnaftermeals).HasColumnName("F2_Doyougetheartburnaftermeals?");
            entity.Property(e => e.F2Doyougetheartburnifyoubendover).HasColumnName("F2_Doyougetheartburnifyoubendover?");
            entity.Property(e => e.F2Doyouhaveanunusualsymptom).HasColumnName("F2_Doyouhaveanunusualsymptom");
            entity.Property(e => e.F2Doyousometimessubconsciouslyrubyourchestwithyourhand).HasColumnName("F2_Doyousometimessubconsciouslyrubyourchestwithyourhand?");
            entity.Property(e => e.F2DyspepticDysmotilitySymptom)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("F2_Dyspeptic(Dysmotility)symptom");
            entity.Property(e => e.F2H2blockersCDose)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("F2_H2BlockersC_Dose");
            entity.Property(e => e.F2H2blockersCFrequency)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("F2_H2BlockersC_Frequency");
            entity.Property(e => e.F2H2blockersCMedicationName)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("F2_H2BlockersC_Medication_Name");
            entity.Property(e => e.F2H2blockersDose)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("F2_H2Blockers_Dose");
            entity.Property(e => e.F2H2blockersFrequency)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("F2_H2Blockers_Frequency");
            entity.Property(e => e.F2H2blockersMedicationName)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("F2_H2Blockers_Medication_Name");
            entity.Property(e => e.F2HbDuration).HasColumnName("F2_HB_Duration");
            entity.Property(e => e.F2HbFrequency).HasColumnName("F2_HB_Frequency");
            entity.Property(e => e.F2HbNocturnal)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("F2_HB_Nocturnal");
            entity.Property(e => e.F2HbPostural)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("F2_HB_Postural");
            entity.Property(e => e.F2HeartburnHeartburn).HasColumnName("F2_HeartburnHeartburn");
            entity.Property(e => e.F2Hillsclassification)
                .HasMaxLength(3)
                .IsUnicode(false)
                .HasColumnName("F2_Hillsclassification");
            entity.Property(e => e.F2HillsclassificationGrade)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("F2_HillsclassificationGrade");
            entity.Property(e => e.F2HillsclassificationRemarks)
                .HasMaxLength(1000)
                .IsUnicode(false)
                .HasColumnName("F2_HillsclassificationRemarks");
            entity.Property(e => e.F2HtRemark)
                .HasMaxLength(250)
                .IsUnicode(false)
                .HasColumnName("F2_HT_Remark");
            entity.Property(e => e.F2HtdPresent)
                .HasMaxLength(3)
                .IsUnicode(false)
                .HasColumnName("F2_HTD_Present");
            entity.Property(e => e.F2HtdRemark)
                .HasMaxLength(250)
                .IsUnicode(false)
                .HasColumnName("F2_HTD_Remark");
            entity.Property(e => e.F2Htpresent)
                .HasMaxLength(3)
                .IsUnicode(false)
                .HasColumnName("F2_HTPresent");
            entity.Property(e => e.F2Laxlesclassification)
                .HasMaxLength(3)
                .IsUnicode(false)
                .HasColumnName("F2_LAXlesclassification");
            entity.Property(e => e.F2LosAngelesGrade)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("F2_LosAngelesGrade");
            entity.Property(e => e.F2LosAngelesGradeRemarks)
                .HasMaxLength(1000)
                .IsUnicode(false)
                .HasColumnName("F2_LosAngelesGradeRemarks");
            entity.Property(e => e.F2ManometryTest)
                .HasMaxLength(3)
                .IsUnicode(false)
                .HasColumnName("F2_ManometryTest");
            entity.Property(e => e.F2ManometryTestAttached)
                .HasMaxLength(3)
                .IsUnicode(false)
                .HasColumnName("F2_ManometryTest Attached");
            entity.Property(e => e.F2ModerationOfAlcohol)
                .HasMaxLength(3)
                .IsUnicode(false)
                .HasColumnName("F2 Moderation of alcohol");
            entity.Property(e => e.F2MtDate)
                .HasColumnType("datetime")
                .HasColumnName("F2_MT_Date");
            entity.Property(e => e.F2MtRemark)
                .HasMaxLength(1000)
                .IsUnicode(false)
                .HasColumnName("F2_MT_Remark");
            entity.Property(e => e.F2NdPresent)
                .HasMaxLength(3)
                .IsUnicode(false)
                .HasColumnName("F2_ND_Present");
            entity.Property(e => e.F2NdRemark)
                .HasMaxLength(250)
                .IsUnicode(false)
                .HasColumnName("F2_ND_Remark");
            entity.Property(e => e.F2OPresent)
                .HasMaxLength(3)
                .IsUnicode(false)
                .HasColumnName("F2_O_Present");
            entity.Property(e => e.F2ORemark)
                .HasMaxLength(250)
                .IsUnicode(false)
                .HasColumnName("F2_O_Remark");
            entity.Property(e => e.F2OthersDose)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("F2_others_Dose");
            entity.Property(e => e.F2OthersFrequency)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("F2_others_Frequency");
            entity.Property(e => e.F2OthersMedicationName)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("F2_others_Medication_Name");
            entity.Property(e => e.F2PHImpedanceMonitoring)
                .HasMaxLength(3)
                .IsUnicode(false)
                .HasColumnName("F2_pH Impedance monitoring");
            entity.Property(e => e.F2PHImpedanceMonitoringDate)
                .HasColumnType("datetime")
                .HasColumnName("F2_pH Impedance monitoring Date");
            entity.Property(e => e.F2PHImpedanceMonitoringRemarks)
                .HasMaxLength(1000)
                .IsUnicode(false)
                .HasColumnName("F2_pH Impedance monitoring Remarks");
            entity.Property(e => e.F2PHimReportAttached)
                .HasMaxLength(3)
                .IsUnicode(false)
                .HasColumnName("F2_pHIM Report Attached");
            entity.Property(e => e.F2PcabDose)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("F2_PCAB_Dose");
            entity.Property(e => e.F2PcabFrequency)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("F2_PCAB_Frequency");
            entity.Property(e => e.F2PcabMedicationName)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("F2_PCAB_Medication_Name");
            entity.Property(e => e.F2PpiDose)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("F2_PPI_Dose");
            entity.Property(e => e.F2PpiFrequency)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("F2_PPI_Frequency");
            entity.Property(e => e.F2PpiMedicationName)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("F2_PPI_Medication_Name");
            entity.Property(e => e.F2ProkineticsDose)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("F2_Prokinetics_Dose");
            entity.Property(e => e.F2ProkineticsFrequency)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("F2_Prokinetics_Frequency");
            entity.Property(e => e.F2ProkineticsMedicationName)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("F2_Prokinetics_Medication_Name");
            entity.Property(e => e.F2RDuration).HasColumnName("F2_R_Duration");
            entity.Property(e => e.F2RFrequency).HasColumnName("F2_R_Frequency");
            entity.Property(e => e.F2RNocturnal)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("F2_R_Nocturnal");
            entity.Property(e => e.F2RPostural)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("F2_R_Postural");
            entity.Property(e => e.F2RaPresent)
                .HasMaxLength(3)
                .IsUnicode(false)
                .HasColumnName("F2_RA_Present");
            entity.Property(e => e.F2RaRemark)
                .HasMaxLength(250)
                .IsUnicode(false)
                .HasColumnName("F2_RA_Remark");
            entity.Property(e => e.F2RegularExercise)
                .HasMaxLength(3)
                .IsUnicode(false)
                .HasColumnName("F2 Regular exercise");
            entity.Property(e => e.F2Regurgitation).HasColumnName("F2_Regurgitation");
            entity.Property(e => e.F2Retrosternalpain).HasColumnName("F2_Retrosternalpain");
            entity.Property(e => e.F2RpDuration).HasColumnName("F2_RP_Duration");
            entity.Property(e => e.F2RpFrequency).HasColumnName("F2_RP_Frequency");
            entity.Property(e => e.F2RpNocturnal)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("F2_RP_Nocturnal");
            entity.Property(e => e.F2RpPostural)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("F2_RP_Postural");
            entity.Property(e => e.F2SsPresent)
                .HasMaxLength(3)
                .IsUnicode(false)
                .HasColumnName("F2_SS_Present");
            entity.Property(e => e.F2SsRemark)
                .HasMaxLength(250)
                .IsUnicode(false)
                .HasColumnName("F2_SS_Remark");
            entity.Property(e => e.F2StopTobaccoUse)
                .HasMaxLength(3)
                .IsUnicode(false)
                .HasColumnName("F2 Stop Tobacco use");
            entity.Property(e => e.F2SucralfateDose)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("F2_Sucralfate_Dose");
            entity.Property(e => e.F2SucralfateFrequency)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("F2_Sucralfate_Frequency");
            entity.Property(e => e.F2SucralfateMedicationName)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("F2_Sucralfate_Medication_Name");
            entity.Property(e => e.F2SymtopmScore)
                .HasMaxLength(103)
                .IsUnicode(false)
                .HasColumnName("F2_SymtopmScore");
            entity.Property(e => e.F2TotalPoints)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("F2_TotalPoints");
            entity.Property(e => e.F2TotalSymptomScoreinGerdpatients).HasColumnName("F2_TotalSymptomScoreinGERDpatients");
            entity.Property(e => e.F2WeightLoss)
                .HasMaxLength(3)
                .IsUnicode(false)
                .HasColumnName("F2 Weight loss");
            entity.Property(e => e.FamilyHistoryOfEsophagoGastricCancer)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("Family History of Esophago-gastric Cancer");
            entity.Property(e => e.FamilyHistoryOfGerd)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("Family History of GERD");
            entity.Property(e => e.FamilyIncome)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Frequency)
                .HasMaxLength(1)
                .IsUnicode(false);
            entity.Property(e => e.FundoplicationRemarks)
                .HasMaxLength(1000)
                .IsUnicode(false)
                .HasColumnName("Fundoplication Remarks");
            entity.Property(e => e.FundoplicationSurgery)
                .HasMaxLength(3)
                .IsUnicode(false)
                .HasColumnName("Fundoplication Surgery");
            entity.Property(e => e.GastricPoemRemarks)
                .HasMaxLength(1000)
                .IsUnicode(false)
                .HasColumnName("Gastric POEM Remarks");
            entity.Property(e => e.GastricPoemSurgery)
                .HasMaxLength(3)
                .IsUnicode(false)
                .HasColumnName("Gastric POEM Surgery");
            entity.Property(e => e.Gastrojejunostomy)
                .HasMaxLength(3)
                .IsUnicode(false);
            entity.Property(e => e.GastrojejunostomyRemarks)
                .HasMaxLength(1000)
                .IsUnicode(false)
                .HasColumnName("Gastrojejunostomy Remarks");
            entity.Property(e => e.Gender)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.GeneralAppearance)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("General Appearance");
            entity.Property(e => e.GeneralAppearanceComments)
                .HasMaxLength(1000)
                .IsUnicode(false)
                .HasColumnName("General Appearance – Comments");
            entity.Property(e => e.GerdRemarks)
                .HasMaxLength(250)
                .IsUnicode(false)
                .HasColumnName("GERD - Remarks");
            entity.Property(e => e.Gerdtype)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("GERDType");
            entity.Property(e => e.GredNoOfYear).HasColumnName("GRED_NoOfYear");
            entity.Property(e => e.Gym)
                .HasMaxLength(3)
                .IsUnicode(false);
            entity.Property(e => e.GymDurationYrs)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("Gym Duration (yrs)");
            entity.Property(e => e.GymFrequencyHrsWeek)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("Gym Frequency (hrs/week)");
            entity.Property(e => e.H2blockersCDose)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("H2BlockersC_Dose");
            entity.Property(e => e.H2blockersCFrequency)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("H2BlockersC_Frequency");
            entity.Property(e => e.H2blockersCMedicationName)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("H2BlockersC_Medication_Name");
            entity.Property(e => e.H2blockersDose)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("H2Blockers_Dose");
            entity.Property(e => e.H2blockersFrequency)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("H2Blockers_Frequency");
            entity.Property(e => e.H2blockersMedicationName)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("H2Blockers_Medication_Name");
            entity.Property(e => e.HeartburnDurationYrs).HasColumnName("HeartburnDuration[Yrs)");
            entity.Property(e => e.HeartburnFrequencyWk).HasColumnName("HeartburnFrequency(/Wk)");
            entity.Property(e => e.HeartburnNocturnal)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.HeartburnPostural)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.HeightInCms)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("Height (in cms)");
            entity.Property(e => e.HillSClassification)
                .HasMaxLength(3)
                .IsUnicode(false)
                .HasColumnName("Hill’s classification");
            entity.Property(e => e.HillSClassificationGrade)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("Hill’s classification Grade");
            entity.Property(e => e.HillSClassificationRemarks)
                .HasMaxLength(1000)
                .IsUnicode(false)
                .HasColumnName("Hill’s classification Remarks");
            entity.Property(e => e.HistoryOfEndoscopy)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("History of Endoscopy");
            entity.Property(e => e.HistoryOfGastroSurgery)
                .HasMaxLength(3)
                .IsUnicode(false)
                .HasColumnName("History of Gastro-surgery");
            entity.Property(e => e.Hypertension)
                .HasMaxLength(3)
                .IsUnicode(false);
            entity.Property(e => e.HypertensionRemarks)
                .HasMaxLength(250)
                .IsUnicode(false)
                .HasColumnName("Hypertension Remarks");
            entity.Property(e => e.Hyperthyroidism)
                .HasMaxLength(3)
                .IsUnicode(false);
            entity.Property(e => e.HyperthyroidismRemarks)
                .HasMaxLength(250)
                .IsUnicode(false)
                .HasColumnName("Hyperthyroidism Remarks");
            entity.Property(e => e.Hypothyroidism)
                .HasMaxLength(3)
                .IsUnicode(false);
            entity.Property(e => e.HypothyroidismRemarks)
                .HasMaxLength(250)
                .IsUnicode(false)
                .HasColumnName("Hypothyroidism Remarks");
            entity.Property(e => e.Initial)
                .HasMaxLength(250)
                .IsUnicode(false);
            entity.Property(e => e.JobOccupationType)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("Job/ Occupation type");
            entity.Property(e => e.Jogging)
                .HasMaxLength(3)
                .IsUnicode(false);
            entity.Property(e => e.JoggingDurationYrs)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("Jogging Duration (yrs)");
            entity.Property(e => e.JoggingFrequencyHrsWeek)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("Jogging Frequency (hrs/week)");
            entity.Property(e => e.KnownCaseOfGerd).HasColumnName("KnownCaseOfGERD");
            entity.Property(e => e.Laxlesclassification)
                .HasMaxLength(3)
                .IsUnicode(false)
                .HasColumnName("LAXlesclassification");
            entity.Property(e => e.LosAngelesGrade)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("Los Angeles Grade");
            entity.Property(e => e.LosAngelesGradeRemarks)
                .HasMaxLength(1000)
                .IsUnicode(false)
                .HasColumnName("Los Angeles Grade Remarks");
            entity.Property(e => e.ModerationOfAlcohol)
                .HasMaxLength(3)
                .IsUnicode(false)
                .HasColumnName("Moderation of alcohol");
            entity.Property(e => e.MtAttached).HasColumnName("MT_Attached");
            entity.Property(e => e.MtDate)
                .HasColumnType("datetime")
                .HasColumnName("MT_Date");
            entity.Property(e => e.MtRemark)
                .HasMaxLength(1000)
                .IsUnicode(false)
                .HasColumnName("MT_Remark");
            entity.Property(e => e.NeurologicalDisorder)
                .HasMaxLength(3)
                .IsUnicode(false)
                .HasColumnName("Neurological Disorder");
            entity.Property(e => e.NeurologicalDisorderRemarks)
                .HasMaxLength(250)
                .IsUnicode(false)
                .HasColumnName("Neurological Disorder Remarks");
            entity.Property(e => e.NsaidsDose)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("NSAIDs - Dose");
            entity.Property(e => e.NsaidsFrequency)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("NSAIDs - Frequency");
            entity.Property(e => e.NsaidsMoleculeName)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("NSAIDs - Molecule Name");
            entity.Property(e => e.Occupation)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Osteoarthritis)
                .HasMaxLength(3)
                .IsUnicode(false);
            entity.Property(e => e.OsteoarthritisRemarks)
                .HasMaxLength(250)
                .IsUnicode(false)
                .HasColumnName("Osteoarthritis Remarks");
            entity.Property(e => e.OtherComorbidity)
                .HasMaxLength(3)
                .IsUnicode(false)
                .HasColumnName("Other Comorbidity");
            entity.Property(e => e.OtherComorbiditySpecify)
                .HasMaxLength(250)
                .IsUnicode(false)
                .HasColumnName("Other Comorbidity- Specify");
            entity.Property(e => e.OtherDose)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("Other - Dose");
            entity.Property(e => e.OtherDrugMoleculeName)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("Other Drug - Molecule Name");
            entity.Property(e => e.OtherExamAreaSpecify)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("Other Exam Area (Specify)");
            entity.Property(e => e.OtherExamComments)
                .HasMaxLength(1000)
                .IsUnicode(false)
                .HasColumnName("Other Exam – Comments");
            entity.Property(e => e.OtherExamStatus)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("Other Exam – Status");
            entity.Property(e => e.OtherExercise)
                .HasMaxLength(3)
                .IsUnicode(false)
                .HasColumnName("Other Exercise");
            entity.Property(e => e.OtherExerciseDurationYrs)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("Other Exercise Duration (yrs)");
            entity.Property(e => e.OtherExerciseFrequencyHrsWeek)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("Other Exercise Frequency (hrs/week)");
            entity.Property(e => e.OtherFrequency)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("Other - Frequency");
            entity.Property(e => e.OtherGastroSurgery)
                .HasMaxLength(3)
                .IsUnicode(false)
                .HasColumnName("Other Gastro Surgery");
            entity.Property(e => e.OtherGastroSurgeryRemarks)
                .HasMaxLength(1000)
                .IsUnicode(false)
                .HasColumnName("Other Gastro Surgery Remarks");
            entity.Property(e => e.Otherdose1)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("otherdose");
            entity.Property(e => e.Otherfrequency1)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("otherfrequency");
            entity.Property(e => e.OthersMedicationName)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("others_Medication_Name");
            entity.Property(e => e.PHImpedanceMonitoring)
                .HasMaxLength(3)
                .IsUnicode(false)
                .HasColumnName("pH Impedance monitoring");
            entity.Property(e => e.PHImpedanceMonitoringDate)
                .HasColumnType("datetime")
                .HasColumnName("pH Impedance monitoring Date");
            entity.Property(e => e.PHImpedanceMonitoringRemarks)
                .HasMaxLength(1000)
                .IsUnicode(false)
                .HasColumnName("pH Impedance monitoring Remarks");
            entity.Property(e => e.PHimReportAttached)
                .HasMaxLength(3)
                .IsUnicode(false)
                .HasColumnName("pHIM Report Attached");
            entity.Property(e => e.PastHistory).HasColumnName("Past History");
            entity.Property(e => e.PcabDose)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("PCAB_Dose");
            entity.Property(e => e.PcabFrequency)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("PCAB_Frequency");
            entity.Property(e => e.PcabMedicationName)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("PCAB_Medication_Name");
            entity.Property(e => e.PerAbdomenExaminationFindings)
                .HasMaxLength(1000)
                .IsUnicode(false)
                .HasColumnName("Per Abdomen Examination Findings");
            entity.Property(e => e.Pincode).HasColumnName("pincode");
            entity.Property(e => e.PlaceType)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.PpiDose)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("PPI_Dose");
            entity.Property(e => e.PpiFrequency)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("PPI_Frequency");
            entity.Property(e => e.PpiMedicationName)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("PPI_Medication_Name");
            entity.Property(e => e.PpiUsage)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("PPI Usage");
            entity.Property(e => e.ProkineticsDose)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("Prokinetics_Dose");
            entity.Property(e => e.ProkineticsFrequency)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("Prokinetics_Frequency");
            entity.Property(e => e.ProkineticsMedicationName)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("Prokinetics_Medication_Name");
            entity.Property(e => e.RefractoryToPpi).HasColumnName("RefractoryToPPI");
            entity.Property(e => e.RegularExercise)
                .HasMaxLength(3)
                .IsUnicode(false)
                .HasColumnName("Regular exercise");
            entity.Property(e => e.RegurgitationDurationYrs).HasColumnName("RegurgitationDuration (Yrs)");
            entity.Property(e => e.RegurgitationFrequencyWk).HasColumnName("RegurgitationFrequency(/Wk)");
            entity.Property(e => e.RegurgitationNocturnal)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.RegurgitationPostural)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.ReportAttached)
                .HasMaxLength(3)
                .IsUnicode(false)
                .HasColumnName("Report Attached");
            entity.Property(e => e.RespiratorySystem)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("Respiratory System");
            entity.Property(e => e.RespiratorySystemComments)
                .HasMaxLength(1000)
                .IsUnicode(false)
                .HasColumnName("Respiratory System – Comments");
            entity.Property(e => e.RetrosternalNocturnal)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("Retrosternal Nocturnal");
            entity.Property(e => e.RetrosternalPainDurationYrs).HasColumnName("RetrosternalPainDuration (Yrs)");
            entity.Property(e => e.RetrosternalPainFrequencyWk).HasColumnName("RetrosternalPainFrequency(/Wk)");
            entity.Property(e => e.RetrosternalPostural)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("Retrosternal Postural");
            entity.Property(e => e.RheumatoidArthritis)
                .HasMaxLength(3)
                .IsUnicode(false)
                .HasColumnName("Rheumatoid Arthritis");
            entity.Property(e => e.RheumatoidArthritisRemrks)
                .HasMaxLength(250)
                .IsUnicode(false)
                .HasColumnName("Rheumatoid Arthritis Remrks");
            entity.Property(e => e.SleepApnea)
                .HasMaxLength(3)
                .IsUnicode(false)
                .HasColumnName("Sleep Apnea");
            entity.Property(e => e.SleepApneaDurationYears)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("Sleep Apnea Duration (years)");
            entity.Property(e => e.SleepApneaFrequencyWeek)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("Sleep Apnea Frequency (/week)");
            entity.Property(e => e.SmartphoneUsageDurationYears).HasColumnName("Smartphone Usage Duration (years)");
            entity.Property(e => e.SmartphoneUsageHrsDay)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("Smartphone Usage (hrs/day)");
            entity.Property(e => e.SmartphoneUse)
                .HasMaxLength(3)
                .IsUnicode(false)
                .HasColumnName("Smartphone Use");
            entity.Property(e => e.Smoking)
                .HasMaxLength(3)
                .IsUnicode(false);
            entity.Property(e => e.SmokingDurationYrs)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("Smoking Duration (yrs)");
            entity.Property(e => e.SmokingFrequencyDay)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("Smoking Frequency (/day)");
            entity.Property(e => e.SmokingQuantityPacks)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("Smoking Quantity (packs)");
            entity.Property(e => e.SocioeconomicStatus)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.SpicyFood)
                .HasMaxLength(3)
                .IsUnicode(false)
                .HasColumnName("Spicy Food");
            entity.Property(e => e.SpicyFoodDurationYrs)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("Spicy Food Duration (yrs)");
            entity.Property(e => e.SpicyFoodFrequencyWeek)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("Spicy Food Frequency(/week)");
            entity.Property(e => e.SpicyFoodQuantity)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("Spicy Food Quantity");
            entity.Property(e => e.StateName).HasMaxLength(50);
            entity.Property(e => e.SteroidsDose)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("Steroids - Dose");
            entity.Property(e => e.SteroidsFrequency)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("Steroids - Frequency");
            entity.Property(e => e.SteroidsMoleculeName)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("Steroids - Molecule Name");
            entity.Property(e => e.StopTobaccoUse)
                .HasMaxLength(3)
                .IsUnicode(false)
                .HasColumnName("Stop Tobacco use");
            entity.Property(e => e.SubjectNo)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.SucralfateDose)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("Sucralfate_Dose");
            entity.Property(e => e.SucralfateFrequency)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("Sucralfate_Frequency");
            entity.Property(e => e.SucralfateMedicationName)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("Sucralfate_Medication_Name");
            entity.Property(e => e.SymtopmScore)
                .HasMaxLength(103)
                .IsUnicode(false);
            entity.Property(e => e.SystemicSclerosis)
                .HasMaxLength(3)
                .IsUnicode(false)
                .HasColumnName("Systemic Sclerosis");
            entity.Property(e => e.SystemicSclerosisRemarks)
                .HasMaxLength(250)
                .IsUnicode(false)
                .HasColumnName("Systemic Sclerosis Remarks");
            entity.Property(e => e.Tea)
                .HasMaxLength(3)
                .IsUnicode(false);
            entity.Property(e => e.TeaDurationYrs)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("Tea Duration(yrs)");
            entity.Property(e => e.TeaFrequencyDay)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("Tea Frequency(/day)");
            entity.Property(e => e.TeaQuantityMl)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("Tea Quantity(ml)");
            entity.Property(e => e.TobaccoInOtherForms)
                .HasMaxLength(3)
                .IsUnicode(false)
                .HasColumnName("Tobacco in other forms");
            entity.Property(e => e.TobaccoInOtherFormsDurationYrs)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("Tobacco in other forms Duration (yrs)");
            entity.Property(e => e.TobaccoInOtherFormsFrequencyDay)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("Tobacco in other forms Frequency (/day)");
            entity.Property(e => e.TobaccoInOtherFormsQuantity)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("Tobacco in other forms Quantity");
            entity.Property(e => e.TotalPoints)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.TotalSymptomScoreinGerdpatients).HasColumnName("TotalSymptomScoreinGERDpatients");
            entity.Property(e => e.Walking)
                .HasMaxLength(3)
                .IsUnicode(false);
            entity.Property(e => e.WalkingDurationYrs)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("Walking Duration (yrs)");
            entity.Property(e => e.WalkingFrequencyHrsWeek)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("Walking Frequency (hrs/week)");
            entity.Property(e => e.WeightInKg)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("Weight (in Kg)");
            entity.Property(e => e.WeightLoss)
                .HasMaxLength(3)
                .IsUnicode(false)
                .HasColumnName("Weight loss");
            entity.Property(e => e.WorkingHoursOccupation)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("Working Hours (Occupation)");
            entity.Property(e => e.Yoga)
                .HasMaxLength(3)
                .IsUnicode(false);
            entity.Property(e => e.YogaDurationYrs)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("Yoga Duration (yrs)");
            entity.Property(e => e.YogaFrequencyHrsWeek)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("Yoga Frequency (hrs/week)");
            entity.Property(e => e.Zumba)
                .HasMaxLength(3)
                .IsUnicode(false);
            entity.Property(e => e.ZumbaDurationYrs)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("Zumba Duration (yrs)");
            entity.Property(e => e.ZumbaFrequencyHrsWeek)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("Zumba Frequency (hrs/week)");
        });

        modelBuilder.Entity<VwManagement>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("vw_Management");

            entity.Property(e => e.CreatedDt).HasColumnType("datetime");
            entity.Property(e => e.ManagementId)
                .ValueGeneratedOnAdd()
                .HasColumnName("ManagementID");
            entity.Property(e => e.ModifiedDt).HasColumnType("datetime");
            entity.Property(e => e.PatientId).HasColumnName("PatientID");
        });

        modelBuilder.Entity<VwMedicalExamination>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("vw_MedicalExamination");

            entity.Property(e => e.CreatedDt).HasColumnType("datetime");
            entity.Property(e => e.DoctorId).HasColumnName("DoctorID");
            entity.Property(e => e.Meid)
                .ValueGeneratedOnAdd()
                .HasColumnName("MEID");
            entity.Property(e => e.ModifiedDt).HasColumnType("datetime");
            entity.Property(e => e.OthersAbNormalCs)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("OthersAbNormal_CS");
            entity.Property(e => e.OthersAbNormalNcs)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("OthersAbNormal_NCS");
            entity.Property(e => e.OthersAbNormalRemark)
                .HasMaxLength(1000)
                .IsUnicode(false);
            entity.Property(e => e.OthersNormal)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.PaeFindings)
                .HasMaxLength(1000)
                .IsUnicode(false)
                .HasColumnName("PAE_Findings");
            entity.Property(e => e.PatientId).HasColumnName("PatientID");
            entity.Property(e => e.PeBmi)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("PE_BMI");
            entity.Property(e => e.PeHeight)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("PE_Height");
            entity.Property(e => e.PeWeight)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("PE_Weight");
            entity.Property(e => e.SeGaabNormalCs)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("SE_GAAbNormal_CS");
            entity.Property(e => e.SeGaabNormalNcs)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("SE_GAAbNormal_NCS");
            entity.Property(e => e.SeGaabNormalRemark)
                .HasMaxLength(1000)
                .IsUnicode(false)
                .HasColumnName("SE_GAAbNormalRemark");
            entity.Property(e => e.SeGanormal)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("SE_GANormal");
            entity.Property(e => e.SeRsabNormalCs)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("SE_RSAbNormal_CS");
            entity.Property(e => e.SeRsabNormalNcs)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("SE_RSAbNormal_NCS");
            entity.Property(e => e.SeRsabNormalRemark)
                .HasMaxLength(1000)
                .IsUnicode(false)
                .HasColumnName("SE_RSAbNormalRemark");
            entity.Property(e => e.SeRsnormal)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("SE_RSNormal");
        });

        modelBuilder.Entity<VwMedication>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("vw_Medication");

            entity.Property(e => e.CreatedDt).HasColumnType("datetime");
            entity.Property(e => e.Dose)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Frequency)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Ghid).HasColumnName("GHID");
            entity.Property(e => e.MedicationId)
                .ValueGeneratedOnAdd()
                .HasColumnName("MedicationID");
            entity.Property(e => e.MedicationName)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.ModifiedDt).HasColumnType("datetime");
            entity.Property(e => e.Molecule)
                .HasMaxLength(50)
                .IsUnicode(false);
        });

        modelBuilder.Entity<VwMedicationRpt>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("vw_MedicationRPT");

            entity.Property(e => e.City).HasColumnName("city");
            entity.Property(e => e.Gender)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Initial)
                .HasMaxLength(250)
                .IsUnicode(false);
            entity.Property(e => e.MedicationName)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.State).HasColumnName("state");
            entity.Property(e => e.SubjectNo)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Zone)
                .HasMaxLength(6)
                .IsUnicode(false);
        });

        modelBuilder.Entity<VwPatient>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("vw_Patient");

            entity.Property(e => e.CreatedDt).HasColumnType("datetime");
            entity.Property(e => e.Date).HasColumnType("datetime");
            entity.Property(e => e.Diet)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.DoctorId).HasColumnName("DoctorID");
            entity.Property(e => e.Education)
                .HasMaxLength(250)
                .IsUnicode(false);
            entity.Property(e => e.FamilyIncome)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Gender)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Initial)
                .HasMaxLength(250)
                .IsUnicode(false);
            entity.Property(e => e.ModifiedDt).HasColumnType("datetime");
            entity.Property(e => e.Occupation)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.PastHistory)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.PatientId)
                .ValueGeneratedOnAdd()
                .HasColumnName("PatientID");
            entity.Property(e => e.PlaceType)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.SocioeconomicStatus)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.SubjectNo)
                .HasMaxLength(50)
                .IsUnicode(false);
        });

        modelBuilder.Entity<VwPatientAgeRpt>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("vw_PatientAgeRPT");

            entity.Property(e => e.AgeGroup)
                .HasMaxLength(5)
                .IsUnicode(false);
            entity.Property(e => e.Gender)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Initial)
                .HasMaxLength(250)
                .IsUnicode(false);
        });

        modelBuilder.Entity<VwPatientHistory>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("vw_PatientHistory");

            entity.Property(e => e.AdDuration)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("AD_Duration");
            entity.Property(e => e.AdFrequency)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("AD_Frequency");
            entity.Property(e => e.AdIntake)
                .HasMaxLength(50)
                .HasColumnName("AD_Intake");
            entity.Property(e => e.AdQuantity)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("AD_Quantity");
            entity.Property(e => e.AerobicsDuration)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("Aerobics_Duration");
            entity.Property(e => e.AerobicsFrequency)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("Aerobics_Frequency");
            entity.Property(e => e.AerobicsIntake)
                .HasMaxLength(10)
                .HasColumnName("Aerobics_Intake");
            entity.Property(e => e.AhDuration)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("AH_Duration");
            entity.Property(e => e.AhFrequency)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("AH_Frequency");
            entity.Property(e => e.AhIntake)
                .HasMaxLength(50)
                .HasColumnName("AH_Intake");
            entity.Property(e => e.AhQuantity)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("AH_Quantity");
            entity.Property(e => e.CfDuration)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("CF_Duration");
            entity.Property(e => e.CfFrequency)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("CF_Frequency");
            entity.Property(e => e.CfIntake)
                .HasMaxLength(50)
                .HasColumnName("CF_Intake");
            entity.Property(e => e.CfQuantity)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("CF_Quantity");
            entity.Property(e => e.CreatedDt).HasColumnType("datetime");
            entity.Property(e => e.CsDuration)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("CS_Duration");
            entity.Property(e => e.CsFrequency)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("CS_Frequency");
            entity.Property(e => e.CsIntake)
                .HasMaxLength(50)
                .HasColumnName("CS_Intake");
            entity.Property(e => e.CsQuantity)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("CS_Quantity");
            entity.Property(e => e.Duration)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.ExerciseIntake)
                .HasMaxLength(10)
                .HasColumnName("Exercise_Intake");
            entity.Property(e => e.GFrequency)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("G_Frequency");
            entity.Property(e => e.GName)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("G_Name");
            entity.Property(e => e.GUsage)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("G_Usage");
            entity.Property(e => e.GYearOfUsage)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("G_YearOfUsage");
            entity.Property(e => e.GymDuration)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("Gym_Duration");
            entity.Property(e => e.GymFrequency)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("Gym_Frequency");
            entity.Property(e => e.GymIntake)
                .HasMaxLength(10)
                .HasColumnName("Gym_Intake");
            entity.Property(e => e.JobType)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.JoggingDuration)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("Jogging_Duration");
            entity.Property(e => e.JoggingFrequency)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("Jogging_Frequency");
            entity.Property(e => e.JoggingIntake)
                .HasMaxLength(10)
                .HasColumnName("Jogging_Intake");
            entity.Property(e => e.ModifiedDt).HasColumnType("datetime");
            entity.Property(e => e.OthersExerciseDuration)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("OthersExercise_Duration");
            entity.Property(e => e.OthersExerciseFrequency)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("OthersExercise_Frequency");
            entity.Property(e => e.OthersExerciseIntake)
                .HasMaxLength(10)
                .HasColumnName("OthersExercise_Intake");
            entity.Property(e => e.PastHistory).HasColumnName("Past_History");
            entity.Property(e => e.PatientHistoryId)
                .ValueGeneratedOnAdd()
                .HasColumnName("PatientHistoryID");
            entity.Property(e => e.SDuration)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("S_Duration");
            entity.Property(e => e.SFrequency)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("S_Frequency");
            entity.Property(e => e.SIntake)
                .HasMaxLength(50)
                .HasColumnName("S_Intake");
            entity.Property(e => e.SQuantity)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("S_Quantity");
            entity.Property(e => e.SfDuration)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("SF_Duration");
            entity.Property(e => e.SfFrequency)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("SF_Frequency");
            entity.Property(e => e.SfIntake)
                .HasMaxLength(50)
                .HasColumnName("SF_Intake");
            entity.Property(e => e.SfQuantity)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("SF_Quantity");
            entity.Property(e => e.SleepApneaDuration)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("SleepApnea_Duration");
            entity.Property(e => e.SleepApneaFrequency)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("SleepApnea_Frequency");
            entity.Property(e => e.SleepApneaIntake)
                .HasMaxLength(10)
                .HasColumnName("SleepApnea_Intake");
            entity.Property(e => e.TDuration)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("T_Duration");
            entity.Property(e => e.TFrequency)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("T_Frequency");
            entity.Property(e => e.TIntake)
                .HasMaxLength(50)
                .HasColumnName("T_Intake");
            entity.Property(e => e.TQuantity)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("T_Quantity");
            entity.Property(e => e.TbDuration)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("TB_Duration");
            entity.Property(e => e.TbFrequency)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("TB_Frequency");
            entity.Property(e => e.TbIntake)
                .HasMaxLength(50)
                .HasColumnName("TB_Intake");
            entity.Property(e => e.TbQuantity)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("TB_Quantity");
            entity.Property(e => e.WalkingDuration)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("Walking_Duration");
            entity.Property(e => e.WalkingFrequency)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("Walking_Frequency");
            entity.Property(e => e.WalkingIntake)
                .HasMaxLength(10)
                .HasColumnName("Walking_Intake");
            entity.Property(e => e.WorkingHours)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.YogaDuration)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("Yoga_Duration");
            entity.Property(e => e.YogaFrequency)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("Yoga_Frequency");
            entity.Property(e => e.YogaIntake)
                .HasMaxLength(10)
                .HasColumnName("Yoga_Intake");
            entity.Property(e => e.ZumbaDuration)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("Zumba_Duration");
            entity.Property(e => e.ZumbaFrequency)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("Zumba_Frequency");
            entity.Property(e => e.ZumbaIntake)
                .HasMaxLength(10)
                .HasColumnName("Zumba_Intake");
        });

        modelBuilder.Entity<VwPatientRpt>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("vw_PatientRPT");

            entity.Property(e => e.APresent)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("A_Present");
            entity.Property(e => e.ARemark)
                .HasMaxLength(250)
                .IsUnicode(false)
                .HasColumnName("A_Remark");
            entity.Property(e => e.AeratedDuration)
                .HasMaxLength(20)
                .IsUnicode(false);
            entity.Property(e => e.AeratedFrequency)
                .HasMaxLength(20)
                .IsUnicode(false);
            entity.Property(e => e.AeratedQuantity)
                .HasMaxLength(20)
                .IsUnicode(false);
            entity.Property(e => e.AerobicsDuration)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("aerobicsDuration");
            entity.Property(e => e.AerobicsFrequency)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("aerobicsFrequency");
            entity.Property(e => e.Aerobicsno)
                .HasMaxLength(3)
                .IsUnicode(false)
                .HasColumnName("aerobicsno");
            entity.Property(e => e.Aerobicsyes)
                .HasMaxLength(3)
                .IsUnicode(false)
                .HasColumnName("aerobicsyes");
            entity.Property(e => e.AlcoholDuration)
                .HasMaxLength(20)
                .IsUnicode(false);
            entity.Property(e => e.AlcoholFrequency)
                .HasMaxLength(20)
                .IsUnicode(false);
            entity.Property(e => e.AlcoholQuantity)
                .HasMaxLength(20)
                .IsUnicode(false);
            entity.Property(e => e.AntiplateletDose)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("Antiplatelet_Dose");
            entity.Property(e => e.AntiplateletFrequency)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("Antiplatelet_Frequency");
            entity.Property(e => e.AntiplateletMolecule)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("Antiplatelet_Molecule");
            entity.Property(e => e.AtDuration).HasColumnName("AT_Duration");
            entity.Property(e => e.AtFrequency).HasColumnName("AT_Frequency");
            entity.Property(e => e.AtNocturnal)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("AT_Nocturnal");
            entity.Property(e => e.AtPostural)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("AT_Postural");
            entity.Property(e => e.BdPresent)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("BD_Present");
            entity.Property(e => e.BdRemark)
                .HasMaxLength(250)
                .IsUnicode(false)
                .HasColumnName("BD_Remark");
            entity.Property(e => e.BisphosphonatesDose)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("Bisphosphonates_Dose");
            entity.Property(e => e.BisphosphonatesFrequency)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("Bisphosphonates_Frequency");
            entity.Property(e => e.BisphosphonatesMolecule)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("Bisphosphonates_Molecule");
            entity.Property(e => e.CPresent)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("C_Present");
            entity.Property(e => e.CRemark)
                .HasMaxLength(250)
                .IsUnicode(false)
                .HasColumnName("C_Remark");
            entity.Property(e => e.CdPresent)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("CD_Present");
            entity.Property(e => e.CdRemark)
                .HasMaxLength(250)
                .IsUnicode(false)
                .HasColumnName("CD_Remark");
            entity.Property(e => e.City).HasColumnName("city");
            entity.Property(e => e.CkdPresent)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("CKD_Present");
            entity.Property(e => e.CkdRemark)
                .HasMaxLength(250)
                .IsUnicode(false)
                .HasColumnName("CKD_Remark");
            entity.Property(e => e.CldPresent)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("CLD_Present");
            entity.Property(e => e.CldRemark)
                .HasMaxLength(250)
                .IsUnicode(false)
                .HasColumnName("CLD_Remark");
            entity.Property(e => e.CmoPresent)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("CMO_Present");
            entity.Property(e => e.CmoRemark)
                .HasMaxLength(250)
                .IsUnicode(false)
                .HasColumnName("CMO_Remark");
            entity.Property(e => e.CoffeeDuration)
                .HasMaxLength(20)
                .IsUnicode(false);
            entity.Property(e => e.CoffeeFrequency)
                .HasMaxLength(20)
                .IsUnicode(false);
            entity.Property(e => e.CoffeeQuantity)
                .HasMaxLength(20)
                .IsUnicode(false);
            entity.Property(e => e.ComputerFrequency)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Date).HasColumnType("datetime");
            entity.Property(e => e.DbPresent)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("DB_Present");
            entity.Property(e => e.DbRemark)
                .HasMaxLength(250)
                .IsUnicode(false)
                .HasColumnName("DB_Remark");
            entity.Property(e => e.DdPresent)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("DD_Present");
            entity.Property(e => e.DdRemark)
                .HasMaxLength(250)
                .IsUnicode(false)
                .HasColumnName("DD_Remark");
            entity.Property(e => e.DietVegetarian).HasColumnName("Diet_Vegetarian");
            entity.Property(e => e.Education)
                .HasMaxLength(250)
                .IsUnicode(false);
            entity.Property(e => e.EndoscopyDate).HasColumnType("datetime");
            entity.Property(e => e.EndoscopyRemark)
                .HasMaxLength(1000)
                .IsUnicode(false);
            entity.Property(e => e.ExerciseIntakeno)
                .HasMaxLength(3)
                .IsUnicode(false)
                .HasColumnName("exerciseIntakeno");
            entity.Property(e => e.ExerciseIntakeyes)
                .HasMaxLength(3)
                .IsUnicode(false)
                .HasColumnName("exerciseIntakeyes");
            entity.Property(e => e.FamilyIncome)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.FhEgc)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("FH_EGC");
            entity.Property(e => e.FhEgcremark)
                .HasMaxLength(250)
                .IsUnicode(false)
                .HasColumnName("FH_EGCRemark");
            entity.Property(e => e.FhGred)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("FH_GRED");
            entity.Property(e => e.FhRemark)
                .HasMaxLength(250)
                .IsUnicode(false)
                .HasColumnName("FH_Remark");
            entity.Property(e => e.Gender)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.GsBariatricSurgery).HasColumnName("GS_BariatricSurgery");
            entity.Property(e => e.GsBsremark)
                .HasMaxLength(1000)
                .IsUnicode(false)
                .HasColumnName("GS_BSRemark");
            entity.Property(e => e.GsGastricPoemsurgery).HasColumnName("GS_GastricPOEMSurgery");
            entity.Property(e => e.GsGastrojejunostomy).HasColumnName("GS_Gastrojejunostomy");
            entity.Property(e => e.GsGjremark)
                .HasMaxLength(1000)
                .IsUnicode(false)
                .HasColumnName("GS_GJRemark");
            entity.Property(e => e.GsGpsremark)
                .HasMaxLength(1000)
                .IsUnicode(false)
                .HasColumnName("GS_GPSRemark");
            entity.Property(e => e.GsOther).HasColumnName("GS_Other");
            entity.Property(e => e.GsOtherRemark)
                .HasMaxLength(1000)
                .IsUnicode(false)
                .HasColumnName("GS_OtherRemark");
            entity.Property(e => e.GymDuration)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("gymDuration");
            entity.Property(e => e.GymFrequency)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("gymFrequency");
            entity.Property(e => e.GymSelectedno)
                .HasMaxLength(3)
                .IsUnicode(false)
                .HasColumnName("gymSelectedno");
            entity.Property(e => e.GymSelectedyes)
                .HasMaxLength(3)
                .IsUnicode(false)
                .HasColumnName("gymSelectedyes");
            entity.Property(e => e.HistoryofEndoscopy)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.HistoryofGs).HasColumnName("HistoryofGS");
            entity.Property(e => e.HtPresent)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("HT_Present");
            entity.Property(e => e.HtRemark)
                .HasMaxLength(250)
                .IsUnicode(false)
                .HasColumnName("HT_Remark");
            entity.Property(e => e.HtdPresent)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("HTD_Present");
            entity.Property(e => e.HtdRemark)
                .HasMaxLength(250)
                .IsUnicode(false)
                .HasColumnName("HTD_Remark");
            entity.Property(e => e.Initial)
                .HasMaxLength(250)
                .IsUnicode(false);
            entity.Property(e => e.JobType)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.JoggingDuration)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("joggingDuration");
            entity.Property(e => e.JoggingFrequency)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("joggingFrequency");
            entity.Property(e => e.JoggingSelectedno)
                .HasMaxLength(3)
                .IsUnicode(false)
                .HasColumnName("joggingSelectedno");
            entity.Property(e => e.JoggingSelectedyes)
                .HasMaxLength(3)
                .IsUnicode(false)
                .HasColumnName("joggingSelectedyes");
            entity.Property(e => e.NdPresent)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("ND_Present");
            entity.Property(e => e.NdRemark)
                .HasMaxLength(250)
                .IsUnicode(false)
                .HasColumnName("ND_Remark");
            entity.Property(e => e.NsaidsDose)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("NSAIDs_Dose");
            entity.Property(e => e.NsaidsFrequency)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("NSAIDs_Frequency");
            entity.Property(e => e.NsaidsMolecule)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("NSAIDs_Molecule");
            entity.Property(e => e.OPresent)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("O_Present");
            entity.Property(e => e.ORemark)
                .HasMaxLength(250)
                .IsUnicode(false)
                .HasColumnName("O_Remark");
            entity.Property(e => e.Occupation)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.OthersDose)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("Others_Dose");
            entity.Property(e => e.OthersDuration)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("othersDuration");
            entity.Property(e => e.OthersFrequency)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("othersFrequency");
            entity.Property(e => e.OthersFrequency1)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("Others_Frequency");
            entity.Property(e => e.OthersMolecule)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("Others_Molecule");
            entity.Property(e => e.Othersno)
                .HasMaxLength(3)
                .IsUnicode(false)
                .HasColumnName("othersno");
            entity.Property(e => e.Othersyes)
                .HasMaxLength(3)
                .IsUnicode(false)
                .HasColumnName("othersyes");
            entity.Property(e => e.PastHistory).HasColumnName("Past_History");
            entity.Property(e => e.PlaceType)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.RDuration).HasColumnName("R_Duration");
            entity.Property(e => e.RFrequency).HasColumnName("R_Frequency");
            entity.Property(e => e.RNocturnal)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("R_Nocturnal");
            entity.Property(e => e.RPostural)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("R_Postural");
            entity.Property(e => e.RaPresent)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("RA_Present");
            entity.Property(e => e.RaRemark)
                .HasMaxLength(250)
                .IsUnicode(false)
                .HasColumnName("RA_Remark");
            entity.Property(e => e.RpDuration).HasColumnName("RP_Duration");
            entity.Property(e => e.RpFrequency).HasColumnName("RP_Frequency");
            entity.Property(e => e.RpNocturnal)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("RP_Nocturnal");
            entity.Property(e => e.RpPostural)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("RP_Postural");
            entity.Property(e => e.SleepApneaDuration)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("sleepApneaDuration");
            entity.Property(e => e.SleepApneaFrequency)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("sleepApneaFrequency");
            entity.Property(e => e.SleepApneano)
                .HasMaxLength(3)
                .IsUnicode(false)
                .HasColumnName("sleepApneano");
            entity.Property(e => e.SleepApneayes)
                .HasMaxLength(3)
                .IsUnicode(false)
                .HasColumnName("sleepApneayes");
            entity.Property(e => e.SmartphoneFrequency)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.SmokingDuration)
                .HasMaxLength(20)
                .IsUnicode(false);
            entity.Property(e => e.SmokingFrequency)
                .HasMaxLength(20)
                .IsUnicode(false);
            entity.Property(e => e.SmokingQuantity)
                .HasMaxLength(20)
                .IsUnicode(false);
            entity.Property(e => e.SocioeconomicStatus)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.SpicyDuration)
                .HasMaxLength(20)
                .IsUnicode(false);
            entity.Property(e => e.SpicyFrequency)
                .HasMaxLength(20)
                .IsUnicode(false);
            entity.Property(e => e.SpicyQuantity)
                .HasMaxLength(20)
                .IsUnicode(false);
            entity.Property(e => e.SsPresent)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("SS_Present");
            entity.Property(e => e.SsRemark)
                .HasMaxLength(250)
                .IsUnicode(false)
                .HasColumnName("SS_Remark");
            entity.Property(e => e.State).HasColumnName("state");
            entity.Property(e => e.SteroidsDose)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("Steroids_Dose");
            entity.Property(e => e.SteroidsFrequency)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("Steroids_Frequency");
            entity.Property(e => e.SteroidsMolecule)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("Steroids_Molecule");
            entity.Property(e => e.SubjectNo)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.SweetsDuration)
                .HasMaxLength(20)
                .IsUnicode(false);
            entity.Property(e => e.SweetsFrequency)
                .HasMaxLength(20)
                .IsUnicode(false);
            entity.Property(e => e.SweetsQuantity)
                .HasMaxLength(20)
                .IsUnicode(false);
            entity.Property(e => e.TeaDuration)
                .HasMaxLength(20)
                .IsUnicode(false);
            entity.Property(e => e.TeaFrequency)
                .HasMaxLength(20)
                .IsUnicode(false);
            entity.Property(e => e.TobaccoDuration)
                .HasMaxLength(20)
                .IsUnicode(false);
            entity.Property(e => e.TobaccoFrequency)
                .HasMaxLength(20)
                .IsUnicode(false);
            entity.Property(e => e.TobaccoQuantity)
                .HasMaxLength(20)
                .IsUnicode(false);
            entity.Property(e => e.UsageOfPpi)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("UsageOfPPI");
            entity.Property(e => e.WalkingDuration)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("walkingDuration");
            entity.Property(e => e.WalkingFrequency)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("walkingFrequency");
            entity.Property(e => e.WalkingSelectedno)
                .HasMaxLength(3)
                .IsUnicode(false)
                .HasColumnName("walkingSelectedno");
            entity.Property(e => e.WalkingSelectedyes)
                .HasMaxLength(3)
                .IsUnicode(false)
                .HasColumnName("walkingSelectedyes");
            entity.Property(e => e.WorkingHours)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.YogaDuration)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("yogaDuration");
            entity.Property(e => e.YogaFrequency)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("yogaFrequency");
            entity.Property(e => e.YogaSelectedno)
                .HasMaxLength(3)
                .IsUnicode(false)
                .HasColumnName("yogaSelectedno");
            entity.Property(e => e.YogaSelectedyes)
                .HasMaxLength(3)
                .IsUnicode(false)
                .HasColumnName("yogaSelectedyes");
            entity.Property(e => e.ZumbaDuration)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("zumbaDuration");
            entity.Property(e => e.ZumbaFrequency)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("zumbaFrequency");
            entity.Property(e => e.Zumbano)
                .HasMaxLength(3)
                .IsUnicode(false)
                .HasColumnName("zumbano");
            entity.Property(e => e.Zumbayes)
                .HasMaxLength(3)
                .IsUnicode(false)
                .HasColumnName("zumbayes");
        });

        modelBuilder.Entity<VwPersonalHistory>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("vw_PersonalHistory");

            entity.Property(e => e.AeratedDuration)
                .HasMaxLength(20)
                .IsUnicode(false);
            entity.Property(e => e.AeratedFrequency)
                .HasMaxLength(20)
                .IsUnicode(false);
            entity.Property(e => e.AeratedQuantity)
                .HasMaxLength(20)
                .IsUnicode(false);
            entity.Property(e => e.AlcoholDuration)
                .HasMaxLength(20)
                .IsUnicode(false);
            entity.Property(e => e.AlcoholFrequency)
                .HasMaxLength(20)
                .IsUnicode(false);
            entity.Property(e => e.AlcoholQuantity)
                .HasMaxLength(20)
                .IsUnicode(false);
            entity.Property(e => e.CoffeeDuration)
                .HasMaxLength(20)
                .IsUnicode(false);
            entity.Property(e => e.CoffeeFrequency)
                .HasMaxLength(20)
                .IsUnicode(false);
            entity.Property(e => e.CoffeeQuantity)
                .HasMaxLength(20)
                .IsUnicode(false);
            entity.Property(e => e.CreatedDt).HasColumnType("datetime");
            entity.Property(e => e.ModifiedDt).HasColumnType("datetime");
            entity.Property(e => e.PersonalHistoryId).ValueGeneratedOnAdd();
            entity.Property(e => e.SmokingDuration)
                .HasMaxLength(20)
                .IsUnicode(false);
            entity.Property(e => e.SmokingFrequency)
                .HasMaxLength(20)
                .IsUnicode(false);
            entity.Property(e => e.SmokingQuantity)
                .HasMaxLength(20)
                .IsUnicode(false);
            entity.Property(e => e.SpicyDuration)
                .HasMaxLength(20)
                .IsUnicode(false);
            entity.Property(e => e.SpicyFrequency)
                .HasMaxLength(20)
                .IsUnicode(false);
            entity.Property(e => e.SpicyQuantity)
                .HasMaxLength(20)
                .IsUnicode(false);
            entity.Property(e => e.SweetsDuration)
                .HasMaxLength(20)
                .IsUnicode(false);
            entity.Property(e => e.SweetsFrequency)
                .HasMaxLength(20)
                .IsUnicode(false);
            entity.Property(e => e.SweetsQuantity)
                .HasMaxLength(20)
                .IsUnicode(false);
            entity.Property(e => e.TeaDuration)
                .HasMaxLength(20)
                .IsUnicode(false);
            entity.Property(e => e.TeaFrequency)
                .HasMaxLength(20)
                .IsUnicode(false);
            entity.Property(e => e.TeaQuantity)
                .HasMaxLength(20)
                .IsUnicode(false);
            entity.Property(e => e.TobaccoDuration)
                .HasMaxLength(20)
                .IsUnicode(false);
            entity.Property(e => e.TobaccoFrequency)
                .HasMaxLength(20)
                .IsUnicode(false);
            entity.Property(e => e.TobaccoQuantity)
                .HasMaxLength(20)
                .IsUnicode(false);
        });

        modelBuilder.Entity<VwSleep>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("vw_Sleep");

            entity.Property(e => e.AerobicsDuration)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("aerobicsDuration");
            entity.Property(e => e.AerobicsFrequency)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("aerobicsFrequency");
            entity.Property(e => e.Aerobicsno)
                .HasMaxLength(3)
                .IsUnicode(false)
                .HasColumnName("aerobicsno");
            entity.Property(e => e.Aerobicsyes)
                .HasMaxLength(3)
                .IsUnicode(false)
                .HasColumnName("aerobicsyes");
            entity.Property(e => e.CreatedAt).HasColumnType("datetime");
            entity.Property(e => e.ExerciseIntakeno)
                .HasMaxLength(3)
                .IsUnicode(false)
                .HasColumnName("exerciseIntakeno");
            entity.Property(e => e.ExerciseIntakeyes)
                .HasMaxLength(3)
                .IsUnicode(false)
                .HasColumnName("exerciseIntakeyes");
            entity.Property(e => e.GymDuration)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("gymDuration");
            entity.Property(e => e.GymFrequency)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("gymFrequency");
            entity.Property(e => e.GymSelectedno)
                .HasMaxLength(3)
                .IsUnicode(false)
                .HasColumnName("gymSelectedno");
            entity.Property(e => e.GymSelectedyes)
                .HasMaxLength(3)
                .IsUnicode(false)
                .HasColumnName("gymSelectedyes");
            entity.Property(e => e.Id).ValueGeneratedOnAdd();
            entity.Property(e => e.JoggingDuration)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("joggingDuration");
            entity.Property(e => e.JoggingFrequency)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("joggingFrequency");
            entity.Property(e => e.JoggingSelectedno)
                .HasMaxLength(3)
                .IsUnicode(false)
                .HasColumnName("joggingSelectedno");
            entity.Property(e => e.JoggingSelectedyes)
                .HasMaxLength(3)
                .IsUnicode(false)
                .HasColumnName("joggingSelectedyes");
            entity.Property(e => e.ModifiedDt).HasColumnType("datetime");
            entity.Property(e => e.OthersDuration)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("othersDuration");
            entity.Property(e => e.OthersFrequency)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("othersFrequency");
            entity.Property(e => e.OthersText)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("othersText");
            entity.Property(e => e.Othersno)
                .HasMaxLength(3)
                .IsUnicode(false)
                .HasColumnName("othersno");
            entity.Property(e => e.Othersyes)
                .HasMaxLength(3)
                .IsUnicode(false)
                .HasColumnName("othersyes");
            entity.Property(e => e.SleepApneaDuration)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("sleepApneaDuration");
            entity.Property(e => e.SleepApneaFrequency)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("sleepApneaFrequency");
            entity.Property(e => e.SleepApneano)
                .HasMaxLength(3)
                .IsUnicode(false)
                .HasColumnName("sleepApneano");
            entity.Property(e => e.SleepApneayes)
                .HasMaxLength(3)
                .IsUnicode(false)
                .HasColumnName("sleepApneayes");
            entity.Property(e => e.Stage).HasColumnName("stage");
            entity.Property(e => e.WalkingDuration)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("walkingDuration");
            entity.Property(e => e.WalkingFrequency)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("walkingFrequency");
            entity.Property(e => e.WalkingSelectedno)
                .HasMaxLength(3)
                .IsUnicode(false)
                .HasColumnName("walkingSelectedno");
            entity.Property(e => e.WalkingSelectedyes)
                .HasMaxLength(3)
                .IsUnicode(false)
                .HasColumnName("walkingSelectedyes");
            entity.Property(e => e.YogaDuration)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("yogaDuration");
            entity.Property(e => e.YogaFrequency)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("yogaFrequency");
            entity.Property(e => e.YogaSelectedno)
                .HasMaxLength(3)
                .IsUnicode(false)
                .HasColumnName("yogaSelectedno");
            entity.Property(e => e.YogaSelectedyes)
                .HasMaxLength(3)
                .IsUnicode(false)
                .HasColumnName("yogaSelectedyes");
            entity.Property(e => e.ZumbaDuration)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("zumbaDuration");
            entity.Property(e => e.ZumbaFrequency)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("zumbaFrequency");
            entity.Property(e => e.Zumbano)
                .HasMaxLength(3)
                .IsUnicode(false)
                .HasColumnName("zumbano");
            entity.Property(e => e.Zumbayes)
                .HasMaxLength(3)
                .IsUnicode(false)
                .HasColumnName("zumbayes");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
