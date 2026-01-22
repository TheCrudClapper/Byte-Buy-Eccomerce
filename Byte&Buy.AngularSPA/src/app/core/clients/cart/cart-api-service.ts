import { HttpClient } from '@angular/common/http';
import { inject, Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { CartResponse } from '../../dto/cart/cart-response';
import { Guid } from 'guid-typescript';
import { SaleCartOfferAddRequest } from '../../dto/cart/cart-item/sale-cart-offer-add-request';
import { SaleCartOfferUpdateRequest } from '../../dto/cart/cart-item/sale-cart-offer-update-request';
import { RentCartOfferAddRequest } from '../../dto/cart/cart-item/rent-cart-offer-add-request';
import { RentCartOfferUpdateRequest } from '../../dto/cart/cart-item/rent-cart-offer-update-request';
import { CartSummaryResponse } from '../../dto/cart/cart-summary-response';
import { environment } from '../../../../environments/environment';
import { API_ENDPOINTS } from '../../constants/api-constants';

@Injectable({
  providedIn: 'root',
})
export class CartApiService {
  private readonly httpClient = inject(HttpClient);
  private readonly baseUrl = environment.apiBaseUrl;

  getCart(): Observable<CartResponse>{
    return this.httpClient.get<CartResponse>(`${this.baseUrl}${API_ENDPOINTS.carts.base}`);
  }

  deleteCartOffer(cartOfferId: Guid): Observable<CartSummaryResponse>{
    return this.httpClient.delete<CartSummaryResponse>(`${this.baseUrl}${API_ENDPOINTS.carts.base}/${cartOfferId}`);
  }
  
  postSaleCartOffer(request: SaleCartOfferAddRequest){
    return this.httpClient.post(`${this.baseUrl}${API_ENDPOINTS.carts.saleOffer}`, request);
  }

  putSaleCartOffer(cartOfferId: Guid, request: SaleCartOfferUpdateRequest): Observable<CartSummaryResponse>{
    return this.httpClient.put<CartSummaryResponse>(`${this.baseUrl}${API_ENDPOINTS.carts.saleOffer}/${cartOfferId}`, request);
  }

  postRentCartOffer(request: RentCartOfferAddRequest){
    return this.httpClient.post(`${this.baseUrl}${API_ENDPOINTS.carts.rentOffer}`, request);
  }

  putRentCartOffer(cartOfferId: Guid, request: RentCartOfferUpdateRequest): Observable<CartSummaryResponse>{
    return this.httpClient.put<CartSummaryResponse>(`${this.baseUrl}${API_ENDPOINTS.carts.rentOffer}/${cartOfferId}`, request);
  }
}
