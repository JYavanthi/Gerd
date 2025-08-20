import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { HomeComponent } from './home/home.component';
import { ContactComponent } from './contact/contact.component';
import { DoctorsRegistrationComponent } from './doctors-registration/doctors-registration.component';
import { LoginFormComponent } from './login-form/login-form.component';
import { DashboardComponent } from './dashboard/dashboard.component';
import { DemographicComponent } from './demographic/demographic.component';
import { FollowUpComponent } from './follow-up/follow-up.component';
import { CaseDetailsComponent } from './case-details/case-details.component';
import { HistoryComponent } from './history/history.component';
import { ChiefComplaintComponent } from './chief-complaint/chief-complaint.component';
import { ComorbiditiesComponent } from './comorbidities/comorbidities.component';
import { PersonalHistoryComponent } from './personal-history/personal-history.component';
import { FamilyHistoryComponent } from './family-history/family-history.component';
import { HistoryEndoscopyComponent } from './history-endoscopy/history-endoscopy.component';
import { CurrentMedicationsComponent } from './current-medications/current-medications.component';
import { SleepComponent } from './sleep/sleep.component';
import { GadgetComponent } from './gadget/gadget.component';
import { MedicalExaminationComponent } from './medical-examination/medical-examination.component';
import { DiagnosisComponent } from './diagnosis/diagnosis.component';
import { ManagamentComponent } from './managament/managament.component';
import { AssessmentComponent } from './assessment/assessment.component';
import { AboutGerdRegistryComponent } from './about-gerd-registry/about-gerd-registry.component';
import { GuidelineComponent } from './guideline/guideline.component';
import { ContactUsComponent } from './contact-us/contact-us.component';
import { AdminDashboardComponent } from './admin-dashboard/admin-dashboard.component';
import { GenderReportComponent } from './gender-report/gender-report.component';
import { TreatmentReportComponent } from './treatment-report/treatment-report.component';
import { CoMorbiditiesReportComponent } from './co-morbidities-report/co-morbidities-report.component';
import { ChiefComplaintComponent2 } from './followUpOne/chief-complaint/chief-complaint.component';
import { ComorbiditiesComponent2 } from './followUpOne/comorbidities/comorbidities.component';
import { AssessmentComponent2 } from './followUpOne/assessment/assessment.component';
import { ManagamentComponent2 } from './followUpOne/management/management.component';
import { ChiefComplaint3Component } from './followUpTwo/chief-complaint3/chief-complaint3.component';
import { Assessment3Component } from './followUpTwo/assessment3/assessment3.component';
import { Comorbidities3Component } from './followUpTwo/comorbidities3/comorbidities3.component';
import { Managament3Component } from './followUpTwo/management3/management3.component';
import { CaseStageViewComponent } from './case-stage-view/case-stage-view.component';
import { DoctorListComponent } from './doctor-list/doctor-list.component';


const routes: Routes = [
  { path: '', component: HomeComponent },
  { path: 'login', component: LoginFormComponent },
  { path: 'register', component: DoctorsRegistrationComponent },
  { path: 'dashboard', component: DashboardComponent },
  // { path: 'contact', component: ContactComponent },
  { path: 'demographic', component: DemographicComponent },
  { path: 'follow-up', component: FollowUpComponent },
  { path: 'case-details/:id/:stage', component: CaseDetailsComponent },
  { path: 'history', component: HistoryComponent },
  { path: 'Personal-history', component: PersonalHistoryComponent },
  { path: 'Personal-history/:patientId/:stage', component: PersonalHistoryComponent },
  { path: 'sleep', component: SleepComponent },
  { path: 'gadget', component: GadgetComponent },
  { path: 'chiefComplaint', component: ChiefComplaintComponent },
  { path: 'comorbidities', component: ComorbiditiesComponent },
  { path: 'family-history', component: FamilyHistoryComponent },
  { path: 'history-endoscopy', component: HistoryEndoscopyComponent },
  { path: 'current-medicaton', component: CurrentMedicationsComponent },
  // { path: 'medical-examination', component: MedicalExaminationComponent },

  { path: 'medical-examination/:patientId/:stage', component: MedicalExaminationComponent },

  { path: 'diagnosis', component: DiagnosisComponent },
  { path: 'managament', component: ManagamentComponent },
  { path: 'assessment', component: AssessmentComponent },
  { path: 'gerdregistry', component: AboutGerdRegistryComponent },
  { path: 'guideline', component: GuidelineComponent },
  { path: 'contact-us', component: ContactUsComponent },
  { path: 'admindashboard', component: AdminDashboardComponent },
  { path: 'genderReport', component: GenderReportComponent },
  { path: 'treatmentReport', component: TreatmentReportComponent },
  { path: 'CoMorbiditiesReport', component: CoMorbiditiesReportComponent },
  { path: 'chiefComplaint/:patientId/:stage', component: ChiefComplaintComponent },
  { path: 'comorbidities/:patientId/:stage', component: ComorbiditiesComponent },
  { path: 'assessment/:patientId/:stage', component: AssessmentComponent },
  { path: 'history/:patientId/:stage', component: HistoryComponent },
  { path: 'history-endoscopy/:patientId/:stage', component: HistoryEndoscopyComponent },
  { path: 'sleep/:patientId/:stage', component: SleepComponent },
  { path: 'gadget/:patientId/:stage', component: GadgetComponent },
  { path: 'family-history/:patientId/:stage', component: FamilyHistoryComponent },
  { path: 'follow-up-1/current-medicaton', component: CurrentMedicationsComponent },

  { path: 'assessment/:patientId/:stage', component: AssessmentComponent },
  { path: 'diagnosis/:patientId/:stage', component: DiagnosisComponent },
  { path: 'managament/:patientId/:stage', component: ManagamentComponent },
  { path: 'chiefComplaint/:patientId/:stage', component: ChiefComplaintComponent2 },
  { path: 'followUpOne/comorbidities/:patientId', component: ComorbiditiesComponent2 },
  { path: 'followUpOne/assessment/:patientId', component: AssessmentComponent2 },
  { path: 'followUpOne/management/:patientId', component: ManagamentComponent2 },
  {
    path: 'case-stage-view/:patientId', component: CaseStageViewComponent
  },
  { path: 'doctor-list', component: DoctorListComponent },




  { path: 'chiefComplaint/:patientId/:stage', component: ChiefComplaintComponent },
  { path: 'follow-up-2/comorbidities', component: ComorbiditiesComponent },
  { path: 'follow-up-2/assessment', component: AssessmentComponent },
  { path: 'follow-up-2/managament', component: ManagamentComponent },
  { path: 'followUpTwo/chiefComplaint/:patientId', component: ChiefComplaint3Component },
  { path: 'followUpTwo/comorbidities/:patientId', component: Comorbidities3Component },
  { path: 'followUpTwo/assessment/:patientId', component: Assessment3Component },
  { path: 'followUpTwo/management/:patientId', component: Managament3Component },
];
@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
