import { Injectable, OnInit, EventEmitter  } from '@angular/core';
import { Log, UserManager, User } from 'oidc-client';
import { Observable } from 'rxjs/Observable';
import 'rxjs/add/observable/fromPromise';

const config: any = {
  authority: 'http://localhost:5000',
  client_id: 'corejs',
  redirect_uri: 'http://localhost:4200/login-callback',
  response_type: 'id_token token',
  scope: 'openid profile coreapi',
  post_logout_redirect_uri: 'http://localhost:4200/index.html'
};

Log.logger = console;
Log.level = Log.DEBUG;

@Injectable()
export class AuthService implements OnInit {
  private manager: UserManager = new UserManager(config);
  public loginStatusChanged: EventEmitter<User>;

  ngOnInit(): void {

  }

  constructor() {
    this.loginStatusChanged = new EventEmitter();
  }

  login() {
    this.manager.signinRedirect();
  }
  loginCallBack() {
    return Observable.create(observer => {
      Observable.fromPromise(this.manager.signinRedirectCallback())
        .subscribe(() => {
          this.tryGetUser().subscribe((user: User) => {
              this.loginStatusChanged.emit(user);
              observer.next(user);
              observer.complete();
          }, e => {
            observer.error(e);
          });
        });
    });
  }
  chekkUser() {
    this.tryGetUser().subscribe((user: User) => {
      this.loginStatusChanged.emit(user);
    }, e => {
      this.loginStatusChanged.emit(null);
    });
  }
  logout() {
    this.manager.signoutRedirect();
  }
  
  private tryGetUser() {
    return Observable.fromPromise(this.manager.getUser());
  }
}
