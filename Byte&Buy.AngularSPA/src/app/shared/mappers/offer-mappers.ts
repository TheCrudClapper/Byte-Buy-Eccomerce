import { DeliveryOptionResponse } from "../../core/dto/delivery/delivery-option-response";
import { DeliveryListItem } from "../models/delivery-list-items";

export function mapToListItem(item: DeliveryOptionResponse): DeliveryListItem{
    return{
        id: item.id,
        amount: item.amount,
        carrier: item.carrier,
        currency: item.currency,
        name: item.name
    }
}