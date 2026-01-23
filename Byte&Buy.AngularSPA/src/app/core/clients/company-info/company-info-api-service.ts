import { inject, Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { HttpClient } from '@angular/common/http';
import { CompanyInfoResponse } from '../../dto/company/company-info-response';
import { environment } from '../../../../environments/environment';
import { API_ENDPOINTS } from '../../constants/api-constants';

@Injectable({
  providedIn: 'root',
})
export class CompanyInfoApiService {
  private readonly httpClient: HttpClient = inject(HttpClient);
  private readonly baseUrl = environment.apiBaseUrl;

  getCompanyInfo(): Observable<CompanyInfoResponse> {
    return this.httpClient.get<CompanyInfoResponse>(`${this.baseUrl}${API_ENDPOINTS.companyInfo.get}`);
  }
}