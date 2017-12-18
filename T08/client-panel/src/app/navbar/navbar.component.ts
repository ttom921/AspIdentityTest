import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { AuthService } from '../services/auth.service';
import 'rxjs/add/operator/map';
import { User } from 'oidc-client';
import { FlashMessagesService } from 'angular2-flash-messages';

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
    private flashMessagesService: FlashMessagesService
  ) { }

  ngOnInit() {
    this.authService.loginStatusChanged.subscribe((user: User) => {
      this.loggedInUser = user;
      this.isLoggedIn = !!user;
      if (user) {
        this.flashMessagesService.show('登陆成功', { cssClass: 'alert alert-success', timeout: 4000 });
      }
    });
    this.authService.checkUser();
  }

  login() {
    this.authService.login();
  }

  logout() {
    this.authService.logout();
  }

}
