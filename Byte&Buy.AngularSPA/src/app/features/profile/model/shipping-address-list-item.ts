import { Guid } from "guid-typescript";

export interface ShippingAddressListItem {
    id: Guid;
    label: string;
    houseNumber: string;
    postalCity: string;
    postalCode: string;
    city: string;
    flatNumber: string | null;
    isDefault: boolean;
}