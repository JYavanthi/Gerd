import { Component } from '@angular/core';

@Component({
  selector: 'app-follow-up',
  templateUrl: './follow-up.component.html',
  styleUrl: './follow-up.component.css'
})
export class FollowUpComponent {
  caseList = [
    { id: '#1', name: 'KK', date: '2025-05-02', status: 'Completed', doctor: 'Dr Krishna Kumar' },
    { id: '#2', name: 'SKM', date: '2025-05-02', status: 'Pending', doctor: 'Dr Krishna Kumar' },
    { id: '#3', name: 'ABC', date: '2025-05-02', status: 'Completed', doctor: 'Dr Krishna Kumar' },
  ];
}
