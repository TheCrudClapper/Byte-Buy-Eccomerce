import { Guid } from "guid-typescript";
import { ImageResponse } from "../../../core/dto/image/image-response";
import { PrivateSeller } from "./private-seller";
import { CompanySeller } from "./company-seller";
import { OfferStatus } from "../../../core/dto/offers/enum/offer-status";

export interface RentOfferDetails{
    id: Guid;
    maxRentalDays: number;
    quantityAvaliable: number; 
    pricePerDayAmount: number;
    pricePerDayCurrency: string; 
    condition: string;
    status: OfferStatus;
    category: string;
    description: string;
    title: string;
    seller: PrivateSeller | CompanySeller
    images: ImageResponse[]
}