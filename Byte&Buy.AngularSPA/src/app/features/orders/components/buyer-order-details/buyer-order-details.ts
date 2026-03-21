import { Component, computed, inject, OnInit, signal } from '@angular/core';
import { OrderApiService } from '../../../../core/clients/orders/order-api-service';
import { ActivatedRoute, Router, RouterLink } from '@angular/router';
import { environment } from '../../../../../environments/environment';
import { OrderStatus } from '../../../../core/dto/order/enum/order-status';
import { DeliveryChannel } from '../../../../core/dto/delivery/enum/delivery-channel';
import { Guid } from 'guid-typescript';
import { OrderDetailsResponse } from '../../../../core/dto/order/order-details-response';
import { ProblemDetails } from '../../../../core/dto/problem-details';
import { CommonModule } from '@angular/common';
import { DocumentsApiService } from '../../../../core/clients/documents/documents-api-service';
import { DialogService } from '../../../../shared/services/dialog-service/dialog-service';
import { OrderStatusComponent } from '../../../../shared/components/order-status-component/order-status-component';

@Component({
  selector: 'app-buyer-order-details',
  imports: [CommonModule, RouterLink, OrderStatusComponent],
  standalone: true,
  templateUrl: './buyer-order-details.html',
  styleUrl: './buyer-order-details.scss',
})
export class BuyerOrderDetails implements OnInit {
  private readonly orderApiService = inject(OrderApiService);
  private readonly dialogService = inject(DialogService);
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

    this.dialogService.confirm({ title: "Are you sure you want to cancel order ?" })
      .then(result => {
        if (result.isConfirmed) {
          this.orderApiService.cancelOrder(orderId).subscribe({
            next: () => {
              this.orderDetails.update(o => {
                if (!o) return;
                return { ...o, status: OrderStatus.Canceled }
              })
              this.dialogService.success("Successfully canceled your order.")
            },
            error: (err: ProblemDetails) => {
              this.dialogService.error(err.detail ?? "Failed to cancel your order.")
            }
          });
        }
      })
  }

  returnOrder() {
    if (!this.orderDetails() || !this.orderDetails()?.id)
      return;
    const orderId = this.orderDetails()!.id;

    this.dialogService.confirm({ title: "Are you sure you want to return this order ?" })
      .then(result => {
        if (result.isConfirmed) {
          this.orderApiService.returnOrder(orderId).subscribe({
            next: () => {
              this.orderDetails.update(o => {
                if (!o) return;
                return { ...o, status: OrderStatus.Returned }
              });
              this.dialogService.success("Successfully returned your order.")
            },
            error: (err: ProblemDetails) => {
              this.dialogService.error(err.detail ?? "Failed to return your order.")
            }
          });
        }
      });
  }

  actionButtonsVisible(): boolean {
    const status = this.orderDetails()?.status;
    return status === OrderStatus.AwaitingPayment
      || status === OrderStatus.Delivered;
  }
}
