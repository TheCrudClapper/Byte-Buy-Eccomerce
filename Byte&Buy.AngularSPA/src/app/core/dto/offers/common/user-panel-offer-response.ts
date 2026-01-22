import { Guid } from "guid-typescript";
import { ImageResponse } from "../../image/image-response";

export interface UserPanelOfferResponse{
    id: Guid;
    title: string;
    image: ImageResponse;
    dateCreated: Date;
    dateEdited?: Date;
    quantityAvaliable: number;
    type: 'sale' | 'rent';
}