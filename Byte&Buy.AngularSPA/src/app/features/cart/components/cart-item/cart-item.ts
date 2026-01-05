import { Component, Input } from '@angular/core';
import { CartItemModel } from '../../models/cart-item-model';

@Component({
  selector: 'app-cart-item',
  imports: [],
  templateUrl: './cart-item.html',
  standalone: true,
  styleUrl: './cart-item.scss',
})
export class CartItem {
  @Input() item!: CartItemModel;
}
