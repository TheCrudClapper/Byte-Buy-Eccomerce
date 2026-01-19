import { ComponentFixture, TestBed } from '@angular/core/testing';

import { SaleCartOffer } from './sale-cart-offer';

describe('SaleCartOffer', () => {
  let component: SaleCartOffer;
  let fixture: ComponentFixture<SaleCartOffer>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [SaleCartOffer]
    })
    .compileComponents();

    fixture = TestBed.createComponent(SaleCartOffer);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
