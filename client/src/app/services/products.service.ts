import { inject, Injectable } from '@angular/core';
import { ProductsApiService } from './api/products-api.service';
import { Observable } from 'rxjs';
import { Product } from '../models/product';
import { CreateEditProduct } from '../models/createEditProduct';

@Injectable({
  providedIn: 'root'
})
export class ProductsService {
  private productsApi = inject(ProductsApiService);

  get(): Observable<Product[]> {
    return this.productsApi.get();
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
