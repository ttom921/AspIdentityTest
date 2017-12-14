import { environment } from './../../environments/environment.prod';
import { Injectable, OnInit, EventEmitter } from '@angular/core';
import { Observable } from 'rxjs/Observable';
import { User, UserManager, Log, WebStorageStateStore } from 'oidc-client';
import 'rxjs/add/observable/fromPromise';
import { Router } from '@angular/router';


Log.logger = console;
Log.level = Log.DEBUG;

@Injectable()
export class AuthService {

  private manager: UserManager = new UserManager(environment.authConfig);
  public loginStatusChanged: EventEmitter<User> = new EventEmitter();
  private userKey = `oidc.user: ${environment.authConfig}:${environment.authConfig.client_id}`;

  constructor(private router: Router) {

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
          localStorage.setItem(this.userKey, JSON.stringify(user));
          observer.next(user);
          observer.complete();
        });
    });
  }

  private tryGetUser() {
    return Observable.fromPromise(this.manager.getUser());
  }

  logout() {
    this.manager.getUser().then(user => {
      return this.manager.signoutRedirect({ id_token_hint: user.id_token }).then(resp => {
        console.log('signed out', resp);
        setTimeout(5000, () => {
          console.log('testing to see if fired...');
        });
      }).catch(function (err) {
        console.log(err);
      });
    });
    // this.manager.signoutRedirect();
  }
  get type(): string {
    return 'Bearer';
  }

  get token(): string | null {
    const temp = localStorage.getItem(this.userKey);
    if (temp) {
      const user: User = JSON.parse(temp);
      return user.access_token;
    }
    return null;
  }

  get authorizationHeader(): string | null {
    if (this.token) {
      return `${this.type} ${this.token}`;
    }
    return null;
  }

  checkUser() {
    this.tryGetUser().subscribe((user: User) => {
      this.loginStatusChanged.emit(user);
    }, e => {
      this.loginStatusChanged.emit(null);
    });
  }

}
