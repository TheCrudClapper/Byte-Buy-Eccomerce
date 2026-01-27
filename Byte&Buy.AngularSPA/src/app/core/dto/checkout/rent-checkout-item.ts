import { MoneyDto } from "../common/money-dto";
import { BaseCheckoutItem } from "./base-checkout-item";

export interface RentCheckoutItem extends BaseCheckoutItem {
  type: 'rent';
  rentalDays: number;
  pricePerDay: MoneyDto;
}