import { Component, Input, OnInit } from '@angular/core';
import { ActivatedRoute, NavigationEnd, Router } from '@angular/router';
import { FormvalidationService } from '../formvalidation.service';
import { HttpserviceService } from '../httpservice.service';
import { PatientHistoryService } from '../Services/patient-history.service';
import { HistoryService } from '../Services/history.servie';
import { error } from 'node:console';
import { PatientService } from '../Services/patient.service';
import { FormGroup, FormControl } from '@angular/forms';
import { Validators } from '@angular/forms'; 

@Component({
  selector: 'app-personal-history',
  templateUrl: './personal-history.component.html',
  styleUrls: ['./personal-history.component.css']
})
export class PersonalHistoryComponent {
  @Input() patientId: number | null = null;
  doctorId: number | null = null;
  isViewMode = false;
  isFollowUp: boolean = false;
  // Define the states of intake
  tabId = 1;
  @Input() stage: number = 0;
  isSaved: boolean = false;
  @Input() isPrintMode = false;
  userData:any;

  intakeStates: { [key: string]: boolean | null } = {
    aerated: null,
    coffee: null,
    tea: null,
    spicy: null,
    alcohol: null,
    sweets: null,
    smoking: null,
    tobacco: null
  };


  formData = new FormGroup({
    aerated: new FormGroup({
      frequency: new FormControl(''),
      quantity: new FormControl(''),
      duration: new FormControl(''),
    }),
    coffee: new FormGroup({
      frequency: new FormControl(''),
      quantity: new FormControl(''),
      duration: new FormControl(''),
    }),
    tea: new FormGroup({
      frequency: new FormControl(''),
      quantity: new FormControl(''),
      duration: new FormControl(''),
    }),
    spicy: new FormGroup({
      frequency: new FormControl(''),
      quantity: new FormControl(''),
      duration: new FormControl(''),
    }),
    alcohol: new FormGroup({
      frequency: new FormControl(''),
      quantity: new FormControl(''),
      duration: new FormControl(''),
    }),
    sweets: new FormGroup({
      frequency: new FormControl(''),
      quantity: new FormControl(''),
      duration: new FormControl(''),
    }),
    smoking: new FormGroup({
      frequency: new FormControl(''),
      quantity: new FormControl(''),
      duration: new FormControl(''),
    }),
    tobacco: new FormGroup({
      frequency: new FormControl(''),
      quantity: new FormControl(''),
      duration: new FormControl(''),
    }),
  });


  constructor(
    private http: HttpserviceService,
    private formValidation: FormvalidationService,
    private router: Router,
    private route: ActivatedRoute,
    private historyService: HistoryService, private patientService: PatientService,
  ) { }



  ngOnInit(): void {
    // 1. Get values from route parameters (path)
    this.stage = Number(this.route.snapshot.params['stage'] || 0);
    this.route.paramMap.subscribe(params => {
      const allowedWithoutSave = [1, 3, 5];
      if (allowedWithoutSave.includes(this.stage)) {
        this.isSaved = true;
      }

      const pid = params.get('patientId');
      const stageVal = params.get('stage');

      this.patientId = pid ? parseInt(pid, 10) : null;
      this.stage = stageVal ? parseInt(stageVal, 10) : 0;

      if (!this.patientId) {
        console.warn('Missing patientId in route.');
        return;
      }

      this.loadExistingData(this.patientId, this.stage);

      const state = history.state;
      this.isViewMode = state?.isViewMode ?? false;
      this.tabId = state?.tabId ?? 1;
      //this.isFollowUp = this.router.url.includes('follow-up-1') || this.router.url.includes('follow-up-2');

      const cachedForm = this.patientService.getPersonalHistoryData();
      const cachedIntake = this.patientService.getPersonalHistoryIntakeStates();

      if (cachedForm && cachedIntake) {
        this.formData.patchValue(cachedForm);
        this.intakeStates = cachedIntake;

        // Enable or disable fields based on intake state
        Object.keys(this.intakeStates).forEach(key => {
          const controlGroup = this.formData.get(key);
          this.intakeStates[key] ? controlGroup?.enable() : controlGroup?.disable();
        });

        console.log('Restored from cache:', cachedForm, cachedIntake);
      } else {
        // 4. Fallback to API call
        //this.loadExistingData(this.patientService.getPatientId(), this.stage);
      }
    });

  }
  private loadExistingData(id: number, stage: number): void {
    this.http.httpGet(`/PersonalHistory/GetPersonalHistoryById/${id}/${stage}`).subscribe({
      next: (res: any) => {
        if (!res || !res.data) return;

        const data = res.data;
        
        console.log("data", data);
        // Update intakeStates (now using `data`)
        this.intakeStates['aerated'] = data.aeratedIntake === true;
        this.intakeStates['coffee'] = data.coffeeIntake === true;
        this.intakeStates['tea'] = data.teaIntake === true;
        this.intakeStates['spicy'] = data.spicyIntake === true;
        this.intakeStates['alcohol'] = data.alcoholIntake === true;
        this.intakeStates['sweets'] = data.sweetsIntake === true;
        this.intakeStates['smoking'] = data.smokingIntake === true;
        this.intakeStates['tobacco'] = data.tobaccoIntake === true;

        this.formData.patchValue({
          aerated: {
            frequency: data.aeratedFrequency || '',
            quantity: data.aeratedQuantity || '',
            duration: data.aeratedDuration || ''
          },
          coffee: {
            frequency: data.coffeeFrequency || '',
            quantity: data.coffeeQuantity || '',
            duration: data.coffeeDuration || ''
          },
          tea: {
            frequency: data.teaFrequency || '',
            quantity: data.teaQuantity || '',
            duration: data.teaDuration || ''
          },
          spicy: {
            frequency: data.spicyFrequency || '',
            quantity: data.spicyQuantity || '',
            duration: data.spicyDuration || ''
          },
          alcohol: {
            frequency: data.alcoholFrequency || '',
            quantity: data.alcoholQuantity || '',
            duration: data.alcoholDuration || ''
          },
          sweets: {
            frequency: data.sweetsFrequency || '',
            quantity: data.sweetsQuantity || '',
            duration: data.sweetsDuration || ''
          },
          smoking: {
            frequency: data.smokingFrequency || '',
            quantity: data.smokingQuantity || '',
            duration: data.smokingDuration || ''
          },
          tobacco: {
            frequency: data.tobaccoFrequency || '',
            quantity: data.tobaccoQuantity || '',
            duration: data.tobaccoDuration || ''
          }
        });

        Object.keys(this.intakeStates).forEach(key => {
          const group = this.formData.get(key);
          if (this.intakeStates[key]) {
            group?.enable();
          } else {
            group?.disable();
          }
        });

        // ✅ Store in cache
        this.patientService.setPersonalHistoryData(this.formData.value);
        this.patientService.setPersonalHistoryIntakeStates(this.intakeStates);


        console.log('Patched form values:', this.formData.value);
        console.log('Intake states updated:', this.intakeStates);
      },
      error: (err) => {
        this.formValidation.showAlert('Failed to load personal history data.', 'danger');
        console.error('Error loading personal history data:', err);
      }
    });
  }
  Submit(): void {
    const patientHistoryId = this.historyService.getPatientHistoryID();
    let user: any = localStorage.getItem('doctor')
    this.userData = JSON.parse(user);
    
    const payload = {
      flag: 'I',
      stage: this.stage,
      personalHistoryId: 0,
      doctorId: this.userData?.doctorId,
      patientId: this.patientService.getPatientId(),
      createdBy: this.userData?.doctorId,
      aeratedIntake: this.intakeStates['aerated'],
      aeratedFrequency: this.formData.get('aerated.frequency')?.value || '',
      aeratedQuantity: this.formData.get('aerated.quantity')?.value || '',
      aeratedDuration: this.formData.get('aerated.duration')?.value || '',

      coffeeIntake: this.intakeStates['coffee'],
      coffeeFrequency: this.formData.get('coffee.frequency')?.value || '',
      coffeeQuantity: this.formData.get('coffee.quantity')?.value || '',
      coffeeDuration: this.formData.get('coffee.duration')?.value || '',

      teaIntake: this.intakeStates['tea'],
      teaFrequency: this.formData.get('tea.frequency')?.value || '',
      teaQuantity: this.formData.get('tea.quantity')?.value || '',
      teaDuration: this.formData.get('tea.duration')?.value || '',

      spicyIntake: this.intakeStates['spicy'],
      spicyFrequency: this.formData.get('spicy.frequency')?.value || '',
      spicyQuantity: this.formData.get('spicy.quantity')?.value || '',
      spicyDuration: this.formData.get('spicy.duration')?.value || '',

      alcoholIntake: this.intakeStates['alcohol'],
      alcoholFrequency: this.formData.get('alcohol.frequency')?.value || '',
      alcoholQuantity: this.formData.get('alcohol.quantity')?.value || '',
      alcoholDuration: this.formData.get('alcohol.duration')?.value || '',

      sweetsIntake: this.intakeStates['sweets'],
      sweetsFrequency: this.formData.get('sweets.frequency')?.value || '',
      sweetsQuantity: this.formData.get('sweets.quantity')?.value || '',
      sweetsDuration: this.formData.get('sweets.duration')?.value || '',

      smokingIntake: this.intakeStates['smoking'],
      smokingFrequency: this.formData.get('smoking.frequency')?.value || '',
      smokingQuantity: this.formData.get('smoking.quantity')?.value || '',
      smokingDuration: this.formData.get('smoking.duration')?.value || '',

      tobaccoIntake: this.intakeStates['tobacco'],
      tobaccoFrequency: this.formData.get('tobacco.frequency')?.value || '',
      tobaccoQuantity: this.formData.get('tobacco.quantity')?.value || '',
      tobaccoDuration: this.formData.get('tobacco.duration')?.value || ''
    };
    this.http.httpPost('/PersonalHistory/SavePersonalHistory', payload).subscribe({
      next: () => {
        alert('Saved Successfully'); // ← Test this
        this.formValidation.showAlert('Saved Successfully', 'success');
        this.isSaved = true;

        this.formValidation.showAlert('Personal history saved successfully.', 'success');
        this.patientService.setPersonalHistoryData(this.formData.value); // Store form
        this.patientService.setPersonalHistoryIntakeStates(this.intakeStates);

      },

      error: (err) => {
        console.error('Failed to save personal history:', err);
        this.formValidation.showAlert('Failed to save personal history.', 'danger');
      }
    });
  }
  onNext(): void {
    const patientHistoryId = this.historyService.getPatientHistoryID();
    this.router.navigate([`sleep/${this.patientId}/${this.stage}`], {
     
      state: {
        tabId: this.tabId,
        stage: this.stage,
        isViewMode: this.isViewMode

      }
    });
  }
  OnNext(): void {
    const patientHistoryId = this.historyService.getPatientHistoryID();
    this.router.navigate([`/sleep/${this.patientId}/${this.stage}`], {
     
      state: {
        tabId: this.tabId,
        stage: this.stage,
        isViewMode: this.isViewMode

      }
    });
  }


  back() {
    this.router.navigate([`/history/${this.patientId}/${this.stage}`], {
      state: {
        tabId: this.tabId,
        patientId: this.patientId,
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

  // Update this method in your component:

  onSelect(item: string, isYes: boolean): void {
    this.intakeStates[item] = isYes;
    const group = this.formData.get(item) as FormGroup;

    if (!isYes) {
      group.patchValue({ frequency: '', quantity: '', duration: '' });
      group.disable();
    } else {
      group.enable();
    }
  }


}

