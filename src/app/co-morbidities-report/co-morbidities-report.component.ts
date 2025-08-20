
// import { Component, OnInit } from '@angular/core';
// import { Router } from '@angular/router';
// import { ChartData, ChartOptions } from 'chart.js';

// import { ComorbidityService } from '../Services/comorbidity.service';
// import { Comorbidity } from '../module/comorbidity.model';

// @Component({
//   selector: 'app-co-morbidities-report',
//   templateUrl: './co-morbidities-report.component.html',
//   styleUrls: ['./co-morbidities-report.component.css']
// })
// export class CoMorbiditiesReportComponent implements OnInit {

//   coMorbiditiesChartData: ChartData<'pie', number[], string> = {

//     datasets: [{
//       data: [],
//       backgroundColor: ['#36A2EB', '#FFCE56', '#4BC0C0', '#FF6384', '#00b894']
//     }]
//   };

//   coMorbiditiesChartOptions: ChartOptions<'pie'> = {
//     responsive: true,
//     plugins: {
//       legend: { display: false },
//       title: {
//         display: true,
//         text: 'Co-morbidities',
//         font: { size: 18, weight: 'bold' }
//       },
//       tooltip: {
//         callbacks: {
//           label: (context) => {
//             const label = context.label || '';
//             const value = context.parsed;
//             return `${label}: ${value}%`;
//           }
//         }
//       }
//     }
//   };

//   constructor(
//     private router: Router,
//     private comorbidityService: ComorbidityService
//   ) { }

//   ngOnInit(): void {
//     this.loadComorbiditiesData();
//   }

//   loadComorbiditiesData(): void {
//     this.comorbidityService.getComorbiditiesData().subscribe((data: Comorbidity[]) => {
//       const comorbidityMap: { [key: string]: string } = {
//         ddPresent: 'Developmental Delay',
//         dbPresent: 'Diabetes',
//         cldPresent: 'Chronic Lung Disease',
//         ndPresent: 'Neurological Disorder',
//         bdPresent: 'Birth Defect'
//       };

//       // Initialize counts
//       const counts: { [label: string]: number } = {};
//       Object.values(comorbidityMap).forEach(label => counts[label] = 0);

//       data.forEach((item:any) => {
//         for (const [key, label] of Object.entries(comorbidityMap)) {
//           if (item[key] === "true") {
//             counts[label]++;
//           }
//         }
//       });

//       const labels = Object.keys(counts).filter(label => counts[label] > 0);
//       const values = labels.map(label => counts[label]);

//       this.coMorbiditiesChartData = {
//         labels,
//         datasets: [{
//           data: values,
//           backgroundColor: ['#36A2EB', '#FFCE56', '#4BC0C0', '#FF6384', '#00b894']
//         }]
//       };
//     });
//   }

//   goDashboard() {
//     this.router.navigate(['/admindashboard']);
//   }

//   goToNext() {
//     this.router.navigate(['/treatmentReport']);
//   }

//   goback() {
//     this.router.navigate(['/genderReport']);
//   }
// }

import { Component, OnInit, ViewChild } from '@angular/core';
import { Router } from '@angular/router';
import { ChartData, ChartOptions } from 'chart.js';
import { BaseChartDirective } from 'ng2-charts';

import { ComorbidityService } from '../Services/comorbidity.service';
import { HttpserviceService } from '../httpservice.service';
import { FormvalidationService } from '../formvalidation.service';
import { API_URLS } from '../shared/API-URLs';
import { Comorbidity } from '../module/comorbidity.model';

@Component({
  selector: 'app-co-morbidities-report',
  templateUrl: './co-morbidities-report.component.html',
  styleUrls: ['./co-morbidities-report.component.css']
})
export class CoMorbiditiesReportComponent implements OnInit {
  @ViewChild(BaseChartDirective) chart?: BaseChartDirective;

  rawData: Comorbidity[] = [];
  filteredData: Comorbidity[] = [];

  zones: string[] = [];
  allStates: any[] = [];
  allCities: any[] = [];

  states: any[] = [];
  cities: any[] = [];

  selectedZone: string = '';
  selectedState: number = 0;
  selectedCity: number = 0;

  coMorbiditiesChartData: ChartData<'pie', number[], string> = {
    labels: [],
    datasets: [{
      data: [],
      backgroundColor: ['#36A2EB', '#FFCE56', '#4BC0C0', '#FF6384', '#00b894']
    }]
  };

  // coMorbiditiesChartOptions: ChartOptions<'pie'> = {
  //   responsive: true,
  //   plugins: {
  //     legend: { display: true, position: 'right' },
  //     title: {
  //       display: true,
  //       text: 'Co-morbidities',
  //       font: { size: 18, weight: 'bold' }
  //     },
  //     tooltip: {
  //       callbacks: {
  //         label: (context) => {
  //           const label = context.label || '';
  //           const value = context.parsed;
  //           return `${label}: ${value}`;
  //         }
  //       }
  //     }
  //   }
  // };


  coMorbiditiesChartOptions: ChartOptions<'pie'> = {
  responsive: true,
  plugins: {
    legend: {
      display: true,
      position: 'right',
      labels: {
        generateLabels: (chart) => {
          const data = chart.data;
          if (!data.labels || !data.datasets.length) return [];

          const dataset = data.datasets[0];
          const counts = dataset.data as number[];

          return data.labels.map((label: any, index: number) => {
            const count = counts[index];
            return {
              text: `${label} (${count})`,
              fillStyle: (dataset.backgroundColor as string[])[index],
              strokeStyle: (dataset.backgroundColor as string[])[index],
              index
            };
          });
        },
        color: '#000',
        font: {
          size: 12,
          weight: 'bold'
        },
        boxWidth: 20,
        padding: 20
      }
    },
    title: {
      display: true,
      text: 'Co-morbidities',
      font: { size: 18, weight: 'bold' }
    },
    tooltip: {
      callbacks: {
        label: (context) => {
          const label = context.label || '';
          const value = context.parsed;
          const dataset = context.dataset;
          const total = dataset.data.reduce((sum, val) => sum + Number(val), 0);
          const percentage = total ? ((value / total) * 100).toFixed(1) : 0;
          return `${label}: ${value} (${percentage}%)`;
        }
      }
    }
  }
};

  constructor(
    private router: Router,
    private comorbidityService: ComorbidityService,
    private http: HttpserviceService,
    private formValidation: FormvalidationService
  ) { }

  ngOnInit(): void {
    this.comorbidityService.getComorbiditiesData().subscribe((data: Comorbidity[]) => {
      this.rawData = data;
      this.filteredData = [...this.rawData];
      this.extractFilterOptions();
      this.updateChartData(this.filteredData);
      this.loadStates();
    });
  }

  extractFilterOptions() {
    this.zones = [...new Set(this.rawData.map(x => x.zone))];
    this.states = [...new Set(this.rawData.map(x => x.state))];
    this.cities = [...new Set(this.rawData.map(x => x.city))];
  }

  updateChartData(data: Comorbidity[]) {
    const comorbidityMap: { [key: string]: string } = {
      ddPresent: 'Developmental Delay',
      dbPresent: 'Diabetes',
      cldPresent: 'Chronic Lung Disease',
      ndPresent: 'Neurological Disorder',
      bdPresent: 'Birth Defect'
    };

    const counts: { [label: string]: number } = {};
    Object.values(comorbidityMap).forEach(label => counts[label] = 0);

    data.forEach(item => {
      for (const [key, label] of Object.entries(comorbidityMap)) {
        if ((item as any)[key] === "true") {
          counts[label]++;
        }
      }
    });

    const labels = Object.keys(counts).filter(label => counts[label] > 0);
    const values = labels.map(label => counts[label]);

    this.coMorbiditiesChartData = {
      labels,
      datasets: [{
        data: values,
        backgroundColor: ['#36A2EB', '#FFCE56', '#4BC0C0', '#FF6384', '#00b894']
      }]
    };

    this.chart?.update();
  }

  filterAllIndia() {
    this.filteredData = [...this.rawData];
    this.selectedZone = '';
    this.selectedState = 0;
    this.selectedCity = 0;

    this.states = [...this.allStates];
    this.cities = [...this.allCities];

    this.updateChartData(this.filteredData);
  }

  onZoneChange(event: any) {
    this.selectedZone = event.target.value;

    const stateIdsInZone = [...new Set(
      this.rawData.filter(item => item.zone === this.selectedZone).map(item => item.state)
    )];

    this.states = this.allStates.filter(state => stateIdsInZone.includes(state.id));

    this.selectedState = 0;
    this.selectedCity = 0;
    this.cities = [];

    this.applyCombinedFilters();
  }

  onStateChange(event: any) {
    this.selectedState = +event.target.value;

    this.http.httpGet(API_URLS.CITY_GET, { stateId: this.selectedState }).subscribe({
      next: (res: any) => {
        this.allCities = res;

        if (this.selectedZone) {
          const cityIdsInZone = new Set(
            this.rawData.filter(item => item.zone === this.selectedZone && item.state === this.selectedState).map(item => item.city)
          );
          this.cities = this.allCities.filter(city => cityIdsInZone.has(city.id));
        } else {
          this.cities = this.allCities;
        }

        this.selectedCity = 0;
        this.applyCombinedFilters();
      },
      error: (err) => {
        this.formValidation.showAlert('Error loading cities', 'danger');
        console.error(err);
      }
    });
  }

  onCityChange(event: any) {
    this.selectedCity = +event.target.value;
    this.applyCombinedFilters();
  }

  applyCombinedFilters() {
    this.filteredData = this.rawData.filter(item => {
      return (!this.selectedZone || item.zone === this.selectedZone) &&
        (!this.selectedState || item.state === this.selectedState) &&
        (!this.selectedCity || item.city === this.selectedCity);
    });

    this.updateChartData(this.filteredData);
  }

  loadStates() {
    this.http.httpGet(API_URLS.STATE_GET).subscribe({
      next: (res: any) => {
        this.allStates = res;
        this.states = res;
      },
      error: (err) => {
        this.formValidation.showAlert('Error loading states', 'danger');
        console.error(err);
      }
    });
  }

  goDashboard() {
    this.router.navigate(['/admindashboard']);
  }

  goToNext() {
    this.router.navigate(['/treatmentReport']);
  }

  goback() {
    this.router.navigate(['/genderReport']);
  }
  login(){
    this.router.navigate(['/login']);
  }


  goToGnederreport(){
    this.router.navigate([`/genderReport`]);
  }
  goTotreatmentReport() {
    this.router.navigate(['/treatmentReport']);
  }
   goDoctorlist(){  
    this.router.navigate(['/doctor-list']);
  }
   goTocontactUs(){
    this.router.navigate(['/contact-us']);
  }
}
