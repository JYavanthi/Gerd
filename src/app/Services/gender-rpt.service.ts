import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { API_URLS } from '../shared/API-URLs';

export interface GenderData {
  initial?: string;
  subjectNo?: string;
  gender?: string;
  state?: number;
  city?: number;
  zone: string;
}

export interface ApiResponse {
  success: boolean;
  message: string;
  data: GenderData[];
}

@Injectable({
  providedIn: 'root'
})
export class GenderRptService {
  private baseUrl = API_URLS.BASE_URL;
  private apiUrl = `${this.baseUrl}/GenderRPT/GetAll`; 
  //Private apiUrl = 'https://gerdregistryofindia.com/GERD/api/GenderRPT/GetAll'; // Replace with your actual API base URL

  constructor(private http: HttpClient) { }

  getGenderReport(): Observable<ApiResponse> {
    return this.http.get<ApiResponse>(this.apiUrl);
  }
}
