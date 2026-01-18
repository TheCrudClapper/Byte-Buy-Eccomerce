import { inject, Injectable } from '@angular/core';
import { CreatedResponse } from '../../../dto/common/created-response';
import { Observable } from 'rxjs';
import { Guid } from 'guid-typescript';
import { HttpClient } from '@angular/common/http';
import { UpdatedResponse } from '../../../dto/common/updated-response';
import { UserRentOfferResponse } from '../../../dto/offers/rent/user-rent-offer-response';

@Injectable({
  providedIn: 'root',
})
export class RentOfferApiSerivce {
  private readonly resourceUri = "http://localhost:5099/api/me/rent-offer";
  private readonly httpClient: HttpClient = inject(HttpClient);

  post(payload: FormData): Observable<CreatedResponse>{
    return this.httpClient.post<CreatedResponse>(`${this.resourceUri}`, payload);
  }

  put(id: Guid, payload: FormData): Observable<UpdatedResponse>{
    return this.httpClient.put<UpdatedResponse>(`${this.resourceUri}/${id}`, payload);
  }

  delete(id: Guid){
    return this.httpClient.delete<UpdatedResponse>(`${this.resourceUri}/${id}`,);
  }

  getById(id: Guid): Observable<UserRentOfferResponse>{
    return this.httpClient.get<UserRentOfferResponse>(`${this.resourceUri}/${id}`);
  }
}
