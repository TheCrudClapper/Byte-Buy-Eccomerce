import { Guid } from "guid-typescript";
import { MoneyDto } from "../../../../core/dto/common/money-dto";
import { ImageResponse } from "../../../../core/dto/image/image-response";

export interface CartOfferBase {
    id: Guid;
    offerId: Guid;
    image: ImageResponse;
    title: string;
    quantity: number;
    subtotal: MoneyDto;
}