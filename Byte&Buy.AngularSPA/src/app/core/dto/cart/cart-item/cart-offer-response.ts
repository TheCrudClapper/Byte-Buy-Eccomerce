import { Guid } from "guid-typescript";
import { ImageResponse } from "../../image/image-response";
import { MoneyDto } from "../../common/money-dto";

export interface CartOfferResponse{
    id: Guid;
    offerId: Guid;
    image: ImageResponse;
    title: string;
    quantity: number;
    subtotal: MoneyDto;
    type: 'sale' | 'rent';
}