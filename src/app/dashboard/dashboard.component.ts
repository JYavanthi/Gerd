import { Component, OnInit, OnDestroy, ViewChild } from '@angular/core';
import { BaseChartDirective } from 'ng2-charts';
import { Router } from '@angular/router';
import { ChartData, ChartOptions } from 'chart.js';
import { Subscription } from 'rxjs';
import { Case } from '../Services/case-data.services';
import { PatientService } from '../Services/patient.service';
import { API_URLS } from '../shared/API-URLs';
import { HttpserviceService } from '../httpservice.service';
import { StageService } from '../Services/StageService.service';

@Component({
  selector: 'app-dashboard',
  templateUrl: './dashboard.component.html',
  styleUrls: ['./dashboard.component.css']
})
export class DashboardComponent implements OnInit, OnDestroy {
  @ViewChild(BaseChartDirective) chart?: BaseChartDirective;
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
  states: any[] = [];
  selectedState: string = '';
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
  doctorId: any = '';
  pieChartLabels: string[] = ['Completed', 'Pending'];
  pieChartData: ChartData<'pie', number[], string> = {
    labels: this.pieChartLabels,
    datasets: [{ data: [], backgroundColor: ['#76e4f7', '#4ab0c4', '#ef476f'] }]
  };
  pieChartOptions: ChartOptions<'pie'> = {
    responsive: true,
    plugins: {
      legend: { position: 'right' }
    }
  };
  barChartLabels: string[] = ['Jan', 'Feb', 'Mar', 'Apr', 'May', 'Jun', 'Jul', 'Aug', 'Sep', 'Oct', 'Nov', 'Dec'];
  barChartData: ChartData<'bar', number[], string> = {
    labels: this.barChartLabels,
    datasets: []
  };
  barChartOptions: ChartOptions<'bar'> = {
    responsive: true,
    plugins: {
      legend: { display: true, position: 'bottom' }
    },
    scales: {
      x: {},
      y: {
        beginAtZero: true,
        ticks: {
          stepSize: 1 // Ensure y-axis increases in steps of 1
        }
      }
    }
  };

  constructor(private router: Router,
    private patientService: PatientService, private http: HttpserviceService,

  ) { }

  ngOnInit(): void {
    this.getPatientList();

  }

  getPatientList() {
    const user: any = localStorage.getItem('doctor');
    this.userData = JSON.parse(user);
    this.doctorId = this.userData?.doctorId;

    this.caseSub = this.http.httpGet('/PatientReg/GetPatient').subscribe((response: any) => {
      if (response?.data && Array.isArray(response.data)) {
        this.tableData = response.data.filter((patient: any) =>
          String(patient.doctorId).trim() === String(this.doctorId).trim()
        );

        // Sort by date descending
        this.tableData = this.tableData.sort((a, b) => {
          const dateA = new Date(a.date).getTime();
          const dateB = new Date(b.date).getTime();
          return dateB - dateA;
        });

        this.updatePagination();
        this.calculateStats();
        this.updatePieChart();
        this.updateBarChart();

      } else {
        console.warn('⚠️ Unexpected response format:', response);
      }
    });
  }

  ngOnDestroy(): void {
    this.caseSub?.unsubscribe();
  }
  calculateStats() {
    this.totalCases = this.tableData.length;
    this.tableData.forEach(row => {
      const stage = row.stage ?? 0;
      row.baseline = stage >= 1 ? 'Completed' : 'Pending';
      row.baselineClass = stage >= 1 ? 'text-success' : 'text-danger';

      row.followUp1 = stage >= 3 ? 'Completed' : 'Pending';
      row.followUp1Class = stage >= 3 ? 'text-success' : 'text-danger';

      row.followUp2 = stage >= 5 ? 'Completed' : 'Pending';
      row.followUp2Class = stage >= 5 ? 'text-success' : 'text-danger';

      const allDone = row.baseline === 'Completed'
        && row.followUp1 === 'Completed'
        && row.followUp2 === 'Completed';

      row.status = allDone ? 'Completed' : 'Pending';
      row.statusClass = allDone ? 'text-success' : 'text-danger';
    });

    this.completedCases = this.tableData.filter(r => r.status === 'Completed').length;
    this.pendingCases = this.tableData.filter(r => r.status === 'Pending').length;
    this.incompleteCases = 0;  // adjust if needed
    this.maleCount = this.tableData.filter(r => r.gender?.toLowerCase() === 'male').length;
    this.femaleCount = this.tableData.filter(r => r.gender?.toLowerCase() === 'female').length;
    this.otherCount = this.tableData.filter(r => r.gender?.toLowerCase() === 'other').length;
  }

  updatePagination() {
    this.totalItems = this.tableData.length;
    // alert('this.totalItems' + this.tableData.length);
    this.totalPages = Math.ceil(this.totalItems / this.itemsPerPage);
    this.updatePaginatedData();
    this.generatePageNumbers();
  }
  updatePaginatedData() {
    const startIndex = (this.currentPage - 1) * this.itemsPerPage;
    const endIndex = startIndex + this.itemsPerPage;
    this.paginatedData = this.tableData.slice(startIndex, endIndex);
    console.log('📦 Paginated Data:', this.paginatedData); // ✅ Add this line
  }
  generatePageNumbers() {
    this.pageNumbers = [];
    const maxVisiblePages = 5;
    let startPage = Math.max(1, this.currentPage - Math.floor(maxVisiblePages / 2));
    let endPage = Math.min(this.totalPages, startPage + maxVisiblePages - 1);

    if (endPage - startPage + 1 < maxVisiblePages) {
      startPage = Math.max(1, endPage - maxVisiblePages + 1);
    }

    for (let i = startPage; i <= endPage; i++) {
      this.pageNumbers.push(i);
    }
  }

  goToPage(page: number) {
    if (page >= 1 && page <= this.totalPages && page !== this.currentPage) {
      this.currentPage = page;
      this.updatePaginatedData();
      this.generatePageNumbers();
    }
  }

  goToPrevious() {
    if (this.currentPage > 1) {
      this.goToPage(this.currentPage - 1);
    }
  }

  goToNext() {
    if (this.currentPage < this.totalPages) {
      this.goToPage(this.currentPage + 1);
    }
  }

  onItemsPerPageChange(event: any) {
    this.itemsPerPage = parseInt(event.target.value);
    this.currentPage = 1;
    this.updatePagination();
  }


  updatePieChart() {
    this.pieChartData = {
      labels: this.pieChartLabels,
      datasets: [{
        data: [this.completedCases, this.pendingCases],
        backgroundColor: ['#76e4f7', '#ef476f'],
        hoverBackgroundColor: ['#76e4f7', '#ef476f']
      }]
    };

    // Optional: manually trigger chart update if you have @ViewChild
    this.chart?.update();
  }



  updateBarChart() {
    const months = 12;
    const completedCounts = Array(months).fill(0);
    const pendingCounts = Array(months).fill(0);

    this.tableData.forEach(row => {
      if (!row.createdDt) return;
      const date = new Date(row.createdDt);
      if (isNaN(date.getTime())) return;

      const month = date.getMonth();
      const status = row.status?.toLowerCase() || '';

      if (status === 'completed') {
        completedCounts[month]++;
      } else if (status === 'pending') {
        pendingCounts[month]++;
      }
    });

    this.barChartData = {
      labels: this.barChartLabels,
      datasets: [
        {
          label: 'Completed',
          data: completedCounts,
          backgroundColor: '#76e4f7',
          hoverBackgroundColor: '#76e4f7'
        },
        {
          label: 'Pending',
          data: pendingCounts,
          backgroundColor: '#ef476f',
          hoverBackgroundColor: '#ef476f'
        }
      ]
    };

    this.chart?.update();
  }


  handleStageClick(row: any, section: 'baseline' | 'followUp1' | 'followUp2') {
    const patientId = row.patientId;

    const baselineCompleted = row.baseline?.toLowerCase() === 'completed';
    const followUp1Completed = row.followUp1?.toLowerCase() === 'completed';
    const followUp2Completed = row.followUp2?.toLowerCase() === 'completed';

    let stage = 0;

    if (section === 'baseline') {
      stage = baselineCompleted ? 1 : 0;
    } else if (section === 'followUp1') {
      stage = followUp1Completed ? 3 : 2;
    } else if (section === 'followUp2') {
      stage = followUp2Completed ? 5 : 4;
    }

    const status = section === 'baseline' ? row.baseline :
      section === 'followUp1' ? row.followUp1 :
        section === 'followUp2' ? row.followUp2 : null;

    const canAccess =
      (section === 'baseline') ||
      (section === 'followUp1' && baselineCompleted) ||
      (section === 'followUp2' && baselineCompleted && followUp1Completed);

    if (!canAccess) {
      alert(`Please complete previous stages before accessing ${section}.`);
      return;
    }

    // View mode if completed
    if (status?.toLowerCase() === 'completed') {
      this.router.navigate(['/demographic', patientId, stage], {
        state: { data: row, isViewMode: true }
      });
    } else {
      // Get next route path
      this.http.getPageRouterByPatientId(patientId).subscribe({
        next: (res: any) => {
          let pageRouter = res?.pageRouter;

          if (pageRouter) {
            pageRouter = pageRouter.trim().replace(/\/+$/, ''); // Remove trailing slash
            let parts = pageRouter.split('/').filter(Boolean);

            // Replace {patientId} if it's a template
            parts = parts.map((p: string) => (p === '{patientId}' ? String(patientId) : p));

            // If stage segment is missing, append it
            if (parts.length === 2) {
              parts.push(String(stage));
            } else if (parts.length >= 3) {
              parts[2] = String(stage); // Ensure correct stage
            }

            const finalRoute = '/' + parts.join('/');
            console.log('Navigating to:', finalRoute);

            this.router.navigate([finalRoute], {
              state: { isViewMode: false }
            });
          } else {
            alert('No route found for this patient.');
          }
        },
        error: err => {
          console.error('❌ Error fetching page router:', err);
        }
      });
    }
  }
  navigateToAddCase() {
    //this.router.navigate(['/demographic/']);
    this.router.navigate([`/demographic/${this.patientId}/${this.stage}`])
  }

  downloadAllStages(patientID: any) {
  // Create a router URL for the case-stage-view route
  const urlTree = this.router.createUrlTree([`/case-stage-view/${patientID}`]);
  const serializedUrl = this.router.serializeUrl(urlTree);

  // Build full URL using current app origin (important!)
  const fullUrl = `${window.location.origin}${serializedUrl}`;

  // Open that route in a new tab
  window.open(fullUrl, '_blank');
}
}
