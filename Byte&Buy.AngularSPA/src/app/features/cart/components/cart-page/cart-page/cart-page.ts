import { Component, inject, OnInit, signal } from '@angular/core';
import { CartApiService } from '../../../../../core/clients/cart/cart-api-service';
import { Cart } from '../../../models/cart';
import { toCartModel } from '../../../mappers/cart-mapper';
import { ProblemDetails } from '../../../../../core/dto/problem-details';
import { DecimalPipe } from '@angular/common';
import { SaleCartOffer } from '../../sale-cart-offer/sale-cart-offer/sale-cart-offer';
import { RentCartOffer } from '../../rent-cart-offer/rent-cart-offer/rent-cart-offer';
import { CartSummary } from '../../../models/cart-summary';
import { Guid } from 'guid-typescript';
import { Router } from '@angular/router';

@Component({
  selector: 'app-cart-page',
  imports: [DecimalPipe, SaleCartOffer, RentCartOffer],
  templateUrl: './cart-page.html',
  styleUrl: './cart-page.scss',
  standalone: true,
})

export class CartPage implements OnInit {
  private readonly cartApiService = inject(CartApiService);

  cartModel = signal<Cart | null>(null);

  ngOnInit(): void {
    this.loadCart();
  }

  loadCart() {
    this.cartApiService.getCart().subscribe({
      next: (data) => {
        this.cartModel.set(toCartModel(data))
      },
      error: (err: ProblemDetails) => console.log(err.detail)
    });
  }

  //update cart summary using child components output
  updateSummary(summary: CartSummary): void {
    this.cartModel.update(cart =>
      cart ? { ...cart, summary } : cart
    );
  }

  removeItem(id: Guid): void {
    this.cartModel.update(cart =>
      cart
        ? { ...cart, items: cart.items.filter(i => i.id !== id) }
        : cart
    );
  }
}
