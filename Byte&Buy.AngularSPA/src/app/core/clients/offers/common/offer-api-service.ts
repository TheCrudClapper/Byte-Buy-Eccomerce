import { HttpClient } from '@angular/common/http';
import { inject, Injectable } from '@angular/core';
import { map, Observable } from 'rxjs';
import { RentOfferDetails } from '../../../../features/offers/models/rent-offer-details';
import { Guid } from 'guid-typescript';
import { RentOfferDetailsResponse } from '../../../dto/offers/rent/rent-offer-details-response';
import { CompanySeller } from '../../../../features/offers/models/company-seller';
import { PrivateSeller } from '../../../../features/offers/models/private-seller';
import { SaleOfferDetails } from '../../../../features/offers/models/sale-offer-details';
import { SaleOfferDetailsResponse } from '../../../dto/offers/sale/sale-offer-details-response';
import { OfferUnion } from '../../../dto/offers/common/offer-browser-union';
import { UserPanelOfferUnion } from '../../../dto/offers/common/user-panel-union';

@Injectable({
  providedIn: 'root',
})
export class OfferApiService {
  private readonly httpClient = inject(HttpClient);
  private readonly resourceUri = "http://localhost:5099/api";

  getRentOfferDetails(id: Guid): Observable<RentOfferDetails> {
    return this.httpClient.get<RentOfferDetailsResponse>(`${this.resourceUri}/offers/rent/details/${id}`)
      .pipe(
        map(dto => ({
          id: dto.id,
          status: dto.status,
          maxRentalDays: dto.maxRentalDays,
          quantityAvaliable: dto.quantityAvaliable,
          pricePerDayAmount: dto.pricePerDay.amount,
          pricePerDayCurrency: dto.pricePerDay.currency,
          condition: dto.condition,
          category: dto.category,
          description: dto.description,
          title: dto.title,
          seller: {
            ...dto.seller,
            type: 'companyName' in dto.seller ? 'company' : 'private'
          } as CompanySeller | PrivateSeller,
          images: dto.images
        }))
      );
  }

  getSaleOfferDetils(id: Guid): Observable<SaleOfferDetails> {
    return this.httpClient.get<SaleOfferDetailsResponse>(`${this.resourceUri}/offers/sale/details/${id}`)
      .pipe(
        map(dto => ({
          id: dto.id,
          status: dto.status,
          quantityAvaliable: dto.quantityAvaliable,
          pricePerItemAmount: dto.pricePerItem.amount,
          pricePerItemCurrency: dto.pricePerItem.currency,
          condition: dto.condition,
          category: dto.category,
          description: dto.description,
          title: dto.title,
          seller: {
            ...dto.seller,
            type: 'companyName' in dto.seller ? 'company' : 'private'
          } as CompanySeller | PrivateSeller,
          images: dto.images
        }))
      );
  }

  browseOffers(): Observable<OfferUnion[]>{
    return this.httpClient.get<OfferUnion[]>(`${this.resourceUri}/offers`); 
  }

  getUserOffers(): Observable<UserPanelOfferUnion[]>{
    return this.httpClient.get<UserPanelOfferUnion[]>(`${this.resourceUri}/offers/my`);
  }
    
}

