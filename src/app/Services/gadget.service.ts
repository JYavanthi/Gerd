import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { API_URLS } from '../shared/API-URLs';

@Injectable({
  providedIn: 'root'
})
export class gadgetService {
  private baseUrl = API_URLS.BASE_URL;

  constructor(private http: HttpClient) { }

  GetGadgetById(id: number, stage: number): Observable<any> {
    return this.http.get<any>(`${this.baseUrl}/Gadget/GetGadgetById/${id}/${stage}`);
  }
}
