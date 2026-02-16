import { HttpClient } from '@angular/common/http';
import { inject, Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { HomeAddressDto } from '../../dto/home-address/home-address-dto';
import { UpdatedResponse } from '../../dto/common/updated-response';
import { ShippingAddressListItem } from '../../../features/profile/model/shipping-address-list-item';
import { ShippingAddressListResponse } from '../../dto/shipping-address/shipping-address-list-response';
import { ShippingAddressResponse } from '../../dto/shipping-address/shipping-address-response';
import { Guid } from 'guid-typescript';
import { ShippingAddressUpdateRequest } from '../../dto/shipping-address/shipping-address-update-request';
import { ShippingAddressAddRequest } from '../../dto/shipping-address/shipping-address-add-request';
import { CreatedResponse } from '../../dto/common/created-response';
import { environment } from '../../../../environments/environment';
import { API_ENDPOINTS } from '../../constants/api-constants';
import { ShippingAddressCheckout } from '../../dto/shipping-address/shipping-address-checkout';

@Injectable({
  providedIn: 'root',
})

export class AddressApiService {
  private readonly httpClient: HttpClient = inject(HttpClient);
  private readonly baseUrl = environment.apiBaseUrl;

  getHomeAddress(): Observable<HomeAddressDto> {
    return this.httpClient.get<HomeAddressDto>(`${this.baseUrl}${API_ENDPOINTS.homeAdresses.base}`);
  }

  putHomeAddress(request: HomeAddressDto): Observable<UpdatedResponse> {
    return this.httpClient.put<UpdatedResponse>(`${this.baseUrl}${API_ENDPOINTS.homeAdresses.base}`, request);
  }

  getShippingAddressesList(): Observable<ShippingAddressListItem[]> {
    return this.httpClient.get<ShippingAddressListResponse[]>(`${this.baseUrl}${API_ENDPOINTS.shippingAddresses.shippingAddressesList}`);
  }

  getShippingAddress(id: Guid): Observable<ShippingAddressResponse> {
    return this.httpClient.get<ShippingAddressResponse>(`${this.baseUrl}${API_ENDPOINTS.shippingAddresses.shippingAddressById(id)}`);
  }

  putShippingAddress(id: Guid, request: ShippingAddressUpdateRequest): Observable<UpdatedResponse> {
    return this.httpClient.put<UpdatedResponse>(`${this.baseUrl}${API_ENDPOINTS.shippingAddresses.shippingAddressById(id)}`, request);
  }

  postShippingAddress(request: ShippingAddressAddRequest): Observable<CreatedResponse> {
    return this.httpClient.post<CreatedResponse>(`${this.baseUrl}${API_ENDPOINTS.shippingAddresses.shippingAddressAdd}`, request);
  }

  deleteShippingAddress(id: Guid): Observable<Object> {
    return this.httpClient.delete(`${this.baseUrl}${API_ENDPOINTS.shippingAddresses.shippingAddressDelete(id)}`);
  }

  getShippingAddressCheckout(id?: Guid) : Observable<ShippingAddressCheckout>{
      return this.httpClient.get<ShippingAddressCheckout>(`${this.baseUrl}${API_ENDPOINTS.shippingAddresses.shippingAddressCheckout(id)}`);
  }
  
}
