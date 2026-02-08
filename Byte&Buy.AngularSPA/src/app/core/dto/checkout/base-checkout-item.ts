import { MoneyDto } from "../common/money-dto";
import { ImageThumbnailDto } from "../image/image-thumbnail";
import { OfferStatus } from "../offers/enum/offer-status";

export interface BaseCheckoutItem {
  offerId: string;
  itemName: string;
  thumbnail: ImageThumbnailDto;
  quantity: number;
  avaliableQuantity: number;
  status: OfferStatus;
  canFinalize: boolean;
  subtotal: MoneyDto;
}