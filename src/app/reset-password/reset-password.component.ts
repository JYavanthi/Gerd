import { Component } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { AuthService } from '../Services/Auth.service';

@Component({
  selector: 'app-reset-password',
  templateUrl: './reset-password.component.html',
  styleUrl: './reset-password.component.css'
})
export class ResetPasswordComponent {
  resetForm: FormGroup;
  token = '';
  message = '';

  constructor(
    private fb: FormBuilder,
    private route: ActivatedRoute,
    private router: Router,
    private authService: AuthService
  ) {
    this.resetForm = this.fb.group({
      newPassword: ['', [Validators.required, Validators.minLength(1)]]
    });
  }

  ngOnInit(): void {
    this.token = this.route.snapshot.queryParams['token'];
  }

  onSubmit() {
    if (this.resetForm.invalid) return;

    this.authService.resetPassword(this.token, this.resetForm.value.newPassword).subscribe({
      next: res => {
        this.message = res.message;
        setTimeout(() => this.router.navigate(['/login']), 3000);
      },
      error: err => this.message = err.error.message || 'Error resetting password'
    });
  }

}
