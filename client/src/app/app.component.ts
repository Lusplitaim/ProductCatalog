import { Component, inject, OnInit } from '@angular/core';
import { RouterOutlet } from '@angular/router';
import { MenuItem } from 'primeng/api';
import { MenubarModule } from 'primeng/menubar';
import { GlobalRoutePath } from './app.routes';
import { AccountService } from './services/account.service';
import { AppArea } from './models/appArea';
import { AreaAction } from './models/areaAction';
import { Permission } from './models/permission';

@Component({
  selector: 'app-root',
  standalone: true,
  imports: [RouterOutlet, MenubarModule],
  templateUrl: './app.component.html',
  styleUrl: './app.component.scss'
})
export class AppComponent implements OnInit {
  private accountService = inject(AccountService);

  menuItems: MenuItem[] = [];

  ngOnInit(): void {
    this.accountService.permissions$
      .subscribe(permissions => {
        this.updateMenuItems(permissions);
      });
  }

  private updateMenuItems(permissions: Permission[] | undefined) {
    this.menuItems = [];

    if (!permissions) {
      this.menuItems = [
        {
          label: "Вход",
          routerLink: this.getRouterLink(GlobalRoutePath.SignIn),
        },
        {
          label: "Регистрация",
          routerLink: this.getRouterLink(GlobalRoutePath.SignUp),
        },
      ];
      return;
    }

    if (permissions.some(p => p.area === AppArea.Products && p.action === AreaAction.Read)) {
      this.menuItems.push({
        label: "Продукты",
        routerLink: this.getRouterLink(GlobalRoutePath.Products),
      });
    }

    if (permissions.some(p => p.area === AppArea.ProductCategories && p.action === AreaAction.Read)) {
      this.menuItems.push(          {
        label: "Категории",
        routerLink: this.getRouterLink(GlobalRoutePath.ProductCategories),
      });
    }

    if (permissions.some(p => p.area === AppArea.Users && p.action === AreaAction.Read)) {
      this.menuItems.push({
        label: "Пользователи",
        routerLink: this.getRouterLink(GlobalRoutePath.Users),
      });
    }
  }

  private getRouterLink(path: string): string {
    return `/${path}`;
  }
}
