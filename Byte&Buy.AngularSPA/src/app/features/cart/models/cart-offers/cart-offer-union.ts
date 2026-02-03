import { RentCartOfferModel } from "./rent-cart-offer-model";
import { SaleCartOfferModel } from "./sale-cart-offer-model";

export type CartOffer = SaleCartOfferModel | RentCartOfferModel;