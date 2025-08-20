import { Component, Input, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { HttpserviceService } from '../httpservice.service';
import { FormvalidationService } from '../formvalidation.service';
import { HistoryService } from '../Services/history.servie';
import { FormBuilder, FormGroup } from '@angular/forms';
import { API_URLS } from '../shared/API-URLs';
import { PatientService } from '../Services/patient.service';
import { sleepService } from '../Services/Sleep.service';

@Component({
  selector: 'app-sleep',
  templateUrl: './sleep.component.html',
  styleUrls: ['./sleep.component.css']
})
export class SleepComponent implements OnInit {
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
      patientId: [this.patientService.getPatientId()],
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

  ngOnInit(): void {
    this.patientId = this.route.snapshot.params['patientId'];
    this.stage = Number(this.route.snapshot.params['stage'] || 0);
    this.sleepForm.get('sleepApnea')?.valueChanges.subscribe(value => {

      const allowedWithoutSave = [1, 3, 5];
      if (allowedWithoutSave.includes(this.stage)) {
        this.isSaved = true;
      }
      if (value === 'no') {
        this.sleepForm.patchValue({
          sleepApneaFrequency: '',
          sleepApneaDuration: ''
        });
        this.sleepForm.get('sleepApneaFrequency')?.disable();
        this.sleepForm.get('sleepApneaDuration')?.disable();
      } else if (value === 'yes' && !this.isViewMode) {
        this.sleepForm.get('sleepApneaFrequency')?.enable();
        this.sleepForm.get('sleepApneaDuration')?.enable();
      }
    });

    // Enable/Disable logic per exercise
    const exercises = ['jogging', 'gym', 'yoga', 'walking', 'aerobics', 'zumba', 'others'];
    exercises.forEach(ex => {
      this.sleepForm.get(ex)?.valueChanges.subscribe(value => {
        const freqCtrl = this.sleepForm.get(`${ex}Frequency`);
        const durCtrl = this.sleepForm.get(`${ex}Duration`);
        if (value === 'yes' && !this.isViewMode) {
          freqCtrl?.enable();
          durCtrl?.enable();
        } else {
          freqCtrl?.reset();
          durCtrl?.reset();
          freqCtrl?.disable();
          durCtrl?.disable();
        }
      });
    });

    // Disable entire exercise block if intake = 'no'
    this.sleepForm.get('exerciseIntake')?.valueChanges.subscribe(value => {
      const fields = [
        'jogging', 'joggingFrequency', 'joggingDuration',
        'gym', 'gymFrequency', 'gymDuration',
        'yoga', 'yogaFrequency', 'yogaDuration',
        'walking', 'walkingFrequency', 'walkingDuration',
        'aerobics', 'aerobicsFrequency', 'aerobicsDuration',
        'zumba', 'zumbaFrequency', 'zumbaDuration',
        'others', 'othersFrequency', 'othersDuration'
      ];
      if (value === 'no') {
        fields.forEach(field => {
          this.sleepForm.get(field)?.reset();
          this.sleepForm.get(field)?.disable();
        });
      } else if (value === 'yes' && !this.isViewMode) {
        const radioFields = ['jogging', 'gym', 'yoga', 'walking', 'aerobics', 'zumba', 'others'];
        radioFields.forEach(field => {
          this.sleepForm.get(field)?.enable();
        });
      }
    });

    // const currentUrl = this.router.url;
    // const state = history.state;

    this.fetchSleepData(this.patientService.getPatientId(), this.stage);
  }

  fetchSleepData(patientId: number, stage: number): void {
    this.sleepService.getSleepByPatientId(patientId,stage).subscribe({
      next: (res: any) => {
        if (res.type === 'S' && res.data) {
          const d = res.data;
          this.sleepForm.patchValue({
            sleepApnea: d.sleepApneayes ? 'yes' : (d.sleepApneano ? 'no' : ''),
            sleepApneaFrequency: d.sleepApneaFrequency ?? '',
            sleepApneaDuration: d.sleepApneaDuration ?? '',
            exerciseIntake: d.exerciseIntakeyes ? 'yes' : (d.exerciseIntakeno ? 'no' : ''),
            jogging: d.joggingSelectedyes ? 'yes' : (d.joggingSelectedno ? 'no' : ''),
            joggingFrequency: d.joggingFrequency ?? '',
            joggingDuration: d.joggingDuration ?? '',
            gym: d.gymSelectedyes ? 'yes' : (d.gymSelectedno ? 'no' : ''),
            gymFrequency: d.gymFrequency ?? '',
            gymDuration: d.gymDuration ?? '',
            yoga: d.yogaSelectedyes ? 'yes' : (d.yogaSelectedno ? 'no' : ''),
            yogaFrequency: d.yogaFrequency ?? '',
            yogaDuration: d.yogaDuration ?? '',
            walking: d.walkingSelectedyes ? 'yes' : (d.walkingSelectedno ? 'no' : ''),
            walkingFrequency: d.walkingFrequency ?? '',
            walkingDuration: d.walkingDuration ?? '',
            aerobics: d.aerobicsyes ? 'yes' : (d.aerobicsno ? 'no' : ''),
            aerobicsFrequency: d.aerobicsFrequency ?? '',
            othersText: d.othersText ?? '',
            zumba: d.zumbayes ? 'yes' : (d.zumbano ? 'no' : ''),
            zumbaFrequency: d.zumbaFrequency ?? '',
            zumbaDuration: d.zumbaDuration ?? '',
            others: d.othersyes ? 'yes' : (d.othersno ? 'no' : ''),
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


  // private loadExistingData(id: number): void {
  //   this.http.httpGet(`/PatientHistory/GetPatientHistory?patientHistoryID=${id}`).subscribe({
  //     next: (res: any) => {
  //       this.sleepForm.patchValue({
  //         sleepApnea: res.sleepApnea_Intake || '',
  //         sleepApneaFrequency: res.sleepApnea_Frequency ?? '',
  //         sleepApneaDuration: res.sleepApnea_Duration ?? '',
  //         exerciseIntake: res.exercise_Intake || '',
  //         jogging: res.jogging_Intake || '',
  //         joggingFrequency: res.jogging_Frequency ?? '',
  //         joggingDuration: res.jogging_Duration ?? '',
  //         gym: res.gym_Intake || '',
  //         gymFrequency: res.gym_Frequency ?? '',
  //         gymDuration: res.gym_Duration ?? '',
  //         yoga: res.yoga_Intake || '',
  //         yogaFrequency: res.yoga_Frequency ?? '',
  //         yogaDuration: res.yoga_Duration ?? '',
  //         walking: res.walking_Intake || '',
  //         walkingFrequency: res.walking_Frequency ?? '',
  //         walkingDuration: res.walking_Duration ?? '',
  //         aerobics: res.aerobics_Intake || '',
  //         aerobicsFrequency: res.aerobics_Frequency ?? '',
  //         aerobicsDuration: res.aerobics_Duration ?? '',
  //         zumba: res.zumba_Intake || '',
  //         zumbaFrequency: res.zumba_Frequency ?? '',
  //         zumbaDuration: res.zumba_Duration ?? '',
  //         othersText: res.othersText ?? '',
  //         others: res.others_Intake || '',
  //         othersFrequency: res.others_Frequency ?? '',
  //         othersDuration: res.others_Duration ?? ''
  //       });
  //     },
  //     error: () => {
  //       this.formValidation.showAlert('Failed to load sleep and exercise data.', 'danger');
  //     }
  //   });
  // }

  onSave() {
    if (!this.formValidation.validateForm(this.sleepForm)) {
      this.sleepForm.markAllAsTouched();
      return;
    }

    const f = this.sleepForm.value;
    const now = new Date().toISOString();
    let user: any = localStorage.getItem('doctor')
    this.userData = JSON.parse(user);
    const param = {


      Flag: 'I',
      Id: 0,
      PatientId: this.patientService.getPatientId(),
      Stage: this.stage,
      sleepApneayes: f.sleepApnea === 'yes' ? 'yes' : '',
      sleepApneano: f.sleepApnea === 'no' ? 'no' : '',
      sleepApneaFrequency: f.sleepApneaFrequency ?? '',
      sleepApneaDuration: f.sleepApneaDuration ?? '',
      exerciseIntakeyes: f.exerciseIntake === 'yes' ? 'yes' : '',
      exerciseIntakeno: f.exerciseIntake === 'no' ? 'no' : '',
      joggingSelectedyes: f.jogging === 'yes' ? 'yes' : '',
      joggingSelectedno: f.jogging === 'no' ? 'no' : '',
      joggingFrequency: f.joggingFrequency ?? '',
      joggingDuration: f.joggingDuration ?? '',
      gymSelectedyes: f.gym === 'yes' ? 'yes' : '',
      gymSelectedno: f.gym === 'no' ? 'no' : '',
      gymFrequency: f.gymFrequency ?? '',
      gymDuration: f.gymDuration ?? '',
      yogaSelectedyes: f.yoga === 'yes' ? 'yes' : '',
      yogaSelectedno: f.yoga === 'no' ? 'no' : '',
      yogaFrequency: f.yogaFrequency ?? '',
      yogaDuration: f.yogaDuration ?? '',
      walkingSelectedyes: f.walking === 'yes' ? 'yes' : '',
      walkingSelectedno: f.walking === 'no' ? 'no' : '',
      walkingFrequency: f.walkingFrequency ?? '',
      walkingDuration: f.walkingDuration ?? '',
      aerobicsyes: f.aerobics === 'yes' ? 'yes' : '',
      aerobicsno: f.aerobics === 'no' ? 'no' : '',
      aerobicsFrequency: f.aerobicsFrequency ?? '',
      aerobicsDuration: f.aerobicsDuration ?? '',
      zumbayes: f.zumba === 'yes' ? 'yes' : '',
      zumbano: f.zumba === 'no' ? 'no' : '',
      zumbaFrequency: f.zumbaFrequency ?? '',
      zumbaDuration: f.zumbaDuration ?? '',
      othersText: f.othersText ?? '',
      othersyes: f.others === 'yes' ? 'yes' : '',
      othersno: f.others === 'no' ? 'no' : '',
      othersFrequency: f.othersFrequency ?? '',
      othersDuration: f.othersDuration ?? '',
      CreatedBy: this.userData?.doctorId,
      CreatedAt: now,
      ModifiedDt: now
    };
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
    const pid = this.patientService.getPatientId();
    this.router.navigate([`/gadget/${pid}/${this.stage}`], {
      state: {
        patientId: pid,
        tabId: this.tabId,
        stage: this.stage
      }
    });
  }
  OnNext(): void {
    const pid = this.patientService.getPatientId();
    this.router.navigate([`/gadget/${pid}/${this.stage}`], {
      state: {
        patientId: pid,
        tabId: this.tabId,
        stage: this.stage
      }
    });
  }
  goback(): void {
    this.router.navigate([`/Personal-history/${this.patientId}/${this.stage}`], {
      queryParams: {
        // id:  this.patientId,
        patientId: this.patientId,
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
    return this.sleepForm.get('sleepApnea')?.value === 'yes';
  }

  get exerciseSelectedYes() {
    return this.sleepForm.get('exerciseIntake')?.value === 'yes';
  }

  get joggingSelectedyes() {
    return this.sleepForm.get('jogging')?.value === 'yes';
  }

  get gymSelectedyes() {
    return this.sleepForm.get('gym')?.value === 'yes';
  }

  get yogaSelectedyes() {
    return this.sleepForm.get('yoga')?.value === 'yes';
  }

  get walkingSelectedyes() {
    return this.sleepForm.get('walking')?.value === 'yes';
  }

  get aerobicsyes() {
    return this.sleepForm.get('aerobics')?.value === 'yes';
  }

  get zumbayes() {
    return this.sleepForm.get('zumba')?.value === 'yes';
  }

  get othersyes() {
    return this.sleepForm.get('others')?.value === 'yes';
  }

}

