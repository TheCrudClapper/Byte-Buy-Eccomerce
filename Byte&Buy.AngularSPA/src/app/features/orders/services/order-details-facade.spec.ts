import { TestBed } from '@angular/core/testing';

import { OrderDetialsFacade } from './order-details-facade';

describe('OrderDetialsFacade', () => {
  let service: OrderDetialsFacade;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(OrderDetialsFacade);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
