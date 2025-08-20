import { Component, Input, OnInit } from '@angular/core';
import { ActivatedRoute, Router, } from '@angular/router';
import { HttpErrorResponse } from '@angular/common/http';
import { HttpserviceService } from '../httpservice.service';
import { FormvalidationService } from '../formvalidation.service';
import { HistoryService } from '../Services/history.servie';
import { PatientHistoryService } from '../Services/patient-history.service';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { API_URLS } from '../shared/API-URLs';
import { PatientService } from '../Services/patient.service';
import { gadgetService } from '../Services/gadget.service';

@Component({
  selector: 'app-gadget',
  templateUrl: './gadget.component.html',
  styleUrls: ['./gadget.component.css']
})


export class GadgetComponent implements OnInit {
  tabId = 1;
  @Input() stage: number = 0;
  gadgetForm!: FormGroup

  isSaved: boolean = false;
@Input() isPrintMode = false;

  @Input() patientId: number | null = null;
  doctorId: number | null = null;
  isViewMode = false;
  isFollowUp: boolean = false;
  id: any;
  userData:any;
  gadgetUsage = {
    computers: {
      used: '',
      frequency: '',
      duration: ''
    },
    smartphones: {
      used: '',
      frequency: '',
      duration: ''
    },
    workingHours: '',
    jobType: '',
    duration: ''
  };

  constructor(
    private router: Router,
    private formValidation: FormvalidationService,
    private http: HttpserviceService,
    private route: ActivatedRoute,
    private historyService: HistoryService,
    private patientHistoryService: PatientHistoryService,
    private fb: FormBuilder, private patientService: PatientService,
    private gadgetService: gadgetService

  ) {

    this.gadgetForm = this.fb.group({
      flag: ['I'],
      id: [0],
      patientId: [this.patientService.getPatientId()],
      computerUsed: [false],
      computerUsedhrs: [''],
      computerUsedyears: [''],
      computerFrequency: ['' ],
      smartphoneUsedhrs: [''],
      smartphoneUsedyears: ['' ],
      computerDurationYears: ['' ],
      smartphoneUsed: [false],
      smartphoneFrequency: [''],
      smartphoneDurationYears: [''],
      workingHours: [''],

      jobType: [''],
      totalWorkingYears: [''],
      createdBy: ['']
    });

  }


ngOnInit(): void {
   this.stage = Number(this.route.snapshot.params['stage']|| 0);
   this.patientId =Number(this.route.snapshot.params['patientId']);
   
  const currentUrl = this.router.url;
  console.log("this.stage:", this.stage );
 const allowedWithoutSave = [1, 3, 5];
  if (allowedWithoutSave.includes(this.stage)) {
    this.isSaved = true;
  }
  this.gadgetForm = this.fb.group({
    computerUsed: ['',],
    computerUsedhrs: [{ value: '', disabled: true }],
    computerUsedyears: [{ value: '', disabled: true }],
    smartphoneUsed: ['',],
    smartphoneUsedhrs: [{ value: '', disabled: true }],
    smartphoneUsedyears: [{ value: '', disabled: true }],
    patientId: [null],
    computerFrequency: [''],
    computerDurationYears: [''],
    smartphoneFrequency: [''],
    smartphoneDurationYears: [''],
    workingHours: [''],
    jobType: [''],
    totalWorkingYears: [''],
    createdBy: ['']
  });

  // ✅ Read navigation state (no queryParams)
  const state = history.state;
  //this.patientId = state?.patientId ?? null;
  this.tabId = state?.tabId ?? 1;
  //this.stage = state?.stage ?? 0;
  this.isViewMode = state?.isViewMode ?? false;

  // ✅ Handle follow-up route detection
  this.isFollowUp = currentUrl.includes('follow-up-1') || currentUrl.includes('follow-up-2');

  // ✅ Auto fetch gadget data
  if ((currentUrl === '/follow-up-1/gadget' || currentUrl === '/gadget') && this.patientService.getPatientId()) {
    this.fetchGadgetData(this.patientService.getPatientId());
  }

  // ✅ Conditional field enable/disable
  this.gadgetForm.get('computerUsed')?.valueChanges.subscribe(value => {
    if (value === true || value === 'true') {
      this.gadgetForm.get('computerUsedhrs')?.enable();
      this.gadgetForm.get('computerUsedyears')?.enable();
    } else {
      this.gadgetForm.get('computerUsedhrs')?.disable();
      this.gadgetForm.get('computerUsedyears')?.disable();
      this.gadgetForm.patchValue({ computerUsedhrs: '', computerUsedyears: '' });
    }
  });

  this.gadgetForm.get('smartphoneUsed')?.valueChanges.subscribe(value => {
    if (value === true || value === 'true') {
      this.gadgetForm.get('smartphoneUsedhrs')?.enable();
      this.gadgetForm.get('smartphoneUsedyears')?.enable();
    } else {
      this.gadgetForm.get('smartphoneUsedhrs')?.disable();
      this.gadgetForm.get('smartphoneUsedyears')?.disable();
      this.gadgetForm.patchValue({ smartphoneUsedhrs: '', smartphoneUsedyears: '' });
    }
  });

  // ✅ Load from local cache or fetch again
  const cachedData = this.patientService.getfamalyhistoryData();
  if (cachedData) {
    this.gadgetForm.patchValue(cachedData);
  } else {
    this.fetchGadgetData(this.patientService.getPatientId());
  }

  // ✅ Load history data if patient history exists
  const patientHistoryId = this.historyService.getPatientHistoryID();
  if (patientHistoryId && patientHistoryId > 0) {
    this.loadExistingData(patientHistoryId);
  }
}


  private loadExistingData(id: number): void {
    this.http.httpGet(`/PatientHistory/GetPatientHistory?patientHistoryID=${id}`).subscribe({
      next: (res: any) => {
        this.gadgetUsage.computers.used = res.g_Usage || '';
        this.gadgetUsage.computers.frequency = res.g_Frequency || '';
        this.gadgetUsage.computers.duration = res.g_YearOfUsage || '';

        this.gadgetUsage.smartphones.used = res.smartphone_Usage || '';
        this.gadgetUsage.smartphones.frequency = res.smartphone_Frequency || '';
        this.gadgetUsage.smartphones.duration = res.smartphone_Duration || '';

        this.gadgetUsage.workingHours = res.workingHours || '';
        this.gadgetUsage.jobType = res.jobType || '';
        this.gadgetUsage.duration = res.duration || '';
      },
      error: () => {
        this.formValidation.showAlert('Failed to load gadget data.', 'danger');
      }
    });
  }

  fetchGadgetData(patientId: number): void {
    this.gadgetService.GetGadgetById(patientId ,this.stage).subscribe({
      next: (res: any) => {
        if (res.type === 'S' && res.data) {
          const data = res.data;
          if(data){
            this.isSaved = true
          }
          this.gadgetForm.patchValue({
            id: data.id ?? 0,
            gadget: data.gadget ?? '',
            computerUsed: data.computerUsed,
            computerUsedhrs: data.computerFrequency ?? '',
            computerUsedyears: data.computerDurationYears ?? '',
            smartphoneUsed: data.smartphoneUsed,
            smartphoneUsedhrs: data.smartphoneFrequency ?? '',
            smartphoneUsedyears: data. smartphoneDurationYears ?? '',
            workingHours: data.workingHours ?? '',
            jobType: data.jobType ?? '',
            totalWorkingYears: data.totalWorkingYears ?? '',
            createdBy: data.createdBy
          });

          this.toggleConditionalControls('computerUsed', data.computerUsed);
          this.toggleConditionalControls('smartphoneUsed', data.smartphoneUsed);

          if (this.isViewMode) {
            this.gadgetForm.disable();
          }

        } else {
          console.warn('⚠️ No gadget data found in response.');
        }
      },
      error: (err: any) => {
        console.error('❌ Error fetching gadget data:', err);
      }
    });
  }


  toggleConditionalControls(controlName: string, value: boolean | string): void {
    const isUsed = value === true || value === 'true';
    const hrsControl = this.gadgetForm.get(`${controlName}hrs`);
    const yearsControl = this.gadgetForm.get(`${controlName}years`);

    
  }


Submit(): void {
  if (!this.gadgetForm.valid) {
    this.gadgetForm.markAllAsTouched();

    // ✅ Loop over controls and show alert for the first invalid one
    for (const controlName in this.gadgetForm.controls) {
      const control = this.gadgetForm.get(controlName);
      if (control && control.invalid) {
        alert(`Field "${this.getFieldLabel(controlName)}" is required.`);
        break;
      }
    }
    return;
  }

  let user: any = localStorage.getItem('doctor');
  this.userData = JSON.parse(user);
  const gadgetFormValues = this.gadgetForm.getRawValue();

  const gadget = {
    flag: 'I',
    id: this.gadgetForm.get('id')?.value,
    patientId: this.patientId,
    gadget: this.gadgetForm.get('gadget')?.value ?? '',
    stage: this.stage,
    computerUsed: this.gadgetForm.get('computerUsed')?.value,
    computerFrequency: this.gadgetForm.get('computerUsedhrs')?.value,
    computerDurationYears: this.gadgetForm.get('computerUsedyears')?.value,

    smartphoneUsed: !!this.gadgetForm.get('smartphoneUsed')?.value,
    smartphoneFrequency: this.gadgetForm.get('smartphoneUsedhrs')?.value,
    smartphoneDurationYears: this.gadgetForm.get('smartphoneUsedyears')?.value,

    workingHours: this.gadgetForm.get('workingHours')?.value ?? '',
    jobType: this.gadgetForm.get('jobType')?.value ?? '',
    totalWorkingYears: this.gadgetForm.get('totalWorkingYears')?.value,
    createdBy: this.userData.doctorId.toString()
  };


  this.http.httpPost(API_URLS.GADGET_SAVE, gadget).subscribe({
    next: (res: any) => {
      if (res.type === 'S') {
        alert('Saved Successfully');
        this.formValidation.showAlert('Saved Successfully', 'success');
        this.isSaved = true;
      } else {
        this.formValidation.showAlert('Error saving data', 'danger');
      }
    },
    error: (err) => {
      this.formValidation.showAlert('Server error while saving data', 'danger');
      console.error('Save error:', err);
    }
  });
}

  convertToNullableNumber(value: any): number | null {
    const n = Number(value);
    return !value || isNaN(n) ? null : n;
  }
  onNext(): void {
    const patientHistoryId = this.historyService.getPatientHistoryID();
    this.router.navigate([`/family-history/${this.patientId}/${this.stage}`], {
      // queryParams: {
      //   // id: patientHistoryId,
      //   tabId: this.tabId,
      //   patientId: this.patientId,
      //   isViewMode: this.isViewMode
      // },
      state: {
        patientId: this.patientId,
        tabId: this.tabId,
        stage: this.stage
      }
    });
  }
  
  OnNext(): void {
    const patientHistoryId = this.historyService.getPatientHistoryID();
    this.router.navigate([`/family-history/${this.patientId}/${this.stage}`], {
      // queryParams: {
      //   id: patientHistoryId,
      //   tabId: this.tabId,
      //   patientId: this.patientId,
      //   isViewMode: this.isViewMode
      // },
      state: {
        patientId: this.patientId,
        tabId: this.tabId,
        stage: this.stage
      }
    });
  }

  goback(): void {
    const patientHistoryId = this.historyService.getPatientHistoryID();
    this.router.navigate([`/sleep/${this.patientId}/${this.stage}`], {
      
      state: {
        patientId: this.patientId,
        tabId: this.tabId,
        stage: this.stage
      }
    });
  }
  back() {
    this.router.navigate([`/sleep/${this.patientId}/${this.stage}`], {
      state: {
        tabId: this.tabId,
        patientId: this.patientId,
        isViewMode: this.isViewMode
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
login(){
    this.router.navigate(['/login']);
  }


  getFieldLabel(fieldName: string): string {
  const fieldLabels: { [key: string]: string } = {
    computerUsed: 'Computer Used',
    computerUsedhrs: 'Computer Use (Hours)',
    computerUsedyears: 'Computer Use (Years)',
    smartphoneUsed: 'Smartphone Used',
    smartphoneUsedhrs: 'Smartphone Use (Hours)',
    smartphoneUsedyears: 'Smartphone Use (Years)',
    computerFrequency: 'Computer Frequency',
    computerDurationYears: 'Computer Duration (Years)',
    smartphoneFrequency: 'Smartphone Frequency',
    smartphoneDurationYears: 'Smartphone Duration (Years)',
    workingHours: 'Working Hours',
    jobType: 'Job Type',
    totalWorkingYears: 'Total Working Years',
    createdBy: 'Created By',
    patientId: 'Patient ID'
  };

  return fieldLabels[fieldName] || fieldName;
}

}