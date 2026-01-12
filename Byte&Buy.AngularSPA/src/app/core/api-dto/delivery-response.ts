import { Guid } from "guid-typescript"

export interface DeliveryResponse{
    id: Guid,
    name: string,
    currency: string;
    amount: string;
    carrier: string;      
}