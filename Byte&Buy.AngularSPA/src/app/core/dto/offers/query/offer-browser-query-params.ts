export interface OfferQueryParams {
  pageNumber: number;
  pageSize: number;
  sortBy?: string;
  categories?: string[];
  conditions?: string[];
  offerTypes?: ('sale' | 'rent')[];
  sellerType?: ('company' | 'private')[];
  city?: string;
  minPrice?: number;
  maxPrice?: number;
}