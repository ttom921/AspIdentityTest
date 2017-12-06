import { Component } from '@angular/core';
import { OAuthService, JwksValidationHandler } from 'angular-oauth2-oidc';
import { authConfig } from './auth.config';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent {
  title = 'app';
  authorityurl = 'http://localhost:5000';
  // 設定
  constructor(private oauthService: OAuthService) {
  }
  private configureWithNewConfigApi() {
    this.oauthService.configure(authConfig);
    this.oauthService.tokenValidationHandler = new JwksValidationHandler();
    this.oauthService.loadDiscoveryDocumentAndTryLogin();
    // Optional
    this.oauthService.setupAutomaticSilentRefresh();
    this.oauthService.events.subscribe(e => {
      console.log('oauth/oidc event', e);
    });

    this.oauthService.events.filter(e => e.type === 'session_terminated').subscribe(e => {
      console.log('Your session has been terminated!');
    });
    this.oauthService.events.filter(e => e.type === 'token_received').subscribe(e => {
      // this.oauthService.loadUserProfile();
    });
  }
  private configFirst() {

    // this.oauthService.loginUrl = "https://steyer-identity-server.azurewebsites.net/identity/connect/authorize"; //Id-Provider?
    // this.oauthService.logoutUrl = "https://steyer-identity-server.azurewebsites.net/identity/connect/endsession?id_token={{id_token}}";
    this.oauthService.loginUrl = this.authorityurl + '/connect/authorize'; // Id-Provider?
    this.oauthService.logoutUrl = this.authorityurl + '/connect/endsession?id_token={{id_token}}';
    this.oauthService.clientId = 'sales';
    this.oauthService.redirectUri = window.location.origin + '/login-callback';
    this.oauthService.responseType = 'id_token token';
    this.oauthService.scope = 'openid profile email salesapi';
    this.oauthService.postLogoutRedirectUri = window.location.origin;
    // tslint:disable-next-line:no-trailing-whitespace

    this.oauthService.issuer = this.authorityurl;
    this.oauthService.oidc = true;
    // tslint:disable-next-line:no-trailing-whitespace

    this.oauthService.tryLogin({});
  }
}
