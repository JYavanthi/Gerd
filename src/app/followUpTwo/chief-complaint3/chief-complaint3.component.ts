import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { FormvalidationService } from '../../formvalidation.service';
import { ActivatedRoute, Router } from '@angular/router';
import { HttpserviceService } from '../../httpservice.service';
import { PatientService } from '../../Services/patient.service';
import { API_URLS } from '../../shared/API-URLs';
import { ChiefComplaintService } from '../../Services/chief-complaint.service';
import { id } from '@swimlane/ngx-charts';

@Component({
  selector: 'app-chief-complaint',
  templateUrl: './chief-complaint3.component.html',
  styleUrls: ['./chief-complaint3.component.css']
})
export class ChiefComplaint3Component implements OnInit {
  tabId = 1;
  patientId: any;

  chiefComplaintForm: FormGroup;
  showCodeMessage = false;
  isViewMode: boolean = false;
  formData: any;
  isFollowUp: boolean = false;
  stage: number=0;
  activeTabId: any = 2;

  constructor(
    private patientService: PatientService,
    private fb: FormBuilder,
    private http: HttpserviceService,
    private formValidation: FormvalidationService,
    public route: ActivatedRoute,
    private router: Router,
    private chiefComplaintService: ChiefComplaintService

  ) {
    // Initialize form
    this.chiefComplaintForm = this.fb.group({
      heartburnDuration: [null, Validators.required],
      heartburnFrequency: [null, Validators.required],
      postural_heartburn: [null, Validators.required],
      nocturnal_heartburn: [null, Validators.required],

      regurgitationDuration: [null, Validators.required],
      regurgitationFrequency: [null, Validators.required],
      postural_regurgitation: [null, Validators.required],
      nocturnal_regurgitation: [null, Validators.required],

      painDuration: [null, Validators.required],
      painFrequency: [null, Validators.required],
      postural_pain: [null, Validators.required],
      nocturnal_pain: [null, Validators.required],

      acidTasteDuration: [null, Validators.required],
      acidTasteFrequency: [null, Validators.required],
      postural_AT: [null, Validators.required],
      nocturnal_AT: [null, Validators.required]
    });
  }

 ngOnInit(): void {
  console.log("Tabid", this.tabId);
  const state = history.state;
  this.tabId = state?.tabId ?? 1;
  this.patientId = state?.patientId;
  this.isViewMode = state?.isViewMode ?? false;
  this.formData = state?.data;

  this.isFollowUp = this.router.url.includes('followUpOne') || this.router.url.includes('followUpTwo');
      this.fetchChiefComplaintData(this.patientService.getPatientId());

  this.route.params.subscribe(params => {
    const idFromRoute = +params['patientId'];
    if (idFromRoute) {
      this.patientId = idFromRoute;
      console.log('✅ Patient ID from route:', this.patientId);

      this.fetchChiefComplaintData(this.patientId);
    } else {
      console.warn('⚠️ Invalid patient ID in route!');
    }
  });
}
fetchChiefComplaintData(patientId: number): void {
  this.chiefComplaintService.getChiefComplaintByPatientId(patientId ,this.stage).subscribe({
    next: (res: any) => {
      if (res.type === 'S' && res.data) {
        const data = res.data;
        console.log('✅ Chief Complaint data:', data);

        this.stage = data.stage;
        console.log("Stage",this.stage)

        this.chiefComplaintForm.patchValue({
          heartburnDuration: data.hbDuration,
          heartburnFrequency: data.hbFrequency,
          postural_heartburn: data.hbPostural,
          nocturnal_heartburn: data.hbNocturnal,
          regurgitationDuration: data.rDuration,
          regurgitationFrequency: data.rFrequency,
          postural_regurgitation: data.rPostural,
          nocturnal_regurgitation: data.rNocturnal,
          painDuration: data.rpDuration,
          painFrequency: data.rpFrequency,
          postural_pain: data.rpPostural,
          nocturnal_pain: data.rpNocturnal,
          acidTasteDuration: data.atDuration,
          acidTasteFrequency: data.atFrequency,
          postural_AT: data.atPostural,
          nocturnal_AT: data.atNocturnal
        });
console.log('Patched form value:', this.chiefComplaintForm.value);
        // ✅ Disable only after patching values
        // if (this.isViewMode) {
        //   this.chiefComplaintForm.disable();
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



  onCodeFocus(): void {
    this.showCodeMessage = true;
  }

  Submit(): void {
    console.log('Chief Complaint Submit function triggered');

    if (this.chiefComplaintForm.invalid) {
      const controls = this.chiefComplaintForm.controls;
      for (const field in controls) {
        if (controls[field].invalid) {
          alert(`Please fill or select: ${field.replace(/_/g, ' ')}`);
          this.chiefComplaintForm.markAllAsTouched();
          return;
        }
      }
    }

    if (!this.formValidation.validateForm(this.chiefComplaintForm)) {
      this.chiefComplaintForm.markAllAsTouched();
      return;
    }

    const patientId = this.patientService.getPatientId();
    const doctorId = this.patientService.getDoctorId() || 0;

    if (!patientId) {
      this.formValidation.showAlert('Patient ID is missing', 'danger');
      return;
    }

    const formValue = this.chiefComplaintForm.value;
    const param = {
      stage: 4,
      flag: 'I',
      cheifCompliantID: 0,
      patientID: patientId,
      doctorID: doctorId,
      hB_Duration: Number(formValue.heartburnDuration),
      hB_Frequency: Number(formValue.heartburnFrequency),
      hB_Postural: formValue.postural_heartburn,
      hB_Nocturnal: formValue.nocturnal_heartburn,
      r_Duration: Number(formValue.regurgitationDuration),
      r_Frequency: Number(formValue.regurgitationFrequency),
      r_Postural: formValue.postural_regurgitation,
      r_Nocturnal: formValue.nocturnal_regurgitation,
      rP_Duration: Number(formValue.painDuration),
      rP_Frequency: Number(formValue.painFrequency),
      rP_Postural: formValue.postural_pain,
      rP_Nocturnal: formValue.nocturnal_pain,
      aT_Duration: Number(formValue.acidTasteDuration),
      aT_Frequency: Number(formValue.acidTasteFrequency),
      aT_Postural: formValue.postural_AT,
      aT_Nocturnal: formValue.nocturnal_AT,
      createdBy: 0
    };

    console.log('Submitting Chief Complaint Payload:', param);

    this.http.httpPost(API_URLS.CHEIF_COMPLAINT_SAVE, param).subscribe(
      (res: any) => {
        if (res.type === 'S') {
          console.log('Chief complaint saved successfully.');

          this.http.httpGet('/PatientReg/GetPatient').subscribe((getRes: any) => {
            if (getRes.type === 'S' && getRes.data?.length > 0) {
              const latestPatient = getRes.data[getRes.data.length - 1];
              const updatedPatientId = latestPatient.patientId;

              this.patientService.setPatientId(updatedPatientId);
              this.patientService.setDoctorId(param.createdBy);

              this.formValidation.showAlert('Chief complaint saved successfully', 'success');
              this.router.navigate([], {
                queryParams: {
                  patientId: updatedPatientId,
                  doctorId: param.createdBy
                }
              });
            } else {
              this.formValidation.showAlert('Unable to fetch Patient ID after save', 'danger');
            }
          });
        } else {
          this.formValidation.showAlert(`Error: ${res.message || 'Unknown error'}`, 'danger');
        }
      },
      error => {
        console.error('Error saving chief complaint:', error);
        this.formValidation.showAlert('Error saving chief complaint', 'danger');
      }
    );
  }

  goToComorbidities(): void {
    this.router.navigate(['/comorbidities']);
  }

OnNext() {
  this.router.navigate(['/comorbidities'], {
    state: {
      tabId: this.tabId,
      patientId: this.patientId,
      isViewMode: this.isViewMode
    }
  });
}

goback() {
  this.router.navigate(['/demographic'], {
    state: {
      tabId: this.tabId,
      patientId: this.patientId,
      isViewMode: this.isViewMode
    }
  });
}
back() {
  this.router.navigate([`/case-details/${this.patientService.getPatientId()}`], {
    state: {
      tabId: this.tabId,
      patientId: this.patientService.getPatientId(),
      isViewMode: this.isViewMode
    }
  });
}
  onNext(){
  console.log("this is followup one")
  console.log("Tabid", this.tabId)
  this.router.navigate([`followUpTwo/comorbidities/${this.patientId}`], {
    state: {
      data: this.formData,
      isViewMode: this.isViewMode,
      patientId: this.patientId,

    }
  });
}






onTabClick(tabId: number) {
  this.activeTabId = tabId;
  console.log('Tab id', this.activeTabId);
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