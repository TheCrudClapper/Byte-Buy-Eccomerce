import { HttpClient } from '@angular/common/http';
import { inject, Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { CartResponse } from '../../dto/cart/cart-response';
import { Guid } from 'guid-typescript';
import { SaleCartOfferAddRequest } from '../../dto/cart/cart-item/sale-cart-offer-add-request';
import { SaleCartOfferUpdateRequest } from '../../dto/cart/cart-item/sale-cart-offer-update-request';
import { RentCartOfferAddRequest } from '../../dto/cart/cart-item/rent-cart-offer-add-request';
import { RentCartOfferUpdateRequest } from '../../dto/cart/cart-item/rent-cart-offer-update-request';
import { CartSummary } from '../../../features/cart/models/cart-summary';
import { CartSummaryResponse } from '../../dto/cart/cart-summary-response';

@Injectable({
  providedIn: 'root',
})
export class CartApiService {
  private readonly httpClient = inject(HttpClient);
  private readonly resourceUri = "http://localhost:5099/api/carts";

  getCart(): Observable<CartResponse>{
    return this.httpClient.get<CartResponse>(`${this.resourceUri}`);
  }

  deleteCartOffer(cartOfferId: Guid): Observable<CartSummaryResponse>{
    return this.httpClient.delete<CartSummaryResponse>(`${this.resourceUri}/${cartOfferId}`);
  }
  
  postSaleCartOffer(request: SaleCartOfferAddRequest){
    return this.httpClient.post(`${this.resourceUri}/sale-offer`, request);
  }

  putSaleCartOffer(cartOfferId: Guid, request: SaleCartOfferUpdateRequest): Observable<CartSummaryResponse>{
    return this.httpClient.put<CartSummaryResponse>(`${this.resourceUri}/sale-offer/${cartOfferId}`, request);
  }

  postRentCartOffer(request: RentCartOfferAddRequest){
    return this.httpClient.post(`${this.resourceUri}/rent-offer`, request);
  }

  putRentCartOffer(cartOfferId: Guid, request: RentCartOfferUpdateRequest): Observable<CartSummaryResponse>{
    return this.httpClient.put<CartSummaryResponse>(`${this.resourceUri}/rent-offer/${cartOfferId}`, request);
  }
}
