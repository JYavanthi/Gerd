import { TestBed } from '@angular/core/testing';

import { GenderRptService } from './gender-rpt.service';

describe('GenderRptService', () => {
  let service: GenderRptService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(GenderRptService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
