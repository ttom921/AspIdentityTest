import { HomeComponent } from './home/home.component';
import { Routes, RouterModule } from '@angular/router';
import { SecuredComponent } from './secured/secured/secured.component';
import { UnsecuredComponent } from './unsecured/unsecured/unsecured.component';
import { AuthService } from './common/services/auth.service';

export const appRoutes: Routes = [
    { path: '', redirectTo: '/home', pathMatch: 'full' },
    { path: 'home', component: HomeComponent},
    { path: 'secured', component: SecuredComponent },
    { path: 'unsecured', component: UnsecuredComponent }
];
export const authProviders = [
    AuthService
];
