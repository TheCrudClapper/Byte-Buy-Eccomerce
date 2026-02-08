import { MoneyDto } from "../../common/money-dto";
import { UserPanelOfferResponse } from "../common/user-panel-offer-response";

export interface UserSalePanelResponse extends UserPanelOfferResponse {
    type: "sale";
    pricePerItem: MoneyDto;
}