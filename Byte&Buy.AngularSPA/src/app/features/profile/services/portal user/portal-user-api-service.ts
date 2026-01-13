import { HttpClient } from '@angular/common/http';
import { inject, Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { UserBasicInfoResponse } from '../../api-dto/user-basic-info-response';
import { UpdatedResponse } from '../../../../shared/api-dto/updated-response';
import { UserBasicInfoUpdateRequest } from '../../api-dto/user-basic-info-update-request';

@Injectable({
  providedIn: 'root',
})

export class PortalUserApiService {
  private readonly resourceUri = "http://localhost:5099/api/portalusers";
  private readonly httpClient = inject(HttpClient);

  getUserBasicInfo(): Observable<UserBasicInfoResponse>{
    return this.httpClient.get<UserBasicInfoResponse>(`${this.resourceUri}/me`);
  }

  putUserBasicInfo(request: UserBasicInfoUpdateRequest): Observable<UpdatedResponse>{
    return this.httpClient.put<UpdatedResponse>(`${this.resourceUri}/me`, request);
  }

}
