import { Component } from '@angular/core';
import { RouterOutlet, RouterLinkActive, RouterLinkWithHref } from '@angular/router';

@Component({
  selector: 'app-profile',
  imports: [RouterOutlet, RouterLinkActive, RouterLinkWithHref],
  templateUrl: './profile-index.html',
  styleUrl: './profile-index.scss',
})
export class ProfileIndex {

}
