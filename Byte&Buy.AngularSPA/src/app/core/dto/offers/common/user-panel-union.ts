import { UserSalePanelResponse } from "../rent/user-sale-panel-response";
import { UserRentPanelResponse } from "../sale/user-rent-panel-response";

export type UserPanelOfferUnion = UserRentPanelResponse | UserSalePanelResponse;