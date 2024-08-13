import { inject, Injectable } from '@angular/core';
import { ProductCategoriesApiService } from './api/product-categories-api.service';
import { Observable } from 'rxjs';
import { ProductCategory } from '../models/productCategory';
import { CreateEditProductCategory } from '../models/createEditProductCategory';

@Injectable({
  providedIn: 'root'
})
export class ProductCategoriesService {
  private productCategoriesApi = inject(ProductCategoriesApiService);

  get(): Observable<ProductCategory[]> {
    return this.productCategoriesApi.get();
  }

  create(model: CreateEditProductCategory): Observable<ProductCategory> {
    return this.productCategoriesApi.create(model);
  }

  update(id: number, model: CreateEditProductCategory): Observable<ProductCategory> {
    return this.productCategoriesApi.update(id, model);
  }

  delete(id: number): Observable<void> {
    return this.productCategoriesApi.delete(id);
  }
}
