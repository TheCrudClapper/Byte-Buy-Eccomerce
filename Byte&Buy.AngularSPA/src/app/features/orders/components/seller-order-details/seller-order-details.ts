import { Component, computed, inject, OnInit, signal } from '@angular/core';
import { ActivatedRoute, Router, RouterLink } from '@angular/router';
import { Guid } from 'guid-typescript';
import { environment } from '../../../../../environments/environment';
import { OrderApiService } from '../../../../core/clients/orders/order-api-service';
import { DeliveryChannel } from '../../../../core/dto/delivery/enum/delivery-channel';
import { OrderStatus } from '../../../../core/dto/order/enum/order-status';
import { OrderDetailsResponse } from '../../../../core/dto/order/order-details-response';
import { ToastService } from '../../../../shared/services/snackbar/toast-service';
import { CommonModule } from '@angular/common';
import { DocumentsApiService } from '../../../../core/clients/documents/documents-api-service';
import { ProblemDetails } from '../../../../core/dto/problem-details';
import { OrderDetialsFacade } from '../../services/order-details-facade';

@Component({
  selector: 'app-seller-order-details',
  standalone: true,
  imports: [CommonModule],
  templateUrl: './seller-order-details.html',
  styleUrl: './seller-order-details.scss',
})
export class SellerOrderDetails implements OnInit {
  protected readonly facade = inject(OrderDetialsFacade);
  private readonly route = inject(ActivatedRoute);
  readonly imageBaseUrl = environment.staticImagesBaseUrl;

  actionButtonsVisible(): boolean {
    const status = this.facade.orderDetails()?.status;
    return status === OrderStatus.AwaitingPayment
      || status === OrderStatus.Delivered;
  }

  ngOnInit(): void {
    const id = this.route.snapshot.paramMap.get('id');
    if (!id) return;

    const guid = Guid.parse(id);
    this.facade.init(guid)
  }

}
