import { Component, inject } from '@angular/core';
import { User } from '../../models/user';
import { DynamicDialogConfig, DynamicDialogRef } from 'primeng/dynamicdialog';
import { FormBuilder, FormControl, FormsModule, ReactiveFormsModule, Validators } from '@angular/forms';
import { UsersService } from '../../services/users.service';
import { UserRole } from '../../models/userRole';
import { CreateUser } from '../../models/createUser';
import { EditUser } from '../../models/editUser';
import { DialogEditAction, DialogEditResult } from '../../models/dialogEditResult';
import { ButtonModule } from 'primeng/button';
import { CardModule } from 'primeng/card';
import { DropdownModule } from 'primeng/dropdown';
import { FloatLabelModule } from 'primeng/floatlabel';
import { InputTextModule } from 'primeng/inputtext';
import { InputTextareaModule } from 'primeng/inputtextarea';
import { MultiSelectModule } from 'primeng/multiselect';
import { PermissionsService } from '../../services/permissions.service';
import { AppArea } from '../../models/appArea';
import { AreaAction } from '../../models/areaAction';

@Component({
  selector: 'app-user-editor',
  standalone: true,
  imports: [
    InputTextModule,
    FormsModule,
    ReactiveFormsModule,
    FloatLabelModule,
    CardModule,
    ButtonModule,
    InputTextareaModule,
    DropdownModule,
    MultiSelectModule,
  ],
  templateUrl: './user-editor.component.html',
  styleUrl: './user-editor.component.scss'
})
export class UserEditorComponent {
  private formBuilder = inject(FormBuilder);
  private usersService = inject(UsersService);
  private permissionsService = inject(PermissionsService);
  private dialogRef = inject(DynamicDialogRef);
  private dialogConfig = inject(DynamicDialogConfig) as DynamicDialogConfig<User>;

  editMode = false;
  user: User | undefined = undefined;
  editForm = this.formBuilder.group({
    userName: new FormControl<string>('', [Validators.required]),
    email: new FormControl<string>('', [Validators.required]),
    password: new FormControl<string>('', [Validators.required]),
    roles: new FormControl<string[]>([], [Validators.required]),
  });
  roles: { id: number, name: string }[] = [];
  roleNames: string[] = [];
  canDelete = false;

  ngOnInit(): void {
    const areaActions = this.permissionsService.getAllowedActionsByArea(AppArea.Users);
    this.roles = Object.keys(UserRole).filter(k => Number(k)).map(k => {
      const key = Number(k);
      return {id: key, name: UserRole[key]} as { id: number, name: string };
    });
    this.roleNames = this.roles.map(el => el.name);
    const user = this.dialogConfig.data;

    if (user) {
      this.editMode = true;
      this.canDelete = this.editForm && areaActions.some(a => a === AreaAction.Delete);
      this.user = this.dialogConfig.data;

      this.editForm.setValue({
        userName: user.userName,
        email: user.email,
        password: '',
        roles: user.roles.map(el => UserRole[el]),
      });
      this.editForm.controls.email.disable();
    }
  }
  
  save() {
    const userName = this.editForm.get("userName")!.value as string;
    const email = this.editForm.get("email")!.value as string;
    const password = this.editForm.get("password")!.value as string;
    const userRoles = this.editForm.get("roles")!.value!.map(el => this.roles.find(ur => ur.name === el)!.id) as UserRole[];

    if (this.editMode) {
      const model: EditUser = {
        userName: userName,
        updatedPassword: password ? password : undefined,
        addedRoles: userRoles.filter(el => !this.user!.roles.includes(el)),
        removedRoles: this.user!.roles.filter(el => !userRoles.includes(el)),
      };
      this.usersService.edit(this.user!.id, model)
        .subscribe(user => {
          this.dialogRef.close(this.getDialogResult(user, 'update'));
        });
    } else {
      const model: CreateUser = { userName, email, password, userRoles, };
      this.usersService.create(model)
        .subscribe(user => {
          this.dialogRef.close(this.getDialogResult(user, 'create'));
        });
    }
  }

  delete() {
    if (this.editMode) {
      this.usersService.delete(this.user!.id)
        .subscribe(_ => {
          this.dialogRef.close(this.getDialogResult(this.user!, 'delete'));
        });
    }
  }

  private getDialogResult(product: User, action: DialogEditAction): DialogEditResult<User> {
    return {
      result: product,
      action: action
    };
  }
}
