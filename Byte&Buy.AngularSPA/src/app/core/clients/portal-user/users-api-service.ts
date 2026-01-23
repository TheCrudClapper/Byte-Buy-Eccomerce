import { HttpClient } from '@angular/common/http';
import { inject, Injectable } from '@angular/core';
import { PasswordChangeRequest } from '../../dto/auth/password-change-request';
import { environment } from '../../../../environments/environment';
import { API_ENDPOINTS } from '../../constants/api-constants';

@Injectable({
  providedIn: 'root',
})

export class UsersApiService {
  private readonly httpClient = inject(HttpClient);
  private readonly baseUrl = environment.apiBaseUrl;

  changePassword(request: PasswordChangeRequest) {
    return this.httpClient.put(`${this.baseUrl}${API_ENDPOINTS.users.password}`, request);
  }
}
