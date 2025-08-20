import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ManagamentComponent2 } from './management.component';

describe('ManagementComponent', () => {
  let component: ManagamentComponent2;
  let fixture: ComponentFixture<ManagamentComponent2>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ManagamentComponent2]
    })
    .compileComponents();
    
    fixture = TestBed.createComponent(ManagamentComponent2);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
