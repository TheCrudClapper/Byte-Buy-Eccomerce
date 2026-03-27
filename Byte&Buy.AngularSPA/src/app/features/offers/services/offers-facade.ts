import { inject, Injectable, signal } from '@angular/core';
import { ConditionApiService } from '../../../core/clients/condition/condition-api-service';
import { CategoryApiService } from '../../../core/clients/category/category-api-service';
import { DeliveryApiService } from '../../../core/clients/delivery/delivery-api-service';
import { AddressApiService } from '../../../core/clients/address/address-api-service';
import { SaleOfferApiService } from '../../../core/clients/offers/sale/sale-offer-api-service';
import { ToastService } from '../../../shared/services/snackbar/toast-service';
import { HomeAddressDto } from '../../../core/dto/home-address/home-address-dto';
import { mapToDeliveryOption } from '../../../shared/mappers/offer-mappers';
import { DeliveryOption } from '../../../shared/models/delivery-options';
import { OfferMode, OfferType } from '../shared/components/base-offer-form/base-offer-form';
import { ImageItem } from '../models/image-item';
import { Guid } from 'guid-typescript';
import { ProblemDetails } from '../../../core/dto/problem-details';
import { Observable } from 'rxjs';
import { UserSaleOfferResponse } from '../../../core/dto/offers/sale/user-sale-offer-response';
import { UserRentOfferResponse } from '../../../core/dto/offers/rent/user-rent-offer-response';
import { RentOfferApiService } from '../../../core/clients/offers/rent/rent-offer-api-serivce';
import { DeliveryGroup } from '../../../shared/models/delivery-group';
import { groupByCarrier } from '../mappers/offer-mapper';
import { OfferAddressResponse } from '../../../core/dto/offers/common/offer-address-response';

export type EditOffer =
  | { type: 'sale'; data: UserSaleOfferResponse }
  | { type: 'rent'; data: UserRentOfferResponse };

@Injectable({
  providedIn: 'root',
})
export class OffersFacade {
  private readonly categoryApi = inject(CategoryApiService);
  private readonly conditionApi = inject(ConditionApiService);
  private readonly deliveryApi = inject(DeliveryApiService);
  private readonly addressApi = inject(AddressApiService);
  private readonly saleApi = inject(SaleOfferApiService);
  private readonly rentApi = inject(RentOfferApiService);
  public readonly toast = inject(ToastService);

  readonly currentOffer = signal<EditOffer | null>(null);

  readonly categories$ = this.categoryApi.getSelectList();
  readonly conditions$ = this.conditionApi.getSelectList();

  deliveries = signal<{
    parcel: DeliveryGroup[],
    courier: DeliveryOption[],
    pickup: DeliveryOption[]
  }>({
    parcel: [],
    courier: [],
    pickup: []
  });

  readonly offerHomeAddress = signal<OfferAddressResponse | null>(null);

  init(): void {
    this.deliveryApi.getAvaliableDeliveries()
      .subscribe(r => this.deliveries.set({
        parcel: groupByCarrier(
          r.parcelLockerDeliveries.map(mapToDeliveryOption)
        ),
        courier: r.courierDeliveries.map(mapToDeliveryOption),
        pickup: r.pickupPointDeliveries.map(mapToDeliveryOption),
      }));
  }

  loadOffer(type: OfferType, id: Guid): void {
    if (type === 'sale') {
      this.saleApi.getById(id).subscribe({
        next: offer => this.currentOffer.set({ type: 'sale', data: offer }),
        error: (err: ProblemDetails) => this.toast.error(err.detail ?? "Failed to load sale offer")
      });
    }
    else {
      this.rentApi.getById(id).subscribe({
        next: offer => this.currentOffer.set({ type: 'rent', data: offer }),
        error: (err: ProblemDetails) => this.toast.error(err.detail ?? "Failed to load rent offer")
      });
    }
  }

  getHomeAddressForOffer() {
    this.addressApi.getOfferHomeAddress()
      .subscribe(a => this.offerHomeAddress.set(a));
  }

  submit(type: OfferType, mode: OfferMode,
    payload: FormData, images: ImageItem[],
    offerId?: Guid) {

    if (!this.validateImages(mode, images)) {
      this.toast.error("Add at least one image !");
      return;
    }

    if (!this.offerHomeAddress()) {
      this.toast.error("You need to set your home address to proceed.");
      return;
    }

    let request$: Observable<unknown>;

    if (type === 'rent') {
      request$ =
        mode === 'add'
          ? this.rentApi.post(payload)
          : this.rentApi.put(offerId!, payload);
    } else {
      request$ =
        mode === 'add'
          ? this.saleApi.post(payload)
          : this.saleApi.put(offerId!, payload);
    }

    request$.subscribe({
      next: () =>
        this.toast.success(
          mode === 'add'
            ? 'Offer created successfully'
            : 'Offer updated successfully'
        ),
      error: (err: ProblemDetails) =>
        this.toast.error(err?.detail ?? 'Operation failed')
    });
  }

  validateImages(mode: OfferMode, images: ImageItem[]): boolean {
    if (mode === 'add') {
      return images.some(image => image.isNew);
    }
    else {
      const length = images.length;
      const deletedCount = images.filter(i => i.isDeleted).length;
      const newCount = images.filter(i => i.isNew).length;
      //check wheter at least image is not deleted
      if (length - deletedCount + newCount <= 0) {
        return false;
      }
    }
    return true;
  }
}


