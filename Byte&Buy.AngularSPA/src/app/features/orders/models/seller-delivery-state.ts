import { Guid } from "guid-typescript";
import { DeliveryOptionResponse } from "../../../core/dto/delivery/delivery-option-response";


export type SellerDeliveryState = 
    | {
        channel: 'Courier',
        delivery: DeliveryOptionResponse,
        shippingAddressId: string;
    }
    | {
        channel: 'ParcelLocker';
        delivery: DeliveryOptionResponse
        parcelLocker: {
            lockerId: string;
        };
    }
    | {
        channel: 'PickupPoint';
        delivery: DeliveryOptionResponse;
        pickupPoint: {
            pickupPointId: string;
            street: string;
            city: string;
            localNumber: string;
        };
    };
    