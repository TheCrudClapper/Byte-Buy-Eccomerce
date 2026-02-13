import { Component, OnInit, signal } from '@angular/core';
import { ActivatedRoute, Router, RouterLink, RouterModule } from "@angular/router";
import { AuthService } from '../../../core/clients/auth/auth-service';

@Component({
  selector: 'app-navbar',
  imports: [RouterModule, RouterLink],
  templateUrl: './navbar.html',
  styleUrl: './navbar.scss',
  standalone: true
})
export class Navbar implements OnInit {
  constructor(private router: Router, protected route: ActivatedRoute, protected auth: AuthService) { }

  protected searchPhrase = signal<string | null>(null);

  ngOnInit(): void {
    this.route.queryParams.subscribe(params => {
      this.searchPhrase.set(params['searchPhrase'] ?? null);
    });
  }

  onSearch() {
    this.router.navigate(['/offers'], {
      queryParams: { searchPhrase: this.searchPhrase() },
      queryParamsHandling: 'merge'
    });
  }

  onLogout(): void {
    this.auth.logout();
    this.router.navigate(['login']);
  }
}

