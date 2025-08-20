// login-form.component.ts
import { Component } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Router } from '@angular/router';
import { API_URLS } from '../shared/API-URLs';

@Component({
  selector: 'app-login-form',
  templateUrl: './login-form.component.html',
  styleUrls: ['./login-form.component.css']
})
export class LoginFormComponent {
  email: string = '';
  mobileNo: string = '';
  password: string = '';
  showPassword: boolean = false;

  constructor(private http: HttpClient, private router: Router) { }

  togglePassword(): void {
    this.showPassword = !this.showPassword;
  }


  onLogin(): void {


    const payload = {
      email: this.email,
      password: this.password,
      mobileNo: this.mobileNo
    };

    this.http.post<any>(`${API_URLS.BASE_URL}${API_URLS.LOGIN}`, payload).subscribe({
      next: (response) => {
        console.log('Login successful:', response);


        // ✅ Store token
        localStorage.setItem('authToken', response.token);

         localStorage.setItem('doctor', JSON.stringify(response.userData));
         localStorage.setItem('doctorid', JSON.stringify(response.userData.doctorid));
        if (this.mobileNo === 'AdminAdmin') {
          this.router.navigate(['/admindashboard']);
          return;
        }
        this.router.navigate(['/dashboard']);
      },
      error: (error) => {
        console.error('Login error:', error);
        alert(error.error?.message || 'Login failed. Please try again.');
      }
    });
  }


  navigateToSignUp() {
    this.router.navigate(['/register']);
  }
}
