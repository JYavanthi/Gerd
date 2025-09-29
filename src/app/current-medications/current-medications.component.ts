

import { Component, Input, OnInit, SimpleChanges } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { HttpserviceService } from '../httpservice.service';
import { FormvalidationService } from '../formvalidation.service';
import { CurrentMedicationsService } from '../Services/current-medications.service';
import { PatientService } from '../Services/patient.service';
import { API_URLS } from '../shared/API-URLs';

@Component({
  selector: 'app-current-medications',
  templateUrl: './current-medications.component.html',
  styleUrls: ['./current-medications.component.css']
})
export class CurrentMedicationsComponent implements OnInit {
  medicationsForm: FormGroup;
  patientId: number | null = null;
  doctorId: number | null = null;
  tabId: number = 1;
  stage: number = 0;
  isViewMode = false;
  isFollowUp = false;
    isSaved: boolean = false;
    @Input() isPrintMode = false;
      @Input() data: any;



  constructor(
    private fb: FormBuilder,
    private router: Router,
    private route: ActivatedRoute,
    private http: HttpserviceService,
    private formValidation: FormvalidationService,
    private patientService: PatientService,
    private currentMedicationsService: CurrentMedicationsService
  ) {
    this.medicationsForm = this.fb.group({
      patientId: [''],
      nsaidsMolecule: [''],
      nsaidsDose: [''],
      nsaidsFrequency: [''],
      bisphosphonatesMolecule: [''],
      bisphosphonatesDose: [''],
      bisphosphonatesFrequency: [''],
      steroidsMolecule: [''],
      steroidsDose: [''],
      steroidsFrequency: [''],
      antiplateletMolecule: [''],
      antiplateletDose: [''],
      antiplateletFrequency: [''],
      othersMolecule: [''],
      othersDose: [''],
      othersFrequency: [''],
      createdBy: [0]
    });
  }

  ngOnInit(): void {

    this.patientId = Number(this.route.snapshot.params['patientId']);
    this.stage = Number(this.route.snapshot.params['stage'])||0;
    this.doctorId=this.patientService.getDoctorId();
    const allowedWithoutSave = [1, 3, 5];
    if (allowedWithoutSave.includes(this.stage)) {
      this.isSaved = true;
    }    
    
    this.isViewMode = this.isViewMode ?? false;

        
    //this.doctorId =  this.patientService.getDoctorId();
    this.fetchCurrentMedicationsData(Number(this.patientId));

    // const cachedData = this.patientService.getfamalyhistoryData();
    // if (cachedData) {
    //   this.medicationsForm.patchValue(cachedData);
    // } else {
    //   this.fetchCurrentMedicationsData(this.patientService.getPatientId());

    // }
  }

  ngOnChanges(changes: SimpleChanges): void {
    if (changes['data'] && this.data) {
      this.patchForm(this.data);
    }
  }

  // Patch only the fields used in the form
  private patchForm(data: any): void {
    this.medicationsForm.patchValue({
      nsaidsMolecule: data.nsaidsMolecule,
      nsaidsDose: data.nsaidsDose,
      nsaidsFrequency: data.nsaidsFrequency,
      bisphosphonatesMolecule: data.bisphosphonatesMolecule,
      bisphosphonatesDose: data.bisphosphonatesDose,
      bisphosphonatesFrequency: data.bisphosphonatesFrequency,
      steroidsMolecule: data.steroidsMolecule,
      steroidsDose: data.steroidsDose,
      steroidsFrequency: data.steroidsFrequency,
      antiplateletMolecule: data.antiplateletMolecule,
      antiplateletDose: data.antiplateletDose,
      antiplateletFrequency: data.antiplateletFrequency,
      othersMolecule: data.othersMolecule,
      othersDose: data.othersDose,
      othersFrequency: data.othersFrequency,
      createdBy: this.doctorId
    });
  }

  fetchCurrentMedicationsData(patientId: number): void {
  this.currentMedicationsService.getCurrentMedicationById(patientId, this.stage).subscribe({
    next: (res: any) => {
    //   this.isSaved = true;
    //  console.log('Medication response:', res); // Debugging

      const med = Array.isArray(res.data) ? res.data[0] : res.data;

      if (res.type === 'S' && med) {
        this.medicationsForm.patchValue({
          nsaidsMolecule: med.nsaidsMolecule,
          nsaidsDose: med.nsaidsDose,
          nsaidsFrequency: med.nsaidsFrequency,
          bisphosphonatesMolecule: med.bisphosphonatesMolecule,
          bisphosphonatesDose: med.bisphosphonatesDose,
          bisphosphonatesFrequency: med.bisphosphonatesFrequency,
          steroidsMolecule: med.steroidsMolecule,
          steroidsDose: med.steroidsDose,
          steroidsFrequency: med.steroidsFrequency,
          antiplateletMolecule: med.antiplateletMolecule,
          antiplateletDose: med.antiplateletDose,
          antiplateletFrequency: med.antiplateletFrequency,
          othersMolecule: med.othersMolecule,
          othersDose: med.othersDose,
          othersFrequency: med.othersFrequency,
          createdBy: this.doctorId
        });
      }
    },
    error: err => {
      console.error('❌ Error fetching medications:', err);
    }
  });
}


  onSave(): void {
    if (!this.formValidation.validateForm(this.medicationsForm)) {
      this.medicationsForm.markAllAsTouched();
      return;
    }

    const param = {
      flag: 'I',
      stage: this.stage,
      id: 0,
      PatientId: this.patientId,
      NSAIDs_Molecule: this.medicationsForm.value.nsaidsMolecule,
      NSAIDs_Dose: this.medicationsForm.value.nsaidsDose,
      NSAIDs_Frequency: this.medicationsForm.value.nsaidsFrequency,
      bisphosphonates_Molecule: this.medicationsForm.value.bisphosphonatesMolecule,
      bisphosphonates_Dose: this.medicationsForm.value.bisphosphonatesDose,
      bisphosphonates_Frequency: this.medicationsForm.value.bisphosphonatesFrequency,
      Steroids_Molecule: this.medicationsForm.value.steroidsMolecule,
      Steroids_Dose: this.medicationsForm.value.steroidsDose,
      Steroids_Frequency: this.medicationsForm.value.steroidsFrequency,
      Antiplatelet_Molecule: this.medicationsForm.value.antiplateletMolecule,
      Antiplatelet_Dose: this.medicationsForm.value.antiplateletDose,
      Antiplatelet_Frequency: this.medicationsForm.value.antiplateletFrequency,
      Others_Molecule: this.medicationsForm.value.othersMolecule,
      Others_Dose: this.medicationsForm.value.othersDose,
      Others_Frequency: this.medicationsForm.value.othersFrequency,
      createdBy: this.doctorId
    };

    this.http.httpPost(API_URLS.CURRENT_MEDICATION_SAVE, param).subscribe({
      next: (res: any) => {
        if (res.type === 'S') {
           this.isSaved = true;
           alert('Saved Successfully');
          this.http.httpGet('/PatientReg/GetPatient').subscribe((getRes: any) => {
           
            if (getRes.type === 'S' && getRes.data?.length > 0) {
              // const latest = getRes.data[getRes.data.length - 1];
              // this.patientService.setPatientId(latest.patientId);
              // this.patientService.setDoctorId(param.CreatedBy);

              this.formValidation.showAlert('Medications saved successfully', 'success');
              // this.router.navigate([], {
              //   queryParams: {
              //     patientId: latest.patientId,
              //     doctorId: param.CreatedBy
              //   }
              // });
            } else {
              this.formValidation.showAlert('Patient ID fetch failed', 'danger');
            }
          });
          
        } else {
          this.formValidation.showAlert('Save failed', 'danger');
        }
      },
      error: () => {
        this.formValidation.showAlert('Error during save', 'danger');
      }
    });
  }

  // OnNext(): void {
  //   this.router.navigate(['/medical-examination'], {
  //     state: {
  //       patientId: this.patientService.getPatientId(),
  //       tabId: this.tabId,
  //       stage: this.stage
  //     }
  //   });
  // }

   onNext(){
    this.router.navigate([`/medical-examination/${this.patientId}/${this.stage}`], {
      state: {
        tabId: this.tabId,
        patientId: this.patientId,
        stage: this.stage,
        isViewMode: this.isViewMode
      }
    });

  }


  back() {
    this.router.navigate([`/history-endoscopy/${this.patientId}/${this.stage}`], {
      state: {
        tabId: this.tabId,
        patientId: this.patientId,
        stage: this.stage,
        isViewMode: this.isViewMode
      }
    });
  }
}
