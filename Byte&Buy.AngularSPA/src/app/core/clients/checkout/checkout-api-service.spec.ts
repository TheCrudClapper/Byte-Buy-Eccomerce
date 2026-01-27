import { TestBed } from '@angular/core/testing';

import { CheckoutApiService } from './checkout-api-service';

describe('CheckoutApiService', () => {
  let service: CheckoutApiService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(CheckoutApiService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
