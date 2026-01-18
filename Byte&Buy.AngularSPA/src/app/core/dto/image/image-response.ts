import { Guid } from "guid-typescript";

export interface ImageResponse{
    id: Guid;
    imagePath: string;
    altText: string;
}