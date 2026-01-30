import { PaymentResponse } from "../../../core/dto/payment/payment-response";
import { PaymentModel } from "../models/payment-model";

export function mapToPaymentModel(dto: PaymentResponse): PaymentModel {
  return {
    method: dto.method,
    paymentTotal: dto.paymentTotal,
  };
}