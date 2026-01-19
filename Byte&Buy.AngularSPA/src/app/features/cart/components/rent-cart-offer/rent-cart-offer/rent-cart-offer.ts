import { Component, inject, Input, input } from '@angular/core';
import { CartApiService } from '../../../../../core/clients/cart/cart-api-service';
import { RentCartOfferModel } from '../../../models/cart-offers/rent-cart-offer-model';

@Component({
  selector: 'app-rent-cart-offer',
  imports: [],
  templateUrl: './rent-cart-offer.html',
  styleUrl: './rent-cart-offer.scss',
})
export class RentCartOffer {
  @Input() rentCartOffer!: RentCartOfferModel;
  private readonly cartApiService = inject(CartApiService);
}
