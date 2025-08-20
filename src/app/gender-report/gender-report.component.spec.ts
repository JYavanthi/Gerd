import { ComponentFixture, TestBed } from '@angular/core/testing';

import { GenderReportComponent } from './gender-report.component';

describe('GenderReportComponent', () => {
  let component: GenderReportComponent;
  let fixture: ComponentFixture<GenderReportComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [GenderReportComponent]
    })
    .compileComponents();
    
    fixture = TestBed.createComponent(GenderReportComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
