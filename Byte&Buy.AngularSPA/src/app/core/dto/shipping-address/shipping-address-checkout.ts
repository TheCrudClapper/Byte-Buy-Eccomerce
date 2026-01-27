import { Guid } from "guid-typescript";

export interface ShippingAddressCheckout {
    id: Guid;
    label: string;
    street: string;
    postalCity: string;
    postalCode: string;
    city: string;
    houseNumber: string;
    flatNumber?: string;
    isDefault: boolean;
}