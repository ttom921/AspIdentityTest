import { Injectable, EventEmitter } from '@angular/core';
import { Http, Headers, RequestOptions, Response } from '@angular/http';
import { Observable } from 'rxjs/Observable';
import 'rxjs/add/observable/fromPromise';
import 'rxjs/add/operator/map';

import { UserManager, User, Log, WebStorageStateStore } from 'oidc-client';

const settings: any = {
  // the identity server uri
  authority : 'http://localhost:5000',
  client_id: 'sales',
  redirect_uri: 'http://localhost:4200/signin-callback.html',
  post_logout_redirect_uri: 'http://localhost:4200',
  response_type: 'id_token token',
  scope: 'openid profile email salesapi',

  silent_redirect_uri: 'http://localhost:4200/silent-renew-callback.html',
  automaticSilentRenew: true,
  accessTokenExpiringNotificationTime: 10,
  // silentRequestTimeout: 10000,
  filterProtocolClaims: true,
  loadUserInfo: true
};

Log.logger = console;
Log.level = Log.DEBUG;

@Injectable()
export class AuthService {
  mgr: UserManager = new UserManager(settings);
  userLoadededEvent: EventEmitter<User> = new EventEmitter<User>();
  currentUser: User;
  loggedIn = false;

  authHeaders: Headers;
  constructor(private http: Http) {
    this.mgr.getUser()
      .then((user) => {
        if (user) {
          this.loggedIn = true;
          this.currentUser = user;
          this.userLoadededEvent.emit(user);
        } else {
          this.loggedIn = false;
        }
      })
      .catch((err) => {
        this.loggedIn = false;
      });
      this.mgr.events.addUserLoaded((user) => {
        this.currentUser = user;
          console.log('authService addUserLoaded', user);
      });

      this.mgr.events.addUserUnloaded((e) => {
        console.log('user unloaded');
      this.loggedIn = false;
    });
   }

   isLoggedInObs(): Observable<boolean> {
    return Observable.fromPromise(this.mgr.getUser()).map<User, boolean>((user) => {
      if (user) {
        return true;
      } else {
        return false;
      }
    });
  }

  clearState() {
    this.mgr.clearStaleState().then(function () {
      console.log('clearStateState success');
    }).catch(function (e) {
      console.log('clearStateState error', e.message);
    });
  }

  getUser() {
    this.mgr.getUser().then((user) => {
      this.currentUser = user;
      console.log('got user', user);
      this.userLoadededEvent.emit(user);
    }).catch(function (err) {
      console.log(err);
    });
  }

  removeUser() {
    this.mgr.removeUser().then(() => {
      this.userLoadededEvent.emit(null);
      console.log('user removed');
    }).catch(function (err) {
      console.log(err);
    });
  }

  startSigninMainWindow() {
    this.mgr.signinRedirect({ data: 'some data' }).then(function () {
      console.log('signinRedirect done');
    }).catch(function (err) {
      console.log(err);
    });
  }

  endSigninMainWindow() {
    this.mgr.signinRedirectCallback().then(function (user) {
      console.log('signed in', user);
    }).catch(function (err) {
      console.log(err);
    });
  }

  startSignoutMainWindow() {
    this.mgr.signoutRedirect().then(function (resp) {
      console.log('signed out', resp);
      setTimeout(5000, () => {
        console.log('testing to see if fired...');

      });
    }).catch(function (err) {
      console.log(err);
    });
  }

  endSignoutMainWindow() {
    this.mgr.signoutRedirectCallback().then(function (resp) {
      console.log('signed out', resp);
    }).catch(function (err) {
      console.log(err);
    });
  }
}
