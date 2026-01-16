import { TestBed } from '@angular/core/testing';

import { OfferApiService } from './offer-api-service';

describe('OfferApiService', () => {
  let service: OfferApiService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(OfferApiService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
