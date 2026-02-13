import { Guid } from "guid-typescript";
import { OfferSortBy } from "../../../../features/offers/models/offer-sort-by";
import { SellerType } from "../../../../shared/models/seller-type";

export interface OfferQueryParams {
  pageNumber: number;
  pageSize: number;
  sortBy?: OfferSortBy;
  categoryIds?: Guid[];
  conditionIds?: Guid[];
  sellerType?: SellerType;
  city?: string;
  searchPhrase?: string;
  minRentalDays?: number;
  maxRentalDays?: number;
  minPrice?: number;
  maxPrice?: number;
}