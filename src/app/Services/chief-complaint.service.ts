import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { API_URLS } from '../shared/API-URLs';

@Injectable({
  providedIn: 'root'
})
export class ChiefComplaintService {

  
  private baseUrl = API_URLS.BASE_URL;
  private apiUrl = `${this.baseUrl}/CheifComplaint`; 
  constructor(private http: HttpClient) {}

  // ✅ Get Chief Complaint by Patient ID
  getChiefComplaintByPatientId(patientId: number,stage: number): Observable<any> {
    return this.http.get<any>(`${this.apiUrl}/GetCheifComplaintById/${patientId}/${stage}`);
  }

  // 🔄 Add other methods here if needed (e.g., saveChiefComplaint, updateChiefComplaint, etc.)
}