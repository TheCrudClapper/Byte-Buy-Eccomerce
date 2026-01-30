import { Component, effect, inject, OnInit, signal } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { OfferApiService } from '../../../../../core/clients/offers/common/offer-api-service';
import { ToastService } from '../../../../../shared/services/snackbar/toast-service';
import { DeliveryApiService } from '../../../../../core/clients/delivery/delivery-api-service';
import { DeliveryOption } from '../../../../../shared/models/delivery-options';
import { Guid } from 'guid-typescript';
import { ProblemDetails } from '../../../../../core/dto/problem-details';
import { CartApiService } from '../../../../../core/clients/cart/cart-api-service';
import { FormBuilder } from '@angular/forms';
import { environment } from '../../../../../../environments/environment';

@Component({
  selector: 'app-base-offer-detail',
  imports: [],
  template: ``,
  styles: ``,
})

//Base abstract class for sale and rent offer details encapsulating common logic
export abstract class BaseOfferDetail {
  protected readonly route = inject(ActivatedRoute);
  protected readonly offerService = inject(OfferApiService);
  protected readonly cartService = inject(CartApiService);
  protected readonly toastService = inject(ToastService);
  protected readonly deliveryService = inject(DeliveryApiService);
  protected readonly imageBaseUrl = environment.staticImagesBaseUrl;
  protected readonly router = inject(Router);
  
  protected loading = signal<boolean>(false);
  protected deliveries = signal<DeliveryOption[]>([]);
  protected offerId = signal<Guid | null>(null);

  constructor(protected builder: FormBuilder) {
    effect(() => {
      const id = this.route.snapshot.paramMap.get('id');
      if (id) {
        this.offerId.set(Guid.parse(id));
      }
    });

    effect(() => {
      const id = this.offerId();
      if (!id) return;

      this.loading.set(true);
      this.loadOffer(id);
      this.loadDeliveries(id);
    });
  }

  abstract addToCart(): void;
  abstract loadOffer(id: Guid): void;
  abstract get description(): string | undefined;

  protected loadDeliveries(id: Guid) {
    this.deliveryService.getDeliveriesListPerOffer(id)
      .subscribe({
        next: (data) => {
          this.deliveries.set(data);
        },
        error: (err: ProblemDetails) => this.toastService.error(err.detail ?? "Failed to load deliveries")
      })
  }
}
