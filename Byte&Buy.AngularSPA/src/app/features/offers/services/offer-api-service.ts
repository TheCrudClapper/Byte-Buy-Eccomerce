import { HttpClient } from '@angular/common/http';
import { inject, Injectable } from '@angular/core';
import { map, Observable } from 'rxjs';
import { RentOfferDetails } from '../models/rent-offer-details';
import { Guid } from 'guid-typescript';
import { RentOfferDetailsResponse } from '../api-dto/rent-offer-details-response';

@Injectable({
  providedIn: 'root',
})
export class OfferApiService {
  private readonly httpClient = inject(HttpClient);
  private readonly resourceUri = "http://localhost:5099/api/offers";  

  getRentOfferDetails(id: Guid): Observable<RentOfferDetails> {
    return this.httpClient.get<RentOfferDetailsResponse>(`${this.resourceUri}/rent/details/${id}`)
    .pipe(
      map(dto => ({
        id: dto.id,
        maxRentalDays: dto.maxRentalDays,
        quantityAvaliable: dto.quantityAvaliable,
        pricePerDayAmount: dto.pricePerDay.amount,
        pricePerDayCurrency: dto.pricePerDay.currency,
        condition: dto.condition,
        category: dto.category,
        description: dto.description,
        title: dto.title,
        seller: dto.seller, 
        images: dto.images
      }))
    );
}

}

