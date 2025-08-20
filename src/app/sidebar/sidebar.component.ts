import { HttpClient,HttpHeaders } from '@angular/common/http';
import { Component,OnInit  } from '@angular/core';

@Component({
  selector: 'app-sidebar',
  templateUrl: './sidebar.component.html',
  // styleUrl: './sidebar.component.css'
  styleUrls: ['./sidebar.component.css'] 
})
export class SidebarComponent {
  doctor: any = null;

  constructor(private http: HttpClient) {}

  ngOnInit(): void {
    
    const doctorData = localStorage.getItem('doctor');

    if (doctorData) {
      this.doctor = JSON.parse(doctorData);
    } else {
      // ✅ Fallback: fetch from API using token
      this.getDoctorData();
    }
  }

  getDoctorData(): void {
    const token = localStorage.getItem('authToken');
    if (!token) {
      console.warn('Token not found.');
      return;
    }

    const headers = new HttpHeaders({
      Authorization: `Bearer ${token}`
    });
    this.http.get<any>('/DoctorReg/GetDoctor',{ headers }).subscribe({
      next: (response) => {
        this.doctor = response.userData;
        localStorage.setItem('token', response.token); 
        localStorage.setItem('DoctorID',response.data.doctorID);
      },
      error: (error) => {
        console.error('Failed to fetch doctor data:', error);
      }
    });
    console.log('Doctor data from API:', this.doctor);
  }
}
