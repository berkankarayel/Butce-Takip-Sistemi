import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';

const routes: Routes = [
  // auth altında authmodule yüklenecek
  {path:'auth',loadChildren:() =>import('./features/auth/auth.module').then(m=>m.AuthModule)},
  { path: 'admin', loadChildren: () => import('./features/admin/admin.module').then(m => m.AdminModule) },
   // varsayılan yönlendirme: /auth/login
  { path: '', redirectTo: 'auth/login', pathMatch: 'full' },
    // olmayan tüm yollar login'e dönsün
  { path: '**', redirectTo: 'auth/login' }
];


@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
