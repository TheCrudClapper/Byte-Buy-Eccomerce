import { PaginationMetadata } from "./pagination-metadata";

export interface PagedList<T>{
    items: T[],
    metadata: PaginationMetadata
}