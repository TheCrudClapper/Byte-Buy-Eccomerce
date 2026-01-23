import { HttpClient } from '@angular/common/http';
import { inject, Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { UserBasicInfoResponse } from '../../dto/portal-user/user-basic-info-response';
import { UpdatedResponse } from '../../dto/common/updated-response';
import { UserBasicInfoUpdateRequest } from '../../dto/portal-user/user-basic-info-update-request';
import { environment } from '../../../../environments/environment';
import { API_ENDPOINTS } from '../../constants/api-constants';

@Injectable({
  providedIn: 'root',
})

export class PortalUserApiService {
  private readonly httpClient = inject(HttpClient);
  private readonly baseUrl = environment.apiBaseUrl;

  getUserBasicInfo(): Observable<UserBasicInfoResponse> {
    return this.httpClient.get<UserBasicInfoResponse>(`${this.baseUrl}${API_ENDPOINTS.portalUsers.me}`);
  }

  putUserBasicInfo(request: UserBasicInfoUpdateRequest): Observable<UpdatedResponse> {
    return this.httpClient.put<UpdatedResponse>(`${this.baseUrl}${API_ENDPOINTS.portalUsers.me}`, request);
  }
}