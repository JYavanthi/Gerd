import { Component, Input } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { HttpserviceService } from '../httpservice.service';
import { FormBuilder, FormsModule } from '@angular/forms';
import { AppComponent } from '../app.component';
import { PatientHistoryService } from '../Services/patient-history.service';
import { PatientService } from '../Services/patient.service';
import { AssessmentService } from '../Services/Assessment.service';
import { FormvalidationService } from '../formvalidation.service';
import { ifError } from 'node:assert';


@Component({
  selector: 'app-assessment',
  templateUrl: './assessment.component.html',
  styleUrl: './assessment.component.css'
})
export class AssessmentComponent {
  symptomAnswers: number[] = [];
  fssgAnswers: number[] = [];
  assessmentForm: any;
  isDataLoaded: boolean = false;



  constructor(private router: Router, private fb: FormBuilder, private formValidation: FormvalidationService, private http: HttpserviceService, private assessmentService: AssessmentService, private patientService: PatientService, public route: ActivatedRoute,) {

    this.fssgAnswers = Array(12).fill(null);
    this.symptomAnswers = Array(12).fill(null);
    const pid = this.router.url.includes('id');
    const stage = this.router.url.includes('stage');
    console.log("constructor patient ID for assessment:", pid, stage);

  }

  tabId = 1;
  //stage = 0
  @Input() stage: number = 0;
  symptomScore: number = 0; 
  isViewMode = false;
  isFollowUp: boolean = false;
  @Input() patientId: number | null = null;
  isSaved: boolean = false;
  @Input() isPrintMode = false;


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
     this.stage = Number(this.route.snapshot.params['stage']|| 0);
    const allowedWithoutSave = [1, 3, 5];
  if (allowedWithoutSave.includes(this.stage)) {
    this.isSaved = true;
  }


    
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
      phDate: [''],
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
      stage: [''],
      symptomScore: [null],
      
    });
    console.log("TABID", this.tabId)
    const state = history.state;
    this.tabId = state?.tabId ?? 1;
    this.patientId = state?.patientId;
    if (this.manometryReportAttached === 'no') {
      this.clearManometryAttachmentFields();
    }
    const currentUrl = this.router.url;
    this.isFollowUp = currentUrl.includes('follow-up-1') || currentUrl.includes('follow-up-2');
    const pid = this.patientService.getPatientId();

    const fromNavigation = state?.fromNavigation === true;

    if (fromNavigation && this.patientId) {
      console.log("Navigated back → loading assessment data...");
      this.loadAssessment(this.patientId);
    } else {
      console.log("Fresh entry or direct navigation → skipping data patch");
    }

  }

  today: string = new Date().toISOString().split('T')[0];

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
  loadAssessment(pid: number): void {
    console.log("Calling loadAssessment for patient ID:", pid);

    this.assessmentService.getAssessmentById(pid ,this.stage).subscribe(response => {
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
    this.isDataLoaded = true;

  }
  loadExistingData(data: any): void {
    this.patientId = data.pid;

    this.fssgAnswers = [
      data.q1, data.q2, data.q3, data.q4, data.q5, data.q6,
      data.q7, data.q8, data.q9, data.q10, data.q11, data.q12
    ];

    this.symptomAnswers = [
      data.heartburnNil ? 0 : data.heartburnMinimal ? 1 : data.heartburnModerate ? 2 : 3,
      data.regurgitationNil ? 0 : data.regurgitationMinimal ? 1 : data.regurgitationModerate ? 2 : 3,
      data.retrosternalPainNil ? 0 : data.retrosternalPainMinimal ? 1 : data.retrosternalPainModerate ? 2 : 3,
      data.acidTasteMouthNil ? 0 : data.acidTasteMouthMinimal ? 1 : data.acidTasteMouthModerate ? 2 : 3
    ];

    const formatDate = (dateStr: string) => dateStr ? dateStr.split('T')[0] : '';
    console.log('data assessment',data)
    this.assessmentForm.patchValue({
      assessmentId: data.assessmentId,
      pid: data.pid,
      q1: data.q1,
      q2: data.q2,
      q3: data.q3,
      q4: data.q4,
      q5: data.q5,
      q6: data.q6,
      q7: data.q7,
      q8: data.q8,
      q9: data.q9,
      q10: data.q10,
      q11: data.q11,
      q12: data.q12,

      acidRefluxSymptom: data.acidRefluxSymptom,
      dysmotity: data.dysmotity,
      totalPoints: data.totalPoints,

      heartburnNil: data.heartburnNil,
      heartburnMinimal: data.heartburnMinimal,
      heartburnModerate: data.heartburnModerate,
      heartburnHeartburn: data.heartburnHeartburn,

      regurgitationNil: data.regurgitationNil,
      regurgitationMinimal: data.regurgitationMinimal,
      regurgitationModerate: data.regurgitationModerate,
      regurgitationHeartburn: data.regurgitationHeartburn,

      retrosternalPainNil: data.retrosternalPainNil,
      retrosternalPainMinimal: data.retrosternalPainMinimal,
      retrosternalPainModerate: data.retrosternalPainModerate,
      retrosternalPainHeartburn: data.retrosternalPainHeartburn,

      acidTasteMouthNil: data.acidTasteMouthNil,
      acidTasteMouthMinimal: data.acidTasteMouthMinimal,
      acidTasteMouthModerate: data.acidTasteMouthModerate,
      acidTasteMouthHeartburn: data.acidTasteMouthHeartburn,
      

      eeLaxlesClassification: data.eeLaxlesClassification,
      eeAngelesGrade: data.eeAngelesGrade,
      eeAgremarks: data.eeAgremarks,
      eeBarrettRemark: data.eeBarrettRemark,
      eeHillClassificationGrade: data.eeHillClassificationGrade,
      eeHillRemarks: data.eeHillRemarks,

      pHimpedanceMonitoring: data.pHimpedanceMonitoring,
      phDate: data.pHimDate?formatDate(data.pHimDate):'',
      pHimAttached: data.pHimAttached,
      pHimAttachement: data.pHimAttachement,
      pHimRemark: data.pHimRemark,

      manometryTest: data.manometryTest,
      mtDate: data.mtDate?formatDate(data.mtDate):'',
      mtAttached: data.mtAttached,
      mtAttachement: data.mtAttachement,
      mtRemark: data.mtRemark,

      biopsy: data.biopsy,
      biopsyDate: data.biopsyDate?formatDate(data.biopsyDate):'',
      biopsyAttached: data.biopsyAttached,
      biopsyAttachement: data.biopsyAttachement,
      biopsyRemark: data.biopsyRemark,

      createdBy: data.createdBy,
      createdDt: data.createdDt,
      modifiedBy: data.modifiedBy,
      modifiedDt: data.modifiedDt,
      stage: data.stage,
      
    });
    // View model helpers (not part of form)
    this.form.symptomScore = data.symptomScore;
    this.form.lax = data.eeLaxlesClassification ? 'Yes' : 'No';
    this.form.laGrade = data.eeAngelesGrade?? '';
    this.form.laRemarks = data.eeAgremarks?? '';
    this.form.barrettsRemarks = data.eeBarrettRemark;
    this.form.hill = data.eeHillClassificationGrade ? 'Yes' : 'No';
    this.form.hillRemarks = data.eeHillRemarks;
    
    this.phYes = data.pHimpedanceMonitoring ? 'yes' : 'no';
    this.phDate = data.PHimDate?new Date(data.PHimDate).toISOString() : '2025-08-20T00:00:00Z';
    this.phReportAttached = data.pHimAttached ? 'yes' : 'no';
    this.phRemarks = data.pHimRemark?? '';

    this.manometryTest = data.manometryTest ? 'yes' : 'no';
    this.manometryDate = data.mtDate?formatDate(data.mtDate): '2025-08-20T00:00:00Z';
    this.manometryReportAttached = data.mtAttached ? 'yes' : 'no';
    this.manometryRemarks = data.mtRemark??'';

    this.biopsyTest = data.biopsy ? 'yes' : 'no';
    this.biopsyDate = data.biopsyDate?formatDate(data.biopsyDate):'2025-08-20T00:00:00Z';
    this.biopsyReportAttached = data.biopsyAttached ? 'yes' : 'no';
    this.biopsyRemarks = data.biopsyRemark??'';

    this.stage = data.stage;
  }

  ptnstage: number = 0;

  submitAssessment(): void {
    console.log('submitAssessment called');
    if (this.stage === 1) this.ptnstage = 2;
    else if (this.stage === 3) this.ptnstage = 4;
    else if (this.stage === 0) this.ptnstage = 0;
    else this.ptnstage = this.stage;

    try {
      console.log('Building param object...');
      const param: any = {
        Flag: 'I',
        stage: this.ptnstage,
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
        EeAngelesGrade: this.form.laGrade?? '',
        EeAgremarks: this.form.laRemarks?? '',
        EeBarrettRemark: this.form.barrettsRemarks?? '',
        EeHillClassificationGrade: this.form.hillGrade?? '',
        EeHillRemarks: this.form.hillRemarks?? '',

        // PH impedance
        PHimpedanceMonitoring: this.phYes == 'yes',
        pHimDate: this.phDate ? new Date(this.phDate).toISOString() :  '2025-08-20T00:00:00Z',
        pHimAttached: this.phReportAttached === 'yes',
        pHimAttachement: this.phFiles.map(f => f.name).join(', ')?? '', // Adjust if actual upload needed
        pHimRemark: this.phRemarks?? '',

        ManometryTest: this.manometryTest === 'yes',
        MtDate: this.manometryDate ? new Date(this.manometryDate).toISOString() :  '2025-08-20T00:00:00Z',
        MtAttached: this.manometryReportAttached === 'yes',
        MtAttachement: this.manometryFiles.map(f => f.name).join(', ')?? '',
        MtRemark: this.manometryRemarks?? '',

        // Biopsy
        Biopsy: this.biopsyYes?? '',
        BiopsyDate: this.biopsyDate ? new Date(this.biopsyDate).toISOString() :  '2025-08-20T00:00:00Z',
        BiopsyAttached: this.biopsyReportAttached === 'yes',
        BiopsyAttachement: this.biopsyFiles.map(f => f.name).join(', ')?? '',
        BiopsyRemark: this.biopsyRemarks?? '',

        // Audit
        CreatedBy: this.patientService.getDoctorId(),
        totalSymptomScore:this.getTotalSymptomScore(),
        symptomScore:this.form.symptomScore
      };

      console.log('Sending param to API:', param);
      this.http.httpPost('/Assessment/SaveAssessment', param).subscribe(
        (res: any) => {
             if (res.type === 'E') {
             console.error('Failed to save assessment:');
             return;
          }
          this.isSaved = true;
          alert('Saved Successfully'); // ← Test this
          this.formValidation.showAlert('Saved Successfully', 'success');
          console.log('Assessment saved:', res);
        },
        (err: any) => {
          console.error('Failed to save assessment:', err);
        }
      );
    } catch (e) {
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

  phYes: any = false;
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
    if (this.stage > 1) {
      this.router.navigate([`/managament/${this.patientId}/${this.stage}`], {
        state: {
          tabId: this.tabId,
          patientId: this.patientService.getPatientId(),
          isViewMode: this.isViewMode,
          fromNavigation: true
        }
      });
    } else {
      this.router.navigate([`/diagnosis/${this.patientId}/${this.stage}`], {
        state: {
          tabId: this.tabId,
          patientId: this.patientService.getPatientId(),
          isViewMode: this.isViewMode,
          fromNavigation: true
        }
      });
    }
  }

  //     OnNext(){
  //     this.router.navigate([`/diagnosis/${this.patientId}/${this.stage}`], {
  //     state: {
  //       tabId: this.tabId,
  //       patientId: this.patientId,
  //       isViewMode: this.isViewMode
  //     }
  //   });
  // }
 goBack() {
  if (this.stage <= 1) {
    this.router.navigate([`/medical-examination/${this.patientId}/${this.stage}`], {
      state: {
        tabId: this.tabId,
        patientId: this.patientId,
        isViewMode: this.isViewMode,
        fromAssessment: true
      }
    });
  } else {
    this.router.navigate([`/comorbidities/${this.patientId}/${this.stage}`], {
      state: {
        tabId: this.tabId,
        patientId: this.patientId,
        isViewMode: this.isViewMode,
        fromAssessment: true
      }
    });
  }
}

  // back() {
  //   this.router.navigate([`/medical-examination/${this.patientId}/${this.stage}`], {
  //     state: {
  //       tabId: this.tabId,
  //       patientId: this.patientId,
  //       isViewMode: this.isViewMode,
  //       fromAssessment: true
  //     }
  //   });
  // }

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


