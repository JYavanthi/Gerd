import { Component } from '@angular/core';
import { FormBuilder } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { FormvalidationService } from '../../formvalidation.service';
import { HttpserviceService } from '../../httpservice.service';
import { API_URLS } from '../../shared/API-URLs';
import { HttpClient } from '@angular/common/http';
import { PatientService } from '../../Services/patient.service';

@Component({
  selector: 'app-management',
  templateUrl: './management.component.html',
  styleUrl: './management.component.css'
})
export class ManagamentComponent2 {
  patientId: number | null = null;
  doctorId: number | null = null;
  isViewMode = false;
  isFollowUp: boolean = false;
  showConfirmation: boolean = false;
 tabId = 1;
 stage: number = 1;
  activeTabId: any = 2;

  constructor(
    private formValidation: FormvalidationService,
    private http: HttpserviceService,
    private router: Router,
    public route: ActivatedRoute,
    private httpClient: HttpClient,
    private patientService: PatientService

  ) {

  }
  ngOnInit(): void {
    console.log("TABID", this.tabId)
         const state = history.state;
    this.tabId = state?.tabId ?? 1;
    this.patientId = state?.patientId;  
    const currentUrl = this.router.url;
    this.isFollowUp = currentUrl.includes('followUpOne') || currentUrl.includes('followUpTwo');
  if (this.patientService.getPatientId()) {
    this.getManagementDataById(this.patientService.getPatientId());
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


getManagementDataById(patientId: number) {
  const url = `https://gerdregistryofindia.com/GERD/api/Management/GetManagementById/${this.patientService.getPatientId()}`;
  this.httpClient.get<any>(url).subscribe({
    next: (res) => {
      if (res && res.data) {
        const data = res.data;

        // Populate lifestyle checkboxes from bitmask
        const bitmask = data.lifestyleRecommendations ?? 0;
        this.updateLifestyleFromBitmask(bitmask);

        // Populate drug therapy fields
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


 
 Submit() {
  const patientId = this.patientService.getPatientId();

  // Step 1: Get current stage from backend
  this.http.httpGet(`${API_URLS.GET_CURRENT_STAGE}/${patientId}`).subscribe((res: any) => {
    let stageToSend = 1;

    const currentStage = res?.currentStage ?? 0;

    // Step 2: Determine next stage based on current stage
    if (currentStage === 0) stageToSend = 1;
    else if (currentStage === 1) stageToSend = 3;
    else if (currentStage === 3) stageToSend = 5;

    // Step 3: Prepare payload
    const param = {
      stage: stageToSend,
      flag: 'I',
      PatientID: patientId,
      lifestyleRecommendations: this.getLifestyleBitmask(),
      createdBy: 0,

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

    // Step 4: Call MANAGEMENT_SAVE API
    this.http.httpPost(API_URLS.MANAGEMENT_SAVE, param).subscribe((res: any) => {
      if (res.type === 'S') {
        console.log('✅ Management Saved. Now calling CompleteCase API.');
        this.stageUpdate();
        // Optionally, call CompleteCase API here if needed

      } else {
        this.formValidation.showAlert('Error!!', 'danger');
      }
    });
  });
}

stageUpdate(){
   // Call CompleteCase API after successful save
      const completeCasePayload = {
        patientId: this.patientService.getPatientId()
      };
   this.http.httpPost('/Management/CompleteCase', completeCasePayload).subscribe(
        (finalRes: any) => {
          if (finalRes.type === 'S') {
            this.lifestyle = {};
            this.drugTherapy.forEach(drug => {
              drug.name = '';
              drug.dose = '';
              drug.frequency = '';
            });
          } 
        },
        error => {
          console.error('❌ Error calling CompleteCase:', error);
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

  onNext() {
    const currentUrl = this.router.url;
    const patientId = this.patientId;

    if (currentUrl.includes('follow-up-1')) {
      this.router.navigate(['/follow-up-1/managament'], {
    state: {
      tabId: this.tabId,
      patientId: this.patientId,
      isViewMode: this.isViewMode
    }
  });
    } else {
      this.router.navigate(['/follow-up-1/managament'], {
    state: {
      tabId: this.tabId,
      patientId: this.patientId,
      isViewMode: this.isViewMode
    }
  });
    }
  }

onTabClick(tabId: number) {
  this.activeTabId = tabId;
  console.log('Tab id', this.activeTabId);
}
  onSaveClick() {
  }

  cancelSave() {
    this.showConfirmation = false;
  }

  confirmSave() {
    this.showConfirmation = false;
    this.Submit();
    this.router.navigate(['/dashboard']);
  }

goback(){
    this.router.navigate([`/assessment`], {
    state: {
      tabId: this.tabId,
      patientId: this.patientId,
      isViewMode: this.isViewMode
    }
  });

}
  back() {
  this.router.navigate([`/followUpOne/assessment/${this.patientService.getPatientId()}`], {
    state: {
      tabId: this.tabId,
      patientId: this.patientService.getPatientId(),
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
