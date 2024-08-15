import { HttpClient } from '@angular/common/http';
import { inject, Injectable } from '@angular/core';
import { environment } from '../../../environments/environment';
import { Observable } from 'rxjs';
import { Permission } from '../../models/permission';
import { BaseApi } from './base-api';

@Injectable({
  providedIn: 'root'
})
export class PermissionsApiService extends BaseApi {
  private http = inject(HttpClient);
  private baseApi = environment.apiUrl;

  get(): Observable<Permission[]> {
    return this.http.get<Permission[]>(this.baseApi + 'users/permissions');
  }
}
