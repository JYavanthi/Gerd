import { ComponentFixture, TestBed } from '@angular/core/testing';

import { AssessmentComponent2 } from './assessment.component';

describe('AssessmentComponent', () => {
  let component: AssessmentComponent2;
  let fixture: ComponentFixture<AssessmentComponent2>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [AssessmentComponent2]
    })
    .compileComponents();
    
    fixture = TestBed.createComponent(AssessmentComponent2);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
