// 


import { Component, ElementRef, ViewChild, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { ChartData, ChartOptions } from 'chart.js';
import html2canvas from 'html2canvas';
import jsPDF from 'jspdf';
import { FormvalidationService } from '../formvalidation.service';
import { HttpserviceService } from '../httpservice.service';
import { API_URLS } from '../shared/API-URLs';
import { BaseChartDirective } from 'ng2-charts';

@Component({
  selector: 'app-treatment-report',
  templateUrl: './treatment-report.component.html',
  styleUrls: ['./treatment-report.component.css']
})
export class TreatmentReportComponent implements OnInit {
  @ViewChild('chartContainer') chartContainer!: ElementRef;
  @ViewChild(BaseChartDirective) chart?: BaseChartDirective;

  rawData: any[] = [];
  filteredData: any[] = [];

  zones: string[] = [];
  states: any[] = [];
  cities: any[] = [];
  allStates: any[] = [];
  allCities: any[] = [];

  selectedZone = '';
  selectedState = 0;
  selectedCity = 0;

  treatmentChartData: ChartData<'pie', number[], string> = {
    labels: [],
    datasets: [
      {
        data: [],
        backgroundColor: [
          '#FF6384', '#36A2EB', '#FFCE56', '#4BC0C0', '#9966FF', '#FF9F40', '#8BC34A', '#00BCD4'
        ]
      }
    ]
  };

  treatmentChartOptions: ChartOptions<'pie'> = {
    responsive: true,
    plugins: {
      title: {
        display: true,
        text: 'Treatment Distribution',
        font: { size: 18, weight: 'bold' }
      },
      legend: {
        display: true,
        position: 'right',
        labels: {
          color: '#000',
          font: { size: 14, weight: 'bold' },
          boxWidth: 20,
          padding: 20
        }
      }
    }
  };

  constructor(
    private http: HttpserviceService,
    private formValidation: FormvalidationService,
    private route: ActivatedRoute,
    private router: Router
  ) {}

  ngOnInit() {
    this.loadStates();
    this.fetchTreatmentData();
  }

  fetchTreatmentData() {
    this.http.httpGet('/VwMedicationRpt/GetAll').subscribe({
      next: (response: any[]) => {
        this.rawData = response;
        this.filteredData = [...this.rawData];

        this.extractFilterOptions();
        this.updateChartData(this.filteredData);
      },
      error: (err: any) => {
        console.error('Error fetching treatment data', err);
      }
    });
  }

  extractFilterOptions() {
    this.zones = [...new Set(this.rawData.map(x => x.zone))];
    this.states = [...new Set(this.rawData.map(x => x.state))];
    this.cities = [...new Set(this.rawData.map(x => x.city))];
  }

  // updateChartData(data: any[]) {
  //   const medicationMap = new Map<string, number>();

  //   data.forEach(item => {
  //     const name = item.medicationName?.trim();
  //     if (name) {
  //       medicationMap.set(name, (medicationMap.get(name) || 0) + 1);
  //     }
  //   });

  //   const labels = Array.from(medicationMap.keys());
  //   const values = Array.from(medicationMap.values());

  //   this.treatmentChartData.labels = labels;
  //   this.treatmentChartData.datasets[0].data = values;

  //   this.chart?.update();
  // }

  updateChartData(data: any[]) {
  const medicationMap = new Map<string, number>();

  data.forEach(item => {
    const name = item.medicationName?.trim();
    if (name) {
      medicationMap.set(name, (medicationMap.get(name) || 0) + 1);
    }
  });

  const labels: string[] = [];
  const values: number[] = [];

  medicationMap.forEach((count, name) => {
    labels.push(`${name} (${count})`);
    values.push(count);
  });

  this.treatmentChartData.labels = labels;
  this.treatmentChartData.datasets[0].data = values;

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

  goback() {
    this.router.navigate(['/CoMorbiditiesReport']);
  }

  goToNext() {
    this.router.navigate(['/admindashboard']);
  }

  downloadReport() {
    html2canvas(this.chartContainer.nativeElement).then(canvas => {
      const imgData = canvas.toDataURL('image/png');
      const pdf = new jsPDF('p', 'mm', 'a4');
      const pdfWidth = pdf.internal.pageSize.getWidth();
      const imgProps = pdf.getImageProperties(imgData);
      const imgHeight = (imgProps.height * pdfWidth) / imgProps.width;
      pdf.addImage(imgData, 'PNG', 0, 10, pdfWidth, imgHeight);
      pdf.save('treatment-report.pdf');
    });
  }

  login(){
    this.router.navigate(['/login']);
  }
  goToCoMorbiditiesReport() {
    this.router.navigate([`/CoMorbiditiesReport`]);
  }
  goToGnederreport(){
    this.router.navigate([`/genderReport`]);
  }
   goDoctorlist(){  
    this.router.navigate(['/doctor-list']);
  }
   goTocontactUs(){
    this.router.navigate(['/contact-us']);
  }
}
