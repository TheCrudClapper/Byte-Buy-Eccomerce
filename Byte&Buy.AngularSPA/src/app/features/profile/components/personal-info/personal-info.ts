import { Component, inject, OnInit } from '@angular/core';
import { UsersApiService } from '../../services/users-api-service';
import { FormControl, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';
import { UserBasicInfoResponse } from '../../api-dto/user-basic-info-response';
import { PortalUserApiService } from '../../services/portal user/portal-user-api-service';
import { UserBasicInfoUpdateRequest } from '../../api-dto/user-basic-info-update-request';
import { getErrorMessage } from '../../../../core/helpers/form-helper';

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
    
  }

  onPasswordSubmit() {
    if (this.passwordForm.invalid) {
      this.passwordForm.markAllAsTouched();
      return;
    }
  }

  onUserInfoSubmit() {
    if (this.userInfoForm.invalid) {
      this.userInfoForm.markAllAsTouched();
      return;
    }

    const data = this.userInfoForm.value;

    const payload: UserBasicInfoUpdateRequest = {
      email: data.email,
      firstName: data.firstName,
      phoneNumber: data.phoneNumber,
      lastName: data.lastName,
    }

    this.portalUserApiService.putUserBasicInfo(payload).subscribe({
      next: (response) => {
        console.log(response.dateUpdated);
      },
      error: () => {
        console.log("gowno");
      }
    });
  }

  getErrorMessage(path: string) {
    return getErrorMessage(this.userInfoForm, path);
  }
}
