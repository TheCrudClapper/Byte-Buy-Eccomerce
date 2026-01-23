import { CartResponse } from "../../../core/dto/cart/cart-response";
import { CartSummaryResponse } from "../../../core/dto/cart/cart-summary-response";
import { Cart } from "../models/cart";
import { CartOffer } from "../models/cart-offers/cart-offer-alias";
import { RentCartOfferModel } from "../models/cart-offers/rent-cart-offer-model";
import { SaleCartOfferModel } from "../models/cart-offers/sale-cart-offer-model";
import { CartSummary } from "../models/cart-summary";

export function toCartModel(response: CartResponse): Cart {
    const items: CartOffer[] = response.cartOffers.map(item => {
        switch (item.type) {
            case 'sale':
                return {
                    id: item.id,
                    image: item.image,
                    offerId: item.offerId,
                    title: item.title,
                    quantity: item.quantity,
                    subtotal: item.subtotal,
                    type: 'sale',
                    pricePerItem: (item as any).pricePerItem
                } as SaleCartOfferModel;

            case 'rent':
                return {
                    id: item.id,
                    image: item.image,
                    offerId: item.offerId,
                    title: item.title,
                    quantity: item.quantity,
                    subtotal: item.subtotal,
                    type: 'rent',
                    pricePerDay: (item as any).pricePerDay,
                    rentalDays: (item as any).rentalDays
                } as RentCartOfferModel;

            default:
                throw new Error(`Unknown cart item type: ${(item as any).type}`);
        }
    });

    const s = response.summary;

    return {
        items,
        summary: {
            itemsQuantity: s.itemsQuantity,
            totalItemsValue: s.totalItemsValue,
            taxValue: s.taxValue,
            estimatedShippingCost: s.estimatedShippingCost,
            totalCost: s.totalCost
        }
    }
}

export function toCartResponseModel(response: CartSummaryResponse): CartSummary {
    return {
        itemsQuantity: response.itemsQuantity,
        totalItemsValue: response.totalItemsValue,
        taxValue: response.taxValue,
        estimatedShippingCost: response.estimatedShippingCost,
        totalCost: response.totalCost
    }

}
