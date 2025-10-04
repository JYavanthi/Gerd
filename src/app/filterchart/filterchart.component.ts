import { HttpClient } from '@angular/common/http';
import { Component } from '@angular/core';
import { Router } from '@angular/router';
import { ChartOptions, ChartType } from 'chart.js';
import { API_URLS } from '../shared/API-URLs';


interface Patient {
  age: number;
  [key: string]: any; // allows extra fields like gender, occupation, etc.
}

interface ChiefComplaint {
  hbPostural: string;
  hbNocturnal: string;
  rPostural: string;
  rNocturnal: string;
  rpPostural: string;
  rpNocturnal: string;
  atPostural: string;
  atNocturnal: string;
  [key: string]: any; // optional extra fields
}

@Component({
  selector: 'app-filterchart',
  templateUrl: './filterchart.component.html',
  styleUrls: ['./filterchart.component.css']
})



export class FilterchartComponent {

  dropdownData: { [key: string]: string[] } = {
    "Age": ["18-30", "31-40", "41-50", "51-60", "61-70", "71-80", ">80"],
    "Gender": ["Male", "Female", "Others"],
    "Education": ["Above Tenth standard", "Below Tenth standard"],
    "Occupation": ["Sedentary", "Non sedentary"],
    "Place Type": ["urban", "sub urban", "rural"],
    "Socioeconomic Status": ["Above poverty line", "Below poverty line"],
    "Annual Family Income (Rupees)": ["< 1 lakh", "1-5 lakhs", "> 5 lakhs"],
    "Chief complaints": ["Heartburn", "Regurgitation", "Retrosternal Pain", "Acid Taste in mouth"],
    "Heartburn": ["Postural", "Nocturnal"],
    "Regurgitation": ["Postural", "Nocturnal"],
    "Retrosternal Pain": ["Postural", "Nocturnal"],
    "Acid Taste in mouth": ["Postural", "Nocturnal"],
    "COMORBIDITIES": ["Hypertension", "Diabetes", "Dyslipidemia", "Chronic liver disease", "Neurological Disorder", "Cardiovascular disorders", "Hypothyroidism", "Hyperthyroidism", "Behavioural disorders", "Chronic kidney disease", "Asthma", "Osteoarthritis", "Rheumatoid arthritis", "Systemic Sclerosis", "Cancer", "Others"],
    "Diet": ["Vegetarian", "Non-Vegetarian"],
    "Patient Personal History": ["Aerated Drinks", "Coffee", "Tea", "Spicy food", "Alcohol", "Chocolates/ sweets", "Smoking (cigarettes/day)", "Tobacco (other forms/day)"],
    "Sleep Apnea": ["Yes", "No"],
    "Exercise": ["Walking", "Jogging", "Gym", "Yoga", "Aerobics", "Zumba", "Others"],
    "Computer Use": ["Yes", "No"],
    "Computer Usage (hrs/day)": ["<1 hr", "<2 hrs", "3-4 hrs", "4-6 hrs", "6-8 hrs", "8-10 hrs"],
    "Computer Usage Duration (years)": ["<1 year", "2-5 years", "6-10 years", "11-15 years", "16-20 years", ">20 years"],
    "Smartphone Use": ["Yes", "No"],
    "Smartphone Usage (hrs/day)": ["<1 hr", "<2 hrs", "3-4 hrs", "4-6 hrs", "6-8 hrs", "8-10 hrs"],
    "Smartphone Usage Duration (years)": ["<1 year", "2-5 years", "6-10 years", "11-15 years", "16-20 years", ">20 years"],
    "Working Hours (Occupation)": ["4.00 am to 12.00 noon (Early Morning shift)", "6.00 am to 3.00 pm (Morning shift)", "9.00 am to 6.00 pm (General shift)", "12.00 noon to 8.00 pm (Afternoon shift)", "8.00 pm to 8.00 am (Night shift)"],
    "Job/ Occupation type": ["Sedentary", "Non sedentary"],
    "Duration (No. of years in the above working hours)": ["<1 year", "2-5 years", "6-10 years", "11-15 years", "16-20 years", ">20 years"],
    "Family History of GERD": ["Yes", "No"],
    "Family History of Esophago-gastric Cancer": ["Yes", "No"],
    "PPI Usage": ["Yes", "No"],
    "History of Endoscopy": ["Yes", "No"],
    "History of Gastro-surgery": ["Yes", "No"],
    "Bariatric Surgery": ["Yes", "No"],
    "Fundoplication Surgery": ["Yes", "No"],
    "Gastric POEM Surgery": ["Yes", "No"],
    "Gastrojejunostomy": ["Yes", "No"],
    "Other Gastro Surgery": ["Yes", "No"],
    "Current Medications": ["PPI", "Combination of PPI + Prokinetics", "Sucralfate", "Alginate", "H₂ Blockers", "H₂ Blockers combinations", "PCAB", "Any others"],
    "Los Angeles Grade": ["Grade A", "Grade B", "Grade C", "Grade D"],
    "Hill’s classification Grade": ["Grade 1", "Grade 2", "Grade 3", "Grade 4"],
    "Newly Diagnosed": ["Yes", "No"],
    "Newly Diagnosed (Gender)": ["Male", "Female"],
    "Known case of GERD": ["Yes", "No"],
    "Known case of GERD (Gender)": ["Male", "Female"],
    "GERDType": ["Erosive GERD", "Non-Erosive GERD"],
    "RefractorytoPPI": ["Yes", "No"],
    "AdherencetoTherapy": ["Yes", "No"],
    "Lifestyle Recommendations": ["Diet modification", "Moderation of alcohol", "Weight loss", "Regular exercise", "Stop Tobacco use"],
    "Drug Therapy Advised": ["PPI", "Combination of PPI + Prokinetics", "Sucralfate", "Alginate", "H₂ Blockers", "H₂ Blockers combinations", "PCAB", "Any others"]
  };

  dropdownKeys = Object.keys(this.dropdownData);
  openDropdowns: { [key: string]: boolean } = { 'main': true };
  secondLevelKey: string | null = 'Age';
  currentDropdownLabel = 'Select Filter';
  selectedValues: { [key: string]: string } = {};

  pieChartData: any = {
    labels: [],
    datasets: [{ data: [], backgroundColor: [], hoverBackgroundColor: [] }]
  };

  pieChartOptions: ChartOptions = {
    responsive: true,
    plugins: { legend: { position: 'bottom' } }
  };

  pieChartType: ChartType = 'pie';

  constructor(private router: Router, private http: HttpClient) { }

  ngOnInit(): void {
    this.loadPieChart('Age');
  }

  toggleDropdown(key: string) {
    this.openDropdowns[key] = !this.openDropdowns[key];
    if (!this.openDropdowns[key]) this.secondLevelKey = null;
  }
  openNextLevel(key: string) {
    this.secondLevelKey = key;

    if (key === 'Chief complaints') {
      this.loadChiefComplaintsPieChart();
    } else {
      this.loadPieChart(key);
    }
  }


  selectValue(key: string, value: string) {
    this.selectedValues[key] = value;
    this.currentDropdownLabel = `${key}: ${value}`;
    this.secondLevelKey = null;
    this.openDropdowns['main'] = false;

    if (key === 'Chief complaints' || key === 'Heartburn' || key === 'Regurgitation' || key === 'Retrosternal Pain' || key === 'Acid Taste in mouth') {
      this.loadChiefComplaintsPieChart(value);
    } else {
      this.loadPieChart(key, value);
    }
  }


  loadChiefComplaintsPieChart(selectedValue?: string) {
    this.http.get<any>(`${API_URLS.BASE_URL}${API_URLS.CHEIF_COMPLAINT_GET}`).subscribe({
      next: (res) => {
        const complaints: ChiefComplaint[] = res?.data || [];

        if (!complaints.length) {
          alert('No chief complaints data available.');
          this.pieChartData = { labels: [], datasets: [{ data: [], backgroundColor: [], hoverBackgroundColor: [] }] };
          return;
        }

        // Define all possible counts
        const counts: { [key: string]: number } = {
          "Heartburn - Postural": 0,
          "Heartburn - Nocturnal": 0,
          "Regurgitation - Postural": 0,
          "Regurgitation - Nocturnal": 0,
          "Retrosternal Pain - Postural": 0,
          "Retrosternal Pain - Nocturnal": 0,
          "Acid Taste in mouth - Postural": 0,
          "Acid Taste in mouth - Nocturnal": 0
        };

        complaints.forEach((c: ChiefComplaint) => {
          // Count based on selectedValue
          if (!selectedValue || selectedValue === "Heartburn") {
            if (c.hbPostural === "Yes") counts["Heartburn - Postural"]++;
            if (c.hbNocturnal === "Yes") counts["Heartburn - Nocturnal"]++;
          }
          if (!selectedValue || selectedValue === "Regurgitation") {
            if (c.rPostural === "Yes") counts["Regurgitation - Postural"]++;
            if (c.rNocturnal === "Yes") counts["Regurgitation - Nocturnal"]++;
          }
          if (!selectedValue || selectedValue === "Retrosternal Pain") {
            if (c.rpPostural === "Yes") counts["Retrosternal Pain - Postural"]++;
            if (c.rpNocturnal === "Yes") counts["Retrosternal Pain - Nocturnal"]++;
          }
          if (!selectedValue || selectedValue === "Acid Taste in mouth") {
            if (c.atPostural === "Yes") counts["Acid Taste in mouth - Postural"]++;
            if (c.atNocturnal === "Yes") counts["Acid Taste in mouth - Nocturnal"]++;
          }
        });

        let labels: string[], data: number[];
        if (selectedValue && selectedValue !== "Chief complaints") {
          labels = Object.keys(counts).filter(k => k.startsWith(selectedValue) && counts[k] > 0);
          data = labels.map(l => counts[l]);
        } else {
          labels = Object.keys(counts).filter(k => counts[k] > 0);
          data = labels.map(l => counts[l]);
        }

        const colors = labels.map(() => this.getRandomColor());

        if (!data.length) {
          alert('No data found for selected chief complaint.');
        }

        this.pieChartData = {
          labels,
          datasets: [{ data, backgroundColor: colors, hoverBackgroundColor: colors }]
        };
      },
      error: (err) => {
        console.error('Error fetching chief complaints:', err);
        alert('Error loading chief complaints data.');
      }
    });
  }


  private matchAgeRange(age: Number, range: string): boolean {
    if (range.includes('-')) {
      let [min, max] = range.split('-').map(Number);
      if (min > max) [min, max] = [max, min]; // swap if reversed
    } else if (range.startsWith('>')) {
    }
    return false;
  }


  loadPieChart(filterKey: string, selectedValue?: string) {
    this.http.get<any>(`${API_URLS.BASE_URL}${API_URLS.PATIENT_REG_GET}`).subscribe({
      next: (res) => {
        const patients: Patient[] = res?.data || [];

        if (!patients.length) {
          alert('No patient data available.');
          this.pieChartData = { labels: [], datasets: [{ data: [], backgroundColor: [], hoverBackgroundColor: [] }] };
          return;
        }

        const counts: { [key: string]: number } = {};
        const options = this.dropdownData[filterKey] || [];

        // Map display text to API value if needed
        const valueMap: { [key: string]: { [key: string]: string } } = {
          Education: {
            "Above Tenth standard": "10th Std & Above",
            "Below Tenth standard": "Below 10th"
          },
          Occupation: {
            "Sedentary": "Sedentary",
            "Non-Sedentary": "Non-Sedentary"
          },
          ' Socioeconomic Status': {
            "Above poverty line": 'Above Poverty Line',
            "Below poverty line": 'Below Poverty Line',
          },
          "Place Type": {
            "Urban": "Urban",
            "Sub Urban": "Sub Urban",
            "Rural": "rural"
          },
          "Annual Family Income (Rupees)": {
            "Less than 1 Lakh": "Less than 1 Lakh",
            "1-5 Lakhs": "1-5 Lakhs",
            "Greater Than 5 Lakhs": "Greater Than 5 Lakhs"
          }
        };

        const fieldMap: { [key: string]: string } = {
          "Place Type": "placeType",
          "Annual Family Income (Rupees)": "familyIncome",
          Education: "education",
          Occupation: "occupation",
          'Socioeconomic Status': "socioeconomicStatus"
        };
        const field = fieldMap[filterKey] || filterKey;

        if (filterKey === 'Age') {
          options.forEach(range => {
            let filteredPatients: Patient[] = [];
            if (range.includes('-')) {
              const [min, max] = range.split('-').map(Number);
              filteredPatients = patients.filter(p => p.age >= min && p.age <= max);
            } else if (range.startsWith('>')) {
              const threshold = Number(range.replace('>', '').trim());
              filteredPatients = patients.filter(p => p.age > threshold);
            }
            if (!selectedValue || selectedValue === range) counts[range] = filteredPatients.length;
          });
        } else {
          options.forEach(option => {
            const actualOption = valueMap[filterKey]?.[option] || option;

            const count = patients.reduce((acc, p) => {
              let val = p[field];
              if (val === undefined || val === null) return acc;

              if (Array.isArray(val)) {
                return acc + (val.some(v => v.toString().toLowerCase() === actualOption.toLowerCase()) ? 1 : 0);
              } else {
                return acc + (val.toString().toLowerCase() === actualOption.toLowerCase() ? 1 : 0);
              }
            }, 0);

            if (!selectedValue || selectedValue.toLowerCase() === option.toLowerCase()) {
              counts[option] = count;
            }
          });
        }

        const labels = Object.keys(counts);
        const data = Object.values(counts);
        const colors = labels.map(() => this.getRandomColor());

        this.pieChartData = {
          labels,
          datasets: [{ data, backgroundColor: colors, hoverBackgroundColor: colors }]
        };

        if (!data.some(d => d > 0)) {
          alert('No data found for selected filter.');
        }
      },
      error: (err) => {
        console.error('Error fetching data:', err);
        alert('Error loading chart data.');
      }
    });
  }




  getRandomColor(): string {
    const r = Math.floor(Math.random() * 200 + 50);
    const g = Math.floor(Math.random() * 200 + 50);
    const b = Math.floor(Math.random() * 200 + 50);
    return `rgb(${r},${g},${b})`;
  }

  login() { this.router.navigate(['/login']); }
  goToCoMorbiditiesReport() { this.router.navigate(['/CoMorbiditiesReport']); }
  goTotreatmentReport() { this.router.navigate(['/treatmentReport']); }
  goDoctorlist() { this.router.navigate(['/doctor-list']); }
  goReport() { this.router.navigate(['/genderReport']); }
}