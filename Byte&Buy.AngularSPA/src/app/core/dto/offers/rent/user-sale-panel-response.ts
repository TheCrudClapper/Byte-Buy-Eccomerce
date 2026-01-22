import { MoneyDto } from "../../common/money-dto";

export interface UserSalePanelResponse{
    type: "sale";
    pricePerItem: MoneyDto;
}