import { Component, Input, SimpleChanges } from '@angular/core';
import { FormBuilder } from '@angular/forms';
import { ActivatedRoute, Params, Router } from '@angular/router';
import { FormvalidationService } from '../formvalidation.service';
import { HttpserviceService } from '../httpservice.service';
import { API_URLS } from '../shared/API-URLs';
import { HttpClient } from '@angular/common/http';
import { PatientService } from '../Services/patient.service';

@Component({
  selector: 'app-managament',
  templateUrl: './managament.component.html',
  styleUrl: './managament.component.css'
})
export class ManagamentComponent {
  @Input() patientId: number | null | undefined = null
  // @Input() patientId: number | null = null;
  @Input() doctorId: number | null = null;
  isViewMode = false;
  isFollowUp: boolean = false;
  showConfirmation: boolean = false;
  tabId = 1;
  @Input() stage: number = 0;
  @Input() data: any;
  @Input() isPrintMode: boolean = false;
  isSaved: boolean = false;

  currentStage: any;
  userData: any
  newStage: any

  bLSubmitted: any | null = null;
  fU1Submitted: any |null = null
  fU2Submitted:any |null = null

  constructor(
    private formValidation: FormvalidationService,
    private http: HttpserviceService,
    private router: Router,
    public route: ActivatedRoute,
    private httpClient: HttpClient,
    private patientService: PatientService

  ) {

    let user: any = localStorage.getItem('doctor')
    this.userData = JSON.parse(user);

    const navigation = this.router.getCurrentNavigation();
    // const state = navigation?.extras?.state;

    // if (state) {
    //   this.newStage = state['stage']
    // }
    // this.route.queryParams.subscribe((params: Params) => {
    //   if (!this.patientId) {
    //     this.patientId = this.patientService.getPatientId();
    //   }

    //   this.stage = Number(this.route.snapshot.params['stage']);
    //   this.doctorId = this.userData?.doctorId;

     // console.log("👉 PatientId resolved:", this.patientId);
   // });
  }


  ngOnChanges(changes: SimpleChanges) {
    if (changes['data'] && this.data) {
      this.patchManagement(this.data);
    }
  }
  private patchManagement(data: any) {
    if (!data) return;

    // Lifestyle (bitmask → checkboxes)
    const bitmask = data.lifestyleRecommendations ?? 0;
    this.updateLifestyleFromBitmask(bitmask);

    // Drug Therapy
    this.drugTherapy[0] = { class: 'PPI', name: data.ppiMedicationName, dose: data.ppiDose, frequency: data.ppiFrequency };
    this.drugTherapy[1] = { class: 'Combination of PPI + Prokinetics', name: data.prokineticsMedicationName, dose: data.prokineticsDose, frequency: data.prokineticsFrequency };
    this.drugTherapy[2] = { class: 'Sucralfate', name: data.sucralfateMedicationName, dose: data.sucralfateDose, frequency: data.sucralfateFrequency };
    this.drugTherapy[3] = { class: 'Alginate', name: data.alginateMedicationName, dose: data.alginateDose, frequency: data.alginateFrequency };
    this.drugTherapy[4] = { class: 'H₂ Blockers', name: data.h2blockersMedicationName, dose: data.h2blockersDose, frequency: data.h2blockersFrequency };
    this.drugTherapy[5] = { class: 'H₂ Blockers combinations', name: data.h2blockersCMedicationName, dose: data.h2blockersCDose, frequency: data.h2blockersCFrequency };
    this.drugTherapy[6] = { class: 'PCAB', name: data.pcabMedicationName, dose: data.pcabDose, frequency: data.pcabFrequency };
    this.drugTherapy[7] = { class: 'Any others', name: data.othersMedicationName, dose: data.othersDose, frequency: data.othersFrequency };

    console.log("Patched Management data", this.drugTherapy, this.lifestyle);
  }

  ngOnInit(): void {
    this.patientId = Number(this.route.snapshot.params['patientId']);
    this.stage = Number(this.route.snapshot.params['stage'] || 0);
    this.doctorId = this.patientService.getDoctorId();

    console.log("👉 Final resolved PatientId:", this.patientId, "Stage:", this.stage);

    if (this.patientId) {
      this.getManagementDataById(this.patientId, this.stage);
    } 
  }
  lifestyleItems = [
    'Diet modification',
    'Moderation of alcohol',
    'Weight loss',
    'Regular exercise',
    'Stop Tobacco use'
  ];
  lifestyle: { [key: string]: boolean } = {};
  drugTherapy = [
    { class: 'PPI', name: '', dose: '', frequency: '' },
    { class: 'Combination of PPI + Prokinetics', name: '', dose: '', frequency: '' },
    { class: 'Sucralfate', name: '', dose: '', frequency: '' },
    { class: 'Alginate', name: '', dose: '', frequency: '' },
    { class: 'H₂ Blockers', name: '', dose: '', frequency: '' },
    { class: 'H₂ Blockers combinations', name: '', dose: '', frequency: '' },
    { class: 'PCAB', name: '', dose: '', frequency: '' },
    { class: 'Any others', name: '', dose: '', frequency: '' },
  ];

  popup() {
    if (this.getLifestyleBitmask() === 0) {
      alert('Select atleast one Life Sytle Recomendation');
      return;
    }
    this.verifyMed();
    if (!this.valflag) {
      alert('Enter atleast one Drug Therapy Advise');
      return;
    }
    this.showConfirmation = true;

  }
  getManagementDataById(patientId: number, stage: number) {
    const url = `${API_URLS.BASE_URL}/Management/GetManagementById/${patientId}/${stage}`;
    this.httpClient.get<any>(url).subscribe({
      next: (res) => {
        if (res && res.data) {
          const data = res.data;
          this.stage = data.stage
          const bitmask = data.lifestyleRecommendations ?? 0;
          this.updateLifestyleFromBitmask(bitmask);

          this.drugTherapy[0] = { class: 'PPI', name: data.ppiMedicationName, dose: data.ppiDose, frequency: data.ppiFrequency };
          this.drugTherapy[1] = { class: 'Combination of PPI + Prokinetics', name: data.prokineticsMedicationName, dose: data.prokineticsDose, frequency: data.prokineticsFrequency };
          this.drugTherapy[2] = { class: 'Sucralfate', name: data.sucralfateMedicationName, dose: data.sucralfateDose, frequency: data.sucralfateFrequency };
          this.drugTherapy[3] = { class: 'Alginate', name: data.alginateMedicationName, dose: data.alginateDose, frequency: data.alginateFrequency };
          this.drugTherapy[4] = { class: 'H₂ Blockers', name: data.h2blockersMedicationName, dose: data.h2blockersDose, frequency: data.h2blockersFrequency };
          this.drugTherapy[5] = { class: 'H₂ Blockers combinations', name: data.h2blockersCMedicationName, dose: data.h2blockersCDose, frequency: data.h2blockersCFrequency };
          this.drugTherapy[6] = { class: 'PCAB', name: data.pcabMedicationName, dose: data.pcabDose, frequency: data.pcabFrequency };
          this.drugTherapy[7] = { class: 'Any others', name: data.othersMedicationName, dose: data.othersDose, frequency: data.othersFrequency };
        }
        console.log("✅ Fetched and patched Management data", res.data);
      },
      error: (err) => {
        console.error('❌ Failed to fetch management data:', err);
      }
    });
  }
  updateLifestyleFromBitmask(bitmask: number) {
    const bitMap: Record<string, number> = {
      'Diet modification': 1,
      'Moderation of alcohol': 2,
      'Weight loss': 4,
      'Regular exercise': 8,
      'Stop Tobacco use': 16
    };

    for (const key in bitMap) {
      this.lifestyle[key] = (bitmask & bitMap[key]) !== 0;
    }
  }

  valflag: boolean = false;
  verifyMed() {
    for (let i = 0; i <= 7; i++) {
      const drug = this.drugTherapy[i];

      if (drug) {
        // if any field is filled, then require all three
        if (
          (drug.name && drug.name.trim() !== '') &&
          (drug.dose && drug.dose.trim() !== '') &&
          (drug.frequency && drug.frequency.trim() !== '')
        ) {
          this.valflag = true;
          return;
        }
      }
      else {
        this.valflag = false;
      }

    }

  }

  ptnstage: number = 0;
  Submit() {
    // const patientId = this.patientService.getPatientId();

    if (this.stage === 1) this.ptnstage = 2;
    else if (this.stage === 3) this.ptnstage = 4;
    else if (this.stage === 0) this.ptnstage = 0;
    else this.ptnstage = this.stage;



    const param = {
      stage: this.ptnstage,
      flag: 'I',
      PatientID: this.patientId,
      lifestyleRecommendations: this.getLifestyleBitmask(),
      createdBy: this.doctorId,
      PPI_Medication_Name: this.drugTherapy[0].name,
      PPI_Dose: this.drugTherapy[0].dose,
      PPI_Frequency: this.drugTherapy[0].frequency,

      Prokinetics_Medication_Name: this.drugTherapy[1].name,
      Prokinetics_Dose: this.drugTherapy[1].dose,
      Prokinetics_Frequency: this.drugTherapy[1].frequency,

      Sucralfate_Medication_Name: this.drugTherapy[2].name,
      Sucralfate_Dose: this.drugTherapy[2].dose,
      Sucralfate_Frequency: this.drugTherapy[2].frequency,

      Alginate_Medication_Name: this.drugTherapy[3].name,
      Alginate_Dose: this.drugTherapy[3].dose,
      Alginate_Frequency: this.drugTherapy[3].frequency,

      H2Blockers_Medication_Name: this.drugTherapy[4].name,
      H2Blockers_Dose: this.drugTherapy[4].dose,
      H2Blockers_Frequency: this.drugTherapy[4].frequency,

      H2BlockersC_Medication_Name: this.drugTherapy[5].name,
      H2BlockersC_Dose: this.drugTherapy[5].dose,
      H2BlockersC_Frequency: this.drugTherapy[5].frequency,

      PCAB_Medication_Name: this.drugTherapy[6].name,
      PCAB_Dose: this.drugTherapy[6].dose,
      PCAB_Frequency: this.drugTherapy[6].frequency,

      others_Medication_Name: this.drugTherapy[7].name,
      others_Dose: this.drugTherapy[7].dose,
      others_Frequency: this.drugTherapy[7].frequency,
    };

    this.http.httpPost(API_URLS.MANAGEMENT_SAVE, param).subscribe((res: any) => {
      if (res.type === 'S') {
        console.log('✅ Management Saved. Now calling CompleteCase API.');


      } else {
        this.formValidation.showAlert('Error!!', 'danger');
      }
    });
  }
  stageUpdate() {
    // const PatientID = this.patientService.getPatientId();
    const currentDateTime = new Date().toISOString(); // "YYYY-MM-DD HH:mm:ss"
    if (this.stage === 1) this.ptnstage = 2;
    else if (this.stage === 3) this.ptnstage = 4;
    else if (this.stage === 0) this.ptnstage = 0, this.bLSubmitted = currentDateTime;
    else if (this.stage === 2) this.fU1Submitted = currentDateTime;
    else if (this.stage === 4) this.fU2Submitted = currentDateTime;
    else this.ptnstage = this.stage;

    const completeCasePayload = {
      // patientId: PatientID,
      patientId: this.patientId,
      stage: this.ptnstage,
      createdby: this.doctorId,
      bLSubmitted: this.bLSubmitted || null,
      fU1Submitted: this.fU1Submitted || null,  
      fU2Submitted: this.fU2Submitted || null,
  
    };

    this.http.httpPost(API_URLS.MANAGEMENT_CompleteCase, completeCasePayload).subscribe(
      (finalRes: any) => {
        if (finalRes.type === 'S') {
          // Clear data if needed
          this.lifestyle = {};
          this.drugTherapy.forEach(drug => {
            drug.name = '';
            drug.dose = '';
            drug.frequency = '';
          });

          // Navigate and force reload after short delay
          setTimeout(() => {
            this.router.navigate(['/dashboard']).then(() => {
              window.location.reload();
            });
          }, 300);
        }
      },
      error => {
        console.error('Error calling CompleteCase:', error);
        alert('Submitted, but failed to update stage. Try again later.');
      }
    );
  }
  getLifestyleBitmask(): number {
    const bitMap: Record<string, number> = {
      'Diet modification': 1,
      'Moderation of alcohol': 2,
      'Weight loss': 4,
      'Regular exercise': 8,
      'Stop Tobacco use': 16
    };

    let value = 0;
    for (const key in this.lifestyle) {
      if (this.lifestyle[key]) {
        value |= bitMap[key] || 0;
      }
    }
    return value;
  }


  cancelSave() {
    this.showConfirmation = false;
    this.Submit();

  }

  confirmSave() {

    if (this.getLifestyleBitmask() === 0) {
      alert('Enter Life Sytle Recomendation');
      return;
    }
    this.showConfirmation = false;
    this.Submit();
    this.stageUpdate();
    this.router.navigate(['/dashboard']);
  }


  // goback(){
  //   const patientId = this.patientId;

  //   if (this.stage > 1) {
  //     this.router.navigate([`/assessment/${patientId}/${this.stage}`], {
  //       state: {
  //         tabId: this.tabId,
  //         patientId: this.patientService.getPatientId(),
  //         isViewMode: this.isViewMode,
  //         fromNavigation: true


  //       }
  //     });
  //   } else {
  //     // Navigate to Diagnosis
  //       this.router.navigate([`/diagnosis/${this.patientId}/${this.stage}`], {
  //       state: {
  //         tabId: this.tabId,
  //         patientId: this.patientId,
  //         isViewMode: this.isViewMode,
  //         fromNavigation: true
  //       }
  //     });
  //   }
  // }

  goback() {
    const patientId = this.patientId || this.patientService.getPatientId();

    if (!patientId) {
      console.error("❌ No patientId found when navigating back!");
      return;
    }

    if (this.stage > 1) {
      this.router.navigate([`/assessment/${patientId}/${this.stage}`], {
        state: {
          tabId: this.tabId,
          patientId: patientId,
          stage: this.stage,
          isViewMode: this.isViewMode,
          fromNavigation: true
        }
      });
    } else {
      this.router.navigate([`/diagnosis/${patientId}/${this.stage}`], {
        state: {
          tabId: this.tabId,
          patientId: patientId,
          stage: this.stage,
          isViewMode: this.isViewMode,
          fromNavigation: true
        }
      });
    }
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

  blockInvalidKeys(event: KeyboardEvent) {
    if ([, 'E', '+'].includes(event.key)) {
      event.preventDefault();
    }
  }

  preventNegative(event: any) {

    if (event.target.value < 0) {
      event.target.value = 0; // reset to 0 if negative
    }
  }
  allowOnlyText(event: KeyboardEvent) {
    const pattern = /[a-zA-Z ]/;
    const inputChar = String.fromCharCode(event.charCode);
    if (!pattern.test(inputChar)) {
      event.preventDefault();
    }
  }
}


