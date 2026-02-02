import { MoneyDto } from "../common/money-dto";
import { ImageThumbnailDto } from "../image/image-thumbnail";

export interface BaseCheckoutItem {
  offerId: string;
  itemName: string;
  thumbnail: ImageThumbnailDto;
  quantity: number;
  subtotal: MoneyDto;
}