import { inject, Injectable } from '@angular/core';
import { TokenResponse } from '../dto/token-response';
import { LoginRequest } from '../dto/login-request';
import { RegisterRequest } from '../dto/register-request';
import { Observable } from 'rxjs';
import { HttpClient } from '@angular/common/http';
import { ProblemDetails } from '../dto/problem-details';

@Injectable({
  providedIn: 'root',
})

export class AuthService {
  private readonly resourceUri = "http://localhost:5099/api/auth";
  private readonly httpClinet = inject(HttpClient);

  login(request: LoginRequest): Observable<TokenResponse> {
    return this.httpClinet.post<TokenResponse>(`${this.resourceUri}/login`, request);
  }

  register(request: RegisterRequest): Observable<void> {
    return this.httpClinet.post<void>(`${this.resourceUri}/register`, request);
  }
}
