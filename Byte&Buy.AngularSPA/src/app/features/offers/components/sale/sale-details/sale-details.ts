import { Component, computed, effect, inject, OnInit, signal } from '@angular/core';
import { FormBuilder, FormControl, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';
import { CommonModule } from '@angular/common';
import { SaleOfferDetails } from '../../../models/sale-offer-details';
import { BaseOfferDetail } from '../../../shared/components/base-offer-detail/base-offer-detail';
import { ProblemDetails } from '../../../../../core/dto/problem-details';
import { Guid } from 'guid-typescript';
import { getErrorMessage } from '../../../../../shared/helpers/form-helper';
import { SellerInfo } from '../../../shared/seller-info/seller-info';
import { SaleCartOfferAddRequest } from '../../../../../core/dto/cart/cart-item/sale-cart-offer-add-request';
import { OfferStatus } from '../../../../../core/dto/offers/enum/offer-status';

@Component({
  selector: 'app-sale-details',
  imports: [CommonModule, ReactiveFormsModule, SellerInfo],
  templateUrl: './sale-details.html',
  styleUrls: [
    './sale-details.scss',
    '../../../shared/styles/offers-shared-styles.scss'],
  standalone: true
})

export class SaleDetails extends BaseOfferDetail {
  cartForm!: FormGroup;

  constructor(builder: FormBuilder) {
    super(builder);
    this.cartForm = this.builder.group({
      quantity: new FormControl(1, [Validators.required, Validators.min(1)]),
    })
  }

  saleOfferDetails = signal<SaleOfferDetails | null>(null);
  seller = computed(() => this.saleOfferDetails()?.seller);
  readonly OfferStatus = OfferStatus;

  loadOffer(id: Guid) {
    this.offerService.getSaleOfferDetils(id)
      .subscribe({
        next: (data) => {
          this.saleOfferDetails.set(data);
        },
        error: (err: ProblemDetails) => this.router.navigate(['/not-found'])
      });
  }

  override get description(): string | undefined {
    return this.saleOfferDetails()?.description?.trim();
  }

  override addToCart(): void {
    if (this.cartForm.invalid) {
      this.cartForm.markAsTouched();
      return;
    }

    const data = this.cartForm.get('quantity')!.value;
    const payload: SaleCartOfferAddRequest = {
      quantity: data!,
      offerId: this.saleOfferDetails()?.id!
    }

    this.cartService.postSaleCartOffer(payload).subscribe({
      next: () => this.toastService.success("Successfully added to cart !"),
      error: (err: ProblemDetails) => this.toastService.error(err?.detail ?? "Failed to add to cart")
    });
  }

  getError(path: string) {
    return getErrorMessage(this.cartForm, path);
  }
}

