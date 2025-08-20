import { Component } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute, } from '@angular/router';
import { FormvalidationService } from '../formvalidation.service';
import { HttpserviceService } from '../httpservice.service';
import { API_URLS } from '../shared/API-URLs';
import { Router } from '@angular/router';



@Component({
  selector: 'app-doctors-registration',
  templateUrl: './doctors-registration.component.html',
  styleUrl: './doctors-registration.component.css'
})
export class DoctorsRegistrationComponent {
  doctorForm: FormGroup;
  showCodeMessage = false;
  states: Array<{ id: number; name: string }> = [];
  cities: any;


  constructor(
    private fb: FormBuilder,
    private formValidation: FormvalidationService,
    private http: HttpserviceService,
    private router: Router,
    public route: ActivatedRoute) {
    this.doctorForm = this.fb.group({
      firstName: ['', Validators.required],
      lastName: ['', Validators.required],
      phone: ['', [Validators.required, Validators.pattern('^[0-9]{10}$')]],
      email: ['', [Validators.required, Validators.email]],
      mciCode: ['', Validators.required],
      practicePlace: ['', Validators.required],
      hospitalName: ['', Validators.required],
      state: ['', Validators.required],
      city: ['', Validators.required],
      codeNumber: [''],
      password: ['', Validators.required],
      reenterPassword: [''],
    });
  }
  ngOnInit(): void {
    this.loadStates();
    this.doctorForm.get('status')?.valueChanges.subscribe((value) => {
      const reenterCtrl = this.doctorForm.get('reenterPassword');
      if (value === 'Active') {
        reenterCtrl?.setValidators([Validators.required]);
      } else {
        reenterCtrl?.clearValidators();
      }
      reenterCtrl?.updateValueAndValidity();
    });
  }
  onCodeFocus(): void {
    this.showCodeMessage = true;
  }

  onSubmit(): void {
    if (this.doctorForm.valid) {
      console.log(this.doctorForm.value);
      alert('Form submitted successfully!');
    } else {
      alert('Please fill all required fields.');
    }
  }
  Submit() {
    if (!this.formValidation.validateForm(this.doctorForm)) {
      this.doctorForm.markAllAsTouched();
      return;
    }
    const password = this.doctorForm.controls['password'].value;
    const rePassword = this.doctorForm.controls['reenterPassword'].value;

    if (password !== rePassword) {
      alert('Passwords do not match'); return;
    }
    const param = {
      flag: "I",
      doctorID: 0,
      name: this.doctorForm.controls['firstName'].value,
      email: this.doctorForm.controls['email'].value,
      phoneNO: this.doctorForm.controls['phone'].value,
      mciCode: this.doctorForm.controls['mciCode'].value,
      placeOfPractice: this.doctorForm.controls['practicePlace'].value,
      hospitalName: this.doctorForm.controls['hospitalName'].value,
      password: password,
      state: Number(this.doctorForm.controls['state'].value),
      city: Number(this.doctorForm.controls['city'].value),
      enterCodeNO: this.doctorForm.controls['codeNumber'].value,
      status: "Active", 
      createdBy: 0
    };

    this.http.httpPost(API_URLS.DOCTOR_REG_SAVE, param).subscribe((res: any) => {
      if (res.type === 'S') {
        this.formValidation.showAlert('Doctor registered successfully!', 'success');
        this.doctorForm.reset();
        this.router.navigate(['/login']);
      } else {
        this.formValidation.showAlert('Error during registration', 'danger');
      }
    }, (error) => {
      this.formValidation.showAlert('Server error occurred', 'danger');
      console.error(error);
    });
  }
  getCities(event: any) {
    this.http.httpGet(API_URLS.CITY_GET, { stateId: event.target.value }).subscribe({
      next: (res: any) => {
        this.cities = res.sort((a: any, b: any) => {
          const nameA = a.name.toLowerCase();
          const nameB = b.name.toLowerCase();
          return nameA.localeCompare(nameB);
        });
      },
      error: (err) => {
        this.formValidation.showAlert('Error loading cities', 'danger');
        console.error(err);
      }
    });
  }
  loadStates() {
    if (this.states.length === 0) {
      this.http.httpGet(API_URLS.STATE_GET).subscribe({
        next: (res: any) => {
          // Sort states alphabetically by name
          this.states = res.sort((a: any, b: any) => {
            const nameA = a.name.toLowerCase();
            const nameB = b.name.toLowerCase();
            return nameA.localeCompare(nameB);
          });
        },
        error: (err) => {
          this.formValidation.showAlert('Error loading states', 'danger');
          console.error(err);
        }
      });
    }
  }

  login() {
  }
}
