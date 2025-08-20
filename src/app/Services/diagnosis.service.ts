import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { API_URLS } from '../shared/API-URLs';

@Injectable({
  providedIn: 'root'
})
export class DiagnosisService {

private baseUrl = API_URLS.BASE_URL;
 
   constructor(private http: HttpClient) {}
 
   // ✅ Get Chief Complaint by Patient ID
   getdiagnosisId(patientId: number): Observable<any> {
     return this.http.get<any>(`${this.baseUrl}/Diagnosis/GetDiagnosisById/${patientId}`);
   }
}
