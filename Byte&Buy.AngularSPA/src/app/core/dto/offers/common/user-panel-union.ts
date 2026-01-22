import { UserSalePanelResponse } from "../rent/user-sale-panel-response";
import { UserRentPanelResponse } from "../sale/user-rent-panel-response";
import { UserPanelOfferResponse } from "./user-panel-offer-response";

export type UserPanelOfferUnion =  UserPanelOfferResponse 
    & (UserRentPanelResponse | UserSalePanelResponse)