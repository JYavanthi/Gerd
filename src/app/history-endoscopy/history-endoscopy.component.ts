import { Component, ElementRef, Input, ViewChild } from '@angular/core';
import { FormBuilder, Validators } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { FormvalidationService } from '../formvalidation.service';
import { HttpserviceService } from '../httpservice.service';
import { API_URLS } from '../shared/API-URLs';
import { PatientService } from '../Services/patient.service';
import { HistoryEndoscopyService } from '../Services/history-endoscopy.service';
import { HttpClient, HttpParams } from '@angular/common/http';
import { truncate } from 'fs';


@Component({
  selector: 'app-history-endoscopy',
  templateUrl: './history-endoscopy.component.html',
  styleUrl: './history-endoscopy.component.css'
})
export class HistoryEndoscopyComponent {

  tabId = 1;
  @Input() stage: number = 0;
  endoscopyGSForm: any
  @Input() patientId: any;
  doctorId: any;
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
  userData: any




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

  
  constructor(private router: Router, private route: ActivatedRoute, private httpClient: HttpClient,
    private formValidation: FormvalidationService, private historyEndoscopyService: HistoryEndoscopyService, private http: HttpserviceService, private fb: FormBuilder, private patientService: PatientService) {

    this.endoscopyGSForm = this.fb.group({
      patientID: [''],
      gerdHistory: [''],
      usageOfPPI: ['', Validators.required],
      historyofEndoscopy: ['', Validators.required],
      endoscopyDate: [''],
      endoscopyAttached: [''],
      endoscopyAttement: [''],
      endoscopyRemark: [''],
      historyofGS: ['', Validators.required],
      gs_BariatricSurgery: ['', Validators.required],
      gs_BSRemark: ['', Validators.required],
      gs_FundoplicationSurgery: ['', Validators.required],
      gs_FSRemark: ['', Validators.required],
      gs_GastricPOEMSurgery: ['', Validators.required],
      gs_GPSRemark: ['', Validators.required],
      gs_Gastrojejunostomy: ['', Validators.required],
      gs_GJRemark: ['', Validators.required],
      gs_OtherText: ['', Validators.required],
      gs_OtherYesNo: ['', Validators.required],
      gs_OtherRemark: ['', Validators.required],
      createdBy: []
    });

    // this.endoscopyGSForm.patchValue({
    //   patientID: Number(this.loggedInPatientId)
    // });


  }

  ngOnInit(): void {
    this.patientId = Number(this.route.snapshot.params['patientId']);
    this.stage = Number(this.route.snapshot.params['stage']);
    const allowedWithoutSave = [1, 3, 5];
    if (allowedWithoutSave.includes(this.stage)) {
      this.isSaved = true;
    }
    //const navigationState = history.state;
   // this.tabId = navigationState?.tabId ?? 1;
   // this.stage = navigationState?.stage ?? 0;
    this.isViewMode = this.isViewMode ?? false;

   
    this.doctorId = this.patientService.getDoctorId();
    this.fetchhistoryendoscopeData(this.patientId);
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
      ['gs_OtherYesNo', 'gs_OtherRemark', 'gs_OtherText']
    ];
    remarkPairs.forEach(([choiceControl, ...remarks]) =>
      this.handleSurgeryRemarks(choiceControl, ...remarks)
    );

    //get attached files for patient
    if(this.patientId!==null && this.stage !== null){
      this.getattach(Number(this.patientId),Number(this.stage),'he');
      
    }
    

  }

  today: string = new Date().toISOString().split('T')[0];
  handleSurgeryRemarks(choiceControl: string, ...dependentControls: string[]): void {
    const choice = this.endoscopyGSForm.get(choiceControl);
    if (!choice) return;

    // Initial state
    dependentControls.forEach(ctrlName => {
      const ctrl = this.endoscopyGSForm.get(ctrlName);
      if (!ctrl) return;
      if (choice.value !== 'yes') {
        ctrl.disable();
      } else {
        ctrl.enable();
      }
    });

    // Watch for changes
    choice.valueChanges.subscribe((value: string) => {
      dependentControls.forEach(ctrlName => {
        const ctrl = this.endoscopyGSForm.get(ctrlName);
        if (!ctrl) return;

        if (value === 'yes') {
          ctrl.enable();
        } else {
          ctrl.disable();
          ctrl.setValue('');
        }
      });
    });
  }

uploadAttachment(event: any, section: string, fileList: any[]): void {
  const selectedFiles = event.target.files;
  if (selectedFiles) {
    for (let i = 0; i < selectedFiles.length; i++) {
      const file = selectedFiles[i];
      const fileType = file.type;

      // validate
      if (['image/jpeg', 'image/jpg', 'image/png', 'application/pdf'].includes(fileType)) {
        const formData = new FormData();
        fileList.push(file);
        formData.append('file', file);

        let params = new HttpParams()
          .set('patientId', this.patientId)
          .set('doctorId', this.patientService.getDoctorId(),)
          .set('stage', this.stage)
          .set('section', section)   // << identify which test
          .set('createdBy', this.patientService.getDoctorId(),);

        this.http.httpPostFileUPload('/Attachment/Upload', formData, params)
          .subscribe((res: any) => {
            console.log(`${section} file uploaded successfully`, res);
          });
          this.getattach(Number(this.patientId),Number(this.stage),'he')
      } else {
        alert('Invalid file type. Only JPG, JPEG, PNG, and PDF are allowed.');
      }
    }
  }
}


attachmentList : any []=[];
  private getattach(patientId: number, stage: number, filesection: string): void {
    this.http.httpGet(`/Attachment/GetByPatient/${patientId}/${stage}/${filesection}`).subscribe({
      next: (res) => {
        if (!res ) return;

        this.attachmentList = res;
        console.log("this.attachmentList", this.attachmentList);

        this.endoscopyGSForm.patchValue({
        endoscopyAttached: this.attachmentList.length > 0 ? 'yes' : 'no'
        });
        
      },
      error: (err) => {
        this.formValidation.showAlert('Failed to load history data.', 'danger');
        console.error('Error loading  history data:', err);
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
          endoscopyAttached: data.endoscopyAttement===true? 'yes' : 'no',
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
          gs_OtherText: data.gsOtherText ?? '',
          gs_OtherYesNo: data.gsOther ? 'yes' : 'no',
          gs_OtherRemark: data.gsOtherRemark ?? '',
          createdBy: data.createdBy
        });

        this.getattach(this.patientId,this.stage,'he')
               
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
      this.otherSpecify ='';
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
      endoscopyDate: this.endoscopyGSForm.controls['endoscopyDate'].value || null,
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
      gS_Other: this.endoscopyGSForm.controls['gs_OtherYesNo'].value === 'yes',
      gs_OtherRemark: this.endoscopyGSForm.controls['gs_OtherRemark']?.value || '',
      createdBy: this.doctorId
    };

    this.http.httpPost(API_URLS.GERD_HISTORY_ADD, param).subscribe((res: any) => {
      if (res.type === 'S') {
        this.isSaved = true;
        
        alert('Saved Successfully');
        this.files = [];
      } else {
        this.formValidation.showAlert('Error!!', 'danger');
      }
    });
  }
    onNext() {
    this.router.navigate([`/current-medicaton/${this.patientId}/${this.stage}`], {
      state: {
        tabId: this.tabId,
        patientId: this.patientId,
        stage:this.stage,
        isViewMode: this.isViewMode
      }
    });

  }

  
  goback() {
    this.router.navigate([`/family-history/${this.patientId}/${this.stage}`], {
      state: {
        tabId: this.tabId,
        patientId: this.patientId,
        stage: this.stage,
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
        this.getattach(this.patientId, this.stage, section); // 🔄 refresh list
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
  if (section === 'he') {
    this.attachmentList.splice(index, 1);
  }
}
  
  
}