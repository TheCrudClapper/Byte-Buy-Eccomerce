import { TestBed } from '@angular/core/testing';

import { SaleOfferApiService } from './sale-offer-api-service';

describe('SaleOfferApiService', () => {
  let service: SaleOfferApiService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(SaleOfferApiService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
