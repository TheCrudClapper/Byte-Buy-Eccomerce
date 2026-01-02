import { Component } from '@angular/core';
import { PersonalInfo } from "../personal-info/personal-info/personal-info";

@Component({
  selector: 'app-profile',
  imports: [PersonalInfo],
  templateUrl: './profile-index.html',
  styleUrl: './profile-index.scss',
})
export class ProfileIndex {

}
