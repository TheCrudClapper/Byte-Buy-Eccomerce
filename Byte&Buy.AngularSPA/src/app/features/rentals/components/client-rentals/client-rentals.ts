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
import { FormControl, FormGroup, ReactiveFormsModule, ɵInternalFormsSharedModule } from '@angular/forms';
import { Guid } from 'guid-typescript';

@Component({
  selector: 'app-client-rentals',
  imports: [DecimalPipe, DatePipe, EmptyState, Pagination, ɵInternalFormsSharedModule, ReactiveFormsModule],
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
  readonly rentalStatuses = Object.values(RentalStatus)
    .filter(v => typeof v === 'number');

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

  filterForm = new FormGroup({
    itemName: new FormControl<string | null>(null),
    rentalStatus: new FormControl<RentalStatus | null>(null),
    rentalStartDate: new FormControl<string | null>(null),
    rentalEndDate: new FormControl<string | null>(null),
  });

  constructor() {
    effect(() => {
      this.fetch(this.query());
    });
  }

  query = signal<UserRentalLenderQuery>({
    pageNumber: 1,
    pageSize: this.PAGE_SIZE,
  });

  ngOnInit(): void {
    this.route.queryParams.subscribe(params => {

      const newQuery: UserRentalLenderQuery = {
        pageNumber: Number(params['pageNumber'] ?? 1),
        pageSize: this.PAGE_SIZE
      };

      if (params['rentalStatus'] !== undefined)
        newQuery.rentalStatus = Number(params['rentalStatus']);

      if (params['itemName'])
        newQuery.itemName = params['itemName'];

      if (params['rentalStartDate'])
        newQuery.rentalStartDate = params['rentalStartDate'];

      if (params['rentalEndDate'])
        newQuery.rentalEndDate = params['rentalEndDate'];

      this.query.set(newQuery);

      this.filterForm.patchValue({
        itemName: newQuery.itemName ?? null,
        rentalStatus: newQuery.rentalStatus ?? null,
        rentalStartDate: this.toDateInputValue(newQuery.rentalStartDate ?? null),
        rentalEndDate: this.toDateInputValue(newQuery.rentalEndDate ?? null),
      }, { emitEvent: false });
    });
  }

  fetch(query: UserRentalLenderQuery) {
    this.rentalApiSerivce.getLenderRentals(query).subscribe({
      next: data => this.pagedList.set(data),
      error: err => this.toastService.error(
        err?.detail ?? "Failed to fetch your rented offers"
      )
    });
  }

  applyFilters() {
    const formValue = this.filterForm.value;

    const newQuery: UserRentalLenderQuery = {
      pageNumber: 1,
      pageSize: this.PAGE_SIZE,
    };

    if (formValue.itemName)
      newQuery.itemName = formValue.itemName;

    if (formValue.rentalStatus !== null)
      newQuery.rentalStatus = formValue.rentalStatus;

    if (formValue.rentalStartDate)
      newQuery.rentalStartDate = formValue.rentalStartDate

    if (formValue.rentalEndDate)
      newQuery.rentalEndDate = formValue.rentalEndDate

    this.query.set(newQuery);

    this.router.navigate([], {
      relativeTo: this.route,
      queryParams: newQuery,
    });
  }

  clearFilters() {
    this.filterForm.reset();

    this.router.navigate([], {
      relativeTo: this.route,
      queryParams: {
        pageNumber: 1,
        pageSize: this.PAGE_SIZE
      }
    });
  }

  private toDateInputValue(date: string | null): string | null {
    if (!date) return null;
    return date.split('T')[0];
  }

  goToPage(page: number) {
    const meta = this.pagedList()?.metadata;
    if (!meta) return;

    const safePage = Math.min(Math.max(page, 1), meta.totalPages);

    if (safePage === this.query().pageNumber) return;

    this.query.update(q => ({
      ...q,
      pageNumber: safePage
    }));
  }

  nextPage() {
    const meta = this.pagedList()?.metadata;
    if (!meta?.hasNext) return;
    this.goToPage(this.query().pageNumber + 1);
  }

  previousPage() {
    const meta = this.pagedList()?.metadata;
    if (!meta?.hasPrevious) return;
    this.goToPage(this.query().pageNumber - 1);
  }
}
