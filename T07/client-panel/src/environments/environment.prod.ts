import { WebStorageStateStore } from 'oidc-client';

export const environment = {
  production: true,
  authConfig: {
    authority: 'http://localhost:5000',
    client_id: 'sales',
    redirect_uri: 'http://localhost:4200/login-callback',
    response_type: 'id_token token',
    scope: 'openid profile email salesapi',
    // post_logout_redirect_uri: 'http://localhost:4200',
    silent_redirect_uri: 'http://localhost:4200/silent-renew.html',
    automaticSilentRenew: true,
    // silentRequestTimeout: 6000,
    accessTokenExpiringNotificationTime: 60,
    userStore: new WebStorageStateStore({ store: window.localStorage })
  }
};
