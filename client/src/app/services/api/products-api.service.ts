import { HttpClient, HttpParams } from '@angular/common/http';
import { inject, Injectable } from '@angular/core';
import { environment } from '../../../environments/environment';
import { Observable } from 'rxjs';
import { Product } from '../../models/product';
import { CreateEditProduct } from '../../models/createEditProduct';
import { ProductEditContext } from '../../models/productEditContext';
import { ProductFiltersContext } from '../../models/productFiltersContext';
import { BaseApi } from './base-api';
import { ProductFilters } from '../../models/productFilters';

@Injectable({
  providedIn: 'root'
})
export class ProductsApiService extends BaseApi {
  private http = inject(HttpClient);
  private baseApi = environment.apiUrl;

  get(filters: ProductFilters | null): Observable<Product[]> {
    let params = new HttpParams();
    if (filters) {
      params = this.addQueryParams(filters);
    }
    return this.http.get<Product[]>(this.baseApi + 'products', { params });
  }

  getEditContext(productId: number | undefined): Observable<ProductEditContext> {
    let params = new HttpParams();
    if (productId) {
      params = params.append('id', productId);
    }
    return this.http.get<ProductEditContext>(this.baseApi + 'product/editor', { params });
  }

  getFiltersContext(): Observable<ProductFiltersContext> {
    return this.http.get<ProductEditContext>(this.baseApi + 'product/filters');
  }

  create(model: CreateEditProduct): Observable<Product> {
    return this.http.post<Product>(this.baseApi + 'products', model);
  }

  edit(id: number, model: CreateEditProduct): Observable<Product> {
    return this.http.put<Product>(this.baseApi + `products/${id}`, model);
  }

  delete(id: number): Observable<void> {
    return this.http.delete<void>(this.baseApi + `products/${id}`);
  }
}
