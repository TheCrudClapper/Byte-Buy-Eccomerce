import { MoneyDto } from "../common/money-dto";
import { SelectListItemResponseInt } from "../common/select-list-item-response-int";
import { SellerGroup } from "./seller-group";

export interface CheckoutResponse {
  buyerFirstName: string;
  buyerLastName: string;
  buyerPhone: string;
  sellersGroups: SellerGroup[];
  avaliablePaymentMethods: SelectListItemResponseInt[];
  itemsCost: MoneyDto;
  tax: MoneyDto;
  totalCost: MoneyDto;
  canPlaceOrder: boolean;
}