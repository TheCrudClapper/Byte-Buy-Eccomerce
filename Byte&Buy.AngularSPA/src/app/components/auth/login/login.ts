import { Component, inject } from '@angular/core';
import { FormControl, FormGroup, ReactiveFormsModule, Validators, ɵInternalFormsSharedModule } from '@angular/forms';
import { AuthService } from '../../../core/services/auth-service';
import { LoginRequest } from '../../../core/dto/login-request';
import { HttpErrorResponse } from '@angular/common/http';
import { ProblemDetails } from '../../../core/dto/problem-details';
import { Router } from '@angular/router';

@Component({
  selector: 'app-login',
  imports: [ɵInternalFormsSharedModule, ReactiveFormsModule],
  standalone: true,
  templateUrl: './login.html',
  styleUrl: './login.scss',
})

export class Login {
  private authService: AuthService = inject(AuthService);

  loginForm: FormGroup = new FormGroup({
    email: new FormControl('', [Validators.required, Validators.email]),
    password: new FormControl('', Validators.required)
  })

  constructor(private router: Router){

  }
  errorMessage: string | null = null;

  onSubmit() {
    if (this.loginForm.invalid) {
      this.loginForm.markAllAsTouched();
      return;
    }

    console.log("clicke");
    const request = this.loginForm.getRawValue() as LoginRequest;

    this.authService.login(request).subscribe({
      next: () => this.router.navigate(['']),
      error: (error: HttpErrorResponse) => {
        const problem = error.error as ProblemDetails;
        this.errorMessage = problem?.detail ?? "Something went wrong";
      }
    });

  }
}
