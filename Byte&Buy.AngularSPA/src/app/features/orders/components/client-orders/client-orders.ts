import { Component, effect, inject, OnInit, signal } from '@angular/core';
import { environment } from '../../../../../environments/environment';
import { OrderApiService } from '../../../../core/clients/orders/order-api-service';
import { UserOrderListResponse } from '../../../../core/dto/order/common/user-order-list-response';
import { OrderStatus } from '../../../../core/dto/order/enum/order-status';
import { ProblemDetails } from '../../../../core/dto/problem-details';
import { ToastService } from '../../../../shared/services/snackbar/toast-service';
import { DatePipe, DecimalPipe } from '@angular/common';
import { ActivatedRoute, Router, RouterLink } from '@angular/router';
import { UserOrderSellerListQuery } from '../../../../core/dto/order/common/user-order-seller-list-query';
import { PagedList } from '../../../../core/pagination/pagedList';
import { EmptyStateModel } from '../../../../shared/models/empty-state-model';
import { EmptyState } from "../../../../shared/components/empty-state/empty-state";
import { Pagination } from "../../../../shared/components/pagination/pagination";
import { FormControl, FormGroup, ReactiveFormsModule } from '@angular/forms';

@Component({
  selector: 'app-client-orders',
  imports: [DecimalPipe, DatePipe, RouterLink, EmptyState, Pagination, ReactiveFormsModule],
  standalone: true,
  templateUrl: './client-orders.html',
  styleUrl: './client-orders.scss',
})
export class ClientOrders implements OnInit {
  private readonly PAGE_SIZE = 10;
  private readonly route = inject(ActivatedRoute);
  private readonly router = inject(Router);
  private readonly orderApiService = inject(OrderApiService);
  protected readonly imageBaseUrl = environment.staticImagesBaseUrl;
  private readonly toastService = inject(ToastService);

  readonly emptyStateModel: EmptyStateModel = {
    description: ` No one has purchased any of your offers so far.
      When a client places an order, it will appear here.`,
    header: "No orders yet",
    mainIconClass: "fa-solid fa-receipt",
    buttonArray: [
      {
        buttonIconClass: "fa-solid fa-box-open me-1",
        buttonText: " View my offers",
        buttonLink: `/profile/my-offers`,
      }
    ]
  }

  pagedList = signal<PagedList<UserOrderListResponse> | undefined>(undefined);

  //declaring it so its visible in template
  readonly OrderStatus = OrderStatus;
  readonly orderStatuses = Object.values(OrderStatus)
    .filter(v => typeof v === 'number');

  query = signal<UserOrderSellerListQuery>({
    pageNumber: 1,
    pageSize: this.PAGE_SIZE,
  });

  filterFrom = new FormGroup({
    orderStatus: new FormControl<OrderStatus | null>(null),
    buyerFullName: new FormControl<string | null>(null),
    itemName: new FormControl<string | null>(null),
    purchasedFrom: new FormControl<string | null>(null),
    purchasedTo: new FormControl<string | null>(null)
  });

  constructor() {
    effect(() => {
      this.fetch(this.query());
    });
  }

  fetch(query: UserOrderSellerListQuery) {
    this.orderApiService.getSellerOrders(query).subscribe({
      next: data => this.pagedList.set(data),
      error: (err: ProblemDetails) => this.toastService.error(err?.detail ?? "Failed to fetch your orders")
    })
  }

  ngOnInit(): void {
    this.route.queryParams.subscribe(params => {
      const newQuery: UserOrderSellerListQuery = {
        pageNumber: Number(params['pageNumber'] ?? 1),
        pageSize: this.PAGE_SIZE
      };

      if (params['itemName']) newQuery.itemName = params['itemName'];
      if (params['buyerFullName']) newQuery.buyerFullName = params['buyerFullName'];
      if (params['purchasedFrom']) newQuery.purchasedFrom = params['purchasedFrom'];
      if (params['purchasedTo']) newQuery.purchasedTo = params['purchasedTo'];
      if (params['status'] !== undefined) newQuery.status = Number(params['status']);

      this.query.set(newQuery);

      this.filterFrom.patchValue({
        itemName: newQuery.itemName ?? null,
        buyerFullName: newQuery.buyerFullName ?? null,
        orderStatus: newQuery.status ?? null,
        purchasedFrom: this.toDateInputValue(newQuery.purchasedFrom ?? null),
        purchasedTo: this.toDateInputValue(newQuery.purchasedTo ?? null),
      })
    });
  }

  applyFilters() {
    const formValue = this.filterFrom.value;

    const newQuery: UserOrderSellerListQuery = {
      pageNumber: 1,
      pageSize: this.query().pageSize,
    };

    if (formValue.itemName)
      newQuery.itemName = formValue.itemName;

    if (formValue.buyerFullName)
      newQuery.buyerFullName = formValue.buyerFullName;

    if (formValue.orderStatus !== null)
      newQuery.status = formValue.orderStatus;

    if (formValue.purchasedFrom)
      newQuery.purchasedFrom = formValue.purchasedFrom

    if (formValue.purchasedTo)
      newQuery.purchasedTo = formValue.purchasedTo

    this.query.set(newQuery);

    this.router.navigate([], {
      relativeTo: this.route,
      queryParams: newQuery,
    });
  }

  clearFilters() {
    this.filterFrom.reset();

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
