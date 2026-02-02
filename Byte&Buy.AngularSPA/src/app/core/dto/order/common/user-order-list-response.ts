import { Guid } from "guid-typescript";
import { MoneyDto } from "../../common/money-dto";
import { OrderStatus } from "../enum/order-status";
import { UserSaleOrderLineResponse } from "../sale/user-sale-order-line-response";
import { UserRentOrderLineResponse } from "../rent/user-rent-order-line-response";

export type UserOrderLineResponse =
    | UserSaleOrderLineResponse
    | UserRentOrderLineResponse;

export interface UserOrderListResponse {
    orderId: Guid;
    status: OrderStatus;
    sellerDisplayName: string;
    purchasedDate: Date;
    linesCount: number;
    totalItemsCost: MoneyDto;
    deliveryCost: MoneyDto;
    totalCost: MoneyDto;
    lines: UserOrderLineResponse[];
}