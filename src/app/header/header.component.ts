import { Component, HostListener } from '@angular/core';
import { Router } from '@angular/router';

@Component({
  selector: 'app-header',
  templateUrl: './header.component.html',
  styleUrl: './header.component.scss'
  
})
export class HeaderComponent {
  // constructor(private router: Router) {}

  // goToHome(): void {
  //   this.router.navigate(['/']); 
  // }

  // navigateTo(page: string): void {
  //   this.router.navigate([page]); 
  // }

  // goToLogin() {
  //   this.router.navigate(['/login']);
  // }

  isScrolled = false;

  @HostListener('window:scroll', [])
  onWindowScroll() {
    this.isScrolled = window.scrollY > 50;
  }
}

