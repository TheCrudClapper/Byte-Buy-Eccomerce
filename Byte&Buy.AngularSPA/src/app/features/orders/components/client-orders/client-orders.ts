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

@Component({
  selector: 'app-client-orders',
  imports: [DecimalPipe, DatePipe, RouterLink, EmptyState],
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

  selectedOrderStatus = signal<OrderStatus | null>(null);
  buyerFullName = signal<string | null>(null);
  itemName = signal<string | null>(null);
  purchasedFrom = signal<string | null>(null);
  purchasedTo = signal<string | null>(null);

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

      this.selectedOrderStatus.set(newQuery.status ?? null);

      this.query.set(newQuery);
    });
  }

  applyFilters() {
    const newQuery: UserOrderSellerListQuery = {
      pageNumber: 1,
      pageSize: this.query().pageSize,
    };

    if (this.buyerFullName() !== null)
      newQuery.buyerFullName = this.buyerFullName()!;

    if (this.selectedOrderStatus() !== null)
      newQuery.status = this.selectedOrderStatus()!;

    if (this.itemName() !== null)
      newQuery.itemName = this.itemName()!;

    if (this.purchasedFrom() !== null)
      newQuery.purchasedFrom = this.purchasedFrom()!;

    if (this.purchasedTo() !== null)
      newQuery.purchasedTo = this.purchasedTo()!;

    this.query.set(newQuery);

    this.router.navigate([], {
      relativeTo: this.route,
      queryParams: newQuery,
      queryParamsHandling: 'merge'
    });
  }

  clearFilters() {
    this.selectedOrderStatus.set(null);
    this.purchasedFrom.set(null);
    this.purchasedTo.set(null);
    this.itemName.set(null);
    this.buyerFullName.set(null);

    this.router.navigate([], {
      relativeTo: this.route,
      queryParams: {
        pageNumber: 1,
        pageSize: this.PAGE_SIZE
      }
    });
  }

  onOrderStatusChange(value: string) {
    if (value === 'null') {
      this.selectedOrderStatus.set(null);
    } else {
      this.selectedOrderStatus.set(Number(value) as OrderStatus);
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
