using gred.Data;


//using gred.Repositories;
using Gred.Repositories;
using Gred.Repositories.Implementation;
using Gred.Repositories.Interface;
using Gred.Repository;
using Gred.Services.Interface;

namespace Gred.PersistenceService
{
    public static class PresistanceServiceRegistration
    {
        public static IServiceCollection AddPersistanceService(this IServiceCollection services
            , IConfiguration configuration)
        {
            services.AddDbContext<GredDbContext>();
            services.AddScoped<IDoctorReg, DoctorRegRepository>();
            services.AddScoped<IPatientReg, PatientRegRepository>();
            services.AddScoped<ICheifComplaint, CheifComplaintRepository>();
            services.AddScoped<IComorbidities, ComorbiditiesRepository>();
            services.AddScoped<IFamilyHistory, FamilyHistoryRepository>();
            services.AddScoped<IGerdHistory, GerdHistoryRepository>();
            services.AddScoped<IDoctorLog, DoctorLogRepository>();
            services.AddScoped<ILogin, LoginRepository>();
            services.AddScoped<IPatientHistory, PatientHistoryRepository>();
            services.AddScoped<ILogin, LoginRepository>();
            services.AddScoped<IDiagnosis, DiagnosisRepository>();
            services.AddScoped<ICurrentMedication, CurrentMedicatonRepositary>();
            services.AddScoped<IGadget, GadgetRepository>();
      //services.AddScoped<IHistoryEndsocopy, HistoryEndsocopyRepository>();
            services.AddScoped<IManagement, ManagementRepository>();
            services.AddScoped<IMedicalExamination, MedicalExaminationRepository>();
            services.AddScoped<IAssessment, AssessmentRepository>();
            services.AddScoped<ISleep, SleepRepository>();
            //services.AddScoped<IAssessment, AssessmentRepository>();
            services.AddScoped<IHistory, HistoryRepository>();
            services.AddScoped<IPersonalHistory, PersonalHistoryRepository>();
            services.AddScoped<IDbService, DbService>();
      services.AddScoped<IMedicationService, MedicationRepository>();
      services.AddScoped<IState, StateRepository>();
      services.AddScoped<ICountry, CountriesRepository>();
      services.AddScoped<ICities, citiesRepository>();
      services.AddScoped<IPatientSubmit, PatientSubmitRepository>();
      services.AddScoped<IGenderRPT, GenderRPTRepository>();
      services.AddScoped<IComorbitiesRptRepository, ComorbitiesRptRepository>();
      services.AddScoped<IVwMedicationRptRepository, VwMedicationRptRepository>();
      services.AddScoped<IPtnTrackRepository, PtnTrackRepository>();
      
      return services;
        }
    }
}
