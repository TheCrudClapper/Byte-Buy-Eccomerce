import { Guid } from "guid-typescript";
import { MoneyDto } from "../../common/money-dto";
import { CompanySellerResponse } from "../common/company-seller-response";
import { PrivateSellerResponse } from "../common/private-seller-response";
import { ImageResponse } from "../../image/image-response";
import { OfferStatus } from "../enum/offer-status";

export interface SaleOfferDetailsResponse {
    id: Guid;
    quantityAvaliable: number;
    status: OfferStatus;
    pricePerItem: MoneyDto;
    condition: string;
    category: string;
    description: string;
    title: string;
    seller: CompanySellerResponse | PrivateSellerResponse;
    images: ImageResponse[];
}