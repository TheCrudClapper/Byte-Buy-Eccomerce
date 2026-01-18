import { Component, computed, inject, OnInit, signal } from '@angular/core';
import { FormControl, FormGroup, FormsModule, Validators, ReactiveFormsModule } from '@angular/forms';
import { CommonModule } from '@angular/common';
import { SellerInfo } from '../../../shared/seller-info/seller-info';
import { Guid } from 'guid-typescript';
import { RentOfferDetails } from '../../../models/rent-offer-details';
import { ProblemDetails } from '../../../../../core/dto/problem-details';
import { getErrorMessage } from '../../../../../shared/helpers/form-helper';
import { BaseOfferDetail } from '../../../shared/components/base-offer-detail/base-offer-detail';

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


  rentOfferDetails = signal<RentOfferDetails | null>(null);
  seller = computed(() => this.rentOfferDetails()?.seller);

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
    const desc = this.rentOfferDetails()?.description;
    return desc ? desc.trim().replace(/\n/g, '<br>') : undefined;
  }

  override addToCart(): void {
    throw new Error('Method not implemented.');
  }

  getError(path: string) {
    getErrorMessage(this.cartForm, path);
  }
}
