import { MoneyDto } from "../common/money-dto";
import { ImageThumbnail } from "../image/image-thumbnail";

export interface BaseCheckoutItem {
  offerId: string;
  itemName: string;
  thumbnail: ImageThumbnail;
  quantity: number;
  subtotal: MoneyDto;
}