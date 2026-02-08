import { MoneyDto } from "../../common/money-dto";
import { UserPanelOfferResponse } from "../common/user-panel-offer-response";

export interface UserRentPanelResponse extends UserPanelOfferResponse {
    type: "rent";
    pricePerDay: MoneyDto;
    maxRentalDays: number;
}