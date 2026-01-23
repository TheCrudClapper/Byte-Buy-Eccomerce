import { HttpClient } from '@angular/common/http';
import { inject, Injectable } from '@angular/core';
import { map, Observable } from 'rxjs';
import { SelectListItem } from '../../../shared/models/select-list-item';
import { SelectListItemResponse } from '../../dto/common/select-list-item-response';
import { DeliveryResponse } from '../../dto/delivery/delivery-response';
import { DeliveryOption } from '../../../shared/models/delivery-options';
import { DeliveryOptionsResponse } from '../../dto/delivery/delivery-options-response';
import { Guid } from 'guid-typescript';
import { API_ENDPOINTS } from '../../constants/api-constants';
import { environment } from '../../../../environments/environment';

@Injectable({
  providedIn: 'root',
})
export class DeliveryApiService {
  private readonly httpClient = inject(HttpClient);
  private readonly baseUrl = environment.apiBaseUrl;

  getSelectList(): Observable<SelectListItem[]> {
    return this.httpClient.get<SelectListItemResponse[]>(`${this.baseUrl}${API_ENDPOINTS.deliveries.options}`).pipe(
      map(response => response.map(item => ({
        id: item.id,
        title: item.title
      })))
    );
  }

  getDeliveriesList(): Observable<DeliveryOption[]> {
    return this.httpClient.get<DeliveryResponse[]>(`${this.baseUrl}${API_ENDPOINTS.deliveries.list}`).pipe(
      map((response: DeliveryResponse[]) =>
        response.map((item: DeliveryResponse): DeliveryOption => ({
          id: item.id,
          name: item.name,
          currency: item.currency,
          amount: item.amount,
          carrier: item.carrier
        }))
      )
    );
  }

  getDeliveriesListPerOffer(id: Guid): Observable<DeliveryOption[]> {
    return this.httpClient.get<DeliveryResponse[]>(`${this.baseUrl}${API_ENDPOINTS.deliveries.offer}/${id}`).pipe(
      map((response: DeliveryResponse[]) =>
        response.map((item: DeliveryResponse): DeliveryOption => ({
          id: item.id,
          name: item.name,
          currency: item.currency,
          amount: item.amount,
          carrier: item.carrier
        }))
      )
    );
  }

  getAvaliableDeliveries(): Observable<DeliveryOptionsResponse> {
    return this.httpClient.get<DeliveryOptionsResponse>(`${this.baseUrl}${API_ENDPOINTS.deliveries.available}`);
  }
}