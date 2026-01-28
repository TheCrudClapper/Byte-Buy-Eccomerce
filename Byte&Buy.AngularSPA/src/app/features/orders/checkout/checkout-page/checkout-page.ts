import { Component, computed, inject, OnInit, signal } from '@angular/core';
import { API_ENDPOINTS } from '../../../../core/constants/api-constants';
import { environment } from '../../../../../environments/environment';
import { AddressApiService } from '../../../../core/clients/address/address-api-service';
import { ShippingAddressCheckout } from '../../../../core/dto/shipping-address/shipping-address-checkout';
import { CheckoutResponse } from '../../../../core/dto/checkout/checkout-response';
import { CheckoutApiService } from '../../../../core/clients/checkout/checkout-api-service';
import { DecimalPipe } from '@angular/common';
import { Guid } from 'guid-typescript';
import { DeliveryOptionResponse } from '../../../../core/dto/delivery/delivery-option-response';
import { MoneyDto } from '../../../../core/dto/common/money-dto';

@Component({
  selector: 'app-checkout-page',
  imports: [DecimalPipe],
  templateUrl: './checkout-page.html',
  styleUrl: './checkout-page.scss',
})
export class CheckoutPage implements OnInit {
  private readonly addressApiService = inject(AddressApiService);
  private readonly checkoutApiService = inject(CheckoutApiService);
  protected readonly imageBaseUrl = environment.staticImagesBaseUrl;

  protected shippingAddress = signal<ShippingAddressCheckout | null>(null);
  protected checkout = signal<CheckoutResponse | null>(null);
  protected totalCost = signal<MoneyDto | null>(null);

  // singal calculating deliveries cost
  deliveryCost = computed(() => {
    let deliveryTotal = 0;
    let currency = '';

    Object.values(this.selectedDelivery()).forEach(o => {
      if (o) {
        deliveryTotal += o.amount;
        currency = o.currency;
      }
    });

    return { amount: deliveryTotal, currency } as MoneyDto;
  })

  // signal calculates total cost including deliveries
  finalTotalCost = computed<MoneyDto | null>(() => {
    const baseTotal = this.totalCost();
    const delivery = this.deliveryCost();

    if (!baseTotal) {
      return delivery.amount > 0 ? delivery : null;
    }

    return {
      currency: baseTotal.currency,
      amount: baseTotal.amount + delivery.amount
    };
  });

  // Holds selected deliveries key -> seller id
  selectedDelivery = signal<Record<string, DeliveryOptionResponse | null>>({});

  // Holds additional info of delviery key -> delivery id
  deliveryDetails = signal<Record<string, {
    parcelLocker?: {
      lockerId: string;
    };
    pickupPoint?: {
      pickupPointId: Guid;
      street: string;
      city: string;
      localNumber: string;
    };
  }>>({});


  canPay = computed(() =>
    Object.entries(this.selectedDelivery()).every(([sellerId, option]) => {
      if (!option) return false;

      const details = this.deliveryDetails()[sellerId];

      if (option.deliveryChannel === 'parcel_locker') {
        return !!details?.parcelLocker?.lockerId;
      }

      if (option.deliveryChannel === 'pickup_point') {
        const p = details?.pickupPoint;
        return !!(p?.pickupPointId && p.street && p.city && p.localNumber);
      }

      return true;
    })
  );


  ngOnInit(): void {
    this.addressApiService.getShippingAddressCheckout().subscribe(data => {
      this.shippingAddress.set(data);
    }
    );
    this.checkoutApiService.getCheckout().subscribe(data => {
      this.checkout.set(data);
      this.totalCost.set(data.totalCost);
    });
  }

  selectDelivery(sellerId: string, option: DeliveryOptionResponse) {
    this.selectedDelivery.update(s => ({
      ...s,
      [sellerId]: option
    }));

    this.deliveryDetails.update(d => ({
      ...d,
      [sellerId]: {}
    }));
  }

  // Sets additional values in deliveries Details
  setParcelLocker(sellerId: string, lockerId: string) {
    this.deliveryDetails.update(d => ({
      ...d,
      [sellerId]: {
        parcelLocker: { lockerId }
      }
    }));
  }

  setPickup(
    sellerId: string,
    field: 'street' | 'city' | 'localNumber',
    event: Event
  ) {
    const value = (event.target as HTMLInputElement).value;

    this.deliveryDetails.update(d => {
      const current = d[sellerId]?.pickupPoint ?? {
        pickupPointId: this.selectedDelivery()[sellerId]!.id,
        street: '',
        city: '',
        localNumber: ''
      };

      return {
        ...d,
        [sellerId]: {
          pickupPoint: {
            ...current,
            [field]: value
          }
        }
      };
    });
  }

  submit(){

  }
}
