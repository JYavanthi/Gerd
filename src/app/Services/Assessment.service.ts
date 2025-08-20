import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { API_URLS } from '../shared/API-URLs';


@Injectable({
    providedIn: 'root'
})
export class AssessmentService {
  constructor(private http: HttpClient) {}
private baseUrl = API_URLS.BASE_URL;

  getAssessmentById(patientId: number, stage:number): Observable<any> {
return this.http.get(`${this.baseUrl}/Assessment/GetAssessmentById/${patientId}/${stage}`);  }
 

}
