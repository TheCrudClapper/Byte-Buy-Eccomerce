import { Guid } from "guid-typescript";
import { ImageResponse } from "../../image/image-response";
import { OfferStatus } from "../enum/offer-status";

export interface OfferBrowserItemResponse{
    id: Guid;
    image: ImageResponse,
    title: string,
    condition: string,
    category: string,
    city: string;
    status: OfferStatus;
    postalCity: string;
    postalCode: string;
    isCompanyOffer: boolean;
    type: 'sale' | 'rent';
}