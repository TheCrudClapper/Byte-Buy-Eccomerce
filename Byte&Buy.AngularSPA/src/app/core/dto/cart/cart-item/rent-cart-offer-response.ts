import { MoneyDto } from "../../common/money-dto";
import { CartOfferResponse } from "./cart-offer-response";

export interface RentCartOfferResponse extends CartOfferResponse{
    pricePerDay: MoneyDto;
    rentalDays: number;
    type: "rent";
}