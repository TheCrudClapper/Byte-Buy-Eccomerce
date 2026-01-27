import { MoneyDto } from "../common/money-dto";
import { RentCheckoutItem } from "./rent-checkout-item";
import { SaleCheckoutItem } from "./sale-checkout-item";

export type CheckoutItem = SaleCheckoutItem | RentCheckoutItem;

export interface SellerGroup {
  sellerId: string;
  sellerDisplayName: string;
  sellerEmail: string;
  itemsWorth: MoneyDto;
  checkoutItems: CheckoutItem[];
}