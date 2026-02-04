import { ComponentFixture, TestBed } from '@angular/core/testing';

import { SellerOrderDetails } from './seller-order-details';

describe('SellerOrderDetails', () => {
  let component: SellerOrderDetails;
  let fixture: ComponentFixture<SellerOrderDetails>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [SellerOrderDetails]
    })
    .compileComponents();

    fixture = TestBed.createComponent(SellerOrderDetails);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
