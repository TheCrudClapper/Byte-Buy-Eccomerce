import { Component, Input, OnInit } from '@angular/core';
import { OrderStatus } from '../../../core/dto/order/enum/order-status';
import { DatePipe } from '@angular/common';
import { OrderStatusModel } from '../../models/order-status-model';

@Component({
  selector: 'app-order-status-component',
  imports: [DatePipe],
  templateUrl: './order-status-component.html',
  styleUrl: './order-status-component.scss',
})

export class OrderStatusComponent implements OnInit {
  orderStatusModel: OrderStatusModel = {
    iconClass: '',
    statusText: '',
    dateDelivered: null
  };

  @Input() status!: OrderStatus;
  @Input() dateDelivered?: string | null;

  ngOnInit(): void {
    this.initialize();
  }

  initialize() {
    if (this.status === undefined || this.status === null) return;

    switch (this.status) {
      case OrderStatus.AwaitingPayment:
        this.orderStatusModel.iconClass = 'fa-solid fa-hourglass-end';
        this.orderStatusModel.statusText = 'Awaiting Payment';
        break;
      case OrderStatus.Paid:
        this.orderStatusModel.iconClass = 'fa-solid fa-coins';
        this.orderStatusModel.statusText = 'Order Paid';
        break;
      case OrderStatus.Canceled:
        this.orderStatusModel.iconClass = 'fa-solid fa-ban';
        this.orderStatusModel.statusText = 'Canceled by User';
        break;

      case OrderStatus.Shipped:
        this.orderStatusModel.iconClass = 'fa-solid fa-truck';
        this.orderStatusModel.statusText = 'Order in Shipment';
        break;

      case OrderStatus.Returned:
        this.orderStatusModel.iconClass = 'fa-solid fa-arrow-rotate-left';
        this.orderStatusModel.statusText = 'Order Returned';
        break;

      case OrderStatus.SystemCanceled:
        this.orderStatusModel.iconClass = 'fa-solid fa-handshake-slash';
        this.orderStatusModel.statusText = 'Order Canceled';
        break;

      case OrderStatus.Delivered:
        this.orderStatusModel.iconClass = 'fa-solid fa-truck-ramp-box';
        this.orderStatusModel.statusText = 'Package Delivered';
        if (this.dateDelivered) {
          this.orderStatusModel.dateDelivered = this.dateDelivered;
        }
        break;

      default:
        this.orderStatusModel.iconClass = 'fa-solid fa-bug';
        this.orderStatusModel.statusText = 'Unknown Status';
        break;
    }
  }
}