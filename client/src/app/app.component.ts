import { Component, inject, OnInit } from '@angular/core';
import { RouterOutlet } from '@angular/router';
import { MenuItem } from 'primeng/api';
import { MenubarModule } from 'primeng/menubar';
import { GlobalRoutePath } from './app.routes';
import { AccountService } from './services/account.service';
import { AppArea } from './models/appArea';
import { AreaAction } from './models/areaAction';

@Component({
  selector: 'app-root',
  standalone: true,
  imports: [RouterOutlet, MenubarModule],
  templateUrl: './app.component.html',
  styleUrl: './app.component.scss'
})
export class AppComponent implements OnInit {
  private accountService = inject(AccountService);

  title = 'client';
  menuItems: MenuItem[] = [];

  ngOnInit(): void {
    this.accountService.permissions$
      .subscribe(permissions => {
        this.menuItems = [];

        if (!permissions) {
          this.menuItems = [
            {
              label: "Вход",
              routerLink: this.getRouterLink(GlobalRoutePath.signIn),
            },
            {
              label: "Регистрация",
              routerLink: this.getRouterLink(GlobalRoutePath.signUp),
            },
          ];
        }

        if (permissions.some(p => p.area === AppArea.products && p.action === AreaAction.read)) {
          this.menuItems.push({
            label: "Продукты",
            routerLink: this.getRouterLink(GlobalRoutePath.products),
          });
        }

        if (permissions.some(p => p.area === AppArea.productCategories && p.action === AreaAction.read)) {
          this.menuItems.push(          {
            label: "Категории",
            routerLink: this.getRouterLink(GlobalRoutePath.productCategories),
          });
        }

        if (permissions.some(p => p.area === AppArea.users && p.action === AreaAction.read)) {
          this.menuItems.push({
            label: "Пользователи",
            routerLink: this.getRouterLink(GlobalRoutePath.users),
          });
        }
      });
  }

  private getRouterLink(path: string): string {
    return `/${path}`;
  }
}
