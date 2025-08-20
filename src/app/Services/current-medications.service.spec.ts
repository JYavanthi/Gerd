import { TestBed } from '@angular/core/testing';

import { CurrentMedicationsService } from './current-medications.service';

describe('CurrentMedicationsService', () => {
  let service: CurrentMedicationsService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(CurrentMedicationsService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
