import { Routes } from '@angular/router';
import { ProductsComponent } from './components/products/products.component';
import { SignInComponent } from './components/sign-in/sign-in.component';
import { SignUpComponent } from './components/sign-up/sign-up.component';
import { authGuard } from './guards/auth.guard';

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
    signIn = 'sign-in',
    signUp = 'sign-up',
};
