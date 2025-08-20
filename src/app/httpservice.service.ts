import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { API_URLS } from './shared/API-URLs';

@Injectable({
  providedIn: 'root'
})
export class HttpserviceService {
  apiUrl = API_URLS.BASE_URL;
  constructor(private http: HttpClient) {}
private baseUrl = API_URLS.BASE_URL;
  headers = new HttpHeaders({
    'Content-Type': 'application/json'
  });

httpGet(endpoint: string, param: any = {}): Observable<any[]> {
  return this.http.get<any[]>(`${this.apiUrl}${endpoint}`, { headers: this.headers, params: param });
}

httpGet2<T>(endpoint: string, param: any = {}): Observable<T> {
  return this.http.get<T>(`${this.apiUrl}${endpoint}`, { headers: this.headers, params: param });
}

  httpPost(endpoint: string, param: any): Observable<any[]> {
    return this.http.post<any[]>(`${this.apiUrl}${endpoint}`, param, { headers: this.headers });
  }

  httpPut(endpoint: string, param: any): Observable<any[]> {
    return this.http.put<any[]>(`${this.apiUrl}${endpoint}`, param, { headers: this.headers });
  }

  httpDelete(endpoint: string, param: any = {}): Observable<any[]> {
    return this.http.delete<any[]>(`${this.apiUrl}${endpoint}`, { headers: this.headers, params: param });
  }

  downloadExcel(endpoint: string) {
    const url = `your-api-base-url/${endpoint}`; // e.g., /api/download/patients-excel
    return this.http.get(url, {
      responseType: 'blob'  // Important for binary file downloads
    });
  }
getPageRouterByPatientId(patientId: number): Observable<any> {
  const url = API_URLS.BASE_URL + API_URLS.GET_PAGE_ROUTER.replace('{patientId}', patientId.toString());
  console.log('Calling URL:', url);
  return this.http.get<any>(url);
}
}
