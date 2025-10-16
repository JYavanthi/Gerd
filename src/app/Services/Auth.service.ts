import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { API_URLS } from '../shared/API-URLs';

@Injectable({
  providedIn: 'root'
})
export class AuthService {
  private baseUrl = API_URLS.BASE_URL + '/auth'; // Auth endpoints are under /api/auth

  constructor(private http: HttpClient) {}

  // Forgot Password
  forgotPassword(email: string): Observable<any> {
    return this.http.post(`${this.baseUrl}/forgot-password`, { email });
  }

  // Reset Password
  resetPassword(token: string, newPassword: string): Observable<any> {
    return this.http.post(`${this.baseUrl}/reset-password`, { token, newPassword });
  }
}
