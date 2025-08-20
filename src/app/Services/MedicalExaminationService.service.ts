
import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { API_URLS } from '../shared/API-URLs';

@Injectable({
    providedIn: 'root'
})
export class MedicalExaminationService {

private baseUrl = API_URLS.BASE_URL;
   
    constructor(private http: HttpClient) { }

    // ✅ Get Chief Complaint by Patient ID
    getMedicalExaminationById(patientId: number): Observable<any> {
        return this.http.get<any>(`${this.baseUrl}/GetMedicalExaminationById/${patientId}`);
        // this.http.get<any>(`${this.baseUrl}/GetCheifComplaintById/${patientId}`);
    }
    

    // 🔄 Add other methods here if needed (e.g., saveChiefComplaint, updateChiefComplaint, etc.)
}

