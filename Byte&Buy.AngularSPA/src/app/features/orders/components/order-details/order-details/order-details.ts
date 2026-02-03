import { Component, inject, OnInit, signal } from '@angular/core';
import { ActivatedRoute, Router, RouterLinkActive, RouterLink } from "@angular/router";
import { OrderApiService } from '../../../../../core/clients/orders/order-api-service';
import { OrderDetailsResponse } from '../../../../../core/dto/order/order-details-response';
import { ToastService } from '../../../../../shared/services/snackbar/toast-service';
import { ProblemDetails } from '../../../../../core/dto/problem-details';
import { Guid } from 'guid-typescript';
import { environment } from '../../../../../../environments/environment';
import { OrderStatus } from '../../../../../core/dto/order/enum/order-status';
import { DeliveryChannel } from '../../../../../core/dto/delivery/enum/delivery-channel';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'app-order-details',
  imports: [CommonModule, RouterLink],
  templateUrl: './order-details.html',
  styleUrl: './order-details.scss',
})

export class OrderDetails implements OnInit {
  private readonly orderApiService = inject(OrderApiService);
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
        this.orderDetails.set(data),
        console.log(data);
      },
      error: (err: ProblemDetails) => {
        this.router.navigate(['/not-found'])
      }
    })
  }
}
