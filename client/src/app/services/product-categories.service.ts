import { inject, Injectable } from '@angular/core';
import { ProductCategoriesApiService } from './api/product-categories-api.service';
import { Observable } from 'rxjs';
import { ProductCategory } from '../models/productCategory';

@Injectable({
  providedIn: 'root'
})
export class ProductCategoriesService {
  productCategoriesApi = inject(ProductCategoriesApiService);

  get(): Observable<ProductCategory[]> {
    return this.productCategoriesApi.get();
  }
}
