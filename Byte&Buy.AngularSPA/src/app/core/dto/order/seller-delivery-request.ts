import { Guid } from "guid-typescript";
import { ParcelLockerDeliveryRequest } from "./parcel-locker-delivery-request";
import { PickupPointDeliveryRequest } from "./pickup-point-delivery-request";

export interface SellerDeliveryRequest{
    sellerId: string;
    deliveryId: string;

    shippingAddressId?: string;
    parcelLockerDeliveryRequest?: ParcelLockerDeliveryRequest;
    pickupPointDeliveryRequest?: PickupPointDeliveryRequest;
}