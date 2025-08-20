import { ComponentFixture, TestBed } from '@angular/core/testing';

import { AboutGerdRegistryComponent } from './about-gerd-registry.component';

describe('AboutGerdRegistryComponent', () => {
  let component: AboutGerdRegistryComponent;
  let fixture: ComponentFixture<AboutGerdRegistryComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [AboutGerdRegistryComponent]
    })
    .compileComponents();
    
    fixture = TestBed.createComponent(AboutGerdRegistryComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
