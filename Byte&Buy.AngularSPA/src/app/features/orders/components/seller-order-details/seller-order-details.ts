import { Component, inject, OnInit, signal } from '@angular/core';
import { ActivatedRoute, Router, RouterLink } from '@angular/router';
import { Guid } from 'guid-typescript';
import { environment } from '../../../../../environments/environment';
import { OrderApiService } from '../../../../core/clients/orders/order-api-service';
import { DeliveryChannel } from '../../../../core/dto/delivery/enum/delivery-channel';
import { OrderStatus } from '../../../../core/dto/order/enum/order-status';
import { OrderDetailsResponse } from '../../../../core/dto/order/order-details-response';
import { ProblemDetails } from '../../../../core/dto/problem-details';
import { ToastService } from '../../../../shared/services/snackbar/toast-service';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'app-seller-order-details',
  standalone: true,
  imports: [CommonModule],
  templateUrl: './seller-order-details.html',
  styleUrl: './seller-order-details.scss',
})
export class SellerOrderDetails implements OnInit {
  private readonly orderApiService = inject(OrderApiService);
  private readonly toastService = inject(ToastService);
  private readonly route = inject(ActivatedRoute);
  private readonly router = inject(Router);
  readonly imageBaseUrl = environment.staticImagesBaseUrl;

  readonly OrderStatus = OrderStatus;
  readonly DeliveryChannel = DeliveryChannel;
  orderDetails = signal<OrderDetailsResponse | undefined>(undefined);

  ngOnInit(): void {
    const id = this.route.snapshot.paramMap.get('id');
    if (!id) return;

    const guid = Guid.parse(id);
    this.loadOrderDetails(guid);
  }

  loadOrderDetails(id: Guid) {
    this.orderApiService.getOrderDetails(id).subscribe({
      next: (data: OrderDetailsResponse) => {
        this.orderDetails.set(data);
      },
      error: () => {
        this.router.navigate(['/not-found']);
      }
    })
  }

  shipOrder() {
    if (!this.orderDetails() || !this.orderDetails()?.id)
      return;

    const orderId = this.orderDetails()!.id;
    this.orderApiService.shipOrder(orderId).subscribe({
      next: () => {
        this.toastService.success("Successfully shipped order.");
        this.orderDetails.update(o => {
          if (!o) return;
          return { ...o, status: OrderStatus.Shipped }
        })
      },
      error: (err) => {
        this.toastService.error(err.detail ?? "Failed to return your order");
      }
    })
  }

  deliverOrder() {
    if (!this.orderDetails() || !this.orderDetails()?.id)
      return;

    const orderId = this.orderDetails()!.id;
    this.orderApiService.deliverOrder(orderId).subscribe({
      next: () => {
        this.toastService.success("Successfully delivered order to user.");
        this.orderDetails.update(o => {
          if (!o) return;
          return { ...o, status: OrderStatus.Delivered }
        })
      },
      error: (err) => {
        this.toastService.error(err.detail ?? "Failed to deliver your order");
      }
    })
  }

  actionButtonsVisible(): boolean {
    const status = this.orderDetails()?.status;
    return status === OrderStatus.Paid
      || status === OrderStatus.Shipped;
  }
}
