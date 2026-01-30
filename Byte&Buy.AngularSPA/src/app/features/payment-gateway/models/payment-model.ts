import { MoneyDto } from "../../../core/dto/common/money-dto";

export interface PaymentModel{
    paymentTotal: MoneyDto;
    method: number;
}