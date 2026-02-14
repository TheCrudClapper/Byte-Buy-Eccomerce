import { RentalStatus } from "../enum/rental-status";

export interface UserRentalLenderQuery{
    pageNumber: number;
    pageSize: number;
    itemName?: string;
    rentalStatus?: RentalStatus;
    rentalStartDate?: string;
    rentalEndDate?: string;
}