import { Guid } from "guid-typescript";
import { PrivateSeller } from "./private-seller";
import { CompanySeller } from "./company-seller";
import { ImageResponse } from "../../../shared/api-dto/image-response";

export interface SaleOfferDetails {
    id: Guid;
    quantityAvaliable: number;
    pricePerItemAmount: number;
    pricePerItemCurrency: string;
    condition: string;
    category: string;
    description: string;
    title: string;
    seller: PrivateSeller | CompanySeller
    images: ImageResponse[]
}