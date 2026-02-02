import { MoneyDto } from "../../common/money-dto";
import { ImageThumbnailDto } from "../../image/image-thumbnail";

export interface UserOrderLineResponse {
    type: "sale" | "rent";
    itemTitle: string;
    quantity: number;
    total: MoneyDto;
    thumbnail: ImageThumbnailDto;
}