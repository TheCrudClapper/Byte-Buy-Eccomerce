import { Component, inject, OnInit, signal } from '@angular/core';
import { OfferApiService } from '../../../../core/clients/offers/common/offer-api-service';
import { UserPanelOfferUnion } from '../../../../core/dto/offers/common/user-panel-union';
import { ToastService } from '../../../../shared/services/snackbar/toast-service';
import { ProblemDetails } from '../../../../core/dto/problem-details';
import { DatePipe } from '@angular/common';
import { Guid } from 'guid-typescript';
import { RentOfferApiSerivce } from '../../../../core/clients/offers/rent/rent-offer-api-serivce';
import { SaleOfferApiService } from '../../../../core/clients/offers/sale/sale-offer-api-service';

@Component({
  selector: 'app-my-offers',
  imports: [DatePipe],
  templateUrl: './my-offers.html',
  styleUrl: './my-offers.scss',
})
export class MyOffers implements OnInit {
  private readonly offerApiService = inject(OfferApiService);
  private readonly rentOfferApiService = inject(RentOfferApiSerivce);
  private readonly saleOfferApiService = inject(SaleOfferApiService);
  private readonly toastService = inject(ToastService);

  userOffers = signal<UserPanelOfferUnion[]>([]);

  ngOnInit(): void {
    this.offerApiService.getUserOffers().subscribe({
      next: data => this.userOffers.set(data),
      error: (err: ProblemDetails) => {
        this.toastService.error(err?.detail ?? "Something went wrong while fetching your offers")
      }
    });
  }

  remove(offer: UserPanelOfferUnion) {
    if (confirm("Are you sure you want to delete this offer ?")) {
      if (offer.type === 'rent') {
        this.removeRent(offer.id);
      }
      else {
        this.removeSale(offer.id);
      }

      this.userOffers.update(offers =>
        offers.filter(o => o.id !== offer.id)
      );
    }
  }

  removeSale(id: Guid) {
    this.saleOfferApiService.delete(id).subscribe({
      next: () => this.toastService.success("Successfully delete offer."),
      error: (err: ProblemDetails) => this.toastService.error(err?.detail ?? "Failed to delete offer")
    });
  }

  removeRent(id: Guid) {
    this.rentOfferApiService.delete(id).subscribe({
      next: () => this.toastService.success("Successfully delete offer."),
      error: (err: ProblemDetails) => this.toastService.error(err?.detail ?? "Failed to delete offer")
    });
  }
}
