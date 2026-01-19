import { Guid } from "guid-typescript";

export interface SaleCartOfferAddRequest{
    quantity: number;
    offerId: Guid;
}