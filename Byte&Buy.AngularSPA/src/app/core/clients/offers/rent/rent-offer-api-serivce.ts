import { inject, Injectable } from '@angular/core';
import { CreatedResponse } from '../../../dto/common/created-response';
import { Observable } from 'rxjs';
import { Guid } from 'guid-typescript';
import { HttpClient } from '@angular/common/http';
import { UpdatedResponse } from '../../../dto/common/updated-response';
import { UserRentOfferResponse } from '../../../dto/offers/rent/user-rent-offer-response';
import { environment } from '../../../../../environments/environment';
import { API_ENDPOINTS } from '../../../constants/api-constants';

@Injectable({
  providedIn: 'root',
})
export class RentOfferApiService {  
  private readonly httpClient: HttpClient = inject(HttpClient);
  private readonly baseUrl = environment.apiBaseUrl;

  post(payload: FormData): Observable<CreatedResponse> {
    return this.httpClient.post<CreatedResponse>(`${this.baseUrl}${API_ENDPOINTS.rentOffer.base}`, payload);
  }

  put(id: Guid, payload: FormData): Observable<UpdatedResponse> {
    return this.httpClient.put<UpdatedResponse>(`${this.baseUrl}${API_ENDPOINTS.rentOffer.base}/${id}`, payload);
  }

  delete(id: Guid) {
    return this.httpClient.delete<UpdatedResponse>(`${this.baseUrl}${API_ENDPOINTS.rentOffer.byId}/${id}`);
  }

  getById(id: Guid): Observable<UserRentOfferResponse> {
    return this.httpClient.get<UserRentOfferResponse>(`${this.baseUrl}${API_ENDPOINTS.rentOffer.byId}/${id}`);
  }
}
