import { NgModule } from '@angular/core';
import { BrowserModule, provideClientHydration } from '@angular/platform-browser';
import { HttpClientModule } from '@angular/common/http';
import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { HeaderComponent } from './header/header.component';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { HomeComponent } from './home/home.component';
import { FooterComponent } from './footer/footer.component';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { ContactComponent } from './contact/contact.component';
import { DoctorsRegistrationComponent } from './doctors-registration/doctors-registration.component';
import { LoginFormComponent } from './login-form/login-form.component';
import { DashboardComponent } from './dashboard/dashboard.component';
import { NavbarComponent } from './components/navbar/navbar.component';
import { DemographicComponent } from './demographic/demographic.component';
import { NgChartsModule } from 'ng2-charts';
import { FollowUpComponent } from './follow-up/follow-up.component';
import { CaseDetailsComponent } from './case-details/case-details.component';
import { HistoryComponent } from './history/history.component';
import { ChiefComplaintComponent } from './chief-complaint/chief-complaint.component';
import { ComorbiditiesComponent } from './comorbidities/comorbidities.component';
import { PersonalHistoryComponent } from './personal-history/personal-history.component';
import { MedicalExaminationComponent } from './medical-examination/medical-examination.component';
import { SleepComponent } from './sleep/sleep.component';
import { FamilyHistoryComponent } from './family-history/family-history.component';
import { HistoryEndoscopyComponent } from './history-endoscopy/history-endoscopy.component';
import { DiagnosisComponent } from './diagnosis/diagnosis.component';
import { ManagamentComponent } from './managament/managament.component';
import { AssessmentComponent } from './assessment/assessment.component';
import { GadgetComponent } from './gadget/gadget.component';
import { SidebarComponent } from './sidebar/sidebar.component';
import { AboutGerdRegistryComponent } from './about-gerd-registry/about-gerd-registry.component';
import { GuidelineComponent } from './guideline/guideline.component';
import { ContactUsComponent } from './contact-us/contact-us.component';
import { AdminDashboardComponent } from './admin-dashboard/admin-dashboard.component';
import { GenderReportComponent } from './gender-report/gender-report.component';
import { TreatmentReportComponent } from './treatment-report/treatment-report.component';
import { CoMorbiditiesReportComponent } from './co-morbidities-report/co-morbidities-report.component';
// import { FontAwesomeModule } from '@fortawesome/angular-fontawesome';
import { CurrentMedicationsComponent } from './current-medications/current-medications.component';
import { ManagamentComponent2 } from './followUpOne/management/management.component';
import { ChiefComplaintComponent2 } from './followUpOne/chief-complaint/chief-complaint.component';
import { ComorbiditiesComponent2 } from './followUpOne/comorbidities/comorbidities.component';
import { AssessmentComponent2 } from './followUpOne/assessment/assessment.component';
import { Assessment3Component } from './followUpTwo/assessment3/assessment3.component';
import { ChiefComplaint3Component } from './followUpTwo/chief-complaint3/chief-complaint3.component';
import { Comorbidities3Component } from './followUpTwo/comorbidities3/comorbidities3.component';
import { Managament3Component } from './followUpTwo/management3/management3.component';
import { CaseStageViewComponent } from './case-stage-view/case-stage-view.component';
import { DoctorListComponent } from './doctor-list/doctor-list.component';


@NgModule({
  declarations: [
    AppComponent,
    HeaderComponent,
    HomeComponent,
    FooterComponent,
    ContactComponent,
    DoctorsRegistrationComponent,
    LoginFormComponent,
    DashboardComponent,
    NavbarComponent,
    DemographicComponent,
    FollowUpComponent,
    CaseDetailsComponent,
    HistoryComponent,
    FamilyHistoryComponent,
    ChiefComplaintComponent,
    ComorbiditiesComponent,
    PersonalHistoryComponent,
    MedicalExaminationComponent,
    SleepComponent,
    HistoryEndoscopyComponent,
    DiagnosisComponent,
    ManagamentComponent,
    AssessmentComponent,
    GadgetComponent,
    SidebarComponent,
    AboutGerdRegistryComponent,
    GuidelineComponent,
    ContactUsComponent,
    CurrentMedicationsComponent,
    ContactUsComponent,
    AdminDashboardComponent,
    GenderReportComponent,
    TreatmentReportComponent,
    CoMorbiditiesReportComponent,
   // ManagamentComponent2,
    // ChiefComplaintComponent2,
  //  ComorbiditiesComponent2,
   // AssessmentComponent2,
   // Assessment3Component,
   // Managament3Component,
    // ChiefComplaint3Component,
   // Comorbidities3Component,
    CaseStageViewComponent,
   DoctorListComponent,
   
  ],

  imports: [
    BrowserModule,
    AppRoutingModule,
    BrowserAnimationsModule,
    FormsModule,
    ReactiveFormsModule,
    HttpClientModule,
    NgChartsModule,
    FormsModule
    
  ],

  providers: [
    provideClientHydration()
  ],

  bootstrap: [AppComponent]
})


export class AppModule { }
