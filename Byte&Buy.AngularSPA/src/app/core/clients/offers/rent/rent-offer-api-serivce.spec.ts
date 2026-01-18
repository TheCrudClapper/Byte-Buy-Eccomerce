import { TestBed } from '@angular/core/testing';

import { RentOfferApiSerivce } from './rent-offer-api-serivce';

describe('RentOfferApiSerivce', () => {
  let service: RentOfferApiSerivce;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(RentOfferApiSerivce);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
