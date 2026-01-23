import { DeliveryOptionResponse } from "../../core/dto/delivery/delivery-option-response";
import { DeliveryOption } from "../models/delivery-options";

export function mapToDeliveryOption(item: DeliveryOptionResponse): DeliveryOption{
    return{
        id: item.id,
        amount: item.amount,
        carrier: item.carrier,
        currency: item.currency,
        name: item.name
    }
}