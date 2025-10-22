// login-form.component.ts
import { Component, HostListener } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Router } from '@angular/router';
import { API_URLS } from '../shared/API-URLs';
import { Subscription } from 'rxjs';

@Component({
  selector: 'app-login-form',
  templateUrl: './login-form.component.html',
  styleUrls: ['./login-form.component.css']
})
export class LoginFormComponent {
    private pushStateCount = 5; 
  email: string = '';
  mobileNo: string = '';
  password: string = '';
  showPassword: boolean = false;

  constructor(private http: HttpClient, private router: Router) { }

  togglePassword(): void {
    this.showPassword = !this.showPassword;
  }
private routerSub!: Subscription;

ngOnInit(): void {
    for (let i = 0; i < this.pushStateCount; i++) {
      history.pushState({ antiBack: true, idx: i }, '', window.location.href);
    }

    history.replaceState({ top: true }, '', window.location.href);
    
  }
   @HostListener('window:popstate', ['$event'])
  onPopState(event: PopStateEvent) {

    const confirmed = window.confirm(
      'Back navigation is disabled. Click OK to log out or Cancel to stay on this page.'
    );

    if (confirmed) {
      this.logoutUser();
      return;
    }

    setTimeout(() => {
      try {
        // push 2 states to ensure repeated backs don't slip through
        history.pushState({ antiBack: true }, '', window.location.href);
        history.pushState({ antiBack: true }, '', window.location.href);
      } catch (e) {
        // In case some browsers throw
        console.warn('pushState failed', e);
      }
    }, 50); // 30–150ms works; 50ms is a good tradeoff

    // Prevent default-like behavior by moving focus back; not strictly necessary:
    window.scrollTo(0, 0);
  }

  // Also handle page unloads (refresh / close)
  @HostListener('window:beforeunload', ['$event'])
  onBeforeUnload(event: BeforeUnloadEvent) {
    // Show native prompt in some browsers (message ignored by modern browsers)
    event.preventDefault();
    event.returnValue = '';
  }

  logoutUser(): void {
    localStorage.clear();
    sessionStorage.clear();
    // Use router navigate with replaceUrl to avoid extra history entry
    this.router.navigate(['/login'], { replaceUrl: true }).then(() => {
      // Force full navigation to ensure clean state
      window.location.href = '/login';
    });
  }

  ngOnDestroy(): void {
    //window.removeEventListener('popstate', this.preventBackNavigation);
    this.routerSub?.unsubscribe();
   
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
