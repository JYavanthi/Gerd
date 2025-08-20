import { Component } from '@angular/core';
import { ActivatedRoute, Params, Router } from '@angular/router';
import { PatientService } from '../../Services/patient.service';
import { HttpserviceService } from '../../httpservice.service';
import { API_URLS } from '../../shared/API-URLs';
import { ComorbiditiesService } from '../../Services/comorbidities.service';
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
export class ComorbiditiesComponent2 {
  tabId = 1;
  patientId: number | null = null;
  doctorId: number | null = null;
  isViewMode = false;
  isFollowUp: boolean = false;
  PatientHistoryID: number | null = null;
  stage: number=0;
  activeTabId: any = 2;

  // Define an object to track each comorbidity's status and remarks
  comorbidities: Comorbidities = {
    hypertension: { present: null, remarks: '' },
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

  constructor(
    private route: ActivatedRoute,
    private router: Router,
    private patientService: PatientService,
    private http: HttpserviceService, private comorbiditiesService: ComorbiditiesService,
  ) {

    const path = this.router.url;
    const isFollowUp1 = path.includes('followUpOne');
    const isFollowUp2 = path.includes('followUpTwo');
    this.route.queryParams.subscribe((params: Params) => {
      this.patientId = +params['patientId'] || null;
      this.doctorId = +params['doctorId'] || null;

      if (this.patientId) this.patientService.setPatientId(this.patientId);
      if (this.doctorId) this.patientService.setDoctorId(this.doctorId);
    });
  }

 ngOnInit(): void {
  const state = history.state;
  const currentUrl = this.router.url;

  this.tabId = state?.tabId ?? 1;
  this.patientId = state?.patientId ?? null;
  this.isViewMode = state?.isViewMode ?? false;
  this.isFollowUp = currentUrl.includes('followUpOne') || currentUrl.includes('followUpTwo');

  console.log('📌 tabId:', this.tabId);
  console.log('📌 patientId:', this.patientId);
  console.log('📌 isViewMode:', this.isViewMode);
    this.fetchComorbiditiesData(this.patientService.getPatientId());

  if (this.patientService.getPatientId()) {
    this.fetchComorbiditiesData(this.patientService.getPatientId());
  }
  console.log("Patient ID", this.patientService.getPatientId())
}
  fetchComorbiditiesData(patientId: number): void {
    this.comorbiditiesService.getComorbiditiesById(patientId,this.stage).subscribe({
      next: (res: any) => {
        if (res.type === 'S' && res.data) {
          const data = res.data;
          this.stage = data.stage;
          console.log("Stage", this.stage)
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

  // Method to handle radio button change

  updateComorbidityStatus(comorbidity: string, value: boolean): void {
    if (this.comorbidities[comorbidity]) {
      this.comorbidities[comorbidity].present = value;
      if (!value) {
        this.comorbidities[comorbidity].remarks = '';
      }
    }
  }

  Submit() {
    const patientId = this.patientService.getPatientId();
    const doctorId = this.patientService.getDoctorId() || 0;

    console.log('Comorbidities Submit triggered');
    console.log('Patient ID:', patientId, 'Doctor ID:', doctorId);


    const payload = {
      comorbiditiesID: 0,
      stage: 2,
      patientID: patientId,
      doctorID: doctorId,
      flag: 'I',
      createdBy: 0,

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
          console.log('Comorbidities saved. Fetching latest data...');

          // Now fetch latest comorbidities using GET
          this.http.httpGet('/Comorbidities/GetComorbidities').subscribe((getRes: any) => {
            if (getRes.type === 'S' && getRes.data?.length > 0) {
              const latest = getRes.data[getRes.data.length - 1];
              const updatedPatientId = latest.patientID;
              const updatedDoctorId = doctorId; // or from latest if available

              this.patientService.setPatientId(updatedPatientId);
              this.patientService.setDoctorId(updatedDoctorId);

              console.log('Latest Comorbidities fetched. Navigating...');
              this.router.navigate(['/history'], {
                queryParams: {
                  patientId: updatedPatientId,
                  doctorId: updatedDoctorId
                }
              });
            } else {
              alert('Saved but failed to fetch latest comorbidities');
            }
          });

        } else {
          alert('Error: ' + (res.message || 'Unknown error'));
        }
      },
      error => {
        console.error('HTTP Error:', error);
        alert('Failed to save comorbidities');
      }
    );
  }

onTabClick(tabId: number) {
  this.activeTabId = tabId;
  console.log('Tab id', this.activeTabId);
}

  goToHistory() {
    console.log('Comorbidities data:', this.comorbidities);
    this.router.navigate(['/history']);
  }
  OnNext(){
    this.router.navigate([`/history`], {
    state: {
      tabId: this.tabId,
      patientId: this.patientId,
      isViewMode: this.isViewMode
    }
  });
}
goback(){
    this.router.navigate([`/chiefComplaint`], {
    state: {
      tabId: this.tabId,
      patientId: this.patientId,
      isViewMode: this.isViewMode
    }
  });

}

onNext() {
   this.router.navigate([`followUpOne/assessment/${this.patientService.getPatientId()}`], {
    state: {
      tabId: this.tabId,
      patientId: this.patientService.getPatientId(),
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
  
  back() {
  this.router.navigate([`/followUpOne/chiefComplaint/${this.patientService.getPatientId()}`], {
    state: {
      tabId: this.tabId,
      patientId: this.patientService.getPatientId(),
      isViewMode: this.isViewMode
    }
  });
}
}



