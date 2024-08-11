import { HttpInterceptorFn } from '@angular/common/http';
import { inject } from '@angular/core';
import { Router } from '@angular/router';
import { catchError } from 'rxjs';
import { AccountService } from '../services/account.service';

export const errorsInterceptor: HttpInterceptorFn = (req, next) => {
  //const toastService = inject(ToastService);
  const accountService = inject(AccountService);
  const router = inject(Router);

  return next(req).pipe(
    catchError(error => {
      if (error) {
        switch (error.status) {
          case 400:
            const errMessage = error.error.errors[0].message;
            //toastService.showDanger("Error", errMessage ?? "Unknown validation error");
            break;
          case 401:
            //toastService.showDanger("Error", "Unauthorized");
            accountService.logout();
            router.navigate(["login"]);
            break;
          case 403:
            //toastService.showDanger("Error", "Forbidden");
            break;
        }
      }

      throw error;
    })
  );
};
