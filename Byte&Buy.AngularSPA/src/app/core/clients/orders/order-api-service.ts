import { HttpClient } from '@angular/common/http';
import { inject, Injectable } from '@angular/core';
import { environment } from '../../../../environments/environment';
import { Observable } from 'rxjs';
import { API_ENDPOINTS } from '../../constants/api-constants';
import { OrderAddRequest } from '../../dto/order/order-add-request';
import { OrderCreatedResponse } from '../../dto/order/order-created-response';
import { UserOrderListResponse } from '../../dto/order/common/user-order-list-response';

@Injectable({
  providedIn: 'root',
})

export class OrderApiService {
  private readonly httpClient = inject(HttpClient);
  private readonly baseApiUrl = environment.apiBaseUrl;

  postOrder(request: OrderAddRequest): Observable<OrderCreatedResponse> {
    return this.httpClient.post<OrderCreatedResponse>(`${this.baseApiUrl + API_ENDPOINTS.orders.post}`, request);
  }

  getUserOrders(): Observable<UserOrderListResponse[]> {
    return this.httpClient.get<UserOrderListResponse[]>(`${this.baseApiUrl + API_ENDPOINTS.orders.base}`)
  }
}
