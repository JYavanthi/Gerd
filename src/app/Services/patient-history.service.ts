import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { API_URLS } from '../shared/API-URLs';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class PatientHistoryService {
  private readonly patientHistoryIdKey = 'PatientHistoryID';
  private patientHistoryId: number = 0;
     constructor(private http: HttpClient) {}


  private PatientHistoryID: number = 0;
  private baseUrl = API_URLS.BASE_URL;
  

    getHistoryByid(PatientID: number, stage:number): Observable<any> {
  return this.http.get(`${this.baseUrl}/History/GetHistoryById/${PatientID}/${stage}`);}

  setPatientHistoryID(id: number): void {
    this.patientHistoryId = id;
    localStorage.setItem(this.patientHistoryIdKey, id.toString());
  }

  getPatientHistoryID(): number {
    if (!this.patientHistoryId) {
      const storedID = localStorage.getItem(this.patientHistoryIdKey);
      this.patientHistoryId = storedID ? +storedID : 0;
    }
    return this.patientHistoryId;
  }

  clearPatientHistoryID(): void {
    this.patientHistoryId = 0;
    localStorage.removeItem(this.patientHistoryIdKey);
  }

  //-----------------------------------------------------------------
  // getPatientHistoryById(patientId: number, stage:number): Observable<any> {
  //   return this.http.get<any>(`${this.baseUrl}/GetPatientHistoryById/${patientId}/${stage}`);
  //   // this.http.get<any>(`${this.baseUrl}/GetCheifComplaintById/${patientId}`);
  // }
}
