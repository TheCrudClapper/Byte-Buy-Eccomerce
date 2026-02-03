import { HttpClient } from '@angular/common/http';
import { inject, Injectable } from '@angular/core';
import { environment } from '../../../../environments/environment';
import { Observable } from 'rxjs';
import { API_ENDPOINTS } from '../../constants/api-constants';
import { OrderAddRequest } from '../../dto/order/order-add-request';
import { OrderCreatedResponse } from '../../dto/order/order-created-response';
import { UserOrderListResponse } from '../../dto/order/common/user-order-list-response';
import { OrderDetailsResponse } from '../../dto/order/order-details-response';
import { Guid } from 'guid-typescript';
import { UpdatedResponse } from '../../dto/common/updated-response';

@Injectable({
  providedIn: 'root',
})

export class OrderApiService {
  private readonly httpClient = inject(HttpClient);
  private readonly baseApiUrl = environment.apiBaseUrl;

  postOrder(request: OrderAddRequest): Observable<OrderCreatedResponse> {
    return this.httpClient.post<OrderCreatedResponse>(`${this.baseApiUrl + API_ENDPOINTS.orders.base}`, request);
  }

  getUserOrders(): Observable<UserOrderListResponse[]> {
    return this.httpClient.get<UserOrderListResponse[]>(`${this.baseApiUrl + API_ENDPOINTS.orders.base}`)
  }

  getOrderDetails(id: Guid): Observable<OrderDetailsResponse>{
    return this.httpClient.get<OrderDetailsResponse>(`${this.baseApiUrl + API_ENDPOINTS.orders.details(id)}`)
  }
  
  cancelOrder(id: Guid): Observable<UpdatedResponse>{
    return this.httpClient.put<UpdatedResponse>(`${this.baseApiUrl + API_ENDPOINTS.orders.cancel(id)}`, null);
  }

  returnOrder(id: Guid): Observable<UpdatedResponse>{
    return this.httpClient.put<UpdatedResponse>(`${this.baseApiUrl + API_ENDPOINTS.orders.return(id)}`, null);
  }
}
