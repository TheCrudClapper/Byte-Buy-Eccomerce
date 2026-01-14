import { HttpClient } from '@angular/common/http';
import { inject, Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { HomeAddressDto } from '../../../shared/api-dto/home-address-dto';
import { UpdatedResponse } from '../../../shared/api-dto/updated-response';


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
}
