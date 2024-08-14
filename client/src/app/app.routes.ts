import { Routes } from '@angular/router';
import { ProductsComponent } from './components/products/products.component';
import { SignInComponent } from './components/sign-in/sign-in.component';
import { SignUpComponent } from './components/sign-up/sign-up.component';
import { authGuard } from './guards/auth.guard';
import { ProductCategoriesComponent } from './components/product-categories/product-categories.component';
import { UsersComponent } from './components/users/users.component';
import { AppArea } from './models/appArea';

export const routes: Routes = [
    {
        path: '',
        redirectTo: GlobalRoutePath.Products,
        pathMatch: 'full',
    },
    {
        path: '',
        canActivateChild: [authGuard],
        children: [
            {
                path: GlobalRoutePath.Products,
                data: { area: AppArea.Products },
                component: ProductsComponent,
            },
            {
                path: GlobalRoutePath.ProductCategories,
                data: { area: AppArea.ProductCategories },
                component: ProductCategoriesComponent,
            },
            {
                path: GlobalRoutePath.Users,
                data: { area: AppArea.Users },
                component: UsersComponent,
            },
        ],
    },
    {
        path: GlobalRoutePath.SignIn,
        component: SignInComponent,
    },
    {
        path: GlobalRoutePath.SignUp,
        component: SignUpComponent,
    },
];

export const enum GlobalRoutePath {
    Products = 'products',
    ProductCategories = 'product-categories',
    Users = 'users',
    SignIn = 'sign-in',
    SignUp = 'sign-up',
};
