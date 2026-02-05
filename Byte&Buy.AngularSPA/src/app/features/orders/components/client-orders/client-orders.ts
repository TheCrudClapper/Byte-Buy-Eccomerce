import { Component, inject, OnInit, signal } from '@angular/core';
import { environment } from '../../../../../environments/environment';
import { OrderApiService } from '../../../../core/clients/orders/order-api-service';
import { UserOrderListResponse } from '../../../../core/dto/order/common/user-order-list-response';
import { OrderStatus } from '../../../../core/dto/order/enum/order-status';
import { ProblemDetails } from '../../../../core/dto/problem-details';
import { ToastService } from '../../../../shared/services/snackbar/toast-service';
import { DatePipe, DecimalPipe } from '@angular/common';
import { RouterLink } from '@angular/router';

@Component({
  selector: 'app-client-orders',
  imports: [DecimalPipe, DatePipe, RouterLink],
  standalone: true,
  templateUrl: './client-orders.html',
  styleUrl: './client-orders.scss',
})
export class ClientOrders implements OnInit {
  private readonly orderApiService = inject(OrderApiService);
  protected readonly imageBaseUrl = environment.staticImagesBaseUrl;
  private readonly toastService = inject(ToastService);

  ordersList = signal<UserOrderListResponse[] | undefined>(undefined);

  //declaring it so its visible in template
  readonly OrderStatus = OrderStatus;

  ngOnInit(): void {
    this.orderApiService.getSellerOrders().subscribe({
      next: data => this.ordersList.set(data),
      error: (err: ProblemDetails) => this.toastService.error(err?.detail ?? "Failed to fetch you sold orders")
    })
  }

}
