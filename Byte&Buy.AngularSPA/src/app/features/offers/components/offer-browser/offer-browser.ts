import { Component, inject, OnInit, signal } from '@angular/core';
import { OfferApiService } from '../../../../core/clients/offers/common/offer-api-service';
import { CommonModule } from '@angular/common';
import { RentBrowserItem } from '../rent/rent-browser-item/rent-browser-item/rent-browser-item';
import { OfferUnion } from '../../../../core/dto/offers/common/offer-browser-union';
import { SaleBrowserItem } from '../sale/sale-browser-item/sale-browser-item/sale-browser-item';
import { RentOfferApiSerivce } from '../../../../core/clients/offers/rent/rent-offer-api-serivce';
import { SaleOfferApiService } from '../../../../core/clients/offers/sale/sale-offer-api-service';
import { Guid } from 'guid-typescript';

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
export class OfferBrowser implements OnInit {
  private readonly offerApiService = inject(OfferApiService);

  offers = signal<OfferUnion[]>([]);

  ngOnInit(): void {
    this.loadOffers();
  }

  loadOffers() {
    this.offerApiService.browseOffers().subscribe({
      next: (data) => {
        this.offers.set(data);
        console.log(data);
      }
    })
  }


}
