import { Guid } from "guid-typescript";
import { PrivateSeller } from "./private-seller";
import { CompanySeller } from "./company-seller";
import { ImageResponse } from "../../../core/dto/image/image-response";
import { OfferStatus } from "../../../core/dto/offers/enum/offer-status";

export interface SaleOfferDetails {
    id: Guid;
    quantityAvaliable: number;
    pricePerItemAmount: number;
    pricePerItemCurrency: string;
    condition: string;
    status: OfferStatus;
    category: string;
    description: string;
    title: string;
    seller: PrivateSeller | CompanySeller
    images: ImageResponse[]
}