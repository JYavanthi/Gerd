import { Component, Input, OnInit, SimpleChanges, HostListener } from '@angular/core';
import { ActivatedRoute, NavigationEnd, Router } from '@angular/router';
import { FormvalidationService } from '../formvalidation.service';
import { HttpserviceService } from '../httpservice.service';
import { PatientHistoryService } from '../Services/patient-history.service';
import { HistoryService } from '../Services/history.servie';
import { error } from 'node:console';
import { PatientService } from '../Services/patient.service';
import { FormGroup, FormControl } from '@angular/forms';
import { Validators } from '@angular/forms';
import { Subscription } from 'rxjs';

@Component({
  selector: 'app-personal-history',
  templateUrl: './personal-history.component.html',
  styleUrls: ['./personal-history.component.css']
})
export class PersonalHistoryComponent {
  private pushStateCount = 5;
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
  ageInYears: number = 0;
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
  private routerSub!: Subscription;

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

    // Age is stored in localStorage from Demographic component
    const storedAge = localStorage.getItem('Age');
    if (storedAge) {
      const age = JSON.parse(storedAge).age; // age in years
      this.ageInYears = age;            // age in years
      console.log('Age in Years:', this.ageInYears);
    }
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
        aeratedFrequency: this.formData.get('aerated.frequency')?.value?.toString() || '',
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

      type PersonalHistoryKey = 'aerated' | 'coffee' | 'tea' | 'spicy' | 'alcohol' | 'sweets' | 'smoking' | 'tobacco';

      const itemLimits: Record<PersonalHistoryKey, { maxFreq: number | null, freqUnit: string, maxQty: number | null, qtyUnit: string }> = {
        aerated: { maxFreq: 24, freqUnit: '/day', maxQty: 2000, qtyUnit: 'ml' },
        coffee: { maxFreq: 24, freqUnit: '/day', maxQty: 1500, qtyUnit: 'ml' },
        tea: { maxFreq: 24, freqUnit: '/day', maxQty: 1500, qtyUnit: 'ml' },
        spicy: { maxFreq: 7, freqUnit: '/week', maxQty: 70, qtyUnit: 'gram' },
        alcohol: { maxFreq: 25, freqUnit: '/week', maxQty: 3500, qtyUnit: 'ml' },
        sweets: { maxFreq: 24, freqUnit: '/week', maxQty: 1000, qtyUnit: 'g' },
        smoking: { maxFreq: 100, freqUnit: '/day', maxQty: 10, qtyUnit: 'packs' },
        tobacco: { maxFreq: 24, freqUnit: '/day', maxQty: 1000, qtyUnit: 'quantity' }
      };

      // Validate frequency and quantity against rules
      for (const key of Object.keys(itemLimits) as PersonalHistoryKey[]) {
        if (!this.intakeStates[key]) continue; // Only check if intake is true

        const freqValue = Number(this.formData.get(`${key}.frequency`)?.value);
        const qtyValue = Number(this.formData.get(`${key}.quantity`)?.value);
        const { maxFreq, freqUnit, maxQty, qtyUnit } = itemLimits[key];

        // For items with limits, validate both frequency and quantity
        if (maxFreq !== null) {
          if (isNaN(freqValue) || freqValue < 0) {
            alert(`Please enter a valid numeric frequency for ${key}.`);
            return;
          }
          if (freqValue > maxFreq) {
            alert(`Frequency for ${key} exceeds the maximum (${maxFreq} ${freqUnit}).`);
            return;
          }
        }
        if (maxQty !== null) {
          if (isNaN(qtyValue) || qtyValue < 0) {
            alert(`Please enter a valid numeric quantity for ${key}.`);
            return;
          }
          if (qtyValue > maxQty) {
            alert(`Quantity for ${key} exceeds the maximum (${maxQty} ${qtyUnit}).`);
            return;
          }
        }
      }

      const enteredaeratedDurationMonths = Number(payload.aeratedDuration);
      const enteredrcoffeeDurationMonths = Number(payload.coffeeDuration);
      const entereteaDurationMonths = Number(payload.teaDuration);
      const enteredalcoholDurationMonths = Number(payload.alcoholDuration);
      const enteredsweetsDurationMonths = Number(payload.sweetsDuration);
      const enteredsmokingDurationMonths = Number(payload.smokingDuration);
      const enteredtobaccoDurationMonths = Number(payload.tobaccoDuration);

      if (
        enteredaeratedDurationMonths > this.ageInYears ||
        enteredrcoffeeDurationMonths > this.ageInYears ||
        entereteaDurationMonths > this.ageInYears ||
        enteredalcoholDurationMonths > this.ageInYears ||
        enteredsweetsDurationMonths > this.ageInYears ||
        enteredsmokingDurationMonths > this.ageInYears ||
        enteredtobaccoDurationMonths > this.ageInYears
      ) {

        alert('Entered duration exceeds the person’s age (' + this.ageInYears + ' years). Please enter valid values.');
        return;
      }
      this.http.httpPost('/PersonalHistory/SavePersonalHistory', payload).subscribe({
        next: () => {
          alert('Saved Successfully');
          this.formValidation.showAlert('Saved Successfully', 'success');
          this.isSaved = true;
        },

        error: (err) => {
          console.error('Failed to save personal history:', err);
          this.formValidation.showAlert('Failed to save personal history.', 'danger');
        }
      });
    }
    else {
      return;
    }
  }
  blockInvalidKeys(event: KeyboardEvent) {
    if (['e', 'E', '+', '-', '.', ')', '(', '*', '&', '%', '$', '#', '@', '!', '~', '^'].includes(event.key)) {
      event.preventDefault();
    }
  }
  preventNegative(event: any) {

    if (event.target.value < 0) {
      event.target.value = 0;
    }
  }



  onNext(): void {
    // const patientHistoryId = this.historyService.getPatientHistoryID();
    this.router.navigate([`sleep/${this.patientId}/${this.stage}`], {

      state: {
        tabId: this.tabId,
        stage: this.stage,
        patienId: this.patientId,
        isViewMode: this.isViewMode

      }
    });
  }
  OnNext(): void {
    //const patientHistoryId = this.historyService.getPatientHistoryID();
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

