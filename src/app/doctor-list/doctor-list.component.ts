
import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { DoctorService } from '../Services/doctor.service';
import { CaseDataService, Case } from '../Services/case-data.services';
import { HttpserviceService } from '../httpservice.service';

@Component({
  selector: 'app-doctor-list',
  templateUrl: './doctor-list.component.html'
})
export class DoctorListComponent implements OnInit {
  doctorList: any[] = [];
  patientId!: number;
  stage!: number;
  stateMap: { [key: number]: string } = {};
  cityMap: { [key: number]: string } = {};
  constructor(
    private doctorService: DoctorService,
    private caseDataService: CaseDataService,
    private router: Router,
    private http: HttpserviceService,
  ) { }

  ngOnInit(): void {
    
    this.doctorService.getAllDoctors().subscribe({
      next: (response: any) => {
        if (Array.isArray(response.data)) {
          // ✅ Assign doctor list first
          this.doctorList = response.data.map((doctor: any) => ({
            ...doctor,
            baseline: 0,
            followUpOne: 0,
            followUpTwo: 0
          }));

          // ✅ Now subscribe to patient cases
          this.caseDataService.getCases().subscribe((cases: Case[]) => {
            this.doctorList.forEach((doctor: any) => {
              const patientsForDoctor = cases.filter(p => p.doctorId === doctor.doctorId);

              doctor.baseline = patientsForDoctor.filter(p => p.stage === 0).length;
              doctor.followUpOne = patientsForDoctor.filter(p => p.stage === 1).length;
              doctor.followUpTwo = patientsForDoctor.filter(p => p.stage === 3).length;
            });
          });

        } else {
          this.doctorList = [];
        }
      },
      error: (error) => {
        console.error(error);
      }
    });
  }


  viewDoctor(doctor: any): void {
    this.router.navigate(['/doctor-details'], { state: { doctor } });
  }



  sendTestEmail(doctor: any) {
    this.caseDataService.getCases().subscribe((cases: Case[]) => {
      const patientsForDoctor = cases.filter(p => p.doctorId === doctor.doctorId);

      if (patientsForDoctor.length === 0) {
        console.warn(`❌ No patients found for Doctor ${doctor.name}`);
        return;
      }

      // ✅ Only follow-up patients
      const followUpPatients = patientsForDoctor.filter(
        p => p.stage === 1 || p.stage === 2 || p.stage === 3
      );

      if (followUpPatients.length === 0) {
        console.warn(`❌ No follow-up patients found for Doctor ${doctor.name}`);
        return;
      }

      // ✅ Call backend once per patient (backend does scheduling)
      followUpPatients.forEach(patient => {
        const stageText = patient.stage === 1 ? "Follow-up One" : "Follow-up Two";

        console.log("patient.patientName");
        const payload = {
          patientId: patient.patientId,
          date: patient.date,
          stage: patient.stage,
          email: doctor.email,
          subject: "Follow-up Reminder",
          body: `
          <p>Dear Dr. ${doctor.name},</p>
          <p>This is a reminder that patient <b>${patient.initial}</b> is scheduled for <b>${stageText}</b>.</p>
  <ul>
            <li><b>Patient initial:</b> ${patient.initial}</li>
            <li><b>Stage:</b> ${stageText}</li>
            <li><b>Scheduled Date:</b> ${patient.date}</li>
          </ul>
          <p>The first email will be sent immediately. Additional reminders are scheduled automatically.</p>
          <br/>
          <p>Best regards,<br/>Admin</p>
        `
        };

        console.log("📩 Sending payload to backend:", payload);

        this.http.httpPost('/Email', payload).subscribe({
          next: () => console.log(`✅ Email & schedule created for patient ${patient.patientId}`),
          error: (err) => console.error(`❌ Failed for patient ${patient.patientId}`, err)
        });
      });
    });
  }
getStateName(id: number): string {
  return this.stateMap[id] || id.toString();
}

getCityName(id: number): string {
  return this.cityMap[id] || id.toString();
}
}