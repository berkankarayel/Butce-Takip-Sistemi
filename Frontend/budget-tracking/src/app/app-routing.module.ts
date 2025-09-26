import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { LoginComponent } from './pages/login/login.component';
import { AdminUserListComponent } from './components/admin-user-list/admin-user-list.component';
const routes: Routes = [
  { path: 'login', component: LoginComponent },
  { path: 'admin/users', component: AdminUserListComponent }, // ✅ admin sayfası
  { path: '', redirectTo: 'login', pathMatch: 'full' }
];


@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule {}

