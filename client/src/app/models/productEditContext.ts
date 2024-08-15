import { Product } from "./product";
import { ProductCategory } from "./productCategory";

export interface ProductEditContext {
    product?: Product;
    categories: ProductCategory[];
}