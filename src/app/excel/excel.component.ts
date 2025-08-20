import { Component } from '@angular/core';
import * as XLSX from 'xlsx';
import { saveAs } from 'file-saver';
import { PatientService } from '../Services/patient.service';
import { ChiefComplaintService } from '../Services/chief-complaint.service';
import { ComorbiditiesService } from '../Services/comorbidities.service'; // ✅ Add this
import { PersonalHistoryService } from '../Services/personal-history.service';

@Component({
  selector: 'app-excel',
  templateUrl: './excel.component.html',
  styleUrls: ['./excel.component.css']
})
export class ExcelComponent {

  constructor(
    private patientService: PatientService,
    private chiefComplaintService: ChiefComplaintService,
    private comorbiditiesService: ComorbiditiesService, // ✅ Inject service
    private personalHistoryService: PersonalHistoryService,
  ) { }

  exportToExcel(): void {
    console.log('🔍 Attempting to export demographic data...');

    let data = this.patientService.getDemographicData();
    console.log('📦 Data from PatientService:', data);

    if (!data) {
      console.warn('⚠️ No data found in PatientService. Checking localStorage...');
      const stored = localStorage.getItem('demographicData');
      if (stored) {
        data = JSON.parse(stored);
        console.log('📦 Data loaded from localStorage:', data);
      } else {
        console.error('❌ No data found in localStorage either.');
      }
    }

    if (!data) {
      alert('No demographic data available to export.');
      return;
    }

    const formattedData = [{
      'Patient Name': data.patientName || '',
      'Initial': data.initial || '',
      'Subject Number': data.subjectNumber || '',
      'Date': data.date || '',
      'Age': data.age || '',
      'DOB': data.dob || '',
      'Gender': data.gender || '',
      'Education': data.education || '',
      'Occupation': data.occupation || '',
      'State': data.state || '',
      'City': data.city || '',
      'Pincode': data.pincode || '',
      'Place Type': data.placeType || '',
      'Socioeconomic Status': data.socioeconomic || '',
      'Annual Family Income': data.annualFamilyIncome || '',
      'Past History': data.pastHistory || '',
      'Diet': data.diet || ''
    }];

    console.log('📝 Formatted data for Excel export:', formattedData);

    const worksheet: XLSX.WorkSheet = XLSX.utils.json_to_sheet(formattedData);
    const workbook: XLSX.WorkBook = {
      Sheets: { 'Demographic Info': worksheet },
      SheetNames: ['Demographic Info']
    };

    const excelBuffer: any = XLSX.write(workbook, { bookType: 'xlsx', type: 'array' });

    const blob: Blob = new Blob([excelBuffer], {
      type: 'application/vnd.openxmlformats-officedocument.spreadsheetml.sheet'
    });

    console.log('✅ Excel file ready to be saved...');
    saveAs(blob, 'DemographicData.xlsx');
    console.log('💾 Excel file download triggered.');
  }

  //--------------------------------------------------------------------------

  exportChiefComplaintToExcel(): void {
    let patientId = this.patientService.getPatientId();

    if (!patientId) {
      const stored = localStorage.getItem('patientId');
      if (stored) {
        patientId = +stored;
        this.patientService.setPatientId(patientId);
        console.log('🔁 Patient ID restored from localStorage:', patientId);
      }
    }

    if (!patientId) {
      alert('❌ Patient ID is missing.');
      return;
    }

    console.log('🔍 Fetching chief complaint data for patientId:', patientId);

    this.chiefComplaintService.getChiefComplaintByPatientId(patientId).subscribe({
      next: (res: any) => {
        if (res?.type === 'S' && res?.data) {
          const data = res.data;
          console.log('✅ Chief Complaint Data:', data);

          const formattedData = [{
            'Heartburn Duration': data.hbDuration || '',
            'Heartburn Frequency': data.hbFrequency || '',
            'Postural Heartburn': data.hbPostural || '',
            'Nocturnal Heartburn': data.hbNocturnal || '',

            'Regurgitation Duration': data.rDuration || '',
            'Regurgitation Frequency': data.rFrequency || '',
            'Postural Regurgitation': data.rPostural || '',
            'Nocturnal Regurgitation': data.rNocturnal || '',

            'Pain Duration': data.rpDuration || '',
            'Pain Frequency': data.rpFrequency || '',
            'Postural Pain': data.rpPostural || '',
            'Nocturnal Pain': data.rpNocturnal || '',

            'Acid Taste Duration': data.atDuration || '',
            'Acid Taste Frequency': data.atFrequency || '',
            'Postural Acid Taste': data.atPostural || '',
            'Nocturnal Acid Taste': data.atNocturnal || ''
          }];

          const worksheet: XLSX.WorkSheet = XLSX.utils.json_to_sheet(formattedData);
          const workbook: XLSX.WorkBook = {
            Sheets: { 'Chief Complaint Info': worksheet },
            SheetNames: ['Chief Complaint Info']
          };

          const excelBuffer: any = XLSX.write(workbook, { bookType: 'xlsx', type: 'array' });
          const blob: Blob = new Blob([excelBuffer], {
            type: 'application/vnd.openxmlformats-officedocument.spreadsheetml.sheet'
          });

          console.log('✅ Chief Complaint Excel ready.');
          saveAs(blob, 'ChiefComplaintData.xlsx');
          console.log('💾 Chief Complaint Excel downloaded.');
        } else {
          alert('⚠️ No chief complaint data found for this patient.');
        }
      },
      error: (err) => {
        console.error('❌ Error fetching chief complaint:', err);
        alert('Error fetching chief complaint data.');
      }
    });
  }

  //------------------------------------------------------------------------

  exportcomorbiditiesToExcel(): void {
    console.log('🔍 Attempting to export comorbidities data...');

    this.comorbiditiesService.getComorbidities().subscribe({
      next: (response: any) => {
        if (response.type === 'S' && response.data?.length > 0) {
          const data = response.data;
          console.log('📦 Data from ComorbiditiesService:', data);

          const formattedData = data.map((item: any) => ({
            'Patient ID': item.patientID || '',
            'Doctor ID': item.doctorID || '',
            'Hypertension': item.HT_Present || '',
            'Hypertension Remark': item.HT_Remark || '',
            'Diabetes': item.DB_Present || '',
            'Diabetes Remark': item.DB_Remark || '',
            'Dyslipidemia': item.DD_Present || '',
            'Dyslipidemia Remark': item.DD_Remark || '',
            'Liver Disease': item.CLD_Present || '',
            'Liver Remark': item.CLD_Remark || '',
            'Neurological Disorder': item.ND_Present || '',
            'Neuro Remark': item.ND_Remark || '',
            'Cardiovascular': item.CD_Present || '',
            'Cardio Remark': item.CD_Remark || '',
            'Hypothyroidism': item.H_Present || '',
            'Hypo Remark': item.H_Remark || '',
            'Hyperthyroidism': item.HTD_Present || '',
            'Hyper Remark': item.HTD_Remark || '',
            'Behavioural Disorder': item.BD_Present || '',
            'Behavioural Remark': item.BD_Remark || '',
            'Kidney Disease': item.CKD_Present || '',
            'Kidney Remark': item.CKD_Remark || '',
            'Asthma': item.A_Present || '',
            'Asthma Remark': item.A_Remark || '',
            'Osteoarthritis': item.O_Present || '',
            'Osteo Remark': item.O_Remark || '',
            'Rheumatoid Arthritis': item.RA_Present || '',
            'RA Remark': item.RA_Remark || '',
            'Sclerosis': item.SS_Present || '',
            'Sclerosis Remark': item.SS_Remark || '',
            'Cancer': item.C_Present || '',
            'Cancer Remark': item.C_Remark || '',
            'Others': item.CMO_Present || '',
            'Other Remarks': item.CMO_Remark || ''
          }));

          console.log('📝 Formatted data for Excel export:', formattedData);

          const worksheet: XLSX.WorkSheet = XLSX.utils.json_to_sheet(formattedData);
          const workbook: XLSX.WorkBook = {
            Sheets: { 'Comorbidities Data': worksheet },
            SheetNames: ['Comorbidities Data']
          };

          const excelBuffer: any = XLSX.write(workbook, { bookType: 'xlsx', type: 'array' });

          const blob: Blob = new Blob([excelBuffer], {
            type: 'application/vnd.openxmlformats-officedocument.spreadsheetml.sheet'
          });

          console.log('✅ Excel file ready to be saved...');
          saveAs(blob, 'ComorbiditiesData.xlsx');
          console.log('💾 Excel file download triggered.');
        } else {
          console.error('❌ No comorbidities data received or response type invalid.');
          alert('No comorbidities data available to export.');
        }
      },
      error: (error: any) => {
        console.error('❌ Error fetching comorbidities data:', error);
        alert('Failed to fetch comorbidities data for export.');
      }
    });
  }

  //------------------------------------------------------------------------
  personalHistoryDownloadExcel() {
    this.personalHistoryService.exportToExcel();
  }
}
