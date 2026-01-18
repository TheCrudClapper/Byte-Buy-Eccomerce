import { Guid } from "guid-typescript"

export interface DeliveryResponse{
    id: Guid,
    name: string,
    currency: string;
    amount: number;
    carrier: string;      
}