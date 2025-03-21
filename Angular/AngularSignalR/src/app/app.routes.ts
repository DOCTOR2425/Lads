import { Routes } from '@angular/router';

// export const routes: Routes = [
//   {
//     path: '',
//     component: HeaderComponent,
//     children: [
//       { path: '', redirectTo: 'home', pathMatch: 'full' },
//       { path: 'home', component: HomeComponent },
//       {
//         path: 'payment',
//         component: PaymentPageComponent,
//         canActivate: [canActivateGuard],
//       },
//       {
//         path: 'card-page',
//         component: CardPageComponent,
//         canDeactivate: [CanDeactivateGuard],
//       },
//       {
//         path: 'about',
//         component: AboutPageComponent,
//         canActivateChild: [canActivateChildGuard],
//         children: [
//           {
//             path: 'home',
//             loadComponent: () =>
//               import('./components/home/home.component').then(
//                 (m) => m.HomeComponent
//               ),
//             canMatch: [canMatchGuard],
//           },
//           {
//             path: 'payment',
//             loadComponent: () =>
//               import('./components/payment-page/payment-page.component').then(
//                 (m) => m.PaymentPageComponent
//               ),
//           },
//           {
//             path: 'card-page',
//             loadComponent: () =>
//               import('./components/card-page/card-page.component').then(
//                 (m) => m.CardPageComponent
//               ),
//           },
//         ],
//       },
//     ],
//   },
//   { path: '**', component: NotFoundComponent },
// ];
