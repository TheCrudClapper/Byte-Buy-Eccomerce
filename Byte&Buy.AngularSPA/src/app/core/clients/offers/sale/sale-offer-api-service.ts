import { HttpClient } from '@angular/common/http';
import { inject, Injectable } from '@angular/core';
import { Guid } from 'guid-typescript';
import { Observable } from 'rxjs';
import { UpdatedResponse } from '../../../dto/common/updated-response';
import { CreatedResponse } from '../../../dto/common/created-response';
import { UserSaleOfferResponse } from '../../../dto/offers/sale/user-sale-offer-response';
import { environment } from '../../../../../environments/environment';
import { API_ENDPOINTS } from '../../../constants/api-constants';

@Injectable({
  providedIn: 'root',
})
export class SaleOfferApiService {
  private readonly httpClient = inject(HttpClient);
  private readonly baseUrl = environment.apiBaseUrl;

  post(payload: FormData): Observable<CreatedResponse> {
    return this.httpClient.post<CreatedResponse>(`${this.baseUrl}${API_ENDPOINTS.saleOffer.base}`, payload);
  }

  put(id: Guid, payload: FormData): Observable<UpdatedResponse> {
    return this.httpClient.put<UpdatedResponse>(`${this.baseUrl}${API_ENDPOINTS.saleOffer.base}/${id}`, payload);
  }

  delete(id: Guid) {
    return this.httpClient.delete<UpdatedResponse>(`${this.baseUrl}${API_ENDPOINTS.saleOffer.byId}/${id}`);
  }

  getById(id: Guid): Observable<UserSaleOfferResponse> {
    return this.httpClient.get<UserSaleOfferResponse>(`${this.baseUrl}${API_ENDPOINTS.saleOffer.byId}/${id}`);
  }
}
