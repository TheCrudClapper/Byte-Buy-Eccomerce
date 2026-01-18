import { TestBed } from '@angular/core/testing';

import { AddressApiService } from './address-api-service';

describe('AddressApiService', () => {
  let service: AddressApiService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(AddressApiService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
