import { Component, inject } from '@angular/core';
import { User } from '../../models/user';
import { DialogService, DynamicDialogConfig, DynamicDialogModule, DynamicDialogRef } from 'primeng/dynamicdialog';
import { UserEditorComponent } from '../user-editor/user-editor.component';
import { DialogEditResult } from '../../models/dialogEditResult';
import { TableModule, TableRowSelectEvent } from 'primeng/table';
import { ButtonModule } from 'primeng/button';
import { UsersService } from '../../services/users.service';
import { PermissionsService } from '../../services/permissions.service';
import { AreaAction } from '../../models/areaAction';
import { AppArea } from '../../models/appArea';

@Component({
  selector: 'app-users',
  standalone: true,
  imports: [TableModule, ButtonModule, DynamicDialogModule],
  providers: [DialogService],
  templateUrl: './users.component.html',
  styleUrl: './users.component.scss'
})
export class UsersComponent {
  private usersService = inject(UsersService);
  private permissionsService = inject(PermissionsService);
  private dialogRef: DynamicDialogRef | undefined;
  private dialogService = inject(DialogService);
  
  users: User[] = [];
  selectedUser: User | undefined;
  canCreate = false;
  canEdit = false;

  ngOnInit(): void {
    const areaActions = this.permissionsService.getAllowedActionsByArea(AppArea.Users);
    this.canCreate = areaActions.some(a => a === AreaAction.Create);
    this.canEdit = areaActions.some(a => a === AreaAction.Update);

    this.usersService.get()
      .subscribe(users => {
        this.users = users;
      });
  }

  openCreationModal() {
    const dialogConfig = this.getDialogConfig<void>();
    dialogConfig.header = 'Создание пользователя';

    this.dialogRef = this.dialogService.open(UserEditorComponent, dialogConfig);

    this.dialogRef.onClose.subscribe((data: DialogEditResult<User>) => {
      if (data) {
        this.users.push(data.result);
      }
    });
  }

  onRowSelect(_: TableRowSelectEvent) {
    if (!this.canEdit) {
      return;
    }

    const dialogConfig = this.getDialogConfig<User>();
    dialogConfig.header = `Пользователь '${this.selectedUser?.userName}'`;
    dialogConfig.data = this.selectedUser;

    this.dialogRef = this.dialogService.open(UserEditorComponent, dialogConfig);

    this.dialogRef.onClose.subscribe((data: DialogEditResult<User>) => {
      if (data) {
        const idx = this.users.findIndex(p => p.id === data.result.id);

        if (data.action === 'update') {
          this.users.splice(idx, 1, data.result);
        } else {
          this.users.splice(idx, 1);
        }
      }
    });
  }

  private getDialogConfig<T>(): DynamicDialogConfig<T> {
    return {
      header: ``,
      width: '20vw',
      modal: true,
      closeOnEscape: true,
      breakpoints: {
          '960px': '75vw',
          '640px': '90vw'
      },
    };
  }
}
