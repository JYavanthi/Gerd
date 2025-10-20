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
  age: number = 0;
  caseSub!: Subscription;
  tableData: any[] = [];
  doctorlist: any[] = [];
  // patient: any[] = [];
  isViewMode = false;
  Math = Math;
  currentPage = 1;
  itemsPerPage = 10;
  totalItems = 0;
  paginatedData: Case[] = [];
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
    "Computer Usage (hrs/day)": ["0-1", "1-2", "2-3", "3-4", "4-6", "6-8", "8-10", "10-24"],
    "Computer Usage Duration (years)": ["0-1", "1-5", "5-10", "10-15", "15-20", "20-100"],
    "Smartphone Use": ["Yes", "No"],
    "Smartphone Usage (hrs/day)": ["0-1", "1-2", "2-3", "3-4", "4-6", "6-8", "8-10", "10-24"],
    "Smartphone Usage Duration (years)": ["0-1", "1-5", "5-10", "10-15", "15-20", "20-100"],
    "Working Hours (Occupation)": ["4.00 am to 12.00 noon (Early Morning shift)", "6.00 am to 3.00 pm (Morning shift)", "9.00 am to 6.00 pm (General shift)", "12.00 noon to 8.00 pm (Afternoon shift)", "8.00 pm to 8.00 am (Night shift)"],
    "Job/ Occupation type": ["Sedentary", "Non sedentary"],
    "Duration (No. of years in the above working hours)": ["0-1", "1-5", "5-10", "10-15", "15-20", "20-100"],
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
    labels: ['male','female','other'],
    datasets: [
      {
        data: [300, 500, 700],
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
  barChartData: ChartData<'bar'> = {
    labels: ['18-30', '31-40', '41-50', '51-60', '61-70', '71-80', '>80'],
    datasets: [
      { label: 'gender', data: [65, 59, 80, 81, 56], backgroundColor: '#36A2EB' }
    ]
  };
  barChartOptions: ChartOptions<'bar'> = {
    responsive: true,
    scales: {
      y: { beginAtZero: true }
    },
    plugins: {
      legend: { position: 'top' }
    }
  };
  constructor(private https: HttpClient, private http: HttpserviceService, private router: Router, private caseDataService: CaseDataService
  ) { }

  private routerSub!: Subscription;
  ngOnInit() {
    this.selectedCategory = null;
    this.selectedOption = null;
    //this.availableOptions = [];
    this.getStates();
    this.getCities();
    this.getPatientList();

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

    

  getStage(c: Case): number {
    const bl = Number(c['blsubmitted'] ?? 0);
    const fu1 = Number(c['fu1submitted'] ?? 0);
    const fu2 = Number(c['fu2submitted'] ?? 0);

    if (fu2 === 1) return 5;
    if (fu1 === 1) return 3;
    if (bl === 1) return 1;
    return 0;
  }



  onCategorySelect(category: string) {
    this.selectedCategory = category;
    this.availableOptions = this.data[category];
    this.selectedOption = null;
    this.showOptionDropdown = true;

   this.loadPiechar(category);
  }


//   searchCharts() {
//   if (!this.selectedCategory) return;

//   // Update charts
// this.loadPiechar(this.selectedCategory, this.selectedOption || undefined);

//   this.filterTableData();
// }

filterTableData() {
  this.paginatedData = this.tableData.filter(row => {
    let matches = true;
    if (this.selectedCategory) {
      // Replace 'categoryField' with your actual field mapping
      matches = matches && row[this.selectedCategory] === this.selectedOption;
    }
    return matches;
  });
}

  onOptionSelect(option: string) {
    this.selectedOption = option;
    console.log(`Selected: ${this.selectedCategory} → ${option}`); // <-- use backticks

    if (this.selectedCategory && this.selectedOption) {
      this.loadPiechar(this.selectedCategory, this.selectedOption);
      
    }
  }


  toggleSelection(item: any, type: string) {
    switch (type) {
      case 'zone':
        this.selectedZone = item;
        this.selectedState = null;
        this.selectedCity = null;
        this.getStates();  // fetch states filtered by selected zone
        this.cities = [];
        break;
      case 'state':
        this.selectedState = item;
        this.selectedCity = null;
        this.getCities();  // fetch cities filtered by selected state
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

    this.loadPiechar(this.selectedCategory!, this.selectedOption!);
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
        // Sort by date descending
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

  goToPage(page: number) {
    if (page >= 1 && page <= this.totalPages) {
      this.currentPage = page;
      this.updatePaginatedData();
      this.generatePageNumbers();
    }
  }

  goToPrevious() { if (this.currentPage > 1) this.goToPage(this.currentPage - 1); }
  goToNext() { if (this.currentPage < this.totalPages) this.goToPage(this.currentPage + 1); }
  onItemsPerPageChange(event: any) { this.itemsPerPage = +event.target.value; this.currentPage = 1; this.updatePagination(); }


  navigateToAddCase() { this.router.navigate([`/demographic/0/0`]); }

  ngOnDestroy() { this.caseSub?.unsubscribe(); this.routerSub?.unsubscribe(); }


  downloadAllStages(patientID: any) {
    this.router.navigate([`/case-stage-view/${patientID}`]);
  }

  categoryPropMap: Record<string, string> = {
    "Age": "age",
    "Education": "education",
    "Occupation": "occupation",
    "Place Type": "placeType",
    "Socioeconomic Status": "socioeconomicStatus",
    "Annual Family Income (Rupees)": "familyIncome",
    "Chief complaints": "chiefComplaint",
    "Heartburn": "rNocturnal",
    "Regurgitation": "rNocturnalq",
    "Retrosternal Pain": "retrosternalPain",
    "Acid Taste in mouth": "acidTasteInMouth",
    "Diet": "Diet",
  };


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

    const field = this.categoryPropMap[category] || category;

    this.http.httpGet(apiUrl).subscribe({
      next: (res: any) => {
        const data = res?.data || [];



        const normalizeValue = (category: string, val: any) => {

          const key = `${category} (${val})`;
          if (category === 'Education') {
            if (val === "10th Std & Above") return "Above Tenth standard";
            if (val === "Below 10th") return "Below Tenth standard";
          }

          const symptomMap: Record<string, Record<string, string>> = {
            'Heartburn': { 'Postural': 'hbPostural', 'Nocturnal': 'hbNocturnal' },
            'Regurgitation': { 'Postural': 'rPostural', 'Nocturnal': 'rNocturnal' },
            'Retrosternal Pain': { 'Postural': 'rpPostural', 'Nocturnal': 'rpNocturnal' },
            'Acid Taste in mouth': { 'Postural': 'atPostural', 'Nocturnal': 'atNocturnal' },
          };

          const comorbiditiesMap: Record<string, string> = {
            'Hypertension': 'htPresent', 'Diabetes': 'dbPresent', 'Hyperlipidemia': 'hlPresent',
            'Obesity': 'oPresent', 'Asthma': 'aPresent', 'COPD': 'cPresent', 'Heart Disease': 'hPresent',
            'Kidney Disease': 'ckdPresent', 'Liver Disease': 'cldPresent', 'Thyroid Disorder': 'htdPresent',
            'Rheumatoid Arthritis': 'raPresent', 'Sickle Cell': 'ssPresent', 'Congenital Disease': 'cmoPresent',
            'Other': 'bdPresent'
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


          const diagnosisFieldMap: Record<string, string> = {
            'Newly Diagnosed (Yes)': 'ndPresent',
            'Newly Diagnosed (No)': 'ndAbsent',
            'Newly Diagnosed (Male)': 'ndMale',
            'Newly Diagnosed (Female)': 'ndFemale',
            'Known case of GERD (Yes)': 'dbPresent',
            'Known case of GERD (No)': 'dbPresent',
            'Known case of GERD (Male)': 'gerdMale',
            'Known case of GERD (Female)': 'gerdFemale',
            'GERDType (Erosive GERD)': 'ddPresent',
            'GERDType (Non-Erosive GERD)': 'ddPresent',
            'RefractorytoPPI (Yes)': 'cldPresent',
            'RefractorytoPPI (No)': 'cldPresent',
            'AdherencetoTherapy (Yes)': 'ndPresent',
            'AdherencetoTherapy (No)': 'ndPresent'
          };

          const SleepMap: Record<string, string> = {
            'Sleep Apnea (Yes)': 'sleepApneayes',
            'Sleep Apnea (No)': 'sleepApneano',
            'Exercise (Yes)': 'exerciseIntakeyes',
            'Exercise (No)': 'exerciseIntakeno',
            'Jogging': 'joggingSelectedyes',
            'Gym': 'gymSelectedyes',
            'Yoga': 'yogaSelectedyes',
            'Walking': 'walkingSelectedyes',
            'Aerobics': 'aerobicsyes',
            'Zumba': 'zumbayes',
            'Others': 'othersyes'
          };

          const GadgetMap: Record<string, string> = {
            'Computer Use (Yes)': 'computerUsed',
            'Computer Use (No)': 'computerNotUsed',
            'Smartphone Use (Yes)': 'smartphoneUsed',
            'Smartphone Use (No)': 'smartphoneNotUsed',
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
            'Diet modification': 'lifestyleRecommendations',
            'Moderation of alcohol': 'lifestyleRecommendations',
            'Weight loss': 'lifestyleRecommendations',
            'Regular exercise': 'lifestyleRecommendations',
            'Stop Tobacco use': 'lifestyleRecommendations',

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
            return diagnosisFieldMap[key] || val;
          }
          if (category === 'Sleep Apnea') return SleepMap[val] || val;
          if (GERDGISTORYMAP[category]) return GERDGISTORYMAP[category];
          if (['Computer Use', 'Smartphone Use'].includes(category)) return GadgetMap[`${category} (${val})`] || val;
          if (familyHistoryMap[category]) return familyHistoryMap[category];
          if (category === 'Lifestyle Recommendations' || category === 'Drug Therapy Advised') {
            return managementMap[val] || val;
          }
          if (diagnosisFieldMap[key]) return diagnosisFieldMap[key];
          return symptomMap[category]?.[val] || val;

        };



        const isYes = (value: any) => value === true || value === 'true' || value?.toString().toLowerCase() === 'yes';

        const normalizedOptions = this.availableOptions.map(opt => normalizeValue(category, opt));

        const optionCounts: Record<string, number> = {};
        normalizedOptions.forEach(opt => optionCounts[opt] = 0);

        const booleanCategoriesNormalized = [
          'htPresent', 'dbPresent', 'hlPresent', 'oPresent', 'aPresent', 'cPresent', 'hPresent',
          'ckdPresent', 'cldPresent', 'htdPresent', 'raPresent', 'ssPresent', 'cmoPresent', 'bdPresent',
          'dietVegetarian', 'dietNonVegetarian',
          'aeratedIntake', 'coffeeIntake', 'teaIntake', 'spicyIntake', 'alcoholIntake', 'sweetsIntake', 'smokingIntake', 'tobaccoIntake',
          'sleepApneayes', 'sleepApneano', 'exerciseIntakeyes', 'exerciseIntakeno', 'joggingSelectedyes', 'gymSelectedyes', 'yogaSelectedyes', 'walkingSelectedyes', 'aerobicsyes', 'zumbayes', 'othersyes',
          'computerUsed', 'computerNotUsed', 'smartphoneUsed', 'smartphoneNotUsed',
          'fhGred', 'fhEgc', 'ghPpi',
          'historyofEndoscopy', 'historyofGs', 'gsBariatricSurgery', 'gsFundoplicationSurgery', 'gsGastricPoemsurgery', 'gsGastrojejunostomy', 'gsOther',
          'ndPresent', 'ndAbsent', 'ndMale', 'ndFemale',
          'dbPresent', 'dbPresent', 'gerdMale', 'gerdFemale',
          'ddPresent', 'ddPresent',
          'cldPresent', 'cldPresent',
          'ndPresent', 'ndPresent'
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
                  this.tableData = filteredData; // overwrite res if needed
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
            // normalize the option the same way as your data
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
          }


          else if (category === 'Computer Usage (hrs/day)') {

            if (option && option.includes('-')) {
              const computerFrequencygrp = option.split('-').map(x => x.trim()); // remove spaces

              if (computerFrequencygrp.length === 2) {
                const mincomputerFrequency = parseInt(computerFrequencygrp[0], 10);
                const maxcomputerFrequency = parseInt(computerFrequencygrp[1], 10);
                if (!isNaN(mincomputerFrequency) && !isNaN(maxcomputerFrequency)) {

                  const filteredData = data.filter((r: any) => r.computerFrequency >= Number(mincomputerFrequency) && r.computerFrequency <= Number(maxcomputerFrequency));
                  this.tableData = filteredData; // overwrite res if needed
                } else {
                  console.error('Invalid age numbers in range:', computerFrequencygrp);
                }
              } else {
                console.error('Age range does not have two numbers:', computerFrequencygrp);
              }
            } else {
              console.error('Option is empty or invalid:', option);
            }
          }
          else if (category === 'Smartphone Usage (hrs/day)') {

            if (option && option.includes('-')) {
              const smartphoneFrequencygrp = option.split('-').map(x => x.trim()); // remove spaces

              if (smartphoneFrequencygrp.length === 2) {
                const minsmartphoneFrequencygrp = parseInt(smartphoneFrequencygrp[0], 10);
                const maxsmartphoneFrequencygrp = parseInt(smartphoneFrequencygrp[1], 10);
                if (!isNaN(minsmartphoneFrequencygrp) && !isNaN(maxsmartphoneFrequencygrp)) {
                  const filteredData = data.filter((r: any) => r.smartphoneFrequency >= Number(minsmartphoneFrequencygrp) && r.smartphoneFrequency <= Number(maxsmartphoneFrequencygrp));
                  this.tableData = filteredData; // overwrite res if needed
                } else {
                  console.error('Invalid smartphoneFrequencygrp numbers in range:', smartphoneFrequencygrp);
                }
              } else {
                console.error('smartphoneFrequencygrp range does not have two numbers:', smartphoneFrequencygrp);
              }
            } else {
              console.error('Option is empty or invalid:', option);
            }
          }

          else if (category === 'Computer Usage Duration (years)') {

            if (option && option.includes('-')) {
              const computerDurationgrp = option.split('-').map(x => x.trim()); // remove spaces

              if (computerDurationgrp.length === 2) {
                const mincomputerDuration = parseInt(computerDurationgrp[0], 10);
                const maxcomputerDuration = parseInt(computerDurationgrp[1], 10);
                if (!isNaN(mincomputerDuration) && !isNaN(maxcomputerDuration)) {

                  const filteredData = data.filter((r: any) => r.computerFrequency >= Number(mincomputerDuration) && r.computerFrequency <= Number(maxcomputerDuration));
                  this.tableData = filteredData; // overwrite res if needed
                } else {
                  console.error('Invalid age numbers in range:', computerDurationgrp);
                }
              } else {
                console.error('Age range does not have two numbers:', computerDurationgrp);
              }
            } else {
              console.error('Option is empty or invalid:', option);
            }
          }

          else if (category === 'Working Hours (Occupation)') {
            this.tableData = data.filter((r: any) => r.workingHours === option);
            this.pieChartData = {
              labels: [option],
              datasets: [{ data: [this.tableData.length], backgroundColor: ['#36A2EB'] }]
            };
          }


          else if (category === 'Lifestyle Recommendations' || category === 'Drug Therapy Advised') {
            const field = normalizeValue(category, option);
            this.tableData = data.filter((item: any) => item[field] && item[field].toString().trim() !== '' && item[field] !== 0);
            this.pieChartData = {
              labels: [option],
              datasets: [{ data: [this.tableData.length], backgroundColor: ['#36A2EB'] }]
            };
          }



          else if (category === 'Los Angeles Grade') {
            this.tableData = data.filter((r: any) => r.eeAngelesGrade === option);
            this.pieChartData = {
              labels: [option],
              datasets: [{ data: [this.tableData.length], backgroundColor: ['#36A2EB'] }]
            };
          }


          else if (category === 'Hill’s classification Grade') {
            this.tableData = data.filter((r: any) => r.eeHillClassificationGrade === option);
            this.pieChartData = {
              labels: [option],
              datasets: [{ data: [this.tableData.length], backgroundColor: ['#36A2EB'] }]
            };
          }

          else if (category === 'Job/ Occupation type') {
            this.tableData = data.filter((r: any) => r.jobType === option);
            this.pieChartData = {
              labels: [option],
              datasets: [{ data: [this.tableData.length], backgroundColor: ['#FF6384'] }]
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
              } else {
                console.error('Invalid working years range:', option);
              }
            } else {
              console.error('Option is empty or invalid:', option);
            }
          }

          else if (category === 'Smartphone Usage Duration (years)') {

            if (option && option.includes('-')) {
              const SmartphoneDurationgrp = option.split('-').map(x => x.trim()); // remove spaces

              if (SmartphoneDurationgrp.length === 2) {
                const minSmartphoneDuration = parseInt(SmartphoneDurationgrp[0], 10);
                const maxSmartphoneDuration = parseInt(SmartphoneDurationgrp[1], 10);
                if (!isNaN(minSmartphoneDuration) && !isNaN(maxSmartphoneDuration)) {

                  const filteredData = data.filter((r: any) => r.computerFrequency >= Number(minSmartphoneDuration) && r.computerFrequency <= Number(maxSmartphoneDuration));
                  this.tableData = filteredData; // overwrite res if needed
                } else {
                  console.error('Invalid age numbers in range:', SmartphoneDurationgrp);
                }
              } else {
                console.error('Age range does not have two numbers:', SmartphoneDurationgrp);
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
          }

          if (this.selectedZone) {

            this.tableData = this.tableData.filter(item => item.zone.trim() === this.selectedZone.trim());
            this.pieChartData = {
              labels: [option],
              datasets: [{ data: [this.tableData.length], backgroundColor: ['#4BC0C0'] }]
            };
          }

          if (this.selectedState) {
            this.tableData = this.tableData.filter(item => item.state.trim() === this.selectedState?.name.trim());
            this.pieChartData = {
              labels: [option],
              datasets: [{ data: [this.tableData.length], backgroundColor: ['#4BC0C0'] }]
            };
          }

          if (this.selectedCity) {
            // alert('this.selectedCity' + this.selectedCity?.name)
            this.tableData = this.tableData.filter(item => item.city.trim() === this.selectedCity?.name.trim());
            // alert('this.selectedState?.name length' + this.tableData.length)
            this.pieChartData = {
              labels: [option],
              datasets: [{ data: [this.tableData.length], backgroundColor: ['#4BC0C0'] }]
            };
          }


          if (this.selectedGender) {
            this.tableData = this.tableData.filter(item => item.gender.trim() === this.selectedGender.trim());
            this.pieChartData = {
              labels: [option],
              datasets: [{ data: [this.tableData.length], backgroundColor: ['#4BC0C0'] }]
            };
          }

          if (this.selectedStage) {
            const stageNumbers = this.stageMapping[this.selectedStage] || [];
            this.tableData = this.tableData.filter(item => stageNumbers.includes(Number(item.stage)));

            this.pieChartData = {
              labels: [option],
              datasets: [{ data: [this.tableData.length], backgroundColor: ['#4BC0C0'] }]
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



}
