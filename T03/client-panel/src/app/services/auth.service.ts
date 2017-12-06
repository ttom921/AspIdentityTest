import { Injectable, OnInit, EventEmitter } from '@angular/core';
import { Observable } from 'rxjs/Observable';
import { User, UserManager, Log, WebStorageStateStore } from 'oidc-client';
import 'rxjs/add/observable/fromPromise';
import { Router } from '@angular/router';

const config: any = {
  authority: 'http://localhost:5000',
  client_id: 'sales',
  redirect_uri: 'http://localhost:4200/login-callback',
  response_type: 'id_token token',
  scope: 'openid profile email salesapi',
  post_logout_redirect_uri: 'http://localhost:4200',
  silent_redirect_uri: 'http://localhost:4200/silent-renew.html',
  automaticSilentRenew: true,
  accessTokenExpiringNotificationTime: 4,
  userStore: new WebStorageStateStore({ store: window.localStorage })
};

Log.logger = console;
Log.level = Log.DEBUG;


@Injectable()
export class AuthService implements OnInit {

  private manager: UserManager = new UserManager(config);
  public loginStatusChanged: EventEmitter<User> = new EventEmitter();

  ngOnInit(): void {

  }

  constructor(
    private router: Router) {
    this.manager.events.addAccessTokenExpired(() => {
      this.login();
    });
  }

  login() {
    this.manager.signinRedirect();
  }

  loginCallBack() {
    return Observable.create(observer => {
      Observable.fromPromise(this.manager.signinRedirectCallback())
        .subscribe((user: User) => {
          this.loginStatusChanged.emit(user);
          observer.next(user);
          observer.complete();
        });
    });
  }

  checkUser() {
    this.tryGetUser().subscribe((user: User) => {
      this.loginStatusChanged.emit(user);
    }, e => {
      this.loginStatusChanged.emit(null);
    });
  }

  private tryGetUser() {
    return Observable.fromPromise(this.manager.getUser());
  }

  logout() {
    this.manager.signoutRedirect();
  }
}
