import { MoneyDto } from "../../common/money-dto";
import { UserOrderLineResponse } from "../common/user-order-line-response";

export interface UserSaleOrderLineResponse extends UserOrderLineResponse {
    type: "sale";
    pricePerItem: MoneyDto;
}