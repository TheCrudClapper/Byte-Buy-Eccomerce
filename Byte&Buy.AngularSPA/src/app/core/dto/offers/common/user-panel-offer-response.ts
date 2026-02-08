import { Guid } from "guid-typescript";
import { ImageResponse } from "../../image/image-response";
import { OfferStatus } from "../enum/offer-status";

export interface UserPanelOfferResponse{
    id: Guid;
    title: string;
    image: ImageResponse;
    dateCreated: Date;
    dateEdited?: Date;
    status: OfferStatus;
    quantityAvaliable: number;
    type: 'sale' | 'rent';
}