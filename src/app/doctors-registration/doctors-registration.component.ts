import { Component,HostListener } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute, } from '@angular/router';
import { FormvalidationService } from '../formvalidation.service';
import { HttpserviceService } from '../httpservice.service';
import { API_URLS } from '../shared/API-URLs';
import { Router } from '@angular/router';
import { Subscription } from 'rxjs';


@Component({
  selector: 'app-doctors-registration',
  templateUrl: './doctors-registration.component.html',
  styleUrl: './doctors-registration.component.css'
})
export class DoctorsRegistrationComponent {
   private pushStateCount = 5; 
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
      reenterPassword: ['',Validators.required],
    });
  }

  private routerSub!: Subscription;
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
    this.routerSub?.unsubscribe();
  }

  onCodeFocus(): void {
    this.showCodeMessage = true;
  }

  onSubmit(): void {
    if (this.doctorForm.valid) {
      //console.log(this.doctorForm.value);
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
        //this.doctorForm.reset();
        alert('Doctor registered successfully!');
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
