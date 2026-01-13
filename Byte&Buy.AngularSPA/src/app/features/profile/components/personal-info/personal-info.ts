import { Component, inject, OnInit } from '@angular/core';
import { UsersApiService } from '../../services/users-api-service';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { UserBasicInfoResponse } from '../../api-dto/user-basic-info-response';
import { PortalUserApiService } from '../../services/portal user/portal-user-api-service';

@Component({
  selector: 'app-personal-info',
  imports: [],
  templateUrl: './personal-info.html',
  styleUrl: './personal-info.scss',
})
export class PersonalInfo implements OnInit{
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
    throw new Error('Method not implemented.');
  }

  onPasswordSubmit(){
    if(this.passwordForm.invalid){
      this.passwordForm.markAllAsTouched();
      return;
    }
  }
  
  onUserInfoSubmit(){
    if(this.userInfoForm.invalid){
      this.userInfoForm.markAllAsTouched();
      return;
    }

    const data = this.userInfoForm.value;

    const payload: UserBasicInfoResponse = {
      email: data.email,
      firstName: data.firstName,
      phoneNumber: data.phoneNumber,
      lastName: data.lastName,
    }
  }
}
