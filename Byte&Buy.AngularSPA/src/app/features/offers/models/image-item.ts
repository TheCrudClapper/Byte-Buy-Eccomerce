import { Guid } from "guid-typescript";

export interface ImageItem {
    id?: Guid;
    file?: File;
    preview?: string;
    alt?: string;
    isDeleted?: boolean;
    isNew?: boolean;
}