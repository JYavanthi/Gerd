
import { Component, Input, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { MedicalExaminationService } from '../Services/medicalExamination.service';
import { HttpserviceService } from '../httpservice.service';
import { FormvalidationService } from '../formvalidation.service';
import { API_URLS } from '../shared/API-URLs';
import { PatientService } from '../Services/patient.service';
import { medicalExaminationService } from '../Services/medicalExam.service';

@Component({
  selector: 'c',
  templateUrl: './medical-examination.component.html',
  styleUrls: ['./medical-examination.component.css']
})
export class MedicalExaminationComponent implements OnInit {
  medicalExaminationForm: FormGroup;
  tabId = 1;
  @Input() stage = 0;
  height: number = 0;
  weight: number = 0;
  bmi: number = 0;
  isViewMode = false;
  isFollowUp: boolean = false;
  @Input() patientId: number | null = null;
  doctorId: number | null = null;
  isSaved: boolean = false;
  @Input() isPrintMode = false;

  constructor(
    private fb: FormBuilder,
    private router: Router,
    private http: HttpserviceService,
    private formValidation: FormvalidationService,
    private patientService: PatientService,
    private medicalExaminationService: medicalExaminationService,
     public route: ActivatedRoute,
  ) {
    this.medicalExaminationForm = this.fb.group({
      Flag: [''],
      MEID: [''],
      DoctorID: [''],
      PatientID: [this.patientService.getPatientId()],
      Stage: [''],

      PE_Height: ['', Validators.required],
      PE_Weight: ['', Validators.required],
      PE_BMI: [{ value: '', disabled: true }],
      SE_GANormal: [''],
      SE_GAAbNormalCS: [''],
      SE_GAAbNormalNCS: [''],
      PE_BMSE_GAAbNormalRemarkI5: [{ value: '', disabled: true }],

      PAE_Findings: [''],

      SE_RSNormal: [''],
      SE_RSAbNormal_CS: [''],
      SE_RSAbNormal_NCS: [''],
      SE_RSAbNormalRemark: [{ value: '', disabled: true }],

      OthersNormal: [''],
      OthersAbNormal_CS: [''],
      OthersAbNormal_NCS: [''],
      OthersAbNormalRemark: [{ value: '', disabled: true }],

      CreatedBy: ['']
    });

  }


  ngOnInit(): void {
 this.stage = Number(this.route.snapshot.params['stage'] || 0);
   const allowedWithoutSave = [1, 3, 5];
  if (allowedWithoutSave.includes(this.stage)) {
    this.isSaved = true;
  }
    const currentUrl = this.router.url;
    const state = history.state;

    this.tabId = state?.tabId ?? 1;
    this.patientId = state?.patientId ?? this.patientService.getPatientId();
    if (this.patientId) {
      this.patientService.setPatientId(this.patientId); // ✅ Store it globally
    } else {
      console.warn('⚠️ Patient ID is missing in state and service');
    }

    this.isViewMode = state?.isViewMode ?? false;
    this.isFollowUp = currentUrl.includes('follow-up-1') || currentUrl.includes('follow-up-2');

    if (currentUrl === '/follow-up-1/medical-examination' && this.patientService.getPatientId()) {
      this.fetchMedicalExaminationData(this.patientService.getPatientId());
    }

    this.medicalExaminationForm.get('PE_Height')?.valueChanges.subscribe(() => this.calculateBMI());
    this.medicalExaminationForm.get('PE_Weight')?.valueChanges.subscribe(() => this.calculateBMI());

    const cachedData = this.patientService.getfamalyhistoryData();
    if (cachedData) {
      this.medicalExaminationForm.patchValue(cachedData);
    } else {
      this.fetchMedicalExaminationData(this.patientService.getPatientId());

    }

  }
  fetchMedicalExaminationData(patientId: number): void {
    this.http.httpGet(`/MedicalExamination/GetMedicalExaminationById/${patientId}`)
      .subscribe({
        next: (res: any) => {
          if (res?.type === 'S' && res.data) {
            const d = res.data;

            this.bmi = d.PE_BMI ?? null;
            this.stage = d.stage,

              this.medicalExaminationForm.patchValue({
                MEID: d.meid ?? '',
                PE_Height: d.peHeight ?? '',
                PE_Weight: d.peWeight ?? '',
                PE_BMI: d.peBmi ?? '',

                SE_GANormal: d.seGanormal === '1' ? 'normal' : d.seGanormal === '0' ? '' : null,
                SE_GAAbNormalCS: d.seGaabNormalCs === '1' ? 'cs' : d.seGaabNormalCs === '0' ? '' : null,
                SE_GAAbNormalNCS: d.seGaabNormalNcs === '1' ? 'ncs' : d.seGaabNormalNcs === '0' ? '' : null,
                PE_BMSE_GAAbNormalRemarkI5: d.seGaabNormalRemark ?? '',

                PAE_Findings: d.paeFindings ?? '',

                SE_RSNormal: d.seRsnormal === '1' ? 'normal' : d.seRsnormal === '0' ? '' : null,
                SE_RSAbNormal_CS: d.seRsabNormalCs === '1' ? 'cs' : d.seRsabNormalCs === '0' ? '' : null,
                SE_RSAbNormal_NCS: d.seRsabNormalNcs === '1' ? 'ncs' : d.seRsabNormalNcs === '0' ? '' : null,
                SE_RSAbNormalRemark: d.seRsabNormalRemark ?? '',

                OthersNormal: d.othersNormal === '1' ? 'normal' : d.othersNormal === '0' ? '' : null,
                OthersAbNormal_CS: d.othersAbNormalCs === '1' ? 'cs' : d.othersAbNormalCs === '0' ? '' : null,
                OthersAbNormal_NCS: d.othersAbNormalNcs === '1' ? 'ncs' : d.othersAbNormalNcs === '0' ? '' : null,
                OthersAbNormalRemark: d.othersAbNormalRemark ?? '',

                CreatedBy: d.createdBy ?? 0
              });
            if (this.isViewMode) {
              this.medicalExaminationForm.disable();
            }

            console.log('✅ Medical Examination data patched:', this.medicalExaminationForm.value);
          } else {
            console.warn('⚠️ No Medical Examination data found.');
          }
        },
        error: (err) => {
          console.error('❌ Error fetching Medical Examination data:', err);
        }
      });
  }
  onGAChange(value: string): void {
    this.medicalExaminationForm.patchValue({
      SE_GANormal: value === 'normal' ? 'normal' : '',
      SE_GAAbNormalCS: value === 'cs' ? 'cs' : '',
      SE_GAAbNormalNCS: value === 'ncs' ? 'ncs' : '',
    });

    const remarkControl = this.medicalExaminationForm.get('PE_BMSE_GAAbNormalRemarkI5');
    if (value === 'cs') {
      remarkControl?.enable();
    } else {
      remarkControl?.disable();
      remarkControl?.setValue('');
    }
  }

  onRespiratoryChange(value: string): void {
    this.medicalExaminationForm.patchValue({
      SE_RSNormal: value === 'normal' ? 'normal' : '',
      SE_RSAbNormal_CS: value === 'cs' ? 'cs' : '',
      SE_RSAbNormal_NCS: value === 'ncs' ? 'ncs' : '',
    });

    const remarkControl = this.medicalExaminationForm.get('SE_RSAbNormalRemark');
    if (value === 'cs') {
      remarkControl?.enable();
    } else {
      remarkControl?.disable();
      remarkControl?.setValue('');
    }
  }

  onOthersChange(value: string): void {
    this.medicalExaminationForm.patchValue({
      OthersNormal: value === 'normal' ? 'normal' : '',
      OthersAbNormal_CS: value === 'cs' ? 'cs' : '',
      OthersAbNormal_NCS: value === 'ncs' ? 'ncs' : '',
    });

    const remarkControl = this.medicalExaminationForm.get('OthersAbNormalRemark');
    if (value === 'cs') {
      remarkControl?.enable();
    } else {
      remarkControl?.disable();
      remarkControl?.setValue('');
    }
  }

  Submit(): void {
    if (!this.formValidation.validateForm(this.medicalExaminationForm)) {
      this.medicalExaminationForm.markAllAsTouched();
      return;
    }

    const formValue = this.medicalExaminationForm.value;
    const medicalExaminationPayload = {
      Meid: !!formValue.MEID ? parseInt(formValue.MEID, 10) : null,
      Flag: !!formValue.MEID ? 'U' : 'I',
      DoctorId: 0,
      PatientId: this.patientService.getPatientId() ?? 0,
      Stage: this.stage,

      pE_Height: formValue.PE_Height !== '' ? formValue.PE_Height : null,
      pE_Weight: formValue.PE_Weight !== '' ? formValue.PE_Weight : null,
      pE_BMI: this.bmi != null ? this.bmi.toString() : null,

      sE_GANormal: formValue.SE_GANormal === 'normal',
      sE_GAAbNormalCS: formValue.SE_GAAbNormalCS === 'cs',
      sE_GAAbNormalNCS: formValue.SE_GAAbNormalNCS === 'ncs',
      pE_BMSE_GAAbNormalRemarkI5: formValue.PE_BMSE_GAAbNormalRemarkI5,

      sE_RSNormal: formValue.SE_RSNormal === 'normal',
      sE_RSAbNormal_CS: formValue.SE_RSAbNormal_CS === 'cs',
      sE_RSAbNormal_NCS: formValue.SE_RSAbNormal_NCS === 'ncs',
      sE_RSAbNormalRemark: formValue.SE_RSAbNormalRemark,

      othersNormal: formValue.OthersNormal === 'normal',
      othersAbNormal_CS: formValue.OthersAbNormal_CS === 'cs',
      othersAbNormal_NCS: formValue.OthersAbNormal_NCS === 'ncs',
      othersAbNormalRemark: formValue.OthersAbNormalRemark,

      paE_Findings: formValue.PAE_Findings,

      CreatedBy: formValue.CreatedBy || 0
    };

    const payload = {
      ...medicalExaminationPayload,
      Stage: this.stage
    };

    console.log('Payload being sent:', JSON.stringify(payload, null, 2));

    this.http.httpPost('/MedicalExamination/SaveMedicalExamination', payload).subscribe({
      next: (res: any) => {
        if (res?.type === 'S') {
          this.isSaved = true;
          alert('Saved Successfully');
        } else {
          this.formValidation.showAlert('Error!!', 'danger');
        }
      },
      error: err => {
        console.error('Submit error: ', err);
        this.formValidation.showAlert('Error!!', 'danger');
      }
    });
  }

  calculateBMI() {
    const height = this.medicalExaminationForm.get('PE_Height')?.value;
    const weight = this.medicalExaminationForm.get('PE_Weight')?.value;

    if (height > 0 && weight > 0) {
      const bmi = weight / ((height / 100) ** 2);
      this.bmi = parseFloat(bmi.toFixed(2)); // update local variable
      this.medicalExaminationForm.patchValue({
        PE_BMI: this.bmi
      });
    } else {
      this.bmi = 0;
      this.medicalExaminationForm.patchValue({
        PE_BMI: ''
      });
    }
  }

  canProceed(): boolean {
    const form = this.medicalExaminationForm;

    const heightValid = form.get('PE_Height')?.valid ?? false;
    const weightValid = form.get('PE_Weight')?.valid ?? false;
    const bmiValue = form.get('PE_BMI')?.value;
    const gaNormal = form.get('SE_GANormal')?.value;
    const rsNormal = form.get('SE_RSNormal')?.value;
    const othersNormal = form.get('OthersNormal')?.value;

    const paE_Findings = form.get('PAE_Findings')?.value;


    return (
      heightValid &&
      weightValid &&
      !!bmiValue &&
      !!gaNormal &&
      !!rsNormal &&
      !!othersNormal &&
      paE_Findings
    );
  }
  onNext() {
    const currentUrl = this.router.url;
    const patientId = this.patientId;

    if (currentUrl.includes('follow-up-1')) {
      this.router.navigate([`/assessment/${this.patientId}/${this.stage}`]);
    } else {
      this.router.navigate([`/assessment/${this.patientId}/${this.stage}`], {
        state: {
          fromNavigation: true ,
          tabId: this.tabId,
          patientId: this.patientId,
          isViewMode: this.isViewMode
        }
      });
    }
  }
  OnNext() {
    this.router.navigate([`/assessment/${this.patientId}/${this.stage}`], {
      state: {
         fromNavigation: true ,
        tabId: this.tabId,
        patientId: this.patientId,
        isViewMode: this.isViewMode
      }
    });
  }
  goback() {
    this.router.navigate([`/history-endoscopy/${this.patientId}/${this.stage}`], {
      state: {
        tabId: this.tabId,
        patientId: this.patientId,
        isViewMode: this.isViewMode
      }
    });
  }

  back() {
    this.router.navigate([`/history-endoscopy/${this.patientId}/${this.stage}`], {
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

}










