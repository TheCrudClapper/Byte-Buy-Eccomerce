import { AddressValueObj } from "../address/addressValueObj";

export interface CompanyInfoResponse {
   companyName :string;
   slogan :string;
   TIN :string;
   email :string;
   phone :string;
   address :AddressValueObj;
}