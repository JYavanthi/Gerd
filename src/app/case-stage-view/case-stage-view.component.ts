




import { ChangeDetectorRef, Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { ChiefComplaintService } from '../Services/chief-complaint.service';
import { ComorbiditiesService } from '../Services/comorbidities.service';
import { HistoryService } from '../Services/history.servie';
import { PatientService } from '../Services/patient.service';
import { PatientHistoryService } from '../Services/patient-history.service';
import { HttpserviceService } from '../httpservice.service';
import { sleepService } from '../Services/Sleep.service';
import { gadgetService } from '../Services/gadget.service';
import { AssessmentService } from '../Services/Assessment.service';
import { ManagementService } from '../Services/management.service';
import { PersonalHistoryService } from '../Services/personal-history.service';
import html2canvas from 'html2canvas';
import jsPDF from 'jspdf';
import { CurrentMedicationsService } from '../Services/current-medications.service';


@Component({
  selector: 'app-case-stage-view',
  templateUrl: './case-stage-view.component.html',
  styleUrls: ['./case-stage-view.component.scss']
})
export class CaseStageViewComponent implements OnInit {
  patientId: number = 0;
  Object = Object;
  baselineData: any = null;
  followUp1Data: any = null;
  followUp2Data: any = null;

  baselineComorbidities: any = null;
  followUp1Comorbidities: any = null;
  followUp2Comorbidities: any = null;


  baselineHistory: any = null;
  followUp1History: any = null;
  followUp2History: any = null;

  baselinePersonalHistory: any = null;
  baselineSleep: any = null;
  baselinegadge: any = null;
  baselinepersonalHistory:any=null

  baselineAssessment: any = null
  followUp1Assessment: any = null;
  followUp2Assessment: any = null;

  baselinemanagement: any = null;
  followUp1management: any = null;
  followUp2management: any = null;

  baselineCurrentMedication:any =null
  constructor(
    private route: ActivatedRoute,
    private chiefComplaintService: ChiefComplaintService,
    private cdr: ChangeDetectorRef,
    private comorbiditiesService: ComorbiditiesService,
    private patientHistoryService: PatientHistoryService,
    private http: HttpserviceService,
    private sleepService: sleepService,
    private gadgetService: gadgetService,
    private assessmentService: AssessmentService,
    private managementService: ManagementService,
    private personalHistoryService: PersonalHistoryService,
    private currentMedicationsService: CurrentMedicationsService
    

  ) { }

  ngOnInit(): void {

    
    // Get patientId from route
    this.route.params.subscribe(params => {
      this.patientId = +params['patientId'] || 0;
      if (!this.patientId) {
        console.warn('⚠️ No valid patient ID found.');
        return;
      }

      // Load all 3 stages
      this.loadStageData(1); // baseline
      this.loadStageData(3); // follow-up 1
      this.loadStageData(5); // follow-up 2
    });
  }

  loadStageData(stage: number): void {
    if (!this.patientId) return;

    this.chiefComplaintService.getChiefComplaintByPatientId(this.patientId, stage).subscribe({
      next: (res: any) => {
        console.log(`API Response for stage ${stage}:`, res);

        // Use type === 'S' instead of success
        if (res.type === 'S' && res.data) {
          switch (stage) {
            case 1:
              this.baselineData = res.data;
              break;
            case 3:
              this.followUp1Data = res.data;
              break;
            case 5:
              this.followUp2Data = res.data;
              break;
          }
        } else {
          console.warn(`⚠️ No data found for stage ${stage}`);
        }
      },
      error: err => {
        console.error(`❌ Error fetching stage ${stage} data:`, err);
      }
    });


    this.comorbiditiesService.getComorbiditiesById(this.patientId, stage).subscribe({
      next: (res: any) => {
        if (res.type === 'S' && res.data) {
          switch (stage) {
            case 1:
              this.baselineComorbidities = res.data;
              break;
            case 3:
              this.followUp1Comorbidities = res.data;
              break;
            case 5:
              this.followUp2Comorbidities = res.data;
              break;
          }
        } else {
          console.warn(`⚠️ No comorbidities found for stage ${stage}`);
        }
      },
      error: err => console.error(`❌ Error fetching comorbidities for stage ${stage}:`, err)
    });

    this.patientHistoryService.getHistoryByid(this.patientId, stage).subscribe({
      next: (res: any) => {
        if (res.type === 'S' && res.data) {
          switch (stage) {
            case 1:
              this.baselineHistory = res.data;
              break;
            case 3:
              this.followUp1History = res.data;
              break;
            case 5:
              this.followUp2History = res.data;
              break;
          }
        } else {
          console.warn(`⚠️ No comorbidities found for stage ${stage}`);
        }
      },
      error: err => console.error(`❌ Error fetching comorbidities for stage ${stage}:`, err)
    });


    this.sleepService.getSleepByPatientId(this.patientId, stage).subscribe({
      next: (res: any) => {
        if (res.type === 'S' && res.data) {
          switch (stage) {
            case 1:
              this.baselineSleep = res.data;
              break;

          }
        } else {
          console.warn(`⚠️ No comorbidities found for stage ${stage}`);
        }
      },
      error: err => console.error(`❌ Error fetching comorbidities for stage ${stage}:`, err)
    });
    this.gadgetService.GetGadgetById(this.patientId, stage).subscribe({
      next: (res: any) => {
        if (res.type === 'S' && res.data) {
          switch (stage) {
            case 1:
              this.baselinegadge = res.data;
              break;

          }
        } else {
          console.warn(`⚠️ No comorbidities found for stage ${stage}`);
        }
      },
      error: err => console.error(`❌ Error fetching comorbidities for stage ${stage}:`, err)
    });


    this.assessmentService.getAssessmentById(this.patientId, stage).subscribe({
      next: (res: any) => {
        if (res.type === 'S' && res.data) {
          switch (stage) {
            case 1:
              this.baselineAssessment = res.data;
              break;
            case 3:
              this.followUp1Assessment = res.data;
              break;
            case 5:
              this.followUp2Assessment = res.data;
              break;
          }
        } else {
          console.warn(`⚠️ No comorbidities found for stage ${stage}`);
        }
      },
      error: err => console.error(`❌ Error fetching comorbidities for stage ${stage}:`, err)
    });


    this.managementService.getManagementDataById(this.patientId, stage).subscribe({
      next: (res: any) => {
        if (res.type === 'S' && res.data) {
          switch (stage) {
            case 1:
              this.baselinemanagement = res.data;
              break;
            case 3:
              this.followUp1management = res.data;
              break;
            case 5:
              this.followUp2management = res.data;
              break;
          }
        } else {
          console.warn(`⚠️ No comorbidities found for stage ${stage}`);
        }
      },
      error: err => console.error(`❌ Error fetching comorbidities for stage ${stage}:`, err)
    });

    this.personalHistoryService.getPersonalHistoryById(this.patientId, stage).subscribe({
      next: (res: any) => {
        if (res.type === 'S' && res.data) {
          switch (stage) {
            case 1:
              this.baselinepersonalHistory = res.data;
              break;
            case 3:
              this.followUp1management = res.data;
              break;
            case 5:
              this.followUp2management = res.data;
              break;
          }
        } else {
          console.warn(`⚠️ No comorbidities found for stage ${stage}`);
        }
      },
      error: err => console.error(`❌ Error fetching comorbidities for stage ${stage}:`, err)
    });



        this.currentMedicationsService.getCurrentMedicationById(this.patientId, stage).subscribe({
      next: (res: any) => {
        if (res.type === 'S' && res.data) {
          switch (stage) {
            case 1:
              this.baselineCurrentMedication = res.data;
              break;

          }
        } else {
          console.warn(`⚠️ No comorbidities found for stage ${stage}`);
        }
      },
      error: err => console.error(`❌ Error fetching comorbidities for stage ${stage}:`, err)
    });


  }

  async downloadAllStages(patientId: number) {
    const content = document.getElementById('caseStageContent');
    if (!content) return;

    const canvas = await html2canvas(content, { scale: 2 });
    const imgData = canvas.toDataURL('image/png');

    const pdf = new jsPDF('p', 'mm', 'a4');
    const imgWidth = 210; 
    const pageHeight = 297; 
    const imgHeight = (canvas.height * imgWidth) / canvas.width;

    let heightLeft = imgHeight;
    let position = 0;

    // First page
    pdf.addImage(imgData, 'PNG', 0, position, imgWidth, imgHeight);
    heightLeft -= pageHeight;

    // Extra pages if content is taller than one page
    while (heightLeft > 0) {
      position = heightLeft - imgHeight;
      pdf.addPage();
      pdf.addImage(imgData, 'PNG', 0, position, imgWidth, imgHeight);
      heightLeft -= pageHeight;
    }

    pdf.save(`Patient_${patientId}_All_Stages.pdf`);
  }
}
