import { Component } from '@angular/core';
import { Router } from '@angular/router';
import { PatientService } from '../Services/patient.service';
import { ExcelExportService } from '../Services/ExcelExportService.services';
import { forkJoin } from 'rxjs';
// import { saveAs } from 'file-saver'; 
import { HttpClient } from '@angular/common/http';
import { Injectable } from "@angular/core";
import { map, Observable } from "rxjs";
import { API_URLS } from "../shared/API-URLs";
import { HttpResponse } from '@angular/common/http';
import { ReportService } from '../Services/report.service';
import { HttpserviceService } from '../httpservice.service';
import { FormvalidationService } from '../formvalidation.service';
import * as XLSX from 'xlsx';
import { saveAs } from 'file-saver';
import { count } from 'console';


@Component({
  selector: 'app-admin-dashboard',
  templateUrl: './admin-dashboard.component.html',
  styleUrl: './admin-dashboard.component.css'
})
export class AdminDashboardComponent {
  startDate: string = '';
  endDate: string = '';
  states: Array<{ id: number; name: string }> = [];
  cities: any;
  dashboardStats = {
    doctors: 0,
    patients: 0,
    completedCases: 0,
    incompleteCases: 0,
    gender: { male: 0, female: 0,other: 0 },
    baselineCases: 0,
    followup1Cases: 0,
    followup2Cases: 0
  };
  patients: any;
  incompleteCases: any[] = [];
  doctors: any[] = [];
  gender!: { male: number; female: number; other: number };
  caseIdsToDownload = [];
  private baseUrl = API_URLS.BASE_URL;
  baslineReportList: any
  followUp1List: any;
  completedList: any;
  completed2List: any;
  inCompletedList: any;
  completedFollowUp2List: any;
  doctorsReportList:any;
  incompleteCount: any;
  completeCount:any;

  constructor(
    private httpClient: HttpClient,
    private router: Router,
    // private http: HttpserviceService,
    private https: HttpClient,
    private patientService: PatientService,
    private excelExportService: ExcelExportService,
    private reportService: ReportService,
    private http: HttpserviceService,
    private formValidation: FormvalidationService,
    // private httpResponse: HttpResponse<Blob>
  ) { }


  ngOnInit() {
    // this.getDoctorCount(); // fetch on load
    this.getPatientCount();
    this.getGenderCount();
    // this.loadBaselineCaseCount();
    // this.downloadCompletedCasesReport();
    this.loadStates();
this.downloadDoctorReportExcel()
    this.downloadBaselineReportExcel()
    this.getFollowUp1ReportExcel()
    this.getFollowUp2ReportExcel()
    this.inCompletedReportExcel()
    this.completedReportExcel()
  }
  filterReport() {
    // Add your logic here
  }

  downloadReport() {
    // Add your logic here
  }

  resetFilter() {
    this.startDate = '';
    this.endDate = '';
  }

  goBack() {
    // Navigate to previous page
  }

  goReport() {
    this.router.navigate([`/genderReport`]);

  }

  // idu all patient's data downloading
  downloadPatientsExcel() {
    this.patientService.getAllPatientData().subscribe(
      (response: any) => {
        console.log('API response:', response); // 🔍 Add this line

        if (response.type === 'S' && response.data && response.data.length > 0) {
          this.patients = response.data;

          // Export all patient data as Excel
          this.excelExportService.exportAsExcelFile(this.patients, 'All_Patients_Data');
        } else {
          alert('No patient data found.');
        }
      },
      (error: any) => {
        console.error('Error fetching patients:', error); // 🔍 Detailed error
        alert('Failed to download patients report.');
      }
    );
  }

  downloadGenderExcel(): void {
    this.patientService.getAllPatientData().subscribe(
      (res: any) => {
        const patients = res.data || [];
        let male = 0;
        let female = 0;
        let other= 0;

        patients.forEach((patient: any) => {
          const gender = patient.gender?.toLowerCase();
          if (gender === 'male' || gender === 'm') male++;
          else if (gender === 'female' || gender === 'f') female++;
          else if (gender === 'other' || gender === 'f') other++;
        });

        const data = [
          { Gender: 'Male', Count: male },
          { Gender: 'Female', Count: female },
          {Gender: 'other',count:other},
        ];

        this.excelExportService.exportAsExcelFile(data, 'Gender_Counts');
      },
      (error: any) => {
        console.error('Error downloading gender data:', error);
        alert('Failed to download gender data.');
      }
    );
  }

  getGenderCount(): void {
    this.patientService.getAllPatientData().subscribe(
      (res: any) => {
        const patients = res.data || [];
        let male = 0;
        let female = 0;
        let other = 0;

        patients.forEach((patient: any) => {
          const gender = patient.gender?.toLowerCase();
          if (gender === 'male' || gender === 'm') male++;
          else if (gender === 'female' || gender === 'f') female++;
           else if (gender === 'other' || gender === 'f') other++;
        });

        this.dashboardStats.gender.male = male;
        this.dashboardStats.gender.female = female;
        this.dashboardStats.gender.other = other;
      },
      (error: any) => {
        console.error('Error fetching gender counts:', error);
      }
    );
  }


  getPatientCount() {
    this.patientService.getAllPatientData().subscribe(
      (res: any) => {
        if (res.data && res.data.length > 0) {
          this.patients = res.data;
          this.dashboardStats.patients = res.data.length;

        }
        else {
          this.dashboardStats.patients = 0;
        }
      },
      (error: any) => {
        console.log("Error in fetching patients data", error);
        this.dashboardStats.doctors = 0;
      }
    )
  }
  // idu  Download Baseline Visit Cases using Report API
  downloadPatientsReportExcel(): void {
    this.http.httpGet('/Report/DownloadFullReport').subscribe((res: any) => {
      let list: any = res.data
      const worksheet = XLSX.utils.json_to_sheet(list);
      const workbook = XLSX.utils.book_new();
      XLSX.utils.book_append_sheet(workbook, worksheet, 'Report');

      const excelBuffer = XLSX.write(workbook, { bookType: 'xlsx', type: 'array' });
      const blob = new Blob([excelBuffer], { type: 'application/octet-stream' });
      saveAs(blob, 'PatientRPT.xlsx');
    }
    );
  }

  downloadBaselineReportExcel(): void {
    this.http.httpGet('/BaselineReport/GetBaselineReportData').subscribe((res: any) => {
      this.baslineReportList = res
    }
    );
  }

  downLoadBaseLine(){
    const worksheet = XLSX.utils.json_to_sheet(this.baslineReportList);
    const workbook = XLSX.utils.book_new();
    XLSX.utils.book_append_sheet(workbook, worksheet, 'Report');

    const excelBuffer = XLSX.write(workbook, { bookType: 'xlsx', type: 'array' });
    const blob = new Blob([excelBuffer], { type: 'application/octet-stream' });
    saveAs(blob, 'baslineReportList.xlsx');
  }


downloadDoctorReportExcel(): void {
  this.http.httpGet('/DoctorReport/DownloadDoctorReport').subscribe((res: any) => {
    if (Array.isArray(res) && res.length > 0) {
      this.doctorsReportList = res.map((doctor: any) => {
        // remove unwanted fields
        const { password, createdDt, createdBy, modifiedDt, modifiedBy, ...rest } = doctor;
        return rest;  // make sure to return!
      });
    } else {
      this.doctorsReportList = [];
      alert('No doctor data found.');
    }
  });
}

downLoadDoctor() {
  const worksheet = XLSX.utils.json_to_sheet(this.doctorsReportList);
  const workbook = XLSX.utils.book_new();
  XLSX.utils.book_append_sheet(workbook, worksheet, 'Report');

  const excelBuffer = XLSX.write(workbook, { bookType: 'xlsx', type: 'array' });
  const blob = new Blob([excelBuffer], { type: 'application/octet-stream' });
  saveAs(blob, 'doctorsReportList.xlsx');
}



  downLoadFollowUp1() {

    const worksheet = XLSX.utils.json_to_sheet(this.followUp1List);
    const workbook = XLSX.utils.book_new();
    XLSX.utils.book_append_sheet(workbook, worksheet, 'Report');

    const excelBuffer = XLSX.write(workbook, { bookType: 'xlsx', type: 'array' });
    const blob = new Blob([excelBuffer], { type: 'application/octet-stream' });
    saveAs(blob, 'downLoadFollowUp1.xlsx');
  }

  getFollowUp1ReportExcel(): void {
    this.http.httpGet('/FollowUp1Report/DownloadFollowUp1Report').subscribe((res: any) => {
      this.followUp1List = res
    }
    );
  }


  downFollowUp1ReportExcel() {

    const worksheet = XLSX.utils.json_to_sheet(this.followUp1List);
    const workbook = XLSX.utils.book_new();
    XLSX.utils.book_append_sheet(workbook, worksheet, 'Report');

    const excelBuffer = XLSX.write(workbook, { bookType: 'xlsx', type: 'array' });
    const blob = new Blob([excelBuffer], { type: 'application/octet-stream' });
    saveAs(blob, 'followUp1List.xlsx');
  }


  getFollowUp2ReportExcel(): void {
    this.http.httpGet('/FollowUp2Report/DownloadFollowUp1Report').subscribe((res: any) => {
      this.completedFollowUp2List = res
    }
    );
  }


  downFollowUp2ReportExcel() {

    const worksheet = XLSX.utils.json_to_sheet(this.completedFollowUp2List);
    const workbook = XLSX.utils.book_new();
    XLSX.utils.book_append_sheet(workbook, worksheet, 'Report');

    const excelBuffer = XLSX.write(workbook, { bookType: 'xlsx', type: 'array' });
    const blob = new Blob([excelBuffer], { type: 'application/octet-stream' });
    saveAs(blob, 'followUp2List.xlsx');
  }


  inCompletedReportExcel(): void {
  this.http.httpGet('/InCompletedReport/GetInCompletedReportDataCount').subscribe((res: any) => {
     this.incompleteCount=res;
    }
    );
  }

 downloadInCompletedList(): void {
    this.http.httpGet('/InCompletedReport/GetInCompletedReportData').subscribe((res: any) => {
    this.inCompletedList = res
    const worksheet = XLSX.utils.json_to_sheet(this.inCompletedList);
    const workbook = XLSX.utils.book_new();
    XLSX.utils.book_append_sheet(workbook, worksheet, 'Report');

    const excelBuffer = XLSX.write(workbook, { bookType: 'xlsx', type: 'array' });
    const blob = new Blob([excelBuffer], { type: 'application/octet-stream' });
    saveAs(blob, 'IncompletedReport.xlsx');

      }
   );
  }

    completedReportExcel(): void {
    this.http.httpGet('/CompletedReport/GetCompletedReportDataCount').subscribe((res: any) => {
      this.completeCount = res
    }
    );
  }
  
downloadCompleted2List(): void {
    this.http.httpGet('/CompletedReport/GetCompletedReportData').subscribe((res: any) => {
    this.completedList = res
    const worksheet = XLSX.utils.json_to_sheet(this.completedList);
    const workbook = XLSX.utils.book_new();
    XLSX.utils.book_append_sheet(workbook, worksheet, 'Report');

    const excelBuffer = XLSX.write(workbook, { bookType: 'xlsx', type: 'array' });
    const blob = new Blob([excelBuffer], { type: 'application/octet-stream' });
    saveAs(blob, 'CompletedReport.xlsx');
      }

    );

  }
  
  downloadFollowUp2Report(): void {
    const apiUrl = `${this.baseUrl}/FollowUp2Report/DownloadFollowUp1Report`;
    this.https.get(apiUrl, { responseType: "blob" }).subscribe(
      (response: Blob) => {
        if (!response || response.size === 0) {
          console.log("❌ The Excel file is empty or invalid.");
          alert('Failed to download the report. The file is empty or corrupted.');
          return;
        }
        const url = window.URL.createObjectURL(response);
        const a = document.createElement("a");
        a.href = url;
        a.download = "FollowUp2_Report.xlsx";
        a.click();
        window.URL.revokeObjectURL(url);
      },
      (error: any) => {
        console.error("Download Failed.....", error);
        alert("Failed to Download FollowUp2Report.");
      }
    );
  }

  downloadFollowUp2ReportExcel(): void {
    this.https.get(`${this.baseUrl}/FollowUp2Report/DownloadFollowUp1Report`, {
      observe: 'response',
      responseType: 'blob'
    }).subscribe(
      (res: HttpResponse<Blob>) => {
        const blob = res.body!;
        const recordCount = res.headers.get('X-Record-Count'); // Optional

        const url = window.URL.createObjectURL(blob);
        const a = document.createElement('a');
        a.href = url;
        a.download = 'FollowUp2_Report.xlsx';
        a.click();
        window.URL.revokeObjectURL(url);

        if (recordCount) {
          console.log('Total records in report:', recordCount);
        }
      },
      (error) => {
        console.error('Download failed', error);
        alert('Failed to download Follow-Up 2 report.');
      }
    );
  }

  loadStates() {
    if (this.states.length === 0) {
      this.http.httpGet(API_URLS.STATE_GET).subscribe({
        next: (res: any) => {
          // Sort states alphabetically by name
          this.states = res.sort((a: any, b: any) => {
            const nameA = a.name.toLowerCase();
            const nameB = b.name.toLowerCase();
            return nameA.localeCompare(nameB);
          });
        },
        error: (err) => {
          this.formValidation.showAlert('Error loading states', 'danger');
          console.error(err);
        }
      });
    }
  }
    login(){
    this.router.navigate(['/login']);
  }

    goToCoMorbiditiesReport(){
    this.router.navigate([`/CoMorbiditiesReport`]);
  }
  goTotreatmentReport(){
    this.router.navigate(['/treatmentReport']);
  }
    goDoctorlist(){  
    this.router.navigate(['/doctor-list']);
  }

    goTocontactUs(){
    this.router.navigate(['/contact-us']);
  }

  goTofilterchart(){
    this.router.navigate(['/filterchartComponent']);
  }
}