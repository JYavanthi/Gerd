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
  // doctorId: number | null = null;
  thdoctorId = this.patientService.getPatientId()
  isViewMode = false;
  isFollowUp: boolean = false;
  tabId = 1;
  @Input() stage = 0;
  isSaved: boolean = false;
  formData: any;
  @Input() isPrintMode = false;
  userData: any;


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
    this.patientId = Number(this.route.snapshot.params['patientId'])||null;
    this.stage = Number(this.route.snapshot.params['stage'])||0;
    
    
    const allowedWithoutSave = [1, 3, 5];
    if (allowedWithoutSave.includes(this.stage)) {
      this.isSaved = true;
    }
     
  this.isViewMode = this.isViewMode ?? false;


   if (this.patientId) {
    this.fetchAndStorePatientId(this.patientId);
  }
    this.showDiag();
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
         
          //this.stage = data.stage;
          this.diagnosisForm.patchValue({
            patientID: data.patientId,
            newlyDiagnosed: data.newlyDiagnosed ? 'Yes' : 'No',
            knownCase: data.knownCaseOfGerd ? 'Yes' : 'No',
            yearsKnown: data.gredNoOfYear,
            gerdType: data.gerdtype,
            refractory: data.refractoryToPpi ? 'Yes' : 'No',
            adherence: data.adherenceToTherapy ? 'Yes' : 'No'
          });

        


        } else {
          console.warn('⚠️ No Diagonsis data found in response.');
        }
      },
      error: (err) => {
        console.error('❌ Error fetching Diagonsis data:', err);
      }
    });
  }


   showDiag() {
    const newlyDiagnosed = this.diagnosisForm.get('newlyDiagnosed')?.value;

    if (newlyDiagnosed === 'No') {
      this.diagnosisForm.get('knownCase')?.setValue('');
      this.diagnosisForm.get('yearsKnown')?.setValue('');
      this.diagnosisForm.get('knownCase')?.disable();
      this.diagnosisForm.get('yearsKnown')?.disable();
    } else if (newlyDiagnosed === 'Yes') {
      this.diagnosisForm.get('knownCase')?.enable();
      this.diagnosisForm.get('yearsKnown')?.enable();
    }else{
     
      this.diagnosisForm.get('knownCase')?.disable();
      this.diagnosisForm.get('yearsKnown')?.disable();
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

    // const patientID = this.patientService.getPatientId();
    const doctorID = this.patientService.getDoctorId();
   // let user: any = localStorage.getItem('doctor')
   // this.userData = JSON.parse(user);

    const param = {
      flag: 'I',
      diagnosisID: 0,
      patientID: this.patientId,
      doctorID: doctorID,
      newlyDiagnosed: this.diagnosisForm.controls['newlyDiagnosed'].value === 'Yes',
      knownCaseOfGERD: this.diagnosisForm.controls['knownCase'].value === 'Yes',
      greD_NoOfYear: Number(this.diagnosisForm.controls['yearsKnown'].value) || 0,
      gerdType: this.diagnosisForm.controls['gerdType'].value,
      refractoryToPPI: this.diagnosisForm.controls['refractory'].value === 'Yes',
      adherenceToTherapy: this.diagnosisForm.controls['adherence'].value === 'Yes',
      stage: this.stage,
      createdBy: doctorID,
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


    // Optional: route to next section or back to dashboard
    this.router.navigate([`/managament/${this.patientId}/${this.stage}`], {
      state: {
        tabId: this.tabId,
        patientId: this.patientId,
        isViewMode: this.isViewMode
      }
    });

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

