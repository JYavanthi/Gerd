import { ComponentFixture, TestBed } from '@angular/core/testing';

import { MedicalExaminationComponent } from './medical-examination.component';

describe('MedicalExaminationComponent', () => {
  let component: MedicalExaminationComponent;
  let fixture: ComponentFixture<MedicalExaminationComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [MedicalExaminationComponent]
    })
    .compileComponents();
    
    fixture = TestBed.createComponent(MedicalExaminationComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
