import { Component, inject } from '@angular/core';
import { RouterOutlet, RouterLinkActive, RouterLinkWithHref, Router } from '@angular/router';
import { AuthService } from '../../../../core/clients/auth/auth-service';
@Component({
  selector: 'app-profile',
  imports: [RouterOutlet, RouterLinkActive, RouterLinkWithHref],
  templateUrl: './profile-page.html',
  styleUrl: './profile-page.scss',
  standalone: true
})
export class ProfilePage {
  private readonly auth: AuthService = inject(AuthService);
  private readonly router: Router = inject(Router);
  
  onLogout(): void{
    this.auth.logout();
    this.router.navigate(['login']);
  }
  
}
