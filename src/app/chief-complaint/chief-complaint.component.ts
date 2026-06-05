import { Component, OnInit, SimpleChanges, HostListener } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { FormvalidationService } from '../formvalidation.service';
import { ActivatedRoute, Router } from '@angular/router';
import { HttpserviceService } from '../httpservice.service';
import { PatientService } from '../Services/patient.service';
import { API_URLS } from '../shared/API-URLs';
import { ChiefComplaintService } from '../Services/chief-complaint.service';
import { Input } from '@angular/core';
import { Subscription } from 'rxjs';

@Component({
  selector: 'app-chief-complaint',
  templateUrl: './chief-complaint.component.html',
  styleUrls: ['./chief-complaint.component.css']
})
export class ChiefComplaintComponent implements OnInit {
  private pushStateCount = 5;
  @Input() patientId!: number;
  @Input() stage!: number;
  tabId = 1;
  @Input() data: any;
  @Input() isPrintMode: boolean = false;
  cheifCompliantID: number = 0;
  @Input() ptnstage: number = 0;
  chiefComplaintForm: FormGroup;
  showCodeMessage = false;
  isViewMode: boolean = false;
  formData: any;

  isFollowUp: boolean = false;
  isSaved: boolean = false;
  constructor(
    private patientService: PatientService,
    private fb: FormBuilder,
    private http: HttpserviceService,
    private formValidation: FormvalidationService,
    public route: ActivatedRoute,
    private router: Router,
    private chiefComplaintService: ChiefComplaintService,

  ) {
    this.chiefComplaintForm = this.fb.group({
      heartburnDuration: [null, Validators.required],
      heartburnFrequency: [null, Validators.required],
      postural_heartburn: [null, Validators.required],
      nocturnal_heartburn: [null, Validators.required],

      regurgitationDuration: [null, Validators.required],
      regurgitationFrequency: [null, Validators.required],
      postural_regurgitation: [null, Validators.required],
      nocturnal_regurgitation: [null, Validators.required],

      painDuration: [null, Validators.required],
      painFrequency: [null, Validators.required],
      postural_pain: [null, Validators.required],
      nocturnal_pain: [null, Validators.required],

      acidTasteDuration: [null, Validators.required],
      acidTasteFrequency: [null, Validators.required],
      postural_AT: [null, Validators.required],
      nocturnal_AT: [null, Validators.required]
    });

  }

  doctorId: number = 0;
  ageInMonths: number = 0;
  private routerSub!: Subscription;
  // ngOnInit(): void {

  //   this.patientId = Number(this.route.snapshot.params['patientId']);
  //   this.stage = Number(this.route.snapshot.params['stage'] || 0);
  //   this.doctorId = this.patientService.getDoctorId();
  //   if (this.stage === 2) { this.cctext = 'Data seen here is as per  information keyed  in baseline. To be edited as per the current complaint' }
  //   else if (this.stage === 4) { this.cctext = 'Data seen here is as per  information keyed  in follow-up1. To be edited as per the current complaint' }
  //   else { this.cctext = '' }

  //   const allowedWithoutSave = [1, 3, 5];
  //   if (allowedWithoutSave.includes(this.stage)) {
  //     this.isSaved = true;
  //   }

  //   if (this.patientId !== 0)
  //     this.fetchChiefComplaintData(this.patientId);

  //   // Age is stored in localStorage from Demographic component
  //   const storedAge = localStorage.getItem('Age');
  //   if (storedAge) {
  //     const age = JSON.parse(storedAge).age; // age in years
  //     this.ageInMonths = age * 12;            // convert years → months
  //     console.log('Age in months:', this.ageInMonths);
  //   }

  //   for (let i = 0; i < this.pushStateCount; i++) {
  //     history.pushState({ antiBack: true, idx: i }, '', window.location.href);
  //   }
  //   history.replaceState({ top: true }, '', window.location.href);


  // }

  ngOnInit(): void {

  this.patientId = Number(this.route.snapshot.params['patientId']);
  this.stage = Number(this.route.snapshot.params['stage'] || 0);
  this.doctorId = this.patientService.getDoctorId();

  if (this.stage === 2) {
    this.cctext =
      'Data seen here is as per information keyed in baseline. To be edited as per the current complaint';
  }
  else if (this.stage === 4) {
    this.cctext =
      'Data seen here is as per information keyed in follow-up1. To be edited as per the current complaint';
  }
  else {
    this.cctext = '';
  }

  const allowedWithoutSave = [1, 3, 5];

  if (allowedWithoutSave.includes(this.stage)) {
    this.isSaved = true;
  }

  // Fetch Chief Complaint Data
  if (this.patientId !== 0) {
    this.fetchChiefComplaintData(this.patientId);
  }

  // Get Age from localStorage
  const storedAge = localStorage.getItem('Age');

  if (storedAge) {

    const parsedAge = JSON.parse(storedAge);

    this.ageInMonths = Number(parsedAge.age) * 12;

    console.log('Age in months:', this.ageInMonths);

  } else {

    console.warn('Age not found in localStorage');

  }

  // Prevent Back Navigation
  for (let i = 0; i < this.pushStateCount; i++) {
    history.pushState(
      { antiBack: true, idx: i },
      '',
      window.location.href
    );
  }

  history.replaceState(
    { top: true },
    '',
    window.location.href
  );

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

  ngOnChanges(changes: SimpleChanges): void {
    if (changes['data'] && this.data) {
      this.patchForm(this.data);
    }
  }
  private patchForm(data: any): void {
    this.chiefComplaintForm.patchValue({
      heartburnDuration: data.hbDuration,
      heartburnFrequency: data.hbFrequency,
      postural_heartburn: data.hbPostural,
      nocturnal_heartburn: data.hbNocturnal,
      regurgitationDuration: data.rDuration,
      regurgitationFrequency: data.rFrequency,
      postural_regurgitation: data.rPostural,
      nocturnal_regurgitation: data.rNocturnal,
      painDuration: data.rpDuration,
      painFrequency: data.rpFrequency,
      postural_pain: data.rpPostural,
      nocturnal_pain: data.rpNocturnal,
      acidTasteDuration: data.atDuration,
      acidTasteFrequency: data.atFrequency,
      postural_AT: data.atPostural,
      nocturnal_AT: data.atNocturnal
    });
  }

  stageval: number = 0;
  cctext: string = '';
  setStage: number = 0;
  fetchChiefComplaintData(patientId: number): void {
    if (this.setStage === 1) return;

    this.chiefComplaintService.getChiefComplaintByPatientId(patientId, this.stage).subscribe({
      next: (res: any) => {
        if (this.stage === 2 || this.stage === 4) this.stageval = this.stage;
        // if (res.type === 'S' && res.data) {
        //   // this.isSaved = false;
        //   const data = res.data;

        if (res.type === 'S' && res.data) {

  this.ageInMonths = Number(res.data.age) * 12;

  console.log('Age in months from API:', this.ageInMonths);

  // this.isSaved = false;

  const data = res.data;
          // console.log('✅ Chief Complaint data:', data);
          this.stage = data.stage;
          this.cheifCompliantID = res.data.cheifCompliantID;
          this.chiefComplaintForm.patchValue({
            heartburnDuration: data.hbDuration,
            heartburnFrequency: data.hbFrequency,
            postural_heartburn: data.hbPostural,
            nocturnal_heartburn: data.hbNocturnal,
            regurgitationDuration: data.rDuration,
            regurgitationFrequency: data.rFrequency,
            postural_regurgitation: data.rPostural,
            nocturnal_regurgitation: data.rNocturnal,
            painDuration: data.rpDuration,
            painFrequency: data.rpFrequency,
            postural_pain: data.rpPostural,
            nocturnal_pain: data.rpNocturnal,
            acidTasteDuration: data.atDuration,
            acidTasteFrequency: data.atFrequency,
            postural_AT: data.atPostural,
            nocturnal_AT: data.atNocturnal
          });

        }
         else {
          if (this.stageval === 2) this.stage = 1
          if (this.stageval === 4) this.stage = 3
          // console.log('this.stageval', this.stageval, this.stage)
          this.fetchChiefComplaintData(Number(this.patientId));
          this.setStage = 1;
          setTimeout(() => {

            this.stage = this.stageval;
          }, 1000);
          //console.log('this.stageval after', this.stageval, this.stage)

        }
      },
      error: (err) => {
        console.error('❌ Error fetching chief complaint data:', err);
      }
    });
  }

  blockInvalidKeys(event: KeyboardEvent) {
    if (['e', 'E', '+', '-', '.', ')','(','*','&','%','$','#', '@', '!', '~', '^'].includes(event.key)) {
      event.preventDefault();
    }

  }


  preventNegative(event: any) {

    if (event.target.value < 0) {
      event.target.value = 0; // reset to 0 if negative
    }
  }

  onCodeFocus(): void {
    this.showCodeMessage = true;
  }

  Submit(): void {

    if (!this.formValidation.validateForm(this.chiefComplaintForm)) {
      this.chiefComplaintForm.markAllAsTouched();
      alert('Enter all fields');
      return;
    }

    if (this.patientId === null) {
      this.formValidation.showAlert('Patient ID is missing', 'danger');
      return;
    }

    if (this.stage === 1) this.ptnstage = 2;
    else if (this.stage === 3) this.ptnstage = 4;
    else if (this.stage === 0) this.ptnstage = 0;
    else this.ptnstage = this.stage;

    const isUpdate = this.cheifCompliantID && this.cheifCompliantID > 0;
    const formValue = this.chiefComplaintForm.value;
    const param = {
      stage: this.ptnstage,
      flag: isUpdate ? 'U' : 'I',
      cheifCompliantID: isUpdate ? this.cheifCompliantID : 0,
      patientID: this.patientId,
      doctorID: this.doctorId,
      hB_Duration: Number(formValue.heartburnDuration),
      hB_Frequency: Number(formValue.heartburnFrequency),
      hB_Postural: formValue.postural_heartburn,
      hB_Nocturnal: formValue.nocturnal_heartburn,
      r_Duration: Number(formValue.regurgitationDuration),
      r_Frequency: Number(formValue.regurgitationFrequency),
      r_Postural: formValue.postural_regurgitation,
      r_Nocturnal: formValue.nocturnal_regurgitation,
      rP_Duration: Number(formValue.painDuration),
      rP_Frequency: Number(formValue.painFrequency),
      rP_Postural: formValue.postural_pain,
      rP_Nocturnal: formValue.nocturnal_pain,
      aT_Duration: Number(formValue.acidTasteDuration),
      aT_Frequency: Number(formValue.acidTasteFrequency),
      aT_Postural: formValue.postural_AT,
      aT_Nocturnal: formValue.nocturnal_AT,
      createdBy: this.doctorId
    };

    console.log('Submitting Chief Complaint Payload:', param);

    const enteredHBDurationMonths = param.hB_Duration;
    const enteredrDurationMonths = param.r_Duration;
    const enteredrPDurationMonths = param.rP_Duration;
    const enteredaTDurationMonths = param.aT_Duration;
    const enteredhB_Frequency = param.hB_Frequency;
    const enteredr_Frequency = param.r_Frequency;
    const enteredrP_Frequency = param.rP_Frequency; 
    const enteredaT_Frequency= param.aT_Frequency

    if (
      enteredHBDurationMonths > this.ageInMonths ||
      enteredrDurationMonths > this.ageInMonths ||
      enteredrPDurationMonths > this.ageInMonths ||
      enteredaTDurationMonths > this.ageInMonths
    ) {

      alert('The entered number of months exceeds the person’s age of (' + this.ageInMonths + ' ) in months. Please enter valid values.');
      return;
    }

    //    //maximum possible hours per week = 24*7=168
    // if (enteredhB_Frequency > 168 || 
    //   enteredr_Frequency > 168 ||
    //   enteredrP_Frequency > 168 ||
    //   enteredaT_Frequency > 168 
    // ) {
    //   alert('Entered Frequency cannot exceed the maximum possible hours per week 168 . Please correct the value.');
    //   return;
    // }

    this.http.httpPost(API_URLS.CHEIF_COMPLAINT_SAVE, param).subscribe(
      (res: any) => {
        if (res.type === 'S') {
          this.isSaved = true;
          this.formValidation.showAlert('Chief complaint saved successfully', 'success');
          alert('Saved Successfully');

          this.router.navigate([], {
            queryParams: {
              patientId: this.patientId,
              stage: this.stage
            }
          });

          // } else {
          //   this.formValidation.showAlert('Unable to fetch Patient ID after save', 'danger');
          //   alert(' Unable to fetch Patient ID after save');
          // }
          //});
        } else {
          const errorMsg = `Error: ${res.message || 'Unknown error'}`;
          this.formValidation.showAlert(errorMsg, 'danger');
          alert(` ${errorMsg}`);
        }
      },
      error => {
        console.error('Error saving chief complaint:', error);
        this.formValidation.showAlert('Error saving chief complaint', 'danger');
        alert(' Error saving chief complaint');
      }
    );
  }

  goToComorbidities(): void {
    this.router.navigate([`comorbidities/${this.patientId}/${this.ptnstage}`]);
  }

  OnNext(): void {
    this.patientService.setChiefComplaintData(this.chiefComplaintForm.value);


    this.router.navigate([`comorbidities/${this.patientId}/${this.stage}`], {
      state: {
        tabId: this.tabId,
        patientId: this.patientId,
        isViewMode: true
      }
    });
  }
  goback() {
    this.router.navigate([`/demographic/${this.patientId}/${this.stage}`], {
      state: {
        tabId: this.tabId,
        patientId: this.patientId,
        isViewMode: this.isViewMode
      }
    });
  }
  back() {
    this.router.navigate([`/demographic/${this.patientId}/${this.stage}`], {
      state: {
        tabId: this.tabId,
        patientId: this.patientId,
        isViewMode: this.isViewMode
      }
    });
  }

  onNext(): void {
    this.patientService.setChiefComplaintData(this.chiefComplaintForm.value);

    const navigationExtras = {
      state: {
        tabId: this.tabId,
        patientId: this.patientId,
        isViewMode: this.isViewMode
      }
    };

    this.router.navigate([`comorbidities/${this.patientId}/${this.stage}`], navigationExtras);
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