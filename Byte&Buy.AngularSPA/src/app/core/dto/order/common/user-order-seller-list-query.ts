import { OrderStatus } from "../enum/order-status";

export interface UserOrderSellerListQuery{
    pageNumber: number;
    pageSize: number;
    buyerFullName?: string;
    status?: OrderStatus;
    itemName?: string; 
    purchasedFrom?: string;
    purchasedTo?: string;
}