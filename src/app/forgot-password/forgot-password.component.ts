import { Component,HostListener } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { AuthService } from '../Services/Auth.service';
import { Subscription } from 'rxjs';
import {  Router } from '@angular/router';

@Component({
  selector: 'app-forgot-password',
  templateUrl: './forgot-password.component.html',
  styleUrl: './forgot-password.component.css'
})
export class ForgotPasswordComponent {
    private pushStateCount = 5; 
forgotForm: FormGroup;
  message = '';

  constructor(private fb: FormBuilder, private authService: AuthService, private router: Router) {
    this.forgotForm = this.fb.group({
      email: ['', [Validators.required, Validators.email]]
    });
  }

  private routerSub!: Subscription;
  onSubmit() {
    if (this.forgotForm.invalid) return;

    this.authService.forgotPassword(this.forgotForm.value.email).subscribe({
      next: res => this.message = res.message,
      error: err => this.message = err.error.message || 'Error sending reset link'
    });
  }
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
}
