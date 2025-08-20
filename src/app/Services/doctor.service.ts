import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { API_URLS } from '../shared/API-URLs';

@Injectable({ providedIn: 'root' })
export class DoctorService {
  // private apiUrl = `${API_URLS.BASE_URL}/DoctorReg/GetDoctor`;
  private baseUrl = API_URLS.BASE_URL;
  constructor(private http: HttpClient) {}

  getAllDoctors(): Observable<any> {
    console.log('base url', this.baseUrl)
    return this.http.get<any>(`${this.baseUrl}/DoctorReg/GetDoctor`);
  }    
}
