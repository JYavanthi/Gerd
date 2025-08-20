import { ComponentFixture, TestBed } from '@angular/core/testing';

import { Assessment3Component } from './assessment3.component';

describe('Assessment3Component', () => {
  let component: Assessment3Component;
  let fixture: ComponentFixture<Assessment3Component>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [Assessment3Component]
    })
    .compileComponents();
    
    fixture = TestBed.createComponent(Assessment3Component);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
