// import { Component } from '@angular/core';
// import { FormBuilder, FormGroup, Validators } from '@angular/forms';
// import { ActivatedRoute, Router } from '@angular/router';
// import { FormvalidationService } from '../formvalidation.service';
// import { HttpserviceService } from '../httpservice.service';

// @Component({
//   selector: 'app-diagnosis',
//   templateUrl: './diagnosis.component.html',
//   styleUrls: ['./diagnosis.component.css']
// })
// export class DiagnosisComponent {
//   diagnosisForm!: FormGroup;

//   constructor(
//     private fb: FormBuilder,
//     private formValidation: FormvalidationService,
//     private http: HttpserviceService,
//     private router: Router,
//     public route: ActivatedRoute
//   ) {
//     this.diagnosisForm = this.fb.group({
//       newlyDiagnosed: ['', Validators.required],
//       knownCase: ['', Validators.required],
//       yearsKnown: ['', Validators.required],
//       gerdType: ['', Validators.required],
//       refractory: ['', Validators.required],
//       adherence: ['', Validators.required]
//     });
//   }

//   ngOnInit(): void {}

//   onSubmit(): void {
//     if (this.diagnosisForm.valid) {
//       this.Submit(); // ✅ Call actual save function
//     } else {
//       console.warn('Form is invalid');
//       this.diagnosisForm.markAllAsTouched();
//     }
//   }

//   Submit(): void {
//     if (!this.formValidation.validateForm(this.diagnosisForm)) {
//       this.diagnosisForm.markAllAsTouched();
//       return;
//     }

//     const param = {
//       flag: 'I',
//       diagnosisID: 0,
//       patientID: 0,
//       doctorID: 0,
//       newlyDiagnosed: this.diagnosisForm.controls['newlyDiagnosed'].value === 'Yes',
//       knownCaseOfGERD: this.diagnosisForm.controls['knownCase'].value === 'Yes',
//       greD_NoOfYear: Number(this.diagnosisForm.controls['yearsKnown'].value) || 0,
//       gerdType: this.diagnosisForm.controls['gerdType'].value,
//       refractoryToPPI: this.diagnosisForm.controls['refractory'].value === 'Yes',
//       adherenceToTherapy: this.diagnosisForm.controls['adherence'].value === 'Yes',
//       stage: 0,
//       createdBy: 0
//     };

//     this.http.httpPost('/Diagnosis/SaveDiagnosis', param).subscribe((res: any) => {
//       if (res.type === 'S') {
//         this.formValidation.showAlert('Successfully Submitted', 'success');
//         alert('Successfully Submitted');
//         this.diagnosisForm.reset();
//         // this.router.navigate(['/managament']); // Optional
//       } else {
//         this.formValidation.showAlert('Error!!', 'danger');
//       }
//     });
//   }
// }


import { Component, Input, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { FormvalidationService } from '../formvalidation.service';
import { HttpserviceService } from '../httpservice.service';
import { PatientService } from '../Services/patient.service';
import { HttpClient } from '@angular/common/http';
import { API_URLS } from '../shared/API-URLs';
import { DiagnosisService } from '../Services/diagnosis.service';

@Component({
  selector: 'app-diagnosis',
  templateUrl: './diagnosis.component.html',
  styleUrls: ['./diagnosis.component.css']
})
export class DiagnosisComponent implements OnInit {
  diagnosisForm!: FormGroup;
  @Input() patientId: number | null = null;
  doctorId: number | null = null;
  isViewMode = false;
  isFollowUp: boolean = false;
  tabId = 1;
  @Input() stage = 0;
  isSaved: boolean = false;
  formData: any;
  @Input() isPrintMode = false;


  constructor(
    private fb: FormBuilder,
    private formValidation: FormvalidationService,
    private http: HttpserviceService,
    private router: Router,
    public route: ActivatedRoute,
    private httpClient: HttpClient,
    private patientService: PatientService,
    private diagnosisService: DiagnosisService
  ) {
    this.diagnosisForm = this.fb.group({
      newlyDiagnosed: [null, Validators.required],
      knownCase: [null, Validators.required],
      yearsKnown: ['', Validators.required],
      gerdType: ['', Validators.required],
      refractory: [null, Validators.required],
      adherence: [null, Validators.required]
    });
  }
  ngOnInit(): void {
    this.stage = Number(this.route.snapshot.params['stage']);
    console.log("Tabid", this.tabId);
    const state = history.state;
  const allowedWithoutSave = [1, 3, 5];
  if (allowedWithoutSave.includes(this.stage)) {
    this.isSaved = true;
  }
    this.tabId = state?.tabId ?? 1;
    this.isViewMode = state?.isViewMode ?? false;
    this.formData = state?.data;
   

    this.patientId = this.patientId || this.patientService.getPatientId();
    if (!this.patientId) {
      console.warn('⚠️ No valid patient ID found.');
      return;
    }

    this.route.params.subscribe(params => {
      const idFromRoute = +params['patientId'];
      if (idFromRoute && idFromRoute !== this.patientId) {
        this.patientId = idFromRoute;
        this.patientService.setPatientId(this.patientId);
      }
      //this.showDiag();
      const cachedData = this.patientService.getDiagnosisData();
      if (cachedData) {
        this.diagnosisForm.patchValue(cachedData);
      } else {
        this.fetchAndStorePatientId(this.patientService.getPatientId());
      }
    });

    this.diagnosisForm.get('knownCase')?.valueChanges.subscribe((value: string) => {
      const yearsControl = this.diagnosisForm.get('yearsKnown');
      if (value === 'Yes') {
        yearsControl?.enable();
      } else {
        yearsControl?.disable();
        yearsControl?.setValue('');
      }
    });

    // Also apply it immediately based on the current value
    const knownCaseInitial = this.diagnosisForm.get('knownCase')?.value;
    if (knownCaseInitial !== 'Yes') {
      this.diagnosisForm.get('yearsKnown')?.disable();
    }

  }
  
  fetchAndStorePatientId(patientId: number): void {
    this.diagnosisService.getdiagnosisId(patientId).subscribe({
      next: (res: any) => {
        if (res.type === 'S' && res.data) {
          const data = res.data;
          console.log('✅ Chief Complaint data:', data);
          this.stage = data.stage;
          this.diagnosisForm.patchValue({
            newlyDiagnosed: data.newlyDiagnosed ? 'Yes' : 'No',
            knownCase: data.knownCaseOfGerd ? 'Yes' : 'No',
            yearsKnown: data.gredNoOfYear,
            gerdType: data.gerdtype,
            refractory: data.refractoryToPpi ? 'Yes' : 'No',
            adherence: data.adherenceToTherapy ? 'Yes' : 'No'
          });

          this.stage = data.stage;




          // fetchAndStorePatientId(): void {
          //   this.httpClient.get<any[]>('/PatientReg/GetPatient').subscribe({
          //     next: (res) => {
          //       const patient = res[0]; 
          //       if (patient?.patientId) {
          //         this.patientService.setPatientId(patient.patientId);
          //         console.log('Patient ID stored:', patient.patientId);
          //       }
          //     },
          //     error: (err) => {
          //       console.error('Failed to fetch patient data:', err);
          //     }
          //   });
          // }
          // fetchAndStorePatientId(): void {
          //   const existingId = this.patientService.getPatientId();
          //   if (existingId && existingId !== 0) {
          //     console.log('✅ Patient ID already stored:', existingId);
          //     this.patientId = existingId;
          //     return;
          //   }

          //   this.patientService.getAllPatientData().subscribe({
          //     next: (res) => {
          //       const patient = res[0]; // or however you determine the active patient
          //       if (patient?.patientID) {
          //         this.patientService.setPatientId(patient.patientID);
          //         this.patientId = patient.patientID;
          //         console.log('✅ Patient ID fetched & stored:', patient.patientID);
          //       } else {
          //         console.warn('⚠️ No patientID found in response');
          //       }
          //     },
          //     error: (err) => {
          //       console.error('❌ Failed to fetch patient data:', err);
          //     }
          //   });
          // }

        } else {
          console.warn('⚠️ No chief complaint data found in response.');
        }
      },
      error: (err) => {
        console.error('❌ Error fetching chief complaint data:', err);
      }
    });
  }

  // showDiag(){
   
  //   if (this.diagnosisForm.controls['NewlyDig'].value === 'yes'){
  //     this.diagnosisForm.controls['knownCase'].disable;
  //     this.diagnosisForm.controls['yearsKnown'].disable;
  //   }
  //   else{
  //     this.diagnosisForm.controls['knownCase'].enabled;
  //     this.diagnosisForm.controls['yearsKnown'].enabled;
  //   }

  // }
  

  showDiag() {
  const newlyDiagnosed = this.diagnosisForm.get('newlyDiagnosed')?.value;

  if (newlyDiagnosed === 'No') {
    this.diagnosisForm.get('knownCase')?.disable();
    this.diagnosisForm.get('yearsKnown')?.disable();
  } else {
    this.diagnosisForm.get('knownCase')?.enable();
    this.diagnosisForm.get('yearsKnown')?.enable();
  }
}

  onSubmit(): void {
    if (this.diagnosisForm.valid) {
      this.Submit();
    } else {
      console.warn('Form is invalid');
      this.diagnosisForm.markAllAsTouched();
    }
  }

  Submit(): void {
    if (!this.formValidation.validateForm(this.diagnosisForm)) {
      this.diagnosisForm.markAllAsTouched();
      return;
    }

    const patientID = this.patientService.getPatientId();
    const doctorID = this.patientService.getDoctorId();

    const param = {
      flag: 'I',
      diagnosisID: 0,
      patientID: this.patientService.getPatientId(),
      doctorID: doctorID || 0,
      newlyDiagnosed: this.diagnosisForm.controls['newlyDiagnosed'].value === 'Yes',
      knownCaseOfGERD: this.diagnosisForm.controls['knownCase'].value === 'Yes',
      greD_NoOfYear: Number(this.diagnosisForm.controls['yearsKnown'].value) || 0,
      gerdType: this.diagnosisForm.controls['gerdType'].value,
      refractoryToPPI: this.diagnosisForm.controls['refractory'].value === 'Yes',
      adherenceToTherapy: this.diagnosisForm.controls['adherence'].value === 'Yes',
      stage: 0,
      createdBy: 0
    };

    this.http.httpPost(API_URLS.DIAGNOSIS_SAVE, param).subscribe((res: any) => {
      if (res.type === 'S') {
        alert('Saved Successfully'); // ← Test this
       // this.formValidation.showAlert('Saved Successfully', 'success');
        this.isSaved = true;


      } else {
        this.formValidation.showAlert('Error!!', 'danger');
      }
    });
  }

  onNext() {
    const currentUrl = this.router.url;
    const patientId = this.patientId;

    if (currentUrl.includes('follow-up-1')) {
      this.router.navigate([`/managament/${this.patientId}/${this.stage}`], {
        state: {
          tabId: this.tabId,
          patientId: this.patientId,
          isViewMode: this.isViewMode,
          stage: this.stage
        }
      });
    } else {
      // Optional: route to next section or back to dashboard
      this.router.navigate([`/managament/${this.patientId}/${this.stage}`], {
        state: {
          tabId: this.tabId,
          patientId: this.patientId,
          isViewMode: this.isViewMode
        }
      });
    }
  }

  OnNext() {
    this.router.navigate([`/managament/${this.patientId}/${this.stage}`], {
      state: {
        tabId: this.tabId,
        patientId: this.patientId,
        isViewMode: this.isViewMode
      }
    });
  }
  goback() {
    this.router.navigate([`/assessment/${this.patientId}/${this.stage}`], {
      state: {
        tabId: this.tabId,
        patientId: this.patientId,
        isViewMode: this.isViewMode,
        fromNavigation: true
      }
    });

  }
  back() {
    
    this.router.navigate([`/assessment/${this.patientId}/${this.stage}`], {
      state: {
        tabId: this.tabId,  
        patientId: this.patientId,
        isViewMode: this.isViewMode,
     fromNavigation: true
      }
    });
  }
  getStatusClass(step: number): string {
    if (this.stage === 0 && step === 1) return 'baseline-blue';

    if (this.stage >= 1 && step === 1) return 'baseline-green';
    if (this.stage >= 1 && this.stage < 3 && step === 2) return 'baseline-blue';

    if (this.stage >= 3 && step === 2) return 'baseline-green';
    if (this.stage >= 3 && this.stage < 5 && step === 3) return 'baseline-blue';

    if (this.stage === 5 && step === 3) return 'baseline-green';

    return 'inactive-tab';
  }
}

