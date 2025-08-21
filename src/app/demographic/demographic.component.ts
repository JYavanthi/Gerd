import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { FormvalidationService } from '../formvalidation.service';
import { HttpserviceService } from '../httpservice.service';
import { PatientService } from '../Services/patient.service';
import { CaseDataService } from '../Services/case-data.services';
import { API_URLS } from '../shared/API-URLs';

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
  doctorId : number | null = null;
  isSaved: boolean = false;
  states: Array<{ id: number; name: string }> = [];
  cities: any;
  userData:any;
  occupation:any;


  constructor(
    private fb: FormBuilder,
    private formValidation: FormvalidationService,
    private http: HttpserviceService,
    private router: Router,
    public route: ActivatedRoute,
    private patientService: PatientService,
    private caseDataService: CaseDataService // ✅ Inject shared service
  ) {
    const state = history.state;

    this.demographicForm = this.fb.group({
      patientName: ['', Validators.required],
      initial: [''],
      subjectNumber: [{ value: '', disabled: true }],
      date: ['', Validators.required],
      age: ['', Validators.required],
      dob: [''],
      stage: state?.stage ?? 1,
      gender: ['', Validators.required],
      education: ['', Validators.required],
      occupation: ['', Validators.required],
      state: ['', Validators.required],
      city: ['', Validators.required],
      pincode: ['',Validators.required,Validators.pattern(/^[1-9][0-9]{5}$/)],
      placeType: ['', Validators.required],
      socioeconomic: ['', Validators.required],
      annualFamilyIncome: ['', Validators.required],
      pastHistory: ['', Validators.required],
      diet: ['', Validators.required],
    });
  }

  ngOnInit(): void {
this.demographicForm.get('date')?.valueChanges.subscribe((dobValue: string) => {
  if (dobValue) {
    const age = this.calculateAge(new Date(dobValue));
    this.demographicForm.get('age')?.setValue(age, { emitEvent: false });
  }
});
    
    const state = history.state;
    this.patientId = state?.patientId;
    this.doctorId = this.patientService.getDoctorId();
    this.tabId = state?.tabId ?? 1;
    this.stage = state?.stage ?? 0;

    this.loadStates();


    this.demographicForm.statusChanges.subscribe(status => {
      this.isSaved = false;
      
    });
    // ✅ Restore form from localStorage
    const demographicData = this.patientService.getDemographicData();
    if (demographicData) {
      this.demographicForm.patchValue(demographicData);
    }

  }
  today: string = new Date().toISOString().split('T')[0];

  onCodeFocus(): void {
    this.showCodeMessage = true;
  }

  Submit() {
    if (!this.formValidation.validateForm(this.demographicForm)) {
      this.demographicForm.markAllAsTouched();
      return;
    }
let user: any = localStorage.getItem('doctor')
    this.userData = JSON.parse(user);
    
    const param = {
      flag: 'I',
      patientID: 0,
      doctorId: this.userData?.doctorId,
      stage: this.stage,
      initial: this.demographicForm.controls['patientName'].value,
      subjectNo: this.demographicForm.controls['subjectNumber'].value,
      date: this.demographicForm.controls['date'].value,
      age: this.demographicForm.controls['age'].value,
      gender: this.demographicForm.controls['gender'].value,
      education: this.demographicForm.controls['education'].value,
      occupation: this.demographicForm.controls['occupation'].value,
      state: Number(this.demographicForm.controls['state'].value),
      city: Number(this.demographicForm.controls['city'].value),
      pincode: this.demographicForm.controls['pincode']?.value || 0,
      placeType: this.demographicForm.controls['placeType'].value,
      socioeconomicStatus: this.demographicForm.controls['socioeconomic'].value,
      familyIncome: this.demographicForm.controls['annualFamilyIncome'].value,
      pastHistory: this.demographicForm.controls['pastHistory'].value,
      diet: this.demographicForm.controls['diet'].value,
      createdBy: this.userData?.doctorId
    };

    this.http.httpPost('/PatientReg/SavePatient', param).subscribe((res: any) => {
      if (res.type === 'S') {
        this.isSaved = true;
        this.occupation=this.demographicForm.controls['occupation'].value;
        this.patientService.setOccupation(this.occupation);
        alert('Saved Successfully'); // ← Test this
        this.formValidation.showAlert('Saved Successfully', 'success');

        this.http.httpGet('/PatientReg/GetPatient').subscribe((getRes: any) => {
          if (getRes.type === 'S' && getRes.data?.length > 0) {
            this.isSaved = true;
            const latestPatient = getRes.data[getRes.data.length - 1];
            const patientId = latestPatient.patientId;
            const doctorId = latestPatient.doctorId || latestPatient.doctorID;
            this.patientService.setPatientId(patientId);
            this.patientService.setDoctorId(doctorId);
            const demographicData = {
              patientName: this.demographicForm.controls['patientName']?.value || '',
              initial: param.initial,
              subjectNumber: param.subjectNo || `MLL/GERD/25-26-${patientId}`,
              date: param.date,
              age: param.age,
              dob: this.demographicForm.controls['dob']?.value || '',
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
            localStorage.setItem('demographicData','');
            localStorage.setItem('demographicData', JSON.stringify(demographicData)); // optional fallback

            const caseData = {
              patientId: patientId,
              doctorId: doctorId,
              initial: this.demographicForm.controls['patientName'].value,
              stage: this.stage,
              tabId: this.tabId,
              subjectNo: param.subjectNo || param.patientID,
              gender: param.gender,
              date: param.date,
              status: 'Ongoing',

            };
            this.caseDataService.addCase(caseData);
            this.formValidation.showAlert('Save Successfully', 'success');
          }
        });

        
      }
      else {
        this.formValidation.showAlert('Submission Failed', 'danger');
      }
    });
    
  }
 getCities(event: any) {
  this.http.httpGet(API_URLS.CITY_GET, { stateId: event.target.value }).subscribe({
    next: (res: any) => {
      // Sort cities alphabetically by name
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
calculateAge(dob: Date): number {
  const today = new Date();
  let age = today.getFullYear() - dob.getFullYear();
  const monthDiff = today.getMonth() - dob.getMonth();
  const dayDiff = today.getDate() - dob.getDate();

  if (monthDiff < 0 || (monthDiff === 0 && dayDiff < 0)) {
    age--;
  }

  return age;
}

  onnext() {
    this.router.navigate(['/chiefComplaint']);
  }
  // fetchPatientIdAndNavigate() {
  //   this.http.httpGet('/PatientReg/GetPatient').subscribe((res: any) => {
  //     if (res.type === 'S' && res.data?.length) {
  //       // Assuming latest patient is the last one in the list
  //       const latestPatient = res.data[res.data.length - 1];
  //       const patientId = latestPatient.patientID; // Adjust field name as needed
  //       this.router.navigate(['/chiefComplaint', patientId]);
  //     } else {
  //       this.formValidation.showAlert('Unable to fetch Patient ID', 'danger');
  //     }
  //   });
  // }

  // getChiefComplaint(patientId: number) {
  //   this.http.httpGet(`/ChiefComplaint/GetComplaint?patientId=${patientId}`).subscribe((res: any) => {
  //     if (res.type === 'S') {
  //       this.complaintData = res.data;
  //     } else {
  //       this.formValidation.showAlert('No complaint found', 'danger');
  //     }
  //   });
  // }
}



