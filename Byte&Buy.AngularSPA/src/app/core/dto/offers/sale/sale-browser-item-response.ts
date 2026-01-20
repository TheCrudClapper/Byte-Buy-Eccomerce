import { MoneyDto } from "../../common/money-dto";
import { OfferBrowserItemResponse } from "../common/offer-browser-item-response";

export interface SaleBrowserItemResponse extends OfferBrowserItemResponse{
    type: 'sale';
    pricePerItem: MoneyDto;
}