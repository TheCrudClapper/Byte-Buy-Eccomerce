import { Component, effect, inject, OnInit, signal } from '@angular/core';
import { RentalApiService } from '../../../../core/clients/rental/rental-api-service';
import { RentalLenderResponse } from '../../../../core/dto/rental/rental-lender-response';
import { environment } from '../../../../../environments/environment';
import { ToastService } from '../../../../shared/services/snackbar/toast-service';
import { DatePipe, DecimalPipe } from '@angular/common';
import { RentalStatus } from '../../../../core/dto/rental/enum/rental-status';
import { PagedList } from '../../../../core/pagination/pagedList';
import { UserRentalLenderQuery } from '../../../../core/dto/rental/common/user-rental-lender-query';
import { ActivatedRoute, Router } from '@angular/router';
import { EmptyState } from "../../../../shared/components/empty-state/empty-state";
import { EmptyStateModel } from '../../../../shared/models/empty-state-model';
import { Pagination } from "../../../../shared/components/pagination/pagination";

@Component({
  selector: 'app-client-rentals',
  imports: [DecimalPipe, DatePipe, EmptyState, Pagination],
  standalone: true,
  templateUrl: './client-rentals.html',
  styleUrl: './client-rentals.scss',
})

export class ClientRentals implements OnInit {
  private readonly PAGE_SIZE = 10;
  private readonly route = inject(ActivatedRoute);
  private readonly router = inject(Router);
  private readonly rentalApiSerivce = inject(RentalApiService);
  protected readonly imageBaseUrl = environment.staticImagesBaseUrl;
  private readonly toastService = inject(ToastService);

  pagedList = signal<PagedList<RentalLenderResponse> | undefined>(undefined);

  readonly RentalStatus = RentalStatus;

  readonly emptyStateModel: EmptyStateModel = {
    description: `None of your items have been rented so far.
       Once a client rents something, it will appear here.`,
    header: "No rentals yet",
    mainIconClass: "fa-solid fa-hand-holding-dollar",
    buttonArray: [
      {
        buttonIconClass: "fa-solid fa-plus me-1",
        buttonText: "Add new rent item",
        buttonLink: `/offers/rent/create`,
      }
    ]
  }

  readonly rentalStatuses = Object.values(RentalStatus)
    .filter(v => typeof v === 'number');

  query = signal<UserRentalLenderQuery>({
    pageNumber: 1,
    pageSize: this.PAGE_SIZE,
  });

  selectedRentalStatus = signal<RentalStatus | null>(null);
  itemName = signal<string | null>(null);
  rentalStartDate = signal<string | null>(null);
  rentalEndDate = signal<string | null>(null);

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

  ngOnInit(): void {
    this.route.queryParams.subscribe(params => {
      const newQuery: UserRentalLenderQuery = {
        pageNumber: Number(params['pageNumber'] ?? 1),
        pageSize: this.PAGE_SIZE
      };

      if (params['rentalStatus'] !== undefined) newQuery.rentalStatus = Number(params['rentalStatus']);
      if (params['itemName']) newQuery.itemName = params['itemName'];
      if (params['rentalStartDate']) newQuery.rentalStartDate = params['rentalStartDate'];
      if (params['rentalEndDate']) newQuery.rentalEndDate = params['rentalEndDate'];

      this.selectedRentalStatus.set(newQuery.rentalStatus ?? null);

      this.itemName.set(params['itemName'] ?? null);

      this.rentalStartDate.set(params['rentalStartDate']);

      this.rentalEndDate.set(params['rentalEndDate']);

      this.query.set(newQuery);
    });
  }

  applyFilters() {
    const newQuery: UserRentalLenderQuery = {
      pageNumber: 1,
      pageSize: this.query().pageSize,
    };

    if (this.selectedRentalStatus() !== null)
      newQuery.rentalStatus = this.selectedRentalStatus()!

    if (this.itemName() !== null)
      newQuery.itemName = this.itemName()!;

    if (this.rentalStartDate() !== null)
      newQuery.rentalStartDate = this.rentalStartDate()!;

    if (this.rentalEndDate() !== null)
      newQuery.rentalEndDate = this.rentalEndDate()!;

    this.query.set(newQuery);

    this.router.navigate([], {
      relativeTo: this.route,
      queryParams: newQuery,
      queryParamsHandling: 'merge'
    });
  }

  clearFilters() {
    this.selectedRentalStatus.set(null);
    this.rentalStartDate.set(null);
    this.rentalEndDate.set(null);
    this.itemName.set(null);

    this.router.navigate([], {
      relativeTo: this.route,
      queryParams: {
        pageNumber: 1,
        pageSize: this.PAGE_SIZE
      }
    });
  }

  onStartDateChange(value: string) {
    if (!value) {
      this.rentalStartDate.set(null);
      return;
    }

    const iso = new Date(value + 'T00:00:00Z').toISOString();
    this.rentalStartDate.set(iso);
  }

  onEndDateChange(value: string) {
    if (!value) {
      this.rentalEndDate.set(null);
      return;
    }

    const iso = new Date(value + 'T00:00:00Z').toISOString();
    this.rentalEndDate.set(iso);
  }

  toDateInputValue(date: string | null): string | null {
    if (!date) return null;
    return date.split('T')[0];
  }

  onRentalStatusChange(value: string) {
    if (value === 'null') {
      this.selectedRentalStatus.set(null);
    } else {
      this.selectedRentalStatus.set(Number(value) as RentalStatus);
    }
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
