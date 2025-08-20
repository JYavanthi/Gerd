import { Injectable } from "@angular/core";
import { BehaviorSubject, map, Observable, tap } from "rxjs";
import { HttpserviceService } from "../httpservice.service";
import { PatientService } from "./patient.service";
import { API_URLS } from '../shared/API-URLs';
import { FormvalidationService } from "../formvalidation.service";

// export interface Case {
//   patientId: number;
//   doctorId: number;
//   initial: string;
//   subjectNo: string;
//   gender: string;
//   date: string;
//   baseline?: string;
//   followUp1?: string;
//   followUp2?: string;
//   status: string;
//   stage?: number;
//   tabId?: number;
//   statusClass?: string;
//   baselineClass?: string;
//   followUp1Class?: string;
//   followUp2Class?: string;
// }
export interface Case {
  patientId: number;
  doctorId: number;
  initial: string;
  subjectNo: string;
  gender: string;
  date: string;
  baseline?: string;
  followUp1?: string;
  followUp2?: string;
  status: string;
  stage?: number;
  tabId?: number;
  statusClass?: string;
  baselineClass?: string;
  followUp1Class?: string;
  followUp2Class?: string;
  patientName?: string;  

  // 👇 Add this to allow indexing by string
  [key: string]: any;
}


interface ApiResponse<T> {
  type: string;
  message: string;
  count: number;
  data: T;
}

@Injectable({
  providedIn: 'root'
})
export class CaseDataService {
  private caseList: Case[] = [];
  private caseListSubject: BehaviorSubject<Case[]> = new BehaviorSubject<Case[]>([]);

  patientlist: any[] = [];

  constructor(
    private http: HttpserviceService,
    private patientservice: PatientService,
    private formValidation: FormvalidationService,
  ) {
    this.loadCases();
  }

  fetchCasesFromServer(): Observable<Case[]> {
    return this.http.httpGet2<ApiResponse<Case>>(
      `/PatientReg/GetPatient/${this.patientservice.getPatientId()}`
    ).pipe(
      tap(response => {
        console.log('API Response:', response);

        const newCase = response?.data;
        if (newCase) {
          const index = this.caseList.findIndex((c: Case) => c.patientId === newCase.patientId);

          if (index !== -1) {
            this.caseList[index] = { ...newCase }; // Overwrite existing case
          } else {
            this.caseList.push(newCase); // Add new case
          }

          this.caseListSubject.next([...this.caseList]);
          this.saveCases();
        }
      }),
      map(() => [...this.caseList])
    );
  }

  private loadCases(): void {
    this.http.httpGet(API_URLS.PATIENT_REG_GET).subscribe({
      next: (res: any) => {
        const patients = res?.data || [];
        this.patientlist = patients.sort((a: any, b: any) => {
          const nameA = a.name?.toLowerCase() || '';
          const nameB = b.name?.toLowerCase() || '';
          return nameA.localeCompare(nameB);
        });

        this.caseList = this.patientlist;
        this.caseListSubject.next([...this.caseList]);
        this.saveCases();
      },
      error: (err) => {
        this.formValidation.showAlert('Error loading case data', 'danger');
        console.error(err);
      }
    });
  }

  private saveCases(): void {
    localStorage.setItem('caseList', JSON.stringify(this.caseList));
  }

  getCases(): Observable<Case[]> {
    return this.caseListSubject.asObservable();
  }

  addCase(newCase: Case): void {
    this.caseList.push(newCase);
    this.saveCases();
    this.caseListSubject.next([...this.caseList]);
  }

  getCaseById(id: number): Case | undefined {
    console.log('this.caseList',this.caseList)
    return this.caseList.find((c: Case) => c.patientId === id);
  }

}
