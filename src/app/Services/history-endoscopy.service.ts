import { Injectable } from '@angular/core';
import { HttpserviceService } from '../httpservice.service';
import { API_URLS } from '../shared/API-URLs';
import { Observable } from 'rxjs';
import { HttpClient } from '@angular/common/http';

// @Injectable({
//   providedIn: 'root'
// })
// export class HistoryEndoscopyService {
 
//  private baseUrl = API_URLS.BASE_URL; // 🔁 Use this private baseUrl = API_URLS.BASE_URL.
//      // constructor(private http: HttpClient) { }
//      // private baseUrl = API_URLS.BASE_URL;
//      constructor(private http: HttpClient) { }
 
//      // ✅ Get Current Medications by Patient ID
//      gethistoryendoscopeById(patientId: number): Observable<any> {
//          return this.http.get<any>(`${this.baseUrl}/GerdHistory/GetGerdHistoryById/{patientId}${patientId}`);
         
//      }
// }

@Injectable({
  providedIn: 'root'
})
export class HistoryEndoscopyService {

  private baseUrl = API_URLS.BASE_URL;

  constructor(private http: HttpClient) { }

  gethistoryendoscopeById(patientId: number): Observable<any> {
    return this.http.get<any>(`${this.baseUrl}/GerdHistory/GetGerdHistoryById/${patientId}`);
  }
}
