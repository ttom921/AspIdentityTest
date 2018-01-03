import { Component, OnInit } from '@angular/core';
import { AuthService } from '../common/services/auth.service';

@Component({
  selector: 'app-secured',
  templateUrl: './secured.component.html',
  styleUrls: ['./secured.component.css']
})
export class SecuredComponent implements OnInit {
  loggedIn: Boolean;
  constructor(private authService: AuthService) { 
    this.authService.isLoggedInObs()
    .subscribe(flag => {
      this.loggedIn = flag;
     
    });
  }


  ngOnInit() {
  }
  logout() {
    this.authService.startSignoutMainWindow();
  }
}
