import { MoneyDto } from "../common/money-dto";

export interface CartSummaryResponse {
    itemsQuantity: number;
    totalItemsValue: MoneyDto;
    taxValue: MoneyDto;
    estimatedShippingCost: MoneyDto;
    totalCost: MoneyDto;
}