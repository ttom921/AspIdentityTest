import { Routes, RouterModule } from '@angular/router';
import { HomeComponent } from './home/home.component';
import { SecuredComponent } from './secured/secured.component';

export const appRoutes: Routes = [
    { path: '', redirectTo: '/home', pathMatch: 'full' },
    { path: 'home', component: HomeComponent },
    { path: 'secured', component: SecuredComponent }

];