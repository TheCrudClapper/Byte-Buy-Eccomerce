import { Component, inject, signal } from '@angular/core';
import { FormControl, FormGroup, ReactiveFormsModule, Validators, ɵInternalFormsSharedModule } from '@angular/forms';
import { AuthService } from '../../../core/services/auth/auth-service';
import { LoginRequest } from '../../../core/api-dto/login-request';
import { HttpErrorResponse } from '@angular/common/http';
import { ProblemDetails } from '../../../core/api-dto/problem-details';
import { Router } from '@angular/router';
import { shouldShowError } from '../../../core/helpers/form-helper';

@Component({
  selector: 'app-login',
  imports: [ɵInternalFormsSharedModule, ReactiveFormsModule],
  standalone: true,
  templateUrl: './login.html',
  styleUrl: './login.scss',
})

export class Login {
  private authService: AuthService = inject(AuthService);
  private router: Router = inject(Router);
  errorMessage = signal<string>("");

  loginForm: FormGroup = new FormGroup({
    email: new FormControl('', [Validators.required, Validators.email]),
    password: new FormControl('', Validators.required)
  })

  onSubmit() {
    if (this.loginForm.invalid) {
      this.loginForm.markAllAsTouched();
      return;
    }

    const request = this.loginForm.getRawValue() as LoginRequest;

    this.authService.login(request).subscribe({
      next: () => this.router.navigate(['']),
      error: (error: HttpErrorResponse) => {
        const problem = error.error as ProblemDetails;
        this.errorMessage.set(problem?.detail ?? "Something went wrong");
      }
    });
  }

  shouldShowError(controlName: string): boolean{
    return shouldShowError(this.loginForm, controlName);
  }
}
