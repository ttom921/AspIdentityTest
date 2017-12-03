
import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { HttpModule} from '@angular/http';
import { HttpClientModule } from '@angular/common/http';

import { AppComponent } from './app.component';
import { NavbarComponent } from './components/navbar/navbar.component';
import { LoginComponent } from './components/login/login.component';
import { AppRoutingModule } from './app-routing.module';
import { PageNotFoundComponent } from './components/page-not-found/page-not-found.component';




import { HomeComponent } from './home/home.component';
import { ClientsComponent } from './components/clients/clients.component';
import { DashboardComponent } from './components/dashboard/dashboard.component';

import { ClientService } from './services/client.service';

import { LoginCallbackComponent } from './components/login-callback/login-callback.component';
import { FlashMessagesModule } from 'angular2-flash-messages';
import { AuthService } from './services/auth.service';



@NgModule({
  declarations: [
    AppComponent,
    DashboardComponent,
    PageNotFoundComponent,
    LoginComponent,
    NavbarComponent,
    HomeComponent,
    LoginCallbackComponent,
    ClientsComponent
  ],
  imports: [
    BrowserModule,
    HttpModule,
    HttpClientModule,
    FlashMessagesModule.forRoot(),
    AppRoutingModule
  ],
  providers: [
    ClientService,
    AuthService
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
