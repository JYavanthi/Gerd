import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ComorbiditiesComponent2 } from './comorbidities.component';

describe('ComorbiditiesComponent', () => {
  let component: ComorbiditiesComponent2;
  let fixture: ComponentFixture<ComorbiditiesComponent2>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ComorbiditiesComponent2]
    })
    .compileComponents();
    
    fixture = TestBed.createComponent(ComorbiditiesComponent2);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
