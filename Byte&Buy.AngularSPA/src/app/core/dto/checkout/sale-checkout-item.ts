import { MoneyDto } from "../common/money-dto";
import { BaseCheckoutItem } from "./base-checkout-item";

export interface SaleCheckoutItem extends BaseCheckoutItem {
  type: 'sale';
  pricePerItem: MoneyDto;
}