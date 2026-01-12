import { inject } from "@angular/core";
import { CanActivateFn, Router } from "@angular/router";
import { AuthService } from "../services/auth/auth-service";

//Guard that restricts urls based on user logged in state
export const authGuard: CanActivateFn = () => {
    const auth = inject(AuthService);
    const router = inject(Router);   

    return auth.isLoggedIn()
        ? true
        : router.createUrlTree(['/login']);
}