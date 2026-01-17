import { Component, computed, inject, OnInit, signal } from '@angular/core';
import { FormControl, FormGroup, FormsModule, Validators, ReactiveFormsModule } from '@angular/forms';
import { CommonModule } from '@angular/common';
import { SellerInfo } from '../../../shared/seller-info/seller-info';
import { Guid } from 'guid-typescript';
import { ActivatedRoute } from '@angular/router';
import { RentOfferDetails } from '../../../models/rent-offer-details';
import { ToastService } from '../../../../../core/services/snackbar/toast-service';
import { OfferApiService } from '../../../services/offer-api-service';
import { ProblemDetails } from '../../../../../core/api-dto/problem-details';
import { getErrorMessage } from '../../../../../core/helpers/form-helper';
import { DeliveryApiService } from '../../../../../core/services/delivery/delivery-api-service';
import { DeliveryListItem } from '../../../../../shared/models/delivery-list-items';
import { PrivateSeller } from '../../../models/private-seller';
import { CompanySeller } from '../../../models/company-seller';

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
  private readonly deliveryService = inject(DeliveryApiService);
  protected readonly imageBaseUrl = "http://localhost:5099/Images/";

  rentOfferId!: Guid;
  rentOfferDetails = signal<RentOfferDetails | null>(null);
  seller = computed(() => this.rentOfferDetails()?.seller);
  deliveries = signal<DeliveryListItem[]>([]);
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
    this.loading.set(true);
    this.loadOffer();
    this.loadDeliveries();
    this.loading.set(false);
  }

  loadOffer() {
    this.loading.set(true);
    this.offerService.getRentOfferDetails(this.rentOfferId)
      .subscribe({
        next: (data) => {
          this.rentOfferDetails.set(data);
        },
        error: (err: ProblemDetails) => this.toastService.error(err.detail ?? "Failed to load offer")
      })
  };

  loadDeliveries() {
    this.deliveryService.getDeliveriesListPerOffer(this.rentOfferId)
      .subscribe({
        next: (data) => {
          this.deliveries.set(data);
        },
        error: (err: ProblemDetails) => this.toastService.error(err.detail ?? "Failed to load deliveries")
      })
  }

  getError(path: string) {
    return getErrorMessage(this.cartForm, path);
  }

  addToCart() {

  }

  get descriptionWithBr(): string | undefined {
    return this.rentOfferDetails()?.description.trim().replace(/\n/g, '<br>');
  }
}
