import { HttpClient } from '@angular/common/http';
import { inject, Injectable } from '@angular/core';
import { Guid } from 'guid-typescript';
import { Observable } from 'rxjs';
import { UpdatedResponse } from '../../../dto/common/updated-response';
import { CreatedResponse } from '../../../dto/common/created-response';
import { UserSaleOfferResponse } from '../../../dto/offers/sale/user-sale-offer-response';

@Injectable({
  providedIn: 'root',
})
export class SaleOfferApiService {
  private readonly httpClient = inject(HttpClient);
  private readonly resourceUri = "http://localhost:5099/api/me/sale-offer";  

   post(payload: FormData): Observable<CreatedResponse>{
      return this.httpClient.post<CreatedResponse>(`${this.resourceUri}`, payload);
    }
  
    put(id: Guid, payload: FormData): Observable<UpdatedResponse>{
      return this.httpClient.put<UpdatedResponse>(`${this.resourceUri}/${id}`, payload);
    }
  
    delete(id: Guid){
      return this.httpClient.delete<UpdatedResponse>(`${this.resourceUri}/${id}`,);
    }
  
    getById(id: Guid): Observable<UserSaleOfferResponse>{
      return this.httpClient.get<UserSaleOfferResponse>(`${this.resourceUri}/${id}`);
    }
  
}
