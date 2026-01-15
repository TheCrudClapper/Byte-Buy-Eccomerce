import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ShippingAddressDialog } from './shipping-address-dialog';

describe('ShippingAddressDialog', () => {
  let component: ShippingAddressDialog;
  let fixture: ComponentFixture<ShippingAddressDialog>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [ShippingAddressDialog]
    })
    .compileComponents();

    fixture = TestBed.createComponent(ShippingAddressDialog);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
