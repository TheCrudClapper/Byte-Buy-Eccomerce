import { Component, Input } from '@angular/core';
import { OrderStatus as OrderStatusEnum } from '../../../core/dto/order/enum/order-status';
import { DatePipe } from '@angular/common';

@Component({
  selector: 'app-order-status-component',
  imports: [DatePipe],
  templateUrl: './order-status-component.html',
  styleUrl: './order-status-component.scss',
})
export class OrderStatusComponent {
  readonly OrderStatusEnum = OrderStatusEnum;
  @Input() status!: OrderStatusEnum;
  @Input() dateDelivered?: string | null;
}

