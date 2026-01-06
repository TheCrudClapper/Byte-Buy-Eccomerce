import { Component, inject } from '@angular/core';
import { FormControl, FormGroup, ReactiveFormsModule, Validators, ɵInternalFormsSharedModule } from '@angular/forms';
import { RegisterRequest } from '../../../core/dto/register-request';
import { AuthService } from '../../../core/services/auth-service';
import { ProblemDetails } from '../../../core/dto/problem-details';
import { HttpErrorResponse } from '@angular/common/http';

@Component({
  selector: 'app-register',
  imports: [ɵInternalFormsSharedModule, ReactiveFormsModule],
  templateUrl: './register.html',
  styleUrl: './register.scss',
})
export class Register {
  private readonly authService: AuthService = inject(AuthService);

  registerForm: FormGroup = new FormGroup({
    firstName: new FormControl('', [Validators.maxLength(50), Validators.required]),
    lastName: new FormControl('', [Validators.maxLength(50), Validators.required]),
    email: new FormControl('', [Validators.required, Validators.email]),
    password: new FormControl('', [Validators.required])
  });

  errorMessage: string | null = null;
   
  onSubmit() {
    if (this.registerForm.invalid) {
      this.registerForm.markAllAsTouched();
      return;
    }

    const request = this.registerForm.getRawValue() as RegisterRequest;

    this.authService.register(request).subscribe({
      next: _ => {
        alert("Successfully regstered");
      },
      error: (error: HttpErrorResponse) => {
        const problem = error.error as ProblemDetails;
        this.errorMessage = problem?.detail ?? "Something went wrong";
      }
    });

  }
}
