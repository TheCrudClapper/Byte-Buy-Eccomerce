import { Component } from '@angular/core';
import { CartItemModel } from '../../../models/cart-item-model';
import { Guid } from 'guid-typescript';
import { CartItem } from "../../cart-item/cart-item";

@Component({
  selector: 'app-cart-page',
  imports: [CartItem],
  templateUrl: './cart-page.html',
  styleUrl: './cart-page.scss',
})

export class CartPage {
 cartItems: CartItemModel[] = [
     {
       id: Guid.create(),
       offerTitle: "Komputer Ryzen 5 5600 + RTX 3070 Ti + 16Gb RAM",
       offerType: "Sale Offer",
       unitPrice: 3500 ,
       quantity: 2,
       imageUrl: "test4.jpg"
     },
     {
       id: Guid.create(),
       offerTitle: "GTX 1080 TI Used",
       offerType: "Rent Offer",
       unitPrice: 50,
       quantity: 3,
       imageUrl: "test.jpg"
     }
   ]
}
