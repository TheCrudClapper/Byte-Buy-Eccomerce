import { MoneyDto } from "../common/money-dto";
import { ImageThumbnailDto } from "../image/image-thumbnail";
import { RentalStatus } from "./enum/rental-status";

export interface RentalLenderResponse {
  id: string;
  status: RentalStatus;
  itemName: string;
  quantity: number;
  totalPricePaid: MoneyDto;
  thumbnail: ImageThumbnailDto;
  dateCreated: string;
  rentalDays: number;
  borrowerName: string;
  borrowerEmail: string;
  startingRentalDate: string; 
  endingRentalDate: Date;
}