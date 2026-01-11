import { Component } from '@angular/core';
import { Router, RouterLink, RouterModule } from "@angular/router";
import { AuthService } from '../../core/services/auth-service';

@Component({
  selector: 'app-navbar',
  imports: [RouterModule, RouterLink],
  templateUrl: './navbar.html',
  styleUrl: './navbar.scss',
  standalone: true
})
export class Navbar {
  constructor(private router: Router, protected auth: AuthService) {}

    onSearch(phrase: string) {
    this.router.navigate(['/offers'], {
      queryParams: { phrase }
    });
  }

  onLogout(): void{
    this.auth.logout();
    this.router.navigate(['login']);
  }

}

