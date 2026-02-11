import { Component, computed, inject, OnInit, signal } from '@angular/core';
import { OrderApiService } from '../../../../core/clients/orders/order-api-service';
import { ToastService } from '../../../../shared/services/snackbar/toast-service';
import { ActivatedRoute, Router, RouterLink } from '@angular/router';
import { environment } from '../../../../../environments/environment';
import { OrderStatus } from '../../../../core/dto/order/enum/order-status';
import { DeliveryChannel } from '../../../../core/dto/delivery/enum/delivery-channel';
import { Guid } from 'guid-typescript';
import { OrderDetailsResponse } from '../../../../core/dto/order/order-details-response';
import { ProblemDetails } from '../../../../core/dto/problem-details';
import { CommonModule } from '@angular/common';
import { DocumentsApiService } from '../../../../core/clients/documents/documents-api-service';

@Component({
  selector: 'app-buyer-order-details',
  imports: [CommonModule, RouterLink],
  standalone: true,
  templateUrl: './buyer-order-details.html',
  styleUrl: './buyer-order-details.scss',
})
export class BuyerOrderDetails implements OnInit {
  private readonly orderApiService = inject(OrderApiService);
  private readonly toastService = inject(ToastService);
  private readonly route = inject(ActivatedRoute);
  private readonly router = inject(Router);
  private readonly documentsApiService = inject(DocumentsApiService);
  readonly imageBaseUrl = environment.staticImagesBaseUrl;

  readonly OrderStatus = OrderStatus;
  readonly DeliveryChannel = DeliveryChannel;
  orderDetails = signal<OrderDetailsResponse | undefined>(undefined);

  canDownloadPdf = computed(() => {
    const order = this.orderDetails();
    if (!order) return false;

    return order.status === OrderStatus.Delivered ||
      order.status === OrderStatus.Returned;
  });

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
      error: (err) => {
        this.router.navigate(['/not-found']);
      }
    })
  }

  downloadPdf() {
    if (!this.orderDetails() || !this.orderDetails()?.id)
      return;

    const orderId = this.orderDetails()!.id;
    this.documentsApiService.downloadOrderDetails(orderId).subscribe(blob => {
      const url = window.URL.createObjectURL(blob);
      const a = document.createElement('a');
      a.href = url;
      a.download = `order-details-${orderId}.pdf`;
      a.click();

      window.URL.revokeObjectURL(url);
    });
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

  actionButtonsVisible(): boolean {
    const status = this.orderDetails()?.status;
    return status === OrderStatus.AwaitingPayment
      || status === OrderStatus.Delivered;
  }
}
