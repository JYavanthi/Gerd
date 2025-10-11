import { Component, Input, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { FormvalidationService } from '../formvalidation.service';
import { HttpserviceService } from '../httpservice.service';
import { PatientService } from '../Services/patient.service';
import { CaseDataService } from '../Services/case-data.services';
import { API_URLS } from '../shared/API-URLs';
import { DemographicService } from '../Services/demographic.service';

@Component({
  selector: 'app-demographic',
  templateUrl: './demographic.component.html',
  styleUrls: ['./demographic.component.css']
})

export class DemographicComponent implements OnInit {
  demographicForm: FormGroup;
  showCodeMessage = false;
  savedPatientId: any;
  complaintData: any;
  stage: number = 0;
  tabId = 1
  patientId: number | null = null;
  doctorId: number | null = null;
  isSaved: boolean = false;
  states: Array<{ id: number; name: string }> = [];
  cities: any;
  userData: any;
  @Input() isPrintMode = false;
  isAgeAbove16 = false;

  constructor(
    private fb: FormBuilder,
    private formValidation: FormvalidationService,
    private http: HttpserviceService,
    private router: Router,
    public route: ActivatedRoute,
    private patientService: PatientService,
    private demographicService: DemographicService,
  ) {
    //const state = history.state;

    this.demographicForm = this.fb.group({
      patientName: ['', Validators.required],
      initial: [''],
      date: ['', Validators.required],
      subjectNumber: [''],
      age: ['', Validators.required],
      dob: [''],
      stage: this.stage ?? 0,
      gender: ['', Validators.required],
      education: ['', Validators.required],
      occupation: ['', Validators.required],
      state: ['', Validators.required],
      city: ['', Validators.required],
      pincode: ['', [Validators.required, Validators.pattern(/^\d{6}$/)]],
      placeType: ['', Validators.required],
      socioeconomic: ['', Validators.required],
      annualFamilyIncome: ['', Validators.required],
      pastHistory: ['', Validators.required],
      diet: ['', Validators.required],
    });
  }

  viewFlag: Boolean = false;
  //stateid : any;
  cityid: any;
  ngOnInit(): void {


    this.route.params.subscribe(params => {
      this.patientId = +params['patientId'] || null;
      this.stage = +params['stage'];
      if (this.stage === 1 || this.stage === 2 || this.stage === 3 || this.stage === 4 || this.stage === 5) this.isSaved = true;



      if (this.stage !== 0) {
        this.demographicForm.disable();
      }
    });

    if (this.patientId !== null || this.patientId) {
      this.fetchDemographicData(Number(this.patientId));
    }




    this.demographicForm.get('date')?.valueChanges.subscribe((dobValue: string) => {
      if (dobValue) {
        const age = this.calculateAge(new Date(dobValue));
        this.demographicForm.get('age')?.setValue(age, { emitEvent: false });
      }
    });
    this.demographicForm.statusChanges.subscribe(status => {
      this.isSaved = false;
      if (this.stage === 1 || this.stage === 2 || this.stage === 3 || this.stage === 4 || this.stage === 5) this.isSaved = true;
    });

    // const demographicData = this.patientService.getDemographicData();
    //  console.log('demographicData', demographicData);
    // if (demographicData) {
    //   this.demographicForm.patchValue(demographicData);
    // }
    this.loadStates();
  }
  today: string = new Date().toISOString().split('T')[0];

  onCodeFocus(): void {
    this.showCodeMessage = true;
  }

  subjectno: string = ''

  fetchDemographicData(patientId: number): void {
    if (this.stage === undefined) {
      console.warn('⚠️ Stage not set for Demography');
      return;
    }

    this.demographicService.getDemographicDetailsByPatientId(patientId).subscribe({
      next: (res: any) => {
        if (res.type === 'S' && res.data) {
          this.isSaved = true;

          const data = res.data;

          // save city id for later
          this.cityid = data.city;
          this.subjectno = data.subjectNo;
          this.pincode = data.pincode
          // patch form without city (yet)
          this.demographicForm.patchValue({
            patientName: data.initial || '',
            initial: data.initial || '',
            subjectNumber: data.subjectNo || '',
            date: data.date ? data.date.split('T')[0] : '',
            age: data.age ?? '',
            dob: data.dob || '',
            gender: data.gender || '',
            education: data.education || '',
            occupation: data.occupation || '',
            state: data.state ?? '',
            pincode: data.pincode ?? '',
            placeType: data.placeType || '',
            socioeconomic: data.socioeconomicStatus || '',
            annualFamilyIncome: data.familyIncome || '',
            diet: data.diet || '',
            pastHistory: data.pastHistory || '',
          });

          this.patientService.setDemographicData(data);



          // Load cities first
          if (data.state) {
            this.http.httpGet(API_URLS.CITY_GET, { stateId: data.state }).subscribe({
              next: (cities: any) => {
                this.cities = cities.sort((a: any, b: any) =>
                  a.name.toLowerCase().localeCompare(b.name.toLowerCase())
                );

                // Patch city if available
                if (this.cityid) {
                  this.demographicForm.patchValue({ city: this.cityid });
                }

                // After city is set, fetch pincodes
                if (this.cityid) {
                  this.http.httpGet(API_URLS.GET_PINCODE, { citiid: this.cityid }).subscribe({
                    next: (res: any) => {
                      this.pincode = res.sort((a: any, b: any) =>
                        (a.pincode || 0) - (b.pincode || 0)
                      );

                      // Patch the selected pincode value
                      if (data.pincode) {
                        this.demographicForm.patchValue({ pincode: data.pincode });
                      }
                    },
                    error: (err) => console.error('❌ Error loading pincodes:', err)
                  });
                }
              },
              error: (err) => console.error('❌ Error loading cities:', err)
            });
          }

          // auto-calc age
          if (data.date) {
            const age = this.calculateAge(new Date(data.date));
            this.demographicForm.get('age')?.setValue(age, { emitEvent: false });
          }
        } else {
        }
      },
      error: (err) => {
      }
    });
  }

  Submit() {

    const socio = this.demographicForm.get('socioeconomic')?.value;
    const income = this.demographicForm.get('annualFamilyIncome')?.value;

    console.log('Socioeconomic:', socio, 'Income:', income);

    if (socio === 'Below Poverty Line' && income !== 'Less than 1 Lakh') {
      alert('Select Annual Family Income Less than 1 Lakh');
      return;
    }
    if (!this.formValidation.validateForm(this.demographicForm)) {
      this.demographicForm.markAllAsTouched();
      return;
    }
    if (this.demographicForm.controls['age'].value > 120) {
      this.formValidation.showAlert('Please enter valid Age', 'danger');
      return;
    }


    let user: any = localStorage.getItem('doctor')
    this.userData = JSON.parse(user);
    this.patientService.setDoctorId(this.userData?.doctorId);

    const param = {
      flag: 'I',
      patientID: this.patientId ?? 0,
      doctorId: this.userData?.doctorId,
      stage: 0,
      initial: this.demographicForm.controls['patientName'].value,
      subjectNo: this.demographicForm.controls['subjectNumber'].value,
      date: this.demographicForm.controls['date'].value,
      age: this.demographicForm.controls['age'].value,
      gender: this.demographicForm.controls['gender'].value,
      education: this.demographicForm.controls['education'].value,
      occupation: this.demographicForm.controls['occupation'].value,
      state: Number(this.demographicForm.controls['state'].value),
      city: Number(this.demographicForm.controls['city'].value),
      pincode: this.demographicForm.controls['pincode']?.value,
      placeType: this.demographicForm.controls['placeType'].value,
      socioeconomicStatus: this.demographicForm.controls['socioeconomic'].value,
      familyIncome: this.demographicForm.controls['annualFamilyIncome'].value,
      pastHistory: this.demographicForm.controls['pastHistory'].value,
      diet: this.demographicForm.controls['diet'].value,
      createdBy: this.patientService.getDoctorId()
    };

    // Save it to localStorage
      const ageValue = this.demographicForm.controls['age'].value;
      localStorage.setItem('Age', JSON.stringify({ age: ageValue }));

    this.http.httpPost('/PatientReg/SavePatient', param).subscribe((res: any) => {
      if (res.type === 'S') {
        this.isSaved = true;
        alert('Saved Successfully'); // ← Test this
        this.formValidation.showAlert('Saved Successfully', 'success');
        if (this.patientId === null) {

          this.http.httpGet('/PatientReg/GetPatient').subscribe((getRes: any) => {
            if (getRes.type === 'S' && getRes.data?.length > 0) {
              // this.isSaved = true;
              const latestPatient = getRes.data[getRes.data.length - 1];
              const patientId = latestPatient.patientId;
              this.patientId = patientId;
              //const doctorId = latestPatient.doctorId || latestPatient.doctorID;
              this.patientService.setPatientId(patientId);
              //this.patientService.setDoctorId(doctorId);
              const demographicData = {
                patientId: param.patientID,
                patientName: param.initial,
                initial: param.initial,
                subjectNumber: param.subjectNo,
                date: param.date,
                age: param.age,
                dob: param.date,
                gender: param.gender,
                education: param.education,
                occupation: param.occupation,
                state: param.state,
                city: param.city,
                pincode: param.pincode,
                placeType: param.placeType,
                socioeconomic: param.socioeconomicStatus,
                annualFamilyIncome: param.familyIncome,
                pastHistory: param.pastHistory,
                diet: param.diet
              };

              this.patientService.setDemographicData(demographicData); // ✅ Make sure this method exists
              localStorage.setItem('demographicData', '');
              localStorage.setItem('demographicData', JSON.stringify(demographicData)); // optional fallback


            }
          });

        }



      }
      else {
        this.formValidation.showAlert('Submission Failed', 'danger');
      }
    });

  }


  pincode: any

  getPincode(event: any) {
    const cityId = event?.target?.value || null;

    if (!cityId) {
      this.pincode = [];
      this.demographicForm.patchValue({ pincode: '' });
      return;
    }

    this.http.httpGet(API_URLS.GET_PINCODE, { citiid: cityId }).subscribe({
      next: (res: any) => {
        this.pincode = res.sort((a: any, b: any) =>
          (a.pincode || 0) - (b.pincode || 0)
        );
      },
      error: (err) => {
        this.formValidation.showAlert('Error loading pincodes', 'danger');
        console.error(err);
      }
    });
  }


  getCities(event: any) {
    const stateId = event?.target?.value || null;

    if (!stateId) {
      this.cities = [];
      this.demographicForm.patchValue({ city: '' });
      return;
    }

    this.http.httpGet(API_URLS.CITY_GET, { stateId }).subscribe({
      next: (res: any) => {
        // Sort cities alphabetically
        this.cities = res.sort((a: any, b: any) =>
          a.name.toLowerCase().localeCompare(b.name.toLowerCase())
        );


        if (this.cityid) {
          this.demographicForm.patchValue({
            city: this.cityid
          });
        }
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
    // this.getCities(this.states)
    // if(this.stateid!==''){
    //   this.demographicForm.patchValue({
    //     city : this.cityid?? this.getCities(this.stateid)
    //   })
    // }
  }
  calculateAge(dob: Date): number {
    const today = new Date();
    let age = today.getFullYear() - dob.getFullYear();
    const monthDiff = today.getMonth() - dob.getMonth();

    if (monthDiff < 0 || (monthDiff === 0 && today.getDate() < dob.getDate())) {
      age--;
    }
    return age >= 0 ? age : 0; // avoid negative age
  }
  onnext() {
    this.router.navigate([`chiefComplaint/${this.patientId}/${this.stage}`], {

    });
  }


  onAgeChange() {
    const age = this.demographicForm.get('age')?.value;

    if (age > 16) {
      this.isAgeAbove16 = true;

      // If "Below 10th" was already selected, clear it
      if (this.demographicForm.get('education')?.value === 'Below 10th') {
        this.demographicForm.get('education')?.setValue('');
      }
    } else {
      this.isAgeAbove16 = false;
    }
  }

  onEducationChange(event: any) {
    const age = this.demographicForm.get('age')?.value;
    const selected = event.target.value;



    if (age < 16 && selected === '10th Std & Above') {
      alert('Age is less than 16, cannot select 10th Std & Above');
      this.demographicForm.get('education')?.setValue('');
    }
  }


}



