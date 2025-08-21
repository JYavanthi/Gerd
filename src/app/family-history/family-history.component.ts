
import { Component, OnInit } from '@angular/core';
import { FormArray, FormBuilder, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { FormvalidationService } from '../formvalidation.service';
import { HttpserviceService } from '../httpservice.service';
import { API_URLS } from '../shared/API-URLs';
import { FamilyHistoryService } from '../Services/family-history.service';
import { PatientService } from '../Services/patient.service';

@Component({
  selector: 'app-family-history',
  templateUrl: './family-history.component.html',
  styleUrls: ['./family-history.component.css']
})
export class FamilyHistoryComponent implements OnInit {
  tabId = 1;
  stage: number = 0;
  familyhistoryForm: FormGroup;
  patientId: number | null = null;
  doctorId: number | null = null;
  isViewMode = false;
  isFollowUp = false;
  isSaved: boolean = false;
  userData: any;

  constructor(
    private fb: FormBuilder,
    private formValidation: FormvalidationService,
    private http: HttpserviceService,
    private router: Router,
    public route: ActivatedRoute,
    private familyHistoryService: FamilyHistoryService,
    private patientService: PatientService
  ) {
    this.familyhistoryForm = this.fb.group({
      fH_GRED: ['', Validators.required],
      fH_Remark: ['', Validators.required],
      fH_EGC: ['', Validators.required],
      fH_EGCRemark: ['', Validators.required],
      ppiusage: ['',Validators.required],
      medications: this.fb.array([this.createMedicationGroup()])
    });
  }

  ngOnInit(): void {
    this.stage = Number(this.route.snapshot.params['stage'] || 0);
    const allowedWithoutSave = [1, 3, 5];
    if (allowedWithoutSave.includes(this.stage)) {
      this.isSaved = true;
    }

    this.familyhistoryForm.get('fH_GRED')?.valueChanges.subscribe(value => {
      const control = this.familyhistoryForm.get('fH_Remark');
      if (value === 'yes') {
        control?.enable();
      } else {
        control?.disable();
        control?.setValue('');
      }
    });

    this.familyhistoryForm.get('fH_EGC')?.valueChanges.subscribe(value => {
      const control = this.familyhistoryForm.get('fH_EGCRemark');
      if (value === 'yes') {
        control?.enable();
      } else {
        control?.disable();
        control?.setValue('');
      }
    });

    if (this.familyhistoryForm.get('fH_GRED')?.value !== 'yes') {
      this.familyhistoryForm.get('fH_Remark')?.disable();
    }

    if (this.familyhistoryForm.get('fH_EGC')?.value !== 'yes') {
      this.familyhistoryForm.get('fH_EGCRemark')?.disable();
    }
    
    this.route.params.subscribe(params => {
      this.patientId = +params['patientId'];
      console.log('this.patientId',this.patientId);
      this.stage = +params['stage'];

      //const currentUrl = this.router.url;
     // this.isFollowUp = currentUrl.includes('follow-up-1') || currentUrl.includes('follow-up-2');

      if (this.patientService.getPatientId()) {
        this.loadFamilyHistory(this.patientId);
        this.loadMedicationData(this.patientId, this.stage);
      }
    });

    //const cachedData = this.patientService.getfamalyhistoryData();
   // if (cachedData) {
   //   this.familyhistoryForm.patchValue(cachedData);
   // }

    // Ensure at least one medication row exists initially if FormArray is empty
    if (this.medications.length === 0) {
      this.medications.push(this.createMedicationGroup());
    }
  }
  createMedicationGroup(): FormGroup {
    return this.fb.group({
      medicationName: ['', Validators.required],
      dose: ['', Validators.required],
      frequency: ['', Validators.required]
    });
  }

  get medications(): FormArray {
    return this.familyhistoryForm.get('medications') as FormArray;
  }

  addRow() {
    this.medications.push(this.createMedicationGroup());
  }

  deleteRow(index: number) {
    if (this.medications.length > 1) {
      this.medications.removeAt(index);
    } else {
      this.medications.at(0).reset();
    }
  }

  
  loadMedicationData(patientId: number, stage: number): void {
    const url = API_URLS.MEDICATION_GET_BY_ID
      .replace('{patientId}', patientId.toString())
      .replace('{stage}', stage.toString());
    // const url = `${API_URLS}/Medication/GetMedicationByPatientId${patientId}/${stage}`
    this.http.httpGet(url).subscribe((res: any) => {
      if (res?.type === 'S') {
        this.medications.clear();

        if (Array.isArray(res.data) && res.data.length > 0) {
          res.data.forEach((med: any) => {
            this.medications.push(this.fb.group({
              medicationName: med.medicationName || '',
              dose: med.dose || '',
              frequency: med.frequency || ''
            }));
          });
        } else {
          // No data found, ensure one empty row is available for input
          this.medications.push(this.createMedicationGroup());
        }
      }
    });
  }

  loadFamilyHistory(patientId: number): void {
    this.familyHistoryService.getFamilyHistoryById(patientId, this.stage).subscribe((res: any) => {
      const data = res?.data;
      //this.stage = data.stage;
      if (data) {
         console.log('loadFamilyHistory',data.patientId)
          this.familyhistoryForm.patchValue({
          fH_GRED: data.fhGred,
          fH_Remark: data.fhRemark,
          fH_EGC: data.fhEgc,
          fH_EGCRemark: data.fhEgcremark,
          ppiusage: data.ghPpi
        });

        if (Array.isArray(data.medications)) {
          this.medications.clear();
          data.medications.forEach((med: any) => {
            this.medications.push(this.fb.group({
              medicationName: med.medicationName || '',
              dose: med.dose || '',
              frequency: med.frequency || ''
            }));
          });
        }
      }
    });
  }

  Submit() {
    if (!this.formValidation.validateForm(this.familyhistoryForm)) {
      this.familyhistoryForm.markAllAsTouched();
      return;
    }
    let user: any = localStorage.getItem('doctor')
    this.userData = JSON.parse(user);

    const param = {
      stage: this.stage,
      Flag: 'I',
      FamilyHistoryID: 0,
      DoctorID: this.userData?.doctorId,
      PatientID: this.patientId,
      FH_GRED: this.familyhistoryForm.get('fH_GRED')?.value,
      FH_Remark: this.familyhistoryForm.get('fH_Remark')?.value,
      FH_EGC: this.familyhistoryForm.get('fH_EGC')?.value,
      FH_EGCRemark: this.familyhistoryForm.get('fH_EGCRemark')?.value,
      gH_PPI: this.familyhistoryForm.get('ppiusage')?.value,
      Medication_Name: '',
      Dose: '',
      Frequency: '',
      CreatedBy: this.userData?.doctorId
    };
    this.http.httpPost(API_URLS.FAMILY_HISTORY_SAVE, param).subscribe((res: any) => {
      if (res.type === 'S') {
        this.isSaved = true;
        const medicationRequests = this.medications.controls.map((medCtrl) => {
          const medValue = medCtrl.value;
          const medParam = {
            Flag: 'I',
            patientId: this.patientId,
            Stage: this.stage,
            MedicationID: 0,
            GHID: 0,
            MedicationName: medValue.medicationName ?? '',
            Dose: medValue.dose ?? '',
            Frequency: medValue.frequency ?? '',
            Molecule: '',
            CreatedBy: 0 //
          };
          return this.http.httpPost(API_URLS.MedicationY_SAVE, medParam).toPromise();
        });

        Promise.all(medicationRequests).then(() => {
          this.formValidation.showAlert('Saved Successfully', 'success');

          this.http.httpGet('/PatientReg/GetPatient').subscribe((getRes: any) => {
            if (getRes.type === 'S' && getRes.data?.length > 0) {
              this.isSaved = true;
              alert('Saved Successfully'); // ← Test this
              this.formValidation.showAlert('Saved Successfully', 'success');
              const latestPatient = getRes.data[getRes.data.length - 1];
              const updatedPatientId = latestPatient.patientId;

              this.patientService.setPatientId(updatedPatientId);
              this.patientService.setDoctorId(param.CreatedBy);

              this.router.navigate([], {
                queryParams: {
                  patientId: updatedPatientId,
                  doctorId: param.CreatedBy
                }
              });
            } else {
              this.formValidation.showAlert('Unable to fetch Patient ID after save', 'danger');
            }
          });
        }).catch(() => {
          this.formValidation.showAlert('Error saving medication data!', 'danger');
        });
      } else {
        this.formValidation.showAlert('Error saving family history!', 'danger');
      }
    });
  }


  onNext() {
    const currentUrl = this.router.url;

    if (currentUrl.includes('follow-up-1')) {
      this.router.navigate([`/history-endoscopy/${this.patientId}/${this.stage}`], {
        state: {
          patientId: this.patientId,
          tabId: this.tabId,
          isViewMode: this.isViewMode,
          stage: this.stage
        }
      });
    } else {
      this.router.navigate(['/follow-up-2/history-endoscopy'], {
        state: {
          patientId: this.patientId,
          tabId: this.tabId,
          isViewMode: this.isViewMode,
          stage: this.stage
        }
      });
    }
  }
  
  OnNext() {
    this.router.navigate([`/history-endoscopy/${this.patientId}/${this.stage}`], {
      state: {
        tabId: this.tabId,
        patientId: this.patientId,
        isViewMode: this.isViewMode
      }
    });
  }
  goback() {
    this.router.navigate([`/gadget/${this.patientId}/${this.stage}`], {
      state: {
        tabId: this.tabId,
        patientId: this.patientId,
        stage:this.stage,
        isViewMode: true
      }
    });
  }
  back() {
    this.router.navigate([`/gadget/${this.patientId}/${this.stage}`], {
      state: {
        tabId: this.tabId,
        patientId: this.patientId,
        isViewMode: true
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
