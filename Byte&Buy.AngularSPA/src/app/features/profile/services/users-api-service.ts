import { HttpClient } from '@angular/common/http';
import { inject, Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { UserBasicInfoResponse } from '../api-dto/user-basic-info-response';
import { PasswordChangeRequest } from '../api-dto/password-change-request';

@Injectable({
  providedIn: 'root',
})

export class UsersApiService {
  private readonly resourceUri = "http://localhost:5099/api/users";
  private readonly httpClient = inject(HttpClient);
  
  getUserBasicInfo(): Observable<UserBasicInfoResponse>{
    return this.httpClient.get<UserBasicInfoResponse>(`${this.resourceUri}/me`);
  }

  changePassword(request: PasswordChangeRequest){
    return this.httpClient.put(`${this.resourceUri}/password`, request);
  }
}
