import { Component, effect, inject, OnInit, signal } from '@angular/core';
import { UserOrderListResponse } from '../../../../../core/dto/order/common/user-order-list-response';
import { OrderApiService } from '../../../../../core/clients/orders/order-api-service';
import { ProblemDetails } from '../../../../../core/dto/problem-details';
import { ToastService } from '../../../../../shared/services/snackbar/toast-service';
import { environment } from '../../../../../../environments/environment';
import { DatePipe, DecimalPipe } from '@angular/common';
import { OrderStatus } from '../../../../../core/dto/order/enum/order-status';
import { RouterLink } from '@angular/router';
import { UserOrderListQuery } from '../../../../../core/dto/order/common/user-order-list-query';
import { PagedList } from '../../../../../core/pagination/pagedList';
import { EmptyStateModel } from '../../../../../shared/models/empty-state-model';
import { EmptyState } from "../../../../../shared/components/empty-state/empty-state";

@Component({
  selector: 'app-my-orders',
  imports: [DecimalPipe, DatePipe, RouterLink, EmptyState],
  templateUrl: './my-orders.html',
  styleUrl: './my-orders.scss',
  standalone: true,
})

export class MyOrders {
  private readonly PAGE_SIZE = 10;
  private readonly orderApiService = inject(OrderApiService);
  protected readonly imageBaseUrl = environment.staticImagesBaseUrl;
  private readonly toastService = inject(ToastService);

  readonly emptyStateModel: EmptyStateModel = {
    description: ` You haven't purchased anything yet,
          When you place an order, it will appear here.`,
    header: "No orders placed yet",
    mainIconClass: "fa-solid fa-truck",
    buttonArray: [
      {
        buttonIconClass: "fa-solid fa-fire me-1",
        buttonLink: `/offers`,
        buttonText: "Find something for yourself",
      }
    ]
  }

  pagedList = signal<PagedList<UserOrderListResponse> | undefined>(undefined);

  query = signal<UserOrderListQuery>({
    pageNumber: 1,
    pageSize: this.PAGE_SIZE,
  });

  constructor() {
    effect(() => {
      this.fetch(this.query());
    })
  }

  //declaring it so its visible in template
  readonly OrderStatus = OrderStatus;

  fetch(query: UserOrderListQuery) {
    this.orderApiService.getUserOrders(query).subscribe({
      next: data => this.pagedList.set(data),
      error: (err: ProblemDetails) => this.toastService.error(err?.detail ?? "Failed to fetch user's orders")
    })
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
