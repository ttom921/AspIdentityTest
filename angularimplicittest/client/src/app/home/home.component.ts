import { Component, OnInit } from '@angular/core';
import { AuthService } from '../common/services/auth.service';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.css']
})
export class HomeComponent implements OnInit {
  loggedIn: Boolean;
  
  constructor( private authService: AuthService ) { 
    this.authService.isLoggedInObs()
      .subscribe(flag => {
        this.loggedIn = flag;
       
      });
  }

  ngOnInit() {
  }
  login() {
    this.authService.startSigninMainWindow();
  }
}
