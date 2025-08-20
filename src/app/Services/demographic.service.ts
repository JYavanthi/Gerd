import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { API_URLS } from '../shared/API-URLs';
@Injectable({
  providedIn: 'root'
})
export class DemographicService {
  private baseUrl = API_URLS.BASE_URL;
  private apiUrl = `${this.baseUrl}/PatientReg/GetPatient`; // <-- change to your base API URL

  constructor(private http: HttpClient) {}

  getDemographicDetailsByPatientId(id: number): Observable<any> {
    return this.http.get(`${this.apiUrl}/${id}`);
  }
}
