import { RentBrowserItemResponse } from "../rent/rent-browser-item-response";
import { SaleBrowserItemResponse } from "../sale/sale-browser-item-response";

export type OfferUnion = SaleBrowserItemResponse | RentBrowserItemResponse;