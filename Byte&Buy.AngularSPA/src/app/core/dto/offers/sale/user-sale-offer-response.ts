import { Guid } from "guid-typescript";
import { MoneyDto } from "../../common/money-dto";
import { ImageResponse } from "../../image/image-response";

export interface UserSaleOfferResponse {
    id: Guid;
    categoryId: Guid;
    conditionId: Guid;
    name: string;
    description: string;
    quantityAvailable: number;
    pricePerItem: MoneyDto;
    images: ImageResponse[];
    parcelLockerDeliveries: Guid[] | null;
    otherDeliveriesIds: Guid[];
}