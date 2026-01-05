import { Guid } from "guid-typescript";

export interface CartItemModel{
    id: Guid;
    imageUrl: string;
    offerTitle: string;
    unitPrice: number;
    quantity: number;
    offerType: string;
}