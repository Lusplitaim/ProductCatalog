import { HttpClient } from '@angular/common/http';
import { inject, Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { environment } from '../../../environments/environment';
import { ProductCategory } from '../../models/productCategory';

@Injectable({
  providedIn: 'root'
})
export class ProductCategoriesApiService {
  private http = inject(HttpClient);
  private baseApi = environment.apiUrl;

  get(): Observable<ProductCategory[]> {
    return this.http.get<ProductCategory[]>(this.baseApi + 'productcategories');
  }
}
