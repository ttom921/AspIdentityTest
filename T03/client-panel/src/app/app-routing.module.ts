import { HomeComponent } from './home/home.component';
import { PageNotFoundComponent } from './components/page-not-found/page-not-found.component';
import { AppComponent } from './app.component';
import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { LoginComponent } from './components/login/login.component';
import { BrowserModule } from '@angular/platform-browser/src/browser';
import { DashboardComponent } from './components/dashboard/dashboard.component';
import { LoginCallbackComponent } from './components/login-callback/login-callback.component';



const routes: Routes=[
    { path:'',component: HomeComponent,pathMatch: 'full'},
    { path:'dashboard',component: DashboardComponent,pathMatch: 'full'},
    { path:'login-callback',component: LoginCallbackComponent,pathMatch: 'full'},
    { path:'login',component: LoginComponent,pathMatch: 'full'},
    { path:'**',component:PageNotFoundComponent,pathMatch: 'full'}
];
@NgModule({
    imports: [RouterModule.forRoot(routes)],
    exports:[RouterModule],
    providers:[]
})
export class AppRoutingModule{}