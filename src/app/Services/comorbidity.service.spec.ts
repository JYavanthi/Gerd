import { TestBed } from '@angular/core/testing';

import { ComorbidityService } from './comorbidity.service';

describe('ComorbidityService', () => {
  let service: ComorbidityService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(ComorbidityService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
