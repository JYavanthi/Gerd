import { Component, OnInit, ViewChild } from '@angular/core';
import { Router } from '@angular/router';
import { ChartOptions } from 'chart.js';
import { GenderRptService } from '../Services/gender-rpt.service';
import { BaseChartDirective } from 'ng2-charts';
import { API_URLS } from '../shared/API-URLs';
import { HttpserviceService } from '../httpservice.service';
import { FormvalidationService } from '../formvalidation.service';


@Component({
  selector: 'app-gender-report',
  templateUrl: './gender-report.component.html',
  styleUrls: ['./gender-report.component.css']
})
export class GenderReportComponent implements OnInit {
  @ViewChild(BaseChartDirective) chart?: BaseChartDirective;
  //  @ViewChild(BaseChartDirective) chart?: BaseChartDirective;
  rawData: any[] = [];
  filteredData: any[] = [];

  states: any = [];
  cities: any;
  allStates: any[] = [];
  allCities: any[] = [];
  zones: string[] = [];
  // states: number[] = [];
  // cities: number[] = [];
  
  selectedZone = '';
  selectedState = 0;
  selectedCity = 0;

  genderChartLabels = ['Male', 'Female'];
  genderChartData = {
    labels: this.genderChartLabels,
    datasets: [
      {
        data: [0, 0],
        backgroundColor: ['#36A2EB', '#FFCE56'],
      },
    ],
  };

  genderChartOptions: ChartOptions<'pie'> = {
    responsive: true,
    plugins: {
      title: {
        display: true,
        text: 'Gender Distribution',
        font: {
          size: 18,
          weight: 'bold'
        }
      },
      legend: {
        display: true,
        position: 'right',
        labels: {
          color: '#000',
          font: {
            size: 14,
            weight: 'bold'
          },
          boxWidth: 20,
          padding: 20,
          generateLabels: (chart) => {
            const data = chart.data;
            if (!data.labels || !data.datasets.length) return [];

            const dataset = data.datasets[0];
            const total = (dataset.data as number[]).reduce((sum, val) => sum + val, 0);

            return data.labels.map((label: any, index: number) => {
              const value = Number(dataset.data[index]);
              const percentage = total ? ((value / total) * 100).toFixed(1) : 0;

              return {
                text: `${label} (${value})`,  // <-- Only percentage number here
                fillStyle: (dataset.backgroundColor as string[])[index],
                strokeStyle: (dataset.backgroundColor as string[])[index],
                index,
              };
            });
          }
        }
      },
      tooltip: {
        callbacks: {
          label: function (context) {
            const dataset = context.dataset;
            const total = dataset.data.reduce((sum, val) => sum + Number(val), 0);
            const currentValue = Number(dataset.data[context.dataIndex]);
            const percentage = total ? ((currentValue / total) * 100).toFixed(1) : 0;
            return `${context.label}: ${percentage}%`;
          }
        }
      }
    }
  };

  constructor(
    private router: Router,
    private genderService: GenderRptService,
    private http: HttpserviceService,
    private formValidation: FormvalidationService,
  ) { }

ngOnInit(): void {
  this.genderService.getGenderReport().subscribe(res => {
    if (res.success && res.data) {
      this.rawData = res.data;
      this.filteredData = [...this.rawData];
      this.extractFilterOptions();
      this.updateChartData(this.filteredData);
      this.chart?.update(); // <-- Ensure chart updates after setting data
      this.loadStates();
    }
  });
}

  extractFilterOptions() {
    this.zones = [...new Set(this.rawData.map(x => x.zone))];
    this.states = [...new Set(this.rawData.map(x => x.state))];
    this.cities = [...new Set(this.rawData.map(x => x.city))];
  }

 updateChartData(data: any[]) {
  const maleCount = data.filter(x => x.gender?.toLowerCase() === 'male').length;
  const femaleCount = data.filter(x => x.gender?.toLowerCase() === 'female').length;
  this.genderChartData.datasets[0].data = [maleCount, femaleCount];
  this.chart?.update();  // <-- Add this here
}

  filterAllIndia() {
  this.filteredData = [...this.rawData];

  this.selectedZone = '';
  this.selectedState = 0;
  this.selectedCity = 0;

  this.states = [...this.allStates];  // Reset full list of states
  this.cities = [...this.allCities];  // Reset full list of cities

  this.updateChartData(this.filteredData);
  this.chart?.update(); // refresh chart
}


  filterByZone(zone: string) {
    this.filteredData = this.rawData.filter(x => x.zone === zone);
    this.selectedZone = zone;
    this.updateChartData(this.filteredData);
  }

  filterByState(state: number) {
    this.filteredData = this.rawData.filter(x => x.state === state);
    this.selectedState = state;
    this.updateChartData(this.filteredData);
  }

  filterByCity(city: number) {
    this.filteredData = this.rawData.filter(x => x.city === city);
    this.selectedCity = city;
    this.updateChartData(this.filteredData);
  }

  goDashboard() {
    this.router.navigate([`/admindashboard`]);
  }

  goToNext() {
    this.router.navigate([`/CoMorbiditiesReport`]);
  }

  loadStates() {
    this.http.httpGet(API_URLS.STATE_GET).subscribe({
      next: (res: any) => {
        this.allStates = res;
        this.states = res; // Default all states
      },
      error: (err) => {
        this.formValidation.showAlert('Error loading states', 'danger');
        console.error(err);
      }
    });
  }

  getCities(event: any) {
    const stateId = event.target.value;
    this.http.httpGet(API_URLS.CITY_GET, { stateId }).subscribe({
      next: (res: any) => {
        this.allCities = res;
        this.cities = res; // Default all cities
      },
      error: (err) => {
        this.formValidation.showAlert('Error loading cities', 'danger');
        console.error(err);
      }
    });
  }
  onZoneChange(event: any) {
    this.selectedZone = event.target.value;

    // Get unique state IDs for the selected zone
    const stateIdsInZone = [...new Set(
      this.rawData
        .filter(item => item.zone === this.selectedZone)
        .map(item => item.state)
    )];

    // Filter the states from full list
    this.states = this.allStates.filter(state => stateIdsInZone.includes(state.id));

    // Reset selected state/city
    this.selectedState = 0;
    this.selectedCity = 0;
    this.cities = [];

    this.applyCombinedFilters();
  }

  onStateChange(event: any) {
    this.selectedState = +event.target.value;

    // Load cities for the selected state
    this.http.httpGet(API_URLS.CITY_GET, { stateId: this.selectedState }).subscribe({
      next: (res: any) => {
        this.allCities = res;

        // Now filter cities based on zone as well
        if (this.selectedZone) {
          const cityIdsInZone = new Set(
            this.rawData
              .filter(item => item.zone === this.selectedZone && item.state === this.selectedState)
              .map(item => item.city)
          );
          this.cities = this.allCities.filter(city => cityIdsInZone.has(city.id));
        } else {
          this.cities = this.allCities;
        }

        this.selectedCity = 0; // reset city
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
    this.chart?.update();
  }
login(){
    this.router.navigate(['/login']);
  }

  goToCoMorbiditiesReport() {
    this.router.navigate([`/CoMorbiditiesReport`]);
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
