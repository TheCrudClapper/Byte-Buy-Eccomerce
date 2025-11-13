import { addressValueObj } from "../address/addressValueObj";

export interface companyInfoResponse{
   companyName :string;
   slogan :string;
   TIN :string;
   email :string;
   phone :string;
   address :addressValueObj;
}