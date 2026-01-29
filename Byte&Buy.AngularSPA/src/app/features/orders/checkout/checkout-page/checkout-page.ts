import { Component, computed, inject, OnInit, signal } from '@angular/core';
import { environment } from '../../../../../environments/environment';
import { AddressApiService } from '../../../../core/clients/address/address-api-service';
import { ShippingAddressCheckout } from '../../../../core/dto/shipping-address/shipping-address-checkout';
import { CheckoutResponse } from '../../../../core/dto/checkout/checkout-response';
import { CheckoutApiService } from '../../../../core/clients/checkout/checkout-api-service';
import { DecimalPipe } from '@angular/common';
import { DeliveryOptionResponse } from '../../../../core/dto/delivery/delivery-option-response';
import { MoneyDto } from '../../../../core/dto/common/money-dto';
import { SellerDeliveryState } from '../../models/seller-delivery-state';
import { OrderAddRequest } from '../../../../core/dto/order/order-add-request';
import { OrderApiService } from '../../../../core/clients/orders/order-api-service';
import { buildSellerDeliveriesPayload } from '../../mappers/order-mappers';

@Component({
  selector: 'app-checkout-page',
  imports: [DecimalPipe],
  templateUrl: './checkout-page.html',
  styleUrl: './checkout-page.scss',
})
export class CheckoutPage implements OnInit {
  private readonly addressApiService = inject(AddressApiService);
  private readonly checkoutApiService = inject(CheckoutApiService);
  private readonly orderApiService = inject(OrderApiService);
  protected readonly imageBaseUrl = environment.staticImagesBaseUrl;

  protected shippingAddress = signal<ShippingAddressCheckout | null>(null);
  protected checkout = signal<CheckoutResponse | null>(null);
  protected totalCost = signal<MoneyDto | null>(null);

  // Holds selected deliveries key -> seller id, value -> type representing given delivery
  deliveryBySeller = signal<Record<string, SellerDeliveryState | null>>({});

  // singal calculating deliveries cost
  deliveryCost = computed(() => {
    let deliveryTotal = 0;
    let currency = '';

    Object.values(this.deliveryBySeller()).forEach(o => {
      if (o) {
        deliveryTotal += o.delivery.amount;
        currency = o.delivery.currency;
      }
    });

    return { amount: deliveryTotal, currency } as MoneyDto;
  });

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

  canPay = computed(() => {
    const deliveries = Object.values(this.deliveryBySeller());
    if (deliveries.length === 0) return false;

    return deliveries.every(d => {
      if (!d) return false;

      switch (d.channel) {
        case 'Courier':
          return !!d.shippingAddressId;

        case 'ParcelLocker':
          return !!d.parcelLocker.lockerId;

        case 'PickupPoint':
          const p = d.pickupPoint;
          return !!(p.street && p.city && p.localNumber);

        default:
          return false;
      }
    });
  });

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
    switch (option.deliveryChannel) {
      case 'Courier':
        this.deliveryBySeller.update(s => ({
          ...s,
          [sellerId]: {
            channel: 'Courier',
            delivery: option,
            shippingAddressId: (this.shippingAddress()!.id).toString()
          }
        }));
        break;
      case 'ParcelLocker':
        this.deliveryBySeller.update(s => ({
          ...s,
          [sellerId]: {
            channel: 'ParcelLocker',
            delivery: option,
            parcelLocker: { lockerId: '' }
          }
        }));
        break;
      case 'PickupPoint':
        this.deliveryBySeller.update(s => ({
          ...s,
          [sellerId]: {
            channel: 'PickupPoint',
            delivery: option,
            pickupPoint: {
              pickupPointId: '',
              street: '',
              city: '',
              localNumber: ''
            }
          }
        }));
        break;
    }
  }

  setParcelLocker(sellerId: string, lockerId: string) {
    this.deliveryBySeller.update(s => {
      const current = s[sellerId];
      if (!current || current.channel !== 'ParcelLocker') return s;

      return {
        ...s,
        [sellerId]: {
          ...current,
          parcelLocker: { lockerId }
        }
      };
    });
  }

  setPickupField(
    sellerId: string,
    field: 'pickupPointId' | 'street' | 'city' | 'localNumber',
    value: string
  ) {
    this.deliveryBySeller.update(s => {
      const current = s[sellerId];
      if (!current || current.channel !== 'PickupPoint') return s;

      return {
        ...s,
        [sellerId]: {
          ...current,
          pickupPoint: {
            ...current.pickupPoint,
            [field]: value
          }
        }
      };
    });
  }

  submit() {
    const sellerPaload = buildSellerDeliveriesPayload(this.deliveryBySeller());
    const finalPayload: OrderAddRequest = {
      paymentMethodId: "9191B0F6-F84D-4D83-BF4D-4A9CBA98F05F",
      selectedDeliveries: sellerPaload
    };

    this.orderApiService.postOrder(finalPayload).subscribe(data => {
      console.log("Data" + data);
    })
  }
}
