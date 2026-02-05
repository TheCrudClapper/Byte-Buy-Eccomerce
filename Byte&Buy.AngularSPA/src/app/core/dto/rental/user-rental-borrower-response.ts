import { Guid } from "guid-typescript";
import { MoneyDto } from "../common/money-dto";
import { ImageThumbnailDto } from "../image/image-thumbnail";
import { RentalStatus } from "./enum/rental-status";

export interface UserRentalBorrowerResponse {
  id: Guid;
  status: RentalStatus;
  itemName: string;
  quantity: number;
  totalPricePaid: MoneyDto;
  thumbnail: ImageThumbnailDto;
  dateCreated: string;
  rentalDays: number;
  lenderName: string;
  startingRentalDate: Date; 
  endingRentalDate: Date;  
}