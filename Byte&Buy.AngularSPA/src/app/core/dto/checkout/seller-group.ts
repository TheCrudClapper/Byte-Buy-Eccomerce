import { Guid } from "guid-typescript";
import { DeliveryOption } from "../../../shared/models/delivery-options";
import { MoneyDto } from "../common/money-dto";
import { DeliveryOptionResponse } from "../delivery/delivery-option-response";
import { DeliveryOptionsResponse } from "../delivery/delivery-options-response";
import { RentCheckoutItem } from "./rent-checkout-item";
import { SaleCheckoutItem } from "./sale-checkout-item";

export type CheckoutItem = SaleCheckoutItem | RentCheckoutItem;

export interface SellerGroup {
  sellerId: Guid;
  sellerDisplayName: string;
  sellerEmail: string;
  itemsWorth: MoneyDto;
  checkoutItems: CheckoutItem[];
  deliveryOptions: DeliveryOptionsResponse
}