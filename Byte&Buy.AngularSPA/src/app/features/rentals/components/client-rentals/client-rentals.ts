import { Component, effect, inject, OnInit, signal } from '@angular/core';
import { RentalApiService } from '../../../../core/clients/rental/rental-api-service';
import { RentalLenderResponse } from '../../../../core/dto/rental/rental-lender-response';
import { environment } from '../../../../../environments/environment';
import { ToastService } from '../../../../shared/services/snackbar/toast-service';
import { DatePipe, DecimalPipe } from '@angular/common';
import { RentalStatus } from '../../../../core/dto/rental/enum/rental-status';
import { PagedList } from '../../../../core/pagination/pagedList';
import { UserRentalLenderQuery } from '../../../../core/dto/rental/common/user-rental-lender-query';

@Component({
  selector: 'app-client-rentals',
  imports: [DecimalPipe, DatePipe],
  standalone: true,
  templateUrl: './client-rentals.html',
  styleUrl: './client-rentals.scss',
})

export class ClientRentals {
  private readonly PAGE_SIZE = 10;
  private readonly rentalApiSerivce = inject(RentalApiService);
  protected readonly imageBaseUrl = environment.staticImagesBaseUrl;
  private readonly toastService = inject(ToastService);

  pagedList = signal<PagedList<RentalLenderResponse> | undefined>(undefined);
  readonly RentalStatus = RentalStatus;

  query = signal<UserRentalLenderQuery>({
    pageNumber: 1,
    pageSize: this.PAGE_SIZE,
  });

  constructor() {
    effect(() => {
      this.fetch(this.query());
    });
  }

  fetch(query: UserRentalLenderQuery) {
    this.rentalApiSerivce.getLenderRentals(query).subscribe({
      next: data => {
        this.pagedList.set(data)
      },
      error: (err) => this.toastService.error(err?.detail ?? "Failed to fetch your rented offers")
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




}
