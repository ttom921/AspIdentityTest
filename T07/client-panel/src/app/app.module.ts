import { ClientService } from './services/client.service';
import { AuthService } from './services/auth.service';
import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { HttpModule} from '@angular/http';
import { HttpClientModule } from '@angular/common/http';

import { FlashMessagesModule } from 'angular2-flash-messages';

import { AppComponent } from './app.component';
import { approuting } from './app.routes';
import { HomeComponent } from './home/home.component';
import { PageNotFoundComponent } from './page-not-found/page-not-found.component';
import { NavbarComponent } from './navbar/navbar.component';
import { LoginCallbackComponent } from './login-callback/login-callback.component';
import { LoginComponent } from './login/login.component';
import { ClientsComponent } from './clients/clients.component';
import { ClaimService } from './services/claim.service';


@NgModule({
  declarations: [
    AppComponent,
    HomeComponent,
    PageNotFoundComponent,
    NavbarComponent,
    LoginCallbackComponent,
    LoginComponent,
    ClientsComponent
  ],
  imports: [
    BrowserModule,
    HttpModule,
    HttpClientModule,
    FlashMessagesModule.forRoot(),
    approuting
  ],
  providers: [
    AuthService,
    ClientService,
    ClaimService],
  bootstrap: [AppComponent]
})
export class AppModule { }
