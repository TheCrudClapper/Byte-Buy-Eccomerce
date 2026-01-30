import { MoneyDto } from "../common/money-dto";

export interface PaymentResponse{
    method: number;
    paymentTotal: MoneyDto;
}