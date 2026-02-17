import { Component, inject, signal, Signal } from '@angular/core';
import { FormControl, FormGroup, ReactiveFormsModule, Validators, ɵInternalFormsSharedModule } from '@angular/forms';
import { RegisterRequest } from '../../../../core/dto/auth/register-request';
import { AuthService } from '../../../../core/clients/auth/auth-service';
import { ProblemDetails } from '../../../../core/dto/problem-details';
import { Router } from '@angular/router';
import { getErrorMessage } from '../../../../shared/helpers/form-helper';
import { finalize } from 'rxjs';
import { ToastService } from '../../../../shared/services/snackbar/toast-service';

@Component({
  selector: 'app-register',
  imports: [ɵInternalFormsSharedModule, ReactiveFormsModule],
  templateUrl: './register-page.html',
  standalone: true,
  styleUrl: './register-page.scss',
})
export class RegisterPage {
  private readonly authService: AuthService = inject(AuthService);
  private readonly router: Router = inject(Router);
  private readonly snackBarService = inject(ToastService);

  errorMessage = signal<string>("");
  loading = signal<boolean>(false);

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

    this.loading.set(true);

    const request = this.registerForm.getRawValue() as RegisterRequest;

    this.authService.register(request)
    .pipe(finalize(() => this.loading.set(false)))
    .subscribe({
      next: () => {
        this.snackBarService.success("Successfully registered!");
        this.router.navigate(['login']);
      },
      error: (error: ProblemDetails) => {
        this.errorMessage.set(error?.detail?.replace(';', '\n') ?? "Something went wrong");
      }
    });
  }

  getErrorMessage(path: string){
    return getErrorMessage(this.registerForm, path);
  }
}
