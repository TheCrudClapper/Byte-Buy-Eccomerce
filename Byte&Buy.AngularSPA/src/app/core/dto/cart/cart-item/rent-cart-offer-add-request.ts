import { Guid } from "guid-typescript";

export interface RentCartOfferAddRequest{
    quantity: number;
    offerId: Guid;
    rentalDays: number;
}
