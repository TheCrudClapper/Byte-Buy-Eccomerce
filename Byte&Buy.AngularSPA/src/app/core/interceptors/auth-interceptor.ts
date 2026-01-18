import { HttpErrorResponse, HttpInterceptorFn } from "@angular/common/http";
import { inject } from "@angular/core";
import { Router } from "@angular/router";
import { AuthService } from "../clients/auth/auth-service";
import { catchError, throwError } from "rxjs";

export const authInterceptor: HttpInterceptorFn = (req, next) => {
    const router = inject(Router);
    const auth = inject(AuthService);

    const token = localStorage.getItem('token');

    const authReq = token 
        ? req.clone({ setHeaders: { Authorization: `Bearer ${token}` }})
        : req;

    return next(authReq).pipe(
        catchError((err: HttpErrorResponse) => {
            if(err.status === 401 ){
                auth.logout();
                router.navigate(['/login']);
            }

            if(err.status === 403){
                router.navigate(['/forbidden']);
            }

            return throwError(() => err);
        })
    )
}