import { HttpClient } from '@angular/common/http';
import { inject, Injectable } from '@angular/core';
import { environment } from '../../../../environments/environment';
import { Observable } from 'rxjs';
import { CreatedResponse } from '../../dto/common/created-response';
import { API_ENDPOINTS } from '../../constants/api-constants';
import { OrderAddRequest } from '../../dto/order/order-add-request';

@Injectable({
  providedIn: 'root',
})

export class OrderApiService {
  private readonly httpClient = inject(HttpClient);
  private readonly baseApiUrl = environment.apiBaseUrl;

  postOrder(request: OrderAddRequest): Observable<CreatedResponse>{
    return this.httpClient.post<CreatedResponse>(`${this.baseApiUrl + API_ENDPOINTS.orders.post}`, request);
  }
  
}
