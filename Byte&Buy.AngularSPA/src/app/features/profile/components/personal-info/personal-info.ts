import { Component, inject } from '@angular/core';
import { UsersApiService } from '../../services/users-api-service';
import { FormControl, FormGroup, Validators } from '@angular/forms';

@Component({
  selector: 'app-personal-info',
  imports: [],
  templateUrl: './personal-info.html',
  styleUrl: './personal-info.scss',
})
export class PersonalInfo {
  private readonly userApiService: UsersApiService = inject(UsersApiService);

  passwordForm: FormGroup = new FormGroup({
    currentPassword: new FormControl<string>("", [Validators.required, Validators.minLength(8)]),
    newPassword: new FormControl<string>("", [Validators.required, Validators.minLength(8)]),
    confirmPassword: new FormControl<string>("", [Validators.required, Validators.minLength(8)]),
  });
}
