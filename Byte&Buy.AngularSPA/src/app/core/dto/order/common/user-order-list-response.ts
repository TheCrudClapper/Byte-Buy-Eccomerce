import { Guid } from "guid-typescript";
import { MoneyDto } from "../../common/money-dto";
import { OrderStatus } from "../enum/order-status";
import { UserSaleOrderLineResponse } from "../sale/user-sale-order-line-response";
import { UserRentOrderLineResponse } from "../rent/user-rent-order-line-response";

export type UserOrderLineResponseUnion =
    | UserSaleOrderLineResponse
    | UserRentOrderLineResponse;

export interface UserOrderListResponse {
    orderId: Guid;
    status: OrderStatus;
    sellerDisplayName: string;
    buyerDisplayName: string;
    purchasedDate: Date;
    linesCount: number;
    isDeletable: boolean;
    totalItemsCost: MoneyDto;
    deliveryCost: MoneyDto;
    totalCost: MoneyDto;
    lines: UserOrderLineResponseUnion[];
}