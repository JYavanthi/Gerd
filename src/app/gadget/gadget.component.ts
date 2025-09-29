import { Component, Input, OnInit, SimpleChanges } from '@angular/core';
import { ActivatedRoute, Router, } from '@angular/router';
import { HttpserviceService } from '../httpservice.service';
import { FormvalidationService } from '../formvalidation.service';
import { HistoryService } from '../Services/history.servie';
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
  @Input() data: any;
  @Input() isPrintMode: boolean = false;
  @Input() patientId: number | null = null;
  doctorId: number | null = null;
  isViewMode = false;
  isFollowUp: boolean = false;
  id: any;
  userData: any;
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
    private fb: FormBuilder, private patientService: PatientService,
    private gadgetService: gadgetService

  ) {

    this.gadgetForm = this.fb.group({
      id: [0],
      gadget: ['', Validators.required],
      computerUsed: ['', Validators.required],
      computerUsedhrs: ['', Validators.required],
      computerUsedyears: ['', Validators.required],

      smartphoneUsed: ['', Validators.required],
      smartphoneUsedhrs: ['', Validators.required],
      smartphoneUsedyears: ['', Validators.required],

      workingHours: ['', Validators.required],
      jobType: [null, Validators.required],
      totalWorkingYears: ['', Validators.required],
    });

  }


  ngOnChanges(changes: SimpleChanges): void {
    if (changes['data'] && this.data) {
      this.patchGadget(this.data);   // ✅ call patch when data changes
    }
  }



  patchGadget(data: any): void {
    if (!data) return;

    this.gadgetForm.patchValue({
      // id: data.id ?? 0,
      // gadget: data.gadget ?? '',

      // computerUsed: data.computerUsed ?? false,
      // computerUsedhrs: data.computerUsedhrs ?? '',
      // computerUsedyears: data.computerUsedyears ?? '',
      // computerFrequency: data.computerFrequency ?? '',
      // computerDurationYears: data.computerDurationYears ?? '',

      // smartphoneUsed: data.smartphoneUsed ?? false,
      // smartphoneUsedhrs: data.smartphoneUsedhrs ?? '',
      // smartphoneUsedyears: data.smartphoneUsedyears ?? '',
      // smartphoneFrequency: data.smartphoneFrequency ?? '',
      // smartphoneDurationYears: data.smartphoneDurationYears ?? '',

      // workingHours: data.workingHours ?? '',
      // jobType: data.jobType ?? '',
      // totalWorkingYears: data.totalWorkingYears ?? '',
      // createdBy: data.createdBy ?? ''

      id: data.id ?? 0,
      gadget: data.gadget ?? '',
      computerUsed: data.computerUsed,
      computerUsedhrs: data.computerFrequency ?? '',
      computerUsedyears: data.computerDurationYears ?? '',
      smartphoneUsed: data.smartphoneUsed,
      smartphoneUsedhrs: data.smartphoneFrequency ?? '',
      smartphoneUsedyears: data.smartphoneDurationYears ?? '',
      workingHours: data.workingHours ?? '',
      jobType: data.jobType ?? '',
      totalWorkingYears: data.totalWorkingYears ?? '',
      createdBy: data.createdBy
    });
  }

  ngOnInit(): void {
    this.stage = Number(this.route.snapshot.params['stage'] || 0);
    this.patientId = Number(this.route.snapshot.params['patientId']);


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


    this.isViewMode = this.isViewMode ?? false;
    this.fetchGadgetData(this.patientId);

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

  }
  fetchGadgetData(patientId: number): void {
    this.gadgetService.GetGadgetById(patientId, this.stage).subscribe({
      next: (res: any) => {
        if (res.type === 'S' && res.data) {
          const data = res.data;
          if (data) {
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
            smartphoneUsedyears: data.smartphoneDurationYears ?? '',
            workingHours: data.workingHours ?? '',
            jobType: data.jobType ?? '',
            totalWorkingYears: data.totalWorkingYears ?? '',
            createdBy: data.createdBy
          });

          this.toggleConditionalControls('computerUsed', data.computerUsed);
          this.toggleConditionalControls('smartphoneUsed', data.smartphoneUsed);

          // if (this.isViewMode) {
          //   this.gadgetForm.disable();
          // }

        } else {
          console.warn('⚠️ No gadget data found in response.');
        }
      },
      error: (err: any) => {
        console.error('❌ Error fetching gadget data:', err);
      }
    });

    this.http.httpGet(`/PatientReg/GetPatient/${patientId}`).subscribe({
      next: (res: any) => {
        if (res && res.data && res.data.occupation) {
          this.gadgetForm.patchValue({
            jobType: res.data.occupation
          });
          this.updateJobTypeRadioState(res.data.occupation);
        }
      },
      error: (err) => console.error('❌ Error fetching patient data:', err)
    });

  }


  toggleConditionalControls(controlName: string, value: boolean | string): void {
    const isUsed = value === true || value === 'true';
    const hrsControl = this.gadgetForm.get(`${controlName}hrs`);
    const yearsControl = this.gadgetForm.get(`${controlName}years`);


  }

  validateFld(): boolean {

    if (this.gadgetForm.get('computerUsed')?.value === '' || this.gadgetForm.get('computerUsed')?.value === null) {
      alert("Select Computer usage")
      return false;
    }
    if (this.gadgetForm.get('computerUsed')?.value) {
      if (this.gadgetForm.get('computerUsedhrs')?.value === '') {
        alert("Enter Computer used hour(s)");
        return false;
      }
      if (this.gadgetForm.get('computerUsedyears')?.value === '') {
        alert("Select Computer used year(s)");
        return false;
      }
    }

    if (this.gadgetForm.get('smartphoneUsed')?.value === '' || this.gadgetForm.get('smartphoneUsed')?.value === null) {
      alert("Select Smartphone usage")
      return false;
    }
    if (this.gadgetForm.get('smartphoneUsed')?.value) {
      if (this.gadgetForm.get('smartphoneUsedhrs')?.value === '') {
        alert("Enter Smartphone used hour(s)");
        return false;
      }
      if (this.gadgetForm.get('smartphoneUsedyears')?.value === '') {
        alert("Select Smartphone used year(s)");
        return false;
      }
    }
    if (this.gadgetForm.get('workingHours')?.value === '' || this.gadgetForm.get('workingHours')?.value === null) {
      alert("Select working hours");
      return false;
    }

    if (this.gadgetForm.get('jobType')?.value === '' || this.gadgetForm.get('jobType')?.value === null) {
      alert("Select job type");
      return false;
    }

    if (this.gadgetForm.get('totalWorkingYears')?.value === '') {
      alert("Select Duration");
      return false;
    }



    return true;
  }

  Submit(): void {

    if (!this.validateFld()) {
      return;
    }

    if (!this.gadgetForm.valid) {
      this.gadgetForm.markAllAsTouched();

      for (const controlName in this.gadgetForm.controls) {
        const control = this.gadgetForm.get(controlName);
        if (control && control.invalid) {
          alert(`Field "${this.getFieldLabel(controlName)}" is required.`);
          break;
        }
      }
      return;
    }

    //let user: any = localStorage.getItem('doctor');
    //  this.userData = JSON.parse(user);
    // const gadgetFormValues = this.gadgetForm.getRawValue();
    this.doctorId = this.patientService.getDoctorId();

    const formValue = this.gadgetForm.value;


    const payload = {
      flag: 'I',
      id: this.gadgetForm.get('id')?.value ?? 0,
      patientId: this.patientId ?? null,
      stage: this.stage ?? null,
      gadget: this.gadgetForm.get('gadget')?.value?.trim() ?? '',

      computerUsed: !!this.gadgetForm.get('computerUsed')?.value,
      computerFrequency: this.gadgetForm.get('computerUsedhrs') != null
        ? String(this.gadgetForm.get('computerUsedhrs')?.value)
        : null,
      computerDurationYears: this.gadgetForm.get('computerUsedyears')?.value
        ? Number(this.gadgetForm.get('computerUsedyears')?.value)
        : null,

      smartphoneUsed: !!this.gadgetForm.get('smartphoneUsed')?.value,
      smartphoneFrequency: this.gadgetForm.get('smartphoneUsedhrs') != null
        ? String(this.gadgetForm.get('smartphoneUsedhrs')?.value)
        : null,
      smartphoneDurationYears: this.gadgetForm.get('smartphoneUsedyears')?.value
        ? Number(this.gadgetForm.get('smartphoneUsedyears')?.value)
        : null,

      workingHours: this.gadgetForm.get('workingHours')?.value ?? null,
      jobType: this.gadgetForm.get('jobType')?.value ?? null,
      totalWorkingYears: this.gadgetForm.get('totalWorkingYears')?.value ?? null,
      createdBy: this.doctorId?.toString() ?? null
    };


    this.http.httpPost(API_URLS.GADGET_SAVE, payload).subscribe({
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
  login() {
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



  blockInvalidKeys(event: KeyboardEvent) {
    if (['e', 'E', '+', '-','.'].includes(event.key)) {
      event.preventDefault();
    }
  }


  preventNegative(event: any) {

    if (event.target.value < 0) {
      event.target.value = 0; // reset to 0 if negative
    }
  }


  updateJobTypeRadioState(selectedValue: string | null): void {
    const sedentaryControl = document.querySelector<HTMLInputElement>(
      'input[name="jobType"][value="Sedentary"]'
    );
    const nonSedentaryControl = document.querySelector<HTMLInputElement>(
      'input[name="jobType"][value="Non-Sedentary"]'
    );

    if (selectedValue === 'Sedentary') {
      sedentaryControl!.disabled = false;
      nonSedentaryControl!.disabled = true;
    } else if (selectedValue === 'Non-Sedentary') {
      sedentaryControl!.disabled = true;
      nonSedentaryControl!.disabled = false;
    } else {
      sedentaryControl!.disabled = false;
      nonSedentaryControl!.disabled = false;
    }
  }

}