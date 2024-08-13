import { HttpInterceptorFn } from '@angular/common/http';
import { inject } from '@angular/core';
import { AccountService } from '../services/account.service';

export const jwtInterceptor: HttpInterceptorFn = (req, next) => {
  const accountService = inject(AccountService);
  const token =  localStorage.getItem('token') ?? '';

  if (token) {
    req = req.clone({
      setHeaders: {
        Authorization: "Bearer " + token
      }
    });
  }

  return next(req);
};
