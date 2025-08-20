import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ChiefComplaintComponent } from './chief-complaint.component';

describe('ChiefComplaintComponent', () => {
  let component: ChiefComplaintComponent;
  let fixture: ComponentFixture<ChiefComplaintComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ChiefComplaintComponent]
    })
    .compileComponents();
    
    fixture = TestBed.createComponent(ChiefComplaintComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
