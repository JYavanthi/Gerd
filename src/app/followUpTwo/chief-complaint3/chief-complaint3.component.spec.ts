import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ChiefComplaint3Component } from './chief-complaint3.component';

describe('ChiefComplaint3Component', () => {
  let component: ChiefComplaint3Component;
  let fixture: ComponentFixture<ChiefComplaint3Component>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ChiefComplaint3Component]
    })
    .compileComponents();
    
    fixture = TestBed.createComponent(ChiefComplaint3Component);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
