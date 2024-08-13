import { Component } from '@angular/core';
import { RouterOutlet } from '@angular/router';
import { MenuItem } from 'primeng/api';
import { MenubarModule } from 'primeng/menubar';
import { GlobalRoutePath } from './app.routes';

@Component({
  selector: 'app-root',
  standalone: true,
  imports: [RouterOutlet, MenubarModule],
  templateUrl: './app.component.html',
  styleUrl: './app.component.scss'
})
export class AppComponent {
  title = 'client';

  menuItems: MenuItem[] = [
    {
      label: "Продукты",
      routerLink: this.getRouterLink(GlobalRoutePath.products),
    },
    {
      label: "Категории",
      routerLink: this.getRouterLink(GlobalRoutePath.productCategories),
    },
    {
      label: "Пользователи",
      routerLink: this.getRouterLink(GlobalRoutePath.users),
    },
    {
      label: "Вход",
      routerLink: this.getRouterLink(GlobalRoutePath.signIn),
    },
    {
      label: "Регистрация",
      routerLink: this.getRouterLink(GlobalRoutePath.signUp),
    },
  ];

  getRouterLink(path: string): string {
    return `/${path}`;
  }
}
