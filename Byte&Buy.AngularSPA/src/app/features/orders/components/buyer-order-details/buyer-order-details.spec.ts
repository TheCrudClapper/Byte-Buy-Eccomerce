import { ComponentFixture, TestBed } from '@angular/core/testing';

import { BuyerOrderDetails } from './buyer-order-details';

describe('BuyerOrderDetails', () => {
  let component: BuyerOrderDetails;
  let fixture: ComponentFixture<BuyerOrderDetails>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [BuyerOrderDetails]
    })
    .compileComponents();

    fixture = TestBed.createComponent(BuyerOrderDetails);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
