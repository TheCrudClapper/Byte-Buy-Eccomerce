import { Component, effect, inject, signal } from '@angular/core';
import { RentalApiService } from '../../../../core/clients/rental/rental-api-service';
import { environment } from '../../../../../environments/environment';
import { ToastService } from '../../../../shared/services/snackbar/toast-service';
import { UserRentalBorrowerResponse } from '../../../../core/dto/rental/user-rental-borrower-response';
import { RentalStatus } from '../../../../core/dto/rental/enum/rental-status';
import { DatePipe, DecimalPipe } from '@angular/common';
import { Guid } from 'guid-typescript';
import { UserRentalBorrowerQuery } from '../../../../core/dto/rental/common/rental-list-query';
import { PagedList } from '../../../../core/pagination/pagedList';

@Component({
  selector: 'app-my-rentals',
  imports: [DecimalPipe, DatePipe],
  standalone: true,
  templateUrl: './my-rentals.html',
  styleUrl: './my-rentals.scss',
})
export class MyRentals {
  private readonly PAGE_SIZE = 10;
  private readonly rentalApiSerivce = inject(RentalApiService);
  protected readonly imageBaseUrl = environment.staticImagesBaseUrl;
  private readonly toastService = inject(ToastService);

  pagedList = signal<PagedList<UserRentalBorrowerResponse> | undefined>(undefined);
  readonly RentalStatus = RentalStatus;

  query = signal<UserRentalBorrowerQuery>({
    pageNumber: 1,
    pageSize: this.PAGE_SIZE,
  });

  constructor() {
    effect(() => {
      this.fetch(this.query());
    });
  }

  fetch(query: UserRentalBorrowerQuery) {
    this.rentalApiSerivce.getBorrowerRentals(query).subscribe({
      next: data => {
        this.pagedList.set(data);
      },
      error: (err) => this.toastService.error(err?.detail ?? "Failed to fetch your rentals")
    });
  }

  goToPage(page: number) {
    const meta = this.pagedList()?.metadata;
    if (!meta) return;

    const safePage = Math.min(
      Math.max(page, 1),
      meta.totalPages
    );

    if (safePage === this.query().pageNumber) return;

    this.query.update(q => ({
      ...q,
      pageNumber: safePage
    }));
  }

  nextPage() {
    const meta = this.pagedList()?.metadata;
    if (!meta || !meta.hasNext) return;

    const current = this.query();
    this.goToPage(current.pageNumber + 1);
  }

  previousPage() {
    const meta = this.pagedList()?.metadata;
    if (!meta || !meta.hasPrevious) return;

    const current = this.query();
    this.goToPage(current.pageNumber - 1);
  }

  returnRental(id: Guid) {
    this.rentalApiSerivce.returnRental(id).subscribe({
      next: () => {
        this.pagedList.update(list => {
          if (!list) return list;

          return {
            ...list,
            items: list.items.map(rental => rental.id === id 
                ? { ...rental, status: RentalStatus.Completed } 
                : rental)
          };
        });

        this.toastService.success("Successfully returned item")
      },
      error: (err) => this.toastService.error(err?.detail ?? "Failed to return item back to lender")
    });
  }
}
