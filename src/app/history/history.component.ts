import { Component, Input, OnInit, SimpleChanges, HostListener } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Router, ActivatedRoute } from '@angular/router';
import { HttpserviceService } from '../httpservice.service';
import { FormvalidationService } from '../formvalidation.service';
import { API_URLS } from '../shared/API-URLs';
import { PatientHistoryService } from '../Services/patient-history.service';
import { HistoryService } from '../Services/history.servie';
import { PatientService } from '../Services/patient.service';
import { Subscription } from 'rxjs';

@Component({
  selector: 'app-history',
  templateUrl: './history.component.html',
  styleUrls: ['./history.component.css']
})
export class HistoryComponent implements OnInit {
  private pushStateCount = 5;
  patientHistory!: FormGroup
  tabId = 1;
  @Input() stage: number = 0;
  patientHistoryForm: FormGroup;
  @Input() patientId: number | null = null;
  doctorId: number | null = null;
  isViewMode: boolean = false;
  formData: any;
  patientHistoryId: number = 0; // Default 0 means new record
  isFollowUp: boolean = false;
  isSaved: boolean = false;
  userData: any;
  @Input() data: any;
  @Input() isPrintMode: boolean = false;


  constructor(
    private fb: FormBuilder,
    private http: HttpserviceService,
    private formValidation: FormvalidationService,
    private route: ActivatedRoute,
    private router: Router,
    private historyService: HistoryService,
    private patientHistoryService: PatientHistoryService,
    private patientService: PatientService
  ) {
    this.patientHistoryForm = this.fb.group({
      pastHistory: ['', Validators.required],
      diet: ['', Validators.required]
    });

  }
  private routerSub!: Subscription;

  ngOnInit(): void {

    this.route.params.subscribe(params => {
      this.patientId = +params['patientId']
      this.stage = +params['stage']

    });


    this.patientHistoryForm.statusChanges.subscribe(status => {
      const allowedWithoutSave = [1, 3, 5];
      if (allowedWithoutSave.includes(this.stage)) {
        this.isSaved = true;
      }

    });
    if (this.data) {
      console.log("📌 Patching from parent onInit:", this.data);
      this.patchForm(this.data);   // ✅ PATCH HERE
    } else if (this.patientId) {
      console.log("📌 Fetching from API");
      this.fetchPatientHistoryById(this.patientId, this.stage);
    }


    console.log("📌 Patient ID from URL:", this.patientId);
    console.log("📌 Stage from URL:", this.stage);


    const state = history.state;
    this.isViewMode = state?.isViewMode ?? false;
    this.tabId = state?.tabId ?? 1;
    this.formData = state?.data;

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

  ngOnChanges(changes: SimpleChanges): void {
    if (changes['data'] && changes['data'].currentValue) {
      console.log("📌 Patching from parent (onChanges):", this.data);
      this.patchForm(this.data);
    }
  }
  private patchForm(data: any): void {
    this.patientHistoryForm.patchValue({
      pastHistory: data.pastHistory || '',
      diet: data.dietVegetarian ? 'Vegetarian' : data.dietNonVegetarian ? 'Non-Vegetarian' : ''
    });

  }

  fetchPatientHistoryById(PatientID: number, stage: number): void {
    this.patientHistoryService.getHistoryByid(PatientID, stage).subscribe({
      next: (res: any) => {
        if (res.type === 'S' && res.data) {

          const data = res.data;
          // console.log('history data', data);
          // this.stage = data.stage;
          this.patientHistoryForm.patchValue({
            pastHistory: data.pastHistory || '',
            diet: data.dietVegetarian ? 'Vegetarian' : data.dietNonVegetarian ? 'Non-Vegetarian' : ''
          });

        }
      },
      error: (err) => {
        console.error('Error fetching patient history:', err);
      }
    });
  }
  isFormComplete(): boolean {
    return this.patientHistoryForm.valid;
  }

  Submit() {
    if (this.patientHistoryForm.invalid) {
      const controls = this.patientHistoryForm.controls;

      if (controls['pastHistory'].invalid) {
        alert('Please fill Past History.');
      } else if (controls['diet'].invalid) {
        alert('Please select Diet.');
      }

      this.patientHistoryForm.markAllAsTouched(); // Highlight missing fields
      return; // Stop further execution
    }

    // ✅ Proceed with submission if form is valid
    const f = this.patientHistoryForm.value;
    let user: any = localStorage.getItem('doctor');
    this.userData = JSON.parse(user);

    const param = {
      stage: this.stage,
      Flag: 'I',
      doctorID: this.userData?.doctorId,
      PatientID: this.patientId,
      Past_History: f.pastHistory,
      Diet_Vegetarian: f.diet === 'Vegetarian',
      Diet_NonVegetarian: f.diet === 'Non-Vegetarian',
      CreatedBy: this.userData?.doctorId
    };

    console.log('Payload to save:', param);

    this.http.httpPost(API_URLS.HISTORY_SAVE, param).subscribe({
      next: (res: any) => {
        if (res.type === 'S') {
          alert('Saved Successfully');
          this.isSaved = true;
        } else {
          this.formValidation.showAlert('Error saving data!', 'danger');
        }
      },
      error: (err) => {
        console.error('Save error:', err);
        this.formValidation.showAlert('Network or server error during save.', 'danger');
      }
    });
  }


  goNext() {
    this.router.navigate([`/Personal-history/${this.patientId}/${this.stage}`], {
      state: this.patientHistoryId ? {

        id: this.patientHistoryId, tabId: this.tabId,
        patientId: this.patientService.getPatientId(), isViewMode: this.isViewMode
      } : {}
    });
  }

  goback() {
    this.router.navigate([`/comorbidities/${this.patientId}/${this.stage}`], {
      state: {
        tabId: this.tabId,
        patientId: this.patientService.getPatientId(),
        isViewMode: this.isViewMode
      }
    });
  }

  onNext() {
    const currentUrl = this.router.url;

    const stateData = this.patientHistoryId ? {
      stage: this.stage,
      tabId: this.tabId,
      isViewMode: this.isViewMode,

      patientId: this.patientService.getPatientId(),
      // Make sure this is true or false as needed
    } : {};

    console.log('Navigating with state:', stateData);
    console.log('passed viewmode is', this.isViewMode) // ✅ Confirm what's being passed

    this.router.navigate([`Personal-history/${this.patientService.getPatientId()}`], {
      state: stateData

    });
  }
  back() {
    this.router.navigate([`/comorbidities`], {
      state: {
        tabId: this.tabId,
        patientId: this.patientService.getPatientId(),
        isViewMode: this.isViewMode
      }
    });
  }
  getStatusClass(step: number): string {
    if (this.stage === 0 && step === 1) return 'baseline-blue';

    if (this.stage >= 1 && step === 1) return 'baseline-green';
    if (this.stage >= 1 && this.stage < 3 && step === 2) return 'baseline-blue';

    if (this.stage >= 3 && step === 2) return 'baseline-green';
    if (this.stage >= 3 && this.stage < 5 && step === 3) return 'baseline-blue';

    if (this.stage === 5 && step === 3) return 'baseline-green';

    return 'inactive-tab';
  }
}
