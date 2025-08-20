
import { Injectable } from "@angular/core";
import { API_URLS } from "../shared/API-URLs";
import { Observable } from "rxjs";
import { HttpClient } from "@angular/common/http";

@Injectable({
  providedIn: 'root'
})
export class PatientService {
  private readonly patientIdKey = 'patientId';
  private readonly doctorIdKey = 'doctorId';
  private readonly patientStage ='stage';
  // ✅ Store demographic data only in memory
  private demographicData: any = null;
  private personalHistoryData: any = null;
  private chiefComplaintData: any;
  private famalyhistoryData: any ;
private baseUrl = API_URLS.BASE_URL;
private personalHistoryIntakeStates: any;
  private diagnosisData: any = null; 
  private caseStatus: string = '';

constructor(private http: HttpClient) { }
 getAllPatientData(): Observable<any> {
       return this.http.get(`${this.baseUrl}/PatientReg/GetPatient`);

  }
  getAllDoctorData():Observable<any>{
    return this.http.get(`${this.baseUrl}/DoctorReg/GetDoctor`);
  }

  setPatientId(id: number): void {
    localStorage.setItem(this.patientIdKey, id.toString());
   }
  
   setPatientstage(ptnstage: number): void {
    localStorage.setItem(this.patientStage, ptnstage.toString());
   }

  getPatientId(): number {
    const id = localStorage.getItem(this.patientIdKey);
    return id ? +id : 0;
  }

  getPatientStage(): number {
    const ptnstage = localStorage.getItem(this.patientStage);
    return ptnstage ? +ptnstage : 0;
  }

  setDoctorId(id: number): void {
    localStorage.setItem(this.doctorIdKey, id.toString());
  }

  getDoctorId(): number {
    const id = localStorage.getItem(this.doctorIdKey);
    return id ? +id : 0;
  }

  clearIds(): void {
    localStorage.removeItem(this.patientIdKey);
    localStorage.removeItem(this.doctorIdKey);
  }

  // ✅ Only in-memory demographic data (cleared on refresh)
  setDemographicData(data: any): void {
    this.demographicData = data;
  }

  getDemographicData(): any {
    return this.demographicData;
  }

  // clearDemographicData(): void {
  //   this.demographicData = null;
  // }

  setChiefComplaintData(data: any) {
  this.chiefComplaintData = data;
}

getChiefComplaintData(): any {
  return this.chiefComplaintData;
}

clearChiefComplaintData() {
  this.chiefComplaintData = null;
}
  
setfamalyhistoryData(data: any): void {
  this.famalyhistoryData = data;
}

getfamalyhistoryData(): any {
  return this.famalyhistoryData;
}

clearfamalyhistoryData(): void {
  this.famalyhistoryData = null;
}
setPersonalHistoryData(data: any) {
  this.personalHistoryData = data;
}

getPersonalHistoryData(): any {
  return this.personalHistoryData;
}

setPersonalHistoryIntakeStates(states: any) {
  this.personalHistoryIntakeStates = states;
}

getPersonalHistoryIntakeStates(): any {
  return this.personalHistoryIntakeStates;
}
 setDiagnosisData(data: any) {
  this.diagnosisData = data;
  localStorage.setItem('diagnosisData', JSON.stringify(data)); // ✅ store in localStorage
}

getDiagnosisData(): any {
  if (this.diagnosisData) return this.diagnosisData;

  const stored = localStorage.getItem('diagnosisData');
  if (stored) {
    this.diagnosisData = JSON.parse(stored);
    return this.diagnosisData;
  }

  return null;
}
  clearDiagnosisData() {
    this.diagnosisData = null;
  }


  setCaseStatus(status: string) {
  this.caseStatus = status;
}

getCaseStatus(): string {
  return this.caseStatus;
}
}
