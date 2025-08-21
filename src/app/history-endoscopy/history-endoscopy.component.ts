import { Component, ElementRef, Input, ViewChild } from '@angular/core';
import { FormBuilder, Validators } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { FormvalidationService } from '../formvalidation.service';
import { HttpserviceService } from '../httpservice.service';
import { API_URLS } from '../shared/API-URLs';
import { PatientService } from '../Services/patient.service';
import { HistoryEndoscopyService } from '../Services/history-endoscopy.service';


@Component({
  selector: 'app-history-endoscopy',
  templateUrl: './history-endoscopy.component.html',
  styleUrl: './history-endoscopy.component.css'
})
export class HistoryEndoscopyComponent {

  tabId = 1;
  @Input() stage: number = 0;
  endoscopyGSForm: any
  @Input() patientId: number | null = null;
  doctorId: number | null = null;
  isViewMode = false;
  isFollowUp: boolean = false;
  bariatricChoice: string = '';
  bariatricRemarks: string = '';
  fundoplicationChoice: string = '';
  fundoplicationRemarks: string = '';
  otherSpecify: string = '';
  otherChoice: string = '';
  otherRemarks: string = '';
  gastrojejunostomyChoice: string = '';
  gastrojejunostomyRemarks: string = '';
  poemChoice: string = '';
  poemRemarks: string = '';
  selectedFiles: File[] = [];
  reportAttached: string = '';
  remarks: string = '';
  files: File[] = [];
  selectedFile: File | null = null;
  acceptedTypes = '.jpg,.jpeg,.png,.pdf';
  loggedInPatientId: any;
  isSaved: boolean = false;
  isDataLoaded: boolean = false;
  fileUploadDisabled: boolean = false;
  @Input() isPrintMode = false;
  userData:any




  @ViewChild('attachmentSection') attachmentSection!: ElementRef;

  scrollToAttachment() {
    if (this.reportAttached === 'yes') {
      setTimeout(() => {
        this.attachmentSection?.nativeElement?.scrollIntoView({
          behavior: 'smooth',
          block: 'start'
        });
      }, 100);
    }
  }

  onFileChange(event: any): void {
    const selectedFiles = event.target.files;
    if (selectedFiles) {
      for (let i = 0; i < selectedFiles.length; i++) {
        const file = selectedFiles[i];
        const fileType = file.type;
        if (['image/jpeg', 'image/jpg', 'image/png', 'application/pdf'].includes(fileType)) {
          this.files.push(file);
        } else {
          alert('Invalid file type. Only JPG, JPEG, PNG, and PDF are allowed.');
        }
      }
    }
  }

  addFile(fileInput: HTMLInputElement) {
    fileInput.value = '';
  }

  constructor(private router: Router, private route: ActivatedRoute,
    private formValidation: FormvalidationService, private historyEndoscopyService: HistoryEndoscopyService, private http: HttpserviceService, private fb: FormBuilder, private patientService: PatientService) {

    this.endoscopyGSForm = this.fb.group({
      patientID: [''],
      gerdHistory: [''],
      usageOfPPI: [''],
      historyofEndoscopy: [''],
      endoscopyDate: [''],
      endoscopyAttached: [''],
      endoscopyAttement: [''],
      endoscopyRemark: [''],
      historyofGS: [''],
      gs_BariatricSurgery: [''],
      gs_BSRemark: [''],
      gs_FundoplicationSurgery: [''],
      gs_FSRemark: [''],
      gs_GastricPOEMSurgery: [''],
      gs_GPSRemark: [''],
      gs_Gastrojejunostomy: [''],
      gs_GJRemark: [''],
      gs_OtherText: [''],
      gs_OtherYesNo: [''],
      gs_OtherRemark: [''],
      createdBy: ['']
    });

    this.endoscopyGSForm.patchValue({
      patientID: Number(this.loggedInPatientId)
    });


  }

  ngOnInit(): void {
   
    this.stage = Number(this.route.snapshot.params['stage']);
   const allowedWithoutSave = [1, 3, 5];
  if (allowedWithoutSave.includes(this.stage)) {
    this.isSaved = true;
  }
    const navigationState = history.state;
    this.tabId = navigationState?.tabId ?? 1;
    this.stage = navigationState?.stage ?? 0;
    this.isViewMode = navigationState?.isViewMode ?? false;

    this.route.queryParams.subscribe(params => {
      const paramPatientId = params['patientId'] ? +params['patientId'] : null;
      //const paramDoctorId = params['doctorId'] ? +params['doctorId'] : null;

      this.patientId = paramPatientId ?? this.patientService.getPatientId();
     // this.doctorId = paramDoctorId ?? this.patientService.getDoctorId();
     let user: any = localStorage.getItem('doctor')
     this.userData = JSON.parse(user); 
     this.doctorId=this.userData?.doctorId
     

      const currentUrl = this.router.url;
      const cachedData = this.patientService.getfamalyhistoryData();
      if (cachedData) {
        this.endoscopyGSForm.patchValue(cachedData);
        this.isDataLoaded = true;
      } else if (this.patientId != null) {
        this.fetchhistoryendoscopeData(this.patientId);
      } else {
        console.warn('⚠️ Patient ID is missing — cannot load history endoscopy data.');
      }
    });
      this.endoscopyGSForm.get('historyofEndoscopy')?.valueChanges.subscribe((value: string) => {
      const dateCtrl = this.endoscopyGSForm.get('endoscopyDate');
      const attachedCtrl = this.endoscopyGSForm.get('endoscopyAttached');
      const remarkCtrl = this.endoscopyGSForm.get('endoscopyRemark');

      const shouldEnable = value === 'yes';

      if (shouldEnable) {
        dateCtrl?.enable();
        attachedCtrl?.enable();
        remarkCtrl?.enable();

        dateCtrl?.setValidators([Validators.required]);
        attachedCtrl?.setValidators([Validators.required]);
        remarkCtrl?.setValidators([Validators.required]);
      } else {
        dateCtrl?.disable();
        attachedCtrl?.disable();
        remarkCtrl?.disable();

        dateCtrl?.clearValidators();
        attachedCtrl?.clearValidators();
        remarkCtrl?.clearValidators();

        dateCtrl?.setValue('');
        attachedCtrl?.setValue('');
        remarkCtrl?.setValue('');
      }

      dateCtrl?.updateValueAndValidity();
      attachedCtrl?.updateValueAndValidity();
      remarkCtrl?.updateValueAndValidity();

      this.fileUploadDisabled = !shouldEnable;
    });

    // Setup surgery-specific dynamic remarks
    const remarkPairs = [
      ['gs_BariatricSurgery', 'gs_BSRemark'],
      ['gs_FundoplicationSurgery', 'gs_FSRemark'],
      ['gs_GastricPOEMSurgery', 'gs_GPSRemark'],
      ['gs_Gastrojejunostomy', 'gs_GJRemark'],
      ['gs_OtherYesNo', 'gs_OtherRemark']
    ];
    remarkPairs.forEach(([choiceControl, remarkControl]) => this.handleSurgeryRemarks(choiceControl, remarkControl));
  }

  today: string = new Date().toISOString().split('T')[0];
  handleSurgeryRemarks(choiceControl: string, remarkControl: string): void {
    const choice = this.endoscopyGSForm.get(choiceControl);
    const remark = this.endoscopyGSForm.get(remarkControl);

    if (!choice || !remark) return;

    // Handle initial state
    if (choice.value !== 'yes') {
      remark.disable();
    } else {
      remark.enable();
    }

    // Watch for changes
    choice.valueChanges.subscribe((value: string) => {
      if (value === 'yes') {
        remark.enable();
      } else {
        remark.disable();
        remark.setValue('');
      }
    });
  }

  fetchhistoryendoscopeData(patientId: number): void {
    this.historyEndoscopyService.gethistoryendoscopeById(patientId).subscribe({
      next: (res: any) => {
        console.log('History Endoscopy response:', res);

        const data = Array.isArray(res.data) ? res.data[0] : res.data;
        this.stage = data.stage;

        if (res.type === 'S' && data) {
          this.endoscopyGSForm.patchValue({
            patientID: data.patientId ?? '',
            usageOfPPI: data.usageOfPpi ?? '',
            historyofEndoscopy: data.historyofEndoscopy ?? '',
            endoscopyDate: data.endoscopyDate ? data.endoscopyDate.split('T')[0] : '',
            endoscopyAttached: data.endoscopyAttached ? 'yes' : 'no',
            endoscopyAttement: data.endoscopyAttement ?? '',
            endoscopyRemark: data.endoscopyRemark ?? '',
            historyofGS: data.historyofGs ? 'yes' : 'no',
            gs_BariatricSurgery: data.gsBariatricSurgery ? 'yes' : 'no',
            gs_BSRemark: data.gsBsremark ?? '',
            gs_FundoplicationSurgery: data.gsFundoplicationSurgery ? 'yes' : 'no',
            gs_FSRemark: data.gsFsremark ?? '',
            gs_GastricPOEMSurgery: data.gsGastricPoemsurgery ? 'yes' : 'no',
            gs_GPSRemark: data.gsGpsremark ?? '',
            gs_Gastrojejunostomy: data.gsGastrojejunostomy ? 'yes' : 'no',
            gs_GJRemark: data.gsGjremark ?? '',
            // gs_OtherText: data.gsOther,
           gs_OtherText: data.gsOtherText ?? '',

            gs_OtherYesNo: data.gsOther ? 'yes' : 'no',
            gs_OtherRemark: data.gsOtherRemark ?? '',
            createdBy: data.createdBy
          });
          this.endoscopyGSForm.updateValueAndValidity();
          this.isDataLoaded = true;
        }
      },
      error: err => {
        console.error('❌ Error fetching endoscopy data:', err);
        this.isDataLoaded = true;
      }
    });
  }
  onBariatricChange() {
    if (this.bariatricChoice === 'no') {
      this.bariatricRemarks = '';
    }
  }
  onFundoplicationChange() {
    if (this.fundoplicationChoice === 'no') {
      this.fundoplicationRemarks = '';
    }
  }
  onPoemChange() {
    if (this.poemChoice === 'no') {
      this.poemRemarks = '';
    }
  }
  onGastrojejunostomyChange() {
    if (this.gastrojejunostomyChoice === 'no') {
      this.gastrojejunostomyRemarks = '';
    }
  }


  onOtherChange() {
    if (this.otherChoice === 'no') {
      this.otherRemarks = '';
    }
  }

  private isValidFileType(file: File): boolean {
    const allowedTypes = ['image/jpeg', 'image/png', 'application/pdf'];
    return allowedTypes.includes(file.type);
  }


  onFileSelected(event: Event): void {
    const input = event.target as HTMLInputElement;
    if (input.files) {
      this.selectedFiles = Array.from(input.files);
    }
  }
  onSave(): void {
    if (!this.formValidation.validateForm(this.endoscopyGSForm)) {
      this.endoscopyGSForm.markAllAsTouched();
      return;
    }

    const endoscopyAttachedValue = this.endoscopyGSForm.controls['endoscopyAttached'].value;
    const endoscopyAttachedBool = (endoscopyAttachedValue === 'yes');
    const param = {

      stage: this.stage,
      flag: "I",
      id: 0,
      doctorID: this.doctorId,
      patientID: this.patientId,
      gerdHistory: this.endoscopyGSForm.get('gerdHistory')?.value || '',
      usageOfPPI: this.endoscopyGSForm.controls['usageOfPPI'].value,
      historyofEndoscopy: this.endoscopyGSForm.controls['historyofEndoscopy'].value,
      endoscopyDate: this.endoscopyGSForm.controls['endoscopyDate'].value || "2025-08-21T10:05:06.354Z",
      endoscopyAttached: endoscopyAttachedBool,
      endoscopyAttement: this.files.map(file => file.name).join(', '),
      endoscopyRemark: this.endoscopyGSForm.controls['endoscopyRemark'].value,
      historyofGS: this.endoscopyGSForm.controls['historyofGS'].value === 'yes',
      gs_BariatricSurgery: this.endoscopyGSForm.controls['gs_BariatricSurgery'].value === 'yes',
      gs_BSRemark: this.endoscopyGSForm.controls['gs_BSRemark'].value,
      gs_FundoplicationSurgery: this.endoscopyGSForm.controls['gs_FundoplicationSurgery'].value === 'yes',
      gs_FSRemark: this.endoscopyGSForm.controls['gs_FSRemark'].value,
      gs_GastricPOEMSurgery: this.endoscopyGSForm.controls['gs_GastricPOEMSurgery'].value === 'yes',
      gs_GPSRemark: this.endoscopyGSForm.controls['gs_GPSRemark'].value,
      gs_Gastrojejunostomy: this.endoscopyGSForm.controls['gs_Gastrojejunostomy'].value === 'yes',
      gs_GJRemark: this.endoscopyGSForm.controls['gs_GJRemark'].value,
      gs_OtherText: this.endoscopyGSForm.controls['gs_OtherText'].value,
      gs_OtherYesNo: this.endoscopyGSForm.controls['gs_OtherYesNo'].value === 'yes',
      gs_OtherRemark: this.endoscopyGSForm.controls['gs_OtherRemark']?.value || '',
      createdBy: this.doctorId
    };

    this.http.httpPost(API_URLS.GERD_HISTORY_ADD, param).subscribe((res: any) => {
      if (res.type === 'S') {
        this.isSaved = true;
        this.http.httpGet('/PatientReg/GetPatient').subscribe((getRes: any) => {
          if (getRes.type === 'S' && getRes.data?.length > 0) {

          

            this.formValidation.showAlert('Chief complaint saved successfully', 'success');
            this.router.navigate([], {
              queryParams: {
                patientId: this.patientId,
                stage: this.stage
              }
            });
          } else {
            this.formValidation.showAlert('Unable to fetch Patient ID after save', 'danger');
          }
        });

        alert('Saved Successfully');
        this.files = [];
      } else {
        this.formValidation.showAlert('Error!!', 'danger');
      }
    });
  }
  onNext(){
   this.router.navigate([`/medical-examination/${this.patientId}/${this.stage}`], {
  state: {
    tabId: this.tabId,
    patientId: this.patientId,
    isViewMode: this.isViewMode
  }
});

  }

  goNext() {
    this.router.navigate([`/medical-examination/${this.patientId}/${this.stage}`], {
      state: {
        tabId: this.tabId,
        patientId: this.patientId,
        isViewMode: this.isViewMode
      }
    });
  }

  goback() {
    this.router.navigate([`/family-history/${this.patientId}/${this.stage}`], {
      state: {
        tabId: this.tabId,
        patientId: this.patientId,
        isViewMode: this.isViewMode
      }
    });
  }

  back() {
    this.router.navigate([`/family-history/${this.patientId}/${this.stage}`], {
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