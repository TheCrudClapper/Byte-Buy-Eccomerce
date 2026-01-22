import { MoneyDto } from "../../common/money-dto";

export interface UserRentPanelResponse {
    type: "rent";
    pricePerDay: MoneyDto;
    maxRentalDays: number;
}