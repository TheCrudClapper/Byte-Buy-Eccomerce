import { Component, inject, OnInit, signal } from '@angular/core';
import { UserOrderListResponse } from '../../../../../core/dto/order/common/user-order-list-response';
import { OrderApiService } from '../../../../../core/clients/orders/order-api-service';
import { ProblemDetails } from '../../../../../core/dto/problem-details';
import { ToastService } from '../../../../../shared/services/snackbar/toast-service';
import { environment } from '../../../../../../environments/environment';
import { DatePipe, DecimalPipe } from '@angular/common';
import { OrderStatus } from '../../../../../core/dto/order/enum/order-status';
import { RouterLink } from '@angular/router';

@Component({
  selector: 'app-my-orders',
  imports: [DecimalPipe, DatePipe, RouterLink],
  templateUrl: './my-orders.html',
  styleUrl: './my-orders.scss',
})
export class MyOrders implements OnInit {
  private readonly orderApiService = inject(OrderApiService);
  protected readonly imageBaseUrl = environment.staticImagesBaseUrl;
  private readonly toastService = inject(ToastService);

  ordersList = signal<UserOrderListResponse[] | undefined>(undefined);

  //declaring it so its visible in template
  readonly OrderStatus = OrderStatus;

  ngOnInit(): void {
    this.orderApiService.getUserOrders().subscribe({
      next: data => this.ordersList.set(data),
      error: (err: ProblemDetails) => this.toastService.error(err?.detail ?? "Failed to fetch user's offers")
    })
  }

}
