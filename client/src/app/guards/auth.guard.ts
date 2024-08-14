import { inject } from '@angular/core';
import { CanActivateFn, Router } from '@angular/router';
import { AccountService } from '../services/account.service';
import { GlobalRoutePath } from '../app.routes';
import { AppArea } from '../models/appArea';
import { PermissionsService } from '../services/permissions.service';
import { AreaAction } from '../models/areaAction';

export const authGuard: CanActivateFn = (route, state) => {
  const accountService = inject(AccountService);
  const permissionsService = inject(PermissionsService);
  const router = inject(Router);

  const isLoggedIn = accountService.isLoggedIn();
  if (!isLoggedIn) {
    router.navigate([GlobalRoutePath.SignIn]);
    return isLoggedIn;
  }

  const area = route.data['area'] as AppArea;
  const allowedAreaActions = permissionsService.getAllowedActionsByArea(area);

  return allowedAreaActions.some(a => a === AreaAction.Read);
};
