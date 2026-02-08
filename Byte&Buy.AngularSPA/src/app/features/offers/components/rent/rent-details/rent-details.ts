import { Component, computed, inject, OnInit, signal } from '@angular/core';
import { FormControl, FormGroup, FormsModule, Validators, ReactiveFormsModule } from '@angular/forms';
import { CommonModule } from '@angular/common';
import { SellerInfo } from '../../../shared/seller-info/seller-info';
import { Guid } from 'guid-typescript';
import { RentOfferDetails } from '../../../models/rent-offer-details';
import { ProblemDetails } from '../../../../../core/dto/problem-details';
import { getErrorMessage } from '../../../../../shared/helpers/form-helper';
import { BaseOfferDetail } from '../../../shared/components/base-offer-detail/base-offer-detail';
import { CartApiService } from '../../../../../core/clients/cart/cart-api-service';
import { RentCartOfferAddRequest } from '../../../../../core/dto/cart/cart-item/rent-cart-offer-add-request';
import { OfferStatus } from '../../../../../core/dto/offers/enum/offer-status';

@Component({
  selector: 'app-rent-details',
  imports: [FormsModule, CommonModule, SellerInfo, ReactiveFormsModule],
  templateUrl: './rent-details.html',
  styleUrls: [
    './rent-details.scss',
    '../../../shared/styles/offers-shared-styles.scss'],
  standalone: true
})

export class RentDetails extends BaseOfferDetail {
  private readonly cartApiService = inject(CartApiService);

  rentOfferDetails = signal<RentOfferDetails | null>(null);
  seller = computed(() => this.rentOfferDetails()?.seller);
  readonly OfferStatus = OfferStatus;
  
  cartForm = new FormGroup({
    quantity: new FormControl(1, [Validators.required, Validators.min(1)]),
    rentalDays: new FormControl(1, [Validators.required, Validators.min(1)]),
  });

  loadOffer(id: Guid) {
    this.offerService.getRentOfferDetails(id)
      .subscribe({
        next: (data) => {
          this.rentOfferDetails.set(data);
        },
        error: (err: ProblemDetails) => this.router.navigate(['/not-found'])
      })
  };

  override get description(): string | undefined {
    return this.rentOfferDetails()?.description?.trim();
  }

  override addToCart(): void {
    if (this.cartForm.invalid) {
      this.cartForm.markAllAsTouched;
      return;
    }

    const data = this.cartForm.value;
    const payload: RentCartOfferAddRequest = {
      offerId: this.rentOfferDetails()?.id ?? Guid.create(),
      quantity: data.quantity!,
      rentalDays: data.rentalDays!
    }

    this.cartApiService.postRentCartOffer(payload).subscribe({
      next: () => this.toastService.success("Successfully added to cart !"),
      error: (err: ProblemDetails) => this.toastService.error(err?.detail ?? "Failed to add to cart")
    });
  }

  getError(path: string) {
    return getErrorMessage(this.cartForm, path);
  }
}
