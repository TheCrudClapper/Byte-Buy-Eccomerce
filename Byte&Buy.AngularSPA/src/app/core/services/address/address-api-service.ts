import { HttpClient } from '@angular/common/http';
import { inject, Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { HomeAddressDto } from '../../../shared/api-dto/home-address-dto';

@Injectable({
  providedIn: 'root',
})
export class AddressApiService {
    private readonly resourceUri = "http://localhost:5099/api/me";
    private readonly httpClient: HttpClient = inject(HttpClient); 

    getHomeAddress(): Observable<HomeAddressDto>{
      return this.httpClient.get<HomeAddressDto>(`${this.resourceUri}/home-address`);
    }
}
