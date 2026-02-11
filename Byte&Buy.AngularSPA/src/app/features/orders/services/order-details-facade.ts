import { computed, inject, Injectable, signal } from '@angular/core';
import { OrderApiService } from '../../../core/clients/orders/order-api-service';
import { ToastService } from '../../../shared/services/snackbar/toast-service';
import { ActivatedRoute, Router } from '@angular/router';
import { DocumentsApiService } from '../../../core/clients/documents/documents-api-service';
import { OrderStatus } from '../../../core/dto/order/enum/order-status';
import { DeliveryChannel } from '../../../core/dto/delivery/enum/delivery-channel';
import { OrderDetailsResponse } from '../../../core/dto/order/order-details-response';
import { Guid } from 'guid-typescript';
import { ProblemDetails } from '../../../core/dto/problem-details';

@Injectable({
  providedIn: 'root',
})
export class OrderDetialsFacade {
  private readonly orderApiService = inject(OrderApiService);
  private readonly toastService = inject(ToastService);
  private readonly router = inject(Router);
  private readonly documentsApiService = inject(DocumentsApiService);

  readonly OrderStatus = OrderStatus;
  readonly DeliveryChannel = DeliveryChannel;
  orderDetails = signal<OrderDetailsResponse | undefined>(undefined);

  canDownloadPdf = computed(() => {
    const order = this.orderDetails();
    if (!order) return false;

    return order.status === OrderStatus.Delivered ||
      order.status === OrderStatus.Returned;
  });

  init(id: Guid): void {
    this.loadOrderDetails(id);
  }

  downloadPdf() {
    if (!this.orderDetails() || !this.orderDetails()?.id)
      return;

    const orderId = this.orderDetails()!.id;
    this.documentsApiService.downloadOrderDetails(orderId).subscribe(
      {
        next: (blob) => {
          const url = window.URL.createObjectURL(blob);
          const a = document.createElement('a');
          a.href = url;
          a.download = `order-details-${orderId}.pdf`;
          a.click();

          window.URL.revokeObjectURL(url);
        },
        error: (err: ProblemDetails) => this.toastService.error(err.detail ?? "Failed to generate pdf")
      });

  }

  loadOrderDetails(id: Guid) {
    this.orderApiService.getOrderDetails(id).subscribe({
      next: (data: OrderDetailsResponse) => {
        this.orderDetails.set(data);
      },
      error: (err) => {
        this.router.navigate(['/not-found']);
      }
    })
  }

  cancelOrder() {
    if (!this.orderDetails() || !this.orderDetails()?.id)
      return;

    const orderId = this.orderDetails()!.id;
    this.orderApiService.cancelOrder(orderId).subscribe({
      next: () => {
        this.toastService.success("Successfully canceled order.");
        this.orderDetails.update(o => {
          if (!o) return;
          return { ...o, status: OrderStatus.Canceled }
        })
      },
      error: (err: ProblemDetails) => {
        this.toastService.error(err.detail ?? "Failed to cancel your order");
      }
    })
  }

  returnOrder() {
    if (!this.orderDetails() || !this.orderDetails()?.id)
      return;

    const orderId = this.orderDetails()!.id;
    this.orderApiService.returnOrder(orderId).subscribe({
      next: () => {
        this.toastService.success("Successfully returned order.");
        this.orderDetails.update(o => {
          if (!o) return;
          return { ...o, status: OrderStatus.Returned }
        })
      },
      error: (err: ProblemDetails) => {
        this.toastService.error(err.detail ?? "Failed to return your order");
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


}
