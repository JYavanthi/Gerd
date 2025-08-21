import { Component, OnInit, SimpleChanges } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { FormvalidationService } from '../formvalidation.service';
import { ActivatedRoute, Router } from '@angular/router';
import { HttpserviceService } from '../httpservice.service';
import { PatientService } from '../Services/patient.service';
import { API_URLS } from '../shared/API-URLs';
import { ChiefComplaintService } from '../Services/chief-complaint.service';
import { id } from '@swimlane/ngx-charts';
import { Input } from '@angular/core';

@Component({
  selector: 'app-chief-complaint',
  templateUrl: './chief-complaint.component.html',
  styleUrls: ['./chief-complaint.component.css']
})
export class ChiefComplaintComponent implements OnInit {
  @Input() patientId!: number;
  @Input() stage!: number;
  tabId = 1;
    @Input() data: any;  
  // patientId: any;
    @Input() isPrintMode: boolean = false;
  cheifCompliantID: number = 0;
  @Input() ptnstage: number = 0;
  chiefComplaintForm: FormGroup;
  showCodeMessage = false;
  isViewMode: boolean = false;
  formData: any;

  isFollowUp: boolean = false;
  // stage: number = 0;
  isSaved: boolean = false;
  constructor(
    private patientService: PatientService,
    private fb: FormBuilder,
    private http: HttpserviceService,
    private formValidation: FormvalidationService,
    public route: ActivatedRoute,
    private router: Router,
    private chiefComplaintService: ChiefComplaintService,

  ){
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

  ngOnInit(): void {

     
    this.stage = Number(this.route.snapshot.params['stage'] || 0);
    this.patientId= Number(this.route.snapshot.params['patientId'] || this.patientService.getPatientId());
    const state = history.state;
    const allowedWithoutSave = [1, 3, 5];
    if (allowedWithoutSave.includes(this.stage)) {
      this.isSaved = true;
    }
    this.isViewMode = state?.isViewMode ?? false;
    this.formData = state?.data;
    //this.isFollowUp = this.router.url.includes('follow-up-1') || this.router.url.includes('follow-up-2');

    // this.route.params.subscribe(params => {
    //   const routeStage = params['stage'];
    //   //if (routeStage) this.stage = routeStage;

    // });
    if (state?.patientId) {
      //this.patientId = state.patientId;
      this.ptnstage = state.stage;
      //this.patientService.setPatientId(this.patientId);
    }

    //this.patientId = this.patientId || this.patientService.getPatientId();
    console.log('oninit patientid',this.patientId);
    if (!this.patientId) {
      console.warn('⚠️ No valid patient ID found.');
      return;
    }
    else{
      console.log(' patientid',this.patientId);
      this.fetchChiefComplaintData(this.patientId);
    }

  //   this.route.params.subscribe(params => {
  //     const idFromRoute = +params['patientId'];
  //     const stageFromRoute = params['stage'];

  //     // if (idFromRoute && idFromRoute !== this.patientId) {
  //     //   this.patientId = idFromRoute;
  //     //    this.stage = stageFromRoute;
  //     //   this.patientService.setPatientId(this.patientId);
  //     //   }

  //     const cachedData = this.patientService.getChiefComplaintData();
      // if (this.patientId !=='') {
      //   this.chiefComplaintForm.patchValue(cachedData);
      // } else {
       // this.fetchChiefComplaintData(this.patientId);
    //  }
  //   });
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
  private loadChiefComplaint() {
    if (!this.patientId) {
      console.warn('⚠️ No valid patient ID found to fetch chief complaint.');
      return;
    }

    // const cachedData = this.patientService.getChiefComplaintData();
    // if (cachedData) {
    //   this.chiefComplaintForm.patchValue(cachedData);
    // } else {
    //   this.fetchChiefComplaintData(this.patientId);
    // }
  }

  fetchChiefComplaintData(patientId: number): void {

   // const stageToFetch = this.stage; // this.stage comes from @Input()
  //  console.log('stage',this.stage);
  //   console.log('patientId',patientId);
  // if (!this.stage) {
  //   console.warn('⚠️ Stage not set for ChiefComplaintComponent');
  //   return;
  // }
    this.chiefComplaintService.getChiefComplaintByPatientId(patientId, this.stage).subscribe({
      next: (res: any) => {
        if (res.type === 'S' && res.data) {
          this.isSaved = true;
          const data = res.data;
          console.log('✅ Chief Complaint data:', data);
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

        } else {
          console.warn('⚠️ No chief complaint data found in response.');
        }
      },
      error: (err) => {
        console.error('❌ Error fetching chief complaint data:', err);
      }
    });
  }

  onCodeFocus(): void {
    this.showCodeMessage = true;
  }

  Submit(): void {
    console.log('Chief Complaint Submit function triggered');

    if (this.chiefComplaintForm.invalid) {
      const controls = this.chiefComplaintForm.controls;
      for (const field in controls) {
        if (controls[field].invalid) {
          alert(`Please fill or select: ${field.replace(/_/g, ' ')}`);
          this.chiefComplaintForm.markAllAsTouched();
          return;
        }
      }
    }

    if (!this.formValidation.validateForm(this.chiefComplaintForm)) {
      this.chiefComplaintForm.markAllAsTouched();
      return;
    }

    const patientId = this.patientService.getPatientId();
    const doctorId = this.patientService.getDoctorId() || 0;

    if (!patientId) {
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
      patientID: patientId,
      doctorID: doctorId,
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
      createdBy: doctorId
    };

    console.log('Submitting Chief Complaint Payload:', param);

    this.http.httpPost(API_URLS.CHEIF_COMPLAINT_SAVE, param).subscribe(
      (res: any) => {
        if (res.type === 'S') {
          this.isSaved = true;
          console.log('Chief complaint saved successfully.');

          this.http.httpGet('/PatientReg/GetPatient').subscribe((getRes: any) => {
            if (getRes.type === 'S' && getRes.data?.length > 0) {
              const latestPatient = getRes.data[getRes.data.length - 1];
              const updatedPatientId = latestPatient.patientId;

              //this.patientService.setPatientId(updatedPatientId);
              //this.patientService.setDoctorId(param.createdBy);

              this.formValidation.showAlert('Chief complaint saved successfully', 'success');
              alert('Saved Successfully');

              this.router.navigate([], {
                queryParams: {
                  patientId: updatedPatientId,
                  doctorId: this.patientService.getDoctorId(),
                  //stage: this.router.param
                }
              });
            } else {
              this.formValidation.showAlert('Unable to fetch Patient ID after save', 'danger');
              alert('⚠️ Unable to fetch Patient ID after save');
            }
          });
        } else {
          const errorMsg = `Error: ${res.message || 'Unknown error'}`;
          this.formValidation.showAlert(errorMsg, 'danger');
          alert(`❌ ${errorMsg}`);
        }
      },
      error => {
        console.error('Error saving chief complaint:', error);
        this.formValidation.showAlert('Error saving chief complaint', 'danger');
        alert('❌ Error saving chief complaint');
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
    this.router.navigate([`/case-details/${this.patientId}/${this.stage}`], {
      state: {
        tabId: this.tabId,
        patientId: this.patientId,
        isViewMode: this.isViewMode
      }
    });
  }
  back() {
    this.router.navigate([`/case-details/${this.patientId}/${this.stage}`], {
      state: {
        tabId: this.tabId,
        patientId: this.patientId,
        isViewMode: this.isViewMode
      }
    });
  }
  //  onNext(): void {
  //   this.patientService.setChiefComplaintData(this.chiefComplaintForm.value);

  //   const navigationExtras = {
  //     state: {
  //       tabId: this.tabId,
  //       patientId: this.patientId,
  //       isViewMode: this.isViewMode
  //     }
  //   };

  //   const currentUrl = this.router.url;
  //       this.router.navigate(['/follow-up-1/comorbidities/${this.patientId}'], navigationExtras);

  //   if (currentUrl.includes('follow-up-1')) {
  //   }




  // }

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