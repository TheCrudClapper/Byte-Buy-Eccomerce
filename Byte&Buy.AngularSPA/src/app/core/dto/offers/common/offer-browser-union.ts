import { RentBrowserItem } from "../../../../features/offers/components/rent/rent-browser-item/rent-browser-item/rent-browser-item";
import { RentBrowserItemResponse } from "../rent/rent-browser-item-response";
import { SaleBrowserItemResponse } from "../sale/sale-browser-item-response";

export type OfferUnion = SaleBrowserItemResponse | RentBrowserItemResponse;