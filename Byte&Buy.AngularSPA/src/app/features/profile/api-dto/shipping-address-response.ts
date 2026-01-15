import { Guid } from "guid-typescript"

export interface ShippingAddressResponse{
    id: Guid;
    countryId: Guid;
    label: string;
    street: string;
    houseNumber :string;
    postalCity: string;
    postalCode :string;
    city :string;
    country :string;
    flatNumber :string | null;
    isDefault: boolean;
}