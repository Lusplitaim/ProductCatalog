import { HttpClient } from '@angular/common/http';
import { inject, Injectable } from '@angular/core';
import { BehaviorSubject, map } from 'rxjs';
import { User } from '../models/user';
import { environment } from '../../environments/environment';
import { LoggedUserData } from '../models/loggedUserData';
import { LoginUser } from '../models/loginUser';
import { RegisterUser } from '../models/registerUser';
import { Permission } from '../models/permission';

@Injectable({
  providedIn: 'root'
})
export class AccountService {
  private baseUrl = environment.apiUrl;
  private http = inject(HttpClient);

  private currentUserSubject$ = new BehaviorSubject<User>(this.getCurrentUser());
  readonly currentUser$ = this.currentUserSubject$.asObservable();

  private permissionsSubject$ = new BehaviorSubject<Permission[] | undefined>(this.getPermissions());
  readonly permissions$ = this.permissionsSubject$.asObservable();

  login(model: LoginUser) {
    return this.http.post<LoggedUserData>(this.baseUrl + 'auth/sign-in', model).pipe(
      map((data) => {
        const user = data.user;
        if(user) {
          this.setCurrentUser(data);
          this.currentUserSubject$.next(data.user);
          this.permissionsSubject$.next(data.permissions);
        }
      })
    );
  }

  register(model: RegisterUser) {
    return this.http.post<any>(this.baseUrl + 'auth/sign-up', model).pipe(
      map((data) => {
        const user = data.user;
        if(user) {
          this.setCurrentUser(data);
          this.currentUserSubject$.next(data.user);
          this.permissionsSubject$.next(data.permissions);
        }
      })
    );
  }

  setCurrentUser(data: LoggedUserData) {
    localStorage.setItem('user', JSON.stringify(data.user));
    localStorage.setItem('permissions', JSON.stringify(data.permissions));
    localStorage.setItem('token', data.token);
  }

  logout() {
    localStorage.removeItem('user');
    localStorage.removeItem('token');
  }

  getDecodedToken(token: string) {
    return JSON.parse(atob(token.split('.')[1]))
  }

  getToken(): string {
    return localStorage.getItem('token') ?? '';
  }

  isLoggedIn() {
    return localStorage.getItem('user') !== null;
  }

  getCurrentUser(): User {
    return JSON.parse(localStorage.getItem('user') ?? '{}') as User;
  }

  getPermissions(): Permission[] {
    return JSON.parse(localStorage.getItem('permissions') ?? '[]') as Permission[];
  }
}
