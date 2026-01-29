import { SellerDeliveryRequest } from "../../../core/dto/order/seller-delivery-request";
import { SellerDeliveryState } from "../models/seller-delivery-state";

export function buildSellerDeliveriesPayload(deliveryBySeller: Record<string, SellerDeliveryState | null>
): SellerDeliveryRequest[] {
    return Object.entries(deliveryBySeller).map(([sellerId, d]) => {
        if (!d) {
            throw new Error(`Missing delivery for seller ${sellerId}`);
        }

        switch (d.channel) {
            case 'Courier':
                return {
                    sellerId,
                    deliveryId: d.delivery.id.toString(),
                    shippingAddressId: d.shippingAddressId ?? undefined,
                };

            case 'ParcelLocker':
                return {
                    sellerId,
                    deliveryId: d.delivery.id.toString(),
                    parcelLockerData: {
                        lockerId: d.parcelLocker.lockerId,
                    },
                };

            case 'PickupPoint':
                return {
                    sellerId,
                    deliveryId: d.delivery.id.toString(),
                    pickupPointData: d.pickupPoint,
                };

            default:
                throw new Error(`Unsupported delivery channel`);
        }
    });
}