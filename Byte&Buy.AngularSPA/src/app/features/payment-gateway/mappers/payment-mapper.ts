import { PaymentResponse } from "../../../core/dto/payment/payment-response";
import { PaymentMethod } from "../models/payment-method";
import { PaymentModel } from "../models/payment-model";

export function mapToPaymentModel(dto: PaymentResponse): PaymentModel {
  const paymentMethod = dto.method  === 0 ? 'Blik' : 'Card';
  return {
    method: dto.method as PaymentMethod,
    paymentTotal: dto.paymentTotal,
  };
}