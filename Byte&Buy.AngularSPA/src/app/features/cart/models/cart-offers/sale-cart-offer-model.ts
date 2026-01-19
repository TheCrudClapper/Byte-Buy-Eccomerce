import { MoneyDto } from "../../../../core/dto/common/money-dto";
import { CartOfferBase } from "./cart-offer-base";

export interface SaleCartOfferModel extends CartOfferBase {
  type: 'sale';
  pricePerItem: MoneyDto;
}