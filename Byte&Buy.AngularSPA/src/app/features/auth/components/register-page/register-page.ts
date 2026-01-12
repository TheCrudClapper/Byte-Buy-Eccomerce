import { Component, inject, signal, Signal } from '@angular/core';
import { FormControl, FormGroup, ReactiveFormsModule, Validators, ɵInternalFormsSharedModule } from '@angular/forms';
import { RegisterRequest } from '../../../../core/api-dto/register-request';
import { AuthService } from '../../../../core/services/auth/auth-service';
import { ProblemDetails } from '../../../../core/api-dto/problem-details';
import { HttpErrorResponse } from '@angular/common/http';
import { Router } from '@angular/router';
import { shouldShowError } from '../../../../core/helpers/form-helper';

@Component({
  selector: 'app-register',
  imports: [ɵInternalFormsSharedModule, ReactiveFormsModule],
  templateUrl: './register-page.html',
  styleUrl: './register-page.scss',
})
export class RegisterPage {
  private readonly authService: AuthService = inject(AuthService);
  private readonly router: Router = inject(Router);
  errorMessage = signal<string>("");

  registerForm: FormGroup = new FormGroup({
    firstName: new FormControl('', [Validators.maxLength(50), Validators.required]),
    lastName: new FormControl('', [Validators.maxLength(50), Validators.required]),
    email: new FormControl('', [Validators.required, Validators.email]),
    password: new FormControl('', [Validators.required])
  });

  onSubmit() {
    if (this.registerForm.invalid) {
      this.registerForm.markAllAsTouched();
      return;
    }

    const request = this.registerForm.getRawValue() as RegisterRequest;

    this.authService.register(request).subscribe({
      next: () => {
        this.router.navigate(['login']);
      },
      error: (error: HttpErrorResponse) => {
        const problem = error.error as ProblemDetails;
        this.errorMessage.set(problem?.detail?.replace(';', '\n') ?? "Something went wrong");
      }
    });
  }

  shouldShowError(controlName: string): boolean{
    return shouldShowError(this.registerForm, controlName);
  }
}
