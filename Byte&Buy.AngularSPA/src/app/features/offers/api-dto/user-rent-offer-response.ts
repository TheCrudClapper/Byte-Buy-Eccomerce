import { Guid } from "guid-typescript";
import { MoneyDto } from "../../../shared/api-dto/money-dto";
import { ImageResponse } from "../../../shared/api-dto/image-response";

export interface UserRentOfferResponse{
    id: Guid;
    categoryId: Guid;
    conditionId: Guid;
    name: string;
    description: string;
    quantityAvailable: number;
    pricePerDay: MoneyDto;
    maxRentalDays: number;
    images: ImageResponse[];
    parcelLockerDeliveries: Guid[] | null;
    otherDeliveriesIds: Guid[];
}