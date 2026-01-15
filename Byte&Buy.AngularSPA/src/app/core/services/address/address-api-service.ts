import { HttpClient } from '@angular/common/http';
import { inject, Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { HomeAddressDto } from '../../../shared/api-dto/home-address-dto';
import { UpdatedResponse } from '../../../shared/api-dto/updated-response';
import { ShippingAddressListItem } from '../../../features/profile/model/shipping-address-list-item';
import { ShippingAddressListResponse } from '../../../features/profile/api-dto/shipping-address-list-response';
import { ShippingAddressResponse } from '../../../features/profile/api-dto/shipping-address-response';
import { Guid } from 'guid-typescript';
import { ShippingAddressUpdateRequest } from '../../../features/profile/api-dto/shipping-address-update-request';
import { ShippingAddressAddRequest } from '../../../features/profile/api-dto/shipping-address-add-request';
import { CreatedResponse } from '../../../shared/api-dto/created-response';

@Injectable({
  providedIn: 'root',
})
export class AddressApiService {
    private readonly resourceUri = "http://localhost:5099/api/me";
    private readonly httpClient: HttpClient = inject(HttpClient); 
    
    getHomeAddress(): Observable<HomeAddressDto>{
      return this.httpClient.get<HomeAddressDto>(`${this.resourceUri}/home-address`);
    }

    putHomeAddress(request: HomeAddressDto): Observable<UpdatedResponse>{
      return this.httpClient.put<UpdatedResponse>(`${this.resourceUri}/home-address`, request);
    }

    getShippingAddressesList(): Observable<ShippingAddressListItem[]>{
      return this.httpClient.get<ShippingAddressListResponse[]>(`${this.resourceUri}/shipping-addresses/list`);
    }
    
    getShippingAddress(id: Guid): Observable<ShippingAddressResponse>{
      return this.httpClient.get<ShippingAddressResponse>(`${this.resourceUri}/shipping-addresses/${id}`);
    }

    putShippingAddress(id: Guid, request: ShippingAddressUpdateRequest): Observable<UpdatedResponse>{
      return this.httpClient.put<UpdatedResponse>(`${this.resourceUri}/shipping-addresses/${id}`, request);
    }

    postShippingAddress(request: ShippingAddressAddRequest): Observable<CreatedResponse>{
      return this.httpClient.post<CreatedResponse>(`${this.resourceUri}/shipping-addresses`, request);
    }

    deleteShippingAddress(id: Guid): Observable<Object>{
      return this.httpClient.delete(`${this.resourceUri}/shipping-addresses/${id}`);
    }
}
