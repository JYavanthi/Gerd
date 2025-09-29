import { Component, Input, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { DemographicService } from '../Services/demographic.service';
import { HttpserviceService } from '../httpservice.service';

@Component({
  selector: 'app-stage-progress',
  templateUrl: './stage-progress.component.html',
  styleUrls: ['./stage-progress.component.css']
})
export class StageProgressComponent implements OnInit {
  // @Input() stage: number = 0; 
  @Input() patientId: number | null = null;

  stageFromApi: number = 0;   

  constructor(
    private ccService: DemographicService,
    private http: HttpserviceService,
    private router: Router
  ) {}

  ngOnInit(): void {
    if (this.patientId) {
      this.ccService.getDemographicDetailsByPatientId(this.patientId).subscribe({
        next: (res) => {
          this.stageFromApi = res?.data?.stage ?? 0;
        },
        error: (err) => {
          console.error('Error fetching demographic details:', err);
        }
      });
    }
  }

  getStageClass(step: 'baseline' | 'fu1' | 'fu2'): string {
    if (step === 'baseline') {
      if (this.stageFromApi >= 1) return 'completed';
      return 'in-progress';
    }
    if (step === 'fu1') {
      if (this.stageFromApi >= 3) return 'completed';
      if (this.stageFromApi >= 1) return 'in-progress';
      return 'pending';
    }
    if (step === 'fu2') {
      if (this.stageFromApi >= 5) return 'completed';
      if (this.stageFromApi >= 3) return 'in-progress';
      return 'pending';
    }
    return 'pending';
  }



// handleStageClick(section: 'baseline' | 'fu1' | 'fu2') {
//   if (!this.patientId) {
//     alert('❌ Patient ID missing');
//     return;
//   }

//   let stage = 0;
//   if (section === 'baseline') stage = this.stageFromApi >= 1 ? 1 : 0;
//   if (section === 'fu1') stage = this.stageFromApi >= 3 ? 3 : 2;
//   if (section === 'fu2') stage = this.stageFromApi >= 5 ? 5 : 4;

//   // ✅ Only go to view mode if actually completed
//   if (
//     (section === 'baseline' && this.stageFromApi >= 1) ||
//     (section === 'fu1' && this.stageFromApi >= 3) ||
//     (section === 'fu2' && this.stageFromApi >= 5)
//   ) {
//     this.router.navigate(['/demographic', this.patientId, stage], {
//       state: { isViewMode: true }
//     });
//     return;
//   }

//   // 🔹 Otherwise → call tracker API
//   this.http.httpGet(`/PtnTrack/GetPageRouterByPatientId/${this.patientId}`).subscribe({
//     next: (res: any) => {
//       let pageRouter = res?.pageRouter;
//       if (!pageRouter) {
//         alert('⚠️ No route found for this patient.');
//         return;
//       }

//       pageRouter = pageRouter.trim().replace(/\/+$/, '');
//       let parts = pageRouter.split('/').filter(Boolean);

//       // Replace {patientId} placeholder
//       parts = parts.map((p: string) =>
//         p === '{patientId}' ? String(this.patientId) : p
//       );

//       // Ensure stage index is present
//       if (parts.length === 2) {
//         parts.push(String(stage));
//       } else if (parts.length >= 3) {
//         parts[2] = String(stage);
//       }

//       const finalRoute = '/' + parts.join('/');
//       console.log('✅ Navigating to:', finalRoute);

//       this.router.navigate([finalRoute], {
//         state: { isViewMode: false } // edit mode
//       });
//     },
//     error: (err) => {
//       console.error('❌ Error fetching page router:', err);
//       alert('Server error while fetching route.');
//     }
//   });
// }


handleStageClick(section: 'baseline' | 'fu1' | 'fu2') {
  if (!this.patientId) {
    alert('❌ Patient ID missing');
    return;
  }

  // 🔹 Enforce sequential restriction
  if (section === 'fu1' && this.stageFromApi < 1) {
    alert('⚠️ Complete Baseline before accessing Follow-up 1');
    return;
  }
  if (section === 'fu2' && this.stageFromApi < 3) {
    alert('⚠️ Complete Follow-up 1 before accessing Follow-up 2');
    return;
  }

  let stage = 0;
  if (section === 'baseline') stage = this.stageFromApi >= 1 ? 1 : 0;
  if (section === 'fu1') stage = this.stageFromApi >= 3 ? 3 : 2;
  if (section === 'fu2') stage = this.stageFromApi >= 5 ? 5 : 4;

  // ✅ If already completed → open in view mode
  if (
    (section === 'baseline' && this.stageFromApi >= 1) ||
    (section === 'fu1' && this.stageFromApi >= 3) ||
    (section === 'fu2' && this.stageFromApi >= 5)
  ) {
    this.router.navigate(['/demographic', this.patientId, stage], {
      state: { isViewMode: true }
    });
    return;
  }

  // 🔹 Otherwise → continue in edit mode
  this.http.httpGet(`/PtnTrack/GetPageRouterByPatientId/${this.patientId}`).subscribe({
    next: (res: any) => {
      let pageRouter = res?.pageRouter;
      if (!pageRouter) {
        alert('⚠️ No route found for this patient.');
        return;
      }

      pageRouter = pageRouter.trim().replace(/\/+$/, '');
      let parts = pageRouter.split('/').filter(Boolean);

      // Replace {patientId} placeholder
      parts = parts.map((p: string) =>
        p === '{patientId}' ? String(this.patientId) : p
      );

      // Ensure stage index is present
      if (parts.length === 2) {
        parts.push(String(stage));
      } else if (parts.length >= 3) {
        parts[2] = String(stage);
      }

      const finalRoute = '/' + parts.join('/');
      console.log('✅ Navigating to:', finalRoute);

      this.router.navigate([finalRoute], {
        state: { isViewMode: false } // edit mode
      });
    },
    error: (err) => {
      console.error('❌ Error fetching page router:', err);
      alert('Server error while fetching route.');
    }
  });
}

}
