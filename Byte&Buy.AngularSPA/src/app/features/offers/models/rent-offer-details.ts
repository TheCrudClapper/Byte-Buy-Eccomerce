import { Guid } from "guid-typescript";
import { OfferSeller } from "./offer-seller";
import { ImageResponse } from "../../../shared/api-dto/image-response";

export interface RentOfferDetails{
    id: Guid;
    maxRentalDays: number;
    quantityAvaliable: number; 
    pricePerDayAmount: number;
    pricePerDayCurrency: number; 
    condition: string;
    category: string;
    description: string;
    title: string;
    seller: OfferSeller;
    images: ImageResponse[]
}