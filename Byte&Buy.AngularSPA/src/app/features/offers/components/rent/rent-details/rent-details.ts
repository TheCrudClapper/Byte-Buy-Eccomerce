import { Component, inject, OnInit, signal } from '@angular/core';
import { FormControl, FormGroup, FormsModule, Validators, ReactiveFormsModule } from '@angular/forms';
import { CommonModule } from '@angular/common';
import { SellerInfo } from '../../../shared/seller-info/seller-info';
import { Guid } from 'guid-typescript';
import { ActivatedRoute } from '@angular/router';
import { RentOfferDetails } from '../../../models/rent-offer-details';
import { ToastService } from '../../../../../core/services/snackbar/toast-service';
import { OfferApiService } from '../../../services/offer-api-service';
import { ProblemDetails } from '../../../../../core/api-dto/problem-details';
import { finalize } from 'rxjs';
import { DeliveryOption } from '../../../shared/delivery-option';
import { getErrorMessage } from '../../../../../core/helpers/form-helper';

@Component({
  selector: 'app-sale-details',
  imports: [FormsModule, CommonModule, SellerInfo, ReactiveFormsModule],
  templateUrl: './rent-details.html',
  styleUrls: [
    './rent-details.scss',
    '../../../shared/offers-shared-styles.scss'],
  standalone: true
})

export class RentDetails implements OnInit {
  private readonly route = inject(ActivatedRoute);
  private readonly offerService = inject(OfferApiService);
  private readonly toastService = inject(ToastService);

  rentOfferId!: Guid;
  rentOfferDetails = signal<RentOfferDetails | null>(null);
  loading = signal<boolean>(false);

  cartForm: FormGroup = new FormGroup({
    quantity: new FormControl(1, [Validators.required, Validators.min(1)]),
    rentalDays: new FormControl(1, [Validators.required, Validators.min(1)]),
  });

  ngOnInit(): void {
    this.route.paramMap.subscribe(params => {
      const id = params.get('id');
      if (id) {
        this.rentOfferId = Guid.parse(id);
      }
    });
    this.loadOffer();
  }

  loadOffer() {
    this.loading.set(true);
    this.offerService.getRentOfferDetails(this.rentOfferId)
      .pipe(finalize(() => this.loading.set(false)))
      .subscribe({
        next: (data) => {
          console.log(data);
          this.rentOfferDetails.set(data);
        },
        error: (err: ProblemDetails) => this.toastService.error(err.detail ?? "Failed to load offer")
      })
  };

  getError(path: string){
    return getErrorMessage(this.cartForm, path);
  }
  
  addToCart(){

  }

  deliveryOptions: DeliveryOption[] = [
    {
      name: 'InPost Parcel Locker - S',
      priceAndCurrency: '10.95 PLN',
      carrier: 'Inpost',
      deliveryChannel: 'Parcel Locker'
    },
    {
      name: 'InPost Parcel Locker - M',
      priceAndCurrency: '10.95 PLN',
      carrier: 'Inpost',
      deliveryChannel: 'Parcel Locker'
    },
    {
      name: 'InPost Parcel Locker - L',
      priceAndCurrency: '10.95 PLN',
      carrier: 'Inpost',
      deliveryChannel: 'Parcel Locker'
    },
    {
      name: 'Courier DPD',
      priceAndCurrency: '10.95 PLN',
      carrier: 'DPD',
      deliveryChannel: 'Courier'
    },
    {
      name: 'Courier DHL',
      priceAndCurrency: '10.95 PLN',
      carrier: 'DHL',
      deliveryChannel: 'Courier'
    }
  ];

  get descriptionWithBr(): string | undefined {
    return this.rentOfferDetails()?.description.trim().replace(/\n/g, '<br>');
  }
}
