import { ComponentFixture, TestBed } from '@angular/core/testing';

import { HistoryEndoscopyComponent } from './history-endoscopy.component';

describe('HistoryEndoscopyComponent', () => {
  let component: HistoryEndoscopyComponent;
  let fixture: ComponentFixture<HistoryEndoscopyComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [HistoryEndoscopyComponent]
    })
    .compileComponents();
    
    fixture = TestBed.createComponent(HistoryEndoscopyComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
