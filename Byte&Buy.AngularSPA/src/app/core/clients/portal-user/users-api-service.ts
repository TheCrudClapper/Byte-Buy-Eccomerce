import { HttpClient } from '@angular/common/http';
import { inject, Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { UserBasicInfoResponse } from '../../dto/portal-user/user-basic-info-response';
import { PasswordChangeRequest } from '../../dto/auth/password-change-request';

@Injectable({
  providedIn: 'root',
})

export class UsersApiService {
  private readonly resourceUri = "http://localhost:5099/api/users";
  private readonly httpClient = inject(HttpClient);

  changePassword(request: PasswordChangeRequest){
    return this.httpClient.put(`${this.resourceUri}/password`, request);
  }
}
