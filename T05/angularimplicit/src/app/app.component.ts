import { AuthService } from './common/services/auth.service';
import { Component } from '@angular/core';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent {
  title = 'app';
  loggedIn: Boolean;

  constructor(private authService: AuthService) {
    // this.authService.isLoggedInObs().subscribe(flag => {
    //   this.loggedIn = flag;
    //   if (!flag) {
    //     this.authService.startSigninMainWing ndow();
    //   }
    // });
  }

  login() {
    this.authService.startSigninMainWindow();
  }

  logout() {
    this.authService.startSignoutMainWindow();
  }
}
