import { HttpClient } from '@angular/common/http';
import { inject, Injectable } from '@angular/core';
import { environment } from '../../../../environments/environment';
import { Guid } from 'guid-typescript';
import { API_ENDPOINTS } from '../../constants/api-constants';

@Injectable({
  providedIn: 'root',
})
export class DocumentsApiService {
  private readonly httpClient = inject(HttpClient);
  private readonly baseUrl = environment.apiBaseUrl;

  downloadOrderDetails(id: Guid){
    return this.httpClient.get(`${this.baseUrl}${API_ENDPOINTS.documents.orderDetails(id)}`, { responseType: 'blob'});
  }
}
