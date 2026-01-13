import { HttpClient } from '@angular/common/http';
import { inject, Injectable } from '@angular/core';
import { map, Observable } from 'rxjs';
import { SelectListItem } from '../../../shared/models/select-list-item';
import { SelectListItemResponse } from '../../../shared/api-dto/select-list-item-response';
import { DeliveryResponse } from '../../api-dto/delivery-response';
import { DeliveryListItem } from '../../../shared/models/delivery-list-items';
import { DeliveryOptionsResponse } from '../../../shared/api-dto/delivery-options-response';

@Injectable({
  providedIn: 'root',
})
export class DeliveryApiService {
  private readonly resourceUri = "http://localhost:5099/api/deliveries";
  private readonly httpClient = inject(HttpClient);
  
  getSelectList(): Observable<SelectListItem[]>{
    return this.httpClient.get<SelectListItemResponse[]>(`${this.resourceUri}/options`).pipe(
      map(response => response.map(item => ({
        id: item.id,
        title: item.title
      })))
    );
  }
  
  getDeliveriesList(): Observable<DeliveryResponse[]>{
    return this.httpClient.get<DeliveryResponse[]>(`${this.resourceUri}/list`).pipe(
      map(response => response.map(item => ({
        id: item.id,
        name: item.name,
        currency: item.currency,
        amount: item.amount,
        carrier: item.carrier
      })))
    );
  }

  getAvaliableDeliveries(): Observable<DeliveryOptionsResponse>{
    return this.httpClient.get<DeliveryOptionsResponse>(`${this.resourceUri}/available`);
  }

}
