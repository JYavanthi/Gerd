import { ComponentFixture, TestBed } from '@angular/core/testing';

import { Comorbidities3Component } from './comorbidities3.component';

describe('Comorbidities3Component', () => {
  let component: Comorbidities3Component;
  let fixture: ComponentFixture<Comorbidities3Component>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [Comorbidities3Component]
    })
    .compileComponents();
    
    fixture = TestBed.createComponent(Comorbidities3Component);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
