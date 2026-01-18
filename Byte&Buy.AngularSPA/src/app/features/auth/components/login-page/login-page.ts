import { Component, inject, signal } from '@angular/core';
import { FormControl, FormGroup, ReactiveFormsModule, Validators, ɵInternalFormsSharedModule } from '@angular/forms';
import { AuthService } from '../../../../core/clients/auth/auth-service';
import { LoginRequest } from '../../../../core/dto/auth/login-request';
import { HttpErrorResponse } from '@angular/common/http';
import { ProblemDetails } from '../../../../core/dto/problem-details';
import { Router } from '@angular/router';
import { finalize } from 'rxjs';
import { getErrorMessage } from '../../../../shared/helpers/form-helper';

@Component({
  selector: 'app-login',
  imports: [ɵInternalFormsSharedModule, ReactiveFormsModule],
  standalone: true,
  templateUrl: './login-page.html',
  styleUrl: './login-page.scss',
})

export class LoginPage{
  private authService: AuthService = inject(AuthService);
  private router: Router = inject(Router);

  errorMessage = signal<string>("");
  loading = signal<boolean>(false);

  loginForm: FormGroup = new FormGroup({
    email: new FormControl('', [Validators.required, Validators.email]),
    password: new FormControl('', Validators.required)
  })

   onSubmit() {
    if (this.loginForm.invalid) {
      this.loginForm.markAllAsTouched();
      return;
    }

    this.loading.set(true);
    const request = this.loginForm.getRawValue() as LoginRequest;

    this.authService.login(request)
    .pipe(finalize(() => this.loading.set(false))) 
    .subscribe({  
      next: () => this.router.navigate(['']),
      error: (error: ProblemDetails) => {
        this.errorMessage.set(error.detail ?? 'Something went wrong');
      }
    });
  }

  getErrorMessage(path: string){
    return getErrorMessage(this.loginForm, path);
  }
}
