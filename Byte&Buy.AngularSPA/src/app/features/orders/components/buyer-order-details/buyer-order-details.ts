import { Component, computed, inject, OnInit, signal } from '@angular/core';
import { ActivatedRoute, Router, RouterLink } from '@angular/router';
import { environment } from '../../../../../environments/environment';
import { OrderStatus } from '../../../../core/dto/order/enum/order-status';
import { Guid } from 'guid-typescript';
import { CommonModule } from '@angular/common';
import { OrderDetialsFacade as OrderDetailsFacade } from '../../services/order-details-facade';

@Component({
  selector: 'app-buyer-order-details',
  imports: [CommonModule, RouterLink],
  standalone: true,
  templateUrl: './buyer-order-details.html',
  styleUrl: './buyer-order-details.scss',
})

export class BuyerOrderDetails implements OnInit {
  protected readonly facade = inject(OrderDetailsFacade);
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
    this.facade.init(guid);
  }
}

