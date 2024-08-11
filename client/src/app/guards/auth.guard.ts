import { inject } from '@angular/core';
import { CanActivateFn, Router } from '@angular/router';
import { AccountService } from '../services/account.service';
import { GlobalRoutePath } from '../app.routes';

export const authGuard: CanActivateFn = (route, state) => {
  const accountService = inject(AccountService);
  const router = inject(Router);

  const isLoggedIn = accountService.isLoggedIn();
  if (!isLoggedIn) {
    router.navigate([GlobalRoutePath.signIn]);
  }
  return isLoggedIn;
};
