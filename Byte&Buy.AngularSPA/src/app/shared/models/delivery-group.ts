import { DeliveryOption } from "./delivery-options";

//hold sorted deliveries by carrier and his deliveries
export interface DeliveryGroup{
    carrier: string;
    options: DeliveryOption[]; 
}