import { Component } from '@angular/core';
import { Router, RouterLink, RouterModule } from "@angular/router";

@Component({
  selector: 'app-navbar',
  imports: [RouterModule, RouterLink],
  templateUrl: './navbar.html',
  styleUrl: './navbar.scss',
  standalone: true
})
export class Navbar {
  constructor(private router: Router) {}

  onSearch(phrase: string) {
  console.log(52);
  this.router.navigate(['/offers'], {
    queryParams: { phrase }
  });
}
}

