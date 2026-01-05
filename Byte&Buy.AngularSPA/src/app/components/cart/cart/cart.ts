import { Component } from '@angular/core';
import { CartItemModel } from '../../../features/cart/models/cart-item-model';
import { Guid } from 'guid-typescript';
import { CartItem } from "../../../features/cart/components/cart-item/cart-item";

@Component({
  selector: 'app-cart',
  imports: [CartItem],
  templateUrl: './cart.html',
  styleUrl: './cart.scss',
})
export class Cart {
  cartItems: CartItemModel[] = [
    {
      id: Guid.create(),
      offerTitle: "Komputer Ryzen 5 5600 + RTX 3070 Ti + 16Gb RAM",
      offerType: "Sale Offer",
      unitPrice: 3500 ,
      quantity: 2,
      imageUrl: "test4.jpg"
    }
  ]
}
