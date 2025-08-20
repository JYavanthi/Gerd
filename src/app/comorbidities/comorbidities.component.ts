
import { Component, Input, SimpleChanges } from '@angular/core';
import { ActivatedRoute, Params, Router } from '@angular/router';
import { PatientService } from '../Services/patient.service';
import { HttpserviceService } from '../httpservice.service';
import { API_URLS } from '../shared/API-URLs';
import { ComorbiditiesService } from '../Services/comorbidities.service';
import { FormGroup } from '@angular/forms';
import { FormvalidationService } from '../formvalidation.service';
// Define interface for comorbidity item
interface ComorbidityItem {
  present: boolean | null;
  remarks: string;
}

// Define interface for comorbidities object with index signature
interface Comorbidities {
  [key: string]: ComorbidityItem;
}

@Component({
  selector: 'app-comorbidities',
  templateUrl: './comorbidities.component.html',
  styleUrls: ['./comorbidities.component.css']
})
export class ComorbiditiesComponent {
  tabId = 1;
  patientId: number | null = null;
  doctorId: number | null = null;
  isViewMode = false;
  isFollowUp: boolean = false;
  PatientHistoryID: number | null = null;
   stage: number = 0;
  isSaved: boolean = false;
  comorbiditiesID: number = 0;
  @Input() data: any;  
  // patientId: any;
    @Input() isPrintMode: boolean = false;
  comorbidities: Comorbidities = {
    hypertension: { present: null, remarks: '', },
    diabetes: { present: null, remarks: '' },
    dyslipidemia: { present: null, remarks: '' },
    liver: { present: null, remarks: '' },
    neuro: { present: null, remarks: '' },
    cardio: { present: null, remarks: '' },
    hypo: { present: null, remarks: '' },
    hyper: { present: null, remarks: '' },
    behavioural: { present: null, remarks: '' },
    kidney: { present: null, remarks: '' },
    asthma: { present: null, remarks: '' },
    osteo: { present: null, remarks: '' },
    rheumatoid: { present: null, remarks: '' },
    sclerosis: { present: null, remarks: '' },
    cancer: { present: null, remarks: '' },
    others: { present: null, remarks: '' }
  };


 ngOnChanges(changes: SimpleChanges) {
    if (changes['data'] && this.data) {
      this.patchComorbidities(this.data);
    }
  }

  private patchComorbidities(data: any) {
    this.comorbidities = {
      hypertension: { present: data.htPresent === 'true', remarks: data.htRemark || '' },
      diabetes: { present: data.dbPresent === 'true', remarks: data.dbRemark || '' },
      dyslipidemia: { present: data.ddPresent === 'true', remarks: data.ddRemark || '' },
      liver: { present: data.cldPresent === 'true', remarks: data.cldRemark || '' },
      neuro: { present: data.ndPresent === 'true', remarks: data.ndRemark || '' },
      cardio: { present: data.cdPresent === 'true', remarks: data.cdRemark || '' },
      hypo: { present: data.hPresent === 'true', remarks: data.hRemark || '' },
      hyper: { present: data.htdPresent === 'true', remarks: data.htdRemark || '' },
      behavioural: { present: data.bdPresent === 'true', remarks: data.bdRemark || '' },
      kidney: { present: data.ckdPresent === 'true', remarks: data.ckdRemark || '' },
      asthma: { present: data.aPresent === 'true', remarks: data.aRemark || '' },
      osteo: { present: data.oPresent === 'true', remarks: data.oRemark || '' },
      rheumatoid: { present: data.raPresent === 'true', remarks: data.raRemark || '' },
      sclerosis: { present: data.ssPresent === 'true', remarks: data.ssRemark || '' },
      cancer: { present: data.cPresent === 'true', remarks: data.cRemark || '' },
      others: { present: data.cmoPresent === 'true', remarks: data.cmoRemark || '' }
    };
  }


  constructor(
    private route: ActivatedRoute,
    private router: Router,
    private patientService: PatientService,
    private http: HttpserviceService, private comorbiditiesService: ComorbiditiesService, private formValidation: FormvalidationService
  ) {
    const path = this.router.url;
    const isFollowUp1 = path.includes('follow-up-1');
    const isFollowUp2 = path.includes('follow-up-2');
    this.route.queryParams.subscribe((params: Params) => {
      this.patientId = +params['patientId'] || null;
      this.doctorId = +params['doctorId'] || null;

      if (this.patientId) this.patientService.setPatientId(this.patientId);
      if (this.doctorId) this.patientService.setDoctorId(this.doctorId);
    });
  }
  ptnstage: number = 0;
  ngOnInit(): void {
    this.stage = Number(this.route.snapshot.params['stage'] || 0);
    console.log('Comorbidities patientId:', this.patientId);
    console.log('Comorbidities stage:', this.stage);
    this.route.params.subscribe(params => {
      const routeStage = +params['stage'];
      if (routeStage) this.stage = routeStage;
    });
    // Read route params if present
    this.route.params.subscribe(params => {
       const allowedWithoutSave = [1, 3, 5];
  if (allowedWithoutSave.includes(this.stage)) {
    this.isSaved = true;
  }
      const routePatientId = +params['patientId'] || null;
      const routeStage = +params['stage'] || 0;

      if (routePatientId) {
        this.patientId = routePatientId;
        this.stage = routeStage;

        this.patientService.setPatientId(this.patientId);
      }

      // Then do the rest of your existing logic
      const state = history.state;
      const currentUrl = this.router.url;
      this.tabId = state?.tabId ?? 1;

      // Only override patientId from state if route param is NOT present
      this.patientId = this.patientId ?? state?.patientId ?? null;
      this.isViewMode = state?.isViewMode ?? false;
      this.isFollowUp = currentUrl.includes('follow-up-1') || currentUrl.includes('follow-up-2');

      console.log('📌 tabId:', this.tabId);
      console.log('📌 patientId:', this.patientId);
      console.log('📌 isViewMode:', this.isViewMode);

      if (this.isViewMode && this.patientService.getPatientId()) {
        this.fetchComorbiditiesData(this.patientService.getPatientId());
      }

      const cachedData = this.patientService.getfamalyhistoryData();
      if (cachedData) {
        Object.keys(cachedData).forEach((key) => {
          if (this.comorbidities[key]) {
            this.comorbidities[key].present = cachedData[key].present;
            this.comorbidities[key].remarks = cachedData[key].remarks;
          }
        });
      } else {
        this.fetchComorbiditiesData(this.patientService.getPatientId());
      }
    });
  }

comorbidityKeys: string[] = [
  'hypertension', 'diabetes', 'dyslipidemia', 'liver', 'neuro',
  'cardio', 'hypo', 'hyper', 'behavioural', 'kidney',
  'asthma', 'osteo', 'rheumatoid', 'sclerosis', 'cancer', 'others'
];

comorbidityLabels: { [key: string]: string } = {
  hypertension: 'Hypertension',
  diabetes: 'Diabetes',
  dyslipidemia: 'Dyslipidemia',
  liver: 'Chronic liver disease',
  neuro: 'Neurological Disorder',
  cardio: 'Cardiovascular disorders',
  hypo: 'Hypothyroidism',
  hyper: 'Hyperthyroidism',
  behavioural: 'Behavioural disorders',
  kidney: 'Chronic kidney disease',
  asthma: 'Asthma',
  osteo: 'Osteoarthritis',
  rheumatoid: 'Rheumatoid arthritis',
  sclerosis: 'Systemic Sclerosis',
  cancer: 'Cancer',
  others: 'Others (Specify)'
};


  fetchComorbiditiesData(patientId: number): void {
    this.comorbiditiesService.getComorbiditiesById(patientId,this.stage).subscribe({
      next: (res: any) => {
        if (res.type === 'S' && res.data) {
          const data = res.data;
          this.stage = data.stage;
          this.comorbiditiesID = res.data.comorbiditiesID;
          this.comorbidities = {
            hypertension: { present: data.htPresent === 'true', remarks: data.htRemark || '' },
            diabetes: { present: data.dbPresent === 'true', remarks: data.dbRemark || '' },
            dyslipidemia: { present: data.ddPresent === 'true', remarks: data.ddRemark || '' },
            liver: { present: data.cldPresent === 'true', remarks: data.cldRemark || '' },
            neuro: { present: data.ndPresent === 'true', remarks: data.ndRemark || '' },
            cardio: { present: data.cdPresent === 'true', remarks: data.cdRemark || '' },
            hypo: { present: data.hPresent === 'true', remarks: data.hRemark || '' },
            hyper: { present: data.htdPresent === 'true', remarks: data.htdRemark || '' },
            behavioural: { present: data.bdPresent === 'true', remarks: data.bdRemark || '' },
            kidney: { present: data.ckdPresent === 'true', remarks: data.ckdRemark || '' },
            asthma: { present: data.aPresent === 'true', remarks: data.aRemark || '' },
            osteo: { present: data.oPresent === 'true', remarks: data.oRemark || '' },
            rheumatoid: { present: data.raPresent === 'true', remarks: data.raRemark || '' },
            sclerosis: { present: data.ssPresent === 'true', remarks: data.ssRemark || '' },
            cancer: { present: data.cPresent === 'true', remarks: data.cRemark || '' },
            others: { present: data.cmoPresent === 'true', remarks: data.cmoRemark || '' }
          };
        }
        console.log('Data', res.data)
      },
      error: err => console.error('Failed to fetch comorbidities:', err)
    });
  }
  updateComorbidityStatus(comorbidity: string, value: boolean): void {
    if (this.comorbidities[comorbidity]) {
      this.comorbidities[comorbidity].present = value;
      if (!value) {
        this.comorbidities[comorbidity].remarks = '';
      }
    }
  }


  Submit() {
    const missingRemarks: string[] = [];

  this.comorbidityKeys.forEach(key => {
    const entry = this.comorbidities[key];
    if (entry.present && (!entry.remarks || entry.remarks.trim() === '')) {
      missingRemarks.push(this.comorbidityLabels[key]);
    }
  });

  if (missingRemarks.length > 0) {
    alert(
      'Please fill in remarks for the following comorbidities marked as "Present":\n\n' +
      missingRemarks.join('\n')
    );
    return;
  }
    const patientId = this.patientService.getPatientId();
    const doctorId = this.patientService.getDoctorId() || 0;

    console.log('Comorbidities Submit triggered');
    console.log('Patient ID:', patientId, 'Doctor ID:', doctorId);

    if (this.stage === 1) this.ptnstage = 2;
    else if (this.stage === 3) this.ptnstage = 4;
    else if (this.stage === 0) this.ptnstage = 0;
    else this.ptnstage = this.stage;

    const isUpdate = this.comorbiditiesID && this.comorbiditiesID > 0;
    const payload = {
      comorbiditiesID: isUpdate ? this.comorbiditiesID : 0,
      stage: this.ptnstage,
      patientID: patientId,
      doctorID: doctorId,
      flag: isUpdate ? 'U' : 'I',
      createdBy: doctorId,
      HT_Present: String(this.comorbidities['hypertension'].present ?? ''),
      HT_Remark: this.comorbidities['hypertension'].remarks,

      DB_Present: String(this.comorbidities['diabetes'].present ?? ''),
      DB_Remark: this.comorbidities['diabetes'].remarks,

      DD_Present: String(this.comorbidities['dyslipidemia'].present ?? ''),
      DD_Remark: this.comorbidities['dyslipidemia'].remarks,

      CLD_Present: String(this.comorbidities['liver'].present ?? ''),
      CLD_Remark: this.comorbidities['liver'].remarks,

      ND_Present: String(this.comorbidities['neuro'].present ?? ''),
      ND_Remark: this.comorbidities['neuro'].remarks,

      CD_Present: String(this.comorbidities['cardio'].present ?? ''),
      CD_Remark: this.comorbidities['cardio'].remarks,

      H_Present: String(this.comorbidities['hypo'].present ?? ''),
      H_Remark: this.comorbidities['hypo'].remarks,

      HTD_Present: String(this.comorbidities['hyper'].present ?? ''),
      HTD_Remark: this.comorbidities['hyper'].remarks,

      BD_Present: String(this.comorbidities['behavioural'].present ?? ''),
      BD_Remark: this.comorbidities['behavioural'].remarks,

      CKD_Present: String(this.comorbidities['kidney'].present ?? ''),
      CKD_Remark: this.comorbidities['kidney'].remarks,

      A_Present: String(this.comorbidities['asthma'].present ?? ''),
      A_Remark: this.comorbidities['asthma'].remarks,

      O_Present: String(this.comorbidities['osteo'].present ?? ''),
      O_Remark: this.comorbidities['osteo'].remarks,

      RA_Present: String(this.comorbidities['rheumatoid'].present ?? ''),
      RA_Remark: this.comorbidities['rheumatoid'].remarks,

      SS_Present: String(this.comorbidities['sclerosis'].present ?? ''),
      SS_Remark: this.comorbidities['sclerosis'].remarks,

      C_Present: String(this.comorbidities['cancer'].present ?? ''),
      C_Remark: this.comorbidities['cancer'].remarks,

      CMO_Present: String(this.comorbidities['others'].present ?? ''),
      CMO_Remark: this.comorbidities['others'].remarks
    };

    console.log('Submitting Comorbidities:', payload);

    this.http.httpPost(API_URLS.COMORBIDITIES_SAVE, payload).subscribe(
      (res: any) => {
        if (res.type === 'S') {
          this.isSaved = true;
          this.formValidation.showAlert('Comorbidities saved successfully', 'success'); // ✅

          this.http.httpGet('/Comorbidities/GetComorbidities').subscribe((getRes: any) => {
            if (getRes.type === 'S' && getRes.data?.length > 0) {
              this.formValidation.showAlert('Saved Successfully', 'success');
              alert('Saved Successfully');
              const latest = getRes.data[getRes.data.length - 1];
              const updatedPatientId = latest.patientID;
              const updatedDoctorId = doctorId;

              this.patientService.setPatientId(updatedPatientId);
              this.patientService.setDoctorId(updatedDoctorId);

              this.router.navigate(['/history'], {
                queryParams: {
                  patientId: updatedPatientId,
                  doctorId: updatedDoctorId
                }
              });
            } else {
              this.formValidation.showAlert('Please complete all fields before submitting', 'danger');
            }
          });

        } else {
          this.formValidation.showAlert(`Error: ${res.message || 'Unknown error'}`, 'danger'); // ✅
        }
      },
      error => {
        console.error('Error saving chief complaint:', error);
        this.formValidation.showAlert('Error saving chief complaint', 'danger');
      }
    );

  }

  goToHistory() {
    console.log('Comorbidities data:', this.comorbidities);
    this.router.navigate(['/history/${this.patientId}/${this.stage}']);
  }

  OnNext() {
    if (this.stage > 1) {
      this.router.navigate([`/assessment/${this.patientId}/${this.stage}`]

        , {
          state: {
            fromNavigation: true,
            tabId: this.tabId,
            patientId: this.patientId,
            isViewMode: this.isViewMode
          }
        });
    }
    else {
      this.router.navigate([`/history/${this.patientId}/${this.stage}`]

        , {
          state: {
            
            tabId: this.tabId,
            patientId: this.patientId,
            isViewMode: this.isViewMode,
            fromNavigation: true,
          }
        });
    }


  }
  back() {
    
    this.router.navigate([`/chiefComplaint/${this.patientId}/${this.stage}`], {

      state: {
        tabId: this.tabId,
        patientId: this.patientId,
            isViewMode: true
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

  // back() {
  //   this.router.navigate([`/follow-up-2/chiefComplaint/${this.patientId}`], {
  //     state: {
  //       tabId: this.tabId,
  //       patientId: this.patientId,
  //       isViewMode: this.isViewMode
  //     }
  //   });
  // }
}






