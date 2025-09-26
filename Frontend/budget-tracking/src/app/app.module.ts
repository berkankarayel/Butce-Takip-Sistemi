import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { FormsModule } from '@angular/forms';
import { HttpClientModule } from '@angular/common/http'; // ✅ eklendi

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { LoginComponent } from './pages/login/login.component';
import { AdminUserListComponent } from './components/admin-user-list/admin-user-list.component';

@NgModule({
  declarations: [
    AppComponent,
    LoginComponent,
    AdminUserListComponent
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    FormsModule,
    HttpClientModule  // ✅ eklendi
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
