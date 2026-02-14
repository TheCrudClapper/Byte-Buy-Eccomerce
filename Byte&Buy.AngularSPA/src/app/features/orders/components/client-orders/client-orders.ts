import { Component, effect, inject, OnInit, signal } from '@angular/core';
import { environment } from '../../../../../environments/environment';
import { OrderApiService } from '../../../../core/clients/orders/order-api-service';
import { UserOrderListResponse } from '../../../../core/dto/order/common/user-order-list-response';
import { OrderStatus } from '../../../../core/dto/order/enum/order-status';
import { ProblemDetails } from '../../../../core/dto/problem-details';
import { ToastService } from '../../../../shared/services/snackbar/toast-service';
import { DatePipe, DecimalPipe } from '@angular/common';
import { RouterLink } from '@angular/router';
import { UserOrderSellerListQuery } from '../../../../core/dto/order/common/user-order-seller-list-query';
import { PagedList } from '../../../../core/pagination/pagedList';

@Component({
  selector: 'app-client-orders',
  imports: [DecimalPipe, DatePipe, RouterLink],
  standalone: true,
  templateUrl: './client-orders.html',
  styleUrl: './client-orders.scss',
})
export class ClientOrders {
  private readonly PAGE_SIZE = 10;
  private readonly orderApiService = inject(OrderApiService);
  protected readonly imageBaseUrl = environment.staticImagesBaseUrl;
  private readonly toastService = inject(ToastService);

  pagedList = signal<PagedList<UserOrderListResponse> | undefined>(undefined);

  //declaring it so its visible in template
  readonly OrderStatus = OrderStatus;

  readonly orderStatuses = Object.values(OrderStatus)
    .filter(v => typeof v === 'number');

  query = signal<UserOrderSellerListQuery>({
    pageNumber: 1,
    pageSize: this.PAGE_SIZE,
  });

  constructor() {
    effect(() => {
      this.fetch(this.query());
    });
  }

  fetch(query: UserOrderSellerListQuery) {
    this.orderApiService.getSellerOrders(query).subscribe({
      next: data => this.pagedList.set(data),
      error: (err: ProblemDetails) => this.toastService.error(err?.detail ?? "Failed to fetch your sold orders")
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
