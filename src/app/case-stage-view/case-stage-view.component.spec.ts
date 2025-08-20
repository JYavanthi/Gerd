import { ComponentFixture, TestBed } from '@angular/core/testing';

import { CaseStageViewComponent } from './case-stage-view.component';

describe('CaseStageViewComponent', () => {
  let component: CaseStageViewComponent;
  let fixture: ComponentFixture<CaseStageViewComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [CaseStageViewComponent]
    })
    .compileComponents();
    
    fixture = TestBed.createComponent(CaseStageViewComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
