import { Guid } from "guid-typescript";
import { ImageResponse } from "../../../shared/api-dto/image-response";
import { PrivateSeller } from "./private-seller";
import { CompanySeller } from "./company-seller";

export interface RentOfferDetails{
    id: Guid;
    maxRentalDays: number;
    quantityAvaliable: number; 
    pricePerDayAmount: number;
    pricePerDayCurrency: string; 
    condition: string;
    category: string;
    description: string;
    title: string;
    seller: PrivateSeller | CompanySeller
    images: ImageResponse[]
}