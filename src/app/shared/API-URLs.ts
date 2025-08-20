export const API_URLS = {
//BASE_URL: 'https://www.gerdregistryofindia.com/GERD/api',
  
BASE_URL: 'http://localhost:5058/api',
  
  KEY: 'TSSACPAPI73263707',
  CHEIF_COMPLAINT_SAVE: '/CheifComplaint/SaveCheifComplaint',
  CHEIF_COMPLAINT_GET: '/CheifComplaint/GetCheifComplaint',

  COMORBIDITIES_GET: '/Comorbidities/GetComorbidities',
  COMORBIDITIES_SAVE: '/Comorbidities/SaveComorbidities',
  GET_COMORBIDITIES_BY_ID: '/Comorbidities/GetComorbditiesById',

  DIAGNOSIS_SAVE: '/Diagnosis/SaveDiagnosis',
  DIAGNOSIS_GET_DOCTOR: '/Diagnosis/GetDoctor',

  DOCTOR_REG_SAVE: '/DoctorReg/SaveDoctorReg',
  DOCTOR_REG_GET: '/DoctorReg/GetDoctor',

  FAMILY_HISTORY_GET: '/FamiyHistory/GetFamilyHistory',
  FAMILY_HISTORY_SAVE: '/FamiyHistory/SaveFamilyHistory',
  FAMILY_HISTORY_GET_BY_ID: '/FamiyHistory/GetFamilyHistoryById/',

  MedicationY_SAVE: '/Medication/SaveMedication',
  MEDICATION_GET_BY_ID: '/Medication/GetMedicationByPatientId/{patientId}/{stage}',

  GERD_HISTORY_ADD: '/GerdHistory/AddGerdHistory',
  GERD_HISTORY_GET: '/GerdHistory/GetGerdHistory',
  GERD_HISTORY_GET_BY_ID: '/GerdHistory/GetGerdHistoryById/{patientId}',

  PATIENT_HISTORY_SAVE: '/PatientHistory/SavePatientHistory',
  PATIENT_HISTORY_GET: '/PatientHistory/GetPatientHistory',
  PATIENT_HISTORY_GETByID: '/PatientHistory/GetPatientHistoryById',

  CURRENT_MEDICATION_SAVE: '/CurrentMedication/SaveCurrentMedication',
  CURRENT_MEDICATION_GET: '/CurrentMedication/GetCurrentMedication',

  MEDICAL_EXAM_SAVE: '/MedicalExamination/SaveMedicalExamination',
  MEDICAL_EXAM_GET: '/MedicalExamination/GetMedicalExamination',

  HISTORY_SAVE: '/History/SaveHistory',
  HISTORY_GET: '/GerdHistory/GetGerdHistory',

  SLEEP_SAVE: '/Sleep/SaveSleep',
  SLEEP_GET: '/Sleep/GetSleep',
  SLEEP_GET_BY_ID: '/api/Sleep/GetSleep',

  GADGET_SAVE: '/Gadget/SaveGadget',
  GADGET_GET: '/Gadget/GetGadget',
  GADGET_GTE_ById: '/Gadget/GetGadgetById/{patientId}',

  STATE_GET: '/State/GetState',
  CITY_GET: '/City/GetCitiesById',

  PERSONAL_HISTORY_SAVE: '/PersonalHistory/SavePersonalHistory',
  PERSONAL_HISTORY_GET: '/PersonalHistory/GetPersonalHistory',
  PERSONAL_HISTORY_GET_ByID: '/PersonalHistory/GetPersonalHistoryById/{id}',

  LOGIN: '/Auth/login',
  MANAGEMENT_SAVE: '/Management/SaveManagement',
  MANAGEMENT_CompleteCase: '/Management/CompleteCase',

  WEATHER_FORECAST: '/WeatherForecast',

  GET_CURRENT_STAGE: '/Management/GetCurrentStage',
  PATIENT_REG_GET: '/PatientReg/GetPatient',

  GET_PAGE_ROUTER: '/PtnTrack/GetPageRouterByPatientId/{patientId}'
};    