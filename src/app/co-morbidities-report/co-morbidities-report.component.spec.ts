import { ComponentFixture, TestBed } from '@angular/core/testing';

import { CoMorbiditiesReportComponent } from './co-morbidities-report.component';

describe('CoMorbiditiesReportComponent', () => {
  let component: CoMorbiditiesReportComponent;
  let fixture: ComponentFixture<CoMorbiditiesReportComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [CoMorbiditiesReportComponent]
    })
    .compileComponents();
    
    fixture = TestBed.createComponent(CoMorbiditiesReportComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
