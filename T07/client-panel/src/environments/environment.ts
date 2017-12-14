import { WebStorageStateStore } from 'oidc-client';

// The file contents for the current environment will overwrite these during build.
// The build system defaults to the dev environment which uses `environment.ts`, but if you do
// `ng build --env=prod` then `environment.prod.ts` will be used instead.
// The list of which env maps to which file can be found in `.angular-cli.json`.

export const environment = {
  production: false,
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
