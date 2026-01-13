import { Guid } from "guid-typescript";

export interface DeliveryOptionResponse{
    id: Guid;
    name: string;
    carrier: string;
    deliveryChannel: string;
    amount: number;
    currency: string;
}