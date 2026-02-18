import { Component, EventEmitter, Input, Output } from '@angular/core';
import { PaginationMetadata } from '../../../core/pagination/pagination-metadata';

@Component({
  selector: 'app-pagination',
  imports: [],
  standalone: true,
  templateUrl: './pagination.html',
  styleUrl: './pagination.scss',
})
export class Pagination {
  @Input() metadata!: PaginationMetadata
  @Output() goToPrevious = new EventEmitter();
  @Output() goToNext = new EventEmitter();

  emitPrevious(){
    this.goToPrevious.emit();
  }

  emitNext(){
    this.goToNext.emit();
  }
}
