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
  ageInYears: number = 0;

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
    this.patientId = Number(this.route.snapshot.params['patientId']) || null;
    this.stage = Number(this.route.snapshot.params['stage']) || 0;

    const allowedWithoutSave = [1, 3, 5];
    if (allowedWithoutSave.includes(this.stage)) {
      this.isSaved = true;
    }

    // Age is stored in localStorage from Demographic component
    const storedAge = localStorage.getItem('Age');
    if (storedAge) {
      const age = JSON.parse(storedAge).age; // age in years
      this.ageInYears = age;            // age in years
      console.log('Age in months:', this.ageInYears);
    }

    this.isViewMode = this.isViewMode ?? false;

    if (this.patientId) {
      this.fetchAndStorePatientId(this.patientId);
    }

    this.diagnosisForm.get('newlyDiagnosed')?.valueChanges.subscribe(value => {
      const knownCase = this.diagnosisForm.get('knownCase');
      const years = this.diagnosisForm.get('yearsKnown');

      if (value === 'No') {
        knownCase?.enable();
        knownCase?.setValue('Yes');
        knownCase?.disable();
      } else {
        knownCase?.disable();
        knownCase?.setValue('');
        years?.disable();
        years?.setValue('');
      }
    });

    this.diagnosisForm.get('knownCase')?.valueChanges.subscribe(value => {
      const years = this.diagnosisForm.get('yearsKnown');
      if (value === 'Yes' && this.diagnosisForm.get('newlyDiagnosed')?.value === 'No') {
        years?.enable();
      } else {
        years?.disable();
        years?.setValue('');
      }
    });

    const newlyDiagnosedInitial = this.diagnosisForm.get('newlyDiagnosed')?.value;
    const knownCaseInitial = this.diagnosisForm.get('knownCase')?.value;

    if (newlyDiagnosedInitial === 'No') {
      this.diagnosisForm.get('knownCase')?.enable();
    } else {
      this.diagnosisForm.get('knownCase')?.disable();
      this.diagnosisForm.get('knownCase')?.setValue('');
    }

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
    const knownCaseControl = this.diagnosisForm.get('knownCase');
    const yearsControl = this.diagnosisForm.get('yearsKnown');

    if (newlyDiagnosed === 'Yes') {
      // Disable both if newly diagnosed = Yes
      knownCaseControl?.setValue('');
      knownCaseControl?.disable();
      yearsControl?.setValue('');
      yearsControl?.disable();
    }
    else if (newlyDiagnosed === 'No') {
      // Enable knownCase when No
      knownCaseControl?.enable();

      // yearsKnown depends on knownCase value
      if (knownCaseControl?.value === 'Yes') {
        yearsControl?.enable();
      } else {
        yearsControl?.disable();
        yearsControl?.setValue('');
      }
    }
    else {
      // Default state
      knownCaseControl?.disable();
      knownCaseControl?.setValue('');
      yearsControl?.disable();
      yearsControl?.setValue('');
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
  validateFields(): boolean {
    const form = this.diagnosisForm;

    // Newly Diagnosed
    if (!form.get('newlyDiagnosed')?.value) {
      alert('Please select Newly Diagnosed');
      return false;
    }

    // Known Case (only required if Newly Diagnosed = No)
    if (form.get('newlyDiagnosed')?.value === 'No' && !form.get('knownCase')?.value) {
      alert('Please select Known Case of GERD');
      return false;
    }

    // Years Known (only required if Known Case = Yes and Newly Diagnosed = No)
    if (
      form.get('newlyDiagnosed')?.value === 'No' &&
      form.get('knownCase')?.value === 'Yes' &&
      !form.get('yearsKnown')?.value
    ) {
      alert('Please enter number of years Known');
      return false;
    }

    // GERD Type
    if (!form.get('gerdType')?.value) {
      alert('Please select GERD Type');
      return false;
    }

    // Refractory
    if (!form.get('refractory')?.value) {
      alert('Please select Refractory to PPI');
      return false;
    }

    // Adherence
    if (!form.get('adherence')?.value) {
      alert('Please select Adherence to Therapy');
      return false;
    }

    return true; // All validations passed
  }

  Submit(): void {

    if (!this.validateFields()) {
      return;
    }

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

    const enteredgreD_NoOfYear = Number(param.greD_NoOfYear);


    if (enteredgreD_NoOfYear > this.ageInYears) {

      alert('Entered number of Year exceeds the person’s age (' + this.ageInYears + ' years). Please enter valid values.');
      return;
    }

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

    this.router.navigate([`/managament/${this.patientId}/${this.stage}`], {
      state: {
        tabId: this.tabId,
        patientId: this.patientId,
        isViewMode: this.isViewMode,
        fromNavigation: true

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

