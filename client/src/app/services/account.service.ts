import { HttpClient } from '@angular/common/http';
import { inject, Injectable } from '@angular/core';
import { map } from 'rxjs';
import { User } from '../models/user';
import { environment } from '../../environments/environment';
import { LoggedUserData } from '../models/loggedUserData';
import { LoginUser } from '../models/loginUser';
import { RegisterUser } from '../models/registerUser';

@Injectable({
  providedIn: 'root'
})
export class AccountService {
  baseUrl = environment.apiUrl;

  http = inject(HttpClient);

  login(model: LoginUser) {
    return this.http.post<LoggedUserData>(this.baseUrl + 'auth/sign-in', model).pipe(
      map((data) => {
        const user = data.user;
        if(user) {
          this.setCurrentUser(data);
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
        }
      })
    );
  }

  setCurrentUser(data: LoggedUserData) {
    localStorage.setItem('user', JSON.stringify(data.user));
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
    return JSON.parse(localStorage.getItem('user') ?? '') as User;
  }
}
