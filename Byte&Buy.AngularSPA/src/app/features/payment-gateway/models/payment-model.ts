import { MoneyDto } from "../../../core/dto/common/money-dto";
import { PaymentMethod } from "./payment-method";

export interface PaymentModel {
    paymentTotal: {
        amount: number;
        currency: string;
    };
    method: PaymentMethod;
}