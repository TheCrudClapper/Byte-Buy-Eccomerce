import { Guid } from "guid-typescript";
import { SellerDeliveryRequest } from "./seller-delivery-request";

export interface OrderAddRequest{
    paymentMethodId: number,
    selectedDeliveries: SellerDeliveryRequest[];
}