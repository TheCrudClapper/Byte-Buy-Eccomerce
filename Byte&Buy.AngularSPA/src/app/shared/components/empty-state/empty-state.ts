import { Component, Input, ChangeDetectionStrategy } from '@angular/core';
import { EmptyStateModel } from '../../models/empty-state-model';
import { RouterLink } from "@angular/router";

@Component({
  selector: 'app-empty-state',
  imports: [RouterLink],
  standalone: true,
  templateUrl: './empty-state.html',
  changeDetection: ChangeDetectionStrategy.Eager,
  styleUrl: './empty-state.scss',
})
export class EmptyState {
  @Input() model!: EmptyStateModel;
}
