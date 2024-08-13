import { inject, Injectable } from '@angular/core';
import { User } from '../models/user';
import { Observable } from 'rxjs';
import { UsersApiService } from './api/users-api.service';
import { CreateUser } from '../models/createUser';
import { EditUser } from '../models/editUser';

@Injectable({
  providedIn: 'root'
})
export class UsersService {
  private api = inject(UsersApiService);

  get(): Observable<User[]> {
    return this.api.get();
  }

  create(model: CreateUser): Observable<User> {
    return this.api.create(model);
  }

  edit(id: number, model: EditUser): Observable<User> {
    return this.api.edit(id, model);
  }

  delete(id: number): Observable<void> {
    return this.api.delete(id);
  }
}
