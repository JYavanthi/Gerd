
import { Component, ElementRef, Input, SimpleChanges, ViewChild } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { HttpserviceService } from '../httpservice.service';
import { FormBuilder, FormsModule, Validators } from '@angular/forms';
import { PatientService } from '../Services/patient.service';
import { AssessmentService } from '../Services/Assessment.service';
import { FormvalidationService } from '../formvalidation.service';
import { ifError } from 'node:assert';
import { HttpParams } from '@angular/common/http';
import { API_URLS } from '../shared/API-URLs';


@Component({
  selector: 'app-assessment',
  templateUrl: './assessment.component.html',
  styleUrls: ['./assessment.component.css']
})
export class AssessmentComponent {
  @ViewChild('phFileInput') phFileInput!: ElementRef;
  symptomAnswers: number[] = [];
  fssgAnswers: number[] = [];
  assessmentForm: any;
  isDataLoaded: boolean = false;


  constructor(private router: Router, private fb: FormBuilder, private formValidation: FormvalidationService, private http: HttpserviceService, private assessmentService: AssessmentService, private patientService: PatientService, public route: ActivatedRoute,) {

    this.fssgAnswers = Array(12).fill(null);
    this.symptomAnswers = Array(12).fill(null);

  }

  tabId = 1;
  @Input() stage: number = 0;
  // symptomScore: number = 0;
     symptomScore: any;

  isViewMode = false;
  isFollowUp: boolean = false;
  @Input() patientId: any;
  isSaved: boolean = false;
  @Input() isPrintMode = false;
  @Input() data: any;
  doctorId: any;
  files: File[] = [];


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




  // form = {
  //   symptomScore: '',
  //   lax: '',
  //   hill: '',
  //   laGrade: '',
  //   laRemarks: '',
  //   barrettsRemarks: '',
  //   hillGrade: '',
  //   hillRemarks: '',
  //   biopsyDate: '',
  //   biopsyReportAttached: false,
  //   biopsyRemarks: '',
  //   biopsyTest: '',
  //   manometryRemarks: '',
  //   manometryReportAttached: '',
  //   manometryTest: '',
  //   manometryDate: '',
  //   phRemarks: '',
  //   phReportAttached: false,
  //   phDate: '',
  //   phTestTaken: '',

  // };

  // symptomScore: Number ;
  lax: string = '';
  laGrade: string = '';
  laRemarks: string = '';
  barrettsRemarks: string = '';
  hill: string = '';
  hillGrade: string = '';
  hillRemarks: string = '';
  laGrades = ['Grade A', 'Grade B', 'Grade C', 'Grade D'];
  hillGrades = ['Grade 1', 'Grade 2', 'Grade 3', 'Grade 4'];

  acceptedTypes: string = '.jpg,.jpeg,.png,.pdf';

  phYes: any = false;
  phNo: boolean = false;
  phDate: string = '';
  phReportYes: boolean = false;
  phReportNo: boolean = false;
  phRemarks: string = '';

  manometryTest: string = '';
  manometryDate: string = '';
  manometryReportAttached: string = '';
  manometryFiles: any[] = [];
  manometryRemarks: string = '';

  phReportAttached: string = '';
  phFiles: any[] = [];

  biopsyYes: boolean = false;
  biopsyNo: boolean = false;
  biopsyDate: string = '';
  biopsyTest: string = '';
  biopsyReportAttached: string = '';
  biopsyFiles: any[] = [];
  biopsyRemarks: string = '';



  ngOnInit(): void {
    this.patientId = Number(this.route.snapshot.params['patientId'] || null);
    this.stage = Number(this.route.snapshot.params['stage'] || 0);
    const allowedWithoutSave = [1, 3, 5];
    if (allowedWithoutSave.includes(this.stage)) {
      this.isSaved = true;
    }



    this.assessmentForm = this.fb.group({
      assessmentId: [null],
      pid: [null],
      q1: [0, Validators.required],
      q2: [0, Validators.required],
      q3: [0, Validators.required],
      q4: [0, Validators.required],
      q5: [0, Validators.required],
      q6: [0, Validators.required],
      q7: [0, Validators.required],
      q8: [0, Validators.required],
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
      lax: [false],
      laxGrade: [''],
      laxRemarks: [''],
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

    if (this.manometryReportAttached === 'no') {
      // this.clearManometryAttachmentFields();
    }

    this.loadAssessment(this.patientId);

    if (this.patientId !== null && this.stage !== null) {
      this.getattach(Number(this.patientId), Number(this.stage), 'ph');
      this.getattach(Number(this.patientId), Number(this.stage), 'manometry');
      this.getattach(Number(this.patientId), Number(this.stage), 'biopsy');
    }

  }


  onHillChange() {
    if (this.hill === 'No') {
      this.hillGrade = '';
      this.hillRemarks = '';
    }
  }

  ngOnChanges(changes: SimpleChanges) {
    if (changes['data'] && this.data) {
      // this.patchAssessment(this.data);
      this.patchAssessment(this.data)
    }
  }


  patchAssessment(data: any): void {
    const formatDate = (dateStr: string) => dateStr ? dateStr.split('T')[0] : '';
    if (data.eeHillClassificationGrade !== '') this.eeHill = 'Yes'; else this.eeHill = 'No';
    // Patch reactive form
    this.assessmentForm.patchValue({
      assessmentId: data.assessmentId,
      pid: data.pid,
      q1: data.q1, q2: data.q2, q3: data.q3, q4: data.q4,
      q5: data.q5, q6: data.q6, q7: data.q7, q8: data.q8,
      q9: data.q9, q10: data.q10, q11: data.q11, q12: data.q12,

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

      symptomScore: data.symptomScore,
      lax: data.eeLaxlesClassification ? 'Yes' : 'No',
      laGrade: data.eeAngelesGrade,
      laRemarks: data.eeAgremarks,
      barrettsRemarks: data.eeBarrettRemark,
      eeLaxlesClassification: data.eeLaxlesClassification,
      eeAngelesGrade: data.eeAngelesGrade,
      eeAgremarks: data.eeAgremarks,
      //eeBarrettRemark: data.eeBarrettRemark,

      hill: this.eeHill ? 'Yes' : 'No',
      eeHillClassificationGrade: data.eeHillClassificationGrade,
      eeHillRemarks: data.eeHillRemarks,

      pHimpedanceMonitoring: data.pHimpedanceMonitoring ? 'yes' : 'no',
      phDate: formatDate(data.pHimDate),
      pHimAttached: data.pHimAttached ? 'yes' : 'no',
      pHimAttachement: data.pHimAttachement,
      pHimRemark: data.pHimRemark,

      manometryTest: data.manometryTest ? 'yes' : 'no',
      mtDate: formatDate(data.mtDate),
      mtAttached: data.mtAttached,
      mtAttachement: data.mtAttachement,
      mtRemark: data.mtRemark,

      biopsy: data.biopsy ? 'yes' : 'no',
      biopsyDate: formatDate(data.biopsyDate),
      biopsyAttached: data.biopsyAttached ? 'yes' : 'no',
      biopsyAttachement: data.biopsyAttachement,
      biopsyRemark: data.biopsyRemark,

      createdBy: data.createdBy,
      createdDt: data.createdDt,
      modifiedBy: data.modifiedBy,
      modifiedDt: data.modifiedDt,
      stage: data.stage,

    });

    // Patch component-level variables
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

    this.symptomScore = data.symptomScore;
    this.lax = data.eeLaxlesClassification ? 'Yes' : 'No';
    this.laGrade = data.eeAngelesGrade ?? '';
    this.laRemarks = data.eeAgremarks ?? '';
    this.barrettsRemarks = data.eeBarrettRemark;
    this.hill = data.eeHillClassificationGrade ? 'Yes' : 'No';
    this.hillRemarks = data.eeHillRemarks;
    this.hillGrade = data.eeHillClassificationGrade ?? '';

    // PH
    this.phYes = data.pHimpedanceMonitoring ? 'yes' : 'no';
    this.phDate = formatDate(data.pHimDate);
    this.phReportAttached = data.pHimAttached ? 'yes' : 'no';
    this.phRemarks = data.pHimRemark ?? '';
    this.phFiles = data.pHimAttachement ? [{ filePath: data.pHimAttachement }] : [];

    // Manometry
    this.manometryTest = data.manometryTest ? 'yes' : 'no';
    this.manometryDate = formatDate(data.mtDate);
    this.manometryReportAttached = data.mtAttached ? 'yes' : 'no';
    this.manometryRemarks = data.mtRemark ?? '';
    this.manometryFiles = data.mtAttachement ? [{ filePath: data.mtAttachement }] : [];

    // Biopsy
    this.biopsyTest = data.biopsy ? 'yes' : 'no';
    this.biopsyDate = formatDate(data.biopsyDate);
    this.biopsyReportAttached = data.biopsyAttached ? 'yes' : 'no';
    this.biopsyRemarks = data.biopsyRemark ?? '';
    this.biopsyFiles = data.biopsyAttachement ? [{ filePath: data.biopsyAttachement }] : [];

    this.stage = data.stage;

    // Load attachments from server if needed
    if (this.patientId !== null && this.stage !== null) {
      this.getattach(Number(this.patientId), Number(this.stage), 'ph');
      this.getattach(Number(this.patientId), Number(this.stage), 'manometry');
      this.getattach(Number(this.patientId), Number(this.stage), 'biopsy');
    }
  }



  today: string = new Date().toISOString().split('T')[0];



  @ViewChild('manometryFileInput') manometryFileInput!: ElementRef;

  onManometryTestClick(event: Event, selectedValue: string): void {
    if (selectedValue === 'no' && this.manometryFiles && this.manometryFiles.length > 0) {
      event.preventDefault();
      alert('There are attached files. You cannot select "No" without deleting them.');
      return;
    }

    if (selectedValue === 'no') {
      this.manometryDate = '';
      this.manometryReportAttached = '';
      this.manometryFiles = [];
      this.manometryRemarks = '';
      if (this.manometryFileInput?.nativeElement) {
        this.manometryFileInput.nativeElement.value = '';
      }
    }

    this.manometryTest = selectedValue;
  }

  onManometryReportClick(event: Event, selectedValue: string): void {
    if (selectedValue.toLowerCase() === 'no' && this.manometryFiles && this.manometryFiles.length > 0) {
      event.preventDefault();
      alert('There are attached files. You cannot select "No" without deleting them.');
      return;
    }

    if (selectedValue.toLowerCase() === 'no') {
      this.manometryFiles = [];
      this.manometryRemarks = '';
      if (this.manometryFileInput?.nativeElement) {
        this.manometryFileInput.nativeElement.value = '';
      }
    }

    this.manometryReportAttached = selectedValue;

    if (selectedValue.toLowerCase() === 'yes' && this.manometryTest === 'yes') {
      // Load attachments if needed
      // this.getattach(this.patientId, this.stage, 'manometry');
    }
  }



  getTotalSymptomScore(): number {
    return this.symptomAnswers.reduce((total, score) => total + Number(score), 0);
  }

  getFssgScoreCounts(): number[] {
    const counts = [0, 0, 0, 0, 0];
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
    this.getattach(this.patientId, this.stage, 'manometry');
  }


  @ViewChild('biopsyFileInput') biopsyFileInput!: ElementRef;

  onBiopsyTestClick(event: Event, selectedValue: string): void {
    if (selectedValue === 'no' && this.biopsyFiles && this.biopsyFiles.length > 0) {
      event.preventDefault();
      alert('There are attached files. You cannot select "No" without deleting them.');
      return;
    }

    if (selectedValue === 'no') {
      this.biopsyDate = '';
      this.biopsyReportAttached = '';
      this.biopsyFiles = [];
      this.biopsyRemarks = '';
      if (this.biopsyFileInput?.nativeElement) {
        this.biopsyFileInput.nativeElement.value = '';
      }
    }

    this.biopsyTest = selectedValue;
  }

  onBiopsyReportClick(event: Event, selectedValue: string): void {
    if (selectedValue.toLowerCase() === 'no' && this.biopsyFiles && this.biopsyFiles.length > 0) {
      event.preventDefault();
      alert('There are attached files. You cannot select "No" without deleting them.');
      return;
    }

    if (selectedValue.toLowerCase() === 'no') {
      this.biopsyFiles = [];
      this.biopsyRemarks = '';
      if (this.biopsyFileInput?.nativeElement) {
        this.biopsyFileInput.nativeElement.value = '';
      }
    }

    this.biopsyReportAttached = selectedValue;

    if (selectedValue.toLowerCase() === 'yes' && this.biopsyTest === 'yes') {
      this.getattach(this.patientId, this.stage, 'biopsy');
    }
  }


  uploadAttachment(event: any, section: string, fileList: any[]): void {
    const selectedFiles = event.target.files;
    if (!selectedFiles) return;

    for (let i = 0; i < selectedFiles.length; i++) {
      const file = selectedFiles[i];
      const fileType = file.type;

      if (file.size > 5 * 1024 * 1024) {
        alert(`Can't attach "${file.name}" (exceeds 5 MB size limit).`);
        continue;
      }

      if (!['image/jpeg', 'image/jpg', 'image/png', 'application/pdf'].includes(fileType)) {
        alert('Invalid file type. Only JPG, JPEG, PNG, and PDF are allowed.');
        continue;
      }

      const formData = new FormData();
      formData.append('file', file);

      let params = new HttpParams()
        .set('patientId', this.patientId)
        .set('doctorId', this.patientService.getDoctorId())
        .set('stage', this.stage)
        .set('section', section)
        .set('createdBy', this.patientService.getDoctorId());

      this.http.httpPostFileUPload('/Attachment/Upload', formData, params).subscribe({
        next: (res: any) => {
          console.log(`${section} file uploaded successfully`, res);

          fileList.push({
            name: file.name,
            section,
            attachmentId: res.attachmentId,
            filePath: res.filePath
          });

          if (this.patientId && this.stage) {
            this.getattach(this.patientId, this.stage, section);
          }

          event.target.value = '';
        },
        error: (err) => {
          console.error(`Failed to upload ${section} file`, err);
        }
      });
    }
  }


  private getattach(patientId: number, stage: number, filesection: string): void {
    this.http.httpGet(`/Attachment/GetByPatient/${patientId}/${stage}/${filesection}`).subscribe({
      next: (res) => {
        if (!res) return;

        switch (filesection) {
          case 'ph':
            this.phFiles = res;
            break;

          case 'manometry':
            this.manometryFiles = res;
            break;

          case 'biopsy':
            this.biopsyFiles = res;
            break;

          default:
            console.warn('Unknown filesection:', filesection);
            break;
        }


        this.assessmentForm.patchValue({

          phReportAttached: this.phFiles.length > 0 ? 'yes' : 'no',
          phYes: this.phFiles.length > 0 ? 'yes' : 'no',
          manometryReportAttached: this.manometryFiles.length > 0 ? 'yes' : 'no',
          manometryTest: this.manometryFiles.length > 0 ? 'yes' : 'no',
          biopsyYes: this.biopsyFiles.length > 0 ? 'yes' : 'no',
          biopsyReportAttached: this.biopsyFiles.length > 0 ? 'yes' : 'no'
        });

      },
      error: (err) => {
        this.formValidation.showAlert('Failed to load history data.', 'danger');
        console.error('Error loading  history data:', err);
      }
    });
  }



  loadAssessment(pid: number): void {
    this.assessmentService.getAssessmentById(pid, this.stage).subscribe(response => {
      if (response?.type === 'S' && response.data) {
        this.loadExistingData(response.data);

      } else {
        console.warn('No assessment data found for patient ID:');
      }
    }, error => {
      console.error('Error fetching assessment data:', error);
    });
    this.isDataLoaded = true;

  }
  eeHill: any;

  loadExistingData(data: any): void {
    
    if(data.eeHillClassificationGrade!=='')  this.eeHill = 'Yes'; else  this.eeHill='No';
    
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

      symptomScore: data.symptomScore,
      lax: data.eeLaxlesClassification===true  ? 'Yes' : 'No',
      laGrade: data.eeAngelesGrade,
      laRemarks: data.eeAgremarks,

      eeLaxlesClassification: data.eeLaxlesClassification===true  ? 'Yes' : 'No',
      eeAngelesGrade: data.eeAngelesGrade,
      eeAgremarks: data.eeAgremarks,
      eeBarrettRemark: data.eeBarrettRemark,

      hill: this.eeHill,
      hillGrade: data.eeHillClassificationGrade,
      hillRemarks: data.eeHillRemarks,

      pHimpedanceMonitoring: data.pHimpedanceMonitoring,
      phDate: formatDate(data.pHimDate),
      pHimAttached: data.pHimAttached,
      pHimAttachement: data.pHimAttachement,
      pHimRemark: data.eeHillRemarks,

      manometryTest: data.manometryTest,
      manometryDate: formatDate(data.mtDate),
      mtAttached: data.mtAttached,
      mtAttachement: data.mtAttachement,
      mtRemark: data.mtRemark,

      biopsy: data.biopsy,
      biopsyDate: formatDate(data.biopsyDate),
      biopsyAttached: data.biopsyAttached,
      biopsyAttachement: data.biopsyAttachement,
      biopsyRemark: data.biopsyRemark,

      createdBy: data.createdBy,
      createdDt: data.createdDt,
      modifiedBy: data.modifiedBy,
      modifiedDt: data.modifiedDt,
      stage: data.stage,
    });

    this.symptomScore = data.symptomScore;
    this.lax = data.eeLaxlesClassification ? 'Yes' : 'No';
    this.laGrade = data.eeAngelesGrade ?? '';
    this.laRemarks = data.eeAgremarks ?? '';
    this.barrettsRemarks = data.eeBarrettRemark;
    this.hill = data.eeHillClassificationGrade ? 'Yes' : 'No';
    this.hillRemarks = data.eeHillRemarks;
    this.hillGrade = data.eeHillClassificationGrade ?? '';


    this.phYes = data.pHimpedanceMonitoring ? 'yes' : 'no';
    this.phDate = formatDate(data.pHimDate);
    this.phReportAttached = data.pHimAttached ? 'yes' : 'no';
    this.phRemarks = data.pHimRemark ?? '';

    this.manometryTest = data.manometryTest ? 'yes' : 'no';
    this.manometryDate = formatDate(data.mtDate);
    this.manometryReportAttached = data.mtAttached ? 'yes' : 'no';
    this.manometryRemarks = data.mtRemark ?? '';

    this.biopsyTest = data.biopsy ? 'yes' : 'no';
    this.biopsyDate = formatDate(data.biopsyDate);
    this.biopsyReportAttached = data.biopsyAttached ? 'yes' : 'no';
    this.biopsyRemarks = data.biopsyRemark ?? '';

    this.stage = data.stage;
  }



  ptnstage: number = 0;

  onLaxChange(): void {
    const laxValue = this.lax;
    if (laxValue !== 'Yes') {
      this.laGrade = '';
      this.laRemarks = '';
    }
  }



  submitAssessment(): void {
    if (!this.formValidation.validateForm(this.assessmentForm)) return;
    if (!this.validatefields()) return;

    console.log('submitAssessment called');
    if (this.stage === 1) this.ptnstage = 2;
    else if (this.stage === 3) this.ptnstage = 4;
    else if (this.stage === 0) this.ptnstage = 0;
    else this.ptnstage = this.stage;

    try {
      const param: any = {
        Flag: 'I',
        stage: this.ptnstage,
        AssessmentId: 0,
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

        AcidRefluxSymptom: this.calculateAcidRefluxScore(),
        Dysmotity: this.calculateDysmotilityScore(),
        TotalPoints: this.calculateTotalFssgScore().toString(),

        HeartburnNil: this.symptomAnswers[0] === 0,
        HeartburnMinimal: this.symptomAnswers[0] === 1,
        HeartburnModerate: this.symptomAnswers[0] === 2,
        HeartburnHeartburn: this.symptomAnswers[0] === 3,

        RegurgitationNil: this.symptomAnswers[1] === 0,
        RegurgitationMinimal: this.symptomAnswers[1] === 1,
        RegurgitationModerate: this.symptomAnswers[1] === 2,
        RegurgitationHeartburn: this.symptomAnswers[1] === 3,

        RetrosternalPainNil: this.symptomAnswers[2] === 0,
        RetrosternalPainMinimal: this.symptomAnswers[2] === 1,
        RetrosternalPainModerate: this.symptomAnswers[2] === 2,
        RetrosternalPainHeartburn: this.symptomAnswers[2] === 3,

        AcidTasteMouthNil: this.symptomAnswers[3] === 0,
        AcidTasteMouthMinimal: this.symptomAnswers[3] === 1,
        AcidTasteMouthModerate: this.symptomAnswers[3] === 2,
        AcidTasteMouthHeartburn: this.symptomAnswers[3] === 3,

        EeLaxlesClassification: this.lax?.toLowerCase() === 'yes',
        EeAngelesGrade: this.laGrade ?? '',
        EeAgremarks: this.laRemarks ?? '',
        EeBarrettRemark: this.barrettsRemarks ?? '',
        eeHillClassificationGrade: this.hillGrade ?? '',
        EeHillRemarks: this.hillRemarks ?? '',

        PHimpedanceMonitoring: this.phYes?.toLowerCase() === 'yes',
        pHimDate: this.phYes?.toLowerCase() === 'yes' && this.phDate ? new Date(this.phDate).toISOString() : '1900-01-01T00:00:00Z',
        pHimAttached: this.phReportAttached?.toLowerCase() === 'yes',
        pHimAttachement: this.phReportAttached?.toLowerCase() === 'yes' ? (this.phFiles || []).map(f => f.name).join(', ') : '',
        pHimRemark: this.phReportAttached?.toLowerCase() === 'yes' ? this.phRemarks ?? '' : '',

        ManometryTest: this.manometryTest?.toLowerCase() === 'yes',
        MtDate: this.manometryTest?.toLowerCase() === 'yes' && this.manometryDate ? new Date(this.manometryDate).toISOString() : '1900-01-01T00:00:00Z',
        MtAttached: this.manometryReportAttached?.toLowerCase() === 'yes',
        MtAttachement: this.manometryReportAttached?.toLowerCase() === 'yes' ? (this.manometryFiles || []).map(f => f.name).join(', ') : '',
        MtRemark: this.manometryReportAttached?.toLowerCase() === 'yes' ? this.manometryRemarks ?? '' : '',

        Biopsy: this.biopsyTest?.toLowerCase() === 'yes',
        BiopsyDate: this.biopsyTest?.toLowerCase() === 'yes' && this.biopsyDate ? new Date(this.biopsyDate).toISOString() : '1900-01-01T00:00:00Z',
        BiopsyAttached: this.biopsyReportAttached?.toLowerCase() === 'yes',
        BiopsyAttachement: this.biopsyReportAttached?.toLowerCase() === 'yes' ? (this.biopsyFiles || []).map(f => f.name).join(', ') : '',
        BiopsyRemark: this.biopsyReportAttached?.toLowerCase() === 'yes' ? this.biopsyRemarks ?? '' : '',

        CreatedBy: this.patientService.getDoctorId(),
        totalSymptomScore: this.getTotalSymptomScore(),
        symptomScore: this.symptomScore
      };

      console.log('Sending param to API:', param);

      this.http.httpPost('/Assessment/SaveAssessment', param).subscribe(
        (res: any) => {
          if (res.type === 'E') {
            console.error('Failed to save assessment');
            this.formValidation.showAlert('Failed to save assessment', 'danger');
            return;
          }
          this.isSaved = true;
          alert('Saved Successfully');
          this.formValidation.showAlert('Saved Successfully', 'success');
          console.log('Assessment saved:', res);
        },
        (err: any) => {
          console.error('Failed to save assessment:', err);
          this.formValidation.showAlert('Error in saving assessment', 'danger');
        }
      );

    } catch (e) {
      console.error('Error in submitAssessment:', e);
      this.formValidation.showAlert('Error in saving assessment', 'danger');
    }
  }



  validatefields(): boolean {

    if (this.fssgAnswers[0] === null) {
      alert('Select Do you get heartburn?');
      return false;
    }
    if (this.fssgAnswers[1] === null) {
      alert('Select Does your stomach get bloated?');
      return false;
    }
    if (this.fssgAnswers[2] === null) {
      alert('Select Does your stomach ever feel heavy after meals?');
      return false;
    }
    if (this.fssgAnswers[3] === null) {
      alert('Select Do you rub your chest with your hand?');
      return false;
    }
    if (this.fssgAnswers[4] === null) {
      alert('Select Do you feel sick after meals?');
      return false;
    }
    if (this.fssgAnswers[5] === null) {
      alert('Select Do you get heartburn after meals?');
      return false;
    }
    if (this.fssgAnswers[6] === null) {
      alert('Select Do you have an unusual sensation in your throat?');
      return false;
    }
    if (this.fssgAnswers[7] === null) {
      alert('Select Do you feel full while eating meals?');
      return false;
    }
    if (this.fssgAnswers[8] === null) {
      alert('Select Do you feel things stuck when you swallow?');
      return false;
    }
    if (this.fssgAnswers[9] === null) {
      alert('Select Do you get bitter liquid in your throat?');
      return false;
    }
    if (this.fssgAnswers[10] === null) {
      alert('Select Do you burp a lot?');
      return false;
    }
    if (this.fssgAnswers[11] === null) {
      alert('Select Do you get heartburn if you bend over?');
      return false;
    }


    for (let i = 0; i < 4; i++) {
      if (this.symptomAnswers[i] === null) {
        const symptomName = ['Heartburn', 'Regurgitation', 'Retrosternal pain', 'Acid taste in mouth'][i];
        alert(`Select Symptom Score for ${symptomName}`);
        return false;
      }
    }

    if (this.symptomScore === null || this.symptomScore === undefined) {
      alert('Select Overall Symptom Score');
      return false;
    }


    if (!this.lax) {
      alert('Select Lax Classification (Yes/No)');
      return false;
    } else if (this.lax.toLowerCase() === 'yes') {
      if (!this.laGrade) {
        alert('Select Los Angeles Grade');
        return false;
      }
      if (!this.laRemarks || this.laRemarks.trim() === '') {
        alert('Enter Los Angeles Remarks');
        return false;
      }
    }


    if (!this.barrettsRemarks || this.barrettsRemarks.trim() === '') {
      alert('Enter Barrett’s Remarks');
      return false;
    }


    if (!this.hill) {
      alert('Select Hill Classification (Yes/No)');
      return false;
    } else if (this.hill.toLowerCase() === 'yes') {
      if (!this.hillGrade) {
        alert('Select Hill Classification Grade');
        return false;
      }
      if (!this.hillRemarks || this.hillRemarks.trim() === '') {
        alert('Enter Hill Remarks');
        return false;
      }
    }


    if (!this.phYes) {
      alert('Select pH Impedance Monitoring was done (Yes/No)');
      return false;
    } else if (this.phYes.toLowerCase() === 'yes') {
      if (!this.phDate) {
        alert('Enter pH Monitoring Date');
        return false;
      }

      if (!this.phReportAttached) {
        alert('Select pH Monitoring Report is attached (Yes/No)');
        return false;
      } else if (this.phReportAttached.toLowerCase() === 'yes') {
        if (!this.phFiles || this.phFiles.length === 0) {
          alert('Attach pH Monitoring Report');
          return false;
        }
        if (!this.phRemarks || this.phRemarks.trim() === '') {
          alert('Enter pH Monitoring Remarks');
          return false;
        }
      }
    }


    if (!this.manometryTest) {
      alert('Select Manometry Test was done (Yes/No)');
      return false;
    } else if (this.manometryTest.toLowerCase() === 'yes') {
      if (!this.manometryDate) {
        alert('Enter Manometry Date');
        return false;
      }

      if (!this.manometryReportAttached) {
        alert('Select Manometry Report is attached (Yes/No)');
        return false;
      } else if (this.manometryReportAttached.toLowerCase() === 'yes') {
        if (!this.manometryFiles || this.manometryFiles.length === 0) {
          alert('Attach Manometry Report');
          return false;
        }
        if (!this.manometryRemarks || this.manometryRemarks.trim() === '') {
          alert('Enter Manometry Remarks');
          return false;
        }
      }
    }


    if (!this.biopsyTest) {
      alert('Select Biopsy was done (Yes/No)');
      return false;
    } else if (this.biopsyTest.toLowerCase() === 'yes') {
      if (!this.biopsyDate) {
        alert('Enter Biopsy Date');
        return false;
      }

      if (!this.biopsyReportAttached) {
        alert('Select Biopsy Report is attached (Yes/No)');
        return false;
      } else if (this.biopsyReportAttached.toLowerCase() === 'yes') {
        if (!this.biopsyFiles || this.biopsyFiles.length === 0) {
          alert('Attach Biopsy Report');
          return false;
        }
        if (!this.biopsyRemarks || this.biopsyRemarks.trim() === '') {
          alert('Enter Biopsy Remarks');
          return false;
        }
      }
    }


    return true;
  }


  calculateTotalFssgScore(): string {
    return this.fssgAnswers.reduce((sum, val) => sum + val, 0).toString();
  }

  calculateAcidRefluxScore(): string {
    const acidIndexes = [0, 3, 5, 6, 8, 9, 11];
    return acidIndexes.reduce((sum, i) => sum + this.fssgAnswers[i], 0).toString();
  }

  calculateDysmotilityScore(): string {
    const dysmotilityIndexes = [1, 2, 4, 7, 10];
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
    //this.getattach(this.patientId,this.stage,'biopsy');
  }

  onSave() {
    this.router.navigate(['/diagnosis']);
  }



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
    //this.getattach(this.patientId,this.stage,'ph');
  }



  onPhTestClick(event: Event, selectedValue: string): void {
    if (selectedValue === 'no' && this.phFiles && this.phFiles.length > 0) {
      event.preventDefault();
      alert('There are attached files. You cannot select "No" without deleting them.');
      return;
    }

    if (selectedValue === 'no') {
      this.phDate = '';
      this.phReportAttached = '';
      this.phFiles = [];
      this.phRemarks = '';
      if (this.phFileInput && this.phFileInput.nativeElement) {
        this.phFileInput.nativeElement.value = '';
      }
    }

    this.phYes = selectedValue;
  }

  onPhReportClick(event: Event, selectedValue: string): void {
    if (selectedValue.toLowerCase() === 'no' && this.phFiles && this.phFiles.length > 0) {
      event.preventDefault();
      alert('There are attached files. You cannot select "No" without deleting them.');
      return;
    }


    if (selectedValue.toLowerCase() === 'no') {
      this.phFiles = [];
      this.phRemarks = '';
      if (this.phFileInput && this.phFileInput.nativeElement) {
        this.phFileInput.nativeElement.value = '';
      }
    }

    this.phReportAttached = selectedValue;
    if (selectedValue.toLowerCase() === 'yes') {
      this.getattach(this.patientId, this.stage, 'ph');
    }
  }



  onNext() {
    const currentUrl = this.router.url;
    const patientId = this.patientId;
    if (this.stage > 1) {
      this.router.navigate([`/managament/${this.patientId}/${this.stage}`], {
        state: {
          tabId: this.tabId,
          patientId: this.patientId,
          isViewMode: this.isViewMode,
          fromNavigation: true
        }
      });
    } else {
      this.router.navigate([`/diagnosis/${this.patientId}/${this.stage}`], {
        state: {
          tabId: this.tabId,
          patientId: this.patientId,
          isViewMode: this.isViewMode,
          fromNavigation: true
        }
      });
    }
  }


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



  getStatusClass(step: number): string {
    if (this.stage === 0 && step === 1) return 'baseline-blue';

    if (this.stage >= 1 && step === 1) return 'baseline-green';
    if (this.stage >= 1 && this.stage < 3 && step === 2) return 'baseline-blue';

    if (this.stage >= 3 && step === 2) return 'baseline-green';
    if (this.stage >= 3 && this.stage < 5 && step === 3) return 'baseline-blue';

    if (this.stage === 5 && step === 3) return 'baseline-green';

    return 'inactive-tab';
  }




  viewAttachment(file: any) {
    if (!file) {
      alert('No file selected.');
      return;
    }

    if (file.attachmentId) {
      const url = `${API_URLS.BASE_URL}/Attachment/View/${file.attachmentId}`;
      this.http.httpGetFile(url).subscribe({
        next: (blob: Blob) => {
          const fileURL = window.URL.createObjectURL(blob);
          window.open(fileURL, '_blank');
        },
        error: (err) => {
          console.error('Failed to open attachment', err);
          alert('Failed to open attachment.');
        }
      });
      return;
    }

    // Case 2: Local file object (not yet uploaded)
    if (file instanceof File || file.fileObject) {
      const fileObj = file instanceof File ? file : file.fileObject;
      const fileURL = window.URL.createObjectURL(fileObj);
      window.open(fileURL, '_blank');
      return;
    }

    // Case 3: Already has a URL/path
    if (file.fileUrl) {
      window.open(file.fileUrl, '_blank');
      return;
    }

    if (file.filePath) {
      window.open(file.filePath, '_blank');
      return;
    }

    alert('No preview available');
  }

  deleteAttachment(file: any, index: number, section: string) {
    if (!confirm('Are you sure you want to delete this file?')) return;

    if (file.attachmentId) {
      this.http.httpDelete(`/Attachment/Delete/${file.attachmentId}`).subscribe({
        next: () => {
          this.formValidation.showAlert('Attachment deleted successfully', 'success');
          this.removeFileFromList(index, section);
        },
        error: (err) => {
          console.error('Delete failed:', err);
          this.formValidation.showAlert('Failed to delete attachment', 'danger');
        }
      });
    } else {
      this.removeFileFromList(index, section);
    }
  }

  removeFileFromList(index: number, section: string) {
    if (section === 'ph') {
      this.phFiles.splice(index, 1);
    } else if (section === 'manometry') {
      this.manometryFiles.splice(index, 1);
    } else if (section === 'biopsy') {
      this.biopsyFiles.splice(index, 1);
    }
  }


}





