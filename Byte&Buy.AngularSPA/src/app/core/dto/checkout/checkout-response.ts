import { MoneyDto } from "../common/money-dto";
import { SellerGroup } from "./seller-group";

export interface CheckoutResponse {
  buyerFirstName: string;
  buyerLastName: string;
  buyerPhone: string;
  sellersGroups: SellerGroup[];
  itemsCost: MoneyDto;
  tax: MoneyDto;
  totalCost: MoneyDto;
}