import { ComponentFixture, TestBed } from '@angular/core/testing';

import { RentCartOffer } from './rent-cart-offer';

describe('RentCartOffer', () => {
  let component: RentCartOffer;
  let fixture: ComponentFixture<RentCartOffer>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [RentCartOffer]
    })
    .compileComponents();

    fixture = TestBed.createComponent(RentCartOffer);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
