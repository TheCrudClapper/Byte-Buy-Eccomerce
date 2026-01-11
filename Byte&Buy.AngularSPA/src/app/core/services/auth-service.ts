import { inject, Injectable } from '@angular/core';
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
  private loggedInSubject$ = new BehaviorSubject<boolean>(false);
  private loggedIn$ = this.loggedInSubject$.asObservable();

  constructor(private httpClient: HttpClient){
    this.loggedInSubject$.next(!!localStorage.getItem('token'));
  }

  login(request: LoginRequest): Observable<TokenResponse> {
   return this.httpClient
      .post<TokenResponse>(`${this.resourceUri}/login`, request)
      .pipe(
        tap(response => {
          localStorage.setItem('token', response.token);
          this.loggedInSubject$.next(true);
        })
      );
  }

  register(request: RegisterRequest): Observable<void> {
    return this.httpClient.post<void>(`${this.resourceUri}/register`, request);
  }

  logout(): void{
    localStorage.removeItem('token');
    this.loggedInSubject$.next(false);
  }

  isLoggedIn(): boolean{
    return this.loggedInSubject$.value;
  }
}
