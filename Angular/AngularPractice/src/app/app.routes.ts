import { Routes } from '@angular/router';
import { HeaderComponent } from './components/header/header.component';
import { HomeComponent } from './components/home/home.component';
import { NotFoundComponent } from './components/not-found/not-found.component';
import { PaymentPageComponent } from './components/payment-page/payment-page.component';
import { CardPageComponent } from './components/card-page/card-page.component';
import { CanDeactivateGuard } from './guards/can-diactivate/can-deactivate.guard';
import { canActivateGuard } from './guards/can-activate/can-activate.guard';
import { AboutPageComponent } from './components/about-page/about-page.component';
import { canActivateChildGuard } from './guards/can-activate-child/can-activate-child.guard';
import { canMatchGuard } from './guards/can-matcth/can-match.guard';

export const routes: Routes = [
  {
    path: '',
    component: HeaderComponent,
    children: [
      { path: '', redirectTo: 'home', pathMatch: 'full' },
      { path: 'home', component: HomeComponent },
      {
        path: 'payment',
        component: PaymentPageComponent,
        canActivate: [canActivateGuard],
      },
      {
        path: 'card-page',
        component: CardPageComponent,
        canDeactivate: [CanDeactivateGuard],
      },
      {
        path: 'about',
        component: AboutPageComponent,
        canActivateChild: [canActivateChildGuard],
        children: [
          {
            path: 'home',
            loadComponent: () =>
              import('./components/home/home.component').then(
                (m) => m.HomeComponent
              ),
            canMatch: [canMatchGuard],
          },
          {
            path: 'payment',
            loadComponent: () =>
              import('./components/payment-page/payment-page.component').then(
                (m) => m.PaymentPageComponent
              ),
          },
          {
            path: 'card-page',
            loadComponent: () =>
              import('./components/card-page/card-page.component').then(
                (m) => m.CardPageComponent
              ),
          },
        ],
      },
    ],
  },
  { path: '**', component: NotFoundComponent },
];
