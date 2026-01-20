import { MoneyDto } from "../../common/money-dto";
import { OfferBrowserItemResponse } from "../common/offer-browser-item-response";

export interface RentBrowserItemResponse extends OfferBrowserItemResponse{
    type: 'rent';
    pricePerDay: MoneyDto;
    maxRentalDays: number;
}