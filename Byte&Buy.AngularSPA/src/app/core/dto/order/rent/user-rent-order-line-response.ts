import { MoneyDto } from "../../common/money-dto";
import { UserOrderLineResponse } from "../common/user-order-line-response";

export interface UserRentOrderLineResponse extends UserOrderLineResponse {
    type: "rent";
    pricePerDay: MoneyDto;
    rentalDays: number;
}