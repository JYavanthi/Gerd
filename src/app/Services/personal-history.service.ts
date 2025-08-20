import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs/internal/Observable';
import * as XLSX from 'xlsx';
import { API_URLS } from '../shared/API-URLs';

@Injectable({
    providedIn: 'root'
})
export class PersonalHistoryService {

    private intakeStates: { [key: string]: boolean } = {
        aerated: false,
        coffee: false,
        tea: false,
        spicy: false,
        alcohol: false,
        sweets: false,
        smoking: false,
        tobacco: false
    };

    private formData: { [key: string]: { frequency: string; quantity: string; duration: string } } = {
        aerated: { frequency: '', quantity: '', duration: '' },
        coffee: { frequency: '', quantity: '', duration: '' },
        tea: { frequency: '', quantity: '', duration: '' },
        spicy: { frequency: '', quantity: '', duration: '' },
        alcohol: { frequency: '', quantity: '', duration: '' },
        sweets: { frequency: '', quantity: '', duration: '' },
        smoking: { frequency: '', quantity: '', duration: '' },
        tobacco: { frequency: '', quantity: '', duration: '' }
    };

     constructor(private http: HttpClient) {}
   private baseUrl = API_URLS.BASE_URL;
   

    setIntakeStates(states: { [key: string]: boolean }) {
        this.intakeStates = { ...states };
        sessionStorage.setItem('intakeStates', JSON.stringify(this.intakeStates));
    }

    setFormData(data: { [key: string]: { frequency: string; quantity: string; duration: string } }) {
        this.formData = { ...data };
        sessionStorage.setItem('formData', JSON.stringify(this.formData));
    }

    getIntakeStates(): { [key: string]: boolean } {
        const saved = sessionStorage.getItem('intakeStates');
        return saved ? JSON.parse(saved) : this.intakeStates;
    }

    getFormData(): { [key: string]: { frequency: string; quantity: string; duration: string } } {
        const saved = sessionStorage.getItem('formData');
        return saved ? JSON.parse(saved) : this.formData;
    }

    exportToExcel(): void {
        const intakeStates = this.getIntakeStates();
        const formData = this.getFormData();

        const exportData = Object.keys(formData).map(key => ({
            Item: key,
            Intake: intakeStates[key] ? 'Yes' : 'No',
            Frequency: formData[key].frequency,
            Quantity: formData[key].quantity,
            Duration: formData[key].duration
        }));

        const worksheet: XLSX.WorkSheet = XLSX.utils.json_to_sheet(exportData);
        const workbook: XLSX.WorkBook = { Sheets: { 'Intake Data': worksheet }, SheetNames: ['Intake Data'] };
        XLSX.writeFile(workbook, 'PersonalHistoryData.xlsx');
    }


      getComorbiditiesById(patientId: number, stage: number): Observable<any> {
return this.http.get(`${this.baseUrl}/Comorbidities/GetComorbditiesById/${patientId}/${stage}`);  }

    
}
