import { Component } from '@angular/core';
import { Router } from '@angular/router';
import { HttpserviceService } from '../../httpservice.service';
import { FormBuilder, FormsModule } from '@angular/forms';
import { AppComponent } from '../../app.component';
import { PatientHistoryService } from '../../Services/patient-history.service';
import { PatientService } from '../../Services/patient.service';
import { AssessmentService } from '../../Services/Assessment.service';


@Component({
  selector: 'app-assessment',
  templateUrl: './assessment3.component.html',
  styleUrl: './assessment3.component.css'
})
export class Assessment3Component {
  symptomAnswers: number[] = [];
  fssgAnswers: number[] = [];
 activeTabId: any = 2;
  assessmentForm: any;

  constructor(private fb: FormBuilder,private assessmentService: AssessmentService,private router: Router, private http: HttpserviceService,    private patientService: PatientService,
) {
    
    this.symptomAnswers = Array(12).fill(0);
    this.fssgAnswers = Array(12).fill(0);
       
    
  }

    tabId = 1;
stage = 0
  symptomScore: string = '';
     isViewMode = false;
isFollowUp: boolean = false;
  patientId: number | null = null;


  symptoms: string[] = ['Heartburn', 'Regurgitation', 'Retrosternal pain', 'Acid taste in the mouth'];
  fssgQuestions: string[] = [
    'Do you get heartburn?',
    'Does your stomach get bloated?',
    'Does your stomach ever feel heavy after meals?',
    'Do you rub your chest with your hand?',
    'Do you feel sick after meals?',
    'Do you get heartburn after meals?',
    'Do you have an unusual sensation in your throat?',
    'Do you feel full while eating meals?',
    'Do you feel things stuck when you swallow?',
    'Do you get bitter liquid in your throat?',
    'Do you burp a lot?',
    'Do you get heartburn if you bend over?'
  ];
  symptomScores = [
    { value: 0, label: '0 = Absence of symptoms' },
    { value: 1, label: '1 = Minimal awareness of symptoms, easily tolerated' },
    { value: 2, label: '2 = Awareness of symptoms which is bothersome but tolerable without impairment of sleep or daily living' },
    { value: 3, label: '3 = Symptoms hard to be tolerated interfering with daily activities and/or sleeping' }
  ];

  
  form = {
    symptomScore: null,
    lax: '',
    hill: '',
    laGrade: '',
    laRemarks: '',
    barrettsRemarks: '',
    hillGrade: '',
    hillRemarks: '',
    biopsyDate: '',
    biopsyReportAttached: false,
    biopsyRemarks: '',
    biopsyTest: '',
    manometryRemarks: '',
    manometryReportAttached: '',
    manometryTest: '',
    manometryDate: '',
    phRemarks: '',
    phReportAttached: false,
    phDate: '',
    phTestTaken: '',

  };

  laGrades = ['Grade A', 'Grade B', 'Grade C', 'Grade D'];
  hillGrades = ['Grade 1', 'Grade 2', 'Grade 3', 'Grade 4'];

  manometryTest: string = '';
  manometryDate: string = '';
  manometryReportAttached: string = '';
  manometryFiles: File[] = [];
  manometryRemarks: string = '';
  acceptedTypes: string = '.jpg,.jpeg,.png,.pdf';

  ngOnInit(): void {
    this.assessmentForm = this.fb.group({
    assessmentId: [null],
    pid: [null],
    q1: [0],
    q2: [0],
    q3: [0],
    q4: [0],
    q5: [0],
    q6: [0],
    q7: [0],
    q8: [0],
    q9: [0],
    q10: [0],
    q11: [0],
    q12: [0],
    acidRefluxSymptom: [''],
    dysmotity: [''],
    totalPoints: [''],
    heartburnNil: [false],
    heartburnMinimal: [false],
    heartburnModerate: [false],
    heartburnHeartburn: [false],
    regurgitationNil: [false],
    regurgitationMinimal: [false],
    regurgitationModerate: [false],
    regurgitationHeartburn: [false],
    retrosternalPainNil: [false],
    retrosternalPainMinimal: [false],
    retrosternalPainModerate: [false],
    retrosternalPainHeartburn: [false],
    acidTasteMouthNil: [false],
    acidTasteMouthMinimal: [false],
    acidTasteMouthModerate: [false],
    acidTasteMouthHeartburn: [false],
    eeLaxlesClassification: [false],
    eeAngelesGrade: [''],
    eeAgremarks: [''],
    eeBarrettRemark: [''],
    eeHillClassificationGrade: [''],
    eeHillRemarks: [''],
    pHimpedanceMonitoring: [false],
    pHimDate: [''],
    pHimAttached: [false],
    pHimAttachement: [''],
    pHimRemark: [''],
    manometryTest: [false],
    mtDate: [''],
    mtAttached: [false],
    mtAttachement: [''],
    mtRemark: [''],
    biopsy: [false],
    biopsyDate: [''],
    biopsyAttached: [false],
    biopsyAttachement: [''],
    biopsyRemark: [''],
    createdBy: [null],
    createdDt: [''],
    modifiedBy: [null],
    modifiedDt: [''],
    stage: [4]
  });
  const pid = this.patientService.getPatientId();
  console.log("Retrieved patient ID for assessment:", pid);


  this.loadAssessment(pid)

console.log("TABID", this.tabId)
         const state = history.state;
    this.tabId = state?.tabId ?? 1;
    this.patientId = state?.patientId;  
    console.log("PAtient ID", this.patientService.getPatientId())
    if (this.manometryReportAttached === 'no') {
      this.clearManometryAttachmentFields();
    }
          const currentUrl = this.router.url;
    this.isFollowUp = currentUrl.includes('followUpOne') || currentUrl.includes('followUpTwo');

  }

loadAssessment(pid: number): void {
  console.log("Calling loadAssessment for patient ID:", pid);

  this.assessmentService.getAssessmentById(pid,this.stage).subscribe(response => {
    if (response?.type === 'S' && response.data) {
      console.log('Assessment data received:', response.data);

      // Use your custom loadExistingData method to assign values
      this.loadExistingData(response.data);

    } else {
      console.warn('No assessment data found for patient ID:', pid);
    }
  }, error => {
    console.error('Error fetching assessment data:', error);
  });
}

loadExistingData(data: any) {
  this.patientId = data.pid;
  this.fssgAnswers = [
    data.q1, data.q2, data.q3, data.q4, data.q5, data.q6,
    data.q7, data.q8, data.q9, data.q10, data.q11, data.q12
  ];
  this.symptomAnswers = [
    data.heartburnScore, data.regurgitationScore, data.retrosternalScore, data.acidTasteScore
  ]; // however you store scores

  // For your form object properties:
  this.form.lax = data.eeLaxlesClassification ? 'yes' : 'no';
  this.form.laGrade = data.eeAngelesGrade;
  this.form.laRemarks = data.eeAgremarks;
  this.form.barrettsRemarks = data.eeBarrettRemark;
  this.form.hillGrade = data.eeHillClassificationGrade;
  this.form.hillRemarks = data.eeHillRemarks;

  // this.PHimpedanceMonitoring = data.pHimpedanceMonitoring ? 'yes' : 'no';
  this.phDate = data.pHimDate;
  this.phReportAttached = data.pHimAttached;
  this.phFiles = []; // if you have attachments, load them accordingly
  this.phRemarks = data.pHimRemark;

  this.manometryTest = data.manometryTest ? 'yes' : 'no';
  this.manometryDate = data.mtDate;
  this.manometryReportAttached = data.mtAttached ? 'yes' : 'no';
  this.manometryRemarks = data.mtRemark;
  this.manometryFiles = [];

  this.form.biopsyTest = data.biopsy ? 'yes' : 'no';
  this.form.biopsyDate = data.biopsyDate;
  this.form.biopsyReportAttached = data.biopsyAttached;
  this.biopsyFiles = [];
  this.form.biopsyRemarks = data.biopsyRemark;

  // any other assignments
}


  onManometryReportAttachedChange(): void {
    if (this.manometryReportAttached === 'no') {
      this.clearManometryAttachmentFields();
    }
  }

  getTotalSymptomScore(): number {
  return this.symptomAnswers.reduce((total, score) => total + Number(score), 0);
}


  getFssgScoreCounts(): number[] {
  const counts = [0, 0, 0, 0, 0]; // Index 0 → NEVER (0), ..., Index 4 → ALWAYS (4)
  for (const answer of this.fssgAnswers) {
    if (answer >= 0 && answer <= 4) {
      counts[answer]++;
    }
  }
  return counts;
}

getTotalResponses(): number {
  return this.getFssgScoreCounts().reduce((sum, val) => sum + val, 0);
}

  onManometryFileChange(event: Event): void {
    const input = event.target as HTMLInputElement;
    if (input.files) {
      this.manometryFiles = Array.from(input.files);
    }
  }
countFssgByValue(value: number): number {
  return this.fssgAnswers.filter((ans) => ans === value).length;
}



  addManometryFile(input: HTMLInputElement): void {
    if (input.files) {
      this.manometryFiles.push(...Array.from(input.files));
      input.value = '';
    }
  }

  private clearManometryAttachmentFields(): void {
    this.manometryFiles = [];
    this.manometryRemarks = '';
  }

  biopsyYes: boolean = false;
  biopsyNo: boolean = false;
  biopsyDate: string = '';
  biopsyTest: string = '';
  biopsyReportAttached: string = '';
  biopsyFiles: File[] = [];
  biopsyRemarks: string = '';

  onBiopsyReportAttachedChange(): void {
    if (this.biopsyReportAttached !== 'yes') {
      this.biopsyFiles = [];
      this.biopsyRemarks = '';
    }
  }


  onBiopsyFileChange(event: Event): void {
    const input = event.target as HTMLInputElement;
    if (input.files) {
      for (let i = 0; i < input.files.length; i++) {
        const file = input.files[i];
        const ext = file.name.split('.').pop()?.toLowerCase();
        if (ext && ['jpg', 'jpeg', 'png', 'pdf'].includes(ext)) {
          this.biopsyFiles.push(file);
        }
      }
      input.value = '';
    }
  }

  
  submitAssessment(): void {
     console.log('submitAssessment called');
     try{
    console.log('Building param object...');
 const param: any = {
      Flag: 'I',
      AssessmentId: 0, // 0 or existing ID for update
      PID: this.patientId,
      Q1: this.fssgAnswers[0],
      Q2: this.fssgAnswers[1],
      Q3: this.fssgAnswers[2],
      Q4: this.fssgAnswers[3],
      Q5: this.fssgAnswers[4],
      Q6: this.fssgAnswers[5],
      Q7: this.fssgAnswers[6],
      Q8: this.fssgAnswers[7],
      Q9: this.fssgAnswers[8],
      Q10: this.fssgAnswers[9],
      Q11: this.fssgAnswers[10],
      Q12: this.fssgAnswers[11],
      AcidRefluxSymptom: this.calculateAcidRefluxScore(), // optional: implement if needed
      Dysmotity: this.calculateDysmotilityScore(), // optional
    TotalPoints: this.calculateTotalFssgScore().toString(),


    
      // Heartburn symptoms
      HeartburnNil: this.symptomAnswers[0] === 0,
      HeartburnMinimal: this.symptomAnswers[0] === 1,
      HeartburnModerate: this.symptomAnswers[0] === 2,
      HeartburnHeartburn: this.symptomAnswers[0] === 3,

      // Regurgitation
      RegurgitationNil: this.symptomAnswers[1] === 0,
      RegurgitationMinimal: this.symptomAnswers[1] === 1,
      RegurgitationModerate: this.symptomAnswers[1] === 2,
      RegurgitationHeartburn: this.symptomAnswers[1] === 3,

      // Retrosternal pain
      RetrosternalPainNil: this.symptomAnswers[2] === 0,
      RetrosternalPainMinimal: this.symptomAnswers[2] === 1,
      RetrosternalPainModerate: this.symptomAnswers[2] === 2,
      RetrosternalPainHeartburn: this.symptomAnswers[2] === 3,

      // Acid taste in mouth
      AcidTasteMouthNil: this.symptomAnswers[3] === 0,
      AcidTasteMouthMinimal: this.symptomAnswers[3] === 1,
      AcidTasteMouthModerate: this.symptomAnswers[3] === 2,
      AcidTasteMouthHeartburn: this.symptomAnswers[3] === 3,

      // Endoscopy
      EeLaxlesClassification: this.form.lax === 'yes',
      EeAngelesGrade: this.form.laGrade,
      EeAgremarks: this.form.laRemarks,
      EeBarrettRemark: this.form.barrettsRemarks,
      EeHillClassificationGrade: this.form.hillGrade,
      EeHillRemarks: this.form.hillRemarks,

      // PH impedance
      PHimpedanceMonitoring: this.phYes,
      PHimDate: this.phDate,
      PHimAttached: this.phReportAttached === 'yes',
      PHimAttachement: this.phFiles.map(f => f.name).join(', '), // Adjust if actual upload needed
      PHimRemark: this.phRemarks,

      // Manometry
      ManometryTest: this.manometryTest === 'yes',
      MtDate: this.manometryDate,
      MtAttached: this.manometryReportAttached === 'yes',
      MtAttachement: this.manometryFiles.map(f => f.name).join(', '),
      MtRemark: this.manometryRemarks,

      // Biopsy
      Biopsy: this.biopsyYes,
      BiopsyDate: this.biopsyDate,
      BiopsyAttached: this.biopsyReportAttached === 'yes',
      BiopsyAttachement: this.biopsyFiles.map(f => f.name).join(', '),
      BiopsyRemark: this.biopsyRemarks,

      // Audit
      CreatedBy: 1, // Replace with actual user ID
    };
     
   
console.log('Sending param to API:', param);
    this.http.httpPost('/Assessment/SaveAssessment', param).subscribe(
      (res: any) => {
        console.log('Assessment saved:', res);
        this.router.navigate(['/follow-up-1/diagnosis']);
      },
      (err: any) => {
        console.error('Failed to save assessment:', err);
      }
    );}catch (e) {
    console.error('Error in submitAssessment:', e);
  }
  }

  calculateTotalFssgScore(): string {
  return this.fssgAnswers.reduce((sum, val) => sum + val, 0).toString();
}

calculateAcidRefluxScore(): string {
  const acidIndexes = [0, 3, 5, 6, 8, 9, 11]; // Q1, Q4, Q6, Q7, Q9, Q10, Q12
  return acidIndexes.reduce((sum, i) => sum + this.fssgAnswers[i], 0).toString();
}

calculateDysmotilityScore(): string {
  const dysmotilityIndexes = [1, 2, 4, 7, 10]; // Q2, Q3, Q5, Q8, Q11
  return dysmotilityIndexes.reduce((sum, i) => sum + this.fssgAnswers[i], 0).toString();
}



  addBiopsyFile(fileInput: HTMLInputElement): void {
    const files = fileInput.files;
    if (files) {
      for (let i = 0; i < files.length; i++) {
        const file = files[i];
        const ext = file.name.split('.').pop()?.toLowerCase();
        if (ext && ['jpg', 'jpeg', 'png', 'pdf'].includes(ext)) {
          this.biopsyFiles.push(file);
        }
      }
      fileInput.value = '';
    }
  }

  onSave() {
    this.router.navigate(['/diagnosis']);
  }
onTabClick(tabId: number) {
  this.activeTabId = tabId;
  console.log('Tab id', this.activeTabId);
}

  phYes: boolean = false;
  phNo: boolean = false;
  phDate: string = '';
  phReportYes: boolean = false;
  phReportNo: boolean = false;
  phRemarks: string = '';



  phReportAttached: string = '';
  phFiles: File[] = [];


  onPhFileChange(event: Event): void {
    const input = event.target as HTMLInputElement;
    if (input.files) {
      this.phFiles = Array.from(input.files);
    }
  }

  addPhFile(input: HTMLInputElement): void {
    if (input.files) {
      this.phFiles.push(...Array.from(input.files));
      input.value = '';
    }
  }

  onPhReportChange(): void {
    if (this.phReportAttached === 'no') {
      this.phFiles = [];
      this.phRemarks = '';
    }
  }

onNext() {
  const currentUrl = this.router.url;
  const patientId = this.patientId;

  if (currentUrl.includes('follow-up-1')) {
    this.router.navigate(['/follow-up-1/diagnosis'], {
    state: {
      tabId: this.tabId,
      patientId: this.patientId,
      isViewMode: this.isViewMode
    }
  });
  } else {
    // Optional: route to next section or back to dashboard
    this.router.navigate(['/follow-up-1/diagnosis'], {
    state: {
      tabId: this.tabId,
      patientId: this.patientId,
      isViewMode: this.isViewMode
    }
  });
  }
}

    OnNext(){
    this.router.navigate([`/followUpTwo/management/${this.patientService.getPatientId()}`], {
    state: {
      tabId: this.tabId,
      patientId: this.patientService.getPatientId(),
      isViewMode: this.isViewMode
    }
  });

}
goback(){
    this.router.navigate([`/medical-examination`], {
    state: {
      tabId: this.tabId,
      patientId: this.patientId,
      isViewMode: this.isViewMode
    }
  });

}
  back() {
  this.router.navigate([`followUpTwo/comorbidities/${this.patientService.getPatientId()}`], {
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

}


