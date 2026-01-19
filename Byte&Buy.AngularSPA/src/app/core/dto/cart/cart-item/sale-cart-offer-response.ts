import { MoneyDto } from "../../common/money-dto";
import { CartOfferResponse } from "./cart-offer-response";

export interface SaleCartOfferResponse extends CartOfferResponse{
    pricePerItem: MoneyDto;
    type: "sale";
}