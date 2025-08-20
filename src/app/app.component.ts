import { Component } from '@angular/core';
import { NavigationEnd, Router } from '@angular/router';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrl: './app.component.scss'
})
export class AppComponent {
  title = 'godaddysites';
  showForm = false;
  toggleForm() {
    this.showForm = !this.showForm;
  }
}
