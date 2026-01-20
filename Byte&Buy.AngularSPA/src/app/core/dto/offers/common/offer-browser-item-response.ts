import { Guid } from "guid-typescript";
import { ImageResponse } from "../../image/image-response";

export interface OfferBrowserItemResponse{
    id: Guid;
    image: ImageResponse,
    title: string,
    condition: string,
    category: string,
    city: string;
    postalCity: string;
    postalCode: string;
    isCompanyOffer: boolean;
    type: 'sale' | 'rent';
}