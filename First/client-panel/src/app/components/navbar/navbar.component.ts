import { Component, OnInit } from '@angular/core';
import { User } from 'oidc-client'
import { AuthService } from '../../services/auth.service';
import { FlashMessagesService } from 'angular2-flash-messages/module/flash-messages.service';
import { Router } from '@angular/router';

@Component({
  selector: 'app-navbar',
  templateUrl: './navbar.component.html',
  styleUrls: ['./navbar.component.css']
})
export class NavbarComponent implements OnInit {

  public isLoggedIn: boolean;
  public loggedInUser: User;

  constructor(
    private authService: AuthService,
    private router: Router,
    private flashMessageService: FlashMessagesService
  ) { }

  ngOnInit() {
    this.authService.loginStatusChanged.subscribe((user: User) => {
      this.loggedInUser = user;
      this.isLoggedIn = !!user;
      if (user) {
        this.flashMessageService.show('登錄成', { cssClass: 'alert alert-success', timeout: 4000 });
      }
      this.authService.chekkUser();
    });
  }

  login(){
    this.authService.login();
  }
  logout(){
    this.authService.logout();
  }
}
