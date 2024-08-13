import { inject, Injectable } from '@angular/core';
import { environment } from '../../../environments/environment';
import { HttpClient } from '@angular/common/http';
import { User } from '../../models/user';
import { Observable } from 'rxjs';
import { CreateUser } from '../../models/createUser';
import { EditUser } from '../../models/editUser';

@Injectable({
  providedIn: 'root'
})
export class UsersApiService {
  private http = inject(HttpClient);
  private baseApi = environment.apiUrl;

  get(): Observable<User[]> {
    return this.http.get<User[]>(this.baseApi + 'users');
  }

  create(model: CreateUser): Observable<User> {
    return this.http.post<User>(this.baseApi + 'users', model);
  }

  edit(id: number, model: EditUser): Observable<User> {
    return this.http.put<User>(this.baseApi + `users/${id}`, model);
  }

  delete(id: number): Observable<void> {
    return this.http.delete<void>(this.baseApi + `users/${id}`);
  }
}
