import { HttpClient } from '@angular/common/http';
import { inject, Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { CartResponse } from '../../dto/cart/cart-response';
import { Guid } from 'guid-typescript';
import { SaleCartOfferAddRequest } from '../../dto/cart/cart-item/sale-cart-offer-add-request';
import { SaleCartOfferUpdateRequest } from '../../dto/cart/cart-item/sale-cart-offer-update-request';
import { RentCartOfferAddRequest } from '../../dto/cart/cart-item/rent-cart-offer-add-request';
import { RentCartOfferUpdateRequest } from '../../dto/cart/cart-item/rent-cart-offer-update-request';

@Injectable({
  providedIn: 'root',
})
export class CartApiService {
  private readonly httpClient = inject(HttpClient);
  private readonly resourceUri = "http://localhost:5099/api/carts";

  getCart(): Observable<CartResponse>{
    return this.httpClient.get<CartResponse>(`${this.resourceUri}`);
  }

  deleteCartOffer(cartOfferId: Guid){
    return this.httpClient.delete(`${this.resourceUri}/${cartOfferId}`);
  }
  
  postSaleCartOffer(request: SaleCartOfferAddRequest){
    return this.httpClient.post(`${this.resourceUri}/sale-offer`, request);
  }

  putSaleCartOffer(cartOfferId: Guid, request: SaleCartOfferUpdateRequest){
    return this.httpClient.put(`${this.resourceUri}/sale-offer/${cartOfferId}`, request);
  }

  postRentCartOffer(request: RentCartOfferAddRequest){
    return this.httpClient.post(`${this.resourceUri}/rent-offer`, request);
  }

  putRentCartOffer(cartOfferId: Guid, request: RentCartOfferUpdateRequest){
    return this.httpClient.put(`${this.resourceUri}/rent-offer/${cartOfferId}`, request);
  }
}
