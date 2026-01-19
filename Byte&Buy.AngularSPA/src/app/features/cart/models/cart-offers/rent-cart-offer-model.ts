import { MoneyDto } from "../../../../core/dto/common/money-dto";
import { CartOfferBase } from "./cart-offer-base";

export interface RentCartOfferModel extends CartOfferBase {
  type: 'rent';
  pricePerDay: MoneyDto;
  rentalDays: number;
}