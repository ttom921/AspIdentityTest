import { PageNotFoundComponent } from './components/page-not-found/page-not-found.component';
import { HomeComponent } from './home/home.component';
import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { LoginCallbackComponent } from './components/login-callback/login-callback.component';

const routes: Routes = [
    { path: '', component: HomeComponent },
    { path: 'login-callback', component: LoginCallbackComponent },
    { path: '**', component: PageNotFoundComponent }

];

@NgModule({
    imports: [RouterModule.forRoot(routes)],
    exports: [RouterModule],
    providers: []
})

export class AppRoutingModule { }
