import { Component, computed, inject, OnInit, signal } from '@angular/core';
import { FormControl, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';
import { CommonModule } from '@angular/common';
import { SaleOfferDetails } from '../../../models/sale-offer-details';
import { BaseOfferDetail } from '../../../shared/components/base-offer-detail/base-offer-detail';
import { ProblemDetails } from '../../../../../core/api-dto/problem-details';
import { Guid } from 'guid-typescript';
import { getErrorMessage } from '../../../../../core/helpers/form-helper';
import { SellerInfo } from '../../../shared/seller-info/seller-info';

@Component({
  selector: 'app-sale-details',
  imports: [CommonModule, ReactiveFormsModule, SellerInfo],
  templateUrl: './sale-details.html',
  styleUrls: [
    './sale-details.scss',
    '../../../shared/offers-shared-styles.scss'],
  standalone: true
})
export class SaleDetails extends BaseOfferDetail {
  cartForm = new FormGroup({
    quantity: new FormControl(1, [Validators.required, Validators.min(1)]),
  });

  saleOfferDetails = signal<SaleOfferDetails | null>(null);
  seller = computed(() => this.saleOfferDetails()?.seller);

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
    const desc = this.saleOfferDetails()?.description;
    return desc ? desc.trim().replace(/\n/g, '<br>') : undefined;
  }

  override addToCart(): void {
    throw new Error('Method not implemented.');
  }

  getError(path: string) {
    getErrorMessage(this.cartForm, path);
  }
}

