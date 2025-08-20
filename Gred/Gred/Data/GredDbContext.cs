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

    public virtual DbSet<VwPatientHistory> VwPatientHistories { get; set; }

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
            entity.HasKey(e => e.DoctorlogId).HasName("PK__DoctorLo__2E8573BA84277F4D");

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
        });

        modelBuilder.Entity<Gadget>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Gadget__3214EC07100F3023");

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
        });

        modelBuilder.Entity<History>(entity =>
        {
            entity.HasKey(e => e.HistoryId).HasName("PK__History__4D7B4ADD1C8A88A1");

            entity.ToTable("History");

            entity.Property(e => e.HistoryId).HasColumnName("HistoryID");
            entity.Property(e => e.CreatedDate).HasColumnType("datetime");
            entity.Property(e => e.DietNonVegetarian).HasColumnName("Diet_NonVegetarian");
            entity.Property(e => e.DietVegetarian).HasColumnName("Diet_Vegetarian");
            entity.Property(e => e.DoctorId).HasColumnName("DoctorID");
            entity.Property(e => e.ModifiedDate).HasColumnType("datetime");
            entity.Property(e => e.PastHistory).HasColumnName("Past_History");
            entity.Property(e => e.PatientId).HasColumnName("PatientID");
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
        });

        modelBuilder.Entity<MedicalExamination>(entity =>
        {
            entity.HasKey(e => e.Meid).HasName("PK__MedicalE__1A36DA7A256C2DDE");

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
            entity.HasKey(e => e.PersonalHistoryId).HasName("PK__Personal__1ED0A1AD88999F4A");

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
            entity.HasKey(e => e.Id).HasName("PK__Sleep__3214EC0707506317");

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

            entity.Property(e => e.APresent)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("A_Present");
            entity.Property(e => e.ARemark)
                .HasMaxLength(250)
                .IsUnicode(false)
                .HasColumnName("A_Remark");
            entity.Property(e => e.AcidRefluxSymptom)
                .HasMaxLength(50)
                .IsUnicode(false);
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
            entity.Property(e => e.CityName).HasMaxLength(100);
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
            entity.Property(e => e.DietModifications)
                .HasMaxLength(3)
                .IsUnicode(false)
                .HasColumnName("Diet Modifications");
            entity.Property(e => e.DietVegetarian).HasColumnName("Diet_Vegetarian");
            entity.Property(e => e.Dose)
                .HasMaxLength(1)
                .IsUnicode(false);
            entity.Property(e => e.Dysmotity)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Education)
                .HasMaxLength(250)
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
            entity.Property(e => e.Frequency)
                .HasMaxLength(1)
                .IsUnicode(false);
            entity.Property(e => e.Gender)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Gerdtype)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("GERDType");
            entity.Property(e => e.GredNoOfYear).HasColumnName("GRED_NoOfYear");
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
            entity.Property(e => e.KnownCaseOfGerd).HasColumnName("KnownCaseOfGERD");
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
            entity.Property(e => e.Otherdose)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("otherdose");
            entity.Property(e => e.Otherfrequency)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("otherfrequency");
            entity.Property(e => e.OthersAbNormalCs)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("OthersAbNormal_CS");
            entity.Property(e => e.OthersAbNormalRemark)
                .HasMaxLength(1000)
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
            entity.Property(e => e.OthersMedicationName)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("others_Medication_Name");
            entity.Property(e => e.OthersMolecule)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("Others_Molecule");
            entity.Property(e => e.OthersNormal)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Othersno)
                .HasMaxLength(3)
                .IsUnicode(false)
                .HasColumnName("othersno");
            entity.Property(e => e.Othersyes)
                .HasMaxLength(3)
                .IsUnicode(false)
                .HasColumnName("othersyes");
            entity.Property(e => e.PHimAttached).HasColumnName("pHIM_Attached");
            entity.Property(e => e.PHimDate)
                .HasColumnType("datetime")
                .HasColumnName("pHIM_Date");
            entity.Property(e => e.PHimRemark)
                .HasMaxLength(1000)
                .IsUnicode(false)
                .HasColumnName("pHIM_Remark");
            entity.Property(e => e.PHimpedanceMonitoring).HasColumnName("pHImpedanceMonitoring");
            entity.Property(e => e.PaeFindings)
                .HasMaxLength(1000)
                .IsUnicode(false)
                .HasColumnName("PAE_Findings");
            entity.Property(e => e.PastHistory).HasColumnName("Past_History");
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
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("RA_Present");
            entity.Property(e => e.RaRemark)
                .HasMaxLength(250)
                .IsUnicode(false)
                .HasColumnName("RA_Remark");
            entity.Property(e => e.RefractoryToPpi).HasColumnName("RefractoryToPPI");
            entity.Property(e => e.RegularExercise)
                .HasMaxLength(3)
                .IsUnicode(false)
                .HasColumnName("Regular exercise");
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
            entity.Property(e => e.SeGaabNormalRemark)
                .HasMaxLength(1000)
                .IsUnicode(false)
                .HasColumnName("SE_GAAbNormalRemark");
            entity.Property(e => e.SeGanormal)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("SE_GANormal");
            entity.Property(e => e.SeRsabNormalRemark)
                .HasMaxLength(1000)
                .IsUnicode(false)
                .HasColumnName("SE_RSAbNormalRemark");
            entity.Property(e => e.SeRsnormal)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("SE_RSNormal");
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
            entity.Property(e => e.StateName).HasMaxLength(50);
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
            entity.Property(e => e.SweetsDuration)
                .HasMaxLength(20)
                .IsUnicode(false);
            entity.Property(e => e.SweetsFrequency)
                .HasMaxLength(20)
                .IsUnicode(false);
            entity.Property(e => e.SweetsQuantity)
                .HasMaxLength(20)
                .IsUnicode(false);
            entity.Property(e => e.SymtopmScore)
                .HasMaxLength(103)
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
            entity.Property(e => e.TotalPoints)
                .HasMaxLength(50)
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
            entity.Property(e => e.WeightLoss)
                .HasMaxLength(3)
                .IsUnicode(false)
                .HasColumnName("Weight loss");
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
            entity.Property(e => e.CityName).HasMaxLength(100);
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
            entity.Property(e => e.StateName).HasMaxLength(50);
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
            entity.Property(e => e.CityName).HasMaxLength(100);
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
            entity.Property(e => e.StateName).HasMaxLength(50);
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

        modelBuilder.Entity<VwFollowup2Rpt>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("vw_Followup2RPT");

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
            entity.Property(e => e.CityName).HasMaxLength(100);
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
            entity.Property(e => e.StateName).HasMaxLength(50);
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
