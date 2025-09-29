
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
      ppiusage: ['', Validators.required],
      medications: this.fb.array([this.createMedicationGroup()])
    });
  }

  ngOnInit(): void {
    this.stage = Number(this.route.snapshot.params['stage'] || 0);
    const allowedWithoutSave = [1, 3, 5];
    if (allowedWithoutSave.includes(this.stage)) {
      this.isSaved = true;
    }
   if (this.familyhistoryForm.get('ppiusage')?.value !== 'yes') {
    this.medications.disable(); // disable all medication controls
  }

  // Listen for changes to PPI usage
  this.familyhistoryForm.get('ppiusage')?.valueChanges.subscribe(value => {
    if (value === 'yes') {
      this.medications.enable(); // enable if yes
    } else {
      this.medications.disable(); 
      this.medications.controls.forEach(ctrl => {
        ctrl.get('medicationName')?.setValue('');
        ctrl.get('dose')?.setValue('');
        ctrl.get('frequency')?.setValue('');
      });
    }
  });


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
      this.stage = +params['stage'];

      //const currentUrl = this.router.url;
      //this.isFollowUp = currentUrl.includes('follow-up-1') || currentUrl.includes('follow-up-2');

      if (this.patientId) {
        this.loadFamilyHistory(this.patientId);
        this.loadMedicationData(this.patientId, this.stage);
      }

    });

   
  }

  

  createMedicationGroup(med: any = {}): FormGroup {
    return this.fb.group({
      medicationId: [med.medicationId || 0],
      medicationName: [med.medicationName || '', Validators.required],
      dose: [med.dose || '', Validators.required],
      frequency: [med.frequency || '', Validators.required]
    });
  }




  get medications(): FormArray {
    return this.familyhistoryForm.get('medications') as FormArray;
  }

  addRow() {
    this.medications.push(this.createMedicationGroup());
  }


  deleteRow(group: any, index: number) {
    const medValue = group.value;
    console.log('group.value', group.value);
    const medicationID = medValue.medicationId || 0;
    console.log('medicationID', medicationID, index);
    if (medicationID > 0) {
      const medParam = {
        Flag: 'D',
        patientId: this.patientId,
        stage: this.stage,
        medicationId: medicationID,
        GHID: 0,
        medicationName: medValue.medicationName ?? '',
        Dose: medValue.dose ?? '',
        Frequency: medValue.frequency ?? '',
        Molecule: '',
        CreatedBy: 0
      };

      this.http.httpPost(API_URLS.MedicationY_SAVE, medParam).subscribe({
        next: (res: any) => {
          console.log('medParam', medParam);
          if (res?.type === 'S') {
            this.medications.removeAt(index);
            alert('Medication deleted successfully!');
            index--;
            // remove only after backend confirms
          } else {
            alert('Failed to delete medication!');
          }
        },
        error: () => {
          alert('Error deleting medication!');
        }
      });
    } else {
      // Row not in DB, just remove from UI
      this.medications.removeAt(index);
    }
  }


  loadMedicationData(patientId: number, stage: number): void {
  const url = API_URLS.MEDICATION_GET_BY_ID
    .replace('{patientId}', patientId.toString())
    .replace('{stage}', stage.toString());

  this.http.httpGet(url).subscribe((res: any) => {
    this.medications.clear(); // clear existing rows

    if (res?.type === 'S' && res.data.length > 0) {
      res.data.forEach((med: any) => {
        const group = this.fb.group({
          medicationId: med.medicationId || '',
          medicationName: med.medicationName || '',
          dose: med.dose || '',
          frequency: med.frequency || ''
        });

        if (this.familyhistoryForm.get('ppiusage')?.value !== 'yes') {
          group.disable();
        }

        this.medications.push(group);
      });
    } else {
      const group = this.createMedicationGroup();
      if (this.familyhistoryForm.get('ppiusage')?.value !== 'yes') {
        group.disable();
      }
      this.medications.push(group);
    }
  });
}


  loadFamilyHistory(patientId: number): void {
    this.familyHistoryService.getFamilyHistoryById(patientId, this.stage).subscribe((res: any) => {
      const data = res?.data;
      //this.stage = data.stage;
      if (data) {
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
              medicationID: med.medicationId || '',
              medicationName: med.medicationName || '',
              dose: med.dose || '',
              frequency: med.frequency || ''
            }));
          });
        }
      }
    });
  }

  medval: any[] = [];
  getmedication(patientId: number, stage: number) {
    const url = API_URLS.MEDICATION_GET_BY_ID
      .replace('{patientId}', patientId.toString())
      .replace('{stage}', stage.toString());

    this.http.httpGet(url).subscribe((res: any) => {
      if (res?.type === 'S') {
        this.medval = res.data;
      }
    });
  }


  validatefields(): boolean {
    const fhGred = this.familyhistoryForm.get('fH_GRED')?.value;
    const fhRemark = this.familyhistoryForm.get('fH_Remark')?.value;
    const fhEGC = this.familyhistoryForm.get('fH_EGC')?.value;
    const fhEGCRemark = this.familyhistoryForm.get('fH_EGCRemark')?.value;
    const ppiUsage = this.familyhistoryForm.get('ppiusage')?.value;

    if (this.familyhistoryForm.get('fH_GRED')?.value === '') {
      alert('Select Family History of GERD');
      return false;
    }


   

    if (fhGred === 'yes' && (!fhRemark || fhRemark.trim() === '')) {
      alert(' Please enter Family History of GERD Remark.');
      return false;
    }


    if (fhEGC === 'yes' && (!fhEGCRemark || fhEGCRemark.trim() === '')) {
      alert(' Please enter Esophago-Gastric Cancer Remark.');
      return false;
    }


     if (this.familyhistoryForm.get('ppiusage')?.value === '') {
      alert('Select Usage of PPI');
      return false;
    }
    if (ppiUsage === 'yes') {
      for (let i = 0; i < this.medications.length; i++) {
        const med = this.medications.at(i);
        if (!med.get('medicationName')?.value || !med.get('dose')?.value || !med.get('frequency')?.value) {
          alert(` Please fill Medication Name, Dose, and Frequency in row ${i + 1}.`);
          return false;
        }
      }
    }

    return true;
  }

async Submit() {
  // Stop submission if validation fails
  if (!this.validatefields()) return;

  let user: any = localStorage.getItem('doctor');
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

  this.http.httpPost(API_URLS.FAMILY_HISTORY_SAVE, param).subscribe(async (res: any) => {
    if (res.type === 'S') {
      this.isSaved = true;
      alert('Family history saved successfully!');

      const ppiUsage = this.familyhistoryForm.get('ppiusage')?.value;

      const medicationRequests = this.medications.controls.map((ctrl) => {
        const medValue = ctrl.value;

        const medParam = {
          Flag: 'I',
          patientId: this.patientId,
          Stage: this.stage,
          MedicationID: medValue.medicationId || 0,
          GHID: 0,
          MedicationName: ppiUsage === 'yes' && medValue.medicationName?.trim() ? medValue.medicationName.toString() : null,
          Dose: ppiUsage === 'yes' && medValue.dose?.trim() ? medValue.dose.toString() : null,
          Frequency: ppiUsage === 'yes' && medValue.frequency?.trim() ? medValue.frequency.toString() : null,
          Molecule: '',
          CreatedBy: this.userData?.doctorId,
        };

        return this.http.httpPost(API_URLS.MedicationY_SAVE, medParam).toPromise();
      });

      try {
        await Promise.all(medicationRequests);
        // alert('Medications saved successfully!');
      } catch (error) {
        alert('Error saving medications!');
      }

    } else {
      alert('Error saving family history!');
    }
  });
}

  onNext() {
    const currentUrl = this.router.url;


  }

  blockInvalidKeys(event: KeyboardEvent) {
    if (['e', 'E', '+',].includes(event.key)) {
      event.preventDefault();
    }

  }


  preventNegative(event: any) {

    if (event.target.value < 0) {
      event.target.value = 0; // reset to 0 if negative
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
        stage: this.stage,
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

  allowOnlyText(event: KeyboardEvent) {
    const pattern = /[a-zA-Z ]/;
    const inputChar = String.fromCharCode(event.charCode);
    if (!pattern.test(inputChar)) {
      event.preventDefault();
    }
  }
}
