import { TestBed } from '@angular/core/testing';

import { RentalApiService } from './rental-api-service';

describe('RentalApiService', () => {
  let service: RentalApiService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(RentalApiService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
