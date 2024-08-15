import { CustomQueryParams } from "./customQueryParams";

export interface ProductFilters extends CustomQueryParams {
    categories: number[];
    minPrice?: number;
    maxPrice?: number;
}