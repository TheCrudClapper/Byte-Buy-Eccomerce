import { Guid } from "guid-typescript";
import { MoneyDto } from "../../common/money-dto";
import { ImageResponse } from "../../image/image-response";
import { OfferAddressResponse } from "../common/offer-address-response";

export interface UserRentOfferResponse{
    id: Guid;
    categoryId: Guid;
    conditionId: Guid;
    name: string;
    description: string;
    quantityAvailable: number;
    pricePerDay: MoneyDto;
    offerAddress: OfferAddressResponse;
    maxRentalDays: number;
    images: ImageResponse[];
    parcelLockerDeliveries: Guid[] | null;
    otherDeliveriesIds: Guid[];
}