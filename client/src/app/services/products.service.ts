import { inject, Injectable } from '@angular/core';
import { ProductsApiService } from './api/products-api.service';
import { Observable } from 'rxjs';
import { Product } from '../models/product';
import { CreateEditProduct } from '../models/createEditProduct';
import { ProductEditContext } from '../models/productEditContext';
import { ProductFiltersContext } from '../models/productFiltersContext';
import { ProductFilters } from '../models/productFilters';

@Injectable({
  providedIn: 'root'
})
export class ProductsService {
  private productsApi = inject(ProductsApiService);

  get(filters: ProductFilters | null = null): Observable<Product[]> {
    return this.productsApi.get(filters);
  }

  getEditContext(productId: number | undefined): Observable<ProductEditContext> {
    return this.productsApi.getEditContext(productId);
  }

  getFiltersContext(): Observable<ProductFiltersContext> {
    return this.productsApi.getFiltersContext();
  }

  create(model: CreateEditProduct): Observable<Product> {
    return this.productsApi.create(model);
  }

  edit(id: number, model: CreateEditProduct): Observable<Product> {
    return this.productsApi.edit(id, model);
  }

  delete(id: number): Observable<void> {
    return this.productsApi.delete(id);
  }
}
