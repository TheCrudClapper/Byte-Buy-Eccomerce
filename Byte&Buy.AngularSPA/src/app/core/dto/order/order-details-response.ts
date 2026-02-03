import { Guid } from "guid-typescript";
import { OrderStatus } from "./enum/order-status";
import { MoneyDto } from "../common/money-dto";
import { CourierDeliveryDetails, ParcelLockerDeliveryDetails, PickupPointDeliveryDetails } from "../order-delivery/order-delivery-details";
import { HomeAddressDto } from "../home-address/home-address-dto";
import { UserOrderLineResponseUnion } from "./common/user-order-list-response";

export type OrderDetailsUnion = 
    | CourierDeliveryDetails
    | PickupPointDeliveryDetails
    | ParcelLockerDeliveryDetails

export interface OrderDetailsResponse {
  id: Guid; 
  status: OrderStatus;
  purchasedDate: Date; 
  dateDelivered?: string | null;
  sellerDisplayName: string;
  linesCount: number;
  totalItemsCost: MoneyDto;
  totalCost: MoneyDto;
  deliveryDetails: OrderDetailsUnion;
  buyerSnapshot: BuyerSnapshotResponse;
  lines: UserOrderLineResponseUnion[];
}

export interface BuyerSnapshotResponse {
  fullName: string;
  email: string;
  phoneNumber: string;
  address: HomeAddressDto;
}
