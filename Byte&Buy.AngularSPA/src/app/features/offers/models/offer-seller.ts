import { CompanySeller } from "./company-seller";
import { PrivateSeller } from "./private-seller";

export type OfferSeller = 
  | PrivateSeller
  | CompanySeller;