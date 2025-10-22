




import { ChangeDetectorRef, Component, OnInit, HostListener } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { ChiefComplaintService } from '../Services/chief-complaint.service';
import { ComorbiditiesService } from '../Services/comorbidities.service';
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
import { Subscription } from 'rxjs';


@Component({
  selector: 'app-case-stage-view',
  templateUrl: './case-stage-view.component.html',
  styleUrls: ['./case-stage-view.component.scss']
})
export class CaseStageViewComponent implements OnInit {
  private pushStateCount = 5; 
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
  baselinepersonalHistory: any = null

  baselineAssessment: any = null
  followUp1Assessment: any = null;
  followUp2Assessment: any = null;

  baselinemanagement: any = null;
  followUp1management: any = null;
  followUp2management: any = null;
  loadingPdf: boolean = false;

  baselineCurrentMedication: any = null

  loadStageData1 = true;
  loadStageData2 = true;
  loadStageData3 = true;
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
    private currentMedicationsService: CurrentMedicationsService,
    private router: Router


  ) { }
  private routerSub!: Subscription;

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
    for (let i = 0; i < this.pushStateCount; i++) {
      history.pushState({ antiBack: true, idx: i }, '', window.location.href);
    }

    history.replaceState({ top: true }, '', window.location.href);
  }
   @HostListener('window:popstate', ['$event'])
  onPopState(event: PopStateEvent) {

    const confirmed = window.confirm(
      'Back navigation is disabled. Click OK to log out or Cancel to stay on this page.'
    );

    if (confirmed) {
      this.logoutUser();
      return;
    }

    setTimeout(() => {
      try {
        // push 2 states to ensure repeated backs don't slip through
        history.pushState({ antiBack: true }, '', window.location.href);
        history.pushState({ antiBack: true }, '', window.location.href);
      } catch (e) {
        // In case some browsers throw
        console.warn('pushState failed', e);
      }
    }, 50); // 30–150ms works; 50ms is a good tradeoff

    // Prevent default-like behavior by moving focus back; not strictly necessary:
    window.scrollTo(0, 0);
  }

  // Also handle page unloads (refresh / close)
  @HostListener('window:beforeunload', ['$event'])
  onBeforeUnload(event: BeforeUnloadEvent) {
    // Show native prompt in some browsers (message ignored by modern browsers)
    event.preventDefault();
    event.returnValue = '';
  }

  logoutUser(): void {
    localStorage.clear();
    sessionStorage.clear();
    // Use router navigate with replaceUrl to avoid extra history entry
    this.router.navigate(['/login'], { replaceUrl: true }).then(() => {
      // Force full navigation to ensure clean state
      window.location.href = '/login';
    });
  }
ngOnDestroy(): void {
    //window.removeEventListener('popstate', this.preventBackNavigation);
    this.routerSub?.unsubscribe();
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
              this.loadStageData1 = false
              break;
            case 3:
              this.followUp1Data = res.data;
              this.loadStageData2 = false
              break;
            case 5:
              this.followUp2Data = res.data;
              this.loadStageData3 = false
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
        }
      }
    })


    this.managementService.getManagementDataById(this.patientId, stage).subscribe({
      next: (res: any) => {
        if (res.type === 'S' && res.data) {
          switch (stage) {
            case 1:
              setTimeout(() => {
                this.baselinemanagement = res.data;
              }, 500);

              break;
            case 3:
              setTimeout(() => {
                this.followUp1management = res.data;
              }, 1500);

              break;
            // this.followUp1management = res.data;
            //break;
            case 5:
              setTimeout(() => {
                this.followUp2management = res.data;
              }, 1500);
              //this.followUp2management = res.data;
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

  // async downloadAllStages(patientId: number) {
  //   const content = document.getElementById('caseStageContent');
  //   if (!content) {
  //     alert('File not downloadable')
  //     return;
  //   }
  //   const canvas = await html2canvas(content, { scale: 1 });
  //   const imgData = canvas.toDataURL('image/png');

  //   const pdf = new jsPDF('p', 'mm', 'a4');
  //   const imgWidth = 210; 
  //   const pageHeight = 290; 
  //   const imgHeight = (canvas.height * imgWidth) / canvas.width;

  //   let heightLeft = imgHeight;
  //   let position = 0;

  //   // First page
  //   pdf.addImage(imgData, 'PNG', 0, position, imgWidth, imgHeight);
  //   heightLeft -= pageHeight;

  //   // Extra pages if content is taller than one page
  //   while (heightLeft > 0) {
  //     position = heightLeft - imgHeight;
  //     pdf.addPage();
  //     pdf.addImage(imgData, 'PNG', 0, position, imgWidth, imgHeight);
  //     heightLeft -= pageHeight;
  //   }

  //   pdf.save(`Patient_${patientId}_All_Stages.pdf`);
  // }


  async downloadAllStages(patientId: number) {
    this.loadingPdf = true;

    try {
      const doc = new jsPDF('p', 'mm', 'a4');
      const pageWidth = 210;
      const pageHeight = 290;

      // Select each component individually
      const components: HTMLElement[] = Array.from(
        document.querySelectorAll(
          'app-demographic, app-chief-complaint, app-comorbidities, app-history, app-personal-history, app-current-medications, app-sleep, app-gadget, app-history-endoscopy, app-assessment, app-diagnosis, app-managament'
        )
      ) as HTMLElement[];

      // Remove the first default empty page that jsPDF creates
      doc.deletePage(1);

      for (let i = 0; i < components.length; i++) {
        const el = components[i];
        if (!el) continue;

        await new Promise(resolve => setTimeout(resolve, 100));

        const canvas = await html2canvas(el, { scale: 2, useCORS: true });
        const imgData = canvas.toDataURL('image/png');
        const imgHeight = (canvas.height * pageWidth) / canvas.width;

        doc.addImage(imgData, 'PNG', 0, 0, pageWidth, imgHeight);

        if (i < components.length - 1) {
          doc.addPage();
        }
      }

      doc.save(`Patient_${patientId}_All_Stages.pdf`);
    } catch (err) {
      console.error('PDF generation failed:', err);
      alert('Failed to generate PDF. Make sure all components are rendered.');
    } finally {
      this.loadingPdf = false;
    }
  }

  printPage() {
    window.print();
  }

  goback() {
    this.router.navigate([`/dashboard`]);
    }
}
