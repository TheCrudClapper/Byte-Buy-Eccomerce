import { Injectable, signal } from '@angular/core';
import { TokenResponse } from '../../dto/token-response';
import { LoginRequest } from '../../dto/auth/login-request';
import { RegisterRequest } from '../../dto/auth/register-request';
import { Observable, tap } from 'rxjs';
import { HttpClient } from '@angular/common/http';
import { environment } from '../../../../environments/environment';
import { API_ENDPOINTS } from '../../constants/api-constants';

@Injectable({
  providedIn: 'root',
})

export class AuthService {
  private readonly loggedIn = signal<boolean>(false);

  constructor(private httpClient: HttpClient){
    this.loggedIn.set(!!localStorage.getItem('token'));
  }

  login(request: LoginRequest): Observable<TokenResponse> {
   return this.httpClient
      .post<TokenResponse>(`${environment.apiBaseUrl}${API_ENDPOINTS.auth.login}`, request)
      .pipe(
        tap(response => {
          localStorage.setItem('token', response.token);
          this.loggedIn.set(true);
        })
      );
  }

  register(request: RegisterRequest): Observable<void> {
    return this.httpClient.post<void>(`${environment.apiBaseUrl}${API_ENDPOINTS.auth.register}`, request);
  }

  logout(): void{
    localStorage.removeItem('token');
    this.loggedIn.set(false);
  }

  isLoggedIn(): boolean{
    return this.loggedIn();
  }
}
