import { Component, effect, inject, OnInit, signal } from '@angular/core';
import { OfferApiService } from '../../../../core/clients/offers/common/offer-api-service';
import { CommonModule } from '@angular/common';
import { RentBrowserItem } from '../rent/rent-browser-item/rent-browser-item/rent-browser-item';
import { OfferUnion } from '../../../../core/dto/offers/common/offer-browser-union';
import { SaleBrowserItem } from '../sale/sale-browser-item/sale-browser-item/sale-browser-item';
import { PagedList } from '../../../../core/pagination/pagedList';
import { OfferQueryParams } from '../../../../core/dto/offers/query/offer-browser-query-params';

@Component({
  selector: 'app-offer-browser',
  imports: [CommonModule, RentBrowserItem, SaleBrowserItem],
  templateUrl: './offer-browser.html',
  standalone: true,
  styleUrls: [
    './offer-browser.scss',
    '../../shared/styles/offers-shared-styles.scss'
  ],
})
export class OfferBrowser {
  private readonly offerApiService = inject(OfferApiService);

  query = signal<OfferQueryParams>({
    pageNumber: 1,
    pageSize: 10
  });

  pagedList = signal<PagedList<OfferUnion> | undefined>(undefined);

  constructor() {
    effect(() => {
      const q = this.query();
      this.fetchOffers(q);
    });
  }

  private fetchOffers(query: OfferQueryParams) {
    this.offerApiService.browseOffers(query)
      .subscribe(data => {
        this.pagedList.set(data);
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
}
