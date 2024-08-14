import { inject, Injectable } from '@angular/core';
import { Permission } from '../models/permission';
import { Observable } from 'rxjs';
import { PermissionsApiService } from './api/permissions-api.service';
import { AppArea } from '../models/appArea';
import { AreaAction } from '../models/areaAction';
import { AccountService } from './account.service';

@Injectable({
  providedIn: 'root'
})
export class PermissionsService {
  private api = inject(PermissionsApiService);
  private accountService = inject(AccountService);

  get(): Observable<Permission[]> {
    return this.api.get();
  }

  getAllowedActionsByArea(area: AppArea): AreaAction[] {
    const permissions = this.accountService.getPermissions();
    return permissions.filter(p => p.area === area).map(p => p.action);
  }
}
