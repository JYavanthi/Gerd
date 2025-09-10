import { Component, Input, OnInit } from '@angular/core';
import { PatientService } from '../Services/patient.service';
import { ChiefComplaintService } from '../Services/chief-complaint.service';

@Component({
  selector: 'app-stage-progress',
  templateUrl: './stage-progress.component.html',
  styleUrls: ['./stage-progress.component.css']
})
export class StageProgressComponent implements OnInit {
  @Input() patientId!: number;
  @Input() stage: number = 0; // Patient stage from API
 currentStage: number = 0;
  constructor(private patientService: PatientService,private ccService: ChiefComplaintService) {}

   ngOnInit(): void {
    if (this.patientId) {
      this.ccService.getChiefComplaintByPatientId(this.patientId, 0).subscribe({
        next: (res) => {
          // Assuming API returns stage as res.data.stage
          //this.stage = res?.data?.stage ?? 0;
        },
        error: (err) => {
          console.error('Error fetching chief complaint:', err);
        }
      });
    }
  }

 getStageClass(step: 'baseline' | 'fu1' | 'fu2'): string {
    if (step === 'baseline') {
      if (this.stage >= 1) return 'completed';
      if (this.stage === 0) return 'in-progress';
    }
    if (step === 'fu1') {
      if (this.stage >= 3) return 'completed';
      if (this.stage === 2) return 'in-progress';
    }
    if (step === 'fu2') {
      if (this.stage === 5) return 'completed';
      if (this.stage === 4) return 'in-progress';
    }
    return 'pending';
  }
}
