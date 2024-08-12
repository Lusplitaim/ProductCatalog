import { Product } from "./product";
import { ProductCategory } from "./productCategory";

export interface ProductEditorData {
    product: Product;
    categories: ProductCategory[];
}