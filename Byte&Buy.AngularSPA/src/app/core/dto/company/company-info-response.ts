import { HomeAddressDto } from "../home-address/home-address-dto";

export interface CompanyInfoResponse {
   companyName :string;
   slogan :string;
   TIN :string;
   email :string;
   phone :string;
   address :HomeAddressDto;
}