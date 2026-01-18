import { HttpClient } from '@angular/common/http';
import { inject, Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root',
})
export class SaleOfferApiService {
  private readonly httpClient = inject(HttpClient);
  private readonly resourceUri = "http://localhost:5099/api/offers";  

  
}
