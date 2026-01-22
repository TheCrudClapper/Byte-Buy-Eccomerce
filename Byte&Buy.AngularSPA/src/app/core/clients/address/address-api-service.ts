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
