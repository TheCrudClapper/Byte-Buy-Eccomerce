import { Component, inject, OnInit, signal } from '@angular/core';
import { API_ENDPOINTS } from '../../../../core/constants/api-constants';
import { environment } from '../../../../../environments/environment';
import { AddressApiService } from '../../../../core/clients/address/address-api-service';
import { ShippingAddressCheckout } from '../../../../core/dto/shipping-address/shipping-address-checkout';
import { CheckoutResponse } from '../../../../core/dto/checkout/checkout-response';
import { CheckoutApiService } from '../../../../core/clients/checkout/checkout-api-service';

@Component({
  selector: 'app-checkout-page',
  imports: [],
  templateUrl: './checkout-page.html',
  styleUrl: './checkout-page.scss',
})
export class CheckoutPage implements OnInit{
  private readonly addressApiService = inject(AddressApiService);
  private readonly checkoutApiService = inject(CheckoutApiService);

  protected shippingAddress = signal<ShippingAddressCheckout | null>(null);
  protected checkout = signal<CheckoutResponse | null>(null);

  ngOnInit(): void {
    this.addressApiService.getShippingAddressCheckout().subscribe(data => 
      this.shippingAddress.set(data)
    ); 
    this.checkoutApiService.getCheckout().subscribe(data => {
      this.checkout.set(data);
    });
  }
}
