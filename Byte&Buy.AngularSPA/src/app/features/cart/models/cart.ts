import { CartOffer } from "./cart-offers/cart-offer-alias";
import { CartSummary } from "./cart-summary";

export interface Cart {
  items: CartOffer[];
  summary: CartSummary;
}