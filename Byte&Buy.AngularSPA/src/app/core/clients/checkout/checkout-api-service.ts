import { inject, Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { CheckoutResponse } from '../../dto/checkout/checkout-response';
import { HttpClient } from '@angular/common/http';
import { environment } from '../../../../environments/environment';
import { API_ENDPOINTS } from '../../constants/api-constants';

@Injectable({
  providedIn: 'root',
})
export class CheckoutApiService {
  private readonly httpClient = inject(HttpClient);
  private readonly baseUrl = environment.apiBaseUrl;
   
  getCheckout(): Observable<CheckoutResponse>{
    return this.httpClient.get<CheckoutResponse>(`${this.baseUrl}${API_ENDPOINTS.checkout.base}`);
  }
}
