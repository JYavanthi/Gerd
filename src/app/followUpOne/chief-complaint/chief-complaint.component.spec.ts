import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ChiefComplaintComponent2 } from './chief-complaint.component';

describe('ChiefComplaintComponent', () => {
  let component: ChiefComplaintComponent2;
  let fixture: ComponentFixture<ChiefComplaintComponent2>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ChiefComplaintComponent2]
    })
    .compileComponents();
    
    fixture = TestBed.createComponent(ChiefComplaintComponent2);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
