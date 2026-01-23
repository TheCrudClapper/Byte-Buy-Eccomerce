import { Guid } from "guid-typescript";

export interface DeliveryOption{
    id: Guid,
    name: string,
    currency: string;
    amount: number;
    carrier: string;    
}