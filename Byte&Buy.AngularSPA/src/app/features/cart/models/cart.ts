import { CartOffer } from "./cart-offers/cart-offer-union";
import { CartSummary } from "./cart-summary";

export interface Cart {
  items: CartOffer[];
  summary: CartSummary;
}