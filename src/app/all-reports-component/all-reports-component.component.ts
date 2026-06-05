import { Component, HostListener } from '@angular/core';
import { API_URLS } from '../shared/API-URLs';
import { HttpserviceService } from '../httpservice.service';
import { ChartData, ChartOptions, ChartType } from 'chart.js';
import { HttpClient } from '@angular/common/http';
import { Router } from '@angular/router';
import { Case, CaseDataService } from '../Services/case-data.services';
import { forkJoin, Subscription } from 'rxjs';
import * as XLSX from 'xlsx';
import { saveAs } from 'file-saver';

type DropdownData = {
  [key: string]: string[];
};

interface State {
  id: number;
  name: string;
}

interface City {
  id: number;
  name: string;
  stateName: string;
}

@Component({
  selector: 'app-all-reports-component',
  templateUrl: './all-reports-component.component.html',
  styleUrls: ['./all-reports-component.component.css']

})

export class AllReportsComponentComponent {
  private pushStateCount = 5;
  selectedValue: 'Yes' | 'No' | null = null;
  age: number = 0;
  caseSub!: Subscription;
  tableData: any[] = [];
  doctorlist: any[] = [];
  // patient: any[] = [];
  isViewMode = false;
  Math = Math;
  currentPage = 1;
  itemsPerPage = 1000;
  totalItems = 0;
  paginatedData: Case[] | null = null;
  totalPages = 0;
  pageNumbers: number[] = [];
  // selectedState: string = '';
  userData: any;
  patient: any;
  stage: number = 0;
  patientId: number = 0;
  totalCases = 0;
  completedCases = 0;
  incompleteCases = 0;
  pendingCases = 0;
  maleCount = 0;
  femaleCount = 0;
  otherCount = 0;
  baseline: any;
  followup1: any;
  followup2: any;
  allRecords: any[] = [];

  originalData: any[] = [];
  filteredData: any[] = [];
  selectedCategory: string | null = null;
  selectedOption: string | null = null;
  availableOptions: string[] = [];


  data: DropdownData = {
    "Age": ["18-30", "31-40", "41-50", "51-60", "61-70", "71-80", ">80"],
    "Education": ["Above Tenth standard", "Below Tenth standard"],
    "Occupation": ["Sedentary", "Non sedentary"],
    "Place Type": ["urban", "sub urban", "rural"],
    "Socioeconomic Status": ["Above poverty line", "Below poverty line"],
    "Annual Family Income (Rupees)": ["< 1 lakh", "1-5 lakhs", "> 5 lakhs"],
    // "Chief complaints": ["Heartburn", "Regurgitation", "Retrosternal Pain", "Acid Taste in mouth"],
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
    "Computer Usage (hrs/day)": ["0-1", "1-2", "2-3", "3-4", "4-6", "6-8", "8-10", "10-24"],
    "Computer Usage Duration (years)": ["0-1", "1-5", "5-10", "10-15", "15-20", "20-120"],
    "Smartphone Use": ["Yes", "No"],
    "Smartphone Usage (hrs/day)": ["0-1", "1-2", "2-3", "3-4", "4-6", "6-8", "8-10", "10-24"],
    "Smartphone Usage Duration (years)": ["0-1", "1-5", "5-10", "10-15", "15-20", "20-120"],
    "Working Hours (Occupation)": ["4.00 am to 12.00 noon (Early Morning shift)", "6.00 am to 3.00 pm (Morning shift)", "9.00 am to 6.00 pm (General shift)", "12.00 noon to 8.00 pm (Afternoon shift)", "8.00 pm to 8.00 am (Night shift)"],
    "Job/ Occupation type": ["Sedentary", "Non sedentary"],
    "Duration (No. of years in the above working hours)": ["0-1", "1-5", "5-10", "10-15", "15-20", "20-120"],
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
    // "Newly Diagnosed (Gender)": ["Male", "Female"],
    "Known case of GERD": ["Yes", "No"],
    // "Known case of GERD (Gender)": ["Male", "Female"],
    "GERDType": ["Erosive GERD", "Non-Erosive GERD"],
    "RefractorytoPPI": ["Yes", "No"],
    "AdherencetoTherapy": ["Yes", "No"],
    "Lifestyle Recommendations": ["Diet modification", "Moderation of alcohol", "Weight loss", "Regular exercise", "Stop Tobacco use"],
    "Drug Therapy Advised": ["PPI", "Combination of PPI + Prokinetics", "Sucralfate", "Alginate", "H₂ Blockers", "H₂ Blockers combinations", "PCAB", "Any others"]
  };
  mainKeys = Object.keys(this.data);


  showCategoryDropdown = true;
  showOptionDropdown = false;

  zones: string[] = ["North", "South", "East", "West"];
  genders: string[] = ["Male", "Female", "Others"];
  Stages: string[] = ["baseline", "FollowUp1", "FollowUp2"];

  stageMapping: Record<string, number[]> = {
    "baseline": [0, 1],
    "FollowUp1": [2, 3],
    "FollowUp2": [4, 5]
  };

  states: State[] = [];
  cities: City[] = [];

  selectedZone: string = '';
  selectedState: State | null = null;
  selectedCity: City | null = null;
  selectedGender: string = '';
  zoneMatch: string[] = [];
  selectedStage: string = '';

  pieChartType: 'pie' = 'pie';
  pieChartData: ChartData<'pie', number[], string | string[]> = {
    labels: [''],
    datasets: [
      {
        data: [],
        backgroundColor: ['#FF6384', '#36A2EB', '#FFCE56']
      }
    ]
  };
  pieChartOptions: ChartOptions<'pie'> = {
    responsive: true,
    plugins: {
      legend: { position: 'top' }
    }
  };

  barChartType: 'bar' = 'bar';
  barChartData: ChartData<'bar', number[], string | string[]> = {
    labels: [''],
    datasets: [
      { label: '', data: [], backgroundColor: '#36A2EB' }
    ]
  };
  barChartOptions: ChartOptions<'bar'> = {
    responsive: true,
    maintainAspectRatio: false,
    scales: {
      y: { beginAtZero: true }
    },
    plugins: { legend: { position: 'top' } },
    datasets: {
      bar: {
        barThickness: 30 // adjust the value as needed
      }
    }
  };

  constructor(private https: HttpClient, private http: HttpserviceService, private router: Router, private caseDataService: CaseDataService
  ) { }

  private routerSub!: Subscription;


  ngOnInit() {
    this.selectedCategory = null;
    this.selectedOption = null;
    this.availableOptions = this.data[''] || [];

    this.paginatedData = [];

    this.getStates();
    this.getCities();

    this.loadPiechar('');

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
        history.pushState({ antiBack: true }, '', window.location.href);
        history.pushState({ antiBack: true }, '', window.location.href);
      } catch (e) {
        console.warn('pushState failed', e);
      }
    }, 50);

    window.scrollTo(0, 0);
  }

  @HostListener('window:beforeunload', ['$event'])
  onBeforeUnload(event: BeforeUnloadEvent) {
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



  getStage(c: Case): number {
    const bl = Number(c['blsubmitted'] ?? 0);
    const fu1 = Number(c['fu1submitted'] ?? 0);
    const fu2 = Number(c['fu2submitted'] ?? 0);

    if (fu2 === 1) return 5;
    if (fu1 === 1) return 3;
    if (bl === 1) return 1;
    return 0;
  }

  // onCategorySelect(category: string) {
  //   this.selectedCategory = category;
  //   this.availableOptions = this.data[category];
  //   this.selectedOption = null;
  //   this.showOptionDropdown = true;

  // }

  onCategorySelect(category: string) {
  this.selectedCategory = category;
  this.availableOptions = this.data[category] || [];
  this.selectedOption = ''; // not mandatory
  this.showOptionDropdown = true;
}
  filterTableData() {
    this.paginatedData = this.tableData.filter(row => {
      let matches = true;
      if (this.selectedCategory) {
        matches = matches && row[this.selectedCategory] === this.selectedOption;
      }
      return matches;
    });
  }

  onOptionSelect(option: string) {
    this.selectedOption = option;
    console.log(`Selected: ${this.selectedCategory} → ${option}`);
  }

  toggleSelection(item: any, type: string) {
    switch (type) {
      case 'zone':
        this.selectedZone = item;
        this.selectedState = null;
        this.selectedCity = null;
        this.getStates();
        this.cities = [];
        break;
      case 'state':
        this.selectedState = item;
        this.selectedCity = null;
        this.getCities();
        break;
      case 'city':
        this.selectedCity = item;
        break;
      case 'gender':
        this.selectedGender = item;
        break;
      case 'stage':
        this.selectedStage = item;
        break;
    }
  }

  get selectedStateName() {
    return this.selectedState ? this.selectedState.name : '';
  }

  get selectedCityName() {
    return this.selectedCity ? this.selectedCity.name : '';
  }

  toggleDropdown(dropdown: string) {
    Object.keys(this.dropdownOpen).forEach(key => {
      if (key !== dropdown) this.dropdownOpen[key] = false;
    });
    this.dropdownOpen[dropdown] = !this.dropdownOpen[dropdown];
  }
  dropdownOpen: Record<string, boolean> = {
    'Category': false,
    'Option': false,
    Zones: false,
    States: false,
    Cities: false,
    Gender: false
  };

  getStates() {
    if (!this.selectedZone) {
      this.http.httpGet(API_URLS.STATE_GET).subscribe({
        next: (res: State[]) => {
          this.states = res;
        },
        error: (err) => {
          console.error('Error fetching states', err);
        }
      });
      return;
    }

    this.http.httpGet(API_URLS.STATE_GET).subscribe({
      next: (res: State[]) => {
        switch (this.selectedZone) {
          case 'North':
            this.states = res.filter(s =>
              [4007, 4015, 4016, 4020, 4021, 4022, 4029, 4031, 4040, 4852].includes(s.id)
            );
            break;

          case 'South':
            this.states = res.filter(s =>
              [4011, 4012, 4017, 4019, 4023, 4026, 4028, 4035].includes(s.id)
            );
            break;

          case 'East':
            this.states = res.filter(s =>
              [4006, 4010, 4013, 4018, 4024, 4025, 4027, 4034, 4036, 4037, 4038, 4853].includes(s.id)
            );
            break;

          case 'West':
            this.states = res.filter(s =>
              [4008, 4009, 4014, 4030, 4033, 4039].includes(s.id)
            );
            break;

          default:
            this.states = res;
        }
      },
      error: (err) => {
        console.error('Error fetching states', err);
      }
    });
  }

  selectCity(city: City) {
    this.selectedCity = city;
    this.dropdownOpen['Cities'] = false;
  }
  getCities() {
    if (!this.selectedState) {
      this.cities = [];
      return;
    }

    this.http.httpGet(API_URLS.CITY_GET_VIEW).subscribe({
      next: (res: City[]) => {
        this.cities = res.filter(c => c.stateName === this.selectedState?.name);
      },
      error: err => console.error('Error fetching cities', err)
    });
  }

  getPatientList() {
    this.caseSub = this.http.httpGet('/PatientReg/GetPatient').subscribe((res: any) => {
      if (res?.data && Array.isArray(res.data)) {
        this.tableData = res.data.sort((a: any, b: any) => new Date(b.date).getTime() - new Date(a.date).getTime());
        this.updatePagination();
      }
    });
  }


  updatePagination() {
    this.totalPages = Math.ceil(this.tableData.length / this.itemsPerPage);
    this.updatePaginatedData();
    this.generatePageNumbers();
  }

  updatePaginatedData() {
    const start = (this.currentPage - 1) * this.itemsPerPage;
    this.paginatedData = this.tableData.slice(start, start + this.itemsPerPage);
  }

  generatePageNumbers() {
    this.pageNumbers = [];
    const maxVisiblePages = 5;
    let start = Math.max(1, this.currentPage - Math.floor(maxVisiblePages / 2));
    let end = Math.min(this.totalPages, start + maxVisiblePages - 1);
    if (end - start + 1 < maxVisiblePages) start = Math.max(1, end - maxVisiblePages + 1);
    for (let i = start; i <= end; i++) this.pageNumbers.push(i);
  }



  navigateToAddCase() { this.router.navigate([`/demographic/0/0`]); }

  ngOnDestroy() { this.caseSub?.unsubscribe(); this.routerSub?.unsubscribe(); }


  downloadAllStages(patientID: any) {
    this.router.navigate([`/case-stage-view/${patientID}`]);
  }



  urlStr: string = '';
  patients: any[] = [];


  clearFilters() {
    this.selectedStage = '';
    this.selectedZone = '';
    this.selectedState = null;
    this.selectedCity = null;
    this.selectedGender = '';
    this.selectedStage = '';
    this.selectedOption = '';
    this.selectedCategory = '';
    this.pieChartData = { labels: [], datasets: [] };
    this.barChartData = { labels: [], datasets: [] };
    this.tableData = [];

    this.updatePagination();
  }


  loadPiechar(category: string, option?: string) {
    let apiUrl = '';

    switch (category) {
      case 'Age':
      case 'Education':
      case 'Occupation':
      case 'Place Type':
      case 'Socioeconomic Status':
      case 'Annual Family Income (Rupees)':
        apiUrl = API_URLS.PATIENT_REG_GET;
        break;

      case 'Chief complaints':
      case 'Heartburn':
      case 'Regurgitation':
      case 'Retrosternal Pain':
      case 'Acid Taste in mouth':
        apiUrl = API_URLS.CHEIF_COMPLAINT_GET;
        break;

      case 'COMORBIDITIES':
        apiUrl = API_URLS.COMORBIDITIES_GET;
        break;

      case 'Diet':
        apiUrl = API_URLS.HISTORYGET;
        break;


      case 'Patient Personal History':
        apiUrl = API_URLS.PERSONAL_HISTORY_GET;
        break;

      case 'Sleep Apnea':
      case 'Exercise':
        apiUrl = API_URLS.SLEEP_GET;
        break;

      case 'Computer Use':
      case 'Computer Usage (hrs/day)':
      case 'Computer Usage Duration (years)':
      case 'Smartphone Use':
      case 'Smartphone Usage (hrs/day)':
      case 'Smartphone Usage Duration (years)':
      case 'Working Hours (Occupation)':
      case 'Job/ Occupation type':
      case 'Duration (No. of years in the above working hours)':
        apiUrl = API_URLS.GADGET_GET;
        break;

      case 'Family History of GERD':
      case 'Family History of Esophago-gastric Cancer':
      case 'PPI Usage':
        apiUrl = API_URLS.FAMILY_HISTORY_GET;
        break;


      case 'History of Endoscopy':
      case 'History of Gastro-surgery':
      case 'Bariatric Surgery':
      case 'Fundoplication Surgery':
      case 'Gastric POEM Surgery':
      case 'Gastrojejunostomy':
      case 'Other Gastro Surgery':
        apiUrl = API_URLS.GERD_HISTORY_GET;
        break;

      case 'Current Medications':
        apiUrl = API_URLS.CURRENT_MEDICATION_GET;
        break;

      case 'Los Angeles Grade':
      case 'Hill’s classification Grade':
        apiUrl = API_URLS.ASSISMENT;
        break;

      case 'Newly Diagnosed':
      case 'Newly Diagnosed (Gender)':
      case 'Known case of GERD':
      case 'Known case of GERD (Gender)':
      case 'GERDType':
      case 'RefractorytoPPI':
      case 'AdherencetoTherapy':
        apiUrl = API_URLS.DIAGNOSIS_GET_DOCTOR;
        break;


      case 'Lifestyle Recommendations':
      case 'Drug Therapy Advised':
        apiUrl = API_URLS.GET_MANAGEMENT;
        break;


      default:
        console.warn('No API for this category');
        return;
    }


    const fieldMap: Record<string, string> = {
      "PPI": "nsaidsMolecule",
      "Combination of PPI + Prokinetics": "bisphosphonatesMolecule",
      "Sucralfate": "steroidsMolecule",
      "Alginate": "antiplateletMolecule",
      "H₂ Blockers": "othersMolecule",
    };



    const SleepMap: Record<string, { yes: string; no: string }> = {
      'Exercise': { yes: 'exerciseIntakeyes', no: 'exerciseIntakeno' },
      'Jogging': { yes: 'joggingSelectedyes', no: 'joggingSelectedno' },
      'Gym': { yes: 'gymSelectedyes', no: 'gymSelectedno' },
      'Yoga': { yes: 'yogaSelectedyes', no: 'yogaSelectedno' },
      'Walking': { yes: 'walkingSelectedyes', no: 'walkingSelectedno' },
      'Aerobics': { yes: 'aerobicsyes', no: 'aerobicsno' },
      'Zumba': { yes: 'zumbayes', no: 'zumbano' },
      'Others': { yes: 'othersyes', no: 'othersno' }
    };

    this.http.httpGet(apiUrl).subscribe({
      next: (res: any) => {
        const data = res?.data || [];

        // Category selected and SubCategory not selected
if (category && !option) {

  let filteredData = [...data];

  // Apply common filters
  if (this.selectedZone) {
    filteredData = filteredData.filter(
      x => x.zone?.trim().toLowerCase() ===
      this.selectedZone.trim().toLowerCase()
    );
  }

  if (this.selectedState) {
    filteredData = filteredData.filter(
      x => x.state?.trim().toLowerCase() ===
      this.selectedState?.name.trim().toLowerCase()
    );
  }

  if (this.selectedCity) {
    filteredData = filteredData.filter(
      x => x.city?.trim().toLowerCase() ===
      this.selectedCity?.name.trim().toLowerCase()
    );
  }

  if (this.selectedGender) {
    filteredData = filteredData.filter(
      x => x.gender?.trim().toLowerCase() ===
      this.selectedGender.trim().toLowerCase()
    );
  }

  if (this.selectedStage) {

    const stageNumbers =
      this.stageMapping[this.selectedStage] || [];

    filteredData = filteredData.filter(
      x => stageNumbers.includes(Number(x.stage))
    );
  }

  const counts: any = {};

  this.availableOptions.forEach((opt:string)=>{
    counts[opt]=0;
  });

  // Age special handling
  if(category==="Age"){

    filteredData.forEach((item:any)=>{

      const age=Number(item.age);

      if(age>=18 && age<=30) counts["18-30"]++;
      else if(age>=31 && age<=40) counts["31-40"]++;
      else if(age>=41 && age<=50) counts["41-50"]++;
      else if(age>=51 && age<=60) counts["51-60"]++;
      else if(age>=61 && age<=70) counts["61-70"]++;
      else if(age>=71 && age<=80) counts["71-80"]++;
      else if(age>80) counts[">80"]++;

    });

  }
  // else{

  //   // Generic handling for all remaining categories
  //   this.availableOptions.forEach((opt:string)=>{

  //     const count=filteredData.filter((x:any)=>
  //       JSON.stringify(x)
  //       .toLowerCase()
  //       .includes(opt.toLowerCase())
  //     ).length;

  //     counts[opt]=count;

  //   });

  // }

  else {

const fieldMap: any = {

  // Normal fields
  "Education":"education",
  "Occupation":"occupation",
  "Place Type":"placeType",
  "Socioeconomic Status":"socioeconomicStatus",
  "Annual Family Income (Rupees)":"familyIncome",
  "Sleep Apnea":"sleepApneayes",
  "Computer Use":"computerUsed",
  "Smartphone Use":"smartphoneUsed",
  "Working Hours (Occupation)":"workingHours",
  "Job/ Occupation type":"jobType",
  "Los Angeles Grade":"eeAngelesGrade",
  "Hill’s classification Grade":"eeHillClassificationGrade",

  // Heartburn type fields
  "Heartburn":"hbPostural",
  "Regurgitation":"rPostural",
  "Retrosternal Pain":"rpPostural",
  "Acid Taste in mouth":"atPostural",
"Newly Diagnosed":"newlyDiagnosed",
"Known case of GERD":"knownCaseOfGerd",
"GERDType":"gerdType",
"RefractorytoPPI":"refractoryToPpi",
"AdherencetoTherapy":"adherenceToTherapy",
};

const field=fieldMap[category];

if(
field || 
category==="COMORBIDITIES" || 
category==="Diet" ||
category==="Patient Personal History" ||
category==="Exercise" ||

category==="Computer Usage (hrs/day)" ||
category==="Computer Usage Duration (years)" ||
category==="Smartphone Usage (hrs/day)" ||
category==="Smartphone Usage Duration (years)" ||
category==="Duration (No. of years in the above working hours)"  ||
 category==="Family History of GERD" ||
 category==="Family History of Esophago-gastric Cancer" ||
 category==="PPI Usage" ||
 category==="History of Endoscopy" ||
 category==="History of Gastro-surgery" ||
 category==="Bariatric Surgery" ||
 category==="Fundoplication Surgery" ||
 category==="Gastric POEM Surgery" ||
 category==="Gastrojejunostomy" ||
 category==="Other Gastro Surgery" ||
category==="Newly Diagnosed"
||
category==="Known case of GERD"
||
category==="GERDType"
||
category==="RefractorytoPPI"
||
category==="AdherencetoTherapy" ||
category==="Lifestyle Recommendations" ||
category==="Drug Therapy Advised"

){

this.availableOptions.forEach((opt:string)=>{

counts[opt]=filteredData.filter((item:any)=>{

const isYes=(v:any)=>{
 return v===true ||
        v==="true" ||
        v==="True" ||
        v==="Yes" ||
        v==="yes" ||
        v===1 ||
        v==="1";
}


/* DIET */

if(category==="Diet"){

const dietMap:any={

'Vegetarian':'dietVegetarian',
'Non-Vegetarian':'dietNonVegetarian'

};

const fieldName=dietMap[opt];

if(!fieldName) return false;

return isYes(item[fieldName]);

}

/* Drug Therapy Advised */

if(category==="Drug Therapy Advised"){

const drugMap:any={

'PPI':'ppiMedicationName',
'Combination of PPI + Prokinetics':'prokineticsMedicationName',
'Sucralfate':'sucralfateMedicationName',
'Alginate':'alginateMedicationName',
'H₂ Blockers':'h2blockersMedicationName',
'H₂ Blockers combinations':'h2blockersCMedicationName',
'PCAB':'pcabMedicationName',
'Any others':'othersMedicationName'

};

const fieldName=drugMap[opt];

if(!fieldName) return false;

/* check value exists */
return item[fieldName] &&
       item[fieldName]
       .toString()
       .trim()!=="";

}

/* USAGE / YEARS COMMON HANDLING */

if(
 category==="Computer Usage (hrs/day)" ||
 category==="Computer Usage Duration (years)" ||
 category==="Smartphone Usage (hrs/day)" ||
 category==="Smartphone Usage Duration (years)" ||
 category==="Duration (No. of years in the above working hours)"
)
{
const fieldMap:any={

"Computer Usage (hrs/day)":"computerFrequency",
"Computer Usage Duration (years)":"computerDurationYears",
"Smartphone Usage (hrs/day)":"smartphoneFrequency",
"Smartphone Usage Duration (years)":"smartphoneDurationYears",
"Duration (No. of years in the above working hours)":"totalWorkingYears"

};

const fieldName=fieldMap[category];

const value=parseFloat(item[fieldName] || 0);

if(opt==="0-1"){
 return value>=0 && value<=1;
}

if(opt==="1-5"){
 return value>1 && value<=5;
}

if(opt==="5-10"){
 return value>5 && value<=10;
}

if(opt==="10-15"){
 return value>10 && value<=15;
}

if(opt==="15-20"){
 return value>15 && value<=20;
}

if(opt==="20-120"){
 return value>20 && value<=120;
}

return false;
}
/* Heartburn */

/* PATIENT PERSONAL HISTORY */

/* PATIENT PERSONAL HISTORY */
if(category==="Lifestyle Recommendations"){

const lifestyleMap:any={

'Diet modification':'dietModifications',
'Moderation of alcohol':'moderationOfAlcohol',
'Weight loss':'weightLoss',
'Regular exercise':'regularExercise',
'Stop Tobacco use':'stopTobaccoUse'

};

const fieldName=lifestyleMap[opt];

if(!fieldName) return false;

return isYes(item[fieldName]);
}

if(category==="Patient Personal History"){

const personalHistoryMap:any={

'Aerated Drinks':'aeratedIntake',
'Coffee':'coffeeIntake',
'Tea':'teaIntake',
'Spicy food':'spicyIntake',
'Alcohol':'alcoholIntake',
'Chocolates/ sweets':'sweetsIntake',
'Smoking (cigarettes/day)':'smokingIntake',
'Tobacco (other forms/day)':'tobaccoIntake'

};

const fieldName = personalHistoryMap[opt];

if(!fieldName) return false;

return isYes(item[fieldName]);

}

else if(
 category==="Family History of GERD" ||
 category==="Family History of Esophago-gastric Cancer" ||
 category==="PPI Usage" ||
 category==="History of Endoscopy" ||
 category==="History of Gastro-surgery" ||
 category==="Bariatric Surgery" ||
 category==="Fundoplication Surgery" ||
 category==="Gastric POEM Surgery" ||
 category==="Gastrojejunostomy" ||
 category==="Other Gastro Surgery"
){

const yesNoMap:any={

"Family History of GERD":"fhGred",
"Family History of Esophago-gastric Cancer":"fhEgc",
"PPI Usage":"ghPpi",
"History of Endoscopy":"historyofEndoscopy",
"History of Gastro-surgery":"historyofGs",
"Bariatric Surgery":"gsBariatricSurgery",
"Fundoplication Surgery":"gsFundoplicationSurgery",
"Gastric POEM Surgery":"gsGastricPoemsurgery",
"Gastrojejunostomy":"gsGastrojejunostomy",
"Other Gastro Surgery":"gsOther"

};

const fieldName=yesNoMap[category];

if(!fieldName) return false;

if(opt==="Yes"){
   return isYes(item[fieldName]);
}

if(opt==="No"){
   return !isYes(item[fieldName]);
}

return false;

}/* SLEEP APNEA */

/* CURRENT MEDICATIONS */

if(category==="Current Medications"){

const medicationMap:any={

"PPI":"ppi",
"Combination of PPI + Prokinetics":"ppiProkinetics",
"Sucralfate":"sucralfate",
"Alginate":"alginate",
"H₂ Blockers":"h2Blockers",
"H₂ Blockers combinations":"h2BlockersCombinations",
"PCAB":"pcab",
"Any others":"otherMedications"

};

const fieldName=medicationMap[opt];

return isYes(item[fieldName]);

}
if(category==="Sleep Apnea"){

if(opt==="Yes"){
   return isYes(item.sleepApneayes);
}

if(opt==="No"){
   return !isYes(item.sleepApneayes);
}

return false;

}
if(category==="Heartburn"){
 return opt==="Postural"
 ? isYes(item.hbPostural)
 : isYes(item.hbNocturnal);
}

if(category==="Regurgitation"){
 return opt==="Postural"
 ? isYes(item.rPostural)
 : isYes(item.rNocturnal);
}

if(category==="Retrosternal Pain"){
 return opt==="Postural"
 ? isYes(item.rpPostural)
 : isYes(item.rpNocturnal);
}

if(category==="Acid Taste in mouth"){
 return opt==="Postural"
 ? isYes(item.atPostural)
 : isYes(item.atNocturnal);
}

/* EXERCISE */

if(category==="Exercise"){

const exerciseMap:any={

'Walking':'walkingSelectedyes',
'Jogging':'joggingSelectedyes',
'Gym':'gymSelectedyes',
'Yoga':'yogaSelectedyes',
'Aerobics':'aerobicsyes',
'Zumba':'zumbayes',
'Others':'othersyes'

};

const fieldName=exerciseMap[opt];

if(!fieldName) return false;

return isYes(item[fieldName]);

}

/* COMORBIDITIES */

if(category==="COMORBIDITIES"){

const comorbidityMap:any={

'Hypertension':'htPresent',
'Diabetes':'dbPresent',
'Dyslipidemia':'ddPresent',
'Chronic liver disease':'cldPresent',
'Neurological Disorder':'ndPresent',
'Cardiovascular disorders':'cdPresent',
'Hypothyroidism':'hPresent',
'Hyperthyroidism':'htdPresent',
'Behavioural disorders':'bdPresent',
'Chronic kidney disease':'ckdPresent',
'Asthma':'aPresent',
'Osteoarthritis':'oPresent',
'Rheumatoid arthritis':'raPresent',
'Systemic Sclerosis':'ssPresent',
'Cancer':'cPresent',
'Others':'cmoPresent'

};

const fieldName=comorbidityMap[opt];

if(!fieldName) return false;

return isYes(item[fieldName]);

}


/* Normal Categories */

let value=item[field];

if(value===true) value="Yes";
if(value===false) value="No";

const normalize=(str:any)=>{

let value=str?.toString()?.trim();

if(!value) return '';

/* Education */

if(value==="10th Std & Above")
 value="Above Tenth standard";

if(value==="Below 10th Std")
 value="Below Tenth standard";

/* Occupation */

if(value==="Non-sedentary")
 value="Non sedentary";

/* Income */

if(
value.toLowerCase().includes("less than")
||
value.includes("<")
){
 value="< 1 lakh";
}

if(
value.toLowerCase().includes("1 to 5")
||
value.toLowerCase().includes("1-5")
){
 value="1-5 lakhs";
}

if(
value.toLowerCase().includes("greater than")
||
value.includes(">")
){
 value="> 5 lakhs";
}

return value
.toLowerCase()
.replace(/[\u2010-\u2015\u2212]/g,'-')
.replace(/[-\s]+/g,'');

}

return normalize(value)===normalize(opt);

}).length;

});
}

}
  this.pieChartData={
    labels:Object.keys(counts),
    datasets:[
      {
        data:Object.values(counts),
        backgroundColor:[
          '#FF6384',
          '#36A2EB',
          '#FFCE56',
          '#4BC0C0',
          '#9966FF',
          '#FF9F40',
          '#8BC34A'
        ]
      }
    ]
  };

  this.barChartData={
    labels:Object.keys(counts),
    datasets:[
      {
        label:category,
        data:Object.values(counts),
        backgroundColor:[
          '#FF6384',
          '#36A2EB',
          '#FFCE56',
          '#4BC0C0',
          '#9966FF',
          '#FF9F40',
          '#8BC34A'
        ]
      }
    ]
  };

  this.tableData = filteredData;

  this.updatePagination();

  return;
}
        if (category === 'Exercise' && option) {
          const isYes = (value: any) =>
            value === true || value === 'true' || value?.toString().toLowerCase() === 'yes';
          const isNo = (value: any) =>
            value === false || value === 'false' || value?.toString().toLowerCase() === 'no';

          const yesField = SleepMap[option]?.yes;
          const noField = SleepMap[option]?.no;

          let filteredData = [...data];

          if (this.selectedZone) {
            filteredData = filteredData.filter(item => item.zone?.trim() === this.selectedZone.trim());
          }
          if (this.selectedState) {
            filteredData = filteredData.filter(item => item.state?.trim() === this.selectedState?.name.trim());
          }
          if (this.selectedCity) {
            filteredData = filteredData.filter(item => item.city?.trim() === this.selectedCity?.name.trim());
          }
          if (this.selectedGender) {
            filteredData = filteredData.filter(item => item.gender?.trim() === this.selectedGender.trim());
          }
          if (this.selectedStage) {
            const stageNumbers = this.stageMapping[this.selectedStage] || [];
            filteredData = filteredData.filter(item => stageNumbers.includes(Number(item.stage)));
          }

          // Step 2: Count yes/no for charts using the same filteredData
          const yesRows = yesField ? filteredData.filter(item => isYes(item[yesField])) : [];
          const noRows = noField ? filteredData.filter(item => isNo(item[noField])) : [];

          const counts = { yes: yesRows.length, no: noRows.length };

          // Step 3: Use the combined rows for table
          const tableData = [...yesRows, ...noRows];

          // Step 4: Update charts
          this.pieChartData = {
            labels: [`${option} Yes`, `${option} No`],
            datasets: [{ data: [counts.yes, counts.no], backgroundColor: ['#36A2EB', '#FF6384'] }]
          };

          this.barChartData = {
            labels: ['Yes', 'No'],
            datasets: [{ label: option, data: [counts.yes, counts.no], backgroundColor: ['#36A2EB', '#FF6384'] }]
          };

          // Step 5: Update table
          this.tableData = tableData;
          if (!this.tableData.length) alert('No data found for selected option');

          // Step 6: Update pagination
          this.updatePagination();
          return;
        }



        const normalizeValue = (category: string, val: any) => {



          const key = `${category} (${val})`;
          if (category === 'Education') {
            if (val === "10th Std & Above") return "Above Tenth standard";
            if (val === "Below 10th Std") return "Below Tenth standard";
          }



          const symptomMap: Record<string, Record<string, string>> = {
            'Heartburn': { 'Postural': 'hbPostural', 'Nocturnal': 'hbNocturnal' },
            'Regurgitation': { 'Postural': 'rPostural', 'Nocturnal': 'rNocturnal' },
            'Retrosternal Pain': { 'Postural': 'rpPostural', 'Nocturnal': 'rpNocturnal' },
            'Acid Taste in mouth': { 'Postural': 'atPostural', 'Nocturnal': 'atNocturnal' },
          };


          const comorbiditiesMap: Record<string, string> = {
            'Hypertension': 'htPresent',
            "Hyperthyroidism": 'hPresent',
            'Diabetes': 'dbPresent',
            'Dyslipidemia': 'ddPresent',
            'Chronic liver disease': 'cldPresent',
            'Neurological Disorder': 'ndPresent',
            'Cardiovascular disorders': 'cdPresent',
            'Hypothyroidism': 'hPresent',
            'Behavioural disorders': 'htdPresent',
            'Chronic kidney disease': 'ckdPresent',
            'Asthma': 'aPresent',
            'Osteoarthritis': 'oPresent',
            'Rheumatoid arthritis': 'raPresent',
            'Systemic Sclerosis': 'ssPresent',
            'Cancer': 'cPresent',
            'Others': 'cmoPresent'
          };


          const dietMap: Record<string, string> = { 'Vegetarian': 'dietVegetarian', 'Non-Vegetarian': 'dietNonVegetarian' };

          const personalHistoryMap: Record<string, string> = {
            'Aerated Drinks': 'aeratedIntake',
            'Coffee': 'coffeeIntake',
            'Tea': 'teaIntake',
            'Spicy food': 'spicyIntake',
            'Alcohol': 'alcoholIntake',
            'Chocolates/ sweets': 'sweetsIntake',
            'Smoking (cigarettes/day)': 'smokingIntake',
            'Tobacco (other forms/day)': 'tobaccoIntake'
          };


          const GadgetMap: Record<string, string> = {
            'Computer Usage (hrs/day)': 'computerFrequency',
            'Computer Usage Duration (years)': 'computerDurationYears',
            'Smartphone Usage (hrs/day)': 'smartphoneFrequency',
            'Smartphone Usage Duration (years)': 'smartphoneDurationYears',
            'Working Hours (Occupation)': 'workingHours',
            'Job/ Occupation type': 'jobType',
            'Duration (No. of years in the above working hours)': 'totalWorkingYears'
          };

          const familyHistoryMap: Record<string, string> = {
            'Family History of GERD': 'fhGred',
            'Family History of Esophago-gastric Cancer': 'fhEgc',
            'PPI Usage': 'ghPpi'
          };

          const GERDGISTORYMAP: Record<string, string> = {
            'History of Endoscopy': 'historyofEndoscopy',
            'History of Gastro-surgery': 'historyofGs',
            'Bariatric Surgery': 'gsBariatricSurgery',
            'Fundoplication Surgery': 'gsFundoplicationSurgery',
            'Gastric POEM Surgery': 'gsGastricPoemsurgery',
            'Gastrojejunostomy': 'gsGastrojejunostomy',
            'Other Gastro Surgery': 'gsOther'
          };


          const managementMap: Record<string, string> = {
            'Diet modification': 'dietModifications',
            'Moderation of alcohol': 'moderationOfAlcohol',
            'Weight loss': 'weightLoss',
            'Regular exercise': 'regularExercise',
            'Stop Tobacco use': 'stopTobaccoUse',

            'PPI': 'ppiMedicationName',
            'Combination of PPI + Prokinetics': 'prokineticsMedicationName',
            'Sucralfate': 'sucralfateMedicationName',
            'Alginate': 'alginateMedicationName',
            'H₂ Blockers': 'h2blockersMedicationName',
            'H₂ Blockers combinations': 'h2blockersCMedicationName',
            'PCAB': 'pcabMedicationName',
            'Any others': 'othersMedicationName'
          };



          if (category === 'COMORBIDITIES') return comorbiditiesMap[val] || val;
          if (category === 'Diet') return dietMap[val] || val;
          if (category === 'Patient Personal History') return personalHistoryMap[val] || val;
          if (category === 'Exercise') return SleepMap[val] || val;
          if (category === 'Newly Diagnosed') {
            const key = `Newly Diagnosed (${val})`;
            return
          }



          if (GERDGISTORYMAP[category]) return GERDGISTORYMAP[category];
          if (['Computer Use', 'Smartphone Use'].includes(category)) return GadgetMap[`${category} (${val})`] || val;
          if (familyHistoryMap[category]) return familyHistoryMap[category];
          if (category === 'Lifestyle Recommendations' || category === 'Drug Therapy Advised') {
            return managementMap[val] || val;
          }
          return symptomMap[category]?.[val] || val;

        };

        const isYes = (value: any) => value === true || value === 'true' || value?.toString().toLowerCase() === 'yes';

        const normalizedOptions = this.availableOptions.map(opt => normalizeValue(category, opt));

        const optionCounts: Record<string, number> = {};
        normalizedOptions.forEach(opt => optionCounts[opt] = 0);


        const booleanCategoriesNormalized = [
          'htPresent', 'dbPresent', 'hlPresent', 'oPresent', 'aPresent', 'cPresent', 'hPresent',
          'ckdPresent', 'cldPresent', 'htdPresent', 'raPresent', 'ssPresent', 'cmoPresent', 'bdPresent',
          'dietVegetarian', 'dietNonVegetarian', 'hPresent',
          'aeratedIntake', 'coffeeIntake', 'teaIntake', 'spicyIntake', 'alcoholIntake', 'sweetsIntake', 'smokingIntake', 'tobaccoIntake',
          'sleepApneayes', 'sleepApneano', 'exerciseIntakeyes', 'exerciseIntakeno', 'joggingSelectedyes', 'gymSelectedyes', 'yogaSelectedyes', 'walkingSelectedyes', 'aerobicsyes', 'zumbano', 'othersyes',
          'computerUsed', 'computerNotUsed', 'smartphoneUsed', 'smartphoneNotUsed',
          'fhGred', 'fhEgc', 'ghPpi',
          'historyofEndoscopy', 'historyofGs', 'gsBariatricSurgery', 'gsFundoplicationSurgery', 'gsGastricPoemsurgery', 'gsGastrojejunostomy', 'gsOther',
          'ndPresent', 'ndAbsent', 'ndMale', 'ndFemale',
          'dbPresent', 'dbPresent', 'gerdMale', 'gerdFemale',
          'ddPresent', 'ddPresent',
          'cldPresent', 'cldPresent',
          'ndPresent', 'ndPresent', 'walkingSelectedno'
        ];


        data.forEach((item: any) => {
          normalizedOptions.forEach(opt => {
            let value = item[opt];

            if (category === 'Current Medications') {
              const field = fieldMap[opt]; // get actual field
              value = item[field];
              if (value && value.toString().trim() !== '') {
                optionCounts[opt]++;
              }
              return;
            }


            if (booleanCategoriesNormalized.includes(opt)) {
              if (isYes(value)) optionCounts[opt]++;
            } else if (category.includes('Usage') || category.includes('Working Hours')) {
              if (value && normalizedOptions.includes(value)) optionCounts[value]++;
            } else {
              if (value === 'Yes') optionCounts[opt]++;
            }
          });
        });



        this.pieChartData = {
          labels: option ? [option] : Object.keys(optionCounts),
          datasets: [{
            data: option ? [0] : Object.values(optionCounts),
            backgroundColor: option ? ['#36A2EB'] : [
              '#FF6384', '#36A2EB', '#FFCE56', '#4BC0C0',
              '#9966FF', '#FF9F40', '#FF7F50', '#87CEEB'
            ]
          }]
        };

        this.barChartData = {
          labels: option ? [option] : Object.keys(optionCounts),
          datasets: [
            {
              label: category,
              data: option ? [this.tableData.length] : Object.values(optionCounts),
              backgroundColor: option
                ? ['#36A2EB']
                : [
                  '#FF6384', '#36A2EB', '#FFCE56', '#4BC0C0',
                  '#9966FF', '#FF9F40', '#FF7F50', '#87CEEB'
                ]
            }
          ]
        };
        if (option) {

          if (category === 'Current Medications') {
            const field = fieldMap[option];
            this.tableData = data.filter((item: any) => item[field] && item[field].trim() !== '');
          }
          else if (category === 'Age') {


            if (option && option.includes('-')) {
              const agegrp = option.split('-').map(x => x.trim()); // remove spaces

              if (agegrp.length === 2) {
                const minAge = parseInt(agegrp[0], 10);
                const maxAge = parseInt(agegrp[1], 10);
                if (!isNaN(minAge) && !isNaN(maxAge)) {

                  const filteredData = data.filter((r: any) => r.age >= Number(minAge) && r.age <= Number(maxAge));
                  this.tableData = filteredData;

                  this.barChartData = {
                    labels: [option],
                    datasets: [
                      {
                        label: category,
                        data: [filteredData.length],
                        backgroundColor: ['#FF6384']
                      }
                    ]
                  };
                } else {
                  console.error('Invalid age numbers in range:', agegrp);
                }
              } else {
                console.error('Age range does not have two numbers:', agegrp);
              }
            } else {
              console.error('Option is empty or invalid:', option);
            }
          }

          else if (category === 'Education') {
            const normalizedOption = normalizeValue('Education', option);

            const filteredData = data.filter((r: any) => {
              const eduNormalized = normalizeValue('Education', r.education);
              return eduNormalized === normalizedOption;
            });

            this.tableData = filteredData;

            this.pieChartData = {
              labels: [option],
              datasets: [
                { data: [filteredData.length], backgroundColor: ['#FF6384'] }
              ]
            };
            this.barChartData = {
              labels: [option],
              datasets: [
                { label: category, data: [filteredData.length], backgroundColor: ['#FF6384'] }
              ]
            };
          }

          else if (category === 'Occupation') {
            const normalizeValue = (val: any) => {
              if (!val) return '';
              return val
                .toString()
                .trim()
                .toLowerCase()
                .replace(/[\u2010-\u2015\u2212]/g, '-') // normalize all dash types
                .replace(/[-\s]+/g, ''); // remove spaces and hyphens for comparison
            };

            const normalizedOption = normalizeValue(option);

            const filteredData = data.filter((r: any) => {
              const occNormalized = normalizeValue(r.occupation);
              return occNormalized === normalizedOption;
            });

            console.log('Filtered length:', filteredData.length);

            this.tableData = filteredData;

            this.pieChartData = {
              labels: [option],
              datasets: [
                { data: [filteredData.length], backgroundColor: ['#FF6384'] }
              ]
            };

            this.barChartData = {
              labels: [option],
              datasets: [
                { label: category, data: [filteredData.length], backgroundColor: ['#FF6384'] }
              ]
            };
          }




          else if (category === 'Place Type') {
            const normalizedOption = option.trim().toLowerCase();

            const filteredData = data.filter((r: any) => {
              if (!r.placeType) return false;
              // split by comma if multiple types, trim, and lowercase for safe comparison
              const placeTypes = r.placeType
                .split(',')
                .map((x: string) => x.trim().toLowerCase());
              return placeTypes.includes(normalizedOption);
            });

            this.tableData = filteredData;

            this.pieChartData = {
              labels: [option],
              datasets: [{ data: [filteredData.length], backgroundColor: ['#FF6384'] }]
            };

            this.barChartData = {
              labels: [option],
              datasets: [{ label: category, data: [filteredData.length], backgroundColor: ['#FF6384'] }]
            };
          }



          else if (category === 'Annual Family Income (Rupees)') {
            // Normalize clicked option to standard bucket
            const normalizeIncome = (income: string): string => {
              if (!income) return '';
              const inc = income.trim().toLowerCase();
              if (inc.includes('less than') || inc.includes('<')) return '< 1 lakh';
              if (inc.includes('1-5') || inc.includes('1 to 5')) return '1-5 lakhs';
              if (inc.includes('greater than') || inc.includes('>')) return '> 5 lakhs';
              return inc; // fallback
            };

            const normalizedOption = normalizeIncome(option);
            const filteredData = data.filter((r: any) => {
              const incomeNormalized = normalizeIncome(r.familyIncome);
              return incomeNormalized === normalizedOption;
            });

            this.tableData = filteredData;

            this.pieChartData = {
              labels: [option],
              datasets: [{ data: [filteredData.length], backgroundColor: ['#FF6384'] }]
            };

            this.barChartData = {
              labels: [option],
              datasets: [{ label: category, data: [filteredData.length], backgroundColor: ['#FF6384'] }]
            };
          }

          else if (category === 'Socioeconomic Status') {
            const normalizeStatus = (status: string): string => {
              if (!status) return '';
              return status.trim().toLowerCase(); // make it lowercase
            };

            const normalizedOption = normalizeStatus(option);

            const filteredData = data.filter((r: any) => {
              const statusNormalized = normalizeStatus(r.socioeconomicStatus);
              return statusNormalized === normalizedOption;
            });

            this.tableData = filteredData;

            this.pieChartData = {
              labels: [option],
              datasets: [{ data: [filteredData.length], backgroundColor: ['#FF6384'] }]
            };

            this.barChartData = {
              labels: [option],
              datasets: [{ label: category, data: [filteredData.length], backgroundColor: ['#FF6384'] }]
            };
          }

          else if (category === 'Computer Usage (hrs/day)') {
            if (option && option.includes('-')) {
              const computerFrequencygrp = option.split('-').map(x => x.trim());

              if (computerFrequencygrp.length === 2) {
                const mincomputerFrequency = parseInt(computerFrequencygrp[0], 10);
                const maxcomputerFrequency = parseInt(computerFrequencygrp[1], 10);

                if (!isNaN(mincomputerFrequency) && !isNaN(maxcomputerFrequency)) {

                  const filteredData = data.filter(
                    (r: any) =>
                      r.computerFrequency >= mincomputerFrequency &&
                      r.computerFrequency <= maxcomputerFrequency
                  );

                  this.tableData = filteredData;

                  // Update charts
                  this.pieChartData = {
                    labels: [option],
                    datasets: [{ data: [filteredData.length], backgroundColor: ['#FF6384'] }]
                  };

                  this.barChartData = {
                    labels: [option],
                    datasets: [{ label: category, data: [filteredData.length], backgroundColor: ['#36A2EB'] }]
                  };

                } else {
                  console.error('Invalid numbers in range:', computerFrequencygrp);
                }
              } else {
                console.error('Range does not have two numbers:', computerFrequencygrp);
              }
            } else {
              console.error('Option is empty or invalid:', option);
            }
          }

          else if (category === 'Smartphone Usage (hrs/day)') {
            if (option && option.includes('-')) {
              const smartphoneFrequencygrp = option.split('-').map(x => x.trim());

              if (smartphoneFrequencygrp.length === 2) {
                const minsmartphoneFrequency = parseInt(smartphoneFrequencygrp[0], 10);
                const maxsmartphoneFrequency = parseInt(smartphoneFrequencygrp[1], 10);

                if (!isNaN(minsmartphoneFrequency) && !isNaN(maxsmartphoneFrequency)) {
                  const filteredData = data.filter(
                    (r: any) =>
                      r.smartphoneFrequency >= minsmartphoneFrequency &&
                      r.smartphoneFrequency <= maxsmartphoneFrequency
                  );

                  this.tableData = filteredData;

                  // Update charts
                  this.pieChartData = {
                    labels: [option],
                    datasets: [{ data: [filteredData.length], backgroundColor: ['#FFCE56'] }]
                  };

                  this.barChartData = {
                    labels: [option],
                    datasets: [{ label: category, data: [filteredData.length], backgroundColor: ['#4BC0C0'] }]
                  };

                } else {
                  console.error('Invalid numbers in range:', smartphoneFrequencygrp);
                }
              } else {
                console.error('Range does not have two numbers:', smartphoneFrequencygrp);
              }
            } else {
              console.error('Option is empty or invalid:', option);
            }
          }

          else if (category === 'Computer Usage Duration (years)') {
            if (option && option.includes('-')) {
              const [minStr, maxStr] = option.split('-').map(x => x.trim());

              const minDuration = parseInt(minStr, 10);
              const maxDuration = parseInt(maxStr, 10);

              if (!isNaN(minDuration) && !isNaN(maxDuration)) {
                const filteredData = data.filter((r: any) => {
                  // convert computerDurationYears to number
                  const duration = parseInt(r.computerDurationYears, 10);
                  return !isNaN(duration) && duration >= minDuration && duration <= maxDuration;
                });

                this.tableData = filteredData;

                this.pieChartData = {
                  labels: [option],
                  datasets: [{ data: [filteredData.length], backgroundColor: ['#FF6384'] }]
                };

                this.barChartData = {
                  labels: [option],
                  datasets: [{ label: category, data: [filteredData.length], backgroundColor: ['#36A2EB'] }]
                };
              } else {
                console.error('Invalid numbers in range:', option);
              }
            } else {
              console.error('Option is empty or invalid:', option);
            }
          }


          else if (category === 'Working Hours (Occupation)') {
            const filteredData = data.filter((r: any) =>
              r.workingHours?.toLowerCase() === option?.toLowerCase()
            );

            this.tableData = filteredData;

            this.pieChartData = {
              labels: [option],
              datasets: [{ data: [filteredData.length], backgroundColor: ['#36A2EB'] }]
            };

            this.barChartData = {
              labels: [option],
              datasets: [{ label: category, data: [filteredData.length], backgroundColor: ['#4BC0C0'] }]
            };
          }

          else if (category === 'Sleep Apnea') {
            const normalizedOption = option.trim().toLowerCase();

            let field = '';
            if (normalizedOption === 'yes') field = 'sleepApneayes';
            else if (normalizedOption === 'no') field = 'sleepApneano';

            if (field) {
              this.tableData = data.filter((item: any) => item[field] && item[field].trim() !== '');
            } else {
              console.error('Invalid Sleep Apnea option:', option);
              this.tableData = [];
            }

            this.pieChartData = {
              labels: [option],
              datasets: [{ data: [this.tableData.length], backgroundColor: ['#36A2EB'] }]
            };

            this.barChartData = {
              labels: [option],
              datasets: [{ label: category, data: [this.tableData.length], backgroundColor: ['#4BC0C0'] }]
            };
          }


          else if (category === 'Family History of GERD') {
            const normalizedOption = option.trim().toLowerCase();
            const field = 'fhGred'; 

            this.tableData = data.filter(
              (item: any) =>
                item[field] &&
                item[field].trim().toLowerCase() === normalizedOption
            );

            // Update charts
            this.pieChartData = {
              labels: [option],
              datasets: [
                { data: [this.tableData.length], backgroundColor: ['#36A2EB'] }
              ]
            };

            this.barChartData = {
              labels: [option],
              datasets: [
                { label: category, data: [this.tableData.length], backgroundColor: ['#4BC0C0'] }
              ]
            };
          }


           else if (category === 'Family History of Esophago-gastric Cancer') {
            const normalizedOption = option.trim().toLowerCase();
            const field = 'fhEgc'; 

            this.tableData = data.filter(
              (item: any) =>
                item[field] &&
                item[field].trim().toLowerCase() === normalizedOption
            );

            // Update charts
            this.pieChartData = {
              labels: [option],
              datasets: [
                { data: [this.tableData.length], backgroundColor: ['#36A2EB'] }
              ]
            };

            this.barChartData = {
              labels: [option],
              datasets: [
                { label: category, data: [this.tableData.length], backgroundColor: ['#4BC0C0'] }
              ]
            };
          }

           else if (category === 'PPI Usage') {
            const normalizedOption = option.trim().toLowerCase();
            const field = 'ghPpi'; 

            this.tableData = data.filter(
              (item: any) =>
                item[field] &&
                item[field].trim().toLowerCase() === normalizedOption
            );

            // Update charts
            this.pieChartData = {
              labels: [option],
              datasets: [
                { data: [this.tableData.length], backgroundColor: ['#36A2EB'] }
              ]
            };

            this.barChartData = {
              labels: [option],
              datasets: [
                { label: category, data: [this.tableData.length], backgroundColor: ['#4BC0C0'] }
              ]
            };
          }



          
           else if (category === 'History of Endoscopy') {
            const normalizedOption = option.trim().toLowerCase();
            const field = 'historyofEndoscopy'; 

            this.tableData = data.filter(
              (item: any) =>
                item[field] &&
                item[field].trim().toLowerCase() === normalizedOption
            );

            // Update charts
            this.pieChartData = {
              labels: [option],
              datasets: [
                { data: [this.tableData.length], backgroundColor: ['#36A2EB'] }
              ]
            };

            this.barChartData = {
              labels: [option],
              datasets: [
                { label: category, data: [this.tableData.length], backgroundColor: ['#4BC0C0'] }
              ]
            };
          }

           else if (category === 'History of Gastro-surgery') {
            const normalizedOption = option.trim().toLowerCase();
            const field = 'historyofGs'; 

            this.tableData = data.filter(
              (item: any) =>
                item[field] &&
                item[field].trim().toLowerCase() === normalizedOption
            );

            // Update charts
            this.pieChartData = {
              labels: [option],
              datasets: [
                { data: [this.tableData.length], backgroundColor: ['#36A2EB'] }
              ]
            };

            this.barChartData = {
              labels: [option],
              datasets: [
                { label: category, data: [this.tableData.length], backgroundColor: ['#4BC0C0'] }
              ]
            };
          }

          else if (category === 'Computer Use') {
            const normalizedOption = option.trim().toLowerCase(); // "yes" or "no"

            let field = 'computerUsed';
            let value: boolean | null = null;

            if (normalizedOption === 'yes') value = true;
            else if (normalizedOption === 'no') value = false;

            if (value !== null) {
              this.tableData = data.filter((item: any) => item[field] === value);
            } else {
              console.error('Invalid Computer Use option:', option);
              this.tableData = [];
            }

            this.pieChartData = {
              labels: [option],
              datasets: [
                { data: [this.tableData.length], backgroundColor: ['#36A2EB'] }
              ]
            };

            this.barChartData = {
              labels: [option],
              datasets: [
                { label: category, data: [this.tableData.length], backgroundColor: ['#4BC0C0'] }
              ]
            };
          }

          else if (category === 'Smartphone Use') {
            const normalizedOption = option.trim().toLowerCase(); // "yes" or "no"

            let field = 'smartphoneUsed';
            let value: boolean | null = null;

            if (normalizedOption === 'yes') value = true;
            else if (normalizedOption === 'no') value = false;

            if (value !== null) {
              this.tableData = data.filter((item: any) => item[field] === value);
            } else {
              console.error('Invalid Computer Use option:', option);
              this.tableData = [];
            }

            this.pieChartData = {
              labels: [option],
              datasets: [
                { data: [this.tableData.length], backgroundColor: ['#36A2EB'] }
              ]
            };

            this.barChartData = {
              labels: [option],
              datasets: [
                { label: category, data: [this.tableData.length], backgroundColor: ['#4BC0C0'] }
              ]
            };
          }


          else if (category === 'Drug Therapy Advised') {
            const field = normalizeValue(category, option);
            this.tableData = data.filter((item: any) => item[field] && item[field].toString().trim() !== '' && item[field] !== 0);
            this.pieChartData = {
              labels: [option],
              datasets: [{ data: [this.tableData.length], backgroundColor: ['#36A2EB'] }]
            };
            this.barChartData = {
              labels: [option],
              datasets: [{ label: category, data: [this.tableData.length], backgroundColor: ['#4BC0C0'] }]
            };
          }



          else if (category === 'Los Angeles Grade') {
            this.tableData = data.filter((r: any) => r.eeAngelesGrade === option);
            this.pieChartData = {
              labels: [option],
              datasets: [{ data: [this.tableData.length], backgroundColor: ['#36A2EB'] }]
            };

            this.barChartData = {
              labels: [option],
              datasets: [{ label: category, data: [this.tableData.length], backgroundColor: ['#4BC0C0'] }]
            };
          }


          else if (category === 'Hill’s classification Grade') {
            this.tableData = data.filter((r: any) => r.eeHillClassificationGrade === option);
            this.pieChartData = {
              labels: [option],
              datasets: [{ data: [this.tableData.length], backgroundColor: ['#36A2EB'] }]
            };

            this.barChartData = {
              labels: [option],
              datasets: [{ label: category, data: [this.tableData.length], backgroundColor: ['#4BC0C0'] }]
            };
          }

          else if (category === 'Job/ Occupation type') {
            const filteredData = data.filter((r: any) =>
              r.jobType?.trim().toLowerCase().replace(/[-]/g, ' ') === option?.trim().toLowerCase()
            );

            this.tableData = filteredData;

            this.pieChartData = {
              labels: [option],
              datasets: [{ data: [filteredData.length], backgroundColor: ['#FF6384'] }]
            };

            this.barChartData = {
              labels: [option],
              datasets: [{ label: category, data: [filteredData.length], backgroundColor: ['#4BC0C0'] }]
            };
          }

else if (category === 'Newly Diagnosed') {

const isYes=(v:any)=>{
 return v===true ||
        v==="true" ||
        v==="True" ||
        v==="Yes" ||
        v==="yes" ||
        v===1 ||
        v==="1";
};

const filteredData=data.filter((r:any)=>
(
 option==="Yes" && isYes(r.newlyDiagnosed)
)
||
(
 option==="No" && !isYes(r.newlyDiagnosed)
)
);

this.tableData=filteredData;

this.pieChartData={
 labels:[option],
 datasets:[
 {
   data:[filteredData.length],
   backgroundColor:['#FF6384']
 }
 ]
};

this.barChartData={
 labels:[option],
 datasets:[
 {
   label:category,
   data:[filteredData.length],
   backgroundColor:['#4BC0C0']
 }
 ]
};

}

          else if (category === 'Known case of GERD') {
            const filteredData = data.filter((r: any) =>
              (option === 'Yes' && r.knownCaseOfGerd === true) ||
              (option === 'No' && r.knownCaseOfGerd === false)
            );

            this.tableData = filteredData;

            this.pieChartData = {
              labels: [option],
              datasets: [{ data: [filteredData.length], backgroundColor: ['#FF6384'] }]
            };

            this.barChartData = {
              labels: [option],
              datasets: [{ label: category, data: [filteredData.length], backgroundColor: ['#4BC0C0'] }]
            };
          }

          else if (category === 'RefractorytoPPI') {
            const filteredData = data.filter((r: any) =>
              (option === 'Yes' && r.refractoryToPpi === true) ||
              (option === 'No' && r.refractoryToPpi === false)
            );

            this.tableData = filteredData;

            this.pieChartData = {
              labels: [option],
              datasets: [{ data: [filteredData.length], backgroundColor: ['#FF6384'] }]
            };

            this.barChartData = {
              labels: [option],
              datasets: [{ label: category, data: [filteredData.length], backgroundColor: ['#4BC0C0'] }]
            };
          }

          else if (category === 'AdherencetoTherapy') {
            const filteredData = data.filter((r: any) =>
              (option === 'Yes' && r.adherenceToTherapy === true) ||
              (option === 'No' && r.adherenceToTherapy === false)
            );

            this.tableData = filteredData;

            this.pieChartData = {
              labels: [option],
              datasets: [{ data: [filteredData.length], backgroundColor: ['#FF6384'] }]
            };

            this.barChartData = {
              labels: [option],
              datasets: [{ label: category, data: [filteredData.length], backgroundColor: ['#4BC0C0'] }]
            };
          }
          else if (category === 'Duration (No. of years in the above working hours)') {
            if (option && option.includes('-')) {
              const range = option.split('-').map(x => parseInt(x.trim(), 10));
              const [min, max] = range;
              if (!isNaN(min) && !isNaN(max)) {
                const filteredData = data.filter((r: any) => r.totalWorkingYears >= min && r.totalWorkingYears <= max);
                this.tableData = filteredData;
                this.pieChartData = {
                  labels: [option],
                  datasets: [{ data: [filteredData.length], backgroundColor: ['#4BC0C0'] }]
                };

                this.barChartData = {
                  labels: [option],
                  datasets: [{ label: category, data: [this.tableData.length], backgroundColor: ['#4BC0C0'] }]
                };
              } else {
                console.error('Invalid working years range:', option);
              }
            } else {
              console.error('Option is empty or invalid:', option);
            }
          }

          else if (category === 'Smartphone Usage Duration (years)') {
            if (option && option.includes('-')) {
              const [minStr, maxStr] = option.split('-').map(x => x.trim());

              const minDuration = parseInt(minStr, 10);
              const maxDuration = parseInt(maxStr, 10);

              if (!isNaN(minDuration) && !isNaN(maxDuration)) {
                // Filter the data using smartphoneDurationYears
                const filteredData = data.filter((r: any) => {
                  const duration = parseInt(r.smartphoneDurationYears, 10); // convert to number
                  return !isNaN(duration) && duration >= minDuration && duration <= maxDuration;
                });

                this.tableData = filteredData; // update table

                // Update charts
                this.pieChartData = {
                  labels: [option],
                  datasets: [{ data: [filteredData.length], backgroundColor: ['#FF6384'] }]
                };

                this.barChartData = {
                  labels: [option],
                  datasets: [{ label: category, data: [filteredData.length], backgroundColor: ['#36A2EB'] }]
                };
              } else {
                console.error('Invalid numbers in range:', option);
              }
            } else {
              console.error('Option is empty or invalid:', option);
            }
          }



          else {
            const normalizedOption = normalizeValue(category, option);
            if (booleanCategoriesNormalized.includes(normalizedOption)) {
              this.tableData = data.filter((item: any) => isYes(item[normalizedOption]));
            } else {
              this.tableData = data.filter((item: any) => item[normalizedOption] === 'Yes');
            }

            this.barChartData = {
              labels: [option],
              datasets: [{ label: category, data: [this.tableData.length], backgroundColor: ['#4BC0C0'] }]
            };
          }

          if (this.selectedZone) {

            this.tableData = this.tableData.filter(item => item.zone.trim() === this.selectedZone.trim());
            this.pieChartData = {
              labels: [option],
              datasets: [{ data: [this.tableData.length], backgroundColor: ['#4BC0C0'] }]
            };

            this.barChartData = {
              labels: [option],
              datasets: [{ label: category, data: [this.tableData.length], backgroundColor: ['#4BC0C0'] }]
            };
          }

          if (this.selectedState) {
            this.tableData = this.tableData.filter(item => item.state.trim() === this.selectedState?.name.trim());
            this.pieChartData = {
              labels: [option],
              datasets: [{ data: [this.tableData.length], backgroundColor: ['#4BC0C0'] }]
            };

            this.barChartData = {
              labels: [option],
              datasets: [{ label: category, data: [this.tableData.length], backgroundColor: ['#4BC0C0'] }]
            };
          }

          if (this.selectedCity) {
            this.tableData = this.tableData.filter(item => item.city.trim() === this.selectedCity?.name.trim());
            this.pieChartData = {
              labels: [option],
              datasets: [{ data: [this.tableData.length], backgroundColor: ['#4BC0C0'] }]
            };

            this.barChartData = {
              labels: [option],
              datasets: [{ label: category, data: [this.tableData.length], backgroundColor: ['#4BC0C0'] }]
            };
          }


          if (this.selectedGender) {
            this.tableData = this.tableData.filter(item => item.gender.trim() === this.selectedGender.trim());
            this.pieChartData = {
              labels: [option],
              datasets: [{ data: [this.tableData.length], backgroundColor: ['#4BC0C0'] }]
            };

            this.barChartData = {
              labels: [option],
              datasets: [{ label: category, data: [this.tableData.length], backgroundColor: ['#4BC0C0'] }]
            };
          }

          if (this.selectedStage) {
            const stageNumbers = this.stageMapping[this.selectedStage] || [];
            this.tableData = this.tableData.filter(item => stageNumbers.includes(Number(item.stage)));

            this.pieChartData = {
              labels: [option],
              datasets: [{ data: [this.tableData.length], backgroundColor: ['#4BC0C0'] }]
            };

            this.barChartData = {
              labels: [option],
              datasets: [{ label: category, data: [this.tableData.length], backgroundColor: ['#4BC0C0'] }]
            };
          }
          if (this.tableData.length) {
            this.pieChartData.datasets[0].data = [this.tableData.length];
          } else {
            alert('No data found for selected option');
            this.pieChartData = { labels: [], datasets: [] };
            this.tableData = [];
            return;
          }
        } else {
          this.tableData = data;
        }



        this.updatePagination();
      },
      error: err => console.error('Error fetching data', err)
    });



  }

  normalize(str: string | undefined) {
    if (!str) return '';
    return str.toString().toLowerCase().replace(/\s+/g, '');
  } updatePieChart() {
    const labels: string[] = [];
    const counts: number[] = [];

    this.tableData.forEach((item: any) => {
      const key = this.selectedOption || 'Unknown';
      labels.push(key);
      counts.push(1);
    });

    this.pieChartData = {
      labels,
      datasets: [{ data: counts, backgroundColor: ['#FF6384', '#36A2EB', '#FFCE56'] }]
    };
  }



  filterDataForPieChart() {
    if (!this.patients || !this.patients.length) return [];

    let filtered = [...this.patients];

    if (this.selectedZone) {
      filtered = filtered.filter(p => p.zone === this.selectedZone);
    }

    if (this.selectedState) {
      filtered = filtered.filter(p => p.stateId === this.selectedState);
    }

    if (this.selectedCity) {
      filtered = filtered.filter(p => p.cityId === this.selectedCity);
    }


    if (this.selectedGender) {
      filtered = filtered.filter(p => p.cityId === this.selectedGender);
    }


    if (this.selectedStage) {
      filtered = filtered.filter(p => p.cityId === this.selectedStage);
    }

    return filtered;
  }


  exportToExcel(): void {
    if (!this.tableData || this.tableData.length === 0) {
      alert("No data available to export.");
      return;
    }

    const exportData = this.tableData.map((item: any) => {
      const { password, createdDt, createdBy, modifiedDt, modifiedBy, ...rest } = item;
      return {
        'Subject No': rest.subjectNo || '',
        'Patient Name': rest.initial || '',
        'Gender': rest.gender || '',
        'Age': rest.age || '',
        'Doctor': rest.doctorName || '',
        'City': rest.city || '',
        'State': rest.state || '',
        'Zone': rest.zone || '',
        'Stage': rest.stage || '',
        'Date': rest.date ? new Date(rest.date).toLocaleDateString() : '',
        'Place Type': rest.placeType || '',
        'Socioeconomic Status': rest.socioeconomicStatus || '',
      };
    });

    const worksheet: XLSX.WorkSheet = XLSX.utils.json_to_sheet(exportData);

    const workbook: XLSX.WorkBook = XLSX.utils.book_new();
    XLSX.utils.book_append_sheet(workbook, worksheet, 'Patient Data');

    const excelBuffer: any = XLSX.write(workbook, { bookType: 'xlsx', type: 'array' });

    const data: Blob = new Blob([excelBuffer], { type: 'application/octet-stream' });
    saveAs(data, `Patient_Report_${new Date().toISOString().slice(0, 10)}.xlsx`);
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

  goTofilterchart() {
    this.router.navigate(['/allReport']);
  }


  goDashboard() {
    this.router.navigate([`/admindashboard`]);

  }

  goReport() {
    this.router.navigate([`/genderReport`]);

  }

  // applyFilter(): void {

  //   if (this.selectedCategory && !this.selectedOption) {
  //     alert('Please select an sub_category for the selected category.');
  //     return;
  //   }
  //   this.filteredData = [...this.allRecords];

  //   if (this.selectedCategory && this.selectedOption) {
  //     this.filteredData = this.filteredData.filter(
  //       d => d[this.selectedCategory!] === this.selectedOption
  //     );
  //   }

  //   if (this.selectedZone) {
  //     this.filteredData = this.filteredData.filter(d => d.zone === this.selectedZone);
  //   }

  //   if (this.selectedState?.name) {
  //     this.filteredData = this.filteredData.filter(d => d.state === this.selectedState!.name);
  //   }

  //   if (this.selectedCity?.name) {
  //     this.filteredData = this.filteredData.filter(d => d.city === this.selectedCity!.name);
  //   }

  //   if (this.selectedGender) {
  //     this.filteredData = this.filteredData.filter(d => d.gender === this.selectedGender);
  //   }

  //   if (this.selectedStage) {
  //     this.filteredData = this.filteredData.filter(d => d.stage === this.selectedStage);
  //   }

  //   if (this.selectedCategory) {
  //     this.loadPiechar(this.selectedCategory, this.selectedOption || undefined);
  //   }


  // }


  applyFilter() {
  // Category + SubCategory selected
  if (this.selectedCategory && this.selectedOption) {
    this.loadPiechar(
      this.selectedCategory,
      this.selectedOption
    );
    return;
  }

  // Category only selected
  if (this.selectedCategory && !this.selectedOption) {
    this.loadPiechar(
      this.selectedCategory
    );
    return;
  }

  // No category selected → load all patients
  this.http.httpGet(API_URLS.PATIENT_REG_GET)
    .subscribe({
      next:(res:any)=>{

        let data = res?.data || [];

        // Zone filter
        if(this.selectedZone){
          data = data.filter((x:any)=>
            x.zone?.trim().toLowerCase()
            === this.selectedZone.trim().toLowerCase()
          );
        }

        // State filter
        if(this.selectedState){
          data = data.filter((x:any)=>
            x.state?.trim().toLowerCase()
            === this.selectedState?.name.trim().toLowerCase()
          );
        }

        // City filter
        if(this.selectedCity){
          data = data.filter((x:any)=>
            x.city?.trim().toLowerCase()
            === this.selectedCity?.name.trim().toLowerCase()
          );
        }

        // Gender filter
        if(this.selectedGender){
          data=data.filter((x:any)=>
            x.gender?.trim().toLowerCase()
            === this.selectedGender.trim().toLowerCase()
          );
        }

        // Stage filter
        if(this.selectedStage){
          const stageNumbers =
          this.stageMapping[this.selectedStage] || [];

          data = data.filter((x:any)=>
            stageNumbers.includes(Number(x.stage))
          );
        }

        this.tableData=data;

        // update table
        this.updatePagination();

        // update chart
        this.pieChartData={
          labels:['Patients'],
          datasets:[
            {
              data:[data.length],
              backgroundColor:['#36A2EB']
            }
          ]
        };

        this.barChartData={
          labels:['Patients'],
          datasets:[
            {
              label:'Count',
              data:[data.length],
              backgroundColor:['#36A2EB']
            }
          ]
        };

      }
    })
}
  updateCharts() {
    this.pieChartData = {
      labels: ['Filtered Data'],
      datasets: [{ data: [this.filteredData.length], backgroundColor: ['#FF6384'] }]
    };

    this.barChartData = {
      labels: ['Filtered Data'],
      datasets: [{ data: [this.filteredData.length], backgroundColor: ['#36A2EB'] }]
    };
  }
}
