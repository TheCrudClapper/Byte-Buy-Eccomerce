import { Component, inject, OnInit, signal } from '@angular/core';
import { UsersApiService } from '../../../../core/clients/portal-user/users-api-service';
import { FormControl, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';
import { PortalUserApiService } from '../../../../core/clients/portal-user/portal-user-api-service';
import { UserBasicInfoUpdateRequest } from '../../../../core/dto/portal-user/user-basic-info-update-request';
import { getErrorMessage } from '../../../../shared/helpers/form-helper';
import { PasswordChangeRequest } from '../../../../core/dto/auth/password-change-request';
import { finalize } from 'rxjs';
import { ToastService } from '../../../../shared/services/snackbar/toast-service';
import { ProblemDetails } from '../../../../core/dto/problem-details';

@Component({
  selector: 'app-personal-info',
  imports: [ReactiveFormsModule],
  templateUrl: './personal-info.html',
  standalone: true,
  styleUrl: './personal-info.scss',
})
export class PersonalInfo implements OnInit {
  private readonly userApiService: UsersApiService = inject(UsersApiService);
  private readonly portalUserApiService: PortalUserApiService = inject(PortalUserApiService);
  private readonly snackBarService: ToastService = inject(ToastService);

  userDataLoading = signal<boolean>(false);
  passwordDataLoading = signal<boolean>(false);

  passwordForm: FormGroup = new FormGroup({
    currentPassword: new FormControl<string>("", [Validators.required, Validators.minLength(8)]),
    newPassword: new FormControl<string>("", [Validators.required, Validators.minLength(8)]),
    confirmPassword: new FormControl<string>("", [Validators.required, Validators.minLength(8)]),
  });

  userInfoForm: FormGroup = new FormGroup({
    firstName: new FormControl<string>("", [Validators.required]),
    lastName: new FormControl<string>("", [Validators.required]),
    email: new FormControl<string>("", [Validators.required, Validators.email]),
    phoneNumber: new FormControl<string | null>("", [Validators.required, Validators.minLength(9)])
  });

  ngOnInit(): void {
    this.userDataLoading.set(true);
    this.loadBasicInfo();
  }

  onPasswordSubmit() {
    if (this.passwordForm.invalid) {
      this.passwordForm.markAllAsTouched();
      return;
    }

    this.passwordDataLoading.set(true);
    const data = this.passwordForm.value;

    const payload: PasswordChangeRequest = {
      confirmPassword: data.confirmPassword,
      newPassword: data.newPassword,
      currentPassword: data.currentPassword,
    }

    this.userApiService.changePassword(payload)
      .pipe(finalize(() => this.passwordDataLoading.set(false)))
      .subscribe({
        next: () => {
          this.snackBarService.success("Successfully updated password !");
        },
        error: (err: ProblemDetails) => {
          this.snackBarService.error(err.detail ?? "Failed to update password");
        }
      })
  }

  onUserInfoSubmit() {
    if (this.userInfoForm.invalid) {
      this.userInfoForm.markAllAsTouched();
      return;
    }

    this.userDataLoading.set(true);
    const data = this.userInfoForm.value;

    const payload: UserBasicInfoUpdateRequest = {
      email: data.email,
      firstName: data.firstName,
      phoneNumber: data.phoneNumber,
      lastName: data.lastName,
    }

    this.portalUserApiService.putUserBasicInfo(payload)
      .pipe(finalize(() => this.userDataLoading.set(false)))
      .subscribe({
        next: () => {
          this.snackBarService.success("Successfully updated user data.");
        },
        error: (err: ProblemDetails) => {
          this.snackBarService.error(err?.detail ?? "Failed to update user data.");
        }
      });
  }

  loadBasicInfo(): void {
    this.portalUserApiService.getUserBasicInfo()
      .pipe(finalize(() => this.userDataLoading.set(false)))
      .subscribe(response => {
        this.userInfoForm.patchValue(
          {
            firstName: response.firstName,
            lastName: response.lastName,
            email: response.email,
            phoneNumber: response.phoneNumber ?? null
          })
      });
  }

  getUserErrorMessage(path: string) {
    return getErrorMessage(this.userInfoForm, path);
  }

  getPasswordErroMessage(path: string) {
    return getErrorMessage(this.passwordForm, path);
  }
}
