import { Component, inject, ChangeDetectionStrategy } from '@angular/core';
import { Router, RouterLink } from '@angular/router';

@Component({
  selector: 'app-fobidden',
  imports: [RouterLink],
  templateUrl: './fobidden.html',
  changeDetection: ChangeDetectionStrategy.Eager,
  styleUrl: './fobidden.scss',
})
export class Fobidden {
 private readonly router = inject(Router);
}
