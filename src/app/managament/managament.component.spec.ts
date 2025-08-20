import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ManagamentComponent } from './managament.component';

describe('ManagamentComponent', () => {
  let component: ManagamentComponent;
  let fixture: ComponentFixture<ManagamentComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ManagamentComponent]
    })
    .compileComponents();
    
    fixture = TestBed.createComponent(ManagamentComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
