import { Component, effect, inject, OnInit, signal } from '@angular/core';
import { OfferApiService } from '../../../../core/clients/offers/common/offer-api-service';
import { UserPanelOfferUnion } from '../../../../core/dto/offers/common/user-panel-union';
import { ToastService } from '../../../../shared/services/snackbar/toast-service';
import { ProblemDetails } from '../../../../core/dto/problem-details';
import { DatePipe, DecimalPipe } from '@angular/common';
import { SaleOfferApiService } from '../../../../core/clients/offers/sale/sale-offer-api-service';
import { RouterLink } from '@angular/router';
import { RentOfferApiService } from '../../../../core/clients/offers/rent/rent-offer-api-serivce';
import { environment } from '../../../../../environments/environment';
import { OfferStatus } from '../../../../core/dto/offers/enum/offer-status';
import { PagedList } from '../../../../core/pagination/pagedList';
import { UserOffersQuery } from '../../../../core/dto/offers/query/user-offers-query';

@Component({
  selector: 'app-my-offers',
  imports: [DatePipe, DecimalPipe, RouterLink],
  templateUrl: './my-offers.html',
  styleUrl: './my-offers.scss',
})
export class MyOffers{
  private readonly PAGE_SIZE = 10;
  private readonly offerApiService = inject(OfferApiService);
  private readonly rentOfferApiService = inject(RentOfferApiService);
  private readonly saleOfferApiService = inject(SaleOfferApiService);
  private readonly toastService = inject(ToastService);
  protected readonly imageBaseUrl = environment.staticImagesBaseUrl;

  pagedList = signal<PagedList<UserPanelOfferUnion> | undefined>(undefined);
  readonly OfferStatus = OfferStatus;

  query = signal<UserOffersQuery>({
    pageNumber: 1,
    pageSize: this.PAGE_SIZE,
  });

  constructor() {
    effect(() => {
      this.fetchOffers(this.query());
    });
  }

  fetchOffers(query: UserOffersQuery) {
    this.offerApiService.getUserOffers(query).subscribe({
      next: data => this.pagedList.set(data),
      error: (err: ProblemDetails) => {
        this.toastService.error(err?.detail ?? "Something went wrong while fetching your offers")
      }
    });
  }

  goToPage(page: number) {
    const meta = this.pagedList()?.metadata;
    if (!meta) return;

    const safePage = Math.min(
      Math.max(page, 1),
      meta.totalPages
    );

    if (safePage === this.query().pageNumber) return;

    this.query.update(q => ({
      ...q,
      pageNumber: safePage
    }));
  }

  nextPage() {
    const meta = this.pagedList()?.metadata;
    if (!meta || !meta.hasNext) return;

    const current = this.query();
    this.goToPage(current.pageNumber + 1);
  }

  previousPage() {
    const meta = this.pagedList()?.metadata;
    if (!meta || !meta.hasPrevious) return;

    const current = this.query();
    this.goToPage(current.pageNumber - 1);
  }

  remove(offer: UserPanelOfferUnion) {
    if (!confirm("Are you sure you want to delete this offer ?")) {
      return;
    }

    const onSuccess = () => {
      this.toastService.success("Successfully deleted offer.");

      this.pagedList.update(offers => {
        if (!offers) return offers;

        return {
          ...offers,
          items: offers.items.filter(o => o.id !== offer.id),
          metadata: {
            ...offers.metadata,
            totalCount: offers.metadata.totalCount - 1
          }
        };
      });
    };

    const onError = (err: ProblemDetails) => {
      this.toastService.error(err?.detail ?? "Failed to delete offer");
    };

    if (offer.type === 'rent') {
      this.rentOfferApiService.delete(offer.id).subscribe({
        next: onSuccess,
        error: onError
      });
    } else {
      this.saleOfferApiService.delete(offer.id).subscribe({
        next: onSuccess,
        error: onError
      });
    }
  }
}
