import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { Comorbidity } from '../module/comorbidity.model';
import { API_URLS } from '../shared/API-URLs';
@Injectable({
  providedIn: 'root'
})
export class ComorbidityService {
  private baseUrl = API_URLS.BASE_URL;
  private apiUrl = `${this.baseUrl}/ComorbitiesRpt/GetAll`; 

  //private apiUrl = 'https://gerdregistryofindia.com/GERD/api/ComorbitiesRpt/GetAll';

  constructor(private http: HttpClient) {}

  getComorbiditiesData(): Observable<Comorbidity[]> {
    return this.http.get<Comorbidity[]>(this.apiUrl);
  }
}
