// import { HttpClient } from '@angular/common/http';
// import { Component, OnInit } from '@angular/core';
// import { ActivatedRoute, Router } from '@angular/router';
// import { FormvalidationService } from '../formvalidation.service';
// import { HttpserviceService } from '../httpservice.service';
// import { API_URLS } from '../shared/API-URLs';
// import { FormBuilder, FormGroup, Validators } from '@angular/forms';
// import { PatientService } from '../Services/patient.service';
// import { CurrentMedicationsService } from '../Services/current-medications.service';

// @Component({
//   selector: 'app-current-medications',
//   templateUrl: './current-medications.component.html',
//   styleUrls: ['./current-medications.component.css']
// })
// export class CurrentMedicationsComponent implements OnInit {
//   medicationsForm: FormGroup;
//   patientId: number | null = null;
//    doctorId: number | null = null;
//   tabId: number = 1;
//   stage: number = 0;
//   isViewMode = false;
//     isFollowUp = false;


//   constructor(
//     private fb: FormBuilder,
//     private formValidation: FormvalidationService,
//     private http: HttpserviceService,
//     private router: Router,
//     private route: ActivatedRoute,
//     private patientService: PatientService,
//     private currentMedicationsService: CurrentMedicationsService
//   ) {
//     this.medicationsForm = this.fb.group({
//       patientId: [''],
//       nsaidsMolecule: [''],
//       nsaidsDose: [''],
//       nsaidsFrequency: [''],
//       bisphosphonatesMolecule: [''],
//       bisphosphonatesDose: [''],
//       bisphosphonatesFrequency: [''],
//       steroidsMolecule: [''],
//       steroidsDose: [''],
//       steroidsFrequency: [''],
//       antiplateletMolecule: [''],
//       antiplateletDose: [''],
//       antiplateletFrequency: [''],
//       othersMolecule: [''],
//       othersDose: [''],
//       othersFrequency: [''],
//       createdBy: [0]
//     });
//   }

//   // ngOnInit(): void {
//   //   const state = history.state;
//   //   this.patientId = state?.patientId ?? null;
//   //   this.tabId = state?.tabId ?? 1;
//   //   this.stage = state?.stage ?? 0;

//   //   if (this.patientService.getPatientId()) {
//   //     this.medicationsForm.patchValue({ patientId: this.patientId });
//   //     this.fetchCurrentMedicationsData(this.patientService.getPatientId());
//   //     // this.myFetch();
//   //   }
//   // }
//   ngOnInit(): void {
//     this.tabId = history.state?.tabId ?? 1;
//     this.isViewMode = history.state?.isViewMode ?? false;

//     // First check for patientId in queryParams
//     this.route.queryParams.subscribe(params => {
//       if (params['patientId']) {
//         this.patientId = +params['patientId'];
//         this.patientService.setPatientId(this.patientId); // update service
//       } else {
//         this.patientId = this.patientService.getPatientId();
//       }

//       if (params['doctorId']) {
//         this.doctorId = +params['doctorId'];
//         this.patientService.setDoctorId(this.doctorId);
//       } else {
//         this.doctorId = this.patientService.getDoctorId();
//       }

//       const currentUrl = this.router.url;
//       console.log("url",currentUrl)
//       this.isFollowUp = currentUrl.includes('follow-up-1') || currentUrl.includes('follow-up-2');


// if (currentUrl === '/follow-up-1/current-medicaton' && this.patientService.getPatientId()) {        
//   this.fetchCurrentMedicationsData(this.patientService.getPatientId());
//       } else {
//         console.error('Patient ID not found');
//       }
//     });
//   }

//   // private baseUrl = 'http://localhost:5058';
//   // myFetch = () => {
//   //   fetch("https://gerdregistryofindia.com/GERD/api/CurrentMedication/GetCurrentMedicationById/"+(this.patientId)).then(response=>response.json()).then(info=>{
//   //     if(info.patientId == null || info.patientId === ""){
//   //       alert("No data fetched");
//   //     }
//   //     else{
//   //       nsaidsMolecule: info.NSAIDs_Molecule
//   //       this.medicationsForm.patchValue({
//   //     }
//   //   })
//   // }

//   fetchCurrentMedicationsData(patientId: number): void {
//     this.currentMedicationsService.getCurrentMedicationById(patientId).subscribe({
//       next: (res: any) => {
//         if (res.type === 'S' && res.data) {
//           const data = res.data;
//           console.log('✅ Current Medications data:', data);

//           this.medicationsForm.patchValue({
//             nsaidsMolecule: data.NSAIDs_Molecule,
//             nsaidsDose: data.NSAIDs_Dose,
//             nsaidsFrequency: data.NSAIDs_Frequency,
//             bisphosphonatesMolecule: data.Bisphosphonates_Molecule,
//             bisphosphonatesDose: data.Bisphosphonates_Dose,
//             bisphosphonatesFrequency: data.Bisphosphonates_Frequency,
//             steroidsMolecule: data.Steroids_Molecule,
//             steroidsDose: data.Steroids_Dose,
//             steroidsFrequency: data.Steroids_Frequency,
//             antiplateletMolecule: data.Antiplatelet_Molecule,
//             antiplateletDose: data.Antiplatelet_Dose,
//             antiplateletFrequency: data.Antiplatelet_Frequency,
//             othersMolecule: data.Others_Molecule,
//             othersDose: data.Others_Dose,
//             othersFrequency: data.Others_Frequency,
//             createdBy: data.CreatedBy
//           });

//           this.patientId = data.PatientId;
//         } else {
//           console.warn('⚠️ No Current Medications data found.');
//         }
//       },
//       error: (err: any) => {
//         console.error('❌ Error fetching Current Medications data:', err);
//       }
//     });
//   }

//   onSubmit(): void {
//     if (!this.formValidation.validateForm(this.medicationsForm)) {
//       this.medicationsForm.markAllAsTouched();
//       return;
//     }

//     const param = {
//       flag: "I",
//       stage: this.stage,
//       id: 0,
//       PatientId: this.patientService.getPatientId(),
//       NSAIDs_Molecule: this.medicationsForm.controls['nsaidsMolecule'].value,
//       NSAIDs_Dose: this.medicationsForm.controls['nsaidsDose'].value,
//       NSAIDs_Frequency: this.medicationsForm.controls['nsaidsFrequency'].value,
//       Bisphosphonates_Molecule: this.medicationsForm.controls['bisphosphonatesMolecule'].value,
//       Bisphosphonates_Dose: this.medicationsForm.controls['bisphosphonatesDose'].value,
//       Bisphosphonates_Frequency: this.medicationsForm.controls['bisphosphonatesFrequency'].value,
//       Steroids_Molecule: this.medicationsForm.controls['steroidsMolecule'].value,
//       Steroids_Dose: this.medicationsForm.controls['steroidsDose'].value,
//       Steroids_Frequency: this.medicationsForm.controls['steroidsFrequency'].value,
//       Antiplatelet_Molecule: this.medicationsForm.controls['antiplateletMolecule'].value,
//       Antiplatelet_Dose: this.medicationsForm.controls['antiplateletDose'].value,
//       Antiplatelet_Frequency: this.medicationsForm.controls['antiplateletFrequency'].value,
//       Others_Molecule: this.medicationsForm.controls['othersMolecule'].value,
//       Others_Dose: this.medicationsForm.controls['othersDose'].value,
//       Others_Frequency: this.medicationsForm.controls['othersFrequency'].value,
//       CreatedBy: this.medicationsForm.controls['createdBy'].value
//     };

//     this.http.httpPost(API_URLS.CURRENT_MEDICATION_SAVE, param).subscribe({
//       next: (res: any) => {
//         if (res.type === 'S') {

//                     this.http.httpGet('/PatientReg/GetPatient').subscribe((getRes: any) => {
//             if (getRes.type === 'S' && getRes.data?.length > 0) {
//               const latestPatient = getRes.data[getRes.data.length - 1];
//               const updatedPatientId = latestPatient.patientId;

//               this.patientService.setPatientId(updatedPatientId);
//               this.patientService.setDoctorId(param.CreatedBy);

//               this.formValidation.showAlert('Chief complaint saved successfully', 'success');
//               this.router.navigate([], {
//                 queryParams: {
//                   patientId: updatedPatientId,
//                   doctorId: param.CreatedBy
//                 }
//               });
//             } else {
//               this.formValidation.showAlert('Unable to fetch Patient ID after save', 'danger');
//             }
//           });

//           alert('Successfully Submitted');
//           this.medicationsForm.reset();
//         } else {
//           this.formValidation.showAlert('Error!!', 'danger');
//         }
//       },
//       error: () => {
//         this.formValidation.showAlert('Error saving data.', 'danger');
//       }
//     });
//   }

//   OnNext(): void {
//     this.router.navigate(['/medical-examination'], {
//       state: {
//         patientId: this.patientService.getPatientId(),
//         tabId: this.tabId,
//         stage: this.stage
//       }
//     });
//   }

//   goback(): void {
//     this.router.navigate(['/history-endoscopy'], {
//       state: {
//         patientId: this.patientId,
//         tabId: this.tabId,
//         stage: this.stage
//       }
//     });
//   }
// }

import { Component, OnInit } from '@angular/core';
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
    this.tabId = history.state?.tabId ?? 1;
    this.stage = history.state?.stage ?? 0;
    this.isViewMode = history.state?.isViewMode ?? false;

    this.route.queryParams.subscribe(params => {
      this.patientId = params['patientId'] ? +params['patientId'] : this.patientService.getPatientId();
      this.doctorId = params['doctorId'] ? +params['doctorId'] : this.patientService.getDoctorId();

      if (this.patientId) this.patientService.setPatientId(this.patientId);
      if (this.doctorId) this.patientService.setDoctorId(this.doctorId);

    const currentUrl = this.router.url;
      console.log("url",currentUrl)
      this.isFollowUp = currentUrl.includes('follow-up-1') || currentUrl.includes('follow-up-2');


if (currentUrl === '/follow-up-1/current-medicaton' && this.patientService.getPatientId()) {         
        this.fetchCurrentMedicationsData(this.patientId);
      } else {
        console.warn('⚠️ Patient ID missing.');
      }
    });

    const cachedData = this.patientService.getfamalyhistoryData();
    if (cachedData) {
      this.medicationsForm.patchValue(cachedData);
    } else {
      this.fetchCurrentMedicationsData(this.patientService.getPatientId());

    }
  }

  fetchCurrentMedicationsData(patientId: number): void {
  this.currentMedicationsService.getCurrentMedicationById(patientId).subscribe({
    next: (res: any) => {
      console.log('Medication response:', res); // Debugging

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
          createdBy: med.createdBy
        });
      }
    },
    error: err => {
      console.error('❌ Error fetching medications:', err);
    }
  });
}


  onSubmit(): void {
    if (!this.formValidation.validateForm(this.medicationsForm)) {
      this.medicationsForm.markAllAsTouched();
      return;
    }

    const param = {
      flag: 'I',
      stage: this.stage,
      id: 0,
      PatientId: this.patientService.getPatientId(),
      NSAIDs_Molecule: this.medicationsForm.value.nsaidsMolecule,
      NSAIDs_Dose: this.medicationsForm.value.nsaidsDose,
      NSAIDs_Frequency: this.medicationsForm.value.nsaidsFrequency,
      Bisphosphonates_Molecule: this.medicationsForm.value.bisphosphonatesMolecule,
      Bisphosphonates_Dose: this.medicationsForm.value.bisphosphonatesDose,
      Bisphosphonates_Frequency: this.medicationsForm.value.bisphosphonatesFrequency,
      Steroids_Molecule: this.medicationsForm.value.steroidsMolecule,
      Steroids_Dose: this.medicationsForm.value.steroidsDose,
      Steroids_Frequency: this.medicationsForm.value.steroidsFrequency,
      Antiplatelet_Molecule: this.medicationsForm.value.antiplateletMolecule,
      Antiplatelet_Dose: this.medicationsForm.value.antiplateletDose,
      Antiplatelet_Frequency: this.medicationsForm.value.antiplateletFrequency,
      Others_Molecule: this.medicationsForm.value.othersMolecule,
      Others_Dose: this.medicationsForm.value.othersDose,
      Others_Frequency: this.medicationsForm.value.othersFrequency,
      CreatedBy: this.medicationsForm.value.createdBy
    };

    this.http.httpPost(API_URLS.CURRENT_MEDICATION_SAVE, param).subscribe({
      next: (res: any) => {
        if (res.type === 'S') {
          this.http.httpGet('/PatientReg/GetPatient').subscribe((getRes: any) => {
            if (getRes.type === 'S' && getRes.data?.length > 0) {
              const latest = getRes.data[getRes.data.length - 1];
              this.patientService.setPatientId(latest.patientId);
              this.patientService.setDoctorId(param.CreatedBy);

              this.formValidation.showAlert('Medications saved successfully', 'success');
              this.router.navigate([], {
                queryParams: {
                  patientId: latest.patientId,
                  doctorId: param.CreatedBy
                }
              });
            } else {
              this.formValidation.showAlert('Patient ID fetch failed', 'danger');
            }
          });
          this.medicationsForm.reset();
        } else {
          this.formValidation.showAlert('Save failed', 'danger');
        }
      },
      error: () => {
        this.formValidation.showAlert('Error during save', 'danger');
      }
    });
  }

  OnNext(): void {
    this.router.navigate(['/medical-examination'], {
      state: {
        patientId: this.patientService.getPatientId(),
        tabId: this.tabId,
        stage: this.stage
      }
    });
  }

  goback(): void {
    this.router.navigate(['/history-endoscopy'], {
      state: {
        patientId: this.patientId,
        tabId: this.tabId,
        stage: this.stage
      }
    });
  }
}
