import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root'
})
export class HistoryService {
  private data: any = {
    flag: 'U',
    patientHistoryID: null,
    createdBy: 1,
    modifiedBy: 0,
    // Initialize all fields to prevent undefined errors
    aD_Intake: '',
    aD_Frequency: '',
    aD_Quantity: '',
    aD_Duration: '',
    cF_Intake: '',
    cF_Frequency: '',
    cF_Quantity: '',
    cF_Duration: '',
    t_Intake: '',
    t_Frequency: '',
    t_Quantity: '',
    t_Duration: '',
    sF_Intake: '',
    sF_Frequency: '',
    sF_Quantity: '',
    sF_Duration: '',
    aH_Intake: '',
    aH_Frequency: '',
    aH_Quantity: '',
    aH_Duration: '',
    cS_Intake: '',
    cS_Frequency: '',
    cS_Quantity: '',
    cS_Duration: '',
    s_Intake: '',
    s_Frequency: '',
    s_Quantity: '',
    s_Duration: '',
    tB_Intake: '',
    tB_Frequency: '',
    tB_Quantity: '',
    tB_Duration: '',
    g_Name: '',
    g_Usage: '',
    g_Frequency: '',
    g_YearOfUsage: '',
    workingHours: '',
    jobType: '',
    duration: '',
    Past_History: '',
    diet: '',
    sleepApnea_Intake: '',
    sleepApnea_Frequency: '',
    sleepApnea_Duration: '',
    exercise_Intake: '',
    walking_Intake: '',
    walking_Frequency: '',
    walking_Duration: '',
    jogging_Intake: '',
    jogging_Frequency: '',
    jogging_Duration: '',
    gym_Intake: '',
    gym_Frequency: '',
    gym_Duration: '',
    yoga_Intake: '',
    yoga_Frequency: '',
    yoga_Duration: '',
    aerobics_Intake: '',
    aerobics_Frequency: '',
    aerobics_Duration: '',
    zumba_Intake: '',
    zumba_Frequency: '',
    zumba_Duration: '',
    othersExercise_Intake: '',
    othersExercise_Frequency: '',
    othersExercise_Duration: ''
  };

  setPatientHistoryID(id: number) {
    this.data.patientHistoryID = id;
    console.log('Setting Patient History ID:', id);
  }

  getPatientHistoryID(): number {
    console.log('Getting Patient History ID:', this.data.patientHistoryID);
    return this.data.patientHistoryID;
  }

  updateSection(sectionData: Partial<any>) {
    // Ensure patientHistoryID is preserved during updates
    const currentPatientHistoryID = this.data.patientHistoryID;
    this.data = { ...this.data, ...sectionData };
    
    // If the incoming data doesn't have patientHistoryID or it's null/0, preserve the existing one
    if (!sectionData['patientHistoryID'] || sectionData['patientHistoryID'] === 0) {
      this.data.patientHistoryID = currentPatientHistoryID;
    }
    
    console.log('Updated section data, Current Patient History ID:', this.data.patientHistoryID);
  }

  getPayload(): any {
    console.log('Final payload with Patient History ID:', this.data.patientHistoryID);
    return this.data;
  }

  clear() {
    const currentPatientHistoryID = this.data.patientHistoryID;
    this.data = {
      flag: 'U',
      patientHistoryID: currentPatientHistoryID, // Preserve the ID
      createdBy: 1,
      modifiedBy: 0,
      // Reset other fields...
      aD_Intake: '',
      aD_Frequency: '',
      aD_Quantity: '',
      aD_Duration: '',
      // ... (all other fields)
    };
  }

  // Additional helper methods
  initializeWithID(id: number) {
    this.setPatientHistoryID(id);
    this.data.flag = id > 0 ? 'U' : 'I'; // Update if ID exists, Insert if new
  }

  isNewRecord(): boolean {
    return !this.data.patientHistoryID || this.data.patientHistoryID <= 0;
  }
}