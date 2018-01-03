import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';

import { AppComponent } from './app.component';
import { HomeComponent } from './home/home.component';
import { AuthService } from './common/services/auth.service';
import { RouterModule } from '@angular/router';
import { appRoutes } from './app.routing';
import { SecuredComponent } from './secured/secured.component';

@NgModule({
  declarations: [
    AppComponent,
    HomeComponent,
    SecuredComponent
  ],
  imports: [
    BrowserModule,
    RouterModule.forRoot(appRoutes),
  ],
  providers: [AuthService],
  bootstrap: [AppComponent]
})
export class AppModule { }
