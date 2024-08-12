import { HttpClient } from '@angular/common/http';
import { inject, Injectable } from '@angular/core';
import { environment } from '../../../environments/environment';
import { Observable } from 'rxjs';
import { Product } from '../../models/product';
import { CreateEditProduct } from '../../models/createEditProduct';

@Injectable({
  providedIn: 'root'
})
export class ProductsApiService {
  private http = inject(HttpClient);
  private baseApi = environment.apiUrl;

  get(): Observable<Product[]> {
    return this.http.get<Product[]>(this.baseApi + 'products');
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
