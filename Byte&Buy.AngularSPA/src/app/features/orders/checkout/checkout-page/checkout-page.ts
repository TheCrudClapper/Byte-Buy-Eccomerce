import { Component, inject, OnInit, signal } from '@angular/core';
import { API_ENDPOINTS } from '../../../../core/constants/api-constants';
import { environment } from '../../../../../environments/environment';
import { AddressApiService } from '../../../../core/clients/address/address-api-service';
import { ShippingAddressCheckout } from '../../../../core/dto/shipping-address/shipping-address-checkout';

@Component({
  selector: 'app-checkout-page',
  imports: [],
  templateUrl: './checkout-page.html',
  styleUrl: './checkout-page.scss',
})
export class CheckoutPage implements OnInit{
  private readonly addressApiService = inject(AddressApiService);

  protected shippingAddress = signal<ShippingAddressCheckout | null>(null);

  ngOnInit(): void {
    this.addressApiService.getShippingAddressCheckout().subscribe(data => 
      this.shippingAddress.set(data)
    ); 
  }
}
