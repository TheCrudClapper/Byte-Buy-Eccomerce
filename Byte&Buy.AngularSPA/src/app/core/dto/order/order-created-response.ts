import { Guid } from "guid-typescript";

export interface OrderCreatedResponse{
    paymentId: Guid;
    methodUsed: number;
}