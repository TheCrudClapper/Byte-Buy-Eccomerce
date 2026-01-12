import { inject, Injectable, signal } from '@angular/core';
import { TokenResponse } from '../dto/token-response';
import { LoginRequest } from '../dto/login-request';
import { RegisterRequest } from '../dto/register-request';
import { BehaviorSubject, Observable, tap } from 'rxjs';
import { HttpClient } from '@angular/common/http';

@Injectable({
  providedIn: 'root',
})

export class AuthService {
  private readonly resourceUri = "http://localhost:5099/api/auth";
  private readonly loggedIn = signal<boolean>(false);

  constructor(private httpClient: HttpClient){
    this.loggedIn.set(!!localStorage.getItem('token'));
  }

  login(request: LoginRequest): Observable<TokenResponse> {
   return this.httpClient
      .post<TokenResponse>(`${this.resourceUri}/login`, request)
      .pipe(
        tap(response => {
          localStorage.setItem('token', response.token);
          this.loggedIn.set(true);
        })
      );
  }

  register(request: RegisterRequest): Observable<void> {
    return this.httpClient.post<void>(`${this.resourceUri}/register`, request);
  }

  logout(): void{
    localStorage.removeItem('token');
    this.loggedIn.set(false);
  }

  isLoggedIn(): boolean{
    return this.loggedIn();
  }
}
