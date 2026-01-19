import { MoneyDto } from "../../../core/dto/common/money-dto";

export interface CartSummary {
  itemsQuantity: number;
  totalItemsValue: MoneyDto;
  taxValue: MoneyDto;
  estimatedShippingCost: MoneyDto;
  totalCost: MoneyDto;
}