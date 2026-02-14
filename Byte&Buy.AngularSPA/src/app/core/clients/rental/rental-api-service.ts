import { HttpClient } from '@angular/common/http';
import { inject, Injectable } from '@angular/core';
import { environment } from '../../../../environments/environment';
import { Guid } from 'guid-typescript';
import { Observable } from 'rxjs';
import { UserRentalBorrowerResponse } from '../../dto/rental/user-rental-borrower-response';
import { API_ENDPOINTS } from '../../constants/api-constants';
import { RentalLenderResponse } from '../../dto/rental/rental-lender-response';
import { UpdatedResponse } from '../../dto/common/updated-response';
import { PagedList } from '../../pagination/pagedList';
import { UserRentalBorrowerQuery } from '../../dto/rental/common/rental-list-query';
import { UserRentalLenderQuery } from '../../dto/rental/common/user-rental-lender-query';

@Injectable({
  providedIn: 'root',
})
export class RentalApiService {
  private readonly httpClient = inject(HttpClient);
  private readonly baseUrl = environment.apiBaseUrl;

   getBorrowerRentals(query: UserRentalBorrowerQuery): Observable<PagedList<UserRentalBorrowerResponse>> {
    return this.httpClient.get<PagedList<UserRentalBorrowerResponse>>(
      `${this.baseUrl}${API_ENDPOINTS.rentals.borrowerList}`, { params: query as any});
  }

  getLenderRentals(query: UserRentalLenderQuery): Observable<PagedList<RentalLenderResponse>> {
    return this.httpClient.get<PagedList<RentalLenderResponse>> (
      `${this.baseUrl}${API_ENDPOINTS.rentals.lenderList}`, { params: query as any});
  }

  returnRental(id: Guid): Observable<UpdatedResponse> {
    return this.httpClient.put<UpdatedResponse>(`${this.baseUrl}${API_ENDPOINTS.rentals.return(id)}`, null);
  }
}
