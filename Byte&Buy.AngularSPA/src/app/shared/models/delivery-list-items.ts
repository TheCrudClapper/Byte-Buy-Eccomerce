import { Guid } from "guid-typescript";
export interface DeliveryListItem{
    id: Guid,
    name: string,
    currency: string;
    amount: number;
    carrier: string;    
}