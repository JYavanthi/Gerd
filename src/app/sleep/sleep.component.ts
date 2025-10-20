import { Component, Input, OnInit, HostListener  } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { HttpserviceService } from '../httpservice.service';
import { FormvalidationService } from '../formvalidation.service';
import { HistoryService } from '../Services/history.servie';
import { FormBuilder, FormGroup } from '@angular/forms';
import { API_URLS } from '../shared/API-URLs';
import { PatientService } from '../Services/patient.service';
import { sleepService } from '../Services/Sleep.service';
import { Subscription } from 'rxjs';

@Component({
  selector: 'app-sleep',
  templateUrl: './sleep.component.html',
  styleUrls: ['./sleep.component.css']
})
export class SleepComponent implements OnInit {
   private pushStateCount = 5;
  sleepForm!: FormGroup;
  tabId = 1;
  @Input() stage!: number;
  @Input() patientId: number | null = null;
  doctorId: number | null = null;
  isViewMode = false;
  isFollowUp = false;
  isSaved = false;
  userData: any;
  exerciseTypes: string[] = [];
  @Input() data: any;
  // patientId: any;
  @Input() isPrintMode: boolean = false;
  ageInYears: number = 0;

  constructor(
    private router: Router,
    private http: HttpserviceService,
    private route: ActivatedRoute,
    private historyService: HistoryService,
    private formValidation: FormvalidationService,
    private fb: FormBuilder,
    private patientService: PatientService,
    private sleepService: sleepService
  ) {
    this.sleepForm = this.fb.group({
      patientId: [''],
      sleepApnea: [''],
      sleepApneaFrequency: [''],
      sleepApneaDuration: [''],
      exerciseIntake: [''],
      jogging: [''],
      joggingFrequency: [''],
      joggingDuration: [''],
      gym: [''],
      gymFrequency: [''],
      gymDuration: [''],
      yoga: [''],
      yogaFrequency: [''],
      yogaDuration: [''],
      walking: [''],
      walkingFrequency: [''],
      walkingDuration: [''],
      aerobics: [''],
      aerobicsFrequency: [''],
      aerobicsDuration: [''],
      zumba: [''],
      zumbaFrequency: [''],
      zumbaDuration: [''],
      othersText: [''],
      others: [''],
      othersFrequency: [''],
      othersDuration: ['']
    });
  }
private routerSub!: Subscription;

  ngOnInit(): void {
    this.patientId = Number(this.route.snapshot.params['patientId']);
    this.stage = Number(this.route.snapshot.params['stage'] || 0);

    this.sleepForm.get('sleepApnea')?.valueChanges.subscribe(value => {

      const allowedWithoutSave = [1, 3, 5];
      if (allowedWithoutSave.includes(this.stage)) {
        this.isSaved = true;
      }
      // Age is stored in localStorage from Demographic component
      const storedAge = localStorage.getItem('Age');
      if (storedAge) {
        const age = JSON.parse(storedAge).age; // age in years
        this.ageInYears = age;            // age in years
        console.log('Age in months:', this.ageInYears);
      }
      if (value === 'No') {
        this.sleepForm.patchValue({
          patientId: this.patientId,
          sleepApneaFrequency: '',
          sleepApneaDuration: ''
        });
        this.sleepForm.get('sleepApneaFrequency')?.disable();
        this.sleepForm.get('sleepApneaDuration')?.disable();
        this.sleepForm.get('othersText')?.disable();
      } else if (value === 'Yes' && !this.isViewMode) {
        this.sleepForm.get('sleepApneaFrequency')?.enable();
        this.sleepForm.get('sleepApneaDuration')?.enable();
      }

      if (this.sleepForm.get('exerciseIntake')?.value === '') {
        this.sleepForm.get('othersText')?.disable();
      }
    });

    const exercises = ['jogging', 'gym', 'yoga', 'walking', 'aerobics', 'zumba', 'others'];
    exercises.forEach(ex => {
      this.sleepForm.get(ex)?.valueChanges.subscribe(value => {
        const freqCtrl = this.sleepForm.get(`${ex}Frequency`);
        const durCtrl = this.sleepForm.get(`${ex}Duration`);
        if (value === 'Yes' && !this.isViewMode) {
          freqCtrl?.enable();
          durCtrl?.enable();
          if (ex === 'others') this.sleepForm.get('othersText')?.enable();
        } else {
          freqCtrl?.reset();
          durCtrl?.reset();
          freqCtrl?.disable();
          durCtrl?.disable();

          if (this.sleepForm.get('others')?.value === 'No') {
            this.sleepForm.get('othersText')?.setValue('');
            this.sleepForm.get('othersText')?.disable();
          }
        }
      });
    });


    this.sleepForm.get('exerciseIntake')?.valueChanges.subscribe(value => {
      const fields = [
        'jogging', 'joggingFrequency', 'joggingDuration',
        'gym', 'gymFrequency', 'gymDuration',
        'yoga', 'yogaFrequency', 'yogaDuration',
        'walking', 'walkingFrequency', 'walkingDuration',
        'aerobics', 'aerobicsFrequency', 'aerobicsDuration',
        'zumba', 'zumbaFrequency', 'zumbaDuration',
        'others', 'othersFrequency', 'othersDuration', 'othersText'
      ];

      if (value === 'No') {
        fields.forEach(field => {
          this.sleepForm.get(field)?.reset();
          this.sleepForm.get(field)?.disable();
        });
      }
      else if (value === 'Yes' && !this.isViewMode) {
        const radioFields = ['jogging', 'gym', 'yoga', 'walking', 'aerobics', 'zumba', 'others'];
        radioFields.forEach(field => {
          this.sleepForm.get(field)?.enable();
        });

        if (this.sleepForm.get('others')?.value === 'Yes') {
          this.sleepForm.get('othersText')?.enable();
        } else {
          this.sleepForm.get('othersText')?.disable();
        }
      }
      else {
        fields.forEach(field => this.sleepForm.get(field)?.disable());
      }
    });




    this.fetchSleepData(this.patientId, this.stage);
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



  blockInvalidKeys(event: KeyboardEvent) {
    if (['e', 'E', '+', '-', '.'].includes(event.key)) {
      event.preventDefault();
    }
  }
  preventNegative(event: any) {

    if (event.target.value < 0) {
      event.target.value = 0; // reset to 0 if negative
    }
  }

  fetchSleepData(patientId: number, stage: number): void {
    this.sleepForm.reset();
    this.sleepService.getSleepByPatientId(patientId, stage).subscribe({
      next: (res: any) => {
        if (res.type === 'S' && res.data) {
          const d = res.data;
          this.sleepForm.patchValue({
            sleepApnea: d.sleepApneayes ? 'Yes' : (d.sleepApneano ? 'No' : ''),
            sleepApneaFrequency: d.sleepApneaFrequency ?? '',
            sleepApneaDuration: d.sleepApneaDuration ?? '',
            exerciseIntake: d.exerciseIntakeyes ? 'Yes' : (d.exerciseIntakeno ? 'No' : ''),
            jogging: d.joggingSelectedyes ? 'Yes' : (d.joggingSelectedno ? 'No' : ''),
            joggingFrequency: d.joggingFrequency ?? '',
            joggingDuration: d.joggingDuration ?? '',
            gym: d.gymSelectedyes ? 'Yes' : (d.gymSelectedno ? 'No' : ''),
            gymFrequency: d.gymFrequency ?? '',
            gymDuration: d.gymDuration ?? '',
            yoga: d.yogaSelectedyes ? 'Yes' : (d.yogaSelectedno ? 'No' : ''),
            yogaFrequency: d.yogaFrequency ?? '',
            yogaDuration: d.yogaDuration ?? '',
            walking: d.walkingSelectedyes ? 'Yes' : (d.walkingSelectedno ? 'No' : ''),
            walkingFrequency: d.walkingFrequency ?? '',
            walkingDuration: d.walkingDuration ?? '',
            aerobics: d.aerobicsyes ? 'Yes' : (d.aerobicsno ? 'No' : ''),
            aerobicsFrequency: d.aerobicsFrequency ?? '',
            aerobicsDuration: d.aerobicsDuration ?? '',
            othersText: d.othersText ?? '',
            zumba: d.zumbayes ? 'Yes' : (d.zumbano ? 'No' : ''),
            zumbaFrequency: d.zumbaFrequency ?? '',
            zumbaDuration: d.zumbaDuration ?? '',

            others: d.othersyes ? 'Yes' : (d.othersno ? 'No' : ''),
            othersFrequency: d.othersFrequency ?? '',
            othersDuration: d.othersDuration ?? ''
          });

          if (this.isViewMode) this.sleepForm.disable();
        }
      },
      error: (err) => {
        console.error('❌ Error fetching sleep data:', err);
      }
    });
  }

  fieldsVal: { [key: string]: boolean | undefined } = {
    jogging: undefined,
    gym: undefined,
    yoga: undefined,
    walking: undefined,
    aerobics: undefined,
    zumba: undefined,
    others: undefined
  };


  selectedfld: number = 0;


  validateFlds(): boolean {
    const missingVal: string[] = [];
    let anyExerciseSelected = false;  // ✅ declare here

    for (const key of Object.keys(this.fieldsVal)) {
      const isYes = this.sleepForm.get(key)?.value === 'Yes';

      if (isYes) {
        anyExerciseSelected = true;   // at least one exercise selected
        const freq = this.sleepForm.get(`${key}Frequency`)?.value;
        const dur = this.sleepForm.get(`${key}Duration`)?.value;

        if (!freq || !dur) {
          missingVal.push(key.charAt(0).toUpperCase() + key.slice(1));
        }
      }
    }

    if (this.sleepForm.get('exerciseIntake')?.value === 'Yes' && !anyExerciseSelected) {
      alert('Please select at least one exercise');
      return false;
    }

    if (missingVal.length > 0) {
      alert("Please fill frequency and duration fields for: " + missingVal.join(", "));
      return false;
    }

    return true;
  }


  // onSave() {

  //   if (!this.formValidation.validateForm(this.sleepForm)) {
  //     this.sleepForm.markAllAsTouched();
  //     return;
  //   }

  //   const f = this.sleepForm.value;
  //   const sleepval = this.sleepForm.get('sleepApnea')?.value;
  //   const sleepfreq = this.sleepForm.get('sleepApneaFrequency')?.value;
  //   const sleepdura = this.sleepForm.get('sleepApneaDuration')?.value;
  //   if (sleepval === '' || sleepval === null) {
  //     alert('Select Sleep Apnea')
  //     return;
  //   }
  //   else {
  //     if (sleepval === 'Yes') {
  //       if (sleepfreq === '' || sleepfreq === null) {
  //         alert('Enter Sleep Frequency')
  //         return;
  //       }
  //       if (sleepdura === '' || sleepdura === null) {
  //         alert('Enter Sleep Duration')
  //         return;
  //       }
  //     }

  //   }
  //   const exval = this.sleepForm.get('exerciseIntake')?.value;
  //   if (exval === '' || exval === null) {
  //     alert('Select Excercise')
  //     return;
  //   }

  //   if (!this.validateFlds()) {
  //     return;
  //   }
  //   const now = new Date().toISOString();
  //   let user: any = localStorage.getItem('doctor')
  //   this.userData = JSON.parse(user);
  //   const param = {


  //     Flag: 'I',
  //     Id: 0,
  //     PatientId: this.patientId,
  //     Stage: this.stage,
  //     sleepApneayes: f.sleepApnea === 'Yes' ? 'Yes' : '',
  //     sleepApneano: f.sleepApnea === 'No' ? 'No' : '',
  //     sleepApneaFrequency: f.sleepApneaFrequency ?? '',
  //     sleepApneaDuration: f.sleepApneaDuration ?? '',

  //     exerciseIntakeyes: f.exerciseIntake === 'Yes' ? 'Yes' : '',
  //     exerciseIntakeno: f.exerciseIntake === 'No' ? 'No' : '',
  //     joggingSelectedyes: f.jogging === 'Yes' ? 'Yes' : '',
  //     joggingSelectedno: f.jogging === 'No' ? 'No' : '',      
  //     joggingFrequency: f.joggingFrequency ? String(f.joggingFrequency) : '',
  //     joggingDuration: f.joggingDuration ? String(f.joggingDuration) : '',
  //     gymSelectedyes: f.gym === 'Yes' ? 'Yes' : '',
  //     gymSelectedno: f.gym === 'No' ? 'No' : '',
  //     gymFrequency: f.gymFrequency  ? String(f.gymFrequency) : '',
  //     gymDuration: f.gymDuration  ? String(f.gymDuration) : '',
  //     yogaSelectedyes: f.yoga === 'Yes' ? 'Yes' : '',
  //     yogaSelectedno: f.yoga === 'No' ? 'No' : '',
  //     yogaFrequency: f.yogaFrequency ? String(f.yogaFrequency) : '',
  //     yogaDuration: f.yogaDuration ? String(f.yogaDuration) : '',
  //     walkingSelectedyes: f.walking === 'Yes' ? 'Yes' : '',
  //     walkingSelectedno: f.walking === 'No' ? 'No' : '',
  //     walkingFrequency: f.walkingFrequency ? String(f.walkingFrequency) : '',
  //     walkingDuration: f.walkingDuration ? String(f.walkingDuration) : '',
  //     aerobicsyes: f.aerobics === 'Yes' ? 'Yes' : '',
  //     aerobicsno: f.aerobics === 'No' ? 'No' : '',
  //     aerobicsFrequency: f.aerobicsFrequency ? String(f.aerobicsFrequency) : '',
  //     aerobicsDuration: f.aerobicsDuration ? String(f.aerobicsDuration) : '',
  //     zumbayes: f.zumba === 'Yes' ? 'Yes' : '',
  //     zumbano: f.zumba === 'No' ? 'No' : '',
  //     zumbaFrequency: f.zumbaFrequency ? String(f.zumbaFrequency) : '',
  //     zumbaDuration: f.zumbaDuration ? String(f.zumbaDuration) : '',
  //     othersText: f.othersText ?? '',
  //     othersyes: f.others === 'Yes' ? 'Yes' : '',
  //     othersno: f.others === 'No' ? 'No' : '',
  //     othersFrequency: f.othersFrequency ? String(f.othersFrequency) : '',
  //     othersDuration: f.othersDuration ? String(f.othersDuration) : '',
  //     CreatedBy: this.userData?.doctorId,
  //     CreatedAt: now,
  //     ModifiedDt: now
  //   };
  //   this.http.httpPost(API_URLS.SLEEP_SAVE, param).subscribe({
  //     next: (res: any) => {
  //       if (res.type === 'S') {
  //         alert('Save Successfully')
  //         this.formValidation.showAlert('Saved Successfully', 'success');
  //         this.isSaved = true;
  //       } else {
  //         this.formValidation.showAlert('Error saving data!', 'danger');
  //       }
  //     },
  //     error: (err) => {
  //       console.error('Save error:', err);
  //       this.formValidation.showAlert('Network or server error during save.', 'danger');
  //     }
  //   });
  // }

  onSave() {

    if (!this.formValidation.validateForm(this.sleepForm)) {
      this.sleepForm.markAllAsTouched();
      return;
    }

    const f = this.sleepForm.value;
    const sleepval = this.sleepForm.get('sleepApnea')?.value;
    const sleepfreq = this.sleepForm.get('sleepApneaFrequency')?.value;
    const sleepdura = this.sleepForm.get('sleepApneaDuration')?.value;
    if (sleepval === '' || sleepval === null) {
      alert('Select Sleep Apnea')
      return;
    }
    else {
      if (sleepval === 'Yes') {
        if (sleepfreq === '' || sleepfreq === null) {
          alert('Enter Sleep Frequency')
          return;
        }
        if (sleepdura === '' || sleepdura === null) {
          alert('Enter Sleep Duration')
          return;
        }
      }

    }
    const exval = this.sleepForm.get('exerciseIntake')?.value;
    if (exval === '' || exval === null) {
      alert('Select Excercise')
      return;
    }

    if (!this.validateFlds()) {
      return;
    }
    const now = new Date().toISOString();
    let user: any = localStorage.getItem('doctor')
    this.userData = JSON.parse(user);
    const param = {


      Flag: 'I',
      Id: 0,
      PatientId: this.patientId,
      Stage: this.stage,
      sleepApneayes: f.sleepApnea === 'Yes' ? 'Yes' : '',
      sleepApneano: f.sleepApnea === 'No' ? 'No' : '',
      sleepApneaFrequency: f.sleepApneaFrequency ? String(f.sleepApneaFrequency) : '',
      sleepApneaDuration: f.sleepApneaDuration ? String(f.sleepApneaDuration) : '',

      exerciseIntakeyes: f.exerciseIntake === 'Yes' ? 'Yes' : '',
      exerciseIntakeno: f.exerciseIntake === 'No' ? 'No' : '',
      joggingSelectedyes: f.jogging === 'Yes' ? 'Yes' : '',
      joggingSelectedno: f.jogging === 'No' ? 'No' : '',
      joggingFrequency: f.joggingFrequency ? String(f.joggingFrequency) : '',
      joggingDuration: f.joggingDuration ? String(f.joggingDuration) : '',
      gymSelectedyes: f.gym === 'Yes' ? 'Yes' : '',
      gymSelectedno: f.gym === 'No' ? 'No' : '',
      gymFrequency: f.gymFrequency ? String(f.gymFrequency) : '',
      gymDuration: f.gymDuration ? String(f.gymDuration) : '',
      yogaSelectedyes: f.yoga === 'Yes' ? 'Yes' : '',
      yogaSelectedno: f.yoga === 'No' ? 'No' : '',
      yogaFrequency: f.yogaFrequency ? String(f.yogaFrequency) : '',
      yogaDuration: f.yogaDuration ? String(f.yogaDuration) : '',
      walkingSelectedyes: f.walking === 'Yes' ? 'Yes' : '',
      walkingSelectedno: f.walking === 'No' ? 'No' : '',
      walkingFrequency: f.walkingFrequency ? String(f.walkingFrequency) : '',
      walkingDuration: f.walkingDuration ? String(f.walkingDuration) : '',
      aerobicsyes: f.aerobics === 'Yes' ? 'Yes' : '',
      aerobicsno: f.aerobics === 'No' ? 'No' : '',
      aerobicsFrequency: f.aerobicsFrequency ? String(f.aerobicsFrequency) : '',
      aerobicsDuration: f.aerobicsDuration ? String(f.aerobicsDuration) : '',
      zumbayes: f.zumba === 'Yes' ? 'Yes' : '',
      zumbano: f.zumba === 'No' ? 'No' : '',
      zumbaFrequency: f.zumbaFrequency ? String(f.zumbaFrequency) : '',
      zumbaDuration: f.zumbaDuration ? String(f.zumbaDuration) : '',
      othersText: f.othersText ?? '',
      othersyes: f.others === 'Yes' ? 'Yes' : '',
      othersno: f.others === 'No' ? 'No' : '',
      othersFrequency: f.othersFrequency ? String(f.othersFrequency) : '',
      othersDuration: f.othersDuration ? String(f.othersDuration) : '',
      CreatedBy: this.userData?.doctorId,
      CreatedAt: now,
      ModifiedDt: now
    };

    const enteredsleepApneaDuration = Number(param.sleepApneaDuration);
    const sleepApneaFrequency = Number(param.sleepApneaFrequency);
    const enteredjoggingDuration = Number(param.joggingDuration);
    const enteredgymDuration = Number(param.gymDuration);
    const enteredyogaDuration = Number(param.yogaDuration);
    const enteredwalkingDuration = Number(param.walkingDuration);
    const enteredaerobicsDuration = Number(param.aerobicsDuration);
    const enteredzumbaDuration = Number(param.zumbaDuration);
    const enteredothersDuration = Number(param.othersDuration);
    const enteredjoggingFrequency = Number(param.joggingFrequency);
    const enteredgymFrequency = Number(param.gymFrequency);
    const enteredyogaFrequency = Number(param.yogaFrequency);
    const enteredwalkingFrequency = Number(param.walkingFrequency);
    const enteredaerobicsFrequency = Number(param.aerobicsFrequency);
    const enteredzumbaFrequency = Number(param.zumbaFrequency);
    const enteredothersFrequency = Number(param.othersFrequency);

      //maximum possible hours per week = 24*7=168
    if (sleepApneaFrequency > 168 || 
      enteredjoggingFrequency > 168 ||
      enteredgymFrequency > 168 ||
      enteredyogaFrequency > 168 ||
      enteredwalkingFrequency > 168 ||
      enteredaerobicsFrequency > 168 ||
      enteredzumbaFrequency > 168 ||
      enteredothersFrequency > 168
    ) {
      alert('Entered Frequency cannot exceed the maximum possible hours per week 168 . Please correct the value.');
      return;
    }
    // Check if any entered duration exceeds age in years
    if (
      enteredsleepApneaDuration > this.ageInYears ||
      enteredjoggingDuration > this.ageInYears ||
      enteredgymDuration > this.ageInYears ||
      enteredyogaDuration > this.ageInYears ||
      enteredwalkingDuration > this.ageInYears ||
      enteredaerobicsDuration > this.ageInYears ||
      enteredzumbaDuration > this.ageInYears ||
      enteredothersDuration > this.ageInYears
    ) {

      alert('Entered duration exceeds the person’s age (' + this.ageInYears + ' years). Please enter valid values.');
      return;
    }

    this.http.httpPost(API_URLS.SLEEP_SAVE, param).subscribe({
      next: (res: any) => {
        if (res.type === 'S') {
          alert('Save Successfully')
          this.formValidation.showAlert('Saved Successfully', 'success');
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


  onNext(): void {
    //const pid = this.patientService.getPatientId();
    this.router.navigate([`/gadget/${this.patientId}/${this.stage}`], {
      state: {
        patientId: this.patientId,
        tabId: this.tabId,
        stage: this.stage
      }
    });
  }
  OnNext(): void {
    //const pid = this.patientService.getPatientId();
    this.router.navigate([`/gadget/${this.patientId}/${this.stage}`], {
      state: {
        patientId: this.patientId,
        tabId: this.tabId,
        stage: this.stage
      }
    });
  }
  goback(): void {
    this.router.navigate([`/Personal-history/${this.patientId}/${this.stage}`], {
      state: {
        patientId: this.patientId,
        tabId: this.tabId,
        stage: this.stage
      },
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

  // === GETTERS ===
  get sleepApneaYes() {
    return this.sleepForm.get('sleepApnea')?.value === 'Yes';
  }

  get exerciseSelectedYes() {
    return this.sleepForm.get('exerciseIntake')?.value === 'Yes';
  }

  get joggingSelectedyes() {
    return this.sleepForm.get('jogging')?.value === 'Yes';
  }

  get gymSelectedyes() {
    return this.sleepForm.get('gym')?.value === 'Yes';
  }

  get yogaSelectedyes() {
    return this.sleepForm.get('yoga')?.value === 'Yes';
  }

  get walkingSelectedyes() {
    return this.sleepForm.get('walking')?.value === 'Yes';
  }

  get aerobicsyes() {
    return this.sleepForm.get('aerobics')?.value === 'Yes';
  }

  get zumbayes() {
    return this.sleepForm.get('zumba')?.value === 'Yes';
  }

  get othersyes() {
    return this.sleepForm.get('others')?.value === 'Yes';

  }



}

