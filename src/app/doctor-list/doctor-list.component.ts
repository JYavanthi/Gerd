import { Component, HostListener, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { DoctorService } from '../Services/doctor.service';
import { CaseDataService, Case } from '../Services/case-data.services';
import { HttpserviceService } from '../httpservice.service';
import { forkJoin, Subscription } from 'rxjs';

import * as XLSX from 'xlsx';
import { saveAs } from 'file-saver';
import jsPDF from 'jspdf';

@Component({
  selector: 'app-doctor-list',
  templateUrl: './doctor-list.component.html',
  styleUrls: ['./doctor-list.component.scss']

})
export class DoctorListComponent implements OnInit {
   private pushStateCount = 5; 
 Math = Math; // ✅ expose global Math object to the template

  currentPage: number = 1;
  itemsPerPage: number = 10;  // you can adjust as needed
  totalItems: number = 0;
  totalPages: number = 0;

  
  pageNumbers: number[] = []
  doctorList: any[] = [];
  filteredDoctors: any[] = [];
  displayedDoctors: any[] = [];
  displayedStates: string[] = [];
  displayedStages: any[] = [];

  selectedDoctorIds: string[] = [];
  selectedStates: string[] = [];
  selectedStages: string[] = [];

  doctorSearch: string = '';
  stateSearch: string = '';
  stageSearch: string = '';

  showDoctorDropdown: boolean = false;
  showStateDropdown: boolean = false;
  showStageDropdown: boolean = false;
  stageOptions = [
    { label: 'Baseline', value: 'baseline' },
    { label: 'FollowUp One', value: 'followUpOne' },
    { label: 'FollowUp Two', value: 'followUpTwo' },
  ];

  expandedDoctorId: string | null = null;
  expandedStage: string | null = null;
  stagePatients: Case[] = [];

  dueDaysforBaseLine?: number;
  dueDaysforFollowUpOne?: number;
  dueDaysforFollowUpTwo?: number;


  constructor(
    private doctorService: DoctorService,
    private caseDataService: CaseDataService,
    private router: Router,
    private http: HttpserviceService,
  ) { }

  private routerSub!: Subscription;
  ngOnInit(): void {
    this.displayedStages = [...this.stageOptions];
    this.loadDoctors();
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
    this.routerSub?.unsubscribe();
  }

  loadDoctors() {
    this.doctorService.getAllDoctorslist().subscribe({
      next: (response: any) => {
        let doctors: any[] = Array.isArray(response.data) ? response.data : response;
        doctors.sort((a, b) => a.name.localeCompare(b.name));

        this.doctorList = doctors.map(d => ({
          ...d,
          baseline: 0,
          followUpOne: 0,
          followUpTwo: 0
        }));

        this.displayedDoctors = [...this.doctorList];
        this.displayedStates = Array.from(new Set(this.doctorList.map(d => d.state))).sort();

        this.caseDataService.getCases().subscribe(cases => {
          const today = this.toDateOnly(new Date());
          const msInDay = 1000 * 60 * 60 * 24;

          this.doctorList.forEach(d => {
            const patients = cases.filter(p => p.doctorId === d.doctorId);

            // Baseline: stage 0, no due check
            const baselinePatients = patients.filter(p => {
              if (p.stage !== 0) return false;
              const baselineDue = new Date(p.createdDt);
              baselineDue.setDate(baselineDue.getDate() + 16);
              return baselineDue <= today;
            });

            // Follow-up 1: stage 1 and due
            const followUpOnePatients = patients.filter(p => {
              if (p.stage !== 1) return false;
              if (!p.blsubmitted) return false;
              const fu1 = new Date(p.blsubmitted);
              fu1.setDate(fu1.getDate() + 46);
              return fu1 <= today;
            });

            // Follow-up 2: stage 3 and due
            const followUpTwoPatients = patients.filter(p => {
              if (p.stage !== 3) return false;
              if (!p.fu1submitted) return false;
              const fu2 = new Date(p.fu1submitted);
              fu2.setDate(fu2.getDate() + 76);
              return fu2 <= today;
            });

            d.baseline = baselinePatients.length;
            d.followUpOne = followUpOnePatients.length;
            d.followUpTwo = followUpTwoPatients.length;
          });

          this.filteredDoctors = [...this.doctorList];

          this.totalItems = this.filteredDoctors.length;
          this.totalPages = Math.ceil(this.totalItems / this.itemsPerPage);
          this.updateDisplayedDoctors();
        });

      },
      error: (err) => console.error(err)
    });
  }

  // Search filters
  filterDoctorList() {
    const search = this.doctorSearch.toLowerCase();
    this.displayedDoctors = this.doctorList.filter(d => d.name.toLowerCase().includes(search));
  }

  filterStateList() {
    const search = this.stateSearch.toLowerCase();
    this.displayedStates = Array.from(new Set(
      this.doctorList.map(d => d.state).filter(s => s.toLowerCase().includes(search))
    )).sort();
  }

  filterStageList() {
    const search = this.stageSearch.toLowerCase();
    this.displayedStages = this.stageOptions.filter(stage =>
      stage.label.toLowerCase().includes(search)
    );
  }

  // Checkbox handlers
  onDoctorChange(event: any, doctorId: string) {
    if (event.target.checked) this.selectedDoctorIds.push(doctorId);
    else this.selectedDoctorIds = this.selectedDoctorIds.filter(id => id !== doctorId);
    this.applyFilters();
  }

  onStateChange(event: any, state: string) {
    if (event.target.checked) this.selectedStates.push(state);
    else this.selectedStates = this.selectedStates.filter(s => s !== state);
    this.applyFilters();
  }

  onStageChange(event: any, stageValue: string) {
    if (event.target.checked) this.selectedStages.push(stageValue);
    else this.selectedStages = this.selectedStages.filter(s => s !== stageValue);
    this.applyFilters();
  }

  // Apply filters to table
  applyFilters() {
    this.filteredDoctors = this.doctorList.filter(d => {
      const matchDoctor = this.selectedDoctorIds.length === 0 || this.selectedDoctorIds.includes(d.doctorId);
      const matchState = this.selectedStates.length === 0 || this.selectedStates.includes(d.state);
      const matchStage = this.selectedStages.length === 0 || this.selectedStages.some(stage => d[stage] && d[stage] > 0);
      return matchDoctor && matchState && matchStage;
    });

     this.totalItems = this.filteredDoctors.length;
  this.totalPages = Math.ceil(this.totalItems / this.itemsPerPage);
  this.currentPage = 1; // reset to first page
  this.updateDisplayedDoctors();
  }

  // Select All / Toggle
  allDoctorsSelected(): boolean {
    return this.selectedDoctorIds.length === this.displayedDoctors.length;
  }

  toggleSelectAllDoctors(event: any) {
    this.selectedDoctorIds = event.target.checked ? this.displayedDoctors.map(d => d.doctorId) : [];
    this.applyFilters();
  }

  allStatesSelected(): boolean {
    return this.selectedStates.length === this.displayedStates.length;
  }

  toggleSelectAllStates(event: any) {
    this.selectedStates = event.target.checked ? [...this.displayedStates] : [];
    this.applyFilters();
  }

  allStagesSelected(): boolean {
    return this.selectedStages.length === this.displayedStages.length;
  }

  toggleSelectAllStages(event: any) {
    this.selectedStages = event.target.checked ? this.displayedStages.map(s => s.value) : [];
    this.applyFilters();
  }

  viewDoctor(doctor: any): void {
    this.router.navigate(['/doctor-details'], { state: { doctor } });
  }






  sendTestEmail(doctor: any) {
  this.caseDataService.getCases().subscribe((cases: Case[]) => {
    const patientsForDoctor = cases.filter(p => p.doctorId === doctor.doctorId);

    if (!patientsForDoctor.length) {
      console.warn(`❌ No patients found for Doctor ${doctor.name}`);
      return;
    }

    const today = this.toDateOnly(new Date()); 
    const msInDay = 1000 * 60 * 60 * 24;

    const duePatients = patientsForDoctor.filter(patient => {
      if (patient.stage === 0) {
        const baselineDue = this.toDateOnly(new Date(patient.createdDt));
        baselineDue.setDate(baselineDue.getDate() + 16);
        return baselineDue <= today;
      } 
      else if (patient.stage === 1) {
        if (!patient.blsubmitted) return false;
        const fu1 = this.toDateOnly(new Date(patient.blsubmitted));
        fu1.setDate(fu1.getDate() + 46);
        return fu1 <= today;
      } 
      else if (patient.stage === 3) {
        if (!patient.fu1submitted) return false;
        const fu2 = this.toDateOnly(new Date(patient.fu1submitted));
        fu2.setDate(fu2.getDate() + 76);
        return fu2 <= today;
      }
      return false;
    });

    if (!duePatients.length) {
      console.warn(`❌ No follow-up patients due for Doctor ${doctor.name}`);
      return;
    }

    // Prepare all email observables
    const emailObservables = duePatients.map(patient => {
      const created = this.toDateOnly(patient.createdDt);
      let stageText = '';
      let dueDate: Date;

      if (patient.stage === 0) {
        stageText = 'BaseLine';
        dueDate = this.toDateOnly(new Date(patient.createdDt));
        dueDate.setDate(dueDate.getDate() + 16);
      } 
      else if (patient.stage === 1) {
        stageText = 'Follow-up1';
        dueDate = this.toDateOnly(new Date(patient.blsubmitted));
        dueDate.setDate(dueDate.getDate() + 46);
      } 
      else if (patient.stage === 3) {
        stageText = 'Follow-up2';
        dueDate = this.toDateOnly(new Date(patient.fu1submitted));
        dueDate.setDate(dueDate.getDate() + 76);
      } 
      else {
        return null;
      }

      const dueDays = Math.ceil((today.getTime() - dueDate.getTime()) / msInDay);

      const payload = {
        patientId: patient.patientId,
        date: created.toISOString().split('T')[0],
        stage: patient.stage,
        email: doctor.email,
        subject: `${stageText} Reminder`,
        dueDays: dueDays,
        body: `
          <p>Dear Dr. ${doctor.name},</p>
          <p>This is a reminder that patient <b>${patient.initial}</b> <b>${stageText}</b> is overdue.</p>
          <ul>
            <li><b>Patient initial:</b> ${patient.initial}</li>
            <li><b>Stage:</b> ${stageText}</li>
            <li><b>Created Date:</b> ${created.toDateString()}</li>
            <li><b>Overdue:</b> ${dueDays} day(s)</li>
          </ul>
          <p>Could you please take action accordingly.</p>
          <br/>
          <p>Best regards,<br/>Admin</p>
        `
      };

      return this.http.httpPostMail('/Email', payload, { responseType: 'text' });
    }).filter(Boolean); // remove nulls

    if (emailObservables.length > 0) {
      forkJoin(emailObservables).subscribe({
        next: () => alert(`Follow-up email(s) sent successfully for Dr. ${doctor.name}!`),
        error: err => alert(`Some email(s) failed to send for Dr. ${doctor.name}. Check console.`)
      });
    }
  });
}


  @HostListener('document:click', ['$event'])
  onClickOutside(event: MouseEvent) {
    const target = event.target as HTMLElement;
    if (!target.closest('.dropdown-container')) {
      this.showDoctorDropdown = false;
      this.showStateDropdown = false;
      this.showStageDropdown = false;
    }
  }



  toggleStagePatients(doctor: any, stage: string) {
    if (this.expandedDoctorId === doctor.doctorId && this.expandedStage === stage) {
      this.expandedDoctorId = null;
      this.expandedStage = null;
      this.stagePatients = [];
      return;
    }

    this.caseDataService.getCases().subscribe((cases: Case[]) => {
      const today = this.toDateOnly(new Date());
      const msInDay = 1000 * 60 * 60 * 24;

      this.stagePatients = cases
        .filter(p => p.doctorId === doctor.doctorId)
        .filter(p => {
          if (stage === 'baseline') {
            if (p.stage !== 0) return false;
            const baselineDue = new Date(p.createdDt);
            baselineDue.setDate(baselineDue.getDate() + 16);
            return baselineDue <= today;
          }

          if (stage === 'followUpOne' && p.stage === 1) {
            if (!p.blsubmitted) return false;
            const fu1 = new Date(p.blsubmitted);
            fu1.setDate(fu1.getDate() + 46);
            return fu1 <= today;
          }

          if (stage === 'followUpTwo' && p.stage === 3) {
            if (!p.fu1submitted) return false;
            const fu2 = new Date(p.fu1submitted);
            fu2.setDate(fu2.getDate() + 76);
            return fu2 <= today;
          }

          return false;
        })
        .map(patient => {
          if (stage === 'baseline') {
            const bs1 = new Date(patient.createdDt);
            bs1.setDate(bs1.getDate() + 16);
            return { ...patient, dueDaysforBaseLine: Math.ceil((today.getTime() - bs1.getTime()) / msInDay) };
          }
          if (stage === 'followUpOne') {
            const fu1 = new Date(patient.blsubmitted);
            fu1.setDate(fu1.getDate() + 46);
            return { ...patient, dueDaysforFollowUpOne: Math.ceil((today.getTime() - fu1.getTime()) / msInDay) };
          }
          if (stage === 'followUpTwo') {
            const fu2 = new Date(patient.fu1submitted);
            fu2.setDate(fu2.getDate() + 76);
            return { ...patient, dueDaysforFollowUpTwo: Math.ceil((today.getTime() - fu2.getTime()) / msInDay) };
          }
          return { ...patient }; // baseline
        });

      this.expandedDoctorId = doctor.doctorId;
      this.expandedStage = stage;
    });
  }


  toDateOnly(date: any): Date {
    const d = new Date(date);
    return new Date(d.getFullYear(), d.getMonth(), d.getDate()); // strip time, keep valid date
  }

  onDoctorRowCheck(event: any, doctorId: string) {
    if (event.target.checked) {
      if (!this.selectedDoctorIds.includes(doctorId)) {
        this.selectedDoctorIds.push(doctorId);
      }
    } else {
      this.selectedDoctorIds = this.selectedDoctorIds.filter(id => id !== doctorId);
    }
  }
  toggleSelectAllDoctorsTable(event: any) {
    if (event.target.checked) {
      this.selectedDoctorIds = this.filteredDoctors.map(d => d.doctorId);
    } else {
      this.selectedDoctorIds = [];
    }
  }

  // Check if all displayed doctors are selected
  allDoctorsSelectedTable(): boolean {
    return this.filteredDoctors.length > 0 &&
      this.filteredDoctors.every(d => this.selectedDoctorIds.includes(d.doctorId));
  }


  sendNotificationToSelectedDoctors() {
    if (this.selectedDoctorIds.length === 0) {
      alert('Please select at least one doctor before sending notification!');
      return;
    }

    const doctorsToNotify = this.doctorList.filter(d =>
      this.selectedDoctorIds.includes(d.doctorId)
    );
    const today = this.toDateOnly(new Date());
    const msInDay = 1000 * 60 * 60 * 24;

    this.caseDataService.getCases().subscribe((cases: Case[]) => {

      const emailObservables: any[] = [];

      doctorsToNotify.forEach(doctor => {
        const patientsForDoctor = cases.filter(p => p.doctorId === doctor.doctorId);

        const duePatients = patientsForDoctor.filter(patient => {
          if (patient.stage === 0) {
            const baselineDue = new Date(patient.createdDt);
            baselineDue.setDate(baselineDue.getDate() + 16);
            return baselineDue <= today;
          } else if (patient.stage === 1) {
            if (!patient.blsubmitted) return false;
            const fu1 = new Date(patient.blsubmitted);
            fu1.setDate(fu1.getDate() + 46);
            return fu1 <= today;
          } else if (patient.stage === 3) {
            if (!patient.fu1submitted) return false;
            const fu2 = new Date(patient.fu1submitted);
            fu2.setDate(fu2.getDate() + 76);
            return fu2 <= today;
          }
          return false;
        });

        duePatients.forEach(patient => {
          const created = this.toDateOnly(patient.createdDt);
          let stageText = '';
          let dueDays = 0;
          let dueDate: Date = new Date(patient.createdDt);

          if (patient.stage === 0) {
            stageText = 'BaseLine';
            dueDate = new Date(patient.createdDt);
            dueDate.setDate(dueDate.getDate() + 16);
          } else if (patient.stage === 1) {
            stageText = 'Follow-up1';
            dueDate = new Date(patient.blsubmitted);
            dueDate.setDate(dueDate.getDate() + 46);
          } else if (patient.stage === 3) {
            stageText = 'Follow-up2';
            dueDate = new Date(patient.fu1submitted);
            dueDate.setDate(dueDate.getDate() + 76);
          }

          dueDays = Math.ceil((today.getTime() - dueDate.getTime()) / msInDay);

          const payload = {
            patientId: patient.patientId,
            date: created.toISOString().split('T')[0],
            stage: patient.stage,
            email: doctor.email,
            subject: `${stageText} Reminder`,
            dueDays: dueDays,
            body: `
            <p>Dear Dr. ${doctor.name},</p>
            <p>This is a reminder that patient <b>${patient.initial}</b> <b>${stageText}</b> is overdue.</p>
            <ul>
              <li><b>Patient initial:</b> ${patient.initial}</li>
              <li><b>Stage:</b> ${stageText}</li>
              <li><b>Created Date:</b> ${created.toDateString()}</li>
              <li><b>Overdue:</b> ${dueDays} day(s)</li>
            </ul>
            <p>Could you please take action accordingly.</p>
            <br/>
            <p>Best regards,<br/>Admin</p>
          `
          };

          emailObservables.push(
            this.http.httpPostMail('/Email', payload, { responseType: 'text' })
          );
        });
      });

      // ✅ Run once after all doctor emails are collected
      if (emailObservables.length > 0) {
        forkJoin(emailObservables).subscribe({
          next: () => alert('All notifications sent successfully!'),
          error: err => alert('Some emails failed to send. Check console for details.')
        });
      }
    });
  }





  updateDisplayedDoctors() {
    const startIndex = (this.currentPage - 1) * this.itemsPerPage;
    const endIndex = startIndex + this.itemsPerPage;
    this.displayedDoctors = this.filteredDoctors.slice(startIndex, endIndex);
    this.generatePageNumbers();
  }

  generatePageNumbers() {
    const maxPagesToShow = 5;
    let startPage = Math.max(1, this.currentPage - Math.floor(maxPagesToShow / 2));
    let endPage = Math.min(this.totalPages, startPage + maxPagesToShow - 1);

    if (endPage - startPage < maxPagesToShow - 1) {
      startPage = Math.max(1, endPage - maxPagesToShow + 1);
    }

    this.pageNumbers = [];
    for (let i = startPage; i <= endPage; i++) {
      this.pageNumbers.push(i);
    }
  }

  goToPage(page: number) {
    if (page < 1 || page > this.totalPages) return;
    this.currentPage = page;
    this.updateDisplayedDoctors();
  }

  goToPrevious() {
    if (this.currentPage > 1) {
      this.currentPage--;
      this.updateDisplayedDoctors();
    }
  }



  // Export currently displayed table to Excel
exportToExcel() {
  const tableElement = document.getElementById('excel-table');

  if (!tableElement) {
    console.error("Table element not found.");
    return;
  }

  // Convert HTML table to worksheet
  const ws: XLSX.WorkSheet = XLSX.utils.table_to_sheet(tableElement);

  // Create a new workbook and append the worksheet
  const wb: XLSX.WorkBook = XLSX.utils.book_new();
  XLSX.utils.book_append_sheet(wb, ws, 'DoctorList');

  // Save as Excel file
  XLSX.writeFile(wb, 'DoctorList.xlsx');
}

  goToNext() {
    if (this.currentPage < this.totalPages) {
      this.currentPage++;
      this.updateDisplayedDoctors();
    }
  }

  login() {
    this.router.navigate(['/login']);
  }

  goToCoMorbiditiesReport() {
    this.router.navigate([`/CoMorbiditiesReport`]);
  }
  goTotreatmentReport() {
    this.router.navigate(['/treatmentReport']);
  }
  goDoctorlist() {
    this.router.navigate(['/doctor-list']);
  }

  goTocontactUs() {
    this.router.navigate(['/contact-us']);
  }


  goReport() {
    this.router.navigate([`/genderReport`]);

  }

  goDashboard() {
    this.router.navigate([`/admindashboard`]);
  }

  goFilterCharts() {
    this.router.navigate([`/allReport`]);
  }
}

