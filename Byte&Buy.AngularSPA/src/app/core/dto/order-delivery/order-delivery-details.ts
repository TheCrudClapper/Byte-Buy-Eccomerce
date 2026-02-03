import { MoneyDto } from "../common/money-dto";
import { DeliveryChannel } from "../delivery/enum/delivery-channel";

export interface OrderDeliveryDetails {
    carrierCode: string;
    deliveryName: string;
    channel: DeliveryChannel;
    price: MoneyDto;
}

export interface CourierDeliveryDetails extends OrderDeliveryDetails {
    type: 'courier';
    street: string;
    houseNumber: string;
    flatNumber?: string | null;
    city: string;
    postalCity: string;
    postalCode: string;
}

export interface PickupPointDeliveryDetails extends OrderDeliveryDetails {
    type: 'pickupPoint';
    pickupPointName: string;
    pickupPointId: string;
    pickupStreet: string;
    pickupCity: string;
    pickupLocalNumber: string;
}

export interface ParcelLockerDeliveryDetails extends OrderDeliveryDetails {
    type: 'parcelLocker';
    parcelLockerId: string;
}