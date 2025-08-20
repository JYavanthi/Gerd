import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ComorbiditiesComponent } from './comorbidities.component';

describe('ComorbiditiesComponent', () => {
  let component: ComorbiditiesComponent;
  let fixture: ComponentFixture<ComorbiditiesComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ComorbiditiesComponent]
    })
    .compileComponents();
    
    fixture = TestBed.createComponent(ComorbiditiesComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
