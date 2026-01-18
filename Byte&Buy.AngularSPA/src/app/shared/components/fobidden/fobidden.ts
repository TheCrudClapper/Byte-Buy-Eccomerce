import { Component, inject } from '@angular/core';
import { Router, RouterLink } from '@angular/router';

@Component({
  selector: 'app-fobidden',
  imports: [RouterLink],
  templateUrl: './fobidden.html',
  styleUrl: './fobidden.scss',
})
export class Fobidden {
 private readonly router = inject(Router);
}
