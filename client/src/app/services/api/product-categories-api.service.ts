import { HttpClient } from '@angular/common/http';
import { inject, Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { environment } from '../../../environments/environment';
import { ProductCategory } from '../../models/productCategory';
import { CreateEditProductCategory } from '../../models/createEditProductCategory';
import { BaseApi } from './base-api';

@Injectable({
  providedIn: 'root'
})
export class ProductCategoriesApiService extends BaseApi {
  private http = inject(HttpClient);
  private baseApi = environment.apiUrl;

  get(): Observable<ProductCategory[]> {
    return this.http.get<ProductCategory[]>(this.baseApi + 'productcategories');
  }

  create(model: CreateEditProductCategory): Observable<ProductCategory> {
    return this.http.post<ProductCategory>(this.baseApi + 'productcategories', model);
  }

  update(id: number, model: CreateEditProductCategory): Observable<ProductCategory> {
    return this.http.put<ProductCategory>(this.baseApi + `productcategories/${id}`, model);
  }

  delete(id: number): Observable<void> {
    return this.http.delete<void>(this.baseApi + `productcategories/${id}`);
  }
}
