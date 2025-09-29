import { Component, Input, OnInit, SimpleChanges } from '@angular/core';
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
  @Input() data: any;
  @Input() isPrintMode: boolean = false;
  userData: any;

  intakeStates: { [key: string]: boolean | undefined } = {
    aerated: undefined,
    coffee: undefined,
    tea: undefined,
    spicy: undefined,
    alcohol: undefined,
    sweets: undefined,
    smoking: undefined,
    tobacco: undefined
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
  ) { 
    
  }

  ngOnChanges(changes: SimpleChanges) {
    if (changes['data'] && this.data) {
      this.patchPersonalHistory(this.data);
    }
  }

  private patchPersonalHistory(data: any) {
    // update intake states
    this.intakeStates['aerated'] = data.aeratedIntake;
    this.intakeStates['coffee'] = data.coffeeIntake;
    this.intakeStates['tea'] = data.teaIntake;
    this.intakeStates['spicy'] = data.spicyIntake;
    this.intakeStates['alcohol'] = data.alcoholIntake;
    this.intakeStates['sweets'] = data.sweetsIntake;
    this.intakeStates['smoking'] = data.smokingIntake;
    this.intakeStates['tobacco'] = data.tobaccoIntake;

    // patch only frequency/quantity/duration
    this.formData.patchValue({
      aerated: {
        frequency: data.aeratedFrequency,
        quantity: data.aeratedQuantity,
        duration: data.aeratedDuration
      },
      coffee: {
        frequency: data.coffeeFrequency,
        quantity: data.coffeeQuantity,
        duration: data.coffeeDuration
      },
      tea: {
        frequency: data.teaFrequency,
        quantity: data.teaQuantity,
        duration: data.teaDuration
      },
      spicy: {
        frequency: data.spicyFrequency,
        quantity: data.spicyQuantity,
        duration: data.spicyDuration
      },
      alcohol: {
        frequency: data.alcoholFrequency,
        quantity: data.alcoholQuantity,
        duration: data.alcoholDuration
      },
      sweets: {
        frequency: data.sweetsFrequency,
        quantity: data.sweetsQuantity,
        duration: data.sweetsDuration
      },
      smoking: {
        frequency: data.smokingFrequency,
        quantity: data.smokingQuantity,
        duration: data.smokingDuration
      },
      tobacco: {
        frequency: data.tobaccoFrequency,
        quantity: data.tobaccoQuantity,
        duration: data.tobaccoDuration
      }
    });
  }

  
  ngOnInit(): void {

    this.patientId = Number(this.route.snapshot.params['patientId'] || this.patientService.getPatientId());
    this.stage = Number(this.route.snapshot.params['stage'] || 0);
    this.route.paramMap.subscribe(params => {
      const allowedWithoutSave = [1, 3, 5];
      if (allowedWithoutSave.includes(this.stage)) {
        this.isSaved = true;
      }
      this.loadExistingData(Number(this.patientId), this.stage);
      this.isViewMode = this.isViewMode ?? false;
      
    });

  }
  private loadExistingData(id: number, stage: number): void {
    this.http.httpGet(`/PersonalHistory/GetPersonalHistoryById/${id}/${stage}`).subscribe({
      next: (res: any) => {
        if (!res || !res.data) return;

        const data = res.data;

        console.log("data", data);
        // Update intakeStates (now using `data`)
        this.intakeStates['aerated'] = data.aeratedIntake===true;
        this.intakeStates['coffee'] = data.coffeeIntake===true;
        this.intakeStates['tea'] = data.teaIntake===true;
        this.intakeStates['spicy'] = data.spicyIntake===true;
        this.intakeStates['alcohol'] = data.alcoholIntake===true;
        this.intakeStates['sweets'] = data.sweetsIntake===true;
        this.intakeStates['smoking'] = data.smokingIntake===true;
        this.intakeStates['tobacco'] = data.tobaccoIntake===true;

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
        //this.patientService.setPersonalHistoryData(this.formData.value);
        //this.patientService.setPersonalHistoryIntakeStates(this.intakeStates);


        // console.log('Patched form values:', this.formData.value);
        // console.log('Intake states updated:', this.intakeStates);
      },
      error: (err) => {
        this.formValidation.showAlert('Failed to load personal history data.', 'danger');
        console.error('Error loading personal history data:', err);
      }
    });
  }

  
  validateFlds(): boolean {
  const missingVal: string[] = [];

  for (const key of Object.keys(this.intakeStates)) {
    const state = this.intakeStates[key];

   
    if (state === null || state === undefined) {
      alert('Please select Yes/No for ' + key);
      return false;  
    }

   if (state === true) {
      const group = this.formData.get(key) as FormGroup;

      if (!group) {
        continue;
      }

      const { frequency, quantity, duration } = group.value;

      if (!frequency || !quantity || !duration) {
        missingVal.push(key);
      }
    }
  }

  if (missingVal.length > 0) {
    alert("Please fill frequency, quantity and duration fields for: " + missingVal.join(", "));
    return false;
  }

  return true;
}


  Submit(): void {
    this.doctorId = this.patientService.getDoctorId();
    if (this.validateFlds()) {
      

    const payload = {
      flag: 'I',
      stage: this.stage,
      personalHistoryId: 0,
      doctorId: this.doctorId,
      patientId: this.patientId,
      createdBy: this.doctorId,
      aeratedIntake: this.intakeStates['aerated'],
      aeratedFrequency: this.formData.get('aerated.frequency')?.value?.toString() || '' ,
      aeratedQuantity: this.formData.get('aerated.quantity')?.value?.toString() || '',
      aeratedDuration: this.formData.get('aerated.duration')?.value?.toString() || '',

      coffeeIntake: this.intakeStates['coffee'],
      coffeeFrequency: this.formData.get('coffee.frequency')?.value?.toString() || '',
      coffeeQuantity: this.formData.get('coffee.quantity')?.value?.toString() || '',
      coffeeDuration: this.formData.get('coffee.duration')?.value?.toString() || '',

      teaIntake: this.intakeStates['tea'],
      teaFrequency: this.formData.get('tea.frequency')?.value?.toString() || '',
      teaQuantity: this.formData.get('tea.quantity')?.value?.toString() || '',
      teaDuration: this.formData.get('tea.duration')?.value?.toString() || '',

      spicyIntake: this.intakeStates['spicy'],
      spicyFrequency: this.formData.get('spicy.frequency')?.value?.toString() || '',
      spicyQuantity: this.formData.get('spicy.quantity')?.value?.toString() || '',
      spicyDuration: this.formData.get('spicy.duration')?.value?.toString() || '',

      alcoholIntake: this.intakeStates['alcohol'],
      alcoholFrequency: this.formData.get('alcohol.frequency')?.value?.toString() || '',
      alcoholQuantity: this.formData.get('alcohol.quantity')?.value?.toString() || '',
      alcoholDuration: this.formData.get('alcohol.duration')?.value?.toString() || '',

      sweetsIntake: this.intakeStates['sweets'],
      sweetsFrequency: this.formData.get('sweets.frequency')?.value?.toString() || '',
      sweetsQuantity: this.formData.get('sweets.quantity')?.value?.toString() || '',
      sweetsDuration: this.formData.get('sweets.duration')?.value?.toString() || '',

      smokingIntake: this.intakeStates['smoking'],
      smokingFrequency: this.formData.get('smoking.frequency')?.value?.toString() || '',
      smokingQuantity: this.formData.get('smoking.quantity')?.value?.toString() || '',
      smokingDuration: this.formData.get('smoking.duration')?.value?.toString() || '',

      tobaccoIntake: this.intakeStates['tobacco'],
      tobaccoFrequency: this.formData.get('tobacco.frequency')?.value?.toString() || '',
      tobaccoQuantity: this.formData.get('tobacco.quantity')?.value?.toString() || '',
      tobaccoDuration: this.formData.get('tobacco.duration')?.value?.toString() || ''
    };
    

    this.http.httpPost('/PersonalHistory/SavePersonalHistory', payload).subscribe({
      next: () => {
        alert('Saved Successfully'); // ← Test this
        this.formValidation.showAlert('Saved Successfully', 'success');
        this.isSaved = true;
      },

      error: (err) => {
        console.error('Failed to save personal history:', err);
        this.formValidation.showAlert('Failed to save personal history.', 'danger');
      }
    });
    }
    else{
      return;
    }
    } 
  blockInvalidKeys(event: KeyboardEvent) {
  if (['e', 'E', '+', '-','.'].includes(event.key)) {
    event.preventDefault();
  }
}
preventNegative(event: any) {
 
  if (event.target.value < 0) {
    event.target.value = 0; 
  }
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
      group.get('frequency')?.clearValidators();
      group.get('quantity')?.clearValidators();
      group.get('duration')?.clearValidators();
      group.reset();
      group.disable();
    } else {

      group.get('frequency')?.setValidators([Validators.required]);
      group.get('quantity')?.setValidators([Validators.required]);
      group.get('duration')?.setValidators([Validators.required]);
      group.enable();
    }
    group.updateValueAndValidity();

  }


}

