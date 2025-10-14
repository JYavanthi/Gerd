import { ComponentFixture, TestBed } from '@angular/core/testing';

import { AllReportsComponentComponent } from './all-reports-component.component';

describe('AllReportsComponentComponent', () => {
  let component: AllReportsComponentComponent;
  let fixture: ComponentFixture<AllReportsComponentComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [AllReportsComponentComponent]
    })
    .compileComponents();
    
    fixture = TestBed.createComponent(AllReportsComponentComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
