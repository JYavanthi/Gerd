import { ComponentFixture, TestBed } from '@angular/core/testing';

import { CaseOutletComponent } from './case-outlet.component';

describe('CaseOutletComponent', () => {
  let component: CaseOutletComponent;
  let fixture: ComponentFixture<CaseOutletComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [CaseOutletComponent]
    })
    .compileComponents();
    
    fixture = TestBed.createComponent(CaseOutletComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
