import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { HttpClient } from '@angular/common/http';
import { CompanyInfoResponse } from '../../models/companyInfo/companyInfoResponse';
@Injectable({
  providedIn: 'root',
})
export class CompanyInfoApiService {
  private API_URL = "http://localhost:5099/api/companyInfo";

  constructor(private httpClient: HttpClient){}

  getCompanyInfo() : Observable<CompanyInfoResponse> {
    return this.httpClient.get<CompanyInfoResponse>(this.API_URL);
  }
}
