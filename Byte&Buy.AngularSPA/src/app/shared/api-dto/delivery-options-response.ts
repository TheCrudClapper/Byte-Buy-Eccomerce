import { DeliveryOptionResponse } from "./delivery-option-response";

export interface DeliveryOptionsResponse{
    parcelLockerDeliveries: DeliveryOptionResponse[];
    courierDeliveries: DeliveryOptionResponse[];
    pickupPointDeliveries: DeliveryOptionResponse[];
}