import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { API_URLS } from '../shared/API-URLs';


@Injectable({
    providedIn: 'root'
})
export class ComorbiditiesService {
  constructor(private http: HttpClient) {}
private baseUrl = API_URLS.BASE_URL;

  getComorbiditiesById(patientId: number, stage: number): Observable<any> {
return this.http.get(`${this.baseUrl}/Comorbidities/GetComorbditiesById/${patientId}/${stage}`);  }
 
saveComorbidities(payload: any): Observable<any> {
    return this.http.post(`${this.baseUrl}/Comorbidities/SaveComorbidities`, payload);
  }
 getComorbidities(): Observable<any> {
    return this.http.get(`${this.baseUrl}/Comorbidities/GetComorbidities`);
 }

}
