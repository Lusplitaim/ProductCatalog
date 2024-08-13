import { Routes } from '@angular/router';
import { ProductsComponent } from './components/products/products.component';
import { SignInComponent } from './components/sign-in/sign-in.component';
import { SignUpComponent } from './components/sign-up/sign-up.component';
import { authGuard } from './guards/auth.guard';
import { ProductCategoriesComponent } from './components/product-categories/product-categories.component';
import { UsersComponent } from './components/users/users.component';

export const routes: Routes = [
    {
        path: '',
        redirectTo: GlobalRoutePath.products,
        pathMatch: 'full',
    },
    {
        path: '',
        canActivate: [authGuard],
        children: [
            {
                path: GlobalRoutePath.products,
                component: ProductsComponent,
            },
            {
                path: GlobalRoutePath.productCategories,
                component: ProductCategoriesComponent,
            },
            {
                path: GlobalRoutePath.users,
                component: UsersComponent,
            },
        ],
    },
    {
        path: GlobalRoutePath.signIn,
        component: SignInComponent,
    },
    {
        path: GlobalRoutePath.signUp,
        component: SignUpComponent,
    },
];

export const enum GlobalRoutePath {
    products = 'products',
    productCategories = 'product-categories',
    users = 'users',
    signIn = 'sign-in',
    signUp = 'sign-up',
};
