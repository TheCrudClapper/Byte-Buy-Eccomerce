import { CartOfferResponse } from "./cart-item/cart-offer-response";
import { CartSummaryResponse } from "./cart-summary-response";

export interface CartResponse{
    cartOffers: CartOfferResponse[];
    summary: CartSummaryResponse;
}