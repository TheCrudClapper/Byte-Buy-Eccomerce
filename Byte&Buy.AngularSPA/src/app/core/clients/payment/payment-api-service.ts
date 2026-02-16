import { HttpClient } from '@angular/common/http';
import { inject, Injectable } from '@angular/core';
import { environment } from '../../../../environments/environment';
import { Observable } from 'rxjs';
import { API_ENDPOINTS } from '../../constants/api-constants';
import { Guid } from 'guid-typescript';
import { BlikPaymentRequest } from '../../dto/payment/blik-payment-request';
import { CardPaymentRequest } from '../../dto/payment/card-payment-request';
import { PaymentResponse } from '../../dto/payment/payment-response';

@Injectable({
  providedIn: 'root',
})
export class PaymentApiService {
  private readonly httpClient = inject(HttpClient);
  private readonly baseApiUrl = environment.apiBaseUrl;

  getPaymentResponse(id: Guid): Observable<PaymentResponse>{
    return this.httpClient
      .get<PaymentResponse>(`${this.baseApiUrl}${API_ENDPOINTS.payments.get(id)}`);
  }

  payWithBlik(id: Guid, request: BlikPaymentRequest){
    return this.httpClient
      .put(`${this.baseApiUrl}${API_ENDPOINTS.payments.blik(id)}`, request);
  }

   payWithCard(id: Guid, request: CardPaymentRequest){
    return this.httpClient
      .put(`${this.baseApiUrl}${API_ENDPOINTS.payments.card(id)}`, request);
  }
}
