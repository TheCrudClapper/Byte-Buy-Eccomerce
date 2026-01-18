import { Guid } from "guid-typescript";
import { MoneyDto } from "../../../shared/api-dto/money-dto";
import { CompanySellerResponse } from "./company-seller-response";
import { PrivateSellerResponse } from "./private-seller-response";
import { ImageResponse } from "../../../shared/api-dto/image-response";

export interface SaleOfferDetailsResponse {
    id: Guid;
    quantityAvaliable: number;
    pricePerItem: MoneyDto;
    condition: string;
    category: string;
    description: string;
    title: string;
    seller: CompanySellerResponse | PrivateSellerResponse;
    images: ImageResponse[];
}