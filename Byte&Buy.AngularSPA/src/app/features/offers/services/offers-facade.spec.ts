import { TestBed } from '@angular/core/testing';

import { OffersFacade } from './offers-facade';

describe('OffersFacade', () => {
  let service: OffersFacade;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(OffersFacade);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
