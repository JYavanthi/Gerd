
import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders, HttpResponse } from "@angular/common/http";
import { map, Observable } from "rxjs";
import { API_URLS } from "../shared/API-URLs";


@Injectable({
    providedIn: 'root'
})
export class ReportService {
    constructor(private http: HttpClient) { }
    private baseUrl = API_URLS.BASE_URL;

    // added baseline report
    // Download Excel for Patients Visit
    downloadFullPatientsReportExcel(): Observable<Blob> {
        return this.http.get(`${this.baseUrl}/Report/DownloadFullReport`, {
            responseType: 'blob'
        });
    }

    // Get Completed Baseline Cases Count
    // getCompletedCasesCount(): Observable<{ type: string; data: any[] }> {
    //     return this.http.get<{ type: string; data: any[] }>(
    //         `${this.baseUrl}/Report/GetCompletedCasesCount`
    //     );
    // }

    // idu baseline download part
    downloadBaselineReportExcel(): Observable<Blob> {
        return this.http.get(`${this.baseUrl}/BaselineReport/DownloadBaselineReport`, {
            responseType: "blob"
        })
    }
    getBaselineReportCount(): Observable<any> {
        return this.http.get(`${this.baseUrl}/BaselineReport/DownloadBaselineReport`);
    }


    // idu followup1 part
    downloadFollowUp1ReportExcel(): Observable<Blob> {
        return this.http.get(`${this.baseUrl}/FollowUp1Report/DownloadFollowUp1Report`, {
            responseType: "blob"
        })
    }

    downloadFollowUp2ReportExcel(): Observable<Blob> {
        return this.http.get(`${this.baseUrl}/FollowUp2Report/DownloadFollowUp2Report`, {
            responseType: "blob"
        })
    }

    downloadCompletedCasesReportExcel(): Observable<Blob> {
        return this.http.get(`${this.baseUrl}/CompletedReport/DownloadCompletedReport`, {
            responseType: 'blob'
        });
    }
    

    // Get count for Completed Cases
    // getCompletedCasesReportCount(): Observable<{ type: string; data: number }> {
    //     return this.http.get<{ type: string; data: number }>(
    //         `${this.baseUrl}/CompletedReport/DownloadCompletedReport`
    //     );
    // }

    // downloadInCompletedCasesReportExcel(): Observable<Blob> {
    //     return this.http.get(`${this.baseUrl}/InCompletedReport/DownloadInCompletedReport`, {
    //         //observe: 'response', // so we can get headers like fetch
    //         responseType: 'blob', // type assertion to make TypeScript happy
    //         headers: new HttpHeaders({
    //             'Accept': 'application/vnd.openxmlformats-officedocument.spreadsheetml.sheet'
    //         })
    //     });
    // }
    downloadInCompletedCasesReportExcel(): Observable<Blob> {
        return this.http.get(`${this.baseUrl}/IncompleteReport/DownloadIncompleteCases`, {
            responseType: 'blob'
        });
    }
}


