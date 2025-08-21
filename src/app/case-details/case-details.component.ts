import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { DemographicService } from '../Services/demographic.service';
import { PatientService } from '../Services/patient.service';
import { Case } from '../Services/case-data.services';
import { API_URLS } from '../shared/API-URLs';
import { HttpserviceService } from '../httpservice.service';
import { FormvalidationService } from '../formvalidation.service';

@Component({
  selector: 'app-case-details',
  templateUrl: './case-details.component.html',
  styleUrls: ['./case-details.component.css'],
})
export class CaseDetailsComponent implements OnInit {
   patientData?: Case;
  patientForm: FormGroup;
  caseData: any;
  isViewMode: boolean = false;
  formData: any;
  stage: number = 0;
  routerstage: number = 0;
  patientId: number | null = null;
  doctorId: number | null = null;
  tabId = 1
  activeTabId: any = 1;
  states: Array<{ id: number; name: string }> = [];
  cities: any;
  stateval: any;
  userData:any;
  
  constructor(
    private fb: FormBuilder,
    private router: Router,
    private route: ActivatedRoute,
    private formValidation: FormvalidationService,
    private http: HttpserviceService,
    private demographicService: DemographicService,
    private patientService: PatientService,
  ) {
    this.patientForm = this.fb.group({
      patient: [{ value: '', disabled: true }],
      subjectNumber: [{ value: '', disabled: true }],
      date: [{ value: '', disabled: true }],
      age: [{ value: '', disabled: true }],
      gender: [{ value: '', disabled: true }],
      education: [{ value: '', disabled: true }],
      occupation: [{ value: '', disabled: true }],
      state: [{ value: '', disabled: true }],
      city: [{ value: '', disabled: true }],
      pincode: [{ value: '', disabled: true }],
      placeType: [{ value: '', disabled: true }],
      socioeconomic: [{ value: '', disabled: true }],
      income: [{ value: '', disabled: true }],
      diet: [{ value: '', disabled: true }],
      initial: [{ value: '', disabled: true }],
      pastHistory: [{ value: '', disabled: true }],
      status: [{ value: '', disabled: true }],
    });
  }

  ngOnInit(): void {
    const routeParams = this.route.snapshot.paramMap;
    this.patientId = +routeParams.get('id')!;
    //this.stage = +routeParams.get('stage')!;
    this.routerstage = +routeParams.get('stage')!;
    const state = history.state;

    this.patientId = state?.patientId;
    this.tabId = state?.tabId ?? this.getActiveTabId();
    const id = this.route.snapshot.paramMap.get('id');
    this.stage = state.stage ?? 0;
    this.doctorId = this.patientService.getDoctorId();
    
    this.isViewMode = state.isViewMode ?? false;
    this.formData = state.data;
     
    this.loadStates();
    
    if (id) {
      this.demographicService.getDemographicDetailsByPatientId(+id).subscribe({
        next: (response) => {
          console.log('📦 Full API response:', response);

          const data = response?.data;
          if (!data) {
            console.warn('⚠️ No demographic data found in response.');
            return;
          }

          this.caseData = data; // store for arrows and follow-up
          this.stage = data.stage;
          //this.stateval=data.state
          this.stateval=this.caseData.state;
          console.log('state data',this.stateval)
          if (!this.tabId) {
            this.tabId = this.getActiveTabId();
          }
          this.patientForm.patchValue({
            patient: data.patientId,
            subjectNumber: data.subjectNo,
            date: data.date ? this.formatDate(data.date) : '',
            age: data.age ?? '',
            gender: data.gender || '',
            education: data.education || '',
            occupation: data.occupation || '',
            state: data.state,
            city: data.city,
            pincode: data.pincode,
            placeType: data.placeType || '',
            socioeconomic: data.socioeconomicStatus || '',
            income: data.familyIncome || '',
            diet: data.diet || '',
            initial: data.initial || '',
            pastHistory: data.pastHistory || '',
            status: data.status || '',

          });
          
          this.getCities(this.stateval);
          console.log('✅ Form values patched.');
        },
        error: (err) => {
          console.error('❌ Error loading demographic data:', err);
        },
      });
    }
  }

  getCity(){
     this.http.httpGet(API_URLS.CITY_GET, { stateId: 4026 }).subscribe({
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
    if (this.stateval!=='') {
       this.patientForm.patchValue({
        //city: this.getCities(this.stateval),
      }); 
    }
    }}

  formatDate(dateString: string): string {
    const date = new Date(dateString);
    const year = date.getFullYear();
    const month = (date.getMonth() + 1).toString().padStart(2, '0');
    const day = date.getDate().toString().padStart(2, '0');
    return `${year}-${month}-${day}`;
  }

  downloadPDF() {
    const element = document.getElementById('pdf-content');
    const buttons = document.querySelector('.buttons') as HTMLElement;

    if (!element) return;
    if (buttons) buttons.style.display = 'none';

    import('html2pdf.js')
      .then((html2pdf) => {
        const options = {
          filename: 'case-details.pdf',
          image: { type: 'jpeg', quality: 0.98 },
          html2canvas: { scale: 2 },
          jsPDF: { unit: 'pt', format: 'a4', orientation: 'portrait' },
        };

        html2pdf.default().set(options).from(element).save().then(() => {
          if (buttons) buttons.style.display = 'flex';
        });
      })
      .catch((error) => {
        console.error('❌ Failed to generate PDF:', error);
        if (buttons) buttons.style.display = 'flex';
      });
  }

  close() {
    this.router.navigate(['/dashboard']);
  }


  onNext(followUpType: 'followUp1' | 'followUp2') {
    const routePrefix = followUpType === 'followUp1' ? 'follow-up-1' : 'follow-up-2';
    const patientId = this.caseData?.patientId;

    if (!patientId) {
      console.error('❌ No patient ID found in caseData');
      return;
    }


    this.router.navigate([`chiefComplaint/${patientId}/${this.routerstage}`], {
      state: {
        data: this.caseData,
        isViewMode: this.isViewMode,
        patientId: patientId,
        // stage: nextStage
      },
      queryParams: {
        //stage: nextStage
      }
    });
  }

  onTabClick(tabId: number) {
    this.activeTabId = tabId;
    console.log('Tab id', this.activeTabId);
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
  getActiveTabId(): number {
    if (this.stage === 1) return 1; // Baseline completed
    if (this.caseData?.followUp1?.toLowerCase() === 'completed') return 2;
    if (this.caseData?.followUp2?.toLowerCase() === 'completed') return 3;
    return 0; // None active yet
  }

  get currentTabName(): string {
    return this.tabId === 1 ? 'Baseline' : this.tabId === 2 ? 'Follow-Up 1' : 'Follow-Up 2';
  }

  allStatusesPending(): boolean {
    const baseline = this.caseData?.baseline?.toLowerCase();
    const followUp1 = this.caseData?.followUp1?.toLowerCase();
    const followUp2 = this.caseData?.followUp2?.toLowerCase();

    return [baseline, followUp1, followUp2].every((status) => status === 'pending');
  }

  navigateToFollowUp(followUpType: 'followUp1' | 'followUp2') {
    const routePrefix = followUpType === 'followUp1' ? 'follow-up-1' : 'follow-up-2';
    const patientId = this.caseData?.patientId;

    if (!patientId) {
      console.error('❌ No patient ID found in caseData');
      return;
    }
    this.router.navigate([`/${routePrefix}/chiefComplaint`, patientId], {
      state: {
        data: this.caseData,
        isViewMode: this.isViewMode
      }
    });
  }

  Followup1() {
    console.log('Navigating to:', `/case-details/${this.patientId}`);

    this.router.navigate([`/case-details/${this.patientId}`], {
      state: {
        patientId: this.patientId,
        tabId: 2,
        stage: this.stage,
        data: this.caseData,
        isViewMode: this.isViewMode

      }
    });
    console.log("Tab id", this.tabId)
  }

  Followup2() {
    console.log('Navigating to:', `/case-details/${this.patientId}`);

    this.router.navigate([`/case-details/${this.patientId}`], {
      state: {
        patientId: this.patientId,
        tabId: 3,
        stage: this.stage,
        data: this.caseData,
        isViewMode: this.isViewMode
      }
    });
    console.log("Tab id", this.tabId)
  }
  onNext2() {
    console.log("this is followup one")
    console.log("Tabid", this.tabId)
    this.router.navigate([`chiefComplaint/${this.patientId}/${this.stage}`], {
      state: {
        data: this.caseData,
        isViewMode: this.isViewMode
      }
    });
  }
  onNext3() {
    console.log("this is followup one")
    console.log("Tabid", this.tabId)
    this.router.navigate([`chiefComplaint/${this.patientId}/${this.stage}`], {
      state: {
        data: this.caseData,
        isViewMode: this.isViewMode
      }
    });
  }
}
